using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Supermarket.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        public override DateTimeOffset? LockoutEnd { get; set; }
        [JsonIgnore]
        public override bool TwoFactorEnabled { get; set; }
        [JsonIgnore]
        public override bool PhoneNumberConfirmed { get; set; }
        public override string PhoneNumber { get; set; }
        [JsonIgnore]
        public override string ConcurrencyStamp { get; set; }
        [JsonIgnore]
        public override string SecurityStamp { get; set; }
        [JsonIgnore]
        public override string PasswordHash { get; set; }
        [JsonIgnore]
        public override bool EmailConfirmed { get; set; }
        [JsonIgnore]
        public override string NormalizedEmail { get; set; }
        public override string Email { get; set; }
        [JsonIgnore]
        public override string NormalizedUserName { get; set; }
        [Key]
        public override string UserName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        [JsonIgnore]
        public override String Id { get; set; }
        [JsonIgnore]
        public override bool LockoutEnabled { get; set; }
        [JsonIgnore]
        public override int AccessFailedCount { get; set; }
        public virtual string AadharNumber { get; set; }
        public virtual string Address { get; set; }
        [JsonIgnore]
        public virtual int UserId { get; set; }
    }
}
