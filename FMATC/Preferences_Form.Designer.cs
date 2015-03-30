namespace FMATC
{
    partial class Preferences_Form
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
            this.lblRecordHotKey = new System.Windows.Forms.Label();
            this.txtHotkey = new System.Windows.Forms.TextBox();
            this.lblOutputLocationLabel = new System.Windows.Forms.Label();
            this.btnOutputLocation = new System.Windows.Forms.Button();
            this.lblOutputLocation = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblRecordHotKey
            // 
            this.lblRecordHotKey.AutoSize = true;
            this.lblRecordHotKey.Location = new System.Drawing.Point(12, 9);
            this.lblRecordHotKey.Name = "lblRecordHotKey";
            this.lblRecordHotKey.Size = new System.Drawing.Size(148, 13);
            this.lblRecordHotKey.TabIndex = 0;
            this.lblRecordHotKey.Text = "Start/Stop Recording Hotkey:";
            // 
            // txtHotkey
            // 
            this.txtHotkey.BackColor = System.Drawing.SystemColors.Window;
            this.txtHotkey.Location = new System.Drawing.Point(166, 6);
            this.txtHotkey.Name = "txtHotkey";
            this.txtHotkey.ReadOnly = true;
            this.txtHotkey.Size = new System.Drawing.Size(100, 20);
            this.txtHotkey.TabIndex = 1;
            this.txtHotkey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHotkey.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseDown);
            // 
            // lblOutputLocationLabel
            // 
            this.lblOutputLocationLabel.AutoSize = true;
            this.lblOutputLocationLabel.Location = new System.Drawing.Point(12, 37);
            this.lblOutputLocationLabel.Name = "lblOutputLocationLabel";
            this.lblOutputLocationLabel.Size = new System.Drawing.Size(86, 13);
            this.lblOutputLocationLabel.TabIndex = 2;
            this.lblOutputLocationLabel.Text = "Output Location:";
            // 
            // btnOutputLocation
            // 
            this.btnOutputLocation.Location = new System.Drawing.Point(104, 32);
            this.btnOutputLocation.Name = "btnOutputLocation";
            this.btnOutputLocation.Size = new System.Drawing.Size(75, 23);
            this.btnOutputLocation.TabIndex = 3;
            this.btnOutputLocation.Text = "Borwse";
            this.btnOutputLocation.UseVisualStyleBackColor = true;
            this.btnOutputLocation.Click += new System.EventHandler(this.btnOutputLocation_Click);
            // 
            // lblOutputLocation
            // 
            this.lblOutputLocation.AutoSize = true;
            this.lblOutputLocation.Location = new System.Drawing.Point(12, 58);
            this.lblOutputLocation.Name = "lblOutputLocation";
            this.lblOutputLocation.Size = new System.Drawing.Size(0, 13);
            this.lblOutputLocation.TabIndex = 4;
            // 
            // Preferences_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 88);
            this.Controls.Add(this.lblOutputLocation);
            this.Controls.Add(this.btnOutputLocation);
            this.Controls.Add(this.lblOutputLocationLabel);
            this.Controls.Add(this.txtHotkey);
            this.Controls.Add(this.lblRecordHotKey);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Preferences_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences_Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRecordHotKey;
        private System.Windows.Forms.TextBox txtHotkey;
        private System.Windows.Forms.Label lblOutputLocationLabel;
        private System.Windows.Forms.Button btnOutputLocation;
        private System.Windows.Forms.Label lblOutputLocation;
    }
}