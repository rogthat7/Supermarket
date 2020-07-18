using Supermarket.API.Persistence.Contexts;

namespace Supermarket.API.Persistence.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly SupermarketDBContext _context;

        public BaseRepository(SupermarketDBContext context)
        {
            _context = context;
        }
    }
}