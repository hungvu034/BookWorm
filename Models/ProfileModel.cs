using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWorn.Models;

namespace BookWorm.Models
{
    public class ProfileModel{
        public User CurrentUser { get; set; }
        public List<Product> Products { get; set; }
    }
}