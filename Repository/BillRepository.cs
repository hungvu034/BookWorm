using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWorm.Models;
using BookWorn.Models;

namespace BookWorm.Repository
{
    public class BillRepository
    {
        private readonly EntityModel _entityModel ; 
        public BillRepository(EntityModel entityModel){
            _entityModel = entityModel ; 
        }

        public List<Bill> GetAllBill(){
            return _entityModel.Bills.ToList() ; 
        }
        public List<ProductBill> GetAllProductBill(){
            return _entityModel.ProductBills.ToList();
        }
        public void CreateBill(string userSellID , string userBuyID , Product product){
            Bill bill = new Bill(){
                ID = Guid.NewGuid().ToString() , 
                Date = DateTime.Now ,
                Price = product.Price * (100 - product.Discount) / 100 , 
                ProductID = product.ID , 
                UserBuy = userBuyID , 
                UserSell = userSellID 
            } ; 
            _entityModel.Bills.Add(bill);
            _entityModel.SaveChanges(); 
        
            ProductBill productBill = new ProductBill(){
                ProductID = product.ID , 
                BillID = bill.ID 
            } ; 
            _entityModel.ProductBills.Add(productBill);
            _entityModel.SaveChanges();
        }
        public void RemoveBillByProductID(string id){
            var bills = GetAllBill(); 
            foreach(var bill in bills){
                if(bill.ProductID == id){
                    _entityModel.Remove(bill);
                }
            }
            _entityModel.SaveChanges();
        }
    }
}