using System.Windows;
using System.Windows.Input;

namespace SmartOffice.Views;

public partial class Register : Window
{
    private Login? _login;
    
    public Register()
    {
        InitializeComponent();
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

    private void CreateUserAccount(object sender, RoutedEventArgs e)
    {
        _login = new Login();
        _login.Show();
        Close();
    }
}