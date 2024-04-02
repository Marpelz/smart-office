using SmartOffice.Entities;
using SmartOffice.Models.MenuModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.Settings;

namespace SmartOffice.Services.FoodOrderServices.MenuService;

public interface IDishService
{
    // Read
    Task<List<Sodishtab>> ReadAllMenus();
    Task<List<DishDataGridModel>> ReadAllMenusForGridById(string restaurantId);
    Task<DishViewModel> ReadMenuById(string restaurantId, string foodNumber);
    
    // Save
    Task SaveMenu(Sodishtab menu);
    Task SaveMenu(DishViewModel menumodel);
    
    // Delete
    Task DeleteMenuById(string restaurantId, string foodNumber);
}