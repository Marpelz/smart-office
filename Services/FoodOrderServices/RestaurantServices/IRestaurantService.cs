using SmartOffice.Entities;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.Settings;

namespace SmartOffice.Services.FoodOrderServices.RestaurantService;

public interface IRestaurantService
{
    // Read
    Task<List<Soresttab>> ReadAllRestaurants();
    Task<List<IdentSchluessel>> ReadAllRestaurantsById();
    Task<List<RestaurantDataGridModel>> ReadAllRestaurantsForGrid();
    Task<RestaurantModel> ReadRestaurantById(string restaurantId);
    
    // Save
    Task SaveRestaurant(Soresttab rest);
    Task SaveRestaurant(RestaurantModel restmodel);
    
    // Delete
    Task DeleteRestaurantById(string restaurantId);
}