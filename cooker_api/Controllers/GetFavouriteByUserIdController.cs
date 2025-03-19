using cooker_api.Features.Favourite.GetFavouriteByUserId;
using cooker_api.Features.Post.GetPostByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/get-favourite-by-user-id/{UserId}")]
    [ApiController]
    public class GetFavouriteByUserIdController : ControllerBase
    {
        private readonly ISender _sender;
        public GetFavouriteByUserIdController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet]
        public async Task<IActionResult> GetFavouriteByUserId(int UserId)
        {
            var command = new GetFavouriteByUserIdQuery(UserId);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
