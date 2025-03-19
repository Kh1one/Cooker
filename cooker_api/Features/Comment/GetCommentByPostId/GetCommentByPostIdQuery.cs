using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Features.Comment.GetCommentByPostId
{
    public record GetCommentByPostIdQuery(int PostId) : IRequest<ResponseModel<List<CommentModel>>>
    {

    }

}
