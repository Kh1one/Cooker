using cooker_api.entities;
using cooker_api.Features.Post.GetPostByUserId;
using cooker_api.Models;
using cooker_api.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.Comment.ModifyCommentLikes
{
    public class ModifyCommentLikesCommandHandler : IRequestHandler<ModifyCommentLikesCommand, ResponseModel<CommentModel>>
    {
        private readonly CookerContext _db;

        public ModifyCommentLikesCommandHandler(CookerContext db)
        {
            _db = db;

        }

        public async Task<ResponseModel<CommentModel>> Handle(ModifyCommentLikesCommand request, CancellationToken cancellationToken)
        {
            var results = await _db.Comments
                .Where(Q => Q.CommentId == request.CommentId)
                .FirstOrDefaultAsync();

            results!.LikeAmount += request.Amount;

            _db.Comments.Update(results);
            await _db.SaveChangesAsync();

            return new ResponseModel<CommentModel>
            {
                Data = null,
                Title = "Like amount changed",
                Status = 204,
                Detail = $"Comment likes changed to {results.LikeAmount}",
                Instance = ""
            };

        }
    }
}
