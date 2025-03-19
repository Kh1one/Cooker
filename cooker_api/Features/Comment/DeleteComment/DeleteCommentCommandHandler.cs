using cooker_api.entities;
using cooker_api.Features.Comment.GetCommentByPostId;
using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.Comment.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, ResponseModel<CommentModel>>
    {
        private readonly CookerContext _db;
        public DeleteCommentCommandHandler(CookerContext db)
        {
            _db = db;
        }

        public async Task<ResponseModel<CommentModel>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var existingComments = await _db.Comments
                .Where(Q => Q.CommentId == request.CommentId)
                .FirstOrDefaultAsync();

            _db.Comments.Remove(existingComments!);
            await _db.SaveChangesAsync();

            return new ResponseModel<CommentModel>
            {
                Data = null,
                Title = "Comments deleted successfully.",
                Status = 204,
                Detail = "Comment entry found and deleted in database.",
                Instance = ""
            };
        }
    }
}
