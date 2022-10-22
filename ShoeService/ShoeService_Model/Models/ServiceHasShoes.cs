using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeService_Model.Models
{
    [Table("ServiceHasShoes")]
    public class ServiceHasShoes
    {
        [Key]
        [Required]
        public int ServiceId { get; set; }

        [Key]
        [Required]
        public int ShoesId { get; set; }

        public ICollection<Service>? Services { get; set; }
        public ICollection<Shoes>? Shoes { get; set; }

    }
}
