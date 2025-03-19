using cooker_api.Features.Favourite.InsertFavourite;
using cooker_api.Features.Post.InsertPost;
using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/insert-favourite")]
    [ApiController]
    public class InsertFavouriteController : ControllerBase
    {
        private readonly ISender _sender;
        public InsertFavouriteController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> InsertFavourite([FromBody] FavouriteModel request)
        {
            var command = new InsertFavouriteCommand(request);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
