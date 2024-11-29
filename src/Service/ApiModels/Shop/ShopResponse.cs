namespace Service.ApiModels.Shop
{
    public class ShopResponse
    {
        public int Id { get; set; }
        public string ShopEmail { get; set; }
        public string ShopName { get; set; }
        public string ShopPhone { get; set; }
        public string ShopAddress { get; set; }
        public string ShopAvatar { get; set; }
        public int? BankId { get; set; }
        public String? AccountNo { get; set; }
        public String? AccountName { get; set; }
        public decimal Rating { get; set; } = 0;
    }
}
