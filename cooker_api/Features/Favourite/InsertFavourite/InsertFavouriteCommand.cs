using cooker_api.Models;
using MediatR;

namespace cooker_api.Features.Favourite.InsertFavourite
{
    public record InsertFavouriteCommand(FavouriteModel Request) : IRequest<ResponseModel<FavouriteModel>>
    {
    }
}
