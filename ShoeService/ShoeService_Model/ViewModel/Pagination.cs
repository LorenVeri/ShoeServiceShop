using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.ViewModel
{
    public class Pagination<T> : PaginationBase where T : class
    {
        public object? Items { get; set; }
    }
}
