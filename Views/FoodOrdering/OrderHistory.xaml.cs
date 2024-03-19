using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartOffice.Views.FoodOrdering;

public partial class OrderHistory : Window
{
    public OrderHistory()
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
}