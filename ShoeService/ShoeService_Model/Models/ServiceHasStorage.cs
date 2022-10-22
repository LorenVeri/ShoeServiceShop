using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeService_Model.Models
{
    [Table("ServiceHasStorage")]
    public class ServiceHasStorage
    {
        [Key]
        [Required]
        public int ServiceId { get; set; }

        [Key]
        [Required]
        public int StorageId { get; set; }

        public ICollection<Service>? Services { get; set; }
        public ICollection<Storage>? Storages { get; set; }
    }
}
