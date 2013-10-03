namespace WebShop.Core.Users
{
    public class Payment
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
        public int Cvc { get; set; }
    }
}