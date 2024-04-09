using Microsoft.EntityFrameworkCore;
using SmartOffice.Context;
using SmartOffice.Entities;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.Settings;

namespace SmartOffice.Services.FoodOrderServices.RestaurantServices;

public class RestaurantService : IRestaurantService
{
    private readonly SoDbContext _dbContext;
    private readonly IServiceProvider _service;

    public RestaurantService(SoDbContext dbContext, IServiceProvider service)
    {
        _dbContext = dbContext;
        _service = service;
    }

    public async Task<List<SoRestTab>> ReadAllRestaurants()
    {
        return await _dbContext.SoRestTabs.ToListAsync();
    }

    public async Task<List<IdentSchluessel>> ReadAllRestaurantsById()
    {
        var rests = await _dbContext.SoRestTabs.ToListAsync();

        List<IdentSchluessel> identschluessel = new List<IdentSchluessel>();
        foreach (var rest in rests)
        {
            identschluessel.Add(new IdentSchluessel
            {
                Key = rest.RestId,
                Bezeichnung = rest.RestName
            });
        }

        return identschluessel;
    }

    private List<RestaurantDataGridModel> DbToScreen(List<SoRestTab> rests)
    {
        var restaurantDataGrid = new List<RestaurantDataGridModel>();

        foreach (var rest in rests)
            restaurantDataGrid.Add(new RestaurantDataGridModel
            {
                RestaurantId = rest.RestId,
                RestaurantName = rest.RestName,
                RestaurantStreet = rest.RestStreet,
                RestaurantZipcode = rest.RestZipcode,
                RestaurantCity = rest.RestCity,
                RestaurantType = rest.RestType,
                RestaurantDelivery = rest.RestDelivery,
            });
        return restaurantDataGrid;
    }
    
    public async Task<List<RestaurantDataGridModel>> ReadAllRestaurantsForGrid()
    {
        return DbToScreen(await _dbContext.SoRestTabs.ToListAsync());
    }
    
    public async Task<RestaurantModel> ReadRestaurantById(string restaurantId)
    {
        var rests = await _dbContext.SoRestTabs.FindAsync(restaurantId) ?? new SoRestTab();

        var restModel = new RestaurantModel
        {
            FoodorderRestaurantIdProp = rests.RestId,
            FoodorderRestaurantNameProp = rests.RestName,
            FoodorderRestaurantStreetProp = rests.RestStreet,
            FoodorderRestaurantZipcodeProp = rests.RestZipcode,
            FoodorderRestaurantCityProp = rests.RestCity,
            FoodorderRestaurantTypeProp = rests.RestType,
            FoodorderRestaurantPhonenumberProp = rests.RestPhonenumber, 
            _foodorderRestaurantDeliveryYesNo = rests.RestDelivery,
            FoodorderRestaurantDeliveryTimeProp = rests.RestDeliveryTime,
            _foodorderRestaurantOrdertypeAppTelephone = rests.RestAppTelephone,
            FoodorderRestaurantMinimalOrderValueProp = rests.RestMinOrderValue,
            FoodorderRestaurantDeliveryCostProp = rests.RestDeliveryCost,
            FoodorderRestaurantLieferandoLinkProp = rests.RestLieferandoLink
            
        };
        return restModel;
    }

    public async Task SaveRestaurant(SoRestTab rest)
    {
        var existingRest = await _dbContext.SoRestTabs.FindAsync(rest.RestId);

        if (existingRest != null)
            _dbContext.Entry(existingRest).CurrentValues.SetValues(rest);
        else
            await _dbContext.SoRestTabs.AddAsync(rest);

        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveRestaurant(RestaurantModel restmodel)
    {
        var modelrest = new SoRestTab
        {
            RestId = restmodel.FoodorderRestaurantIdProp,
            RestName = restmodel.FoodorderRestaurantNameProp,
            RestStreet = restmodel.FoodorderRestaurantStreetProp,
            RestZipcode = restmodel.FoodorderRestaurantZipcodeProp,
            RestCity = restmodel.FoodorderRestaurantCityProp,
            RestType = restmodel.FoodorderRestaurantTypeProp,
            RestPhonenumber = restmodel.FoodorderRestaurantPhonenumberProp,
            RestDelivery = restmodel._foodorderRestaurantDeliveryYesNo,
            RestDeliveryTime = restmodel.FoodorderRestaurantDeliveryTimeProp,
            RestAppTelephone = restmodel._foodorderRestaurantOrdertypeAppTelephone,
            RestMinOrderValue = restmodel.FoodorderRestaurantMinimalOrderValueProp,
            RestDeliveryCost = restmodel.FoodorderRestaurantDeliveryCostProp,
            RestLieferandoLink = restmodel.FoodorderRestaurantLieferandoLinkProp
        };
        await SaveRestaurant(modelrest);
    }

    public async Task DeleteRestaurantById(string restaurantId)
    {
        await _dbContext.SoRestTabs.Where(rest => rest.RestId == restaurantId).ExecuteDeleteAsync();
    }
}