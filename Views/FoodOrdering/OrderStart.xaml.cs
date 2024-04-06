using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using MaterialDesignExtensions.Controls;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using SmartOffice.Models.DishModels;
using SmartOffice.Models.OrderModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Services.FoodOrderServices.MenuService;
using SmartOffice.Services.FoodOrderServices.OrderService;
using SmartOffice.Services.FoodOrderServices.RestaurantService;
using SmartOffice.Services.UserService;

namespace SmartOffice.Views.FoodOrdering;

public partial class OrderStart : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly IServiceProvider _service;
    private readonly IRestaurantService _restaurantService;
    private readonly IDishService _dishService;
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;
    private RestaurantModel _selectedRestaurant;
    private DishModel _selectedDish;

    public OrderStart(IServiceProvider service)
    {
        InitializeComponent();
        _service = service;
        _restaurantService = _service.GetRequiredService<IRestaurantService>();
        _dishService = _service.GetRequiredService<IDishService>();
        _orderService = _service.GetRequiredService<IOrderService>();
        _userService = _service.GetRequiredService<IUserService>();
        _selectedRestaurant = new RestaurantModel();
        _selectedDish = new DishModel();
        Restaurants = new List<RestaurantModel>();
        Dishes = new List<DishModel>();
        OrderDetails = new List<OrderDetailsModel>();
        OrderViewDetails = new List<OrderDetailsViewModel>();

        stepper.SelectedIndex = 0;

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
    public List<OrderDetailsModel> OrderDetails { get; set; }
    public List<OrderDetailsViewModel> OrderViewDetails { get; set; }

    public RestaurantModel SelectedRestaurant
    {
        get => _selectedRestaurant;
        set
        {
            _selectedRestaurant = value;
            OnPropertyChanged(nameof(SelectedRestaurant));
        }
    }

    public DishModel SelectedDish
    {
        get => _selectedDish;
        set
        {
            _selectedDish = value;
            OnPropertyChanged(nameof(SelectedDish));
        }
    }

    // Functions

    private async Task LoadRestaurants()
    {
        var restaurants = await _restaurantService.ReadAllRestaurants();
        Restaurants = restaurants.Select(rest => new RestaurantModel
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
            FoodorderDishIdProp = dish.DishId,
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

    private async Task LoadOrderDetails()
{
    try
    {
        // Get last OrderId
        string lastOrderId = (await _orderService.ReadAllOrders()).LastOrDefault()?.OrderId;

        if (lastOrderId != null)
        {
            // Load all order details for the last created order
            var orderDetails = await _orderService.ReadOrderDetailsByOrderId(lastOrderId);

            // Create a list to store the formatted order details view models
            var formattedOrderDetails = new List<OrderDetailsViewModel>();

            // Iterate through each order detail and format the data
            foreach (var orderDetail in orderDetails)
            {
                // Retrieve additional information such as username, dish details, payment method, etc.
                var username = await _userService.GetUsernameById(orderDetail.UserId);
                var dishDesignation = await _dishService.ReadDishDesignationById(orderDetail.DishId);
                var paymentMethod = orderDetail.PaymentMethod;
                var price = await _dishService.ReadDishPriceById(orderDetail.DishId);

                // Create a view model object to hold the formatted data
                var orderDetailViewModel = new OrderDetailsViewModel
                {
                    OrderdetailsUsernameProp = username,
                    OrderdetailsDishDesignationProp = dishDesignation,
                    OrderdetailsUserPaymentMethodProp = paymentMethod,
                    OrderdetailsDishPriceProp = price
                };

                // Add the formatted order detail view model to the list
                formattedOrderDetails.Add(orderDetailViewModel);
            }

            // Update the OrderViewDetails property with the formatted order details view models
            OrderViewDetails = formattedOrderDetails;

            // Notify that the OrderViewDetails property has changed
            OnPropertyChanged(nameof(OrderViewDetails));
        }
        else
        {
            MessageBox.Show("Es gibt keine Bestellung, um Bestelldetails zu laden.", "Info", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Fehler beim Laden der Bestelldetails: {ex.Message}", "Fehler", MessageBoxButton.OK,
            MessageBoxImage.Error);
        Console.WriteLine($"Fehler beim Laden der Bestelldetails: {ex.Message}");
    }
}

    private async Task CreateNewOrder()
    {
        try
        {
            // Generate OrderId
            string lastOrderId = (await _orderService.ReadAllOrders()).LastOrDefault()?.OrderId ?? "0000";
            int newOrderId = int.Parse(lastOrderId) + 1;
            string orderId = newOrderId.ToString("0000");

            // Get UserId
            string username = AppSettings.Username;
            int userId = await _userService.GetUserIdByUsername(username);

            // Get RestaurantId
            string restaurantId = SelectedRestaurant.FoodorderRestaurantIdProp;

            // Set actual Date
            string orderDate = DateTime.Now.ToString("dd.MM.yyyy");


            OrderModel newOrder = new OrderModel
            {
                FoodorderOrderIdProp = orderId,
                FoodorderRestaurantIdProp = restaurantId,
                FoodorderUserIdProp = userId,
                FoodorderOrderDateProp = orderDate
            };

            await _orderService.SaveOrder(newOrder);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim Erstellen der Bestellung: {ex.Message}", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
            Console.WriteLine($"{ex.Message}");
        }
    }

    private async Task CreateNewOrderDetails()
    {
        try
        {
            // Get last OrderId
            string lastOrderId = (await _orderService.ReadAllOrders()).LastOrDefault()?.OrderId ?? "0000";

            // Get UserId
            string username = AppSettings.Username;
            int userId = await _userService.GetUserIdByUsername(username);

            // Get selected dishes
            List<DishModel> selectedDishes = Dishes.Where(dish => dish.IsSelected).ToList();

            if (selectedDishes.Count == 0)
            {
                MessageBox.Show("Bitte wählen Sie mindestens ein Gericht aus.", "Info", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            // Set PaymentMethod
            string paymentMethod = ((ComboBoxItem)paymentmethod.SelectedItem)?.Content.ToString();
            if (string.IsNullOrEmpty(paymentMethod))
            {
                MessageBox.Show("Bitte wählen Sie eine Zahlungsmethode aus.", "Info", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            foreach (var dish in selectedDishes)
            {
                // Create new OrderDetail for each selected dish
                OrderDetailsModel newOrderDetail = new OrderDetailsModel
                {
                    FoodorderOrderDetailsIdProp = lastOrderId + userId + dish.FoodorderDishIdProp,
                    FoodorderOrderIdProp = lastOrderId,
                    FoodorderUserIdProp = userId,
                    FoodorderDishIdProp = dish.FoodorderDishIdProp,
                    FoodorderPaymentMethodProp = paymentMethod
                };

                await _orderService.SaveOrderDetails(newOrderDetail);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task DeleteLastOder()
    {
        try
        {
            // Get last OrderId
            string lastOrderId = (await _orderService.ReadAllOrders()).LastOrDefault()?.OrderId;

            if (lastOrderId != null)
            {
                await _orderService.DeleteOrderById(lastOrderId);
                MessageBox.Show("Die letzte Bestellung wurde erfolgreich gelöscht.",
                    "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Es gibt keine Bestellung zum Löschen.", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim Löschen der letzten Bestellung: {ex.Message}",
                "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task DeleteLastOrderDetails()
    {
        try
        {
            // Get UserId
            var usrnamelabel = _service.GetRequiredService<HomeScreen>();
            string username = usrnamelabel.username.Content.ToString()!;
            int userId = await _userService.GetUserIdByUsername(username);

            // Get last OrderId
            string lastOrderId = (await _orderService.ReadAllOrders()).LastOrDefault()?.OrderId;

            if (lastOrderId != null)
            {
                // Retrieve the last created order details for the current user based on the order id
                var lastOrderDetailsForUser =
                    await _orderService.ReadOrderDetailsByOrderIdAndUserId(lastOrderId, userId);

                if (lastOrderDetailsForUser != null && lastOrderDetailsForUser.Any())
                {
                    // Delete the last created order details for the current user
                    foreach (var orderDetail in lastOrderDetailsForUser)
                    {
                        await _orderService.DeleteOrderDetailsById(orderDetail.OrderDetailsId);
                    }

                    MessageBox.Show("Die zuletzt hinzugefügten Bestelldetails wurden erfolgreich gelöscht.",
                        "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Es gibt keine Bestelldetails zum Löschen für den aktuellen Benutzer.",
                        "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Es gibt keine Bestellung zum Löschen von Bestelldetails.",
                    "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim Löschen der zuletzt hinzugefügten Bestelldetails: {ex.Message}",
                "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    // Click-Events

    private async void LoadStepTwo_OnClick(object sender, RoutedEventArgs e)
    {
        stepper.SelectedIndex++;
        await CreateNewOrder();
        await LoadDishes();
    }

    private async void LoadStepThree_OnClick(object sender, RoutedEventArgs e)
    {
        stepper.SelectedIndex++;
        await CreateNewOrderDetails();
        await LoadOrderDetails();
    }

    private void Back_OnClick(object sender, RoutedEventArgs e)
    {
        stepper.SelectedIndex--;
    }

    private async void BackToStepOne_OnClick(object sender, RoutedEventArgs e)
    {
        stepper.SelectedIndex--;
        await LoadRestaurants();
    }

    private async void BackToStepTwo_OnClick(object sender, RoutedEventArgs e)
    {
        stepper.SelectedIndex--;
        await DeleteLastOrderDetails();
        await LoadDishes();
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