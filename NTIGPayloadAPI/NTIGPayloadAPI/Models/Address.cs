using System.ComponentModel.DataAnnotations;

namespace NTIGPayloadAPI.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Unit { get; set; } = string.Empty;

        [Required]
        public string Street { get; set; } = string.Empty;
        [Required]
        public string Suburb { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public string Postcode { get; set; } = string.Empty;

    }
}
