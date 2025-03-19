using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Features.User.GetUserByUserId
{
    public record GetUserByUserIdQuery(int UserId) : IRequest<ResponseModel<UserModel>>
    {
    }
}
