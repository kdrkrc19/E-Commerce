using Entities;
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
    public class EMailUsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EMailUsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("add-mail", Name = "AddMail")]
        public IActionResult AddEmail(EmailUsDto eMail)
        {
            _serviceManager.EmailService.CreateMail(eMail);
            return Ok(eMail);
        }

        [HttpGet("get-mail-by-id/{id}")]
        public IActionResult GetModel(int id)
        {
            var incomingMail = _serviceManager.EmailService.GetMail(id, false);
            if (incomingMail != null) return Ok(incomingMail);
            return NotFound(id);
        }

        [HttpGet("get-mail-list")]
        public IActionResult GetModelList([FromQuery] RequestParameters parameters)
        {
            Response.Headers.Add("Content-Type", "application/json");
            Response.Headers.Add("Allow", "GET");

            var incomingMailList = _serviceManager.EmailService.GetAllMailsList(parameters, false);
            if (incomingMailList != null) return Ok(incomingMailList);
            return NotFound();

        }
    }
}
