using cooker_api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Features.Favourite.DeleteFavourite
{
    public record DeleteFavouriteCommand(FavouriteModel Request) : IRequest<ResponseModel<FavouriteModel>>
    {

    }
}
