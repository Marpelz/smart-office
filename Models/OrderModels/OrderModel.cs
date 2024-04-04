using System.ComponentModel;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.UserModels;

namespace SmartOffice.Models.OrderModels;

public class OrderModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }

    public RestaurantModel Restaurant { get; set; } // FK
    public UserModel User { get; set; } // FK
    public List<OrderDetailsModel> OrderDetails { get; set; }

    private string _foodorderOrderId = ""; // PK 
    private string _foodorderRestaurantId = ""; // FK
    private int _foodorderUserId = int.MinValue; // FK
    private string _foodorderOrderDate = ""; // Actual Date
    
    
    public string FoodorderOrderIdProp
    {
        get => _foodorderOrderId;
        set
        {
            _foodorderOrderId = value;
            OnPropertyChanged(nameof(FoodorderOrderIdProp));
        }
    }
    
    public string FoodorderRestaurantIdProp
    {
        get => _foodorderRestaurantId;
        set
        {
            _foodorderRestaurantId = value;
            OnPropertyChanged(nameof(FoodorderRestaurantIdProp));
        }
    }
    
    public int FoodorderUserIdProp
    {
        get => _foodorderUserId;
        set
        {
            _foodorderUserId = value;
            OnPropertyChanged(nameof(FoodorderUserIdProp));
        }
    }
    
    public string FoodorderOrderDateProp
    {
        get => _foodorderOrderDate;
        set
        {
            _foodorderOrderDate = value;
            OnPropertyChanged(nameof(FoodorderOrderDateProp));
        }
    }
}