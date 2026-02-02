namespace Challenge.Presentation.Forms
{
    partial class DogManagementForm
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
            txtSpecialInstructions = new TextBox();
            lblSpecialInstructions = new Label();
            txtWeight = new TextBox();
            lblWeight = new Label();
            txtAge = new TextBox();
            lblAge = new Label();
            txtBreed = new TextBox();
            lblBreed = new Label();
            txtName = new TextBox();
            lblName = new Label();
            cmbClient = new ComboBox();
            lblClient = new Label();
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
            dgvDogs = new DataGridView();
            pnlPagination = new Panel();
            btnPrevious = new Button();
            lblPaginationInfo = new Label();
            btnNext = new Button();
            grpData.SuspendLayout();
            grpActions.SuspendLayout();
            grpSearch.SuspendLayout();
            grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDogs).BeginInit();
            pnlPagination.SuspendLayout();
            SuspendLayout();
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 120, 215);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(230, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Gestión de Perros";
            //
            // grpData
            //
            grpData.Controls.Add(txtSpecialInstructions);
            grpData.Controls.Add(lblSpecialInstructions);
            grpData.Controls.Add(txtWeight);
            grpData.Controls.Add(lblWeight);
            grpData.Controls.Add(txtAge);
            grpData.Controls.Add(lblAge);
            grpData.Controls.Add(txtBreed);
            grpData.Controls.Add(lblBreed);
            grpData.Controls.Add(txtName);
            grpData.Controls.Add(lblName);
            grpData.Controls.Add(cmbClient);
            grpData.Controls.Add(lblClient);
            grpData.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpData.Location = new Point(20, 60);
            grpData.Name = "grpData";
            grpData.Size = new Size(380, 360);
            grpData.TabIndex = 1;
            grpData.TabStop = false;
            grpData.Text = "Datos del Perro";
            //
            // txtSpecialInstructions
            //
            txtSpecialInstructions.Location = new Point(20, 260);
            txtSpecialInstructions.Multiline = true;
            txtSpecialInstructions.Name = "txtSpecialInstructions";
            txtSpecialInstructions.ScrollBars = ScrollBars.Vertical;
            txtSpecialInstructions.Size = new Size(340, 80);
            txtSpecialInstructions.TabIndex = 11;
            //
            // lblSpecialInstructions
            //
            lblSpecialInstructions.AutoSize = true;
            lblSpecialInstructions.Font = new Font("Segoe UI", 9F);
            lblSpecialInstructions.Location = new Point(20, 242);
            lblSpecialInstructions.Name = "lblSpecialInstructions";
            lblSpecialInstructions.Size = new Size(147, 15);
            lblSpecialInstructions.TabIndex = 10;
            lblSpecialInstructions.Text = "Instrucciones Especiales:";
            //
            // txtWeight
            //
            txtWeight.Location = new Point(200, 210);
            txtWeight.Name = "txtWeight";
            txtWeight.Size = new Size(160, 23);
            txtWeight.TabIndex = 9;
            //
            // lblWeight
            //
            lblWeight.AutoSize = true;
            lblWeight.Font = new Font("Segoe UI", 9F);
            lblWeight.Location = new Point(200, 192);
            lblWeight.Name = "lblWeight";
            lblWeight.Size = new Size(69, 15);
            lblWeight.TabIndex = 8;
            lblWeight.Text = "Peso (kg):";
            //
            // txtAge
            //
            txtAge.Location = new Point(20, 210);
            txtAge.Name = "txtAge";
            txtAge.Size = new Size(160, 23);
            txtAge.TabIndex = 7;
            //
            // lblAge
            //
            lblAge.AutoSize = true;
            lblAge.Font = new Font("Segoe UI", 9F);
            lblAge.Location = new Point(20, 192);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(79, 15);
            lblAge.TabIndex = 6;
            lblAge.Text = "Edad (años):";
            //
            // txtBreed
            //
            txtBreed.Location = new Point(20, 160);
            txtBreed.Name = "txtBreed";
            txtBreed.Size = new Size(340, 23);
            txtBreed.TabIndex = 5;
            //
            // lblBreed
            //
            lblBreed.AutoSize = true;
            lblBreed.Font = new Font("Segoe UI", 9F);
            lblBreed.Location = new Point(20, 142);
            lblBreed.Name = "lblBreed";
            lblBreed.Size = new Size(35, 15);
            lblBreed.TabIndex = 4;
            lblBreed.Text = "Raza:";
            //
            // txtName
            //
            txtName.Location = new Point(20, 110);
            txtName.Name = "txtName";
            txtName.Size = new Size(340, 23);
            txtName.TabIndex = 3;
            //
            // lblName
            //
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9F);
            lblName.Location = new Point(20, 92);
            lblName.Name = "lblName";
            lblName.Size = new Size(54, 15);
            lblName.TabIndex = 2;
            lblName.Text = "Nombre:";
            //
            // cmbClient
            //
            cmbClient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClient.FormattingEnabled = true;
            cmbClient.Location = new Point(20, 45);
            cmbClient.Name = "cmbClient";
            cmbClient.Size = new Size(340, 23);
            cmbClient.TabIndex = 1;
            //
            // lblClient
            //
            lblClient.AutoSize = true;
            lblClient.Font = new Font("Segoe UI", 9F);
            lblClient.Location = new Point(20, 27);
            lblClient.Name = "lblClient";
            lblClient.Size = new Size(47, 15);
            lblClient.TabIndex = 0;
            lblClient.Text = "Cliente:";
            //
            // grpActions
            //
            grpActions.Controls.Add(btnClear);
            grpActions.Controls.Add(btnDelete);
            grpActions.Controls.Add(btnSave);
            grpActions.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpActions.Location = new Point(20, 430);
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
            grpList.Controls.Add(dgvDogs);
            grpList.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpList.Location = new Point(420, 150);
            grpList.Name = "grpList";
            grpList.Size = new Size(750, 360);
            grpList.TabIndex = 4;
            grpList.TabStop = false;
            grpList.Text = "Lista de Perros";
            //
            // dgvDogs
            //
            dgvDogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDogs.Dock = DockStyle.Fill;
            dgvDogs.Location = new Point(3, 19);
            dgvDogs.Name = "dgvDogs";
            dgvDogs.Size = new Size(744, 338);
            dgvDogs.TabIndex = 0;
            dgvDogs.CellClick += dgvDogs_CellClick;
            //
            // pnlPagination
            //
            pnlPagination.BackColor = Color.White;
            pnlPagination.BorderStyle = BorderStyle.FixedSingle;
            pnlPagination.Controls.Add(btnPrevious);
            pnlPagination.Controls.Add(lblPaginationInfo);
            pnlPagination.Controls.Add(btnNext);
            pnlPagination.Location = new Point(420, 520);
            pnlPagination.Name = "pnlPagination";
            pnlPagination.Size = new Size(750, 50);
            pnlPagination.TabIndex = 5;
            //
            // btnPrevious
            //
            btnPrevious.BackColor = Color.FromArgb(0, 120, 215);
            btnPrevious.FlatStyle = FlatStyle.Flat;
            btnPrevious.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrevious.ForeColor = Color.White;
            btnPrevious.Location = new Point(10, 10);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(100, 30);
            btnPrevious.TabIndex = 0;
            btnPrevious.Text = "◀ Anterior";
            btnPrevious.UseVisualStyleBackColor = false;
            btnPrevious.Click += btnPrevious_Click;
            //
            // lblPaginationInfo
            //
            lblPaginationInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPaginationInfo.Location = new Point(120, 10);
            lblPaginationInfo.Name = "lblPaginationInfo";
            lblPaginationInfo.Size = new Size(510, 30);
            lblPaginationInfo.TabIndex = 1;
            lblPaginationInfo.Text = "Página 1 de 1";
            lblPaginationInfo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // btnNext
            //
            btnNext.BackColor = Color.FromArgb(0, 120, 215);
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNext.ForeColor = Color.White;
            btnNext.Location = new Point(640, 10);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(100, 30);
            btnNext.TabIndex = 2;
            btnNext.Text = "Siguiente ▶";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            //
            // DogManagementForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1190, 590);
            Controls.Add(pnlPagination);
            Controls.Add(grpList);
            Controls.Add(grpSearch);
            Controls.Add(grpActions);
            Controls.Add(grpData);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "DogManagementForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Perros - Dog Walking";
            grpData.ResumeLayout(false);
            grpData.PerformLayout();
            grpActions.ResumeLayout(false);
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDogs).EndInit();
            pnlPagination.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private GroupBox grpData;
        private ComboBox cmbClient;
        private Label lblClient;
        private TextBox txtName;
        private Label lblName;
        private TextBox txtBreed;
        private Label lblBreed;
        private TextBox txtAge;
        private Label lblAge;
        private TextBox txtWeight;
        private Label lblWeight;
        private TextBox txtSpecialInstructions;
        private Label lblSpecialInstructions;
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
        private DataGridView dgvDogs;
        private Panel pnlPagination;
        private Button btnPrevious;
        private Label lblPaginationInfo;
        private Button btnNext;
    }
}
