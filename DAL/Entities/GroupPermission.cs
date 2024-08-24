namespace DAL.Entities
{
    public class GroupPermission
    {
        public GroupPermission()
        {
            Sort = 0;
            IsActived = true;
        }
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int Sort { get; set; }
        public string? Icon { get; set; }
        public bool IsActived { get; set; }
    }
}
