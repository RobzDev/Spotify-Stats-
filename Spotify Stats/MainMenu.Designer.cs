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
            MainFlowPanel = new FlowLayoutPanel();
            btnPlaylists = new Button();
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
            // MainFlowPanel
            // 
            MainFlowPanel.AutoScroll = true;
            MainFlowPanel.BorderStyle = BorderStyle.FixedSingle;
            MainFlowPanel.FlowDirection = FlowDirection.TopDown;
            MainFlowPanel.Location = new Point(412, 62);
            MainFlowPanel.Name = "MainFlowPanel";
            MainFlowPanel.Size = new Size(604, 564);
            MainFlowPanel.TabIndex = 3;
            MainFlowPanel.WrapContents = false;
            // 
            // btnPlaylists
            // 
            btnPlaylists.Location = new Point(74, 324);
            btnPlaylists.Name = "btnPlaylists";
            btnPlaylists.Size = new Size(147, 23);
            btnPlaylists.TabIndex = 4;
            btnPlaylists.Text = "See Playlists";
            btnPlaylists.UseVisualStyleBackColor = true;
            btnPlaylists.Click += btnPlaylists_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(1055, 683);
            Controls.Add(btnPlaylists);
            Controls.Add(btnLogOut);
            Controls.Add(lblUsername);
            Controls.Add(pboxUserPhoto);
            Controls.Add(MainFlowPanel);
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
        private FlowLayoutPanel MainFlowPanel;
        private Button btnPlaylists;
    }
}