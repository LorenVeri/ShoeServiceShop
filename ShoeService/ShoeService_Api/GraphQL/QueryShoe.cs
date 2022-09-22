using ShoeService_Data;
using ShoeService_Data.Repository;
using ShoeService_Model.Models;

namespace ShoeService_Api.GraphQL
{
    [ExtendObjectType("Query")]
    public class QueryShoe
    {
        private readonly IShoeRepository _shoeRepository;

        public QueryShoe(IShoeRepository shoeRepository)
        {
            _shoeRepository = shoeRepository;
        }

        [UsePaging(IncludeTotalCount = true, MaxPageSize = 100)]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Shoe> GetShoe()
        {
            return _shoeRepository.GetAll();
        }
    }
}
