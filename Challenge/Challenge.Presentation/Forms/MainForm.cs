using Challenge.Business.Features.Auth.Logout;
using Challenge.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Presentation.Forms;

public partial class MainForm : Form
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public MainForm(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
        InitializeComponent();

        // Mostrar información del usuario actual
        lblWelcome.Text = $"Bienvenido, {_currentUserService.Username}";
    }

    private void btnClients_Click(object sender, EventArgs e)
    {
        var clientForm = Program.ServiceProvider.GetRequiredService<ClientManagementForm>();
        clientForm.ShowDialog();
    }

    private void btnDogs_Click(object sender, EventArgs e)
    {
        var dogForm = Program.ServiceProvider.GetRequiredService<DogManagementForm>();
        dogForm.ShowDialog();
    }

    private void btnWalks_Click(object sender, EventArgs e)
    {
        var walkForm = Program.ServiceProvider.GetRequiredService<WalkManagementForm>();
        walkForm.ShowDialog();
    }

    private async void btnLogout_Click(object sender, EventArgs e)
    {
        var confirm = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (confirm == DialogResult.Yes)
        {
            await _mediator.Send(new LogoutCommand());

            var loginForm = Program.ServiceProvider.GetRequiredService<LoginForm>();
            loginForm.Show();
            this.Close();
        }
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}
