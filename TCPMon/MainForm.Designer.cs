namespace TCPMon
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.activeConns = new System.Windows.Forms.ToolStripStatusLabel();
            this.connectionPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.consoleBox = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newClientStrip = new System.Windows.Forms.ToolStripButton();
            this.closeConnsStrip = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.editorStrip = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsStrip = new System.Windows.Forms.ToolStripButton();
            this.helpToolstrip = new System.Windows.Forms.ToolStripDropDownButton();
            this.blazeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activeConns});
            this.statusStrip1.Location = new System.Drawing.Point(0, 501);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(708, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // activeConns
            // 
            this.activeConns.Name = "activeConns";
            this.activeConns.Size = new System.Drawing.Size(122, 17);
            this.activeConns.Text = "Active Connections: 0";
            // 
            // connectionPanel
            // 
            this.connectionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.connectionPanel.Location = new System.Drawing.Point(12, 27);
            this.connectionPanel.Name = "connectionPanel";
            this.connectionPanel.Size = new System.Drawing.Size(684, 327);
            this.connectionPanel.TabIndex = 5;
            // 
            // consoleBox
            // 
            this.consoleBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.consoleBox.HideSelection = false;
            this.consoleBox.Location = new System.Drawing.Point(12, 360);
            this.consoleBox.Name = "consoleBox";
            this.consoleBox.ReadOnly = true;
            this.consoleBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.consoleBox.Size = new System.Drawing.Size(684, 138);
            this.consoleBox.TabIndex = 6;
            this.consoleBox.Text = "";
            this.consoleBox.WordWrap = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newClientStrip,
            this.closeConnsStrip,
            this.toolStripSeparator3,
            this.editorStrip,
            this.toolStripSeparator2,
            this.settingsStrip,
            this.helpToolstrip});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(708, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newClientStrip
            // 
            this.newClientStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newClientStrip.Image = global::TCPMon.Properties.Resources.network_cool_two_pcs_4;
            this.newClientStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newClientStrip.Name = "newClientStrip";
            this.newClientStrip.Size = new System.Drawing.Size(23, 22);
            this.newClientStrip.ToolTipText = "New Client";
            this.newClientStrip.Click += new System.EventHandler(this.newClientStrip_Click);
            // 
            // closeConnsStrip
            // 
            this.closeConnsStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.closeConnsStrip.Image = global::TCPMon.Properties.Resources.disconnect;
            this.closeConnsStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeConnsStrip.Name = "closeConnsStrip";
            this.closeConnsStrip.Size = new System.Drawing.Size(23, 22);
            this.closeConnsStrip.ToolTipText = "Close All Connections";
            this.closeConnsStrip.Click += new System.EventHandler(this.closeConnsStrip_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // editorStrip
            // 
            this.editorStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editorStrip.Image = global::TCPMon.Properties.Resources.editor_icon;
            this.editorStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editorStrip.Name = "editorStrip";
            this.editorStrip.Size = new System.Drawing.Size(23, 22);
            this.editorStrip.ToolTipText = "Open Editor";
            this.editorStrip.Click += new System.EventHandler(this.editorStrip_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // settingsStrip
            // 
            this.settingsStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsStrip.Image = global::TCPMon.Properties.Resources.settings_gear_5;
            this.settingsStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsStrip.Name = "settingsStrip";
            this.settingsStrip.Size = new System.Drawing.Size(23, 22);
            this.settingsStrip.ToolTipText = "Settings";
            this.settingsStrip.Click += new System.EventHandler(this.settingsStrip_Click);
            // 
            // helpToolstrip
            // 
            this.helpToolstrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blazeToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolstrip.Image = global::TCPMon.Properties.Resources.chm_0;
            this.helpToolstrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolstrip.Name = "helpToolstrip";
            this.helpToolstrip.Size = new System.Drawing.Size(29, 22);
            // 
            // blazeToolStripMenuItem
            // 
            this.blazeToolStripMenuItem.Name = "blazeToolStripMenuItem";
            this.blazeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.blazeToolStripMenuItem.Text = "Blaze";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 523);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.consoleBox);
            this.Controls.Add(this.connectionPanel);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "TCPMon";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel activeConns;
        private System.Windows.Forms.FlowLayoutPanel connectionPanel;
        private System.Windows.Forms.RichTextBox consoleBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton editorStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton helpToolstrip;
        private System.Windows.Forms.ToolStripButton newClientStrip;
        private System.Windows.Forms.ToolStripButton closeConnsStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem blazeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton settingsStrip;
    }
}

