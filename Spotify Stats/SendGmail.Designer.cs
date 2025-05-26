namespace Spotify_Stats
{
    partial class SendGmail
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
            panelLogin = new Panel();
            btnLogin = new Button();
            lblLogIn = new Label();
            panelLogic = new Panel();
            txtboxContent = new TextBox();
            lblEmailTo = new Label();
            txtboxTo = new TextBox();
            btnSendEmail = new Button();
            panelLogin.SuspendLayout();
            panelLogic.SuspendLayout();
            SuspendLayout();
            // 
            // panelLogin
            // 
            panelLogin.Controls.Add(btnLogin);
            panelLogin.Controls.Add(lblLogIn);
            panelLogin.Location = new Point(47, 52);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(270, 358);
            panelLogin.TabIndex = 0;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(75, 151);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(115, 49);
            btnLogin.TabIndex = 1;
            btnLogin.Text = "Log in";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblLogIn
            // 
            lblLogIn.AutoSize = true;
            lblLogIn.Location = new Point(93, 133);
            lblLogIn.Name = "lblLogIn";
            lblLogIn.Size = new Size(73, 15);
            lblLogIn.TabIndex = 0;
            lblLogIn.Text = "Please Login";
            // 
            // panelLogic
            // 
            panelLogic.Controls.Add(txtboxContent);
            panelLogic.Controls.Add(lblEmailTo);
            panelLogic.Controls.Add(txtboxTo);
            panelLogic.Controls.Add(btnSendEmail);
            panelLogic.Location = new Point(45, 52);
            panelLogic.Name = "panelLogic";
            panelLogic.Size = new Size(272, 356);
            panelLogic.TabIndex = 2;
            panelLogic.Visible = false;
            // 
            // txtboxContent
            // 
            txtboxContent.Location = new Point(-16, 201);
            txtboxContent.Multiline = true;
            txtboxContent.Name = "txtboxContent";
            txtboxContent.ReadOnly = true;
            txtboxContent.Size = new Size(305, 183);
            txtboxContent.TabIndex = 3;
            // 
            // lblEmailTo
            // 
            lblEmailTo.AutoSize = true;
            lblEmailTo.Location = new Point(32, 144);
            lblEmailTo.Name = "lblEmailTo";
            lblEmailTo.Size = new Size(27, 15);
            lblEmailTo.TabIndex = 2;
            lblEmailTo.Text = "For:";
            // 
            // txtboxTo
            // 
            txtboxTo.Location = new Point(32, 162);
            txtboxTo.Name = "txtboxTo";
            txtboxTo.Size = new Size(137, 23);
            txtboxTo.TabIndex = 1;
            // 
            // btnSendEmail
            // 
            btnSendEmail.Location = new Point(175, 162);
            btnSendEmail.Name = "btnSendEmail";
            btnSendEmail.Size = new Size(75, 23);
            btnSendEmail.TabIndex = 0;
            btnSendEmail.Text = "Send";
            btnSendEmail.UseVisualStyleBackColor = true;
            btnSendEmail.Click += btnSendEmail_Click;
            // 
            // SendGmail
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 457);
            Controls.Add(panelLogic);
            Controls.Add(panelLogin);
            Name = "SendGmail";
            Text = "SendGmail";
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            panelLogic.ResumeLayout(false);
            panelLogic.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLogin;
        private Button btnLogin;
        private Label lblLogIn;
        private Panel panelLogic;
        private Label lblEmailTo;
        private TextBox txtboxTo;
        private Button btnSendEmail;
        private TextBox txtboxContent;
    }
}