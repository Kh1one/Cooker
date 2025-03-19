using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Features.Comment.DeleteComment
{
    public record DeleteCommentCommand(int CommentId) : IRequest<ResponseModel<CommentModel>>
    { }
}
