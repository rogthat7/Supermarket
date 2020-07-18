using System.Threading.Tasks;
using Supermarket.API.Domain.Repositories;
using Supermarket.API.Persistence.Contexts;

namespace Supermarket.API.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SupermarketDBContext _context;

        public UnitOfWork(SupermarketDBContext context)
        {
            _context = context;     
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}