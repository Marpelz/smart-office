using System.ComponentModel;
using SmartOffice.Models.MenuModels;

namespace SmartOffice.Models.RestaurantModels;

public class RestaurantModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }
    
    
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string RestaurantType { get; set; }
    public string RestaurantPhoneNumber { get; set; }
    public string RestaurantDeliveryAvailability { get; set; }
    public string RestaurantOrderingMethodAppPhone { get; set; }
    public decimal RestaurantMinimumOrderAmount { get; set; }
    public decimal RestaurantDeliveryCosts { get; set; }
    public string RestaurantLieferandoLink { get; set; }

    
    public List<RestaurantAddressModel> RestaurantAddresses { get; set; }
    public RestaurantOpeningAndDeliveryTimeModel RestaurantOpeningAndDeliveryTime { get; set; }

    
    public List<DishModel> Dishes { get; set; }
}
