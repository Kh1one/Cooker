using Azure;
using cooker_api.Models;
using MediatR;

namespace cooker_api.Features.Post.InsertPost
{
    public record InsertPostCommand(PostModel Request) : IRequest<ResponseModel<PostModel>>
    {

    }
}
