using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Features.Comment.UpdateComment
{
    public record UpdateCommentCommand(int CommentId, string Content) : IRequest<ResponseModel<CommentModel>>
    { }
}
