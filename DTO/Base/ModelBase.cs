namespace DTO.Base
{
    public class ModelBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public bool IsEdit { get; set; } = false;
        public bool IsActived { get; set; } = true;
        public int Sort { get; set; }
    }
}
