namespace USDTSender
{
    partial class ActivationForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblInstruction;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnActivate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblAppName;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblInstruction = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnActivate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblAppName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            
            // lblInstruction
            this.lblInstruction.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular);
            this.lblInstruction.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            this.lblInstruction.Location = new System.Drawing.Point(120, 70);
            this.lblInstruction.Name = "lblInstruction";
            this.lblInstruction.Size = new System.Drawing.Size(300, 40);
            this.lblInstruction.Text = "Enter your activation key to unlock the application:";
            
            // txtKey
            this.txtKey.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtKey.Location = new System.Drawing.Point(120, 110);
            this.txtKey.MaxLength = 34;
            this.txtKey.Size = new System.Drawing.Size(300, 23);
            this.txtKey.BorderStyle = BorderStyle.FixedSingle;
            
            // btnActivate
            this.btnActivate.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnActivate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActivate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActivate.ForeColor = System.Drawing.Color.White;
            this.btnActivate.Location = new System.Drawing.Point(120, 150);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(300, 32);
            this.btnActivate.Text = "Activate";
            this.btnActivate.FlatAppearance.BorderSize = 0;
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            
            // pictureBox1
            this.pictureBox1.Image = SystemIcons.Shield.ToBitmap();
            this.pictureBox1.Location = new System.Drawing.Point(30, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            
            // lblAppName
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.lblAppName.Location = new System.Drawing.Point(0, 20);
            this.lblAppName.Size = new System.Drawing.Size(450, 30);
            this.lblAppName.Text = "USDT SENDER";
            this.lblAppName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            // ActivationForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(450, 220);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblAppName);
            this.Controls.Add(this.lblInstruction);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.btnActivate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ActivationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Activation Required";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}