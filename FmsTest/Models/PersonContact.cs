using System.ComponentModel.DataAnnotations;

namespace FmsTest.Models
{
    public class PersonContact
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string[] TextComments { get; set; }

        public string JobPosition { get; set; }

        public Address? Address { get; set; }

        [Required]
        public long? CompanyId { get; set; }

        public CompanyContact Company { get; set; }

        public long? GroupId { get; set; }

        public UsersGroupContact? Group { get; set; }
    }
}
