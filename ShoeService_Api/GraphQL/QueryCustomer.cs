using ShoeService_Data;
using ShoeService_Data.Repository;
using ShoeService_Model.Models;

namespace ShoeService_Api.GraphQL
{
    [ExtendObjectType("Query")]
    public class QueryCustomer
    {
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 100)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Customer> GetCustomer(ShoeServiceDbContext context)
        {
            return context.Customers.Where(x => x.IsDeleted == false && x.IsActived == true);
        }
    }
}
