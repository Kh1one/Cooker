using cooker_api.entities;
using cooker_api.Features.Favourite.InsertFavourite;
using cooker_api.Models;
using cooker_api.Services;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace cooker_api.Features.Comment.InsertComment
{
    public class InsertCommentCommandHandler : IRequestHandler<InsertCommentCommand, ResponseModel<CommentModel>>
    {
        private readonly CookerContext _db;
        private readonly IValidator<CommentModel> _registerValidator;

        public InsertCommentCommandHandler(CookerContext db, IValidator<CommentModel> registerValidator)
        {
            _db = db;
            _registerValidator = registerValidator;
        }

        public async Task<ResponseModel<CommentModel>> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _registerValidator.ValidateAsync(request.Request);
            if (validationResult.IsValid == false)
            {
                return new ResponseModel<CommentModel>
                {
                    Data = null,
                    Title = "Invalid model state",
                    Status = 400,
                    //Detail = validationResult.Errors.Select(e => e.ErrorMessage).ToList().ToString(),
                    Detail = "Validation failed: " + string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)),
                    Instance = ""
                    //HttpContext.Request.Path
                };
            }

            var entity = new entities.Comment
            {
                PostId = request.Request.PostId,
                UserId = request.Request.UserId,
                Content = request.Request.Content,
                LikeAmount = request.Request.LikeAmount,
                Edited = request.Request.Edited
            };

            _db.Comments.Add(entity);
            await _db.SaveChangesAsync();

            return new ResponseModel<CommentModel>
            {
                Data = null,
                Title = "Comment insert successful.",
                Status = 201,
                Detail = "New comment entry created in database.",
                Instance = ""
            };
        }
    }
}
