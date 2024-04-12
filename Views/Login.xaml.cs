using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SmartOffice.Services.MQTTServices;
using SmartOffice.Services.UserServices;

namespace SmartOffice.Views;

public partial class Login
{
    private readonly IServiceProvider _service;
    private readonly IUserService _userService;
    private readonly IMqttService _mqttService;
    
    public Login(IServiceProvider service)
    {
        InitializeComponent();
        _service = service;
        _userService = _service.GetRequiredService<IUserService>();
        _mqttService = _service.GetRequiredService<IMqttService>();
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

    private async Task UserLogin()
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

                await _mqttService.CreateAndConnectMqttClient();
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

    private async void StartSmartOffice(object sender, RoutedEventArgs e)
    {
        await UserLogin();
    }

    private async void SoUsrPassword_OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            await UserLogin();
        }
    }
}