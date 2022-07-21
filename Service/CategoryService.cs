using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWorm.Repository;
using BookWorn.Models;

namespace BookWorm.Service
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository ; 
        public CategoryService(CategoryRepository categoryRepository){
            _categoryRepository = categoryRepository ; 
        }
        public Dictionary<string , string> GetIdNameCategory(){
            Dictionary<string , string> dictionary = new Dictionary<string, string>(); 
            var listCategorist =  _categoryRepository.GetCategories();
            foreach(var item in listCategorist){
                dictionary.Add(item.ID , item.Name);
            }
            return dictionary ; 
        }
        public List<Category> GetAllCategories(){
            return _categoryRepository.GetCategories();
        }
    }
}