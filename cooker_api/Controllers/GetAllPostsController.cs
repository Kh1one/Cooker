using cooker_api.Features.Post.GetAllPosts;
using cooker_api.Features.Post.GetPostByUserId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/get-all-posts/{Page}/{PageSize}")]
    [ApiController]
    public class GetAllPostsController : ControllerBase
    {
        private readonly ISender _sender;
        public GetAllPostsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts(int Page, int PageSize)
        {
            var command = new GetAllPostsQuery(Page, PageSize);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
