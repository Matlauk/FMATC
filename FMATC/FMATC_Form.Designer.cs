namespace FMATC
{
    partial class FMATC_Form
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
            this.gbOutputDevices = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lstOACDevices = new System.Windows.Forms.ListBox();
            this.lstOAVDevices = new System.Windows.Forms.ListBox();
            this.gbInputDevices = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstIAVDevices = new System.Windows.Forms.ListBox();
            this.lstIACDevices = new System.Windows.Forms.ListBox();
            this.MenuBar = new System.Windows.Forms.MenuStrip();
            this.filleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Record = new System.Windows.Forms.Button();
            this.gbOutputDevices.SuspendLayout();
            this.gbInputDevices.SuspendLayout();
            this.MenuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOutputDevices
            // 
            this.gbOutputDevices.Controls.Add(this.label3);
            this.gbOutputDevices.Controls.Add(this.label4);
            this.gbOutputDevices.Controls.Add(this.lstOACDevices);
            this.gbOutputDevices.Controls.Add(this.lstOAVDevices);
            this.gbOutputDevices.Location = new System.Drawing.Point(12, 220);
            this.gbOutputDevices.Name = "gbOutputDevices";
            this.gbOutputDevices.Size = new System.Drawing.Size(619, 187);
            this.gbOutputDevices.TabIndex = 9;
            this.gbOutputDevices.TabStop = false;
            this.gbOutputDevices.Text = "Output Devices:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(309, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Acitve Devices:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Availiable Devices:";
            // 
            // lstOACDevices
            // 
            this.lstOACDevices.FormattingEnabled = true;
            this.lstOACDevices.HorizontalScrollbar = true;
            this.lstOACDevices.Location = new System.Drawing.Point(312, 32);
            this.lstOACDevices.Name = "lstOACDevices";
            this.lstOACDevices.Size = new System.Drawing.Size(300, 147);
            this.lstOACDevices.Sorted = true;
            this.lstOACDevices.TabIndex = 3;
            // 
            // lstOAVDevices
            // 
            this.lstOAVDevices.FormattingEnabled = true;
            this.lstOAVDevices.HorizontalScrollbar = true;
            this.lstOAVDevices.Location = new System.Drawing.Point(6, 32);
            this.lstOAVDevices.Name = "lstOAVDevices";
            this.lstOAVDevices.Size = new System.Drawing.Size(300, 147);
            this.lstOAVDevices.Sorted = true;
            this.lstOAVDevices.TabIndex = 2;
            // 
            // gbInputDevices
            // 
            this.gbInputDevices.Controls.Add(this.label2);
            this.gbInputDevices.Controls.Add(this.label1);
            this.gbInputDevices.Controls.Add(this.lstIAVDevices);
            this.gbInputDevices.Controls.Add(this.lstIACDevices);
            this.gbInputDevices.Location = new System.Drawing.Point(12, 27);
            this.gbInputDevices.Name = "gbInputDevices";
            this.gbInputDevices.Size = new System.Drawing.Size(619, 187);
            this.gbInputDevices.TabIndex = 8;
            this.gbInputDevices.TabStop = false;
            this.gbInputDevices.Text = "Input Devices:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(309, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Acitve Devices:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Availiable Devices:";
            // 
            // lstIAVDevices
            // 
            this.lstIAVDevices.FormattingEnabled = true;
            this.lstIAVDevices.HorizontalScrollbar = true;
            this.lstIAVDevices.Location = new System.Drawing.Point(6, 32);
            this.lstIAVDevices.Name = "lstIAVDevices";
            this.lstIAVDevices.Size = new System.Drawing.Size(300, 147);
            this.lstIAVDevices.Sorted = true;
            this.lstIAVDevices.TabIndex = 0;
            // 
            // lstIACDevices
            // 
            this.lstIACDevices.FormattingEnabled = true;
            this.lstIACDevices.HorizontalScrollbar = true;
            this.lstIACDevices.Location = new System.Drawing.Point(312, 32);
            this.lstIACDevices.Name = "lstIACDevices";
            this.lstIACDevices.Size = new System.Drawing.Size(300, 147);
            this.lstIACDevices.Sorted = true;
            this.lstIACDevices.TabIndex = 1;
            // 
            // MenuBar
            // 
            this.MenuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filleToolStripMenuItem,
            this.editToolStripMenuItem});
            this.MenuBar.Location = new System.Drawing.Point(0, 0);
            this.MenuBar.Name = "MenuBar";
            this.MenuBar.Size = new System.Drawing.Size(643, 24);
            this.MenuBar.TabIndex = 10;
            this.MenuBar.Text = "menuStrip1";
            // 
            // filleToolStripMenuItem
            // 
            this.filleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.filleToolStripMenuItem.Name = "filleToolStripMenuItem";
            this.filleToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.filleToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferencesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.preferencesToolStripMenuItem.Text = "&Preferences";
            this.preferencesToolStripMenuItem.Click += new System.EventHandler(this.preferencesToolStripMenuItem_Click);
            // 
            // btn_Record
            // 
            this.btn_Record.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Record.Location = new System.Drawing.Point(12, 413);
            this.btn_Record.Name = "btn_Record";
            this.btn_Record.Size = new System.Drawing.Size(619, 50);
            this.btn_Record.TabIndex = 11;
            this.btn_Record.Text = "START RECORDING";
            this.btn_Record.UseVisualStyleBackColor = true;
            this.btn_Record.Click += new System.EventHandler(this.btn_Record_Click);
            // 
            // FMATC_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 477);
            this.Controls.Add(this.btn_Record);
            this.Controls.Add(this.gbOutputDevices);
            this.Controls.Add(this.gbInputDevices);
            this.Controls.Add(this.MenuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.MenuBar;
            this.Name = "FMATC_Form";
            this.Text = "FMATC";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gbOutputDevices.ResumeLayout(false);
            this.gbOutputDevices.PerformLayout();
            this.gbInputDevices.ResumeLayout(false);
            this.gbInputDevices.PerformLayout();
            this.MenuBar.ResumeLayout(false);
            this.MenuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOutputDevices;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lstOACDevices;
        private System.Windows.Forms.ListBox lstOAVDevices;
        private System.Windows.Forms.GroupBox gbInputDevices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstIAVDevices;
        private System.Windows.Forms.ListBox lstIACDevices;
        private System.Windows.Forms.MenuStrip MenuBar;
        private System.Windows.Forms.ToolStripMenuItem filleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.Button btn_Record;
    }
}

