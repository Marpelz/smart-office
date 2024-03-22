using System.ComponentModel;
using SmartOffice.Models.OrderModels;

namespace SmartOffice.Models.UserModels;

public class UserPaymentMethodModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }
    
    public int UserPaymentMethodId { get; set; }
    public string UserPaymentMethodAdditive { get; set; }

    

    public UserModel User { get; set; }

    public int UserId { get; set; }
    
    public PaymentMethodModel PaymentMethod { get; set; }

    public int PaymentMethodId { get; set; }

    
    
    public List<OrderDetailsModel> OrderDetails { get; set; }
}