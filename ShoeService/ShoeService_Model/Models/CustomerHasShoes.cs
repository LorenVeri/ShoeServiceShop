using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.Models
{
    public class CustomerHasShoes
    {
        [Key]
        public int ShoesId { get; set; }

        [Key]
        public int CustomerId { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<Shoes> Shoes { get; set; }
    }
}
