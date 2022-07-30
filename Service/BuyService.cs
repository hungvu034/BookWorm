using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWorm.Repository;
using BookWorn.Models;

namespace BookWorm.Service
{
    public class BuyService
    {
        private readonly CategoryService _categoryService ; 
        private readonly ProductService _productService ; 
        private readonly BillRepository _billRepository ; 
        public BuyService(CategoryService categoryService , ProductService productService , BillRepository billRepository){
            _categoryService = categoryService ; 
            _productService = productService ; 
            _billRepository = billRepository ; 
        }
        public void buyProduct(string userBuyID  , string userSellID , string productID){
            Product product = _productService.GetProductById(productID); 
            _billRepository.CreateBill(userSellID , userBuyID , product);
        }
    }
}