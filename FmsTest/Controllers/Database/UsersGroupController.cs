using FmsTest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.Design;

namespace FmsTest.Controllers.Database
{
    public class UsersGroupController : Controller
    {
        private ApplicationContext _db;

        public UsersGroupController(ApplicationContext db)
        {
            _db = db;
        }

        private string ValidateUsersGroupContact(UsersGroupContact usersGroupContact)
        {
            if (usersGroupContact == null)
            {
                return "Request body is empty";
            }
            if (string.IsNullOrWhiteSpace(usersGroupContact.Name))
            {
                return "Group\'s name is required.";
            }
            if (string.IsNullOrWhiteSpace(usersGroupContact.PhoneNumber))
            {
                return "Group\'s phone number is required.";
            }
            return string.Empty;
        }

        [HttpPost]
        public IActionResult CreateUsersGroupContact(UsersGroupContact usersGroupContact)
        {
            var validateMsg = ValidateUsersGroupContact(usersGroupContact);
            if (!string.IsNullOrEmpty(validateMsg))
            {
                return BadRequest(validateMsg);
            }
            try
            {
                _db.UsersGroups.Add(usersGroupContact);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult EditUsersGroupContact(UsersGroupContact usersGroupContact)
        {
            var validateMsg = ValidateUsersGroupContact(usersGroupContact);
            if (!string.IsNullOrEmpty(validateMsg))
            {
                return BadRequest(validateMsg);
            }
            try
            {
                _db.UsersGroups.Update(usersGroupContact);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        public IActionResult DeleteUsersGroupContact(long id)
        {
            try
            {
                var contact = _db.UsersGroups.Find(id);
                _db.UsersGroups.Remove(contact);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        public IActionResult AddPerson(long groupId, long personId)
        {
            try
            {
                var group = _db.UsersGroups.Find(groupId);
                foreach (var per in group.Persons)
                {
                    if (per.Id == personId)
                        return Ok();
                }
                var person = _db.PersonContacts.Find(personId);
                person.Group = group;
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
