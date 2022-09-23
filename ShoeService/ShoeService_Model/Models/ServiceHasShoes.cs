using System.ComponentModel.DataAnnotations;

namespace ShoeService_Model.Models
{
    public class ServiceHasShoes
    {
        [Key]
        public int ServiceId { get; set; }

        [Key]
        public int ShoesId { get; set; }

        public ICollection<Service> Services { get; set; }
        public ICollection<Shoes> Shoes { get; set; }

    }
}
