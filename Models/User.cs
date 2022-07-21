using Microsoft.AspNetCore.Identity;

namespace BookWorn.Models
{
    public class User : IdentityUser
    {
        public Decimal Money { get; set; } = 1000 ; 
        public string? Location { get; set; }
        public string? Image { get; set; } 

        public string? Description { get; set; } 

    }
}