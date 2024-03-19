using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartOffice.Views.FoodOrdering;

public partial class OrderStart : Window
{
    public OrderStart()
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