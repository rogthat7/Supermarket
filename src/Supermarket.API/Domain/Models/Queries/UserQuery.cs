namespace Supermarket.API.Domain.Models.Queries
{
    public class UserQuery : Query
    {
        public int? UserId { get; set; }

        public UserQuery(int? id, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            UserId = id;
        }
    }
}