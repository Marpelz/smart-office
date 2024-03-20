using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using SmartOffice.Models.RestaurantModels;

namespace SmartOffice.Models.MenuModels;

public class MenuViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string info)
    {
        var handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(info));
    }

    private string _foodorderRestaurantId = "";
    private string _foodorderFoodNumber = "";
    private string _foodorderFoodCategory = "";
    private string _foodorderFoodDesignation = "";
    private string _foodorderFoodContents = "";
    private string _foodorderFoodAdditionalSelection = "";
    private string _foodorderFoodPrice = "";
    
    
    public string FoodorderRestaurantIdProp
    {
        get => _foodorderRestaurantId;
        set
        {
            _foodorderRestaurantId = value;
            OnPropertyChanged(nameof(FoodorderRestaurantIdProp));
        }
    }
    
    public string FoodorderFoodNumberProp
    {
        get => _foodorderFoodNumber;
        set
        {
            _foodorderFoodNumber = value;
            OnPropertyChanged(nameof(FoodorderFoodNumberProp));
        }
    }
    
    public string FoodorderFoodCategoryProp
    {
        get => _foodorderFoodCategory;
        set
        {
            _foodorderFoodCategory = value;
            OnPropertyChanged(nameof(FoodorderFoodCategoryProp));
        }
    }
    
    public string FoodorderFoodDesignationProp
    {
        get => _foodorderFoodDesignation;
        set
        {
            _foodorderFoodDesignation = value;
            OnPropertyChanged(nameof(FoodorderFoodDesignationProp));
        }
    }
    
    public string FoodorderFoodContentsProp
    {
        get => _foodorderFoodContents;
        set
        {
            _foodorderFoodContents = value;
            OnPropertyChanged(nameof(FoodorderFoodContentsProp));
        }
    }
    
    public string FoodorderFoodAdditionalSelectionProp
    {
        get => _foodorderFoodAdditionalSelection;
        set
        {
            _foodorderFoodAdditionalSelection = value;
            OnPropertyChanged(nameof(FoodorderFoodAdditionalSelectionProp));
        }
    }
    
    public string FoodorderFoodPriceProp
    {
        get => _foodorderFoodPrice;
        set
        {
            _foodorderFoodPrice = value;
            OnPropertyChanged(nameof(FoodorderFoodPriceProp));
        }
    }
}