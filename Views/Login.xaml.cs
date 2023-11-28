using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using MaterialDesignExtensions.Themes;
using PaletteHelper = MaterialDesignThemes.Wpf.PaletteHelper;
using PaletteHelperExt = MaterialDesignExtensions.Themes.PaletteHelper;

namespace SmartOffice.Views;

public partial class Login : Window
{
    private Register? _register;
    private HomeScreen? _homeScreen;
    
    
    private bool IsLightTheme { get; set; }
    
    public Login()
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

    private void OpenRegister(object sender, RoutedEventArgs e)
    {
        _register = new Register();
        _register.Show();
        Close();
    }

    private void StartEasyOffice(object sender, RoutedEventArgs e)
    {
        _homeScreen = new HomeScreen();
        _homeScreen.Show();
        Close();
    }
}