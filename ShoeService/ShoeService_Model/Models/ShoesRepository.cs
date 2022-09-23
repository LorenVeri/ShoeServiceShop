using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    public class ShoesRepository : EntityBase
    {
        [Key]
        [Required]
        public int ShoesRepositoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        [MaxLength(25)]
        public string? Status { get; set; }

        public ServiceHasShoesRepository ServiceHasShoesRepositories { get; set; }
        public ICollection<Shoes> Shoes { get; set; }
    }
}
