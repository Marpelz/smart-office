using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignExtensions.Controls;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Services.FoodOrderServices.RestaurantService;

namespace SmartOffice.Views.FoodOrdering;

public partial class OrderStart : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged; 
    private readonly IServiceProvider _service;
    private readonly IRestaurantService _restaurantService;
    public List<RestaurantViewModel> Restaurants { get; set; }
    
    public OrderStart(IServiceProvider service)
    {
        InitializeComponent();
        _service = service;
        _restaurantService = _service.GetRequiredService<IRestaurantService>();
        LoadRestaurants();
        DataContext = this;
    }
    
    // Propertys
    
    

    // Functions
    
    private async Task LoadRestaurants()
    {
        var soresttabs = await _restaurantService.ReadALlRestaurants();
        Restaurants = soresttabs.Select(rest => new RestaurantViewModel
        {
            FoodorderRestaurantIdProp = rest.RestId,
            FoodorderRestaurantNameProp = rest.RestName,
            FoodorderRestaurantStreetProp = rest.RestStreet,
            FoodorderRestaurantZipcodeProp = rest.RestZipcode,
            FoodorderRestaurantCityProp = rest.RestCity,
            FoodorderRestaurantTypeProp = rest.RestType,
            FoodorderRestaurantPhonenumberProp = rest.RestPhonenumber, 
            _foodorderRestaurantDeliveryYesNo = rest.RestDelivery,
            FoodorderRestaurantDeliveryTimeProp = rest.RestDeliveryTime,
            _foodorderRestaurantOrdertypeAppTelephone = rest.RestAppTelephone,
            FoodorderRestaurantMinimalOrderValueProp = rest.RestMinOrderValue,
            FoodorderRestaurantDeliveryCostProp = rest.RestDeliveryCost,
            FoodorderRestaurantLieferandoLinkProp = rest.RestLieferandoLink
        }).ToList();
        OnPropertyChanged(nameof(Restaurants));
    }
    
    // Click-Events

    private void Close_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
    // Basics
    
    private void OnPropertyChanged(string info = "")
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }
    
    private void WindowDragMove(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }
}