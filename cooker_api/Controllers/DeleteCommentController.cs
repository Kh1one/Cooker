using cooker_api.Features.Comment.DeleteComment;
using cooker_api.Features.Favourite.DeleteFavourite;
using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/delete-comment/{CommentId}")]
    [ApiController]
    public class DeleteCommentController : ControllerBase
    {
        private readonly ISender _sender;
        public DeleteCommentController(ISender sender)
        {
            _sender = sender;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int CommentId)
        {
            var command = new DeleteCommentCommand(CommentId);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
