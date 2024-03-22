using SmartOffice.Entities;
using SmartOffice.Models.MenuModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.Settings;

namespace SmartOffice.Services.FoodOrderServices.MenuService;

public interface IMenuService
{
    // Read
    Task<List<Somenutab>> ReadAllMenus();
    Task<List<DishDataGridModel>> ReadAllMenusForGridById(string restaurantId);
    Task<MenuViewModel> ReadMenuById(string restaurantId, string foodNumber);
    
    // Save
    Task SaveMenu(Somenutab menu);
    Task SaveMenu(MenuViewModel menumodel);
    
    // Delete
    Task DeleteMenuById(string restaurantId, string foodNumber);
}