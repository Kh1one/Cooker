using cooker_api.Features.Comment.GetCommentByPostId;
using cooker_api.Features.Post.GetPostByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/get-comment-by-post-id/{PostId}")]
    [ApiController]
    public class GetCommentByPostIdController : ControllerBase
    {
        private readonly ISender _sender;
        public GetCommentByPostIdController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet]
        public async Task<IActionResult> GetCommentByPostId(int PostId)
        {
            var command = new GetCommentByPostIdQuery(PostId);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
