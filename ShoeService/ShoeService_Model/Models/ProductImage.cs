using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    public class ProductImage : EntityBase
    {
        [Key]
        [Required]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(int.MaxValue)]
        public string? URL { get; set; }

        public Product Products { get; set; }

    }
}
