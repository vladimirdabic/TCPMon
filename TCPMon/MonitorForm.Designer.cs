namespace TCPMon
{
    partial class MonitorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.connStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.schemaStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.schemaButton = new System.Windows.Forms.ToolStripButton();
            this.monitorPanel = new TCPMon.MonitorPanel();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connStrip,
            this.schemaStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 504);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1043, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // connStrip
            // 
            this.connStrip.Name = "connStrip";
            this.connStrip.Size = new System.Drawing.Size(159, 17);
            this.connStrip.Text = "Connection: 192.168.1.1:8000";
            // 
            // schemaStatus
            // 
            this.schemaStatus.Name = "schemaStatus";
            this.schemaStatus.Size = new System.Drawing.Size(127, 17);
            this.schemaStatus.Text = "Current Schema: None";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.schemaButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(1043, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // schemaButton
            // 
            this.schemaButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.schemaButton.Image = global::TCPMon.Properties.Resources.binary;
            this.schemaButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.schemaButton.Name = "schemaButton";
            this.schemaButton.Size = new System.Drawing.Size(23, 22);
            this.schemaButton.ToolTipText = "Change Schema";
            this.schemaButton.Click += new System.EventHandler(this.schemaButton_Click);
            // 
            // monitorPanel
            // 
            this.monitorPanel.Location = new System.Drawing.Point(0, 28);
            this.monitorPanel.Name = "monitorPanel";
            this.monitorPanel.Size = new System.Drawing.Size(1040, 471);
            this.monitorPanel.TabIndex = 7;
            // 
            // MonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 526);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.monitorPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MonitorForm";
            this.Text = "TCPMon - Data Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MonitorForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MonitorPanel monitorPanel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel connStrip;
        private System.Windows.Forms.ToolStripStatusLabel schemaStatus;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton schemaButton;
    }
}