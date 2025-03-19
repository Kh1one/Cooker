using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Features.User.InsertUserData
{
    public record InsertUserDataCommand(UserModel Request) : IRequest<ResponseModel<UserModel>>
    { }
}
