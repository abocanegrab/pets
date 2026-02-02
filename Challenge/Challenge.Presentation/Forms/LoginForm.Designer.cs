namespace Challenge.Presentation.Forms
{
    partial class LoginForm
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
            lblTitle = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnExit = new Button();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(50, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(300, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Dog Walking Management";
            //
            // lblUsername
            //
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(50, 100);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(60, 15);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Usuario:";
            //
            // txtUsername
            //
            txtUsername.Location = new Point(50, 120);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(300, 23);
            txtUsername.TabIndex = 2;
            //
            // lblPassword
            //
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(50, 160);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(70, 15);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Contraseña:";
            //
            // txtPassword
            //
            txtPassword.Location = new Point(50, 180);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(300, 23);
            txtPassword.TabIndex = 4;
            txtPassword.KeyPress += txtPassword_KeyPress;
            //
            // btnLogin
            //
            btnLogin.Location = new Point(50, 230);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(140, 35);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Iniciar Sesión";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            //
            // btnExit
            //
            btnExit.Location = new Point(210, 230);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(140, 35);
            btnExit.TabIndex = 6;
            btnExit.Text = "Salir";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            //
            // LoginForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 300);
            Controls.Add(btnExit);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login - Dog Walking";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnExit;
    }
}
