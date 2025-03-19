using System;
using System.Collections.Generic;

namespace cooker_api.entities;

public partial class Post
{
    public int PostId { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Ingridients { get; set; } = null!;

    public string Instructions { get; set; } = null!;

    public int FavouriteAmount { get; set; }

    public byte[]? HeaderPicture { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    public virtual User User { get; set; } = null!;
}
