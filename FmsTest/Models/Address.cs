using System.ComponentModel.DataAnnotations;

namespace FmsTest.Models
{
    public class Address
    {
        [Key]
        public long Id { get; set; }

        public string Country { get; set; } = "РФ";

        public string Region { get; set; }

        public string City { get; set; }

        public string? Street { get; set; }

        public int? House { get; set; }

        [Required]
        public int? PostalCode { get; set; }
    }
}
