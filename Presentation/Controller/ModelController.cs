using Entities.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
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
        public ModelController(IServiceManager serviceMananger)
        {
            _serviceManager = serviceMananger;
        }

        [HttpPost("add-model")]
        public IActionResult AddModel(ModelsDto inputModel)
        {
            _serviceManager.ModelService.CreateModel(inputModel);
            return Ok(inputModel);
        }

        [HttpGet("get-model-list")]
        public IActionResult GetModelList()
        {
            var modelList = _serviceManager.ModelService.GetAllModels(false);
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
