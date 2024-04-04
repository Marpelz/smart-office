using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SmartOffice.Models.OrderModels;

namespace SmartOffice.Models.UserModels
{
    public class UserModel
    {
        public List<OrderModel> Orders { get; set; }
        public List<OrderDetailsModel> OrderDetails { get; set; }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
        public bool ActivePaypal { get; set; }
        public string? PaypalEmail { get; set; }
    }
}
