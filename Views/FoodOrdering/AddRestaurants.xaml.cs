using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Services.FoodOrderServices.RestaurantServices;

namespace SmartOffice.Views.FoodOrdering;

public partial class AddRestaurants : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged; 
    private readonly IServiceProvider _service;
    private readonly IRestaurantService _restaurantService;
    private RestaurantModel _restaurantModel;
    private List<RestaurantDataGridModel> _restaurantDataGrid;
    
    public AddRestaurants(IServiceProvider service)
    {
        InitializeComponent();
        _service = service;
        _restaurantService = _service.GetRequiredService<IRestaurantService>();
        _restaurantModel = new RestaurantModel();
        _restaurantDataGrid = new List<RestaurantDataGridModel>();
        RestModel = new RestaurantModel();
        
        Task.Run(async () =>
        {
            await ReloadDataGrid();
        });
        
        DataContext = this;
    }
    
    //Propertys

    public RestaurantModel RestModel
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

    private async Task ReloadDataGrid()
    {
        RestDataGrid = await _restaurantService.ReadAllRestaurantsForGrid();
    }

    private async Task NewRestaurantModel()
    {
        try
        {
            var restaurants = await _restaurantService.ReadAllRestaurants();
            var lastRestaurantIdAsString = restaurants
                .Where(soresttab => !string.IsNullOrEmpty(soresttab.RestId))
                .Max(soresttab => soresttab.RestId);

            if (!string.IsNullOrEmpty(lastRestaurantIdAsString) && int.TryParse(lastRestaurantIdAsString, out int lastRestaurantId))
            {
                int newId = lastRestaurantId + 1;
                string newIdString = newId.ToString("D3");
                restId.Text = newIdString;
            }
            else
            {
                restId.Text = "001";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim Erstellen eines neuen Restaurants: {ex.Message}", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
    }

    private async Task SaveRestaurant()
    {
        try
        {
            if (RestModel.FoodorderRestaurantIdProp != "" &&
                RestModel.FoodorderRestaurantNameProp != "")
            {
                await _restaurantService.SaveRestaurant(RestModel);
                await ReloadDataGrid();
            }
            else
            {
                MessageBox.Show("Pflichtfelder: Restaurant-ID und Name sind nicht befüllt!");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim speichern eines neuen Restaurants: {ex.Message}", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task DeleteSelectedRestaurant()
    {
        var result = MessageBox.Show("Möchten Sie das ausgewählte Restaurant wirklich entfernen?", "Löschen bestätigen",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            await _restaurantService.DeleteRestaurantById(RestModel.FoodorderRestaurantIdProp);
            RestModel = new RestaurantModel();
            await ReloadDataGrid();
        }
    }
    
    // Click-Events

    private async void GetNewRestaurantModel_OnClick(object sender, RoutedEventArgs e)
    {
        await NewRestaurantModel();
    }

    private async void SaveRestaurant_OnClick(object sender, RoutedEventArgs e)
    {
        await SaveRestaurant();
    }
    
    private async void Delete_OnClick(object sender, RoutedEventArgs e)
    {
        await DeleteSelectedRestaurant();
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
    
    private void WindowDragMove(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }
}