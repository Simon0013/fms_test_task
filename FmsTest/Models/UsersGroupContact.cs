using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FmsTest.Models
{
    public class UsersGroupContact
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string[] TextComments { get; set; }

        [Required]
        public DateTime? Created {  get; set; }

        public List<PersonContact> Persons { get; set; }

        [Required]
        public long? CompanyId { get; set; }

        public CompanyContact Company { get; set; }
    }
}
