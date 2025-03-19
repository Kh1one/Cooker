namespace cooker_api.Models
{
    public class CommentModel
    {
        public int CommentId { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

        public string Content { get; set; } = string.Empty;

        public int LikeAmount { get; set; }

        public bool Edited { get; set; }

    }
}
