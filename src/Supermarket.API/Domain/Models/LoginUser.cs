using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using Supermarket.API.Models;

namespace Supermarket.API.Domain.Models
{
    public class LoginUser : RegisteredUser
    {
        [Phone]
        public override string PhoneNumber { get; set; }
        [EmailAddress]
        public override string Email { get; set; }
        public override string UserName { get; set; }
        [JsonIgnore]
        public override string FirstName { get; set; }
        [JsonIgnore]
        public override string LastName { get; set; }
        [JsonIgnore]
        public override string AadharNumber { get; set; }
        [JsonIgnore]
        public override string Address { get; set; }
        [DataType(DataType.Password)]
        public override string Password { get; set; }

    }
}