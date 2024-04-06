using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartOffice.Models.DishModels;
using SmartOffice.Models.OrderModels;

namespace SmartOffice.Models.RestaurantModels;

public class RestaurantModel : INotifyPropertyChanged
{ 
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }
    
    public List<DishModel> Dishes { get; set; }
    public List<OrderModel> Orders { get; set; }

    private string _foodorderRestaurantId = ""; // 3-stellige ID (z.B. 001)
    private string _foodorderRestaurantName = ""; 
    private string _foodorderRestaurantStreet = "";
    private string _foodorderRestaurantZipcode = "";
    private string _foodorderRestaurantCity = "";
    private string _foodorderRestaurantType = "";
    private string _foodorderRestaurantPhonenumber = "";
    public string _foodorderRestaurantDeliveryYesNo { get; set; } = "";
    private string _foodorderRestaurantDeliveryTime = "";
    public string _foodorderRestaurantOrdertypeAppTelephone { get; set; } = "";
    private string _foodorderRestaurantMinimalOrderValue = "";
    private string _foodorderRestaurantDeliveryCost = "";
    private string _foodorderRestaurantLieferandoLink = "";
    
    
    public string FoodorderRestaurantIdProp
    {
        get => _foodorderRestaurantId;
        set
        {
            _foodorderRestaurantId = value;
            OnPropertyChanged(nameof(FoodorderRestaurantIdProp));
        }
    }
    
    public string FoodorderRestaurantNameProp
    {
        get => _foodorderRestaurantName;
        set
        {
            _foodorderRestaurantName = value;
            OnPropertyChanged(nameof(FoodorderRestaurantNameProp));
        }
    }
    
    public string FoodorderRestaurantStreetProp
    {
        get => _foodorderRestaurantStreet;
        set
        {
            _foodorderRestaurantStreet = value;
            OnPropertyChanged(nameof(FoodorderRestaurantStreetProp));
        }
    }
    
    public string FoodorderRestaurantZipcodeProp
    {
        get => _foodorderRestaurantZipcode;
        set
        {
            _foodorderRestaurantZipcode = value;
            OnPropertyChanged(nameof(FoodorderRestaurantZipcodeProp));
        }
    }
    
    public string FoodorderRestaurantCityProp
    {
        get => _foodorderRestaurantCity;
        set
        {
            _foodorderRestaurantCity = value;
            OnPropertyChanged(nameof(FoodorderRestaurantCityProp));
        }
    }
    
    public string FoodorderRestaurantTypeProp
    {
        get => _foodorderRestaurantType;
        set
        {
            _foodorderRestaurantType = value;
            OnPropertyChanged(nameof(FoodorderRestaurantTypeProp));
        }
    }
    
    public string FoodorderRestaurantPhonenumberProp
    {
        get => _foodorderRestaurantPhonenumber;
        set
        {
            _foodorderRestaurantPhonenumber = value;
            OnPropertyChanged(nameof(FoodorderRestaurantPhonenumberProp));
        }
    }

    public Delivery FoodorderRestaurantDeliveryYesNoProp
    {
        get
        {
            var ret = Delivery.None;
            if (_foodorderRestaurantDeliveryYesNo == "Ja")
                ret = Delivery.Yes;
            else if (_foodorderRestaurantDeliveryYesNo == "Nein")
                ret = Delivery.No;
            else if (_foodorderRestaurantDeliveryYesNo == "k.A.")
                ret = Delivery.None;
            return ret;
        }
        set
        {
            if (value == Delivery.Yes)
                _foodorderRestaurantDeliveryYesNo = "Ja";
            else if (value == Delivery.No)
                _foodorderRestaurantDeliveryYesNo = "Nein";
            else if (value == Delivery.None)
                _foodorderRestaurantDeliveryYesNo = "k.A.";
        }
    }
    
    public string FoodorderRestaurantDeliveryTimeProp
    {
        get => _foodorderRestaurantDeliveryTime;
        set
        {
            _foodorderRestaurantDeliveryTime = value;
            OnPropertyChanged(nameof(FoodorderRestaurantDeliveryTimeProp));
        }
    }
    
    public Ordertype FoodorderRestaurantOrdertypeAppTelephoneProp
    {
        get
        {
            var ret = Ordertype.None;
            if (_foodorderRestaurantOrdertypeAppTelephone == "App")
                ret = Ordertype.App;
            else if (_foodorderRestaurantOrdertypeAppTelephone == "Telefonisch")
                ret = Ordertype.Telephone;
            else if (_foodorderRestaurantOrdertypeAppTelephone == "k.A.")
                ret = Ordertype.None;
            return ret;
        }
        set
        {
            if (value == Ordertype.App)
                _foodorderRestaurantOrdertypeAppTelephone = "App";
            else if (value == Ordertype.Telephone)
                _foodorderRestaurantOrdertypeAppTelephone = "Telefonisch";
            else if (value == Ordertype.None)
                _foodorderRestaurantOrdertypeAppTelephone = "k.A.";
        }
    }
    
    public string FoodorderRestaurantMinimalOrderValueProp
    {
        get => _foodorderRestaurantMinimalOrderValue;
        set
        {
            _foodorderRestaurantMinimalOrderValue = value;
            OnPropertyChanged(nameof(FoodorderRestaurantMinimalOrderValueProp));
        }
    }
    
    public string FoodorderRestaurantDeliveryCostProp
    {
        get => _foodorderRestaurantDeliveryCost;
        set
        {
            _foodorderRestaurantDeliveryCost = value;
            OnPropertyChanged(nameof(FoodorderRestaurantDeliveryCostProp));
        }
    }
    
    public string FoodorderRestaurantLieferandoLinkProp
    {
        get => _foodorderRestaurantLieferandoLink;
        set
        {
            _foodorderRestaurantLieferandoLink = value;
            OnPropertyChanged(nameof(FoodorderRestaurantLieferandoLinkProp));
        }
    }
}