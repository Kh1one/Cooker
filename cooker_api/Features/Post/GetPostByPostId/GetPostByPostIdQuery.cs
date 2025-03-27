using cooker_api.Models;
using MediatR;

namespace cooker_api.Features.Post.GetPostByPostId
{
    public record GetPostByPostIdQuery(int PostId) : IRequest<ResponseModel<PostModel>>
    { }
}
