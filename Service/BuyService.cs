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
        private const int MAX = 4 ; 
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
        public class ProductTime{
            public Product Product { get; set ;}
            public DateTime Date { get; set; }
        }
        public List<ProductTime> getProductBillByTime(DateTime time){
             var productToday = from bills in _billRepository.GetAllBill() 
                                                join productbills in  _billRepository.GetAllProductBill()
                                                on bills.ID equals productbills.BillID
                                                join products in _productService.GetAllProduct()
                                                on productbills.ProductID equals products.ID 
                                                where CompareDateTime(bills.Date , time) == true 
                                                select new ProductTime{
                                                    Product = products , 
                                                    Date = bills.Date 
                                                }
                                                ;
            return productToday.ToList(); 
        }
        
        private bool CompareDateTime(DateTime left , DateTime right){
            return left.Year == right.Year && left.Month == right.Month && left.Day == right.Day ; 
        }

        public List<string> GetAllCustomerIDbyUserID(string ID){
           return _billRepository.GetAllBill().Where(bill => { return bill.UserSell == ID ; })
                                        .Select(bill => bill.UserBuy).Distinct().ToList();
        }
        
        public List<Product> GetProductBySellerAndCustomer(string SellerID , string CusomnerID){
                var ProductIDs = _billRepository.GetAllBill().Where(bill => 
                { return bill.UserSell == SellerID && bill.UserBuy == CusomnerID ; })
                                        .Select(bill => bill.ProductID).Distinct();
                List<Product> products = new List<Product>();
                foreach(var id in ProductIDs){
                    products.Add(_productService.GetProductById(id));
                }
                return products ; 
        }
        public Decimal GetMoneySpendByUserID(string ID){
            var Bills = _billRepository.GetAllBill();
            Decimal Money = 0 ; 
            foreach(var bill in Bills){
                if(bill.UserBuy == ID){
                    Money += bill.Price ; 
                }
            }
            return Money ; 
        }
        public Decimal GetIncomeByUserID(string ID){
            var Bills = _billRepository.GetAllBill();
            Decimal Money = 0 ; 
            foreach(var bill in Bills){
                if(bill.UserSell == ID){
                    Money += bill.Price ; 
                }
            }
            return Money ; 
        }
        public int CountTodayUser(string UserID){
            var Bills = _billRepository.GetAllBill(); 
            return Bills.Where(bill => bill.UserSell == UserID).DistinctBy(bill => bill.UserBuy).Count();
        }

        public List<Product> GetTopBestSellerProdut(){
            var bills = _billRepository.GetAllBill();
            var BillGrouping = bills.GroupBy(bill => bill.ProductID ); 
            int i = 0 ; 
            List<Product> products = new List<Product>(); 
            foreach(var bill in BillGrouping){
                i++ ; 
                products.Add(_productService.GetProductById(bill.Key));
                if(i == MAX){
                    break ; 
                }
            }
            return products;
        }

        public List<Product> GetBessDiscountProduct(){
            var products = _productService.GetAllProduct() ; 
            var topProduct = from product in products
                             orderby product.Discount descending
                             select product ;
            return topProduct.Take(MAX).ToList() ;    
        }

        public List<Product> GetTopNewProduct(){
            var products = _productService.GetAllProduct() ; 
            Comparison<Product> comparison = (Product left , Product right) => {
                return DateTime.Compare((DateTime)right.CreatedDate , (DateTime)left.CreatedDate) ; 
            };
            products.Sort(comparison);
            return products.Take(MAX).ToList();
        }
    }
    
}