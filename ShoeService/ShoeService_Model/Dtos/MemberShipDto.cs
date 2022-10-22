using ShoeService_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Dtos
{
    public class MemberShipDto : EntityBase
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public DateTime ExpiredDate { get; set; }

        public string? Status { get; set; }
    }
}
