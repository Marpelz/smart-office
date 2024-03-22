using System.ComponentModel;
using SmartOffice.Models.MenuModels;
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
    
    
    public OrderModel Order { get; set; }
    public int OrderId { get; set; }

    
    public UserModel User { get; set; }
    public int UserId { get; set; }

    
    public UserPaymentMethodModel UserPaymentMethod { get; set; }
    public int UserPaymentMethodId { get; set; }

    
    public DishModel Dish { get; set; }
    public int DishId { get; set; }
}