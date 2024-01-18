using Entities;
using Entities.ModelsDTO;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 30)]
    //[ResponseCache(CacheProfileName = "MyCache")] 
    //Validation Model Cache Yöntemi Kullanılırsa
    //Eğer veri üzerinde değişiklik olmamışsa 304 not modified kodu döner.(Veriniz Hala Güncel)
    public class ProductController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly LinkGenerator _linkGenerator;
        //[FromHeader(Name = "Accept")]
        //public string mediaType { get; set; }
        public ProductController(IServiceManager serviceMananger, LinkGenerator linkGenerator)
        {
            _serviceManager = serviceMananger;
            _linkGenerator = linkGenerator;
        }

        [HttpPost("add-product",Name = "AddProduct")]
        public IActionResult AddProduct(ProductsDto inputProduct)
        {
            _serviceManager.ProductService.CreateProduct(inputProduct);
            return Ok(inputProduct);
        }

        //[HttpGet("get-product-list")]
        //public IActionResult GetProductList()
        //{
        //    var incomingProductList = _serviceManager.ProductService.GetAllProducts(false);
        //    if (incomingProductList != null) return Ok(incomingProductList);
        //    return NotFound();
        //}

        [HttpGet("get-product-by-id/{id}")]
        public IActionResult GetProduct(int id)
        {
            var incomingProduct = _serviceManager.ProductService.GetProduct(id, false);
            if (incomingProduct != null) return Ok(incomingProduct);
            return NotFound(id);
        }

        [HttpPut("update-product")]
        public IActionResult UpdateProduct(ProductsDto inputProduct)
        {
            var incomingProduct = _serviceManager.ProductService.GetProduct(inputProduct.ProductId, false);
            if (incomingProduct != null)
            {
                _serviceManager.ProductService.UpdateProduct(inputProduct);
                return Ok(inputProduct);
            }
            return NotFound(inputProduct.ProductId);

        }

        [HttpDelete("delete-product", Name = "DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            var incomingProduct = _serviceManager.ProductService.GetProduct(id, false);
            if (incomingProduct != null)
            {
                _serviceManager.ProductService.DeleteProduct(id);
                return Ok(incomingProduct);
            }
            return NotFound(id);
        }

        [HttpGet(Name = "GetRoot")]
        private IActionResult GetRoot()
        {
            var mediaTypess = HttpContext.Request.Headers["Accept"].ToString().Split(',');

            if (mediaTypess.Contains("application/vnd.acunmedyaakademi.apiroot"))
            {
            var list = new List<Link>() {
                new Link() {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(GetRoot), new {  }),
                        Rel = "_self",
                        Method = "GET"
                },
                new Link() {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(AddProduct), new {  }),
                        Rel = "_self",
                        Method = "POST"
                },
                new Link() {
                        Href = _linkGenerator.GetUriByName(HttpContext, nameof(DeleteProduct), new { }),
                        Rel = "_self",
                        Method = "DELETE"
                }
                };
            return Ok(list);
            }
            return NoContent();
        }

        //[HttpOptions]
        //[HttpHead]
        //[Authorize] // kullanıcı giriş yapmışsa 
        [HttpGet("get-product-list")]
        //[ResponseCache(Duration = 30)] //Maksimum Cacheleme süresi 60 saniye olarak ayarlandı
        public IActionResult GetProductList([FromQuery] RequestParameters parameters)
        {
            Response.Headers.Add("Content-Type", "application/json");
            Response.Headers.Add("Allow", "GET");
            var incomingProductList = _serviceManager.ProductService.GetAllProductsList(parameters, false);
            if (incomingProductList != null) return Ok(incomingProductList);
            return NotFound();
        }


        [HttpGet("get-product-list-pagination")]
		//[ResponseCache(Duration = 30)] //Maksimum Cacheleme süresi 60 saniye olarak ayarlandı
		public IActionResult GetProductListPagination([FromQuery] RequestParameters parameters)
		{
			Response.Headers.Add("Content-Type", "application/json");
			Response.Headers.Add("Allow", "GET");
			var incomingProductListWithPagination = _serviceManager.ProductService.GetPagedAndShapedProducts(parameters, false);
			if (incomingProductListWithPagination != null) return Ok(incomingProductListWithPagination);
			return NotFound();
		}

		//[HttpHead(Name = "get-product2")]
		//public IActionResult GetProduct2(int id) 
		//{
		//    var incomingProduct = _serviceManager.ProductService.GetProduct(id, false);
		//    if (incomingProduct!= null) return Ok(incomingProduct);
		//    return NotFound();
		//}



	}
}

//1- Pagination Filter modelinin entity katmanında tanımlanması gerekmektedir.
//2- Tanımlanan Pagination Filter modeli Data katmanındaki yada repodaki query eklenir.
//3- Service tarafında Pagination Filter modeline göre düzenleme yapılır.
//4- Sunum katmanındaki controller altında bulunan endpoint Pagination Filter'a göre düzenlenir.