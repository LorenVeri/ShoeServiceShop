using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int ServiceId { get; set; }

        [Key]
        public int OrderId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double AmoutPrice { get; set; }

        [Required]
        public string? Status { get; set; }
    }
}
