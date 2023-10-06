using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FmsTest.Models
{
    public class CompanyContact
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string[] TextComments { get; set; }

        public Address? MainOfficeAddress { get; set; }

        public List<PersonContact> Persons { get; set; }

        public List<UsersGroupContact> UsersGroups { get; set; }
    }
}
