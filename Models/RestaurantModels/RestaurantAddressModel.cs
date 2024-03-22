using System.ComponentModel;

namespace SmartOffice.Models.RestaurantModels;

public class RestaurantAddressModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }
    
    
    public int RestaurantAddressId { get; set; }
    public string RestaurantStreet { get; set; }
    public string RestaurantHouseNumber { get; set; }
    
    
    public RestaurantModel Restaurant { get; set; }
    public int RestaurantId { get; set; }
    public ZipCodeCityModel ZipCodeCity { get; set; }
    public int RestaurantZipCodeCityId { get; set; }
}