using ShoeService_Data;
using ShoeService_Model.Models;

namespace ShoeService_Api.GraphQL
{
    [ExtendObjectType("Query")]
    public class QueryMemberShip
    {
        [UsePaging(IncludeTotalCount = true, MaxPageSize = 100)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<MemberShip> GetMemberShip(ShoeServiceDbContext context)
        {
            return context.MemberShips.Where(x => x.IsDeleted == false);
        }
    }
}
