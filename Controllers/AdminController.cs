using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using BookWorn.Models;
using BookWorm.Service;
using Microsoft.AspNetCore.Authorization;

namespace BookWorm.Controllers
{
    [Route("/admin/{action=index}/{id?}")]
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<User> _userManage ; 
        private readonly IWebHostEnvironment _environment ; 
        private readonly SignInManager<User> _signInManager ; 
        private readonly ProductService _productService ; 
        public AdminController(ILogger<AdminController> logger 
        , UserManager<User> userManager
         , IWebHostEnvironment webHostEnvironment 
         , SignInManager<User> signInManager
         ,ProductService productService 
         )
        {
            _userManage = userManager ; 
            _environment = webHostEnvironment ; 
            _signInManager = signInManager ; 
            _productService = productService ; 
            _logger = logger;

        }
        public IActionResult Index()
        {
            string UserID = _userManage.GetUserId(User); 
            if(string.IsNullOrEmpty(UserID)){
                return View(_productService.GetAllProduct());
            }
            List<Product> products = _productService.GetProductsByUserID(UserID);
            return View(products);
        }
        
        public IActionResult Detail(string id){
            string UserID = _userManage.GetUserId(User); 
            List<Product> products = _productService.GetProductsByUserID(UserID);    
            var product = products.Find(product => product.ID == id); 
            return View(product);
        }
        [HttpGet()]
        public IActionResult Edit(string id){
            var foundedProduct = _productService.GetProductById(id);
            return View(foundedProduct);
        }

        public IActionResult Edit(Product product , IFormFile imageFile){
            if(ModelState.IsValid){
                product.UpdateDate = DateTime.Now ; 
                product.Image = imageFile.FileName ;        
                System.IO.File.Delete(Path.Combine(_environment.WebRootPath , "images" , product.Image));
                var stream = new FileStream(Path.Combine(_environment.WebRootPath , "images" , imageFile.FileName),FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close(); 
                _productService.EditProduct(product) ; 
                return RedirectToAction("index" , "admin");
            }
            return View(product);
        }

        public IActionResult Remove(string id){
            Product foundedProduct = _productService.GetProductById(id);
            _productService.RemoveProduct(foundedProduct);
            return RedirectToAction("index" , "admin");
        }

        public IActionResult Create(){
            return View(); 
        }
        [HttpPost()]
        public IActionResult Create(Product product , IFormFile file ){
            foreach(var item in ModelState.Values){
                foreach(var i in item.Errors){
                    Console.WriteLine(i.ErrorMessage);
                }
            }
            if(ModelState.IsValid){
                 string FilePath = Path.Combine(_environment.WebRootPath , "images" , file.FileName);
                 FileStream fileStream = new FileStream(FilePath , FileMode.Create);
                 file.CopyTo(fileStream);
                 fileStream.Close();
                 product.UserID = _userManage.GetUserId(User);
                 product.Image = file.FileName ;  
                 product.CreatedDate = DateTime.Now ; 
                 product.UpdateDate = DateTime.Now ; 
                _productService.CreateProduct(product);     
                return RedirectToAction("index");            
            }
            return View(product);
        }

        public IActionResult UserProfile(){
            return View(); 
        }
    }
}