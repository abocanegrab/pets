namespace Challenge.Presentation.Forms
{
    partial class ClientManagementForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _presenter?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            grpData = new GroupBox();
            txtZipCode = new TextBox();
            lblZipCode = new Label();
            txtState = new TextBox();
            lblState = new Label();
            txtCity = new TextBox();
            lblCity = new Label();
            txtAddress = new TextBox();
            lblAddress = new Label();
            txtEmail = new TextBox();
            lblEmail = new Label();
            txtPhoneNumber = new TextBox();
            lblPhoneNumber = new Label();
            txtLastName = new TextBox();
            lblLastName = new Label();
            txtFirstName = new TextBox();
            lblFirstName = new Label();
            grpActions = new GroupBox();
            btnClear = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            grpSearch = new GroupBox();
            btnRefresh = new Button();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblSearch = new Label();
            grpList = new GroupBox();
            dgvClients = new DataGridView();
            grpData.SuspendLayout();
            grpActions.SuspendLayout();
            grpSearch.SuspendLayout();
            grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClients).BeginInit();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 120, 215);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(250, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Gestión de Clientes";
            //
            // grpData
            //
            grpData.Controls.Add(txtZipCode);
            grpData.Controls.Add(lblZipCode);
            grpData.Controls.Add(txtState);
            grpData.Controls.Add(lblState);
            grpData.Controls.Add(txtCity);
            grpData.Controls.Add(lblCity);
            grpData.Controls.Add(txtAddress);
            grpData.Controls.Add(lblAddress);
            grpData.Controls.Add(txtEmail);
            grpData.Controls.Add(lblEmail);
            grpData.Controls.Add(txtPhoneNumber);
            grpData.Controls.Add(lblPhoneNumber);
            grpData.Controls.Add(txtLastName);
            grpData.Controls.Add(lblLastName);
            grpData.Controls.Add(txtFirstName);
            grpData.Controls.Add(lblFirstName);
            grpData.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpData.Location = new Point(20, 60);
            grpData.Name = "grpData";
            grpData.Size = new Size(380, 320);
            grpData.TabIndex = 1;
            grpData.TabStop = false;
            grpData.Text = "Datos del Cliente";
            //
            // txtZipCode
            //
            txtZipCode.Location = new Point(220, 270);
            txtZipCode.Name = "txtZipCode";
            txtZipCode.Size = new Size(140, 23);
            txtZipCode.TabIndex = 15;
            //
            // lblZipCode
            //
            lblZipCode.AutoSize = true;
            lblZipCode.Font = new Font("Segoe UI", 9F);
            lblZipCode.Location = new Point(220, 252);
            lblZipCode.Name = "lblZipCode";
            lblZipCode.Size = new Size(87, 15);
            lblZipCode.TabIndex = 14;
            lblZipCode.Text = "Código Postal:";
            //
            // txtState
            //
            txtState.Location = new Point(20, 270);
            txtState.Name = "txtState";
            txtState.Size = new Size(180, 23);
            txtState.TabIndex = 13;
            //
            // lblState
            //
            lblState.AutoSize = true;
            lblState.Font = new Font("Segoe UI", 9F);
            lblState.Location = new Point(20, 252);
            lblState.Name = "lblState";
            lblState.Size = new Size(45, 15);
            lblState.TabIndex = 12;
            lblState.Text = "Estado:";
            //
            // txtCity
            //
            txtCity.Location = new Point(20, 220);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(340, 23);
            txtCity.TabIndex = 11;
            //
            // lblCity
            //
            lblCity.AutoSize = true;
            lblCity.Font = new Font("Segoe UI", 9F);
            lblCity.Location = new Point(20, 202);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(48, 15);
            lblCity.TabIndex = 10;
            lblCity.Text = "Ciudad:";
            //
            // txtAddress
            //
            txtAddress.Location = new Point(20, 170);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(340, 23);
            txtAddress.TabIndex = 9;
            //
            // lblAddress
            //
            lblAddress.AutoSize = true;
            lblAddress.Font = new Font("Segoe UI", 9F);
            lblAddress.Location = new Point(20, 152);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(60, 15);
            lblAddress.TabIndex = 8;
            lblAddress.Text = "Dirección:";
            //
            // txtEmail
            //
            txtEmail.Location = new Point(20, 120);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(340, 23);
            txtEmail.TabIndex = 7;
            //
            // lblEmail
            //
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9F);
            lblEmail.Location = new Point(20, 102);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 6;
            lblEmail.Text = "Email:";
            //
            // txtPhoneNumber
            //
            txtPhoneNumber.Location = new Point(220, 70);
            txtPhoneNumber.Name = "txtPhoneNumber";
            txtPhoneNumber.Size = new Size(140, 23);
            txtPhoneNumber.TabIndex = 5;
            //
            // lblPhoneNumber
            //
            lblPhoneNumber.AutoSize = true;
            lblPhoneNumber.Font = new Font("Segoe UI", 9F);
            lblPhoneNumber.Location = new Point(220, 52);
            lblPhoneNumber.Name = "lblPhoneNumber";
            lblPhoneNumber.Size = new Size(58, 15);
            lblPhoneNumber.TabIndex = 4;
            lblPhoneNumber.Text = "Teléfono:";
            //
            // txtLastName
            //
            txtLastName.Location = new Point(220, 30);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(140, 23);
            txtLastName.TabIndex = 3;
            //
            // lblLastName
            //
            lblLastName.AutoSize = true;
            lblLastName.Font = new Font("Segoe UI", 9F);
            lblLastName.Location = new Point(220, 12);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(54, 15);
            lblLastName.TabIndex = 2;
            lblLastName.Text = "Apellido:";
            //
            // txtFirstName
            //
            txtFirstName.Location = new Point(20, 30);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(180, 23);
            txtFirstName.TabIndex = 1;
            //
            // lblFirstName
            //
            lblFirstName.AutoSize = true;
            lblFirstName.Font = new Font("Segoe UI", 9F);
            lblFirstName.Location = new Point(20, 12);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(54, 15);
            lblFirstName.TabIndex = 0;
            lblFirstName.Text = "Nombre:";
            //
            // grpActions
            //
            grpActions.Controls.Add(btnClear);
            grpActions.Controls.Add(btnDelete);
            grpActions.Controls.Add(btnSave);
            grpActions.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpActions.Location = new Point(20, 390);
            grpActions.Name = "grpActions";
            grpActions.Size = new Size(380, 80);
            grpActions.TabIndex = 2;
            grpActions.TabStop = false;
            grpActions.Text = "Acciones";
            //
            // btnClear
            //
            btnClear.BackColor = Color.FromArgb(108, 117, 125);
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(260, 28);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(100, 35);
            btnClear.TabIndex = 2;
            btnClear.Text = "Limpiar";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            //
            // btnDelete
            //
            btnDelete.BackColor = Color.FromArgb(220, 53, 69);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(140, 28);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 35);
            btnDelete.TabIndex = 1;
            btnDelete.Text = "Eliminar";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            //
            // btnSave
            //
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(20, 28);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 35);
            btnSave.TabIndex = 0;
            btnSave.Text = "Guardar";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            //
            // grpSearch
            //
            grpSearch.Controls.Add(btnRefresh);
            grpSearch.Controls.Add(btnSearch);
            grpSearch.Controls.Add(txtSearch);
            grpSearch.Controls.Add(lblSearch);
            grpSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpSearch.Location = new Point(420, 60);
            grpSearch.Name = "grpSearch";
            grpSearch.Size = new Size(750, 80);
            grpSearch.TabIndex = 3;
            grpSearch.TabStop = false;
            grpSearch.Text = "Búsqueda";
            //
            // btnRefresh
            //
            btnRefresh.BackColor = Color.FromArgb(0, 123, 255);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(630, 28);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 35);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Actualizar";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            //
            // btnSearch
            //
            btnSearch.BackColor = Color.FromArgb(0, 123, 255);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(510, 28);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 35);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Buscar";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            //
            // txtSearch
            //
            txtSearch.Location = new Point(70, 35);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(420, 23);
            txtSearch.TabIndex = 1;
            txtSearch.KeyPress += txtSearch_KeyPress;
            //
            // lblSearch
            //
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9F);
            lblSearch.Location = new Point(20, 38);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(45, 15);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Buscar:";
            //
            // grpList
            //
            grpList.Controls.Add(dgvClients);
            grpList.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpList.Location = new Point(420, 150);
            grpList.Name = "grpList";
            grpList.Size = new Size(750, 320);
            grpList.TabIndex = 4;
            grpList.TabStop = false;
            grpList.Text = "Lista de Clientes";
            //
            // dgvClients
            //
            dgvClients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClients.Dock = DockStyle.Fill;
            dgvClients.Location = new Point(3, 19);
            dgvClients.Name = "dgvClients";
            dgvClients.Size = new Size(744, 298);
            dgvClients.TabIndex = 0;
            dgvClients.CellClick += dgvClients_CellClick;
            //
            // ClientManagementForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1190, 490);
            Controls.Add(grpList);
            Controls.Add(grpSearch);
            Controls.Add(grpActions);
            Controls.Add(grpData);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "ClientManagementForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Clientes - Dog Walking";
            grpData.ResumeLayout(false);
            grpData.PerformLayout();
            grpActions.ResumeLayout(false);
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvClients).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private GroupBox grpData;
        private TextBox txtFirstName;
        private Label lblFirstName;
        private TextBox txtLastName;
        private Label lblLastName;
        private TextBox txtPhoneNumber;
        private Label lblPhoneNumber;
        private TextBox txtEmail;
        private Label lblEmail;
        private TextBox txtAddress;
        private Label lblAddress;
        private TextBox txtCity;
        private Label lblCity;
        private TextBox txtState;
        private Label lblState;
        private TextBox txtZipCode;
        private Label lblZipCode;
        private GroupBox grpActions;
        private Button btnSave;
        private Button btnDelete;
        private Button btnClear;
        private GroupBox grpSearch;
        private TextBox txtSearch;
        private Label lblSearch;
        private Button btnSearch;
        private Button btnRefresh;
        private GroupBox grpList;
        private DataGridView dgvClients;
    }
}
