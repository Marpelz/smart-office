using Microsoft.EntityFrameworkCore;
using SmartOffice.Context;
using SmartOffice.Entities;
using SmartOffice.Models.OrderModels;

namespace SmartOffice.Services.FoodOrderServices.OrderServices;

public class OrderService : IOrderService
{
    private readonly SoDbContext _dbContext;
    private readonly IServiceProvider _service;

    public OrderService(SoDbContext dbContext, IServiceProvider service)
    {
        _dbContext = dbContext;
        _service = service;
    }

    public async Task<List<SoOrderTab>> ReadAllOrders()
    {
        return await _dbContext.SoOrderTabs.ToListAsync();
    }

    public async Task<List<SoOrderDetailsTab>> ReadAllOrderDetails()
    {
        return await _dbContext.SoOrderDetailsTabs.ToListAsync();
    }

    public async Task<List<SoOrderDetailsTab>> ReadOrderDetailsByOrderId(string orderId)
    {
        return await _dbContext.SoOrderDetailsTabs
            .Where(od => od.OrderId == orderId)
            .ToListAsync();
    }

    public async Task<List<SoOrderDetailsTab>> ReadOrderDetailsByOrderIdAndUserId(string orderId, int userId)
    {
        return await _dbContext.SoOrderDetailsTabs
            .Where(od => od.OrderId == orderId && od.UserId == userId)
            .ToListAsync();
    }

    public async Task<OrderModel> ReadOrderById(string orderId)
    {
        var orders = await _dbContext.SoOrderTabs.FindAsync(orderId) ?? new SoOrderTab();

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
        var details = await _dbContext.SoOrderDetailsTabs.FindAsync(orderDetailId) ?? new SoOrderDetailsTab();

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

    public async Task SaveOrder(SoOrderTab order)
    {
        var existingOrder = await _dbContext.SoOrderTabs.FindAsync(order.OrderId);

        if (existingOrder != null)
            _dbContext.Entry(existingOrder).CurrentValues.SetValues(order);
        else
            await _dbContext.SoOrderTabs.AddAsync(order);

        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveOrder(OrderModel ordermodel)
    {
        var modelorder = new SoOrderTab
        {
            OrderId = ordermodel.FoodorderOrderIdProp,
            RestaurantId = ordermodel.FoodorderRestaurantIdProp,
            UserId = ordermodel.FoodorderUserIdProp,
            OrderDate = ordermodel.FoodorderOrderDateProp
        };
        await SaveOrder(modelorder);
    }

    public async Task SaveOrderDetails(SoOrderDetailsTab details)
    {
        var existingOrderDetails = await _dbContext.SoOrderDetailsTabs.FindAsync(details.OrderDetailsId);

        if (existingOrderDetails != null)
            _dbContext.Entry(existingOrderDetails).CurrentValues.SetValues(details);
        else
            await _dbContext.SoOrderDetailsTabs.AddAsync(details);

        await _dbContext.SaveChangesAsync();
    }

    public async Task SaveOrderDetails(OrderDetailsModel detailsmodel)
    {
        var modeldetails = new SoOrderDetailsTab
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
        await _dbContext.SoOrderTabs.Where(order => order.OrderId == orderId).ExecuteDeleteAsync();
    }

    public async Task DeleteOrderDetailsById(string orderDetailsId)
    {
        await _dbContext.SoOrderDetailsTabs.Where(detail => detail.OrderDetailsId == orderDetailsId)
            .ExecuteDeleteAsync();
    }
}