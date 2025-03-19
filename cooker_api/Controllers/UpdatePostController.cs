using Azure.Core;
using cooker_api.Features.Post.DeletePost;
using cooker_api.Features.Post.UpdatePost;
using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/update-post")]
    [ApiController]
    public class UpdatePostController : ControllerBase
    {
        private readonly ISender _sender;
        public UpdatePostController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost([FromBody] PostModel request)
        {
            var command = new UpdatePostCommand(request);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
