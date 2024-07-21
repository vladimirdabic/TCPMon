namespace TCPMon
{
    partial class MonitorPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.packetListPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.packetHexBox = new Be.Windows.Forms.HexBox();
            this.clearPackets = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // packetListPanel
            // 
            this.packetListPanel.AutoScroll = true;
            this.packetListPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.packetListPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.packetListPanel.Location = new System.Drawing.Point(6, 23);
            this.packetListPanel.Name = "packetListPanel";
            this.packetListPanel.Size = new System.Drawing.Size(190, 445);
            this.packetListPanel.TabIndex = 9;
            this.packetListPanel.WrapContents = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Incoming Packets";
            // 
            // packetHexBox
            // 
            this.packetHexBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.packetHexBox.ColumnInfoVisible = true;
            this.packetHexBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.packetHexBox.LineInfoVisible = true;
            this.packetHexBox.Location = new System.Drawing.Point(202, 3);
            this.packetHexBox.Name = "packetHexBox";
            this.packetHexBox.ReadOnly = true;
            this.packetHexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.packetHexBox.Size = new System.Drawing.Size(645, 465);
            this.packetHexBox.StringViewVisible = true;
            this.packetHexBox.TabIndex = 7;
            this.packetHexBox.UseFixedBytesPerLine = true;
            this.packetHexBox.VScrollBarVisible = true;
            // 
            // clearPackets
            // 
            this.clearPackets.Image = global::TCPMon.Properties.Resources.iks;
            this.clearPackets.Location = new System.Drawing.Point(176, 3);
            this.clearPackets.Name = "clearPackets";
            this.clearPackets.Size = new System.Drawing.Size(20, 20);
            this.clearPackets.TabIndex = 10;
            this.toolTip1.SetToolTip(this.clearPackets, "Clear");
            this.clearPackets.UseVisualStyleBackColor = true;
            this.clearPackets.Click += new System.EventHandler(this.clearPackets_Click);
            // 
            // MonitorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clearPackets);
            this.Controls.Add(this.packetListPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.packetHexBox);
            this.Name = "MonitorPanel";
            this.Size = new System.Drawing.Size(851, 471);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel packetListPanel;
        private System.Windows.Forms.Label label1;
        private Be.Windows.Forms.HexBox packetHexBox;
        private System.Windows.Forms.Button clearPackets;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
