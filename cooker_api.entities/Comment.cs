using System;
using System.Collections.Generic;

namespace cooker_api.entities;

public partial class Comment
{
    public int CommentId { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }

    public string Content { get; set; } = null!;

    public int LikeAmount { get; set; }

    public bool Edited { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
