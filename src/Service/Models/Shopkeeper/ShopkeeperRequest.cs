namespace Service.Models.Shopkeeper
{
    public class ShopkeeperRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
    }
}
