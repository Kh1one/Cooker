using cooker_api.entities;
using cooker_api.Features.Favourite.GetFavouriteByUserId;
using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace cooker_api.Features.Comment.GetCommentByPostId
{
    public class GetCommentByPostIdQueryHandler : IRequestHandler<GetCommentByPostIdQuery, ResponseModel<List<CommentModel>>>
    {
        private readonly CookerContext _db;
        public GetCommentByPostIdQueryHandler(CookerContext db)
        {
            _db = db;
        }

        public async Task<ResponseModel<List<CommentModel>>> Handle(GetCommentByPostIdQuery request, CancellationToken cancellationToken)
        {
            var postComments = await _db.Comments
                .Where(Q => Q.PostId == request.PostId)
                .Select(Q => new CommentModel
                {
                    CommentId = Q.CommentId,
                    PostId = Q.PostId,
                    UserId = Q.UserId,
                    Content = Q.Content,
                    LikeAmount = Q.LikeAmount,
                    Edited = Q.Edited
                })
                .ToListAsync();

            return new ResponseModel<List<CommentModel>>
            {
                Data = postComments.Any() ? postComments: null,
                Title = postComments.Any() ? "Comments retrieved successfully." : "No comments found.",
                Status = postComments.Any() ? 200 : 404,
                Detail = postComments.Any() ? "Comments found in database." : $"No comments found for the Post ID {request.PostId}.",
                Instance = ""
            };
        }
    } 
}

