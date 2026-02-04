using Backend.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        IPostService _postService;
        public PostController(IPostService titlesService) 
        {
        
        }
        [HttpGet]
        public async Task<IEnumerable<PostDto>> Get()
        {
            return await _postService.Get();
        }



    }
}
