using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    [Table("Functions")]
    public class Function
    {
        [Key]
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string? Name { get; set; }

        [MaxLength(200)]
        [Required]
        public string? Url { get; set; }

        [Required]
        public int SortOrder { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? ParentId { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? Icon { get; set; }
    }
}
