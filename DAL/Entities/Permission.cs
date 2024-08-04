namespace DAL.Entities
{
    public class Permission : EntitiesBase
    {
        public required Role Role { get; set; }
        public required string ControllerName { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsApprove { get; set; }
        public bool IsStatistic { get; set; }
    }
}
