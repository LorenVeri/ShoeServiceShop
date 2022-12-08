using ShoeService_Model.Models;

namespace ShoeService_Model.Dtos
{
    public class ShoesDto : EntityBase
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Models { get; set; }

        public string? Brand { get; set; }

        public string? Material { get; set; }
        public int CustomerId { get; set; }
    }
}
