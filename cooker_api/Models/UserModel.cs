namespace cooker_api.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        public int PostAmount { get; set; }

        public int FavouriteAmount { get; set; }

        public string? Username { get; set; } = string.Empty;

        public byte[]? ProfilePicture { get; set; }

    }
}
