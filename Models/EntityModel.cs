using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookWorn.Models
{
    public class EntityModel : IdentityDbContext<User , IdentityRole , string>
    {
        public EntityModel(DbContextOptions<EntityModel> options) : base(options){
 
        }
    }
}