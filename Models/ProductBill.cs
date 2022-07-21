using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWorn.Models;

namespace BookWorm.Models
{
    public class ProductBill
    {
        [KeyAttribute()]
        public string? ID { get; set; }
        public string? ProductID { get; set; }
        public string? BillID{ get; set; }
        [ForeignKeyAttribute("ProductID")]
        public Product Product { get; set; }
        [ForeignKey("BillID")]
        public Bill Bill { get; set; }
    }
}