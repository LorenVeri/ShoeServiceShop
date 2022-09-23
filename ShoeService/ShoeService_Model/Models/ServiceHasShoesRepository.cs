using System.ComponentModel.DataAnnotations;

namespace ShoeService_Model.Models
{
    public class ServiceHasShoesRepository
    {
        [Key]
        public Guid ServiceId { get; set; }

        [Key]
        public Guid ShoesRepositoryId { get; set; }

        public ICollection<Shoes> Shoes { get; set; }
        public ICollection<ShoesRepository> ShoesRepositories { get; set; }
    }
}
