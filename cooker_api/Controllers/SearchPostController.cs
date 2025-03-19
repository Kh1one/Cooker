using cooker_api.Features.Post.GetAllPosts;
using cooker_api.Features.Post.SearchPost;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Controllers
{
    [Route("api/v1/search-posts/{Page}/{PageSize}/{SearchTerm}")]
    [ApiController]
    public class SearchPostController : ControllerBase
    {
        private readonly ISender _sender;
        public SearchPostController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> SearchPost(int Page, int PageSize, string SearchTerm)
        {
            var command = new SearchPostQuery(Page, PageSize, SearchTerm);
            var response = await _sender.Send(command);

            response.Instance = HttpContext.Request.Path;
            return Ok(response);
        }
    }
}
