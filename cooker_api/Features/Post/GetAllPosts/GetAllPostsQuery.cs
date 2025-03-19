using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Features.Post.GetAllPosts
{
    public record GetAllPostsQuery(int Page, int PageSize) : IRequest<ResponseModel<List<PostModel>>>
    {
    }

}
