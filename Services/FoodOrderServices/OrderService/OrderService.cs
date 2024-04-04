using Microsoft.EntityFrameworkCore;
using SmartOffice.Context;
using SmartOffice.Entities;
using SmartOffice.Models.OrderModels;
using SmartOffice.Models.RestaurantModels;

namespace SmartOffice.Services.FoodOrderServices.OrderService;

public class OrderService : IOrderService
{
    private readonly SoDbContext _dbContext;
    private readonly IServiceProvider _service;
    
    public OrderService(SoDbContext dbContext, IServiceProvider service)
    {
        _dbContext = dbContext;
        _service = service;
    }
    
    public async Task<List<Soordertab>> ReadAllOrders()
    {
        return await _dbContext.Soordertabs.ToListAsync();
    }
    
    public async Task<List<Soorderdetailstab>> ReadAllOrderDetails()
    {
        return await _dbContext.Soorderdetailstabs.ToListAsync();
    }
    
    public async Task<OrderModel> ReadOrderById(string orderId)
    {
        var orders = await _dbContext.Soordertabs.FindAsync(orderId) ?? new Soordertab();

        var orderModel = new OrderModel
        {
            FoodorderOrderIdProp = orders.OrderId,
            FoodorderRestaurantIdProp = orders.RestaurantId,
            FoodorderUserIdProp = orders.UserId,
            FoodorderOrderDateProp = orders.OrderDate
            
        };
        return orderModel;
    }
    
    public async Task<OrderDetailsModel> ReadOrderDetailsById(string orderDetailId)
    {
        var details = await _dbContext.Soorderdetailstabs.FindAsync(orderDetailId) ?? new Soorderdetailstab();

        var orderDetailModel = new OrderDetailsModel
        {
            FoodorderOrderDetailsIdProp = details.OrderDetailsId,
            FoodorderOrderIdProp = details.OrderId,
            FoodorderUserIdProp = details.UserId,
            FoodorderDishIdProp = details.DishId,
            FoodorderPaymentMethodProp = details.PaymentMethod
            
        };
        return orderDetailModel;
    }
    
    public async Task SaveOrder(Soordertab order)
    {
        var existingOrder = await _dbContext.Soordertabs.FindAsync(order.OrderId);

        if (existingOrder != null)
            _dbContext.Entry(existingOrder).CurrentValues.SetValues(order);
        else
            await _dbContext.Soordertabs.AddAsync(order);

        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveOrder(OrderModel ordermodel)
    {
        var modelorder = new Soordertab
        {
            OrderId = ordermodel.FoodorderOrderIdProp,
            RestaurantId = ordermodel.FoodorderRestaurantIdProp,
            UserId = ordermodel.FoodorderUserIdProp,
            OrderDate = ordermodel.FoodorderOrderDateProp
        };
        await SaveOrder(modelorder);
    }
    
    public async Task SaveOrderDetails(Soorderdetailstab details)
    {
        var existingOrderDetails = await _dbContext.Soorderdetailstabs.FindAsync(details.OrderDetailsId);

        if (existingOrderDetails != null)
            _dbContext.Entry(existingOrderDetails).CurrentValues.SetValues(details);
        else
            await _dbContext.Soorderdetailstabs.AddAsync(details);

        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveOrderDetails(OrderDetailsModel detailsmodel)
    {
        var modeldetails = new Soorderdetailstab
        {
            OrderDetailsId = detailsmodel.FoodorderOrderDetailsIdProp,
            OrderId = detailsmodel.FoodorderOrderIdProp,
            UserId = detailsmodel.FoodorderUserIdProp,
            DishId = detailsmodel.FoodorderDishIdProp,
            PaymentMethod = detailsmodel.FoodorderPaymentMethodProp
        };
        await SaveOrderDetails(modeldetails);
    }

    public async Task DeleteOrderById(string orderId)
    {
        await _dbContext.Soordertabs.Where(order => order.OrderId == orderId).ExecuteDeleteAsync();
    }
    
    public async Task DeleteOrderDetailsById(string orderDetailsId)
    {
        await _dbContext.Soorderdetailstabs.Where(detail => detail.OrderDetailsId == orderDetailsId).ExecuteDeleteAsync();
    }
}