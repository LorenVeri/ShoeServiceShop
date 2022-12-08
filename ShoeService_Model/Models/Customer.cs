using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeService_Model.Models
{
    [Table("Customer")]
    public class Customer : EntityBase
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? CustomerName { get; set; }

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

        public ICollection<Shoes>? Shoes { get; set; }
        public MemberShip? MemberShip { get; set; }

    }
}
