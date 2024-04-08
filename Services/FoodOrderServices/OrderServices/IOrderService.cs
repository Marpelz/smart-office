using SmartOffice.Entities;
using SmartOffice.Models.OrderModels;

namespace SmartOffice.Services.FoodOrderServices.OrderServices;

public interface IOrderService
{
    // Read
    Task<List<Soordertab>> ReadAllOrders();
    Task<List<Soorderdetailstab>> ReadAllOrderDetails();
    Task<List<Soorderdetailstab>> ReadOrderDetailsByOrderId(string orderId);
    Task<List<Soorderdetailstab>> ReadOrderDetailsByOrderIdAndUserId(string orderId, int userId);
    Task<OrderModel> ReadOrderById(string orderId);
    Task<OrderDetailsModel> ReadOrderDetailsById(string orderDetailId);

    // Save
    Task SaveOrder(Soordertab order);
    Task SaveOrder(OrderModel ordermodel);
    Task SaveOrderDetails(Soorderdetailstab details);
    Task SaveOrderDetails(OrderDetailsModel detailsmodel);

    // Delete
    Task DeleteOrderById(string orderId);
    Task DeleteOrderDetailsById(string orderDetailsId);
}