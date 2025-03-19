using cooker_api.Models;
using MediatR;

namespace cooker_api.Features.Post.SearchPost
{
    public record SearchPostQuery(int Page, int PageSize, string SearchTerm) : IRequest<ResponseModel<List<PostModel>>>
    {
    }
}
