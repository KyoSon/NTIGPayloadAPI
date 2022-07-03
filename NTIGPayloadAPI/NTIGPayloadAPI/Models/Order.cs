using System.ComponentModel.DataAnnotations;

namespace NTIGPayloadAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime RequestedPickupTime { get; set; } = DateTime.Now;

        [Required]
        public Address PickupAddress { get; set; }

        [Required]
        public Address DeliveryAddress { get; set; }

        [Required]
        public List<Item> Items { get; set; }
        
        [Required]
        [StringLength(100)]
        public string PickupInstructions { get; set; } = string.Empty;

        [StringLength(100)]
        public string DeliveryInstructions { get; set; } = string.Empty;

    }
}
