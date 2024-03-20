using ReactiveUI;

namespace SmartOffice.Models.MenuModels;

public class MenuDataGridModel : ReactiveObject
{
    private string _foodNumber = "";
    private string _foodCategory = "";
    private string _foodDesignation = "";
    private string _foodContents = "";
    private string _foodPrice = "";
    
    public string FoodNumber
    {
        get => _foodNumber;
        set => this.RaiseAndSetIfChanged(ref _foodNumber, value);
    }
    
    public string FoodCategory
    {
        get => _foodCategory;
        set => this.RaiseAndSetIfChanged(ref _foodCategory, value);
    }
    
    public string FoodDesignation
    {
        get => _foodDesignation;
        set => this.RaiseAndSetIfChanged(ref _foodDesignation, value);
    }
    
    public string FoodContents
    {
        get => _foodContents;
        set => this.RaiseAndSetIfChanged(ref _foodContents, value);
    }
    
    public string FoodPrice
    {
        get => _foodPrice;
        set => this.RaiseAndSetIfChanged(ref _foodPrice, value);
    }
}