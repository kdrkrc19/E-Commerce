using Entities.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        public BrandController(IServiceManager serviceMananger)
        {
            _serviceManager = serviceMananger;
        }

        [HttpPost("add-brand")]
        public IActionResult AddBrand(BrandsDto brand)
        {
            _serviceManager.BrandService.CreateBrand(brand);
            return Ok(brand);
        }

        [HttpGet("get-brand-list")]
        public IActionResult GetBrandList()
        {
            var incomingBrandList = _serviceManager.BrandService.GetAllBrands(false);
            if (incomingBrandList != null) return Ok(incomingBrandList);
            else return NotFound();
        }

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
    }    
}