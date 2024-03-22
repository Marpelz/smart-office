using System.ComponentModel;

namespace SmartOffice.Models.RestaurantModels;

public class RestaurantOpeningAndDeliveryTimeModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }
    
    public int RestaurantTimeId { get; set; }
    public string OpeningTimeMonday { get; set; }
    public string OpeningTimeTuesday { get; set; }
    public string OpeningTimeWednesday { get; set; }
    public string OpeningTimeThursday { get; set; }
    public string OpeningTimeFriday { get; set; }
    public string OpeningTimeSaturday { get; set; }
    public string OpeningTimeSunday { get; set; }
    public string DeliveryTimeFrom { get; set; }

    
    public RestaurantModel Restaurant { get; set; }
    public int RestaurantId { get; set; }
}