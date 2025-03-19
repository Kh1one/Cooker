using cooker_api.Models;
using FluentValidation;
using MediatR;
using FluentValidation.Results;
using cooker_api.entities;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.Post.InsertPost
{
    public class InsertPostCommandHandler : IRequestHandler<InsertPostCommand, ResponseModel<PostModel>>
    {
        private readonly CookerContext _db;
        private readonly IValidator<PostModel> _registerValidator;

        public InsertPostCommandHandler(CookerContext db, IValidator<PostModel> registerValidator)
        {
            _db = db;
            _registerValidator = registerValidator;
        }

        public async Task<ResponseModel<PostModel>> Handle(InsertPostCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _registerValidator.ValidateAsync(request.Request);
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

            // Map properties from request to entity
            var entity = new entities.Post()
            {
                UserId = request.Request.UserId,
                Description = request.Request.Description,
                Ingridients = request.Request.Ingridients,
                Instructions = request.Request.Instructions,
                FavouriteAmount = request.Request.FavouriteAmount,
                HeaderPicture = request.Request.HeaderPicture,
                Title = request.Request.Title
            };

            var userData = await _db.Users
                .Where(Q => Q.UserId == request.Request.UserId)
                .FirstOrDefaultAsync();

            if (userData == null)
            {
                return new ResponseModel<PostModel>
                {
                    Data = null,
                    Title = "Post creation unsuccessful.",
                    Status = 422,
                    Detail = $"The User ID of {request.Request.UserId} does not exist in database.",
                    Instance = ""
                };
            }
            else
            {
                userData.PostAmount++;

                _db.Users.Update(userData);
                await _db.SaveChangesAsync();

                _db.Posts.Add(entity);
                await _db.SaveChangesAsync();

                return new ResponseModel<PostModel>
                {
                    Data = null,
                    Title = "Post created successfully.",
                    Status = 201,
                    Detail = "The post has been added to the database.",
                    Instance = ""
                };
            }
        }
    }
}
