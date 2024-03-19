using System.Windows;
using System.Windows.Input;

namespace SmartOffice.Views.FoodOrdering;

public partial class AddMenus : Window
{
    public AddMenus()
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
    
    private void CloseBTN_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}