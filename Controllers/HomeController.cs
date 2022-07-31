using System;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookWorn.Models;
using BookWorm.Service ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BookWorm.Models;

namespace BookWorn.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ProductService _productService ; 
    private readonly ILogger<HomeController> _logger;
    private readonly EntityModel _entityModel ; 
    private readonly UserManager<User> _userManager ; 
    private readonly BuyService _buyService ; 
    public HomeController(ILogger<HomeController> logger , EntityModel entityModel , ProductService productService , UserManager<User> UserManager , BuyService buyService)
    {
        _entityModel = entityModel ; 
        _logger = logger;
        _productService = productService ; 
        _userManager = UserManager ; 
        _buyService = buyService ; 
    }
    [Route("/{id?}")] 
    public IActionResult Index(string? id , string textFind)
    {
        IEnumerable<Product> products = _productService.GetAllProduct() ; 
        if(!string.IsNullOrEmpty(id)){
            products = products.Where(product => product.CategoryID == id);
        }
        if(!string.IsNullOrEmpty(textFind)){
            products = products.Where(product => product.Name.ToLower().Contains(textFind.ToLower()));
        }
        return View(products.ToList());
    }
    [Route("/detail/{id}")]
    public IActionResult Detail(string id){
        Product product = _productService.GetProductById(id);
        return View(product);
    }
    [Route("/profile/{id?}")]
    public async Task<IActionResult> ProfileAsync(string? id){
        User user ; 
        if(id == null){
            user = await _userManager.GetUserAsync(User);     
        }
        else{
            user = await _userManager.FindByIdAsync(id);
        }
        List<Product> products =  _productService.GetProductsByUserID(user.Id);
        ProfileModel profileModel = new ProfileModel(); 
        profileModel.CurrentUser = user ; 
        profileModel.Products = products ;
        return View(profileModel);  
    }
    public IActionResult Privacy()
    {
        return View();
    }
    [Route("/buy/{id?}")]
    public async Task<IActionResult> Buy(string id , string returnUrl){
       Product product = _productService.GetProductById(id); 
       User user = await _userManager.GetUserAsync(User);
       StringModel stringModel = new StringModel(); 
       if(user.Id == product.UserID){
            stringModel.StringMessage = "You can't buy your item" ; 
       }
       else if(user.Money < product.Price){
            stringModel.StringMessage = "You can't buy this item because you don't have enough money"; 
       }
       else{
            stringModel.StringMessage = "Buy success" ; 
            user.Money -= product.Price ;   
            User Seller = await _userManager.FindByIdAsync(product.UserID) ; 
            Seller.Money += product.Price ; 
            await _userManager.UpdateAsync(user);
            _buyService.buyProduct(user.Id , product.UserID , product.ID);
       }
       return View(stringModel); 
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
