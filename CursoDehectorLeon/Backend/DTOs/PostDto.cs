namespace Backend.DTOs
{
    public class PostDto
    {

        public  long Id { get; set; }
        public  long UserId {  get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }



    }
}
