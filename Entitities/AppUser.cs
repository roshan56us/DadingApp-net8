namespace API.Entitities
{
    public class AppUser
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required byte[] PassswordHash { get; set; }
        public required byte[] PassswordSalt { get; set; }

    }
}