namespace Challenge.Presentation.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblWelcome = new Label();
            btnClients = new Button();
            btnDogs = new Button();
            btnWalks = new Button();
            btnLogout = new Button();
            btnExit = new Button();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(150, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Dog Walking Management System";
            //
            // lblWelcome
            //
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 10F);
            lblWelcome.Location = new Point(50, 80);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(100, 19);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "Bienvenido, ";
            //
            // btnClients
            //
            btnClients.Font = new Font("Segoe UI", 11F);
            btnClients.Location = new Point(250, 130);
            btnClients.Name = "btnClients";
            btnClients.Size = new Size(200, 60);
            btnClients.TabIndex = 2;
            btnClients.Text = "Gestionar Clientes";
            btnClients.UseVisualStyleBackColor = true;
            btnClients.Click += btnClients_Click;
            //
            // btnDogs
            //
            btnDogs.Font = new Font("Segoe UI", 11F);
            btnDogs.Location = new Point(250, 210);
            btnDogs.Name = "btnDogs";
            btnDogs.Size = new Size(200, 60);
            btnDogs.TabIndex = 3;
            btnDogs.Text = "Gestionar Perros";
            btnDogs.UseVisualStyleBackColor = true;
            btnDogs.Click += btnDogs_Click;
            //
            // btnWalks
            //
            btnWalks.Font = new Font("Segoe UI", 11F);
            btnWalks.Location = new Point(250, 290);
            btnWalks.Name = "btnWalks";
            btnWalks.Size = new Size(200, 60);
            btnWalks.TabIndex = 4;
            btnWalks.Text = "Gestionar Paseos";
            btnWalks.UseVisualStyleBackColor = true;
            btnWalks.Click += btnWalks_Click;
            //
            // btnLogout
            //
            btnLogout.Location = new Point(150, 400);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(150, 35);
            btnLogout.TabIndex = 5;
            btnLogout.Text = "Cerrar Sesión";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            //
            // btnExit
            //
            btnExit.Location = new Point(400, 400);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(150, 35);
            btnExit.TabIndex = 6;
            btnExit.Text = "Salir";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 480);
            Controls.Add(btnExit);
            Controls.Add(btnLogout);
            Controls.Add(btnWalks);
            Controls.Add(btnDogs);
            Controls.Add(btnClients);
            Controls.Add(lblWelcome);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dog Walking - Dashboard";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblWelcome;
        private Button btnClients;
        private Button btnDogs;
        private Button btnWalks;
        private Button btnLogout;
        private Button btnExit;
    }
}
