using ShoeService_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Dtos
{
    public class StorageDto : EntityBase
    {
        public string? Name { get; set; }

        public string? Code { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public string? Status { get; set; }
    }
}
