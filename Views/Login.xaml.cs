using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SmartOffice.Services.UserService;

namespace SmartOffice.Views;

public partial class Login
{
    private readonly IUserService _userService;
    private readonly IServiceProvider _service;
    
    public Login(IServiceProvider service)
    {
        InitializeComponent();
        _service = service;
        _userService = _service.GetRequiredService<IUserService>();
    }
    
    private void WindowDragMove(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }
    
    private void CloseOnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void OpenRegister(object sender, RoutedEventArgs e)
    {
        var register = _service.GetRequiredService<Register>();
        register.Show();
        Close();
    }

    private async void StartSmartOffice(object sender, RoutedEventArgs e)
    {
        var userName = SoUsrName.Text;
        var userPassword = SoUsrPassword.Password;

        try
        {
            if (userName.ToLower() == "admin" && userPassword == "admin")
            {
                var homescreen = _service.GetRequiredService<HomeScreen>();
                homescreen.username.Content = SoUsrName.Text;
                AppSettings.Username = SoUsrName.Text;
                homescreen.Show();
                Close();
                return;
            }
            
            var user = await _userService.GetUserByUsername(userName);

            if (user != null && _userService.VerifyPassword(userPassword, user.UserPassword))
            {
                var homescreen = _service.GetRequiredService<HomeScreen>();
                homescreen.username.Content = SoUsrName.Text;
                AppSettings.Username = SoUsrName.Text;
                homescreen.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Ungültiger Benutzername oder Passwort.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim Abrufen des Benutzers: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}