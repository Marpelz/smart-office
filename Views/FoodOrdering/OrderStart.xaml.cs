using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;
using Notification.Wpf;
using SmartOffice.Models.DishModels;
using SmartOffice.Models.OrderModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Services.FoodOrderServices.DishServices;
using SmartOffice.Services.FoodOrderServices.OrderServices;
using SmartOffice.Services.FoodOrderServices.RestaurantServices;
using SmartOffice.Services.MQTTServices;
using SmartOffice.Services.UserServices;

namespace SmartOffice.Views.FoodOrdering;

public partial class OrderStart : Window, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly IServiceProvider _service;
    private readonly IRestaurantService _restaurantService;
    private readonly IDishService _dishService;
    private readonly IOrderService _orderService;
    private readonly IUserService _userService;
    private readonly IMqttService _mqttService;
    private readonly MqttService _mqttServiceInstance;
    private readonly NotificationManager _notification;
    private IManagedMqttClient _mqttClient;
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
        _mqttService = _service.GetRequiredService<IMqttService>();
        _mqttServiceInstance = (MqttService)_mqttService;
        _mqttClient = _mqttServiceInstance.GetMqttClient();
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

        var dishes = 
            await _dishService.ReadAllDishesForGridById(SelectedRestaurant.FoodorderRestaurantIdProp);

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

    public async Task LoadDishesFromLastOrder()
    {
        try
        {
            // Get last Order
            var lastOrder = (await _orderService.ReadAllOrders()).LastOrDefault();

            if (lastOrder != null)
            {
                // Get RestaurantId from last Order
                var restaurantId = lastOrder.RestaurantId;

                // Load Dishes
                var dishes = await _dishService.ReadAllDishesForGridById(restaurantId);
                
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
            else
            {
                MessageBox.Show("Es gibt keine vorherige Bestellung.", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim Laden der Speisen: {ex.Message}", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
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
            MessageBox.Show("Es gibt keine Bestellung, um Bestelldetails zu laden.", "Info",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Fehler beim Laden der Bestelldetails: {ex.Message}", "Fehler",
            MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Bitte wählen Sie mindestens ein Gericht aus.", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Set PaymentMethod
            string paymentMethod = ((ComboBoxItem)paymentmethod.SelectedItem)?.Content.ToString();
            if (string.IsNullOrEmpty(paymentMethod))
            {
                MessageBox.Show("Bitte wählen Sie eine Zahlungsmethode aus.", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
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
            var username = AppSettings.Username;
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
    
    private async Task PublishFoodOrderMessage()
    {
        try
        {
            string json = JsonConvert.SerializeObject(new { message = "Bestellung gestartet", sent = DateTimeOffset.UtcNow });

            await _mqttClient.PublishAsync("smartoffice/foodorder", json);
            
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Publishen der Nachricht: {ex.Message}");
        }
    }

    private async Task<string> CalculateUserOrderDetailsSum()
    {
        try
        {
            decimal sum = OrderViewDetails
                .Where(detail => detail.OrderdetailsUsernameProp == AppSettings.Username)
                .Sum(detail => decimal.Parse(detail.OrderdetailsDishPriceProp));

            
            return $"{sum:C}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim Berechnen der Bestellsumme: {ex.Message}", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return "Fehler";
        }
    }
    
    private async Task<string> CalculateOrderDetailsSum()
    {
        try
        {
            decimal sum = OrderViewDetails
                .Sum(detail => decimal.Parse(detail.OrderdetailsDishPriceProp));

            return $"{sum:C}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim Berechnen der Bestellsumme: {ex.Message}", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return "Fehler";
        }
    }

    private async Task GetOrderingUserInformation()
    {
        try
        {
            // Get last Order
            var lastOrder = (await _orderService.ReadAllOrders()).LastOrDefault();

            if (lastOrder != null)
            {
                // Get UserId
                string username = await _userService.GetUsernameById(lastOrder.UserId);

                // Get user's paypal email
                string paypalEmail = await _userService.GetUserPaypalEmailById(lastOrder.UserId);

                // Update the labels with user information
                orderingusername.Content = username;
                orderinguserpaypal.Content = paypalEmail;
            }
            else
            {
                MessageBox.Show("Es gibt keine vorherige Bestellung.", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim Laden der Benutzerinformationen: {ex.Message}", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task GetOrderingRestInformation()
    {
        try
        {
            // Get last Order
            var lastOrder = (await _orderService.ReadAllOrders()).LastOrDefault();

            if (lastOrder != null)
            {
                // Get Restaurantname
                var restdata = await _restaurantService.ReadRestaurantById(lastOrder.RestaurantId);
                string restname = restdata.FoodorderRestaurantNameProp;

                // Get Restaurant Deliver Costs
                string restdeliverycost = restdata.FoodorderRestaurantDeliveryCostProp;

                // Update the labels with Restaurant information
                orderingrestname.Content = restname;
                orderingrestdeliverycost.Content = restdeliverycost;
            }
            else
            {
                MessageBox.Show("Es gibt keine vorherige Bestellung.", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fehler beim Laden der Restaurantinformationen: {ex.Message}", "Fehler",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    // Click-Events

    private async void LoadStepTwo_OnClick(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Möchten Sie wirklich beim ausgewählten Restaurant bestellen? Vorgang kann nicht rückgängig gemacht werden.", "Bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            stepper.SelectedIndex++;
            await CreateNewOrder();
            await LoadDishes();
            await PublishFoodOrderMessage();
        }
        else
        {
            return;
        }
    }

    private async void LoadStepThree_OnClick(object sender, RoutedEventArgs e)
    {
        var userId = await _userService.GetUserIdByUsername(AppSettings.Username);
        var lastOrder = (await _orderService.ReadAllOrders()).LastOrDefault();

        if (paymentmethod.SelectedItem == null)
        {
            MessageBox.Show("Bitte wählen Sie eine Zahlungsmethode aus.", "Hinweis", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }
        
        MessageBoxResult result = MessageBox.Show("Möchten Sie die ausgewählten Speisen wirklich hinzufügen? Vorgang kann nicht rückgängig gemacht werden.", "Bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            stepper.SelectedIndex++;
            await CreateNewOrderDetails();
            await LoadOrderDetails();

            if (lastOrder != null && userId == lastOrder.UserId)
            {
                string orderSumAsString = await CalculateOrderDetailsSum();
                orderingdetailsum.Content = orderSumAsString;
                await GetOrderingRestInformation();
                restorderinginformation.Visibility = Visibility.Visible;
            }
            else
            {
                string userSumAsString = await CalculateUserOrderDetailsSum();
                userdetailsum.Content = userSumAsString;
                await GetOrderingUserInformation();
                userorderinginformation.Visibility = Visibility.Visible;
            }
        }
        else
        {
            return;
        }
        
    }

    private async void ReloadOrderDetails_OnClick(object sender, RoutedEventArgs e)
    {
        await LoadOrderDetails();
        
        var userId = await _userService.GetUserIdByUsername(AppSettings.Username);
        var lastOrder = (await _orderService.ReadAllOrders()).LastOrDefault();
        
        if (lastOrder != null && userId == lastOrder.UserId)
        {
            string orderSumAsString = await CalculateOrderDetailsSum();
            orderingdetailsum.Content = orderSumAsString;
            await GetOrderingRestInformation();
            restorderinginformation.Visibility = Visibility.Visible;
        }
        else
        {
            string userSumAsString = await CalculateUserOrderDetailsSum();
            userdetailsum.Content = userSumAsString;
            await GetOrderingUserInformation();
            userorderinginformation.Visibility = Visibility.Visible;
        }
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

    private void DishListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListView listView && listView.SelectedItem is DishModel selectedDish)
        {
            SelectedDish = selectedDish;
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