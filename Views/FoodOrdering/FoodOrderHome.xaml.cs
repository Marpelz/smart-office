using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace SmartOffice.Views.FoodOrdering;

public partial class FoodOrderHome : UserControl
{
    private readonly IServiceProvider _service;
    
    public FoodOrderHome(IServiceProvider service)
    {
        InitializeComponent();
        _service = service;
    }

    private void OpenFoodOrderStartBTN_Click(object sender, RoutedEventArgs e)
    {
        var orderStart = _service.GetRequiredService<OrderStart>(); 
        orderStart.Show();
    }
    
    private void OpenAddRestaurantsBTN_Click(object sender, RoutedEventArgs e)
    {
        var addRestaurants = _service.GetRequiredService<AddRestaurants>();
        addRestaurants.Show();
    }
    
    private void OpenAddMenusBTN_Click(object sender, RoutedEventArgs e)
    {
        var addMenus = _service.GetRequiredService<AddDishes>();
        addMenus.Show();
    }
    
    private void OpenOrderHistoryBTN_Click(object sender, RoutedEventArgs e)
    {
        var orderHistory = _service.GetRequiredService<OrderHistory>();
        orderHistory.Show();
    }
}