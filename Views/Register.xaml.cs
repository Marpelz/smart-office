using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SmartOffice.Models.UserModels;
using SmartOffice.Services.UserServices;

namespace SmartOffice.Views;

public partial class Register
{
    private readonly IUserService _userService;
    private readonly IServiceProvider _service;

    public Register(IServiceProvider service)
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
    
    private void BackToUserLogin(object sender, RoutedEventArgs e)
    {
        var login = _service.GetRequiredService<Login>();
        login.Show();
        Close();
    }
    
    private void SetPaypalToggle_Checked(object sender, RoutedEventArgs e)
    {
        SetUsrPaypal.IsEnabled = true;
    }
    
    private void SetPaypalToggle_Unchecked(object sender, RoutedEventArgs e)
    {
        SetUsrPaypal.IsEnabled = false;
    }

    private async void CreateUserAccount(object sender, RoutedEventArgs e)
    {
        try
        {
            var userName = SetUsrName.Text;
            var userPassword = SetUsrPassword.Password;
            var paypalEnabled = SetPaypalToggle.IsChecked ?? false;
            var paypalAddress = SetUsrPaypal.Text;

            if (SetUsrName.Text == "" && SetUsrPassword.Password == "")
            {
                MessageBox.Show("Pflichtfelder sind nicht befüllt. Bitte Benutzername und Passwort ausfüllen.", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var newUser = new UserModel
            {
                UserName = userName,
                UserPassword = userPassword,
                ActivePaypal = paypalEnabled,
                PaypalEmail = paypalAddress
            };

            await _userService.SetUser(newUser, userPassword);

            MessageBox.Show("Benutzerkonto erfolgreich erstellt.", "Erfolg", MessageBoxButton.OK, MessageBoxImage.Information);

            var login = _service.GetRequiredService<Login>();
            login.Show();
            
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim Erstellen des Benutzerkontos: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}