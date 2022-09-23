namespace ShoeService_Model.Models
{
    public abstract class EntityBase
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public string? UpdatedBy { get; set; }
        public bool IsLocked { get; set; } = false;

    }
}
