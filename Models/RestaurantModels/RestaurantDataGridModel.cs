using ReactiveUI;

namespace SmartOffice.Models.RestaurantModels;

public class RestaurantDataGridModel : ReactiveObject
{
    private string _restaurantId = "";
    private string _restaurantName = "";
    private string _restaurantStreet = "";
    private string _restaurantZipcode = "";
    private string _restaurantCity = "";
    private string _restaurantType = "";
    private string _restaurantDelivery = "";
    

    public string RestaurantId
    {
        get => _restaurantId;
        set => this.RaiseAndSetIfChanged(ref _restaurantId, value);
    }
    
    public string RestaurantName
    {
        get => _restaurantName;
        set => this.RaiseAndSetIfChanged(ref _restaurantName, value);
    }
    
    public string RestaurantStreet
    {
        get => _restaurantStreet;
        set => this.RaiseAndSetIfChanged(ref _restaurantStreet, value);
    }
    
    public string RestaurantZipcode
    {
        get => _restaurantZipcode;
        set => this.RaiseAndSetIfChanged(ref _restaurantZipcode, value);
    }
    
    public string RestaurantCity
    {
        get => _restaurantCity;
        set => this.RaiseAndSetIfChanged(ref _restaurantCity, value);
    }
    
    public string RestaurantType
    {
        get => _restaurantType;
        set => this.RaiseAndSetIfChanged(ref _restaurantType, value);
    }
    
    public string RestaurantDelivery
    {
        get => _restaurantDelivery;
        set => this.RaiseAndSetIfChanged(ref _restaurantDelivery, value);
    }
}