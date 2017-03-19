﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Models
{
	public class ApplicationUser : IdentityUser
    {
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Company { get; set; }

		public string City { get; set; }

		public string Address { get; set; }

		public string DeliveryAddress { get; set; }

		public string Phone { get; set; }

		public string InvoiceData { get; set; }

		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}