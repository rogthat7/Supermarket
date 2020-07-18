using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Models.Queries;

namespace Supermarket.API.Domain.Repositories
{
    public interface ISellerRepository
    {
        Task<QueryResult<Product>> ListAsync(SellersQuery query);
        Task AddAsync(Seller seller);
        Task<Seller> FindByIdAsync(int id);
        void Update(Seller seller);
        void Remove(Seller seller);
    }
}