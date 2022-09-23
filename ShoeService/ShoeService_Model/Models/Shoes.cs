using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    public class Shoes : EntityBase
    {
        [Key]
        [Required]
        public int ShoesId { get; set; }

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

        public Guid ShoeRepositoryId { get; set; }

        public CustomerHasShoes CustomerHasShoes { get; set; }
        public ShoesRepository ShoesRepositories { get; set; }
        public ServiceHasShoes ServiceHasShoes { get; set; }
    }
}
