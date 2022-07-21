using BookWorm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookWorn.Models
{
    public class EntityModel : IdentityDbContext<User , IdentityRole , string>
    {

        public DbSet<Bill> Bills { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; } 
        public DbSet<ProductBill> ProductBills { get; set; }
    
        public EntityModel(DbContextOptions<EntityModel> options) : base(options){
 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach(var entityType in builder.Model.GetEntityTypes()){
                string TableName = entityType.GetTableName() ; 
                if(TableName.StartsWith("AspNet")){
                    entityType.SetTableName(TableName.Substring(6));
                }
            }
            builder.Entity<Bill>(
                buildAction => buildAction.Property(bill => bill.ID).ValueGeneratedOnAdd()   
            );
            builder.Entity<Product>(
                buildAction => buildAction.Property(product => product.ID).ValueGeneratedOnAdd()   
            );
            builder.Entity<Category>(
                buildAction => buildAction.Property(category => category.ID).ValueGeneratedOnAdd()   
            );
            builder.Entity<ProductBill>(
                buildAction => buildAction.Property(productBill => productBill.ID).ValueGeneratedOnAdd()   
            );
        }
    }
}