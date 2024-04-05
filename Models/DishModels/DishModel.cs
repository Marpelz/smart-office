using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SmartOffice.Models.OrderModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.Settings;

namespace SmartOffice.Models.MenuModels;

public class DishModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }
    
    public RestaurantModel Restaurant { get; set; } // FK
    public List<OrderDetailsModel> OrderDetails { get; set; }

    private bool _isSelected;
    private string _foodorderDishId = ""; // RestaurantId + FoodNumber 
    private string _foodorderDishRestaurantId = ""; // FK
    private string _foodorderDishNumber = "";
    private string _foodorderDishCategory = "";
    private string _foodorderDishDesignation = "";
    private string _foodorderDishContents = "";
    private string _foodorderDishAdditionalSelection = "";
    private string _foodorderDishPrice = "";
    
    
    public bool IsSelected
    {
        get { return _isSelected; }
        set
        {
            if (_isSelected != value)
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }
    
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