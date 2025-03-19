using cooker_api.entities;
using cooker_api.Features.Favourite.GetFavouriteByUserId;
using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace cooker_api.Features.User.GetUserByUserId
{
     public class GetUserByUserIdQueryHandler : IRequestHandler<GetUserByUserIdQuery, ResponseModel<UserModel>>
    {
        private readonly CookerContext _db;

        public GetUserByUserIdQueryHandler(CookerContext db)
        {
            _db = db;
        }

        public async Task<ResponseModel<UserModel>> Handle(GetUserByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userData = await _db.Users
                .Where(Q => Q.UserId == request.UserId)
                .Select(Q => new UserModel
                {
                    UserId = Q.UserId,
                    PostAmount = Q.PostAmount,
                    FavouriteAmount = Q.FavouriteAmount,
                    Username = Q.Username,
                    ProfilePicture = Q.ProfilePicture,
                })
                .FirstOrDefaultAsync();

            if (userData == null)
            {
                return new ResponseModel<UserModel>
                {
                    Data = null,
                    Title = "User data not found.",
                    Status = 404,
                    Detail = $"No user data found for User ID {request.UserId}.",
                    Instance = ""
                };
            }
            else
            {
                return new ResponseModel<UserModel>
                {
                    Data = userData,
                    Title = "User data found.",
                    Status = 200,
                    Detail = "User data retrieved successfully.",
                    Instance = ""
                };
            }
        }
    }
}
