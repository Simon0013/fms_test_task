using FmsTest.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FmsTest.Controllers.Database
{
    public class UserController : Controller
    {
        private ApplicationContext _db;

        public UserController(ApplicationContext db)
        {
            _db = db;
        }

        private string ValidatePersonContact(PersonContact personContact)
        {
            if (personContact == null)
            {
                return "Request body is empty";
            }
            if (string.IsNullOrWhiteSpace(personContact.Name))
            {
                return "Person\'s name is required.";
            }
            if (string.IsNullOrWhiteSpace(personContact.PhoneNumber))
            {
                return "Person\'s phone number is required.";
            }
            if (string.IsNullOrWhiteSpace(personContact.JobPosition))
            {
                return "Person\'s job position is required.";
            }
            if (personContact.CompanyId is null)
            {
                return "Person\'s company is required.";
            }
            return string.Empty;
        }

        [HttpPost]
        public IActionResult CreatePersonContact(PersonContact personContact)
        {
            var validateMsg = ValidatePersonContact(personContact);
            if (!string.IsNullOrEmpty(validateMsg))
            {
                return BadRequest(validateMsg);
            }
            try
            {
                _db.PersonContacts.Add(personContact);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult EditPersonContact(PersonContact personContact)
        {
            var validateMsg = ValidatePersonContact(personContact);
            if (!string.IsNullOrEmpty(validateMsg))
            {
                return BadRequest(validateMsg);
            }
            try
            {
                _db.PersonContacts.Update(personContact);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        public IActionResult DeletePersonContact(long id)
        {
            try
            {
                var contact = _db.PersonContacts.Find(id);
                _db.PersonContacts.Remove(contact);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        public IActionResult AddAddress(long addressId, long personId)
        {
            try
            {
                var person = _db.PersonContacts.Find(personId);
                if (person.Address != null)
                {
                    if (person.Address.Id == addressId)
                        return Ok();
                }
                var address = _db.Address.Find(addressId);
                person.Address = address;
                _db.PersonContacts.Update(person);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }
    }
}
