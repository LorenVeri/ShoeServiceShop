using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    public class Customer : EntityBase
    {
        [Key]
        [Required]
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(150)]
        public string? PasswordHash { get; set; }

        [MaxLength(150)]
        public string? CustomerEmail { get; set; }

        [MaxLength(255)]
        public string? CustomerAddress { get; set; }

        [Required]
        public bool IsActived { get; set; }

        [Required]
        public int CustomerTelNumber { get; set; }

        public DateTime LastLoginDate { get; set; }

        public int MemberShipId { get; set; }

        public CustomerHasShoes CustomerHasShoes { get; set; }

    }
}
