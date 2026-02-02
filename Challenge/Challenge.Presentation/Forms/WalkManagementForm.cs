using Challenge.Core.Interfaces;
using Challenge.Presentation.Presenters.Walk;
using Challenge.Presentation.ViewModels;
using Challenge.Presentation.Views.Walk;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace Challenge.Presentation.Forms;

/// <summary>
/// View "tonta" - solo renderiza UI y dispara eventos
/// Toda la lógica está en WalkPresenter
/// </summary>
public partial class WalkManagementForm : Form, IWalkView
{
    private readonly WalkPresenter _presenter;
    private bool _isLoading;

    public WalkManagementForm(IMediator mediator, ILogger<WalkPresenter> logger, ICurrentUserService currentUserService)
    {
        InitializeComponent();
        _presenter = new WalkPresenter(this, mediator, logger, currentUserService);

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
        dgvWalks.AutoGenerateColumns = false;
        dgvWalks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvWalks.MultiSelect = false;
        dgvWalks.ReadOnly = true;
        dgvWalks.AllowUserToAddRows = false;
        dgvWalks.RowHeadersVisible = false;

        // Configurar columnas
        dgvWalks.Columns.Clear();
        dgvWalks.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Id",
            HeaderText = "ID",
            Width = 50,
            Visible = false
        });
        dgvWalks.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "DogName",
            HeaderText = "Perro",
            Width = 130
        });
        dgvWalks.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "ClientName",
            HeaderText = "Cliente",
            Width = 150
        });
        dgvWalks.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "DateDisplay",
            HeaderText = "Fecha",
            Width = 130
        });
        dgvWalks.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "DurationMinutes",
            HeaderText = "Duración (min)",
            Width = 100
        });
        dgvWalks.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "DistanceDisplay",
            HeaderText = "Distancia",
            Width = 100
        });
        dgvWalks.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "WalkedByUsername",
            HeaderText = "Paseador",
            Width = 100
        });

        // Estilos
        dgvWalks.BackgroundColor = Color.White;
        dgvWalks.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
        dgvWalks.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
        dgvWalks.DefaultCellStyle.SelectionForeColor = Color.White;
        dgvWalks.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
        dgvWalks.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvWalks.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvWalks.EnableHeadersVisualStyles = false;
    }

    // Implementación de IWalkView - Propiedades
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int? SelectedDogId
    {
        get => cmbDog.SelectedValue as int?;
        set
        {
            if (value.HasValue)
                cmbDog.SelectedValue = value.Value;
            else
                cmbDog.SelectedIndex = -1;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DateTime WalkDate
    {
        get => dtpWalkDate.Value;
        set => dtpWalkDate.Value = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string DurationMinutes
    {
        get => nudDuration.Value.ToString();
        set
        {
            if (int.TryParse(value, out int duration))
                nudDuration.Value = duration;
            else
                nudDuration.Value = 0;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Distance
    {
        get => nudDistance.Value.ToString();
        set
        {
            if (decimal.TryParse(value, out decimal distance))
                nudDistance.Value = distance;
            else
                nudDistance.Value = 0;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string Notes
    {
        get => txtNotes.Text;
        set => txtNotes.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string SearchText
    {
        get => txtSearch.Text;
        set => txtSearch.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int? SelectedWalkId { get; set; }

    // Implementación de IWalkView - Métodos
    public void LoadWalks(IEnumerable<WalkViewModel> walks)
    {
        dgvWalks.DataSource = new BindingSource { DataSource = walks.ToList() };
    }

    public void LoadDogs(IEnumerable<(int Id, string Name, string ClientName)> dogs)
    {
        var dogItems = dogs.Select(d => new
        {
            Id = d.Id,
            Name = $"{d.Name} ({d.ClientName})"
        }).ToList();

        cmbDog.DisplayMember = "Name";
        cmbDog.ValueMember = "Id";
        cmbDog.DataSource = dogItems;
        cmbDog.SelectedIndex = -1;
    }

    public void ClearForm()
    {
        SelectedWalkId = null;
        cmbDog.SelectedIndex = -1;
        dtpWalkDate.Value = DateTime.Now;
        nudDuration.Value = 0;
        nudDistance.Value = 0;
        txtNotes.Clear();
        cmbDog.Focus();
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

    // Eventos para el Presenter
    public event EventHandler? LoadRequested;
    public event EventHandler? SaveRequested;
    public event EventHandler? DeleteRequested;
    public event EventHandler? ClearRequested;
    public event EventHandler? SearchRequested;
    public event EventHandler<int>? WalkSelected;

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
            "¿Está seguro que desea eliminar este paseo?",
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

    private void dgvWalks_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0 && e.RowIndex < dgvWalks.Rows.Count)
        {
            // Obtener el WalkViewModel directamente del DataSource
            if (dgvWalks.DataSource is BindingSource bindingSource &&
                bindingSource.DataSource is List<WalkViewModel> walks &&
                e.RowIndex < walks.Count)
            {
                var walk = walks[e.RowIndex];
                WalkSelected?.Invoke(this, walk.Id);
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
}
