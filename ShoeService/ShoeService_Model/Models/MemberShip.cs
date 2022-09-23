using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    public class MemberShip : EntityBase
    {
        [Key]
        [Required]
        public int MemberShipId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public DateTime ExpiredDate { get; set; }

        [Required]
        public string? Status { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
