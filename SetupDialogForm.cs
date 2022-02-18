using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ASCOM.Utilities;
using ASCOM.SkyHat;

namespace ASCOM.SkyHat
{
    [ComVisible(false)]					// Form not registered for COM!
    public partial class SetupDialogForm : Form
    {
        private Dome dome;
        
        public SetupDialogForm(Dome _dome)
        {
            dome = _dome;

            InitializeComponent();
            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void cmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            if (this.comboBoxComPort.SelectedItem != null)
                Properties.Settings.Default.ComPortString = comboBoxComPort.GetItemText(comboBoxComPort.SelectedItem);
            else
                Properties.Settings.Default.ComPortString = "(None)";

            Dome.tl.Enabled = chkTrace.Checked;

            Properties.Settings.Default.First = firstLeft.Checked ? 'l' : 'r';
            Properties.Settings.Default.Move = moveLeft.Checked ? 'l' : (moveRight.Checked ? 'r' : 'a');
            Properties.Settings.Default.Timeout = (int)timeout.Value;
            Properties.Settings.Default.Brightness = (int)brightness.Value;
            Properties.Settings.Default.Threshold = (int)threshold.Value;
            Properties.Settings.Default.MaxSpeed  = (int) maxSpeed.Value;
            Properties.Settings.Default.Velocity  = (int) velocity.Value;
        }

        private void cmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("http://ascom-standards.org/");
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {
            chkTrace.Checked = Dome.tl.Enabled;
            // set the list of com ports to those that are currently available
            comboBoxComPort.Items.Clear();
            comboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());      // use System.IO because it's static
            // select the current port if possible
            if (comboBoxComPort.Items.Contains(Properties.Settings.Default.ComPortString))
            {
                comboBoxComPort.SelectedItem = Properties.Settings.Default.ComPortString;
                comboBoxComPort_SelectedIndexChanged(null, null);
            }
        }

        private void moveLeft_CheckedChanged(object sender, EventArgs e)
        {
            firstLeft.Enabled = false;
            firstRight.Enabled = false;
        }

        private void moveRight_CheckedChanged(object sender, EventArgs e)
        {
            firstLeft.Enabled = false;
            firstRight.Enabled = false;
        }

        private void moveBoth_CheckedChanged(object sender, EventArgs e)
        {
            firstLeft.Enabled = true;
            firstRight.Enabled = true;
        }

        private void comboBoxComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ComPortString = comboBoxComPort.SelectedItem.ToString();

            try
            {
                dome.SerialConnect();

                if (dome.serial.Connected == true)
                {
                    Dome.LogMessage("SetupDialog Connected:", "objSerial.Connected = true");
                    dome.connectedState = true;

                    // wait for time specified in DelayOnConnect
                    //System.Threading.Thread.Sleep(1000);

                    Dome.LogMessage("SetupDialog Connected", Convert.ToString(dome.serial.Connected));

                    dome.SerialCommand_GetEEPROM();

                    moveLeft.Enabled = true;
                    moveRight.Enabled = true;
                    moveBoth.Enabled = true;

                    timeout.Enabled = true;
                    brightness.Enabled = true;
                    threshold.Enabled = true;
                    maxSpeed.Enabled = true;
                    velocity.Enabled = true;

                    moveLeft.Checked = (Properties.Settings.Default.Move == 'l');
                    moveRight.Checked = (Properties.Settings.Default.Move == 'r');
                    moveBoth.Checked = (Properties.Settings.Default.Move == 'a');

                    firstLeft.Checked = (Properties.Settings.Default.First == 'l');
                    firstRight.Checked = (Properties.Settings.Default.First == 'r');

                    firstLeft.Enabled = moveBoth.Checked;
                    firstRight.Enabled = moveBoth.Checked;

                    timeout.Value = Properties.Settings.Default.Timeout;
                    brightness.Value = Properties.Settings.Default.Brightness;
                    threshold.Value = Properties.Settings.Default.Threshold;
                    maxSpeed.Value = Properties.Settings.Default.MaxSpeed;
                    velocity.Value = Properties.Settings.Default.Velocity;
                }
            }
            catch (Exception ex)
            {
                moveLeft.Enabled = false;
                moveRight.Enabled = false;
                moveBoth.Enabled = false;

                firstLeft.Enabled = false;
                firstRight.Enabled = false;

                timeout.Enabled = false;
                brightness.Enabled = false;
                threshold.Enabled = false;
                maxSpeed.Enabled = false;
                velocity.Enabled = false;
            }
        }
    }
}