using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Features.Post.GetPostByUserId
{
    public record GetPostByUserIdQuery(int UserId) : IRequest<ResponseModel<List<PostModel>>>
    {

    }
}
