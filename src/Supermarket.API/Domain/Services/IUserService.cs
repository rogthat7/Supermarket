using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services.Communication;
using Supermarket.API.Models;

namespace Supermarket.API.Domain.Services
{
    public interface IUserService
    {
         Task<IEnumerable<ApplicationUser>> ListAsync();
         Task<ApplicationUser> SaveAsync(ApplicationUser user);
         Task<ApplicationUser> UpdateAsync(int Userid, ApplicationUser category);
         Task<ApplicationUser> DeleteAsync(int Userid);
    }
}