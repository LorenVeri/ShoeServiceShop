using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeService_Model.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ParentId { get; set; }
        public List<ChildCategoryViewModel>? childCategoryViewModels { get; set; }
    }

    public class ChildCategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int ParentId { get; set; }
        public List<ChildCategoryViewModel>? childCategoryViewModels { get; set; }
    }
}
