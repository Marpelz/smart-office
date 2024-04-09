using SmartOffice.Entities;
using SmartOffice.Models.OrderModels;

namespace SmartOffice.Services.FoodOrderServices.OrderServices;

public interface IOrderService
{
    // Read
    Task<List<SoOrderTab>> ReadAllOrders();
    Task<List<SoOrderDetailsTab>> ReadAllOrderDetails();
    Task<List<SoOrderDetailsTab>> ReadOrderDetailsByOrderId(string orderId);
    Task<List<SoOrderDetailsTab>> ReadOrderDetailsByOrderIdAndUserId(string orderId, int userId);
    Task<OrderModel> ReadOrderById(string orderId);
    Task<OrderDetailsModel> ReadOrderDetailsById(string orderDetailId);

    // Save
    Task SaveOrder(SoOrderTab order);
    Task SaveOrder(OrderModel ordermodel);
    Task SaveOrderDetails(SoOrderDetailsTab details);
    Task SaveOrderDetails(OrderDetailsModel detailsmodel);

    // Delete
    Task DeleteOrderById(string orderId);
    Task DeleteOrderDetailsById(string orderDetailsId);
}