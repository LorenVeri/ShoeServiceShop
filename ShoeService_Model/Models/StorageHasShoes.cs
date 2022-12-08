using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeService_Model.Models
{
    [Table("StorageHasShoes")]
    public class StorageHasShoes
    {
        [Key]
        [Required]
        public int StorageId { get; set; }

        [Key]
        [Required]
        public int ShoesId { get; set; }

        public ICollection<Storage>? Storages { get; set; }

        public ICollection<Shoes>? Shoes { get; set; }

    }
}
