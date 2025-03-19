using cooker_api.Features.Post.DeletePost;
using cooker_api.Features.Post.InsertPost;
using cooker_api.Models;
using cooker_api.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/delete-post/{PostId}")]
    [ApiController]
    public class DeletePostController : ControllerBase
    {
        private readonly ISender _sender;
        public DeletePostController(ISender sender)
        {
            _sender = sender;
        }


        [HttpDelete]
        public async Task<IActionResult> DeletePost(int PostId)
        {
            var command = new DeletePostCommand(PostId);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
