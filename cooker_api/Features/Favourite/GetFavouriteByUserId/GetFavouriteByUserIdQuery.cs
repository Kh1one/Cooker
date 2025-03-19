using cooker_api.Models;
using MediatR;

namespace cooker_api.Features.Favourite.GetFavouriteByUserId
{
    public record GetFavouriteByUserIdQuery(int UserId) : IRequest<ResponseModel<List<PostModel>>>
    {
    }
}
