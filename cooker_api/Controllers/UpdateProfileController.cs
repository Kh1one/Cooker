using cooker_api.Features.Post.UpdatePost;
using cooker_api.Features.User.UpdateUsername;
using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/update-profile")]
    [ApiController]
    public class UpdateProfileController : ControllerBase
    {
        private readonly ISender _sender;
        public UpdateProfileController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UserModel request)
        {
            var command = new UpdateProfileCommand(request);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
