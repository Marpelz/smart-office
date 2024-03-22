using System.ComponentModel;

namespace SmartOffice.Models.UserModels;

public class PaymentMethodModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }
    
    public int PaymentMethodId { get; set; }
    public string PaymentMethod { get; set; }
    
    
    public List<UserPaymentMethodModel> UserPaymentMethods { get; set; }
}