using cooker_api.Features.Comment.InsertComment;
using cooker_api.Features.Favourite.InsertFavourite;
using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/insert-comment")]
    [ApiController]
    public class InsertCommentController : Controller
    {
        private readonly ISender _sender;
        public InsertCommentController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> InsertComment([FromBody] CommentModel request)
        {
            var command = new InsertCommentCommand(request);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
