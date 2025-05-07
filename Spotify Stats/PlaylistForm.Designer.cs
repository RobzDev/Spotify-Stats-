namespace Spotify_Stats
{
    partial class PlaylistForm
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
            lblUsername = new Label();
            pboxUserPhoto = new PictureBox();
            pbPlaylistImage = new PictureBox();
            lblPlaylistName = new Label();
            ((System.ComponentModel.ISupportInitialize)pboxUserPhoto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbPlaylistImage).BeginInit();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Yu Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.ForeColor = Color.Green;
            lblUsername.Location = new Point(79, 25);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(80, 25);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Default";
            // 
            // pboxUserPhoto
            // 
            pboxUserPhoto.Location = new Point(12, 12);
            pboxUserPhoto.Name = "pboxUserPhoto";
            pboxUserPhoto.Size = new Size(61, 48);
            pboxUserPhoto.TabIndex = 3;
            pboxUserPhoto.TabStop = false;
            // 
            // pbPlaylistImage
            // 
            pbPlaylistImage.Location = new Point(263, 12);
            pbPlaylistImage.Name = "pbPlaylistImage";
            pbPlaylistImage.Size = new Size(158, 148);
            pbPlaylistImage.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPlaylistImage.TabIndex = 4;
            pbPlaylistImage.TabStop = false;
            // 
            // lblPlaylistName
            // 
            lblPlaylistName.AutoSize = true;
            lblPlaylistName.Font = new Font("Yu Gothic UI Semibold", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPlaylistName.ForeColor = Color.Green;
            lblPlaylistName.Location = new Point(300, 163);
            lblPlaylistName.Name = "lblPlaylistName";
            lblPlaylistName.Size = new Size(86, 37);
            lblPlaylistName.TabIndex = 5;
            lblPlaylistName.Text = "label1";
            // 
            // PlaylistForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(728, 439);
            Controls.Add(lblPlaylistName);
            Controls.Add(pbPlaylistImage);
            Controls.Add(pboxUserPhoto);
            Controls.Add(lblUsername);
            Name = "PlaylistForm";
            Text = "Playlist";
            FormClosed += PlaylistForm_FormClosed;
            ((System.ComponentModel.ISupportInitialize)pboxUserPhoto).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbPlaylistImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUsername;
        private PictureBox pboxUserPhoto;
        private PictureBox pbPlaylistImage;
        private Label lblPlaylistName;
    }
}