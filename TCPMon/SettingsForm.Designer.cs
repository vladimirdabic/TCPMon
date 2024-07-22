namespace TCPMon
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.generalTabPage = new System.Windows.Forms.TabPage();
            this.blazeTabPage = new System.Windows.Forms.TabPage();
            this.printByteData = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.blzcPath = new System.Windows.Forms.TextBox();
            this.setBlazePath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.useDebug = new System.Windows.Forms.CheckBox();
            this.simpleModuleName = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.generalTabPage.SuspendLayout();
            this.blazeTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.generalTabPage);
            this.tabControl1.Controls.Add(this.blazeTabPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(409, 271);
            this.tabControl1.TabIndex = 0;
            // 
            // generalTabPage
            // 
            this.generalTabPage.Controls.Add(this.groupBox1);
            this.generalTabPage.Location = new System.Drawing.Point(4, 22);
            this.generalTabPage.Name = "generalTabPage";
            this.generalTabPage.Size = new System.Drawing.Size(401, 245);
            this.generalTabPage.TabIndex = 0;
            this.generalTabPage.Text = "General";
            this.generalTabPage.UseVisualStyleBackColor = true;
            // 
            // blazeTabPage
            // 
            this.blazeTabPage.Controls.Add(this.groupBox2);
            this.blazeTabPage.Controls.Add(this.label1);
            this.blazeTabPage.Controls.Add(this.setBlazePath);
            this.blazeTabPage.Controls.Add(this.blzcPath);
            this.blazeTabPage.Location = new System.Drawing.Point(4, 22);
            this.blazeTabPage.Name = "blazeTabPage";
            this.blazeTabPage.Size = new System.Drawing.Size(401, 245);
            this.blazeTabPage.TabIndex = 1;
            this.blazeTabPage.Text = "Blaze";
            this.blazeTabPage.UseVisualStyleBackColor = true;
            // 
            // printByteData
            // 
            this.printByteData.AutoSize = true;
            this.printByteData.Location = new System.Drawing.Point(6, 19);
            this.printByteData.Name = "printByteData";
            this.printByteData.Size = new System.Drawing.Size(154, 17);
            this.printByteData.TabIndex = 0;
            this.printByteData.Text = "Print receive/sent data info";
            this.printByteData.UseVisualStyleBackColor = true;
            this.printByteData.CheckedChanged += new System.EventHandler(this.printByteData_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.printByteData);
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 234);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Console";
            // 
            // blzcPath
            // 
            this.blzcPath.Location = new System.Drawing.Point(19, 33);
            this.blzcPath.Name = "blzcPath";
            this.blzcPath.ReadOnly = true;
            this.blzcPath.Size = new System.Drawing.Size(298, 20);
            this.blzcPath.TabIndex = 0;
            // 
            // setBlazePath
            // 
            this.setBlazePath.Location = new System.Drawing.Point(323, 33);
            this.setBlazePath.Name = "setBlazePath";
            this.setBlazePath.Size = new System.Drawing.Size(28, 20);
            this.setBlazePath.TabIndex = 1;
            this.setBlazePath.Text = "...";
            this.setBlazePath.UseVisualStyleBackColor = true;
            this.setBlazePath.Click += new System.EventHandler(this.setBlazePath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Path to compiler (blzc.exe)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.simpleModuleName);
            this.groupBox2.Controls.Add(this.useDebug);
            this.groupBox2.Location = new System.Drawing.Point(19, 70);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 153);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Build parameters";
            // 
            // useDebug
            // 
            this.useDebug.AutoSize = true;
            this.useDebug.Location = new System.Drawing.Point(6, 19);
            this.useDebug.Name = "useDebug";
            this.useDebug.Size = new System.Drawing.Size(96, 17);
            this.useDebug.TabIndex = 0;
            this.useDebug.Text = "Use debug (-d)";
            this.useDebug.UseVisualStyleBackColor = true;
            this.useDebug.CheckedChanged += new System.EventHandler(this.useDebug_CheckedChanged);
            // 
            // simpleModuleName
            // 
            this.simpleModuleName.AutoSize = true;
            this.simpleModuleName.Location = new System.Drawing.Point(6, 42);
            this.simpleModuleName.Name = "simpleModuleName";
            this.simpleModuleName.Size = new System.Drawing.Size(127, 17);
            this.simpleModuleName.TabIndex = 1;
            this.simpleModuleName.Text = "Simplify module name";
            this.simpleModuleName.UseVisualStyleBackColor = true;
            this.simpleModuleName.CheckedChanged += new System.EventHandler(this.simpleModuleName_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 271);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "TCPMon - Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.generalTabPage.ResumeLayout(false);
            this.blazeTabPage.ResumeLayout(false);
            this.blazeTabPage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage generalTabPage;
        private System.Windows.Forms.TabPage blazeTabPage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox printByteData;
        private System.Windows.Forms.TextBox blzcPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button setBlazePath;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox useDebug;
        private System.Windows.Forms.CheckBox simpleModuleName;
    }
}