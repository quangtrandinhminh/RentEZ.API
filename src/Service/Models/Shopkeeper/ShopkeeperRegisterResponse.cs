namespace Service.Models.Shopkeeper
{
    public class ShopkeeperRegisterResponse
    {
        // user
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
    }
}
