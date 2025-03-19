using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Features.Comment.ModifyCommentLikes
{
    public record ModifyCommentLikesCommand(int CommentId, int Amount) : IRequest<ResponseModel<CommentModel>>
    { }
}
