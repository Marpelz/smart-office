using Microsoft.EntityFrameworkCore;
using SmartOffice.Context;
using SmartOffice.Entities;
using SmartOffice.Models.MenuModels;

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

    public Task<List<Sodishtab>> ReadAllMenus()
    {
        return _dbContext.Sodishtabs.ToListAsync();
    }

    private List<DishDataGridModel> DbToScreen(List<Sodishtab> dishes)
    {
        var dishDataGrid = new List<DishDataGridModel>();

        foreach (var dish in dishes)
            dishDataGrid.Add(new DishDataGridModel
            {
                DishRestaurantId = dish.DishRestId,
                DishNumber = dish.DishNumber,
                DishCategory = dish.DishCategory,
                DishDesignation = dish.DishDesignation,
                DishContents = dish.DishContents,
                DishPrice = dish.DishPrice
            });
        return dishDataGrid;
    }

    public async Task<List<DishDataGridModel>> ReadAllMenusForGridById(string restaurantId)
    {
        return DbToScreen(await _dbContext.Sodishtabs.Where(dish => dish.DishRestId == restaurantId).ToListAsync());
    }
    
    public async Task<DishViewModel> ReadMenuById(string restaurantId, string foodNumber)
    {
        var dishes = await _dbContext.Sodishtabs.Where(dish => dish.DishId == restaurantId + foodNumber).FirstOrDefaultAsync() ?? new Sodishtab();

        var dishModel = new DishViewModel
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

    public async Task SaveMenu(Sodishtab dish)
    {
        var existingDish = await _dbContext.Sodishtabs.FindAsync(dish.DishId);

        if (existingDish != null)
            _dbContext.Entry(existingDish).CurrentValues.SetValues(dish);
        else
            await _dbContext.Sodishtabs.AddAsync(dish);

        await _dbContext.SaveChangesAsync();
    }
    
    public async Task SaveMenu(DishViewModel menumodel)
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
        await SaveMenu(modelmenu);
    }
    
    public async Task DeleteMenuById(string restaurantId, string foodNumber)
    {
        await _dbContext.Sodishtabs.Where(dish => dish.DishId == restaurantId + foodNumber).ExecuteDeleteAsync();
    }
}