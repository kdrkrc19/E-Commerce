using Entities;
using Entities.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Services.Contracts;

namespace Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly LinkGenerator _linkGenerator;
        public BrandController(IServiceManager serviceMananger, LinkGenerator linkGenerator)
        {
            _serviceManager = serviceMananger;
            _linkGenerator = linkGenerator;
        }

        [HttpPost("add-brand", Name = "AddBrand")]
        public IActionResult AddBrand(BrandsDto brand)
        {
            _serviceManager.BrandService.CreateBrand(brand);
            return Ok(brand);
        }

        //[HttpGet("get-brand-list")]
        //public IActionResult GetBrandList()
        //{
        //    var incomingBrandList = _serviceManager.BrandService.GetAllBrandsList(,false);
        //    if (incomingBrandList != null) return Ok(incomingBrandList);
        //    else return NotFound();
        //}

        [HttpGet("get-brand-by-id/{id}")]
        public IActionResult GetBrand(int id)
        {
            var incomingBrand = _serviceManager.BrandService.GetBrand(id, false);
            if (incomingBrand != null) return Ok(incomingBrand);
            else return NotFound(id);
        }

        [HttpPut("update-brand")]
        public IActionResult UpdateBrand(BrandsDto inputBrand)
        {
            var incomingProduct = _serviceManager.BrandService.GetBrand(inputBrand.BrandId, false);
            if (incomingProduct != null)
            {
                _serviceManager.BrandService.UpdateBrand(inputBrand);
                return Ok(inputBrand);
            }
            else return NotFound(inputBrand.BrandId);
        }

        [HttpDelete("delete-brand")]

        public IActionResult DeleteBrand(int id)
        {
            var incomingBrand = _serviceManager.BrandService.GetBrand(id, false);
            if (incomingBrand != null)
            {
                _serviceManager.BrandService.DeleteBrand(id);
                return Ok(incomingBrand);
            }
            else return NotFound(id);
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
						Href = _linkGenerator.GetUriByName(HttpContext, nameof(AddBrand), new {  }),
						Rel = "_self",
						Method = "POST"
				},
				new Link() {
						Href = _linkGenerator.GetUriByName(HttpContext, nameof(DeleteBrand), new { }),
						Rel = "_self",
						Method = "DELETE"
				}
				};
				return Ok(list);
			}
			return NoContent();
		}

		[HttpGet("get-brand-list")]
		//[ResponseCache(Duration = 30)] //Maksimum Cacheleme süresi 60 saniye olarak ayarlandı
		public IActionResult GetBrandList([FromQuery] RequestParameters parameters)
		{
			Response.Headers.Add("Content-Type", "application/json");
			Response.Headers.Add("Allow", "GET");
			var incomingProductList = _serviceManager.BrandService.GetAllBrandsList(parameters, false);
			if (incomingProductList != null) return Ok(incomingProductList);
			return NotFound();
		}

		[HttpGet("get-brand-list-pagination")]
		//[ResponseCache(Duration = 30)] //Maksimum Cacheleme süresi 60 saniye olarak ayarlandı
		public IActionResult GetBrandListPagination([FromQuery] RequestParameters parameters)
		{
			Response.Headers.Add("Content-Type", "application/json");
			Response.Headers.Add("Allow", "GET");
			var incomingBrandList = _serviceManager.BrandService.GetPagedAndShapedBrands(parameters, false);
			if (incomingBrandList != null) return Ok(incomingBrandList);
			return NotFound();
		}
	}    
}