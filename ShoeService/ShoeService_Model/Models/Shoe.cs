using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    public class Shoe : EntityBase
    {
        [Key]
        [Required]
        public Guid ShoeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Models { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Brand { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Material { get; set; }
    }
}
