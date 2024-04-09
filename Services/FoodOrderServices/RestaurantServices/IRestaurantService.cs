using SmartOffice.Entities;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.Settings;

namespace SmartOffice.Services.FoodOrderServices.RestaurantServices;

public interface IRestaurantService
{
    // Read
    Task<List<SoRestTab>> ReadAllRestaurants();
    Task<List<IdentSchluessel>> ReadAllRestaurantsById();
    Task<List<RestaurantDataGridModel>> ReadAllRestaurantsForGrid();
    Task<RestaurantModel> ReadRestaurantById(string restaurantId);
    
    // Save
    Task SaveRestaurant(SoRestTab rest);
    Task SaveRestaurant(RestaurantModel restmodel);
    
    // Delete
    Task DeleteRestaurantById(string restaurantId);
}