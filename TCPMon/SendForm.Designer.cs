namespace TCPMon
{
    partial class SendForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendForm));
            this.packetHexBox = new Be.Windows.Forms.HexBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.connStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.sendButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // packetHexBox
            // 
            this.packetHexBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.packetHexBox.ColumnInfoVisible = true;
            this.packetHexBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.packetHexBox.LineInfoVisible = true;
            this.packetHexBox.Location = new System.Drawing.Point(12, 12);
            this.packetHexBox.Name = "packetHexBox";
            this.packetHexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.packetHexBox.Size = new System.Drawing.Size(645, 264);
            this.packetHexBox.StringViewVisible = true;
            this.packetHexBox.TabIndex = 8;
            this.packetHexBox.UseFixedBytesPerLine = true;
            this.packetHexBox.VScrollBarVisible = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connStrip});
            this.statusStrip1.Location = new System.Drawing.Point(0, 309);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(674, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // connStrip
            // 
            this.connStrip.Name = "connStrip";
            this.connStrip.Size = new System.Drawing.Size(75, 17);
            this.connStrip.Text = "Connection: ";
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(12, 282);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(645, 22);
            this.sendButton.TabIndex = 10;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // SendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 331);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.packetHexBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SendForm";
            this.Text = "TCPMon - Sender";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SendForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Be.Windows.Forms.HexBox packetHexBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel connStrip;
        private System.Windows.Forms.Button sendButton;
    }
}