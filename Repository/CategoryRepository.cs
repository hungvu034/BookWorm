using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWorn.Models;

namespace BookWorm.Repository
{
    public class CategoryRepository
    {
        private EntityModel _entityModel; 
        public CategoryRepository(EntityModel entityModel){
            _entityModel = entityModel ; 
        }
        public List<Category> GetCategories(){
            return _entityModel.Categories.ToList() ; 
        }
        public bool CheckCategoryNameExist(string Name){
            var Category = ( from categories in _entityModel.Categories 
                           where categories.Name == Name 
                           select categories ).FirstOrDefault() ; 
            if(Category == null){
                return false ; 
            }
            return true ;  
        }
        public void CreateCategory(Category category){
            _entityModel.Add(category); 
            _entityModel.SaveChanges();
        }
        public void RemoveCategory(Category category){
            _entityModel.Remove(category);
            _entityModel.SaveChanges();
        }
        public Category FindCategoryByName(string Name){
            var category =( from categories in _entityModel.Categories 
                            where categories.Name == Name
                            select categories ).FirstOrDefault() ;
            return category ;                 
        }
        public Category FindCategoryByID(string ID){
                        var category =( from categories in _entityModel.Categories 
                            where categories.ID == ID
                            select categories ).FirstOrDefault() ;
            return category ;     
        }
    }
}