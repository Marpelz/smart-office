using Microsoft.EntityFrameworkCore;
using SmartOffice.Context;
using SmartOffice.Entities;
using SmartOffice.Models.DishModels;

namespace SmartOffice.Services.FoodOrderServices.MenuService;

public class DishService : IDishService
{
    private readonly SoDbContext _dbContext;
    private readonly IServiceProvider _service;
    
    public DishService(SoDbContext dbContext, IServiceProvider service)
    {
        _dbContext = dbContext;
        _service = service;
    }

    public Task<List<Sodishtab>> ReadAllDishes()
    {
        return _dbContext.Sodishtabs.ToListAsync();
    }

    private List<DishDataGridModel> DbToScreen(List<Sodishtab> dishes)
    {
        var dishDataGrid = new List<DishDataGridModel>();

        foreach (var dish in dishes)
            dishDataGrid.Add(new DishDataGridModel
            {
                DishId = dish.DishId,
                DishRestaurantId = dish.DishRestId,
                DishNumber = dish.DishNumber,
                DishCategory = dish.DishCategory,
                DishDesignation = dish.DishDesignation,
                DishContents = dish.DishContents,
                DishAdditionalSelection = dish.DishAdditionalSelection,
                DishPrice = dish.DishPrice
            });
        return dishDataGrid;
    }

    public async Task<List<DishDataGridModel>> ReadAllDishesForGridById(string restaurantId)
    {
        return DbToScreen(await _dbContext.Sodishtabs.Where(dish => dish.DishRestId == restaurantId).ToListAsync());
    }
    
    public async Task<DishModel> ReadDishById(string restaurantId, string foodNumber)
    {
        var dishes = await _dbContext.Sodishtabs.Where(dish => dish.DishId == restaurantId + foodNumber).FirstOrDefaultAsync() ?? new Sodishtab();

        var dishModel = new DishModel
        {
            FoodorderDishIdProp = dishes.DishId,
            FoodorderDishRestaurantIdProp = dishes.DishRestId,
            FoodorderDishNumberProp = dishes.DishNumber,
            FoodorderDishCategoryProp = dishes.DishCategory,
            FoodorderDishDesignationProp = dishes.DishDesignation,
            FoodorderDishContentsProp = dishes.DishContents,
            FoodorderDishAdditionalSelectionProp = dishes.DishAdditionalSelection,
            FoodorderDishPriceProp = dishes.DishPrice
        };
        return dishModel;
    }

    public async Task<string> ReadDishDesignationById(string dishId)
    {
        var designation = await _dbContext.Sodishtabs.FindAsync(dishId);
        return designation?.DishDesignation ?? throw new InvalidOperationException("Speisebezeichnung nicht gefunden");
    }

    public async Task<string> ReadDishPriceById(string dishId)
    {
        var price = await _dbContext.Sodishtabs.FindAsync(dishId);
        return price?.DishPrice ?? throw new InvalidOperationException("Speisepreis nicht gefunden");
    }

    public async Task SaveDish(Sodishtab dish)
    {
        var existingDish = await _dbContext.Sodishtabs.FindAsync(dish.DishId);

        if (existingDish != null)
            _dbContext.Entry(existingDish).CurrentValues.SetValues(dish);
        else
            await _dbContext.Sodishtabs.AddAsync(dish);

        await _dbContext.SaveChangesAsync();
    }
    
    public async Task SaveDish(DishModel menumodel)
    {
        var menuId = menumodel.FoodorderDishRestaurantIdProp + menumodel.FoodorderDishNumberProp;
        
        var modelmenu = new Sodishtab
        {
            DishId = menuId,
            DishRestId = menumodel.FoodorderDishRestaurantIdProp,
            DishNumber = menumodel.FoodorderDishNumberProp,
            DishCategory = menumodel.FoodorderDishCategoryProp,
            DishDesignation = menumodel.FoodorderDishDesignationProp,
            DishContents = menumodel.FoodorderDishContentsProp,
            DishAdditionalSelection = menumodel.FoodorderDishAdditionalSelectionProp,
            DishPrice = menumodel.FoodorderDishPriceProp
            
        };
        await SaveDish(modelmenu);
    }
    
    public async Task DeleteDishById(string restaurantId, string foodNumber)
    {
        await _dbContext.Sodishtabs.Where(dish => dish.DishId == restaurantId + foodNumber).ExecuteDeleteAsync();
    }
}