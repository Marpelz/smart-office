using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Services.FoodOrderServices.RestaurantService;

namespace SmartOffice.Views.FoodOrdering;

public partial class AddRestaurants : Window
{
    public event PropertyChangedEventHandler? PropertyChanged; 
    private readonly IServiceProvider _service;
    private readonly IRestaurantService _restaurantService;
    private RestaurantViewModel _restaurantModel;
    private List<RestaurantDataGridModel> _restaurantDataGrid;
    
    public AddRestaurants(IServiceProvider service)
    {
        InitializeComponent();
        _service = service;
        _restaurantService = _service.GetRequiredService<IRestaurantService>();
        _restaurantModel = new RestaurantViewModel();
        DataContext = this;
    }
    
    //Propertys

    public RestaurantViewModel RestModel
    {
        get => _restaurantModel;
        set
        {
            _restaurantModel = value;
            OnPropertyChanged(nameof(RestModel));
        }
    }

    public List<RestaurantDataGridModel> RestDataGrid
    {
        get => _restaurantDataGrid;
        set
        {
            _restaurantDataGrid = value;
            OnPropertyChanged(nameof(RestDataGrid));
        }
    }
    
    // Functions
    
    private async void RestaurantDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var restRepo = _service.GetRequiredService<IRestaurantService>();
        var dataGrid = (DataGrid)sender;

        var selected = dataGrid.SelectedItem as RestaurantDataGridModel;

        if (selected != null)
        {
            var rest = await restRepo.ReadRestaurantById(selected.RestaurantId);
            RestModel = rest;
        }
    }

    private async Task ReloadDataGrid()
    {
        RestDataGrid = await _restaurantService.ReadAllRestaurantsForGrid();
    }
    
    // Click-Events
    
    private void Delete_OnClick(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }
    
    private void Close_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
    // Helper
    
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