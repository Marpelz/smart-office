using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartOffice.Models.OrderModels;

namespace SmartOffice.Models.UserModels
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
        
        
        public List<UserPaymentMethodModel> UserPaymentMethods { get; set; }
        public List<OrderDetailsModel> OrderDetails { get; set; }
    }
}
