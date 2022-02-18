namespace ASCOM.SkyHat
{
    partial class SetupDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.brightness = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.firstLeft = new System.Windows.Forms.RadioButton();
            this.firstRight = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.moveLeft = new System.Windows.Forms.RadioButton();
            this.moveBoth = new System.Windows.Forms.RadioButton();
            this.moveRight = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.velocity = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.maxSpeed = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.threshold = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.timeout = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightness)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.velocity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeout)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(397, 376);
            this.cmdOK.Margin = new System.Windows.Forms.Padding(4);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(79, 30);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(397, 413);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(4);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(79, 31);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "MO SkyFlat Telescope  motorised cap\r\nASCOM Driver";
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = global::ASCOM.SkyHat.Properties.Resources.ASCOM;
            this.picASCOM.Location = new System.Drawing.Point(411, 11);
            this.picASCOM.Margin = new System.Windows.Forms.Padding(4);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 56);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 3;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            this.picASCOM.DoubleClick += new System.EventHandler(this.BrowseToAscom);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Comm Port";
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.Location = new System.Drawing.Point(255, 64);
            this.chkTrace.Margin = new System.Windows.Forms.Padding(4);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(87, 21);
            this.chkTrace.TabIndex = 6;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(102, 62);
            this.comboBoxComPort.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(119, 24);
            this.comboBoxComPort.TabIndex = 7;
            this.comboBoxComPort.SelectedIndexChanged += new System.EventHandler(this.comboBoxComPort_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.brightness);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.velocity);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.maxSpeed);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.threshold);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.timeout);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(19, 118);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(457, 251);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advanced";
            // 
            // brightness
            // 
            this.brightness.Enabled = false;
            this.brightness.Location = new System.Drawing.Point(233, 202);
            this.brightness.Maximum = 255;
            this.brightness.Name = "brightness";
            this.brightness.Size = new System.Drawing.Size(218, 56);
            this.brightness.TabIndex = 26;
            this.brightness.Value = 128;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 202);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 17);
            this.label5.TabIndex = 25;
            this.label5.Text = "EL Panel Brightness";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.firstLeft);
            this.panel2.Controls.Add(this.firstRight);
            this.panel2.Location = new System.Drawing.Point(233, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(209, 28);
            this.panel2.TabIndex = 9;
            // 
            // firstLeft
            // 
            this.firstLeft.AutoSize = true;
            this.firstLeft.Enabled = false;
            this.firstLeft.Location = new System.Drawing.Point(5, 4);
            this.firstLeft.Name = "firstLeft";
            this.firstLeft.Size = new System.Drawing.Size(53, 21);
            this.firstLeft.TabIndex = 7;
            this.firstLeft.TabStop = true;
            this.firstLeft.Text = "Left";
            this.firstLeft.UseVisualStyleBackColor = true;
            // 
            // firstRight
            // 
            this.firstRight.AutoSize = true;
            this.firstRight.Enabled = false;
            this.firstRight.Location = new System.Drawing.Point(64, 4);
            this.firstRight.Name = "firstRight";
            this.firstRight.Size = new System.Drawing.Size(62, 21);
            this.firstRight.TabIndex = 8;
            this.firstRight.TabStop = true;
            this.firstRight.Text = "Right";
            this.firstRight.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.moveLeft);
            this.panel1.Controls.Add(this.moveBoth);
            this.panel1.Controls.Add(this.moveRight);
            this.panel1.Location = new System.Drawing.Point(233, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 31);
            this.panel1.TabIndex = 9;
            // 
            // moveLeft
            // 
            this.moveLeft.AutoSize = true;
            this.moveLeft.Enabled = false;
            this.moveLeft.Location = new System.Drawing.Point(5, 3);
            this.moveLeft.Name = "moveLeft";
            this.moveLeft.Size = new System.Drawing.Size(53, 21);
            this.moveLeft.TabIndex = 25;
            this.moveLeft.TabStop = true;
            this.moveLeft.Text = "Left";
            this.moveLeft.UseVisualStyleBackColor = true;
            this.moveLeft.CheckedChanged += new System.EventHandler(this.moveLeft_CheckedChanged);
            // 
            // moveBoth
            // 
            this.moveBoth.AutoSize = true;
            this.moveBoth.Enabled = false;
            this.moveBoth.Location = new System.Drawing.Point(132, 3);
            this.moveBoth.Name = "moveBoth";
            this.moveBoth.Size = new System.Drawing.Size(58, 21);
            this.moveBoth.TabIndex = 27;
            this.moveBoth.TabStop = true;
            this.moveBoth.Text = "Both";
            this.moveBoth.UseVisualStyleBackColor = true;
            this.moveBoth.CheckedChanged += new System.EventHandler(this.moveBoth_CheckedChanged);
            // 
            // moveRight
            // 
            this.moveRight.AutoSize = true;
            this.moveRight.Enabled = false;
            this.moveRight.Location = new System.Drawing.Point(64, 3);
            this.moveRight.Name = "moveRight";
            this.moveRight.Size = new System.Drawing.Size(62, 21);
            this.moveRight.TabIndex = 26;
            this.moveRight.TabStop = true;
            this.moveRight.Text = "Right";
            this.moveRight.UseVisualStyleBackColor = true;
            this.moveRight.CheckedChanged += new System.EventHandler(this.moveRight_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 36);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 17);
            this.label4.TabIndex = 24;
            this.label4.Text = "Move part:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(366, 173);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 17);
            this.label12.TabIndex = 23;
            this.label12.Text = "0..255 PWM";
            // 
            // velocity
            // 
            this.velocity.Enabled = false;
            this.velocity.Location = new System.Drawing.Point(239, 171);
            this.velocity.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.velocity.Name = "velocity";
            this.velocity.Size = new System.Drawing.Size(120, 22);
            this.velocity.TabIndex = 22;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 173);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 17);
            this.label13.TabIndex = 21;
            this.label13.Text = "Motor velocity";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(366, 145);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 17);
            this.label10.TabIndex = 20;
            this.label10.Text = "0..255 PWM";
            // 
            // maxSpeed
            // 
            this.maxSpeed.Enabled = false;
            this.maxSpeed.Location = new System.Drawing.Point(239, 143);
            this.maxSpeed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.maxSpeed.Name = "maxSpeed";
            this.maxSpeed.Size = new System.Drawing.Size(120, 22);
            this.maxSpeed.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 145);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 17);
            this.label11.TabIndex = 18;
            this.label11.Text = "Motor maximum speed";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(366, 117);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 17);
            this.label8.TabIndex = 17;
            this.label8.Text = "ADU";
            // 
            // threshold
            // 
            this.threshold.Enabled = false;
            this.threshold.Location = new System.Drawing.Point(239, 115);
            this.threshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.threshold.Name = "threshold";
            this.threshold.Size = new System.Drawing.Size(120, 22);
            this.threshold.TabIndex = 16;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 117);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(165, 17);
            this.label9.TabIndex = 15;
            this.label9.Text = "Current sensor threshold";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(366, 89);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "seconds";
            // 
            // timeout
            // 
            this.timeout.Enabled = false;
            this.timeout.Location = new System.Drawing.Point(239, 87);
            this.timeout.Name = "timeout";
            this.timeout.Size = new System.Drawing.Size(120, 22);
            this.timeout.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 89);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Move timeout:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 62);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "First Hat part move:";
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 453);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxComPort);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SkyHat Setup";
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightness)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.velocity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.threshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown velocity;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown maxSpeed;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown threshold;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown timeout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton firstRight;
        private System.Windows.Forms.RadioButton firstLeft;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton moveBoth;
        private System.Windows.Forms.RadioButton moveRight;
        private System.Windows.Forms.RadioButton moveLeft;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar brightness;
        private System.Windows.Forms.Label label5;
    }
}