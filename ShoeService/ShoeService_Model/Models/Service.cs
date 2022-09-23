using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    public class Service : EntityBase
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double DiscountPrice { get; set; }

        [Required]
        public bool IsFree { get; set; }

        [Required]
        public bool IsOnSale { get; set; }

        public ServiceHasShoesRepository ServiceHasShoesRepositories { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public ServiceHasShoes ServiceHasShoes { get; set; }

    }
}
