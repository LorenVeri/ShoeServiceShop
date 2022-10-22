using ShoeService_Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Dtos
{
    public class CustomerDto : EntityBase
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PasswordHash { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerAddress { get; set; }

        public int CustomerTelNumber { get; set; }
        public int MemberShipId { get; set; }
    }
}
