using Microsoft.AspNetCore.Mvc;
using XfaFlatteningApi.Contracts;
using XfaFlatteningApi.Models;

namespace XfaFlatteningApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlattenerController : ControllerBase
    {
        private readonly ILogger<FlattenerController> _logger;
        private readonly IFlattenerUtil _flattenerUtil;

        public FlattenerController(ILogger<FlattenerController> logger, IFlattenerUtil flattenerUtil)
        {
            _logger = logger;
            _flattenerUtil = flattenerUtil;
        }

        [HttpPost(Name = "Flatten")]
        public async Task<IActionResult> Flatten()
        {
            try
            {
                var file = Request.Form.Files["file"];
                var flattenedBytes = await _flattenerUtil.GetFlattenedBytesAsync(file);

                if (flattenedBytes.Length == 0)
                {
                    _logger.LogError("Failed to convert the uploaded file to Flattened file.");

                    var errorResponse = GetErrorResponse("Flattened bytes found zero sized.");
                    return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
                }

                return new FileContentResult(flattenedBytes, "application/pdf")
                {
                    FileDownloadName = "flattened-file.pdf"
                };
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                    ex = ex.InnerException;

                var errorResponse = GetErrorResponse(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        private static ErrorResponse GetErrorResponse(string detail)
        {
            return new ErrorResponse(500, "An unexpected error occurred", detail);
        }
    }
}
