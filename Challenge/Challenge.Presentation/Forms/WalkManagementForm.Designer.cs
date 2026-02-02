namespace Challenge.Presentation.Forms
{
    partial class WalkManagementForm
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
            txtNotes = new TextBox();
            lblNotes = new Label();
            nudDistance = new NumericUpDown();
            lblDistance = new Label();
            nudDuration = new NumericUpDown();
            lblDuration = new Label();
            dtpWalkDate = new DateTimePicker();
            lblWalkDate = new Label();
            cmbDog = new ComboBox();
            lblDog = new Label();
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
            dgvWalks = new DataGridView();
            pnlPagination = new Panel();
            btnPrevious = new Button();
            lblPaginationInfo = new Label();
            btnNext = new Button();
            grpData.SuspendLayout();
            grpActions.SuspendLayout();
            grpSearch.SuspendLayout();
            grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvWalks).BeginInit();
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
            lblTitle.Size = new Size(240, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Gestión de Paseos";
            //
            // grpData
            //
            grpData.Controls.Add(txtNotes);
            grpData.Controls.Add(lblNotes);
            grpData.Controls.Add(nudDistance);
            grpData.Controls.Add(lblDistance);
            grpData.Controls.Add(nudDuration);
            grpData.Controls.Add(lblDuration);
            grpData.Controls.Add(dtpWalkDate);
            grpData.Controls.Add(lblWalkDate);
            grpData.Controls.Add(cmbDog);
            grpData.Controls.Add(lblDog);
            grpData.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpData.Location = new Point(20, 60);
            grpData.Name = "grpData";
            grpData.Size = new Size(380, 360);
            grpData.TabIndex = 1;
            grpData.TabStop = false;
            grpData.Text = "Datos del Paseo";
            //
            // txtNotes
            //
            txtNotes.Location = new Point(20, 230);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.ScrollBars = ScrollBars.Vertical;
            txtNotes.Size = new Size(340, 110);
            txtNotes.TabIndex = 9;
            //
            // lblNotes
            //
            lblNotes.AutoSize = true;
            lblNotes.Font = new Font("Segoe UI", 9F);
            lblNotes.Location = new Point(20, 212);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(42, 15);
            lblNotes.TabIndex = 8;
            lblNotes.Text = "Notas:";
            //
            // nudDistance
            //
            nudDistance.DecimalPlaces = 2;
            nudDistance.Location = new Point(200, 180);
            nudDistance.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudDistance.Name = "nudDistance";
            nudDistance.Size = new Size(160, 23);
            nudDistance.TabIndex = 7;
            //
            // lblDistance
            //
            lblDistance.AutoSize = true;
            lblDistance.Font = new Font("Segoe UI", 9F);
            lblDistance.Location = new Point(200, 162);
            lblDistance.Name = "lblDistance";
            lblDistance.Size = new Size(89, 15);
            lblDistance.TabIndex = 6;
            lblDistance.Text = "Distancia (km):";
            //
            // nudDuration
            //
            nudDuration.Location = new Point(20, 180);
            nudDuration.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
            nudDuration.Name = "nudDuration";
            nudDuration.Size = new Size(160, 23);
            nudDuration.TabIndex = 5;
            //
            // lblDuration
            //
            lblDuration.AutoSize = true;
            lblDuration.Font = new Font("Segoe UI", 9F);
            lblDuration.Location = new Point(20, 162);
            lblDuration.Name = "lblDuration";
            lblDuration.Size = new Size(112, 15);
            lblDuration.TabIndex = 4;
            lblDuration.Text = "Duración (minutos):";
            //
            // dtpWalkDate
            //
            dtpWalkDate.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpWalkDate.Format = DateTimePickerFormat.Custom;
            dtpWalkDate.Location = new Point(20, 120);
            dtpWalkDate.Name = "dtpWalkDate";
            dtpWalkDate.ShowUpDown = true;
            dtpWalkDate.Size = new Size(340, 23);
            dtpWalkDate.TabIndex = 3;
            //
            // lblWalkDate
            //
            lblWalkDate.AutoSize = true;
            lblWalkDate.Font = new Font("Segoe UI", 9F);
            lblWalkDate.Location = new Point(20, 102);
            lblWalkDate.Name = "lblWalkDate";
            lblWalkDate.Size = new Size(105, 15);
            lblWalkDate.TabIndex = 2;
            lblWalkDate.Text = "Fecha y Hora:";
            //
            // cmbDog
            //
            cmbDog.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDog.FormattingEnabled = true;
            cmbDog.Location = new Point(20, 45);
            cmbDog.Name = "cmbDog";
            cmbDog.Size = new Size(340, 23);
            cmbDog.TabIndex = 1;
            //
            // lblDog
            //
            lblDog.AutoSize = true;
            lblDog.Font = new Font("Segoe UI", 9F);
            lblDog.Location = new Point(20, 27);
            lblDog.Name = "lblDog";
            lblDog.Size = new Size(41, 15);
            lblDog.TabIndex = 0;
            lblDog.Text = "Perro:";
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
            grpList.Controls.Add(dgvWalks);
            grpList.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grpList.Location = new Point(420, 150);
            grpList.Name = "grpList";
            grpList.Size = new Size(750, 360);
            grpList.TabIndex = 4;
            grpList.TabStop = false;
            grpList.Text = "Lista de Paseos";
            //
            // dgvWalks
            //
            dgvWalks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvWalks.Dock = DockStyle.Fill;
            dgvWalks.Location = new Point(3, 19);
            dgvWalks.Name = "dgvWalks";
            dgvWalks.Size = new Size(744, 338);
            dgvWalks.TabIndex = 0;
            dgvWalks.CellClick += dgvWalks_CellClick;
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
            btnPrevious.Location = new Point(20, 8);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(120, 32);
            btnPrevious.TabIndex = 0;
            btnPrevious.Text = "◀ Anterior";
            btnPrevious.UseVisualStyleBackColor = false;
            btnPrevious.Click += btnPrevious_Click;
            //
            // lblPaginationInfo
            //
            lblPaginationInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPaginationInfo.Location = new Point(150, 8);
            lblPaginationInfo.Name = "lblPaginationInfo";
            lblPaginationInfo.Size = new Size(450, 32);
            lblPaginationInfo.TabIndex = 1;
            lblPaginationInfo.Text = "Página 1 de 1 (Total: 0 registros)";
            lblPaginationInfo.TextAlign = ContentAlignment.MiddleCenter;
            //
            // btnNext
            //
            btnNext.BackColor = Color.FromArgb(0, 120, 215);
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNext.ForeColor = Color.White;
            btnNext.Location = new Point(610, 8);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(120, 32);
            btnNext.TabIndex = 2;
            btnNext.Text = "Siguiente ▶";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            //
            // WalkManagementForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1190, 580);
            Controls.Add(pnlPagination);
            Controls.Add(grpList);
            Controls.Add(grpSearch);
            Controls.Add(grpActions);
            Controls.Add(grpData);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "WalkManagementForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Paseos - Dog Walking";
            grpData.ResumeLayout(false);
            grpData.PerformLayout();
            grpActions.ResumeLayout(false);
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvWalks).EndInit();
            pnlPagination.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private GroupBox grpData;
        private ComboBox cmbDog;
        private Label lblDog;
        private DateTimePicker dtpWalkDate;
        private Label lblWalkDate;
        private NumericUpDown nudDuration;
        private Label lblDuration;
        private NumericUpDown nudDistance;
        private Label lblDistance;
        private TextBox txtNotes;
        private Label lblNotes;
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
        private DataGridView dgvWalks;
        private Panel pnlPagination;
        private Button btnPrevious;
        private Label lblPaginationInfo;
        private Button btnNext;
    }
}
