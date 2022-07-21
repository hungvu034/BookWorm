using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWorn.Models;

namespace BookWorm.Repository
{
    public class ProductRepository
    {
        private readonly EntityModel _entityModel ;
        public ProductRepository(EntityModel entityModel){
            _entityModel = entityModel ; 
        }  
        public List<Product> GetProducts(){
           return _entityModel.Products.ToList() ; 
        }
        public Product GetProductByID(string ID){
            var product = (from products in _entityModel.Products
                          where products.ID == ID
                          select products).FirstOrDefault(); 
            if(product == null){
                throw new Exception(message: "Product ID NULL") ;
            }
            return product ; 
        }
        public void EditProduct(Product product){
            var CurrentProduct = GetProductByID(product.ID);
            _entityModel.Entry<Product>(CurrentProduct).CurrentValues.SetValues(product);
            _entityModel.SaveChanges();
        }
        public void CreateProduct(Product product){
            _entityModel.Add(product); 
            _entityModel.SaveChanges();
        }
        public void DeleteProduct(Product product){
            var CurrentProduct = GetProductByID(product.ID); 
            _entityModel.Remove(CurrentProduct); 
            _entityModel.SaveChanges();
        }
        public List<Product> GetProductByUserId(string userID){
            var product = from products in _entityModel.Products
                          where products.UserID == userID 
                          select products ; 
            return product.ToList() ; 
        }
    }
}