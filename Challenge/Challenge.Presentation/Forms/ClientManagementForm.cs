using Challenge.Presentation.Presenters.Client;
using Challenge.Presentation.ViewModels;
using Challenge.Presentation.Views.Client;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace Challenge.Presentation.Forms;

/// <summary>
/// View "tonta" - solo renderiza UI y dispara eventos
/// Toda la lógica está en ClientPresenter
/// </summary>
public partial class ClientManagementForm : Form, IClientView
{
    private readonly ClientPresenter _presenter;
    private bool _isLoading;

    public ClientManagementForm(IMediator mediator, ILogger<ClientPresenter> logger)
    {
        InitializeComponent();
        _presenter = new ClientPresenter(this, mediator, logger);

        // Configurar estilos del DataGridView
        ConfigureDataGridView();
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        await _presenter.InitializeAsync();
    }

    private void ConfigureDataGridView()
    {
        dgvClients.AutoGenerateColumns = false;
        dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvClients.MultiSelect = false;
        dgvClients.ReadOnly = true;
        dgvClients.AllowUserToAddRows = false;
        dgvClients.RowHeadersVisible = false;

        // Configurar columnas
        dgvClients.Columns.Clear();
        dgvClients.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Id",
            HeaderText = "ID",
            Width = 50,
            Visible = false
        });
        dgvClients.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "FullName",
            HeaderText = "Nombre Completo",
            Width = 200
        });
        dgvClients.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "PhoneNumber",
            HeaderText = "Teléfono",
            Width = 120
        });
        dgvClients.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Email",
            HeaderText = "Email",
            Width = 200
        });
        dgvClients.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "City",
            HeaderText = "Ciudad",
            Width = 150
        });
        dgvClients.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "State",
            HeaderText = "Estado",
            Width = 100
        });

        // Estilos
        dgvClients.BackgroundColor = Color.White;
        dgvClients.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        dgvClients.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
        dgvClients.DefaultCellStyle.SelectionForeColor = Color.White;
        dgvClients.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
        dgvClients.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvClients.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvClients.EnableHeadersVisualStyles = false;
    }

    // Implementación de IClientView - Propiedades
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string FirstName
    {
        get => txtFirstName.Text;
        set => txtFirstName.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string LastName
    {
        get => txtLastName.Text;
        set => txtLastName.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string PhoneNumber
    {
        get => txtPhoneNumber.Text;
        set => txtPhoneNumber.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Email
    {
        get => txtEmail.Text;
        set => txtEmail.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Address
    {
        get => txtAddress.Text;
        set => txtAddress.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string City
    {
        get => txtCity.Text;
        set => txtCity.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string State
    {
        get => txtState.Text;
        set => txtState.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string ZipCode
    {
        get => txtZipCode.Text;
        set => txtZipCode.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SearchText
    {
        get => txtSearch.Text;
        set => txtSearch.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int? SelectedClientId { get; set; }

    // Implementación de IClientView - Métodos
    public void LoadClients(IEnumerable<ClientViewModel> clients)
    {
        dgvClients.DataSource = new BindingSource { DataSource = clients.ToList() };
    }

    public void ClearForm()
    {
        SelectedClientId = null;
        txtFirstName.Clear();
        txtLastName.Clear();
        txtPhoneNumber.Clear();
        txtEmail.Clear();
        txtAddress.Clear();
        txtCity.Clear();
        txtState.Clear();
        txtZipCode.Clear();
        txtFirstName.Focus();
    }

    public void EnableForm(bool enabled)
    {
        grpData.Enabled = enabled;
        grpActions.Enabled = enabled;
    }

    public void ShowMessage(string message, string title, bool isError)
    {
        MessageBox.Show(
            message,
            title,
            MessageBoxButtons.OK,
            isError ? MessageBoxIcon.Error : MessageBoxIcon.Information);
    }

    public void ShowLoading(bool show)
    {
        _isLoading = show;
        Cursor = show ? Cursors.WaitCursor : Cursors.Default;
    }

    public void UpdatePaginationInfo(int currentPage, int totalPages, int totalRecords, bool hasPrevious, bool hasNext)
    {
        lblPaginationInfo.Text = $"Página {currentPage} de {totalPages} (Total: {totalRecords} registros)";
        btnPrevious.Enabled = hasPrevious && !_isLoading;
        btnNext.Enabled = hasNext && !_isLoading;
    }

    // Eventos para el Presenter
    public event EventHandler? LoadRequested;
    public event EventHandler? SaveRequested;
    public event EventHandler? DeleteRequested;
    public event EventHandler? ClearRequested;
    public event EventHandler? SearchRequested;
    public event EventHandler<int>? ClientSelected;
    public event EventHandler? PreviousPageRequested;
    public event EventHandler? NextPageRequested;

    // Handlers de botones - solo disparan eventos
    private void btnSave_Click(object sender, EventArgs e)
    {
        if (_isLoading) return;
        SaveRequested?.Invoke(this, EventArgs.Empty);
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (_isLoading) return;

        var confirm = MessageBox.Show(
            "¿Está seguro que desea eliminar este cliente?",
            "Confirmar Eliminación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (confirm == DialogResult.Yes)
        {
            DeleteRequested?.Invoke(this, EventArgs.Empty);
        }
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        ClearRequested?.Invoke(this, EventArgs.Empty);
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        SearchRequested?.Invoke(this, EventArgs.Empty);
    }

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        txtSearch.Clear();
        LoadRequested?.Invoke(this, EventArgs.Empty);
    }

    private void dgvClients_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && e.RowIndex < dgvClients.Rows.Count)
        {
            // Obtener el ClientViewModel directamente del DataSource
            if (dgvClients.DataSource is BindingSource bindingSource &&
                bindingSource.DataSource is List<ClientViewModel> clients &&
                e.RowIndex < clients.Count)
            {
                var client = clients[e.RowIndex];
                ClientSelected?.Invoke(this, client.Id);
            }
        }
    }

    private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            btnSearch_Click(sender, e);
        }
    }

    private void btnPrevious_Click(object sender, EventArgs e)
    {
        if (_isLoading) return;
        PreviousPageRequested?.Invoke(this, EventArgs.Empty);
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
        if (_isLoading) return;
        NextPageRequested?.Invoke(this, EventArgs.Empty);
    }
}
