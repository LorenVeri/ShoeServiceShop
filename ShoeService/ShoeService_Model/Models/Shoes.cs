using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    [Table("Shoes")]
    public class Shoes : EntityBase
    {
        [Key]
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(20)]
        public string? Models { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Brand { get; set; }

        [MaxLength(30)]
        public string? Material { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public ServiceHasShoes? ServiceHasShoes { get; set; }

        public StorageHasShoes? StorageHasShoes { get; set; }
    }
}
