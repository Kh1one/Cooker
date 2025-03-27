using cooker_api.Features.Post.GetPostByPostId;
using cooker_api.Features.Post.GetPostByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/get-post-by-post-id/{PostId}")]
    [ApiController]
    public class GetPostByPostIdController : ControllerBase
    {
        private readonly ISender _sender;
        public GetPostByPostIdController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostByPostId(int PostId)
        {
            var command = new GetPostByPostIdQuery(PostId);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
