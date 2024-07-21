namespace TCPMon
{
    partial class ConnectionControl
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
            this.conName = new System.Windows.Forms.Label();
            this.monitorButton = new System.Windows.Forms.Button();
            this.closeConn = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.sendData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // conName
            // 
            this.conName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.conName.Location = new System.Drawing.Point(-1, -1);
            this.conName.Name = "conName";
            this.conName.Size = new System.Drawing.Size(165, 18);
            this.conName.TabIndex = 0;
            this.conName.Text = "Name (addr)";
            this.conName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // monitorButton
            // 
            this.monitorButton.Image = global::TCPMon.Properties.Resources.monitoring;
            this.monitorButton.Location = new System.Drawing.Point(1, 19);
            this.monitorButton.Name = "monitorButton";
            this.monitorButton.Size = new System.Drawing.Size(26, 25);
            this.monitorButton.TabIndex = 2;
            this.monitorButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.monitorButton, "Data Monitor");
            this.monitorButton.UseVisualStyleBackColor = true;
            this.monitorButton.Click += new System.EventHandler(this.monitorButton_Click);
            // 
            // closeConn
            // 
            this.closeConn.Image = global::TCPMon.Properties.Resources.disconnect;
            this.closeConn.Location = new System.Drawing.Point(133, 19);
            this.closeConn.Name = "closeConn";
            this.closeConn.Size = new System.Drawing.Size(26, 25);
            this.closeConn.TabIndex = 1;
            this.closeConn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.closeConn, "Close Connection");
            this.closeConn.UseVisualStyleBackColor = true;
            this.closeConn.Click += new System.EventHandler(this.closeConn_Click);
            // 
            // sendData
            // 
            this.sendData.Image = global::TCPMon.Properties.Resources.hex;
            this.sendData.Location = new System.Drawing.Point(28, 19);
            this.sendData.Name = "sendData";
            this.sendData.Size = new System.Drawing.Size(26, 25);
            this.sendData.TabIndex = 3;
            this.sendData.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.sendData, "Send Data");
            this.sendData.UseVisualStyleBackColor = true;
            this.sendData.Click += new System.EventHandler(this.sendData_Click);
            // 
            // ConnectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.sendData);
            this.Controls.Add(this.monitorButton);
            this.Controls.Add(this.closeConn);
            this.Controls.Add(this.conName);
            this.Name = "ConnectionControl";
            this.Size = new System.Drawing.Size(163, 47);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label conName;
        private System.Windows.Forms.Button closeConn;
        private System.Windows.Forms.Button monitorButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button sendData;
    }
}
