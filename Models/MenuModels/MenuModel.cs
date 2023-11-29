using System.ComponentModel.DataAnnotations.Schema;
using SmartOffice.Models.RestaurantModels;

namespace SmartOffice.Models.MenuModels;

public class MenuModel
{
    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }
    public virtual RestaurantModel Restaurant { get; set; }
    
    public int MenuOrderNumber { get; set; }
    public string MenuDishName { get; set; }
    public string MenuDishIngredients { get; set; }
    public decimal MenuDishPrice { get; set; }
}