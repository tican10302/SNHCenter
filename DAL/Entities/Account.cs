namespace DAL.Entities
{
    public class Account : EntitiesBase
    {
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
        public string? Avatar { get; set; }
    }

}

