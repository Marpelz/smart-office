using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartOffice.Models.RestaurantModels;

public class ZipCodeCityModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }
    
    
    public int ZipCodeCityId { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    
    public List<RestaurantAddressModel> RestaurantAddresses { get; set; }
}