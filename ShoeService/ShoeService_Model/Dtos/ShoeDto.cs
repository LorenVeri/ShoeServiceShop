using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Dtos
{
    public class ShoeDto
    {
        public Guid ShoeId { get; set; }

        public string? Name { get; set; }

        public string? Models { get; set; }

        public string? Brand { get; set; }

        public string? Material { get; set; }
    }
}
