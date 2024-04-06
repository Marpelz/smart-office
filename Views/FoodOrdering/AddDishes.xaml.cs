using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SmartOffice.Models.DishModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.Settings;
using SmartOffice.Services.FoodOrderServices.MenuService;
using SmartOffice.Services.FoodOrderServices.RestaurantService;

namespace SmartOffice.Views.FoodOrdering;

public partial class AddDishes : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged; 
    private readonly IServiceProvider _service;
    private readonly IDishService _dishService;
    private readonly IRestaurantService _restaurantService;
    private DishModel _dishModel;
    private List<DishDataGridModel> _dishDataGrid;
    private List<IdentSchluessel> _restaurantCmbx;
    
    public AddDishes(IServiceProvider service)
    {
        InitializeComponent();
        _service = service;
        _dishService = _service.GetRequiredService<IDishService>();
        _restaurantService = _service.GetRequiredService<IRestaurantService>();
        _dishModel = new DishModel();
        Task.Run(async () =>
        {
            await InitData();
        });
        
        DataContext = this;
    }
    
    // Propertys

    public DishModel DishModel
    {
        get => _dishModel;
        set
        {
            _dishModel = value;
            OnPropertyChanged(nameof(DishModel));
        }
    }
    
    public List<DishDataGridModel> DishDataGrid
    {
        get => _dishDataGrid;
        set
        {
            _dishDataGrid = value;
            OnPropertyChanged(nameof(DishDataGrid));
        }
    }
    
    public List<IdentSchluessel> Restaurants
    {
        get => _restaurantCmbx;
        set
        {
            _restaurantCmbx = value;
            OnPropertyChanged(nameof(Restaurants));
        }
    }
    
    // Functions
    
    private async Task LoadGridForSelectedRestaurant()
    {
        var restaurantId = DishModel.FoodorderDishRestaurantIdProp;
        DishDataGrid = await _dishService.ReadAllDishesForGridById(restaurantId);
    }
    
    private async void DishDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var dishRepo = _service.GetRequiredService<IDishService>();
        var dataGrid = (DataGrid)sender;

        var selected = dataGrid.SelectedItem as DishDataGridModel;

        if (selected != null)
        {
            var dish = await dishRepo.ReadDishById(selected.DishRestaurantId, selected.DishNumber);
            DishModel = dish;
        }
    }

    public async Task InitData()
    {
        Restaurants = await _restaurantService.ReadAllRestaurantsById() ?? new List<IdentSchluessel>();
    }
    
    private async Task NewDishModel()
    {
        dishcategory.Text = "";
        dishdesignation.Text = "";
        dishcontents.Text = "";
        dishadditionals.Text = "";
        dishprice.Text = "";

        await LoadGridForSelectedRestaurant();

        if (DishDataGrid != null && DishDataGrid.Any())
        {
            List<int> dishNumbers = DishDataGrid.Select(dish => int.Parse(dish.DishNumber)).ToList();

            int maxDishNumber = dishNumbers.Max();

            int newDishNumber = maxDishNumber + 1;

            string newDishNumberString = newDishNumber.ToString("D3");

            dishnumber.Text = newDishNumberString;
        }
        else
        {
            dishnumber.Text = "001";
        }
    }
    
    private async Task SaveDish()
    {
        Debug.WriteLine($"FoodorderFoodNumberProp: {DishModel?.FoodorderDishNumberProp}");
        Debug.WriteLine($"FoodorderFoodDesignationProp: {DishModel?.FoodorderDishDesignationProp}");
        
        if (DishModel?.FoodorderDishNumberProp != "" && 
            DishModel?.FoodorderDishDesignationProp != "")
        {
            await _dishService.SaveDish(DishModel);
            await LoadGridForSelectedRestaurant();
        }
        else
        {
            MessageBox.Show("Pflichtfelder: Speise-Nr. und Bezeichnung sind nicht befüllt!");
        }
    }

    private async Task DeleteSelectedDish()
    {
        var result = MessageBox.Show("Möchten Sie die ausgewählte Speise wirklich entfernen?", "Löschen bestätigen",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            await _dishService.DeleteDishById(DishModel.FoodorderDishRestaurantIdProp, DishModel.FoodorderDishNumberProp);
            await NewDishModel();
            await LoadGridForSelectedRestaurant();
        }
    }
    
    
    // Click-Events

    private async void SearchDishForRestaurant_OnClick(object sender, RoutedEventArgs e)
    {
        await LoadGridForSelectedRestaurant();
    }

    private async void GetNewDishModel_OnClick(object sender, RoutedEventArgs e)
    {
        await NewDishModel();
    }
    
    private async void SaveDish_OnClick(object sender, RoutedEventArgs e)
    {
        await SaveDish();
    }
    
    private async void Delete_OnClick(object sender, RoutedEventArgs e)
    {
        await DeleteSelectedDish();
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
    
    private void WindowDragMove(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }
    
    
}