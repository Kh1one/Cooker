using cooker_api.entities;
using cooker_api.Features.Post.DeletePost;
using cooker_api.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.Post.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, ResponseModel<PostModel>>
    {
        private readonly CookerContext _db;
        private readonly IValidator<PostModel> _registerValidator;

        public UpdatePostCommandHandler(CookerContext db, IValidator<PostModel> registerValidator)
        {
            _registerValidator = registerValidator;
            _db = db;
        }

        public async Task<ResponseModel<PostModel>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _registerValidator.ValidateAsync(request.UpdateRequest);
            if (validationResult.IsValid == false)
            {
                return new ResponseModel<PostModel>
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

            var existingData = await _db.Posts
                .Where(Q => Q.PostId == request.UpdateRequest.PostId)
                .FirstOrDefaultAsync();

            if (existingData == null)
            {
                return new ResponseModel<PostModel>
                {
                    Data = null,
                    Title = "Post not found.",
                    Status = 422,
                    Detail = $"Post ID of {request.UpdateRequest.PostId} does not exist in database.",
                    Instance = ""
                };
            }
            else
            {
                existingData.Description = request.UpdateRequest.Description;
                existingData.Ingridients = request.UpdateRequest.Ingridients;
                existingData.Instructions = request.UpdateRequest.Instructions;
                existingData.HeaderPicture = request.UpdateRequest.HeaderPicture;
                existingData.Title = request.UpdateRequest.Title;


                _db.Posts.Update(existingData);
                await _db.SaveChangesAsync();

                return new ResponseModel<PostModel>
                {
                    Data = request.UpdateRequest,
                    Title = "Post updated successfully.",
                    Status = 200,
                    Detail = "The post has been updated successfully.",
                    Instance = ""
                };
            }
        }
    }
}
