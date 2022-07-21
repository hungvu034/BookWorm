using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookWorn.Models;
using BookWorm.Service ;
using Microsoft.AspNetCore.Authorization;

namespace BookWorn.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ProductService _productService ; 
    private readonly ILogger<HomeController> _logger;
    private readonly EntityModel _entityModel ; 
    public HomeController(ILogger<HomeController> logger , EntityModel entityModel , ProductService productService)
    {
        _entityModel = entityModel ; 
        _logger = logger;
        _productService = productService ; 
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
    [Route("/user/{id?}")]
    public IActionResult User(){
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
