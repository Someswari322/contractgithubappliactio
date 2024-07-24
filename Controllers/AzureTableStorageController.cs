using azureapplicationdemo.api.Services.AzureTable;
using azureapplicationdemo.api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace azureapplicationdemo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureTableStorageController : ControllerBase
    {
        private readonly IAzurestorageTableservice _azurestorageTableservice;

        public AzureTableStorageController(IAzurestorageTableservice azurestorageTableservice)
        {
            _azurestorageTableservice = azurestorageTableservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var response= await  _azurestorageTableservice.GetAll();
            return Ok(response);
        
        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> GetById(string Id)
        {
            var response = await _azurestorageTableservice.GetbyId(Id);

            if (response == null)
            {
                return NotFound();

            }
            else { return Ok(response); }
        }

        [HttpPost]

        public async Task<IActionResult> Post([FromBody] BlogResponse blogResponse)
        {
            var response = await _azurestorageTableservice.Add(blogResponse);
            return new StatusCodeResult((int)HttpStatusCode.Created);
        }

        [HttpPost("{Id}")]

        public async Task<IActionResult> Put(string Id, [FromBody] BlogResponse blogResponse)
        {
            var response = await _azurestorageTableservice.Update(Id,blogResponse);
            if(!response)
            {
                return BadRequest(); 
            }
            return NoContent();
        }

        [HttpDelete("{Id}")]

        public async Task<IActionResult> Delete(string Id)
        {
            var response = await _azurestorageTableservice.Delete(Id);
            if (!response)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
