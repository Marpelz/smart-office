using System.ComponentModel;
using SmartOffice.Models.DishModels;
using SmartOffice.Models.UserModels;

namespace SmartOffice.Models.OrderModels;

public class OrderDetailsModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }
    
    public OrderModel Order { get; set; } // FK
    public UserModel User { get; set; } // FK
    public DishModel Dish { get; set; } // FK

    private string _foodorderOrderDetailsId = ""; // PK
    private string _foodorderOrderId = ""; // FK
    private int _foodorderUserId = int.MinValue; // Fk
    private string _foodorderDishId = ""; // FK
    private string _foodorderPaymentMethod = ""; 
    
    
    public string FoodorderOrderDetailsIdProp
    {
        get => _foodorderOrderDetailsId;
        set
        {
            _foodorderOrderDetailsId = value;
            OnPropertyChanged(nameof(FoodorderOrderDetailsIdProp));
        }
    }
    
    public string FoodorderOrderIdProp
    {
        get => _foodorderOrderId;
        set
        {
            _foodorderOrderId = value;
            OnPropertyChanged(nameof(FoodorderOrderIdProp));
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
    
    public string FoodorderDishIdProp
    {
        get => _foodorderDishId;
        set
        {
            _foodorderDishId = value;
            OnPropertyChanged(nameof(FoodorderDishIdProp));
        }
    }
    
    public string FoodorderPaymentMethodProp
    {
        get => _foodorderPaymentMethod;
        set
        {
            _foodorderPaymentMethod = value;
            OnPropertyChanged(nameof(FoodorderPaymentMethodProp));
        }
    }
}