using cooker_api.Features.Comment.InsertComment;
using cooker_api.Features.User.InsertUserData;
using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/insert-user-data")]
    [ApiController]
    public class InsertUserDataController : Controller
    {
        private readonly ISender _sender;
        public InsertUserDataController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> InsertNewUser([FromBody] UserModel request)
        {
            var command = new InsertUserDataCommand(request);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
