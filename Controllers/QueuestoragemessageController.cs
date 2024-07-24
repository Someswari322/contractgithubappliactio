using azureapplicationdemo.api.Services;
using azureapplicationdemo.api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using System.Net;

namespace azureapplicationdemo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class QueuestoragemessageController : ControllerBase
    {
        private readonly IAzureinterfacebusservice _azureinterfacebusservice;
        public QueuestoragemessageController(IAzureinterfacebusservice azureinterfacebusservice) 
        {
            _azureinterfacebusservice = azureinterfacebusservice;
        }
        [HttpPost]
        public async Task<IActionResult> Post(string message) 
        {
            await  _azureinterfacebusservice.SendMessageAsync(message);
            return new StatusCodeResult((int)HttpStatusCode.Created);
        }
        [HttpPost("withJsonBody")]
        public async Task<IActionResult> Post([FromBody] MessageRequest messageRequest)
        {
            await _azureinterfacebusservice.SendMessageAsync(messageRequest);
            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetPeekMessages(int msgcount=5)
        {
           var messages =  await _azureinterfacebusservice.ReceivedMessageAsync(msgcount);
            return Ok(messages);

        }

        [HttpGet("receivedanddeleted")]
        public async Task<IActionResult> GetReceivedMessagesdelete(int msgcount = 5)
        {
            var messages = await _azureinterfacebusservice.ReceivedMessageDeleteAsync(msgcount);
            return Ok(messages);

        }
    }
}
