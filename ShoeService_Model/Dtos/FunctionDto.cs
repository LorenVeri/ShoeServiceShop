using ShoeService_Model.Models;

namespace ShoeService_Model.Dtos
{
    public class FunctionDto
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Url { get; set; }

        public int SortOrder { get; set; }

        public string? ParentId { get; set; }

        public string? Icon { get; set; }

        public List<FuncChild>? FuncChild { get; set; }
    }

    public class FuncChild
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Url { get; set; }

        public int SortOrder { get; set; }

        public string? ParentId { get; set; }

        public string? Icon { get; set; }
    }
}
