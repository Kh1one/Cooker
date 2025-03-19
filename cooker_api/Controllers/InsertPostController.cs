using cooker_api.Features.Post.InsertPost;
using cooker_api.Models;
using cooker_api.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/insert-post")]
    [ApiController]
    public class InsertPostController : ControllerBase
    {
        private readonly ISender _sender;
        public InsertPostController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> InsertNewPost([FromBody] PostModel request)
        {
            var command = new InsertPostCommand(request);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
