using System.ComponentModel.DataAnnotations;
namespace BookWorn.Models
{
    public class Bill
    {
        [KeyAttribute()]
        public string? ID{get; set;}
        public DateTime Date{get; set;}
        public string UserBuy{get; set;}
        public string UserSell{get ; set ;}
        public string ProductID{get; set;}
        public Decimal Price{get; set;}
    }
}