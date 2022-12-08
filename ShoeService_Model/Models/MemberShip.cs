using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    [Table("MemberShip")]
    public class MemberShip : EntityBase
    {
        [Key]
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public DateTime ExpiredDate { get; set; }

        [Required]
        public string? Status { get; set; }

        public ICollection<Customer>? Customers { get; set; }
    }
}
