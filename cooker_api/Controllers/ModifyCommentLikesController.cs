using cooker_api.Features.Comment.ModifyCommentLikes;
using cooker_api.Features.Post.UpdatePost;
using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/modify-likes/{CommentId}/{Amount}")]
    [ApiController]
    public class ModifyCommentLikesController : ControllerBase
    {
        private readonly ISender _sender;
        public ModifyCommentLikesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut]
        public async Task<IActionResult> ModifyComment(int CommentId, int Amount)
        {
            var command = new ModifyCommentLikesCommand(CommentId, Amount);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
