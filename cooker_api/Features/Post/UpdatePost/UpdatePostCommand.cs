using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Features.Post.UpdatePost
{
    public record UpdatePostCommand(PostModel UpdateRequest) : IRequest<ResponseModel<PostModel>>
    {
    }
}
