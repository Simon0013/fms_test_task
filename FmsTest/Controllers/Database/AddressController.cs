using FmsTest.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FmsTest.Controllers.Database
{
    public class AddressController : Controller
    {
        private ApplicationContext _db;

        public AddressController(ApplicationContext db)
        {
            _db = db;
        }

        private string ValidateAddress(Address address)
        {
            if (address == null)
            {
                return "Request body is empty";
            }
            if (string.IsNullOrWhiteSpace(address.Country))
                address.Country = "РФ";
            if (string.IsNullOrWhiteSpace(address.Region))
            {
                return "Region is required.";
            }
            if (string.IsNullOrWhiteSpace(address.City))
            {
                return "City is required.";
            }
            if (address.PostalCode == null)
            {
                return "Postal code is required.";
            }
            return string.Empty;
        }

        [HttpPost]
        public IActionResult CreateAddress(Address address)
        {
            var validateMsg = ValidateAddress(address);
            if (!string.IsNullOrEmpty(validateMsg))
            {
                return BadRequest(validateMsg);
            }
            try
            {
                _db.Address.Add(address);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult EditAddress(Address address)
        {
            var validateMsg = ValidateAddress(address);
            if (!string.IsNullOrEmpty(validateMsg))
            {
                return BadRequest(validateMsg);
            }
            try
            {
                _db.Address.Update(address);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok();
        }

        public IActionResult DeleteAddress(long id)
        {
            try
            {
                var address = _db.Address.Find(id);
                _db.Address.Remove(address);
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
