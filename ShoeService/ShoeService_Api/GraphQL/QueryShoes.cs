using ShoeService_Data;
using ShoeService_Data.Repository;
using ShoeService_Model.Models;

namespace ShoeService_Api.GraphQL
{
    [ExtendObjectType("Query")]
    public class QueryShoes
    {
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 100)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Shoes> GetShoes(ShoeServiceDbContext context)
        {
            return context.Shoes.Where(x => x.IsDeleted == false);
        }
    }
}
