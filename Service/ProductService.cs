using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWorm.Repository ;
using BookWorn.Models;
using Microsoft.AspNetCore.Identity;

namespace BookWorm.Service
{
    public class ProductService
    {
        private ProductRepository _productRepository ; 
        public ProductService(ProductRepository productRepository , CategoryRepository categoryRepository){
            _productRepository = productRepository ; 
        }
        public List<Product> GetAllProduct(){
            return _productRepository.GetProducts(); 
        }
        public List<Product> GetProductsByUser(User user){
            return GetAllProduct().Where(product => product.UserID == user.Id).ToList(); 
        }
        public void CreateProduct(Product product){
             _productRepository.CreateProduct(product); 
        }

        public List<Product> GetProductsByUserID(string userID)
        {
            return _productRepository.GetProductByUserId(userID); 
        }

        public Product GetProductById(string id)
        {
            return _productRepository.GetProductByID(id);
        }

        public void EditProduct(Product product){
            _productRepository.EditProduct(product); 
        }
        public void RemoveProduct(Product product){
            _productRepository.DeleteProduct(product);
        }
    }
}