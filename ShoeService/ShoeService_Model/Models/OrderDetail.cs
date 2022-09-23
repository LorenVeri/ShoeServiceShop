using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    public class OrderDetail
    {
        [Key]
        public int ShoesId { get; set; }

        [Key]
        public int OrderId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double AmoutPrice { get; set; }

        [Required]
        public string? Status { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
