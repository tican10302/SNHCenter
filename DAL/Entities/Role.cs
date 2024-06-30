namespace DAL.Entities
{
    public class Role : EntitiesBase
    {
        // Admin: 0, User: 1
        public int RoleCode { get; set; } = 1;
        public required string Name { get; set; }
    }
}
