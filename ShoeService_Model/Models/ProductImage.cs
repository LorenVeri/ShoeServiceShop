using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    [Table("ProductImage")]
    public class ProductImage
    {
        [Key]
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Required]
        [MaxLength(int.MaxValue)]
        public string? URL { get; set; }

        [Required]
        public int ProductId { get; set; }

        public Product? Product { get; set; }

    }
}
