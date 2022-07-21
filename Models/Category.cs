using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
namespace BookWorn.Models
{
    public class Category
    {
        [KeyAttribute()]
        public string? ID {get; set;}  
        [ColumnAttribute(TypeName = "nvarchar(MAX)")]
        public string Name { get; set ;} 
        
    }
}