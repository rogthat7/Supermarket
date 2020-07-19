using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Supermarket.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int AadharNumber { get; set; }
        public string Address { get; set; }
    }
}
