using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Dtos
{
    public class ShoesDto
    {
        public int ShoeId { get; set; }

        public string? Name { get; set; }

        public string? Models { get; set; }

        public string? Brand { get; set; }

        public string? Material { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public bool IsDelete { get; set; }
    }
}
