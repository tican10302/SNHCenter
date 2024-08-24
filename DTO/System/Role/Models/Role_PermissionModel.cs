namespace DTO.System.Role.Models
{
    public class Role_PermissionModel
    {
        public Guid Id { get; set; }
        public Guid? RoleId { get; set; }
        public string? ControllerName { get; set; }
        public bool IsEdit { get; set; } = false;
        public bool IsApprove { get; set; } = false;
        public bool IsAdd { get; set; } = false;
        public bool IsStatistic { get; set; } = false;
        public bool IsView { get; set; } = false;
        public bool IsDelete { get; set; } = false;
        public string? Name { get; set; }
        public bool HasEdit { get; set; } = false;
        public bool HasApprove { get; set; } = false;
        public bool HasAdd { get; set; } = false;
        public bool HasStatistic { get; set; } = false;
        public bool HasView { get; set; } = false;
        public bool HasDelete { get; set; } = false;
    }
}
