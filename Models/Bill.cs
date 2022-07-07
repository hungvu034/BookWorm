namespace BookWorn.Models
{
    public class Bill
    {
        public string ID{get; set;}
        public DateTime Date{get; set;}
        public int UserBuy{get; set;}
        public int UserSell{get ; set ;}
        public int ProductID{get; set;}
        public Decimal Price{get; set;}
    }
}