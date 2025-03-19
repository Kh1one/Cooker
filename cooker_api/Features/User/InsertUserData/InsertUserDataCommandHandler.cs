using cooker_api.entities;
using cooker_api.Features.Comment.InsertComment;
using cooker_api.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.User.InsertUserData
{
    public class InsertUserDataCommandHandler : IRequestHandler<InsertUserDataCommand, ResponseModel<UserModel>>
    {
        private readonly CookerContext _db;

        public InsertUserDataCommandHandler(CookerContext db)
        {
            _db = db;
        }

        public async Task<ResponseModel<UserModel>> Handle(InsertUserDataCommand request, CancellationToken cancellationToken)
        {
            if(request.Request.Username == null)
            {
                return new ResponseModel<UserModel>
                {
                    Data = null,
                    Title = "User data insert failed.",
                    Status = 4,
                    Detail = "Username field must not be null.",
                    Instance = ""
                };
            }
            else
            {
                var entity = new entities.User
                {
                    Username = request.Request.Username,
                    ProfilePicture = request.Request.ProfilePicture,
                };

                _db.Users.Add(entity);
                await _db.SaveChangesAsync();

                return new ResponseModel<UserModel>
                {
                    Data = null,
                    Title = "User insert successful.",
                    Status = 201,
                    Detail = "New user data entry created in database.",
                    Instance = ""
                };
            }
        }
    }
}
