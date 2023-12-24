using Entities.ModelsDTO;
using Microsoft.AspNetCore.Http.Headers;
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
    public class ModelController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly LinkGenerator _linkGenerator;
        public ModelController(IServiceManager serviceMananger, LinkGenerator linkGenerator)
        {
            _serviceManager = serviceMananger;
            _linkGenerator = linkGenerator;
        }

        [HttpPost("add-model", Name = "AddModel")]
        public IActionResult AddModel(ModelsDto inputModel)
        {
            _serviceManager.ModelService.CreateModel(inputModel);
            return Ok(inputModel);
        }

        [HttpGet("get-model-list")]
        public IActionResult GetModelList()
        {
            //Response.Headers.Add("Content-Type", "application/json");
            //Response.Headers.Add("Allow", "GET");
            //var incomingModelList = _serviceManager.ModelService.GetAllModels
            var modelList = _serviceManager.ModelService.GetAllModelList(false);
            return Ok(modelList);
        }

        [HttpGet("get-model-by-id/{id}")]
        public IActionResult GetModel(int id)
        {
            var incomingModel = _serviceManager.ModelService.GetModel(id, false);
            if (incomingModel != null) return Ok(incomingModel);
            return NotFound(id);
        }

        [HttpPut("update-model")]
        public IActionResult UpdateModel(ModelsDto inputModel)
        {
            var incomingModel = _serviceManager.ModelService.GetModel(inputModel.ModelId, false);
            if (incomingModel != null)
            {
                _serviceManager.ModelService.UpdateModel(inputModel);
                return Ok(inputModel);
            }
            return NotFound(inputModel.ModelId);
        }

        [HttpDelete("delete-model")]
        public IActionResult DeleteModel(int id)
        {
            var incomingModel = _serviceManager.ModelService.GetModel(id, false);
            if (incomingModel != null)
            {
                _serviceManager.ModelService.DeleteModel(id);
                return Ok(incomingModel);
            }
            return NotFound(id);
        }
    }
}
