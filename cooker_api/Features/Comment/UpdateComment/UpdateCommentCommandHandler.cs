using cooker_api.entities;
using cooker_api.Features.Comment.InsertComment;
using cooker_api.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.Comment.UpdateComment
{
    //for updating comment's content, not for updating like amount, that's its own function
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, ResponseModel<CommentModel>>
    {
        private readonly CookerContext _db;

        public UpdateCommentCommandHandler(CookerContext db)
        {
            _db = db;
        }

        public async Task<ResponseModel<CommentModel>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var existingComment = await _db.Comments
                .Where(Q => Q.CommentId == request.CommentId)
                .FirstOrDefaultAsync();

            existingComment!.Content = request.Content;

            _db.Comments.Update(existingComment);
            await _db.SaveChangesAsync();

            return new ResponseModel<CommentModel>
            {
                Data = null,
                Title = "Comment update successful.",
                Status = 204,
                Detail = $"Comment's content was updated to \"{request.Content}\".",
                Instance = ""
            };
        }
    }
}
