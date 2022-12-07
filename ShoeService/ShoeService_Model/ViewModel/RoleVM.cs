using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.ViewModel
{
    public class RoleVM
    {
        [Required(ErrorMessage = "Id is required")]
        public string? Id { get; set; }

        public string? Name { get; set; }
    }
}
