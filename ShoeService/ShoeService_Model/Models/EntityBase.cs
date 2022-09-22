using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    public abstract class EntityBase
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public bool IsDelete { get; set; }

    }
}
