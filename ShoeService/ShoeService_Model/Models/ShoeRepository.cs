using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    public class ShoeRepository : EntityBase
    {
        [Key]
        [Required]
        public Guid ShoeRepositoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Weight { get; set; }
    }
}
