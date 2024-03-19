using Microsoft.EntityFrameworkCore;
using SmartOffice.Context;
using SmartOffice.Entities;
using SmartOffice.Models.RestaurantModels;

namespace SmartOffice.Services.FoodOrderServices.RestaurantService;

public class RestaurantService : IRestaurantService
{
    private readonly SoDbContext _dbContext;
    private readonly IServiceProvider _service;

    public RestaurantService(SoDbContext dbContext, IServiceProvider service)
    {
        _dbContext = dbContext;
        _service = service;
    }

    public Task<List<Soresttab>> ReadALlRestaurants()
    {
        return _dbContext.Soresttabs.ToListAsync();
    }

    private List<RestaurantDataGridModel> DbToScreen(List<Soresttab> rests)
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
        return DbToScreen(await _dbContext.Soresttabs.ToListAsync());
    }
    
    public async Task<RestaurantViewModel> ReadRestaurantById(string restaurantId)
    {
        var rests = await _dbContext.Soresttabs.FindAsync(restaurantId) ?? new Soresttab();

        var restModel = new RestaurantViewModel
        {
            FoodorderRestaurantIdProp = rests.RestId,
            FoodorderRestaurantNameProp = rests.RestName,
            FoodorderRestaurantStreetProp = rests.RestStreet,
            FoodorderRestaurantZipcodeProp = rests.RestZipcode,
            FoodorderRestaurantCityProp = rests.RestCity,
            FoodorderRestaurantTypeProp = rests.RestType,
            FoodorderRestaurantPhonenumberProp = rests.RestPhonenumber,
            _foodorderRestaurantDeliveryYesNo = rests.RestDelivery,
            _foodorderRestaurantOrdertypeAppTelephone = rests.RestAppTelephone,
            FoodorderRestaurantMinimalOrderValueProp = rests.RestMinOrderValue,
            FoodorderRestaurantLieferandoLinkProp = rests.RestLieferandoLink
            
        };
        return restModel;
    }

    public async Task SaveRestaurant(Soresttab rest)
    {
        var existingRest = await _dbContext.Soresttabs.FindAsync(rest.RestId);

        if (existingRest != null)
            _dbContext.Entry(existingRest).CurrentValues.SetValues(rest);
        else
            await _dbContext.Soresttabs.AddAsync(rest);

        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveRestaurant(RestaurantViewModel restmodel)
    {
        var modelrest = new Soresttab
        {
            RestId = restmodel.FoodorderRestaurantIdProp,
            RestName = restmodel.FoodorderRestaurantNameProp,
            RestStreet = restmodel.FoodorderRestaurantStreetProp,
            RestZipcode = restmodel.FoodorderRestaurantZipcodeProp,
            RestCity = restmodel.FoodorderRestaurantCityProp,
            RestType = restmodel.FoodorderRestaurantTypeProp,
            RestPhonenumber = restmodel.FoodorderRestaurantPhonenumberProp,
            RestDelivery = restmodel._foodorderRestaurantDeliveryYesNo,
            RestAppTelephone = restmodel._foodorderRestaurantOrdertypeAppTelephone,
            RestMinOrderValue = restmodel.FoodorderRestaurantMinimalOrderValueProp,
            RestLieferandoLink = restmodel.FoodorderRestaurantLieferandoLinkProp
        };
        await SaveRestaurant(modelrest);
    }

    public async Task DeleteRestaurantById(string restaurantId)
    {
        await _dbContext.Soresttabs.Where(rest => rest.RestId == restaurantId).ExecuteDeleteAsync();
    }
}