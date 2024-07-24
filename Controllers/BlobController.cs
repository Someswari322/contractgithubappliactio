using azureapplicationdemo.api.Services.BlogStorage;
using azureapplicationdemo.api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace azureapplicationdemo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly IAzureBlogService _azureBlogService;

        public BlobController(IAzureBlogService azureBlogService)
        {
            _azureBlogService = azureBlogService;
        }
        [HttpPost("upload"),DisableRequestSizeLimit]
        
        public async Task<IActionResult> UploadFile([FromBody] FileUploadmodel model)
        {
            if(model.File == null || model.File.Length == 0)
            {
                return BadRequest("Invalid File");
            }
            var filestream = model.File.OpenReadStream();
            var result =  await _azureBlogService.UploadFileAsync("blobUpload", "mynewfolder", model.File.FileName, filestream);

            if (result.IsError)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
         

    }
}
