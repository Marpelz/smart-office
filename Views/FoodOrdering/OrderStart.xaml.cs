using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignExtensions.Controls;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using SmartOffice.Models.MenuModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Services.FoodOrderServices.MenuService;
using SmartOffice.Services.FoodOrderServices.RestaurantService;

namespace SmartOffice.Views.FoodOrdering;

public partial class OrderStart : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged; 
    private readonly IServiceProvider _service;
    private readonly IRestaurantService _restaurantService;
    private readonly IDishService _dishService;
    private RestaurantModel _selectedRestaurant;
    
    public OrderStart(IServiceProvider service)
    {
        InitializeComponent();
        _service = service;
        _restaurantService = _service.GetRequiredService<IRestaurantService>();
        _dishService = _service.GetRequiredService<IDishService>();
        _selectedRestaurant = new RestaurantModel();
        Restaurants = new List<RestaurantModel>();
        Dishes = new List<DishModel>();
        
        Task.Run(async () =>
        {
            await LoadRestaurants();
            if (Restaurants != null && Restaurants.Count > 0)
            {
                SelectedRestaurant = Restaurants[0];
            }
        });
        
        DataContext = this;
    }
    
    // Propertys
    
    public List<RestaurantModel> Restaurants { get; set; }
    public List<DishModel> Dishes { get; set; }
    
    public RestaurantModel SelectedRestaurant
    {
        get => _selectedRestaurant;
        set
        {
            _selectedRestaurant = value;
            OnPropertyChanged(nameof(SelectedRestaurant));
        }
    }

    // Functions
    
    private async Task LoadRestaurants()
    {
        var soresttabs = await _restaurantService.ReadAllRestaurants();
        Restaurants = soresttabs.Select(rest => new RestaurantModel
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

    private async Task LoadDishes()
    {
        if (SelectedRestaurant == null)
        {
            return;
        }

        var dishes = await _dishService.ReadAllDishesForGridById(SelectedRestaurant.FoodorderRestaurantIdProp);

        Dishes = dishes.Select(dish => new DishModel
        {
            FoodorderDishRestaurantIdProp = dish.DishRestaurantId,
            FoodorderDishNumberProp = dish.DishNumber,
            FoodorderDishCategoryProp = dish.DishCategory,
            FoodorderDishDesignationProp = dish.DishDesignation,
            FoodorderDishContentsProp = dish.DishContents,
            FoodorderDishAdditionalSelectionProp = dish.DishAdditionalSelection,
            FoodorderDishPriceProp = dish.DishPrice
        }).ToList();

        OnPropertyChanged(nameof(Dishes));
    }
    
    // Click-Events

    private async void LoadStepTwo_OnClick(object sender, RoutedEventArgs e)
    {
        stepper.SelectedIndex++;
        await LoadDishes();
    }

    private async void LoadStepThree_OnClick(object sender, RoutedEventArgs e)
    {
        stepper.SelectedIndex++;
    }

    private void Back_OnClick(object sender, RoutedEventArgs e)
    {
        stepper.SelectedIndex--;
    }
    
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
    
    private void RestListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListView listView && listView.SelectedItem is RestaurantModel selectedRestaurant)
        {
            SelectedRestaurant = selectedRestaurant;
        }
    }
    
    private void WindowDragMove(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }
}