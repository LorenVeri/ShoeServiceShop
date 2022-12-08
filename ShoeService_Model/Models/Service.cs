using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    [Table("Service")]
    public class Service : EntityBase
    {
        [Key]
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

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

        public ServiceHasStorage? ServiceHasStorage { get; set; }
        public ServiceHasShoes? ServiceHasShoes { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }

    }
}
