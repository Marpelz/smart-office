using ReactiveUI;

namespace SmartOffice.Models.MenuModels;

public class DishDataGridModel : ReactiveObject
{
    private string _dishRestaurantId = "";
    private string _dishNumber = "";
    private string _dishCategory = "";
    private string _dishDesignation = "";
    private string _dishContents = "";
    private string _dishAdditionalSelection = "";
    private string _dishPrice = "";
    
    public string DishRestaurantId
    {
        get => _dishRestaurantId;
        set => this.RaiseAndSetIfChanged(ref _dishRestaurantId, value);
    }
    
    public string DishNumber
    {
        get => _dishNumber;
        set => this.RaiseAndSetIfChanged(ref _dishNumber, value);
    }
    
    public string DishCategory
    {
        get => _dishCategory;
        set => this.RaiseAndSetIfChanged(ref _dishCategory, value);
    }
    
    public string DishDesignation
    {
        get => _dishDesignation;
        set => this.RaiseAndSetIfChanged(ref _dishDesignation, value);
    }
    
    public string DishContents
    {
        get => _dishContents;
        set => this.RaiseAndSetIfChanged(ref _dishContents, value);
    }
    
    public string DishAdditionalSelection
    {
        get => _dishAdditionalSelection;
        set => this.RaiseAndSetIfChanged(ref _dishAdditionalSelection, value);
    }
    
    public string DishPrice
    {
        get => _dishPrice;
        set => this.RaiseAndSetIfChanged(ref _dishPrice, value);
    }
}