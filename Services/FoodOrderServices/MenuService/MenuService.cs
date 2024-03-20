using SmartOffice.Context;

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
}