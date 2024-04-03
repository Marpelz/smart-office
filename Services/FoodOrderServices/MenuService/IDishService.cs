using SmartOffice.Entities;
using SmartOffice.Models.MenuModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.Settings;

namespace SmartOffice.Services.FoodOrderServices.MenuService;

public interface IDishService
{
    // Read
    Task<List<Sodishtab>> ReadAllDishes();
    Task<List<DishDataGridModel>> ReadAllDishesForGridById(string restaurantId);
    Task<DishViewModel> ReadDishById(string restaurantId, string foodNumber);
    
    // Save
    Task SaveDish(Sodishtab menu);
    Task SaveDish(DishViewModel menumodel);
    
    // Delete
    Task DeleteDishById(string restaurantId, string foodNumber);
}