namespace Spotify_Stats
{
    partial class MainMenu
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
            pboxUserPhoto = new PictureBox();
            lblUsername = new Label();
            btnLogOut = new Button();
            recentlyPlayedPanel = new FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)pboxUserPhoto).BeginInit();
            SuspendLayout();
            // 
            // pboxUserPhoto
            // 
            pboxUserPhoto.Location = new Point(36, 73);
            pboxUserPhoto.Name = "pboxUserPhoto";
            pboxUserPhoto.Size = new Size(236, 164);
            pboxUserPhoto.TabIndex = 0;
            pboxUserPhoto.TabStop = false;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Yu Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsername.ForeColor = Color.Green;
            lblUsername.Location = new Point(119, 240);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(80, 25);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Default";
            // 
            // btnLogOut
            // 
            btnLogOut.Location = new Point(110, 279);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(75, 23);
            btnLogOut.TabIndex = 2;
            btnLogOut.Text = "Log Out";
            btnLogOut.UseVisualStyleBackColor = true;
            btnLogOut.Click += btnLogOut_Click;
            // 
            // recentlyPlayedPanel
            // 
            recentlyPlayedPanel.AutoScroll = true;
            recentlyPlayedPanel.BorderStyle = BorderStyle.FixedSingle;
            recentlyPlayedPanel.FlowDirection = FlowDirection.TopDown;
            recentlyPlayedPanel.Location = new Point(345, 73);
            recentlyPlayedPanel.Name = "recentlyPlayedPanel";
            recentlyPlayedPanel.Size = new Size(531, 525);
            recentlyPlayedPanel.TabIndex = 3;
            recentlyPlayedPanel.WrapContents = false;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1055, 683);
            Controls.Add(recentlyPlayedPanel);
            Controls.Add(btnLogOut);
            Controls.Add(lblUsername);
            Controls.Add(pboxUserPhoto);
            Name = "MainMenu";
            FormClosed += MainMenu_FormClosed;
            ((System.ComponentModel.ISupportInitialize)pboxUserPhoto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pboxUserPhoto;
        private Label lblUsername;
        private Button btnLogOut;
        private FlowLayoutPanel recentlyPlayedPanel;
    }
}