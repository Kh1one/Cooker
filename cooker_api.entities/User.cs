using System;
using System.Collections.Generic;

namespace cooker_api.entities;

public partial class User
{
    public int UserId { get; set; }

    public int PostAmount { get; set; }

    public int FavouriteAmount { get; set; }

    public string Username { get; set; } = null!;

    public byte[]? ProfilePicture { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
