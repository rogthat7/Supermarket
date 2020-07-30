using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Supermarket.API.Models;

namespace Supermarket.API.Domain.Models
{
    public class RegisteredUser : ApplicationUser
    {
        public override string PhoneNumber { get; set; }
        [EmailAddress]
        public override string Email { get; set; }
        [JsonIgnore]
        public override string UserName { get; set; }
        public override string FirstName { get; set; }
        public override string LastName { get; set; }
        public override string AadharNumber { get; set; }
        public override string Address { get; set; }
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

    }
}