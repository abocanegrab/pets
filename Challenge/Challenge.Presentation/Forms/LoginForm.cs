using Challenge.Business.Features.Auth.Login;
using Challenge.Core.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Challenge.Presentation.Forms;

public partial class LoginForm : Form
{
    private readonly IMediator _mediator;

    public LoginForm(IMediator mediator)
    {
        _mediator = mediator;
        InitializeComponent();
    }

    private async void btnLogin_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            MessageBox.Show("Por favor ingrese usuario y contraseña", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        btnLogin.Enabled = false;
        Cursor = Cursors.WaitCursor;

        try
        {
            var command = new LoginCommand
            {
                Username = txtUsername.Text.Trim(),
                Password = txtPassword.Text
            };

            var result = await _mediator.Send(command);

            if (result.Data != null)
            {
                MessageBox.Show($"Bienvenido {result.Data.FullName}", "Login Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Abrir MainForm
                var mainForm = Program.ServiceProvider.GetRequiredService<MainForm>();
                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
        }
        catch (DomainException ex)
        {
            MessageBox.Show(ex.Message, "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            btnLogin.Enabled = true;
            Cursor = Cursors.Default;
        }
    }

    private void btnExit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }

    private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            btnLogin_Click(sender, e);
        }
    }
}
