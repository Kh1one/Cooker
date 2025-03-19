using System;
using System.Collections.Generic;

namespace cooker_api.entities;

public partial class Favourite
{
    public int FavouriteId { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
