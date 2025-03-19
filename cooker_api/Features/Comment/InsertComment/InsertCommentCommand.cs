using cooker_api.Models;
using MediatR;

namespace cooker_api.Features.Comment.InsertComment
{
    public record InsertCommentCommand(CommentModel Request) : IRequest<ResponseModel<CommentModel>>
    { }
}
