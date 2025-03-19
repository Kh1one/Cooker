using cooker_api.Models;
using MediatR;

namespace cooker_api.Features.Post.DeletePost
{
    public record DeletePostCommand(int RequestId) : IRequest<ResponseModel<PostModel>>
    {

    }
}
