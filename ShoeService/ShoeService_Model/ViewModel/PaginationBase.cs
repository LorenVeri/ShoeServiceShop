using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.ViewModel
{
    public class PaginationBase
    {
        public string? Status { get; set; }

        public string? Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int PageCount
        {
            get
            {
                var pageCount = (double)TotalRecords / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
        }
    }
}
