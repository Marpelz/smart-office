using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using SmartOffice.Models.MenuModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.Settings;
using SmartOffice.Services.FoodOrderServices.MenuService;
using SmartOffice.Services.FoodOrderServices.RestaurantService;

namespace SmartOffice.Views.FoodOrdering;

public partial class AddMenus : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged; 
    private readonly IServiceProvider _service;
    private readonly IMenuService _menuService;
    private readonly IRestaurantService _restaurantService;
    private MenuViewModel _menuModel;
    private List<MenuDataGridModel> _menuDataGrid;
    private List<IdentSchluessel> _restaurantCmbx;
    
    public AddMenus(IServiceProvider service)
    {
        InitializeComponent();
        _service = service;
        _menuService = _service.GetRequiredService<IMenuService>();
        _restaurantService = _service.GetRequiredService<IRestaurantService>();
        _menuModel = new MenuViewModel();
        Task.Run(async () =>
        {
            await InitData();
        });
        
        DataContext = this;
    }
    
    // Propertys

    public MenuViewModel MenuModel
    {
        get => _menuModel;
        set
        {
            _menuModel = value;
            OnPropertyChanged(nameof(MenuModel));
        }
    }
    
    public List<MenuDataGridModel> MenuDataGrid
    {
        get => _menuDataGrid;
        set
        {
            _menuDataGrid = value;
            OnPropertyChanged(nameof(MenuDataGrid));
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
        var restaurantId = MenuModel.FoodorderFoodRestaurantIdProp;
        MenuDataGrid = await _menuService.ReadAllMenusForGridById(restaurantId);
    }
    
    private async void MenuDataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var menuRepo = _service.GetRequiredService<IMenuService>();
        var dataGrid = (DataGrid)sender;

        var selected = dataGrid.SelectedItem as MenuDataGridModel;

        if (selected != null)
        {
            var menu = await menuRepo.ReadMenuById(selected.FoodRestaurantId, selected.FoodNumber);
            MenuModel = menu;
        }
    }

    public async Task InitData()
    {
        Restaurants = await _restaurantService.ReadAllRestaurants() ?? new List<IdentSchluessel>();
    }
    
    private async Task NewMenuModel()
    {
        foodcategory.Text = "";
        fooddesignation.Text = "";
        foodcontents.Text = "";
        foodadditionals.Text = "";
        foodprice.Text = "";

        await LoadGridForSelectedRestaurant();

        if (MenuDataGrid != null && MenuDataGrid.Any())
        {
            List<int> foodNumbers = MenuDataGrid.Select(menu => int.Parse(menu.FoodNumber)).ToList();

            int maxFoodNumber = foodNumbers.Max();

            int newFoodNumber = maxFoodNumber + 1;

            string newFoodNumberString = newFoodNumber.ToString("D3");

            foodnumber.Text = newFoodNumberString;
        }
        else
        {
            foodnumber.Text = "001";
        }
    }
    
    private async Task SaveMenu()
    {
        Debug.WriteLine($"FoodorderFoodNumberProp: {MenuModel?.FoodorderFoodNumberProp}");
        Debug.WriteLine($"FoodorderFoodDesignationProp: {MenuModel?.FoodorderFoodDesignationProp}");
        
        if (MenuModel?.FoodorderFoodNumberProp != "" && 
            MenuModel?.FoodorderFoodDesignationProp != "")
        {
            await _menuService.SaveMenu(MenuModel);
            await LoadGridForSelectedRestaurant();
        }
        else
        {
            MessageBox.Show("Pflichtfelder: Speise-Nr. und Bezeichnung sind nicht befüllt!");
        }
    }

    private async Task DeleteSelectedMenu()
    {
        var result = MessageBox.Show("Möchten Sie die ausgewählte Speise wirklich entfernen?", "Löschen bestätigen",
            MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            await _menuService.DeleteMenuById(MenuModel.FoodorderFoodRestaurantIdProp, MenuModel.FoodorderFoodNumberProp);
            await NewMenuModel();
            await LoadGridForSelectedRestaurant();
        }
    }
    
    
    // Click-Events

    private async void SearchMenuForRestaurant_OnClick(object sender, RoutedEventArgs e)
    {
        await LoadGridForSelectedRestaurant();
    }

    private async void GetNewMenuModel_OnClick(object sender, RoutedEventArgs e)
    {
        await NewMenuModel();
    }
    
    private async void SaveMenu_OnClick(object sender, RoutedEventArgs e)
    {
        await SaveMenu();
    }
    
    private async void Delete_OnClick(object sender, RoutedEventArgs e)
    {
        await DeleteSelectedMenu();
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