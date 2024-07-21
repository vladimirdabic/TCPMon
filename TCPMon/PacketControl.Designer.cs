namespace TCPMon
{
    partial class PacketControl
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
            this.dateTimeLabel = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.hexButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimeLabel
            // 
            this.dateTimeLabel.AutoSize = true;
            this.dateTimeLabel.Location = new System.Drawing.Point(2, 5);
            this.dateTimeLabel.Name = "dateTimeLabel";
            this.dateTimeLabel.Size = new System.Drawing.Size(72, 13);
            this.dateTimeLabel.TabIndex = 0;
            this.dateTimeLabel.Text = "18:57 (21/07)";
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(2, 22);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(79, 13);
            this.infoLabel.TabIndex = 1;
            this.infoLabel.Text = "Size: 200 bytes";
            // 
            // hexButton
            // 
            this.hexButton.Image = global::TCPMon.Properties.Resources.hex;
            this.hexButton.Location = new System.Drawing.Point(128, 10);
            this.hexButton.Name = "hexButton";
            this.hexButton.Size = new System.Drawing.Size(26, 23);
            this.hexButton.TabIndex = 2;
            this.hexButton.UseVisualStyleBackColor = true;
            this.hexButton.Click += new System.EventHandler(this.hexButton_Click);
            // 
            // PacketControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.hexButton);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.dateTimeLabel);
            this.Name = "PacketControl";
            this.Size = new System.Drawing.Size(162, 42);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dateTimeLabel;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.Button hexButton;
    }
}
