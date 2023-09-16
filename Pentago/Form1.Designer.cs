namespace Pentago
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.turnCounter = new System.Windows.Forms.PictureBox();
            this.pve = new System.Windows.Forms.PictureBox();
            this.pvp = new System.Windows.Forms.PictureBox();
            this.boardBackground = new System.Windows.Forms.PictureBox();
            this.watermark = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.turnCounter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pve)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pvp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // turnCounter
            // 
            this.turnCounter.BackColor = System.Drawing.Color.White;
            this.turnCounter.Location = new System.Drawing.Point(415, 415);
            this.turnCounter.Name = "turnCounter";
            this.turnCounter.Size = new System.Drawing.Size(15, 15);
            this.turnCounter.TabIndex = 2;
            this.turnCounter.TabStop = false;
            this.turnCounter.Visible = false;
            // 
            // pve
            // 
            this.pve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(31)))), ((int)(((byte)(36)))));
            this.pve.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pve.Image = global::Pentago.Properties.Resources.pve;
            this.pve.Location = new System.Drawing.Point(92, 555);
            this.pve.Name = "pve";
            this.pve.Size = new System.Drawing.Size(245, 152);
            this.pve.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pve.TabIndex = 1;
            this.pve.TabStop = false;
            this.pve.Click += new System.EventHandler(this.mode_Click);
            // 
            // pvp
            // 
            this.pvp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(31)))), ((int)(((byte)(36)))));
            this.pvp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pvp.Image = global::Pentago.Properties.Resources.pvp;
            this.pvp.Location = new System.Drawing.Point(508, 140);
            this.pvp.Name = "pvp";
            this.pvp.Size = new System.Drawing.Size(245, 152);
            this.pvp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pvp.TabIndex = 1;
            this.pvp.TabStop = false;
            this.pvp.Click += new System.EventHandler(this.mode_Click);
            // 
            // boardBackground
            // 
            this.boardBackground.Image = global::Pentago.Properties.Resources.menu;
            this.boardBackground.Location = new System.Drawing.Point(6, 6);
            this.boardBackground.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.boardBackground.Name = "boardBackground";
            this.boardBackground.Size = new System.Drawing.Size(835, 835);
            this.boardBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.boardBackground.TabIndex = 0;
            this.boardBackground.TabStop = false;
            // 
            // watermark
            // 
            this.watermark.AutoSize = true;
            this.watermark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(31)))), ((int)(((byte)(36)))));
            this.watermark.Font = new System.Drawing.Font("Andy", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.watermark.ForeColor = System.Drawing.Color.White;
            this.watermark.Location = new System.Drawing.Point(67, 811);
            this.watermark.Name = "watermark";
            this.watermark.Size = new System.Drawing.Size(161, 21);
            this.watermark.TabIndex = 3;
            this.watermark.Text = "Omer Kohavi - 2022";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(847, 847);
            this.Controls.Add(this.watermark);
            this.Controls.Add(this.turnCounter);
            this.Controls.Add(this.pve);
            this.Controls.Add(this.pvp);
            this.Controls.Add(this.boardBackground);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pentago";
            ((System.ComponentModel.ISupportInitialize)(this.turnCounter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pve)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pvp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.boardBackground)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox boardBackground;
        private System.Windows.Forms.PictureBox pvp;
        private System.Windows.Forms.PictureBox pve;
        private System.Windows.Forms.PictureBox turnCounter;
        private System.Windows.Forms.Label watermark;
    }
}

