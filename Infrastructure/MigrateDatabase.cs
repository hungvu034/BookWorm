using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWorn.Models;
using Microsoft.EntityFrameworkCore;
namespace BookWorm.Infrastructure
{
    public static class MigrateDatabase
    {
        public static IHost MigrateDb(this IHost host){
            var scope = host.Services.CreateScope();
            var model = scope.ServiceProvider.GetRequiredService<EntityModel>();
            model.Database.Migrate();
            return host ;
        } 
    }
}