using SmartOffice.Entities;
using SmartOffice.Models.DishModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.Settings;

namespace SmartOffice.Services.FoodOrderServices.MenuService;

public interface IDishService
{
    // Read
    Task<List<Sodishtab>> ReadAllDishes();
    Task<List<DishDataGridModel>> ReadAllDishesForGridById(string restaurantId);
    Task<DishModel> ReadDishById(string restaurantId, string foodNumber);
    Task<string> ReadDishDesignationById(string dishId);
    Task<string> ReadDishPriceById(string dishId);
    
    // Save
    Task SaveDish(Sodishtab menu);
    Task SaveDish(DishModel menumodel);
    
    // Delete
    Task DeleteDishById(string restaurantId, string foodNumber);
}