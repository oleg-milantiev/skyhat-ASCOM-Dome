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

            Dome.LogMessage("SetupDialogForm", "start");

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

            if (dome.connectedState)
            {
                Properties.Settings.Default.First = firstLeft.Checked ? 'l' : 'r';
                Properties.Settings.Default.Move = moveLeft.Checked ? 'l' : (moveRight.Checked ? 'r' : 'a');
                Properties.Settings.Default.Timeout = (int)timeout.Value;
                Properties.Settings.Default.Brightness = (int)brightness.Value;
                Properties.Settings.Default.Threshold = (int)threshold.Value;
                Properties.Settings.Default.MaxSpeed = (int)maxSpeed.Value;
                Properties.Settings.Default.Velocity = (int)velocity.Value;

                dome.SerialDisconnect();
            }
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
            Dome.LogMessage("SetupDialogForm InitUI", "start");
            chkTrace.Checked = Dome.tl.Enabled;

            refresh.Font = new Font("Wingdings 3", 20, FontStyle.Bold);
            refresh.Text = Char.ConvertFromUtf32(80); // or 80
            refresh.Width = 35;
            refresh.Height = 35;

            refreshCom();
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
            Dome.LogMessage("SetupDialogForm comPort Changed", "start");
            Properties.Settings.Default.ComPortString = comboBoxComPort.SelectedItem.ToString();            
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            refreshCom();
        }

        private void refreshCom()
        {
            Dome.LogMessage("SetupDialogForm refreshCom", "start");

            // set the list of com ports to those that are currently available
            comboBoxComPort.Items.Clear();
            comboBoxComPort.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());      // use System.IO because it's static
            // select the current port if possible
            if (comboBoxComPort.Items.Contains(Properties.Settings.Default.ComPortString))
            {
                Dome.LogMessage("SetupDialogForm refreshCom", "set active port");

                comboBoxComPort.SelectedItem = Properties.Settings.Default.ComPortString;
 //               comboBoxComPort_SelectedIndexChanged(null, null);
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
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

                    if (!dome.SerialCommand_GetEEPROM())
                    {
                        MessageBox.Show("SetupDialog: No SkyHat at port " + dome.serial.PortName + ". Please choose another");

                        dome.SerialDisconnect();

                        return;
                    }

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

                    buttonConnect.Enabled = false;
                    comboBoxComPort.Enabled = false;
                    refresh.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Dome.LogMessage("SetupDialog Exception", ex.Message);

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

                buttonConnect.Enabled = true;
                comboBoxComPort.Enabled = true;
                refresh.Enabled = true;
            }
        }
    }
}