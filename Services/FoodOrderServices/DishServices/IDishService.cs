using SmartOffice.Entities;
using SmartOffice.Models.DishModels;

namespace SmartOffice.Services.FoodOrderServices.DishServices;

public interface IDishService
{
    // Read
    Task<List<SoDishTab>> ReadAllDishes();
    Task<List<DishDataGridModel>> ReadAllDishesForGridById(string restaurantId);
    Task<DishModel> ReadDishById(string restaurantId, string foodNumber);
    Task<string> ReadDishDesignationById(string dishId);
    Task<string> ReadDishPriceById(string dishId);
    
    // Save
    Task SaveDish(SoDishTab menu);
    Task SaveDish(DishModel menumodel);
    
    // Delete
    Task DeleteDishById(string restaurantId, string foodNumber);
}