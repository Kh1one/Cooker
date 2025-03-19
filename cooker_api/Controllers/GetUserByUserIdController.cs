using cooker_api.Features.Post.GetPostByUserId;
using cooker_api.Features.User.GetUserByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/get-user-by-user-id/{UserId}")]
    [ApiController]
    public class GetUserByUserIdController : ControllerBase
    {
        private readonly ISender _sender;
        public GetUserByUserIdController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByUserId(int UserId)
        {
            var command = new GetUserByUserIdQuery(UserId);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
