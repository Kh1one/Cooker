using Microsoft.AspNetCore.Mvc;

namespace cooker_api.Models
{
    public class PostModel
    {
        public int? PostId { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; } = string.Empty;

        public string Ingridients { get; set; } = string.Empty;

        public string Instructions { get; set; } = string.Empty;

        public int FavouriteAmount { get; set; }

        public byte[]? HeaderPicture { get; set; } = null;

        public string Title { get; set; } = string.Empty;

    }
}
