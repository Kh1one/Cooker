using Azure.Core;
using cooker_api.Features.Favourite.DeleteFavourite;
using cooker_api.Features.Post.DeletePost;
using cooker_api.Features.Post.InsertPost;
using cooker_api.Models;
using cooker_api.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/delete-favourite")]
    [ApiController]
    public class DeleteFavouriteController : ControllerBase
    {
        private readonly ISender _sender;
        public DeleteFavouriteController(ISender sender)
        {
            _sender = sender;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFavourite([FromBody] FavouriteModel request)
        {
            var command = new DeleteFavouriteCommand(request);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
