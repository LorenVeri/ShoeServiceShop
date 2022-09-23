using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    public class Order : EntityBase
    {
        [Key]
        [Required]
        public int OrderId { get; set; }

        public OrderDetail OrderDetail { get; set; }
    }
}
