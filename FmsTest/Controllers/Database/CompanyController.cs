using FmsTest.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FmsTest.Controllers.Database
{
    public class CompanyController : Controller
    {
        private ApplicationContext _db;

        public CompanyController(ApplicationContext db)
        {
            _db = db;
        }

        private string ValidateCompanyContact(CompanyContact companyContact)
        {
            if (companyContact == null)
            {
                return "Request body is empty";
            }
            if (string.IsNullOrWhiteSpace(companyContact.Name))
            {
                return "Company\'s name is required.";
            }
            if (string.IsNullOrWhiteSpace(companyContact.PhoneNumber))
            {
                return "Company\'s phone number is required.";
            }
            return string.Empty;
        }

        [HttpPost]
        public IActionResult CreateCompanyContact(CompanyContact companyContact)
        {
            var validateMsg = ValidateCompanyContact(companyContact);
            if (!string.IsNullOrEmpty(validateMsg))
            {
                return BadRequest(validateMsg);
            }
            try
            {
                _db.CompanyContacts.Add(companyContact);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult EditCompanyContact(CompanyContact companyContact)
        {
            var validateMsg = ValidateCompanyContact(companyContact);
            if (!string.IsNullOrEmpty(validateMsg))
            {
                return BadRequest(validateMsg);
            }
            try
            {
                _db.CompanyContacts.Update(companyContact);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        public IActionResult DeleteCompanyContact(long id)
        {
            try
            {
                var contact = _db.CompanyContacts.Find(id);
                _db.CompanyContacts.Remove(contact);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        public IActionResult AddPerson(long companyId, long personId)
        {
            try
            {
                var company = _db.CompanyContacts.Find(companyId);
                foreach (var per in company.Persons)
                {
                    if (per.Id == personId)
                        return Ok();
                }
                var person = _db.PersonContacts.Find(personId);
                person.Company = company;
                _db.PersonContacts.Update(person);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        public IActionResult AddUsersGroup(long companyId, long groupId)
        {
            try
            {
                var company = _db.CompanyContacts.Find(companyId);
                foreach (var gr in company.UsersGroups)
                {
                    if (gr.Id == groupId)
                        return Ok();
                }
                var group = _db.UsersGroups.Find(groupId);
                group.Company = company;
                _db.UsersGroups.Update(group);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        public IActionResult AddAddress(long addressId, long companyId)
        {
            try
            {
                var company = _db.CompanyContacts.Find(companyId);
                if (company.MainOfficeAddress != null)
                {
                    if (company.MainOfficeAddress.Id == addressId)
                        return Ok();
                }
                var address = _db.Address.Find(addressId);
                company.MainOfficeAddress = address;
                _db.CompanyContacts.Update(company);
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
