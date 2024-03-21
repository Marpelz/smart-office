using Microsoft.EntityFrameworkCore;
using SmartOffice.Context;
using SmartOffice.Entities;
using SmartOffice.Models.MenuModels;

namespace SmartOffice.Services.FoodOrderServices.MenuService;

public class MenuService : IMenuService
{
    private readonly SoDbContext _dbContext;
    private readonly IServiceProvider _service;
    
    public MenuService(SoDbContext dbContext, IServiceProvider service)
    {
        _dbContext = dbContext;
        _service = service;
    }

    public Task<List<Somenutab>> ReadAllMenus()
    {
        return _dbContext.Somenutabs.ToListAsync();
    }

    private List<MenuDataGridModel> DbToScreen(List<Somenutab> menus)
    {
        var menuDataGrid = new List<MenuDataGridModel>();

        foreach (var menu in menus)
            menuDataGrid.Add(new MenuDataGridModel
            {
                FoodRestaurantId = menu.MenuRestId,
                FoodNumber = menu.MenuFoodNumber,
                FoodCategory = menu.MenuFoodCategory,
                FoodDesignation = menu.MenuFoodDesignation,
                FoodContents = menu.MenuFoodContents,
                FoodPrice = menu.MenuFoodPrice
            });
        return menuDataGrid;
    }

    public async Task<List<MenuDataGridModel>> ReadAllMenusForGridById(string restaurantId)
    {
        return DbToScreen(await _dbContext.Somenutabs.Where(menu => menu.MenuRestId == restaurantId).ToListAsync());
    }
    
    public async Task<MenuViewModel> ReadMenuById(string restaurantId, string foodNumber)
    {
        var menus = await _dbContext.Somenutabs.Where(menu => menu.MenuId == restaurantId + foodNumber).FirstOrDefaultAsync() ?? new Somenutab();

        var menuModel = new MenuViewModel
        {
            FoodorderFoodMenuIdProp = menus.MenuId,
            FoodorderFoodRestaurantIdProp = menus.MenuRestId,
            FoodorderFoodNumberProp = menus.MenuFoodNumber,
            FoodorderFoodCategoryProp = menus.MenuFoodCategory,
            FoodorderFoodDesignationProp = menus.MenuFoodDesignation,
            FoodorderFoodContentsProp = menus.MenuFoodContents,
            FoodorderFoodAdditionalSelectionProp = menus.MenuFoodAdditionalSelection,
            FoodorderFoodPriceProp = menus.MenuFoodPrice
        };
        return menuModel;
    }

    public async Task SaveMenu(Somenutab menu)
    {
        var existingMenu = await _dbContext.Somenutabs.FindAsync(menu.MenuId);

        if (existingMenu != null)
            _dbContext.Entry(existingMenu).CurrentValues.SetValues(menu);
        else
            await _dbContext.Somenutabs.AddAsync(menu);

        await _dbContext.SaveChangesAsync();
    }
    
    public async Task SaveMenu(MenuViewModel menumodel)
    {
        var menuId = menumodel.FoodorderFoodRestaurantIdProp + menumodel.FoodorderFoodNumberProp;
        
        var modelmenu = new Somenutab
        {
            MenuId = menuId,
            MenuRestId = menumodel.FoodorderFoodRestaurantIdProp,
            MenuFoodNumber = menumodel.FoodorderFoodNumberProp,
            MenuFoodCategory = menumodel.FoodorderFoodCategoryProp,
            MenuFoodDesignation = menumodel.FoodorderFoodDesignationProp,
            MenuFoodContents = menumodel.FoodorderFoodContentsProp,
            MenuFoodAdditionalSelection = menumodel.FoodorderFoodAdditionalSelectionProp,
            MenuFoodPrice = menumodel.FoodorderFoodPriceProp
            
        };
        await SaveMenu(modelmenu);
    }
    
    public async Task DeleteMenuById(string restaurantId, string foodNumber)
    {
        await _dbContext.Somenutabs.Where(menu => menu.MenuId == restaurantId + foodNumber).ExecuteDeleteAsync();
    }
}