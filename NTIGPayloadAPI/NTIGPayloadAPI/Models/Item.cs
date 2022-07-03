using System.ComponentModel.DataAnnotations;

namespace NTIGPayloadAPI.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ItemCode { get; set; }   = string.Empty;

        public int Quantity { get; set; } = 0;
    }
}
