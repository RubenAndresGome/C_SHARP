using Backend.DTOs;
using System.Text.Json;

namespace Backend.Services
{
    public class PostService :IPostService 
    {
        private HttpClient _httpClient;
        public PostService(HttpClient httpclient)
        {
            _httpClient = httpclient;
        }

        public async Task<IEnumerable<PostDto>> Get()
        {

            string url = "https://jsonplaceholder.typicode.com/posts";
            var result =await _httpClient.GetAsync(url);
            var body = await result.Content.ReadAsStringAsync();


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var post = JsonSerializer.Deserialize<IEnumerable<DTOs.PostDto>>(
                body,options
            );

            return post;
        }

    }
}
