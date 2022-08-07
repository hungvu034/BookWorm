using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace BookWorn.Models
{
    public class Product
    {
        [KeyAttribute()]
        public string? ID{ get; set;}
        [ColumnAttribute(TypeName = "nvarchar(MAX)")]

        public string Name{ get; set; }
        public string? Image{ get; set;} 
        public decimal Price{ get; set;}
        [ColumnAttribute(TypeName = "nvarchar(MAX)")]
        public string Content{get; set;}
       [ColumnAttribute(TypeName = "nvarchar(MAX)")]
        public string Title{get; set;}

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int Discount { get; set; }
        public string? UserID { get; set; }
        [ForeignKeyAttribute("UserID")]
        public User? User { get; set; }
        [ForeignKey("CategoryID")]
        public Category? Category { get; set; }
        [Required()]
        public string CategoryID { get; set; }


    }
}