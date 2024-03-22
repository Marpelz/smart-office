using System.ComponentModel;
using SmartOffice.Models.OrderModels;
using SmartOffice.Models.RestaurantModels;

namespace SmartOffice.Models.MenuModels;

public class DishModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }
    
    public int DishId { get; set; }
    public string DishNumber { get; set; }
    public string DishCategory { get; set; }
    public string DishName { get; set; }
    public string DishContents { get; set; }
    public string DishAdditionalOptions { get; set; }
    public decimal DishPrice { get; set; }

    
    public List<OrderDetailsModel> OrderDetails { get; set; }

    
    public RestaurantModel Restaurant { get; set; }
    public int RestaurantId { get; set; }
}