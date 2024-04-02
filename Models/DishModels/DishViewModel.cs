using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.Settings;

namespace SmartOffice.Models.MenuModels;

public class DishViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }
    
    public RestaurantViewModel Restaurant { get; set; }

    private string _foodorderDishId = ""; // RestaurantId + FoodNumber 
    private string _foodorderDishRestaurantId = ""; 
    private string _foodorderDishNumber = "";
    private string _foodorderDishCategory = "";
    private string _foodorderDishDesignation = "";
    private string _foodorderDishContents = "";
    private string _foodorderDishAdditionalSelection = "";
    private string _foodorderDishPrice = "";
    
    
    public string FoodorderDishIdProp
    {
        get => _foodorderDishId;
        set
        {
            _foodorderDishId = value;
            OnPropertyChanged(nameof(FoodorderDishIdProp));
        }
    }
    
    public string FoodorderDishRestaurantIdProp
    {
        get => _foodorderDishRestaurantId;
        set
        {
            _foodorderDishRestaurantId = value;
            OnPropertyChanged(nameof(FoodorderDishRestaurantIdProp));
        }
    }

    public IdentSchluessel FoodorderDishRestaurantIdPropObj
    {
        get
        {
            var idk = new IdentSchluessel();
            idk.Key = _foodorderDishRestaurantId;
            return idk;
        }
        set
        {
            _foodorderDishRestaurantId = value.Key;
            OnPropertyChanged(nameof(FoodorderDishRestaurantIdProp));
        }
    }
    
    public string FoodorderDishNumberProp
    {
        get => _foodorderDishNumber;
        set
        {
            _foodorderDishNumber = value;
            OnPropertyChanged(nameof(FoodorderDishNumberProp));
        }
    }
    
    public string FoodorderDishCategoryProp
    {
        get => _foodorderDishCategory;
        set
        {
            _foodorderDishCategory = value;
            OnPropertyChanged(nameof(FoodorderDishCategoryProp));
        }
    }
    
    public string FoodorderDishDesignationProp
    {
        get => _foodorderDishDesignation;
        set
        {
            _foodorderDishDesignation = value;
            OnPropertyChanged(nameof(FoodorderDishDesignationProp));
        }
    }
    
    public string FoodorderDishContentsProp
    {
        get => _foodorderDishContents;
        set
        {
            _foodorderDishContents = value;
            OnPropertyChanged(nameof(FoodorderDishContentsProp));
        }
    }
    
    public string FoodorderDishAdditionalSelectionProp
    {
        get => _foodorderDishAdditionalSelection;
        set
        {
            _foodorderDishAdditionalSelection = value;
            OnPropertyChanged(nameof(FoodorderDishAdditionalSelectionProp));
        }
    }
    
    public string FoodorderDishPriceProp
    {
        get => _foodorderDishPrice;
        set
        {
            _foodorderDishPrice = value;
            OnPropertyChanged(nameof(FoodorderDishPriceProp));
        }
    }
}