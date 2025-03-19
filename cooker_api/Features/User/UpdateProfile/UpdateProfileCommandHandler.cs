using cooker_api.entities;
using cooker_api.Features.Post.UpdatePost;
using cooker_api.Models;
using cooker_api.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.User.UpdateUsername
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, ResponseModel<UserModel>>
    {
        private readonly CookerContext _db;

        public UpdateProfileCommandHandler(CookerContext db)
        {
             _db = db;
        }

        public async Task<ResponseModel<UserModel>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        { 
            var existingData = await _db.Users
                .Where(Q => Q.UserId == request.UpdateRequest.UserId)
                .FirstOrDefaultAsync();

            if (request.UpdateRequest.Username != null)
            {
                existingData!.Username = request.UpdateRequest.Username;
            }

            if (request.UpdateRequest.ProfilePicture != null)
            {
                existingData!.ProfilePicture = request.UpdateRequest.ProfilePicture;
            }

            if (request.UpdateRequest.Username != null || request.UpdateRequest.ProfilePicture != null)
            {
                _db.Users.Update(existingData!);
                await _db.SaveChangesAsync();

                var result = new UserModel
                {
                    UserId = existingData!.UserId,
                    Username = existingData.Username,
                    ProfilePicture = existingData.ProfilePicture,
                    PostAmount = existingData.PostAmount,
                    FavouriteAmount = existingData.FavouriteAmount
                };

                return new ResponseModel<UserModel>
                {
                    Data = result,
                    Title = "Profile update successful.",
                    Status = 200,
                    Detail = "Entry in database has been updated.",
                    Instance = ""
                };
            }

            return new ResponseModel<UserModel>
            {
                Data = null,
                Title = "No update done.",
                Status = 200,
                Detail = "No change in database.",
                Instance = ""
            };

        }

    }
}
