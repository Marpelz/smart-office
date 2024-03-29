using System.ComponentModel;

namespace SmartOffice.Models.OrderModels;

public class OrderModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }
    
    
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }

    public List<OrderDetailsModel> OrderDetails { get; set; }
}