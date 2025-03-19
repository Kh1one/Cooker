using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Features.User.UpdateUsername
{
    public record UpdateProfileCommand(UserModel UpdateRequest) : IRequest<ResponseModel<UserModel>>
    {

    }
}
