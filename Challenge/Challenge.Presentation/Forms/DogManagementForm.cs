using Challenge.Presentation.Presenters.Dog;
using Challenge.Presentation.ViewModels;
using Challenge.Presentation.Views.Dog;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace Challenge.Presentation.Forms;

/// <summary>
/// View "tonta" - solo renderiza UI y dispara eventos
/// Toda la lógica está en DogPresenter
/// </summary>
public partial class DogManagementForm : Form, IDogView
{
    private readonly DogPresenter _presenter;
    private bool _isLoading;

    public DogManagementForm(IMediator mediator, ILogger<DogPresenter> logger)
    {
        InitializeComponent();
        _presenter = new DogPresenter(this, mediator, logger);

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
        dgvDogs.AutoGenerateColumns = false;
        dgvDogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDogs.MultiSelect = false;
        dgvDogs.ReadOnly = true;
        dgvDogs.AllowUserToAddRows = false;
        dgvDogs.RowHeadersVisible = false;

        // Configurar columnas
        dgvDogs.Columns.Clear();
        dgvDogs.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Id",
            HeaderText = "ID",
            Width = 50,
            Visible = false
        });
        dgvDogs.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Name",
            HeaderText = "Nombre",
            Width = 150
        });
        dgvDogs.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Breed",
            HeaderText = "Raza",
            Width = 150
        });
        dgvDogs.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Age",
            HeaderText = "Edad",
            Width = 70
        });
        dgvDogs.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "WeightDisplay",
            HeaderText = "Peso",
            Width = 100
        });
        dgvDogs.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "ClientName",
            HeaderText = "Cliente",
            Width = 200
        });

        // Estilos
        dgvDogs.BackgroundColor = Color.White;
        dgvDogs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        dgvDogs.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
        dgvDogs.DefaultCellStyle.SelectionForeColor = Color.White;
        dgvDogs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
        dgvDogs.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvDogs.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvDogs.EnableHeadersVisualStyles = false;
    }

    // Implementación de IDogView - Propiedades
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int? SelectedClientId
    {
        get => cmbClient.SelectedValue as int?;
        set
        {
            if (value.HasValue)
                cmbClient.SelectedValue = value.Value;
            else
                cmbClient.SelectedIndex = -1;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string DogName
    {
        get => txtName.Text;
        set => txtName.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Breed
    {
        get => txtBreed.Text;
        set => txtBreed.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Age
    {
        get => txtAge.Text;
        set => txtAge.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Weight
    {
        get => txtWeight.Text;
        set => txtWeight.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SpecialInstructions
    {
        get => txtSpecialInstructions.Text;
        set => txtSpecialInstructions.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SearchText
    {
        get => txtSearch.Text;
        set => txtSearch.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int? SelectedDogId { get; set; }

    // Implementación de IDogView - Métodos
    public void LoadDogs(IEnumerable<DogViewModel> dogs)
    {
        dgvDogs.DataSource = new BindingSource { DataSource = dogs.ToList() };
    }

    public void LoadClients(IEnumerable<(int Id, string Name)> clients)
    {
        var clientItems = clients.Select(c => new
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();

        cmbClient.DisplayMember = "Name";
        cmbClient.ValueMember = "Id";
        cmbClient.DataSource = clientItems;
        cmbClient.SelectedIndex = -1;
    }

    public void ClearForm()
    {
        SelectedDogId = null;
        cmbClient.SelectedIndex = -1;
        txtName.Clear();
        txtBreed.Clear();
        txtAge.Clear();
        txtWeight.Clear();
        txtSpecialInstructions.Clear();
        cmbClient.Focus();
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
    public event EventHandler<int>? DogSelected;
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
            "¿Está seguro que desea eliminar este perro?",
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

    private void dgvDogs_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && e.RowIndex < dgvDogs.Rows.Count)
        {
            // Obtener el DogViewModel directamente del DataSource
            if (dgvDogs.DataSource is BindingSource bindingSource &&
                bindingSource.DataSource is List<DogViewModel> dogs &&
                e.RowIndex < dogs.Count)
            {
                var dog = dogs[e.RowIndex];
                DogSelected?.Invoke(this, dog.Id);
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
