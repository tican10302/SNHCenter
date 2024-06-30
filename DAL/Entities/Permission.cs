namespace DAL.Entities
{
    public class Permission : EntitiesBase
    {
        public required Role Role { get; set; }
        public required string ControllerName { get; set; }
        public bool IsView { get; set; } = false;
        public bool IsAdd { get; set; } = false;
        public bool IsEdit { get; set; } = false;
        public bool IsDelete { get; set; } = false;
    }
}
