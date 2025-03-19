using cooker_api.Features.Post.DeletePost;
using cooker_api.Features.Post.GetPostByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/get-post-by-user-id/{UserId}")]
    [ApiController]
    public class GetPostByUserIdController : ControllerBase
    {
        private readonly ISender _sender;
        public GetPostByUserIdController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostByUserId(int UserId)
        {
            var command = new GetPostByUserIdQuery(UserId);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
