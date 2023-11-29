using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartOffice.Models.RestaurantModels;

public class RestaurantModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
    public string RestaurantStreet { get; set; }
    public string RestaurantHouseNumber { get; set; }
    public string RestaurantZipCode { get; set; }
    public string RestaurantCity { get; set; }
    public string RestaurantPhoneNumber { get; set; }
    public string RestaurantType { get; set; }
    public bool OrderViaAppOrMobile { get; set; }
    public bool DeliveryServiceOrTakeAway { get; set; }
}