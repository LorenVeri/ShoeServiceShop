using ShoeService_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Dtos
{
    public class ServiceDto : EntityBase
    {
        public string? Name { get; set; }

        public string? Code { get; set; }

        public double Price { get; set; }

        public double DiscountPrice { get; set; }

        public bool IsFree { get; set; }

        public bool IsOnSale { get; set; }
    }
}
