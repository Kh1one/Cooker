namespace cooker_api.Models
{
    public class ResponseModel<Model>
    {
        public Model? Data { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; } = 0;
        public string Detail { get; set; } = string.Empty;
        public string Instance { get; set; } = string.Empty;


    }
}
