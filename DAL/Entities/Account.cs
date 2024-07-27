namespace DAL.Entities
{
    public class Account : EntitiesBase
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? Avatar { get; set; }
    }

}

