using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {


        [HttpGet]
        public decimal Get(decimal a , decimal b)
        {
            return a + b;

        }

        [HttpPost]
        public decimal Add( Numbers numbers ,[FromHeader] string Host,
            [FromHeader(Name = "Content-Lenght")] string ContentLenght  )
            
        {
            Console.WriteLine(Host);
            Console.WriteLine(ContentLenght);
            return numbers.A - numbers.B;

        }

        [HttpDelete]
        public decimal Delete(decimal a, decimal b)
        {
            return a * b;

        }



        [HttpPut]
        public decimal Put(decimal a, decimal b)
        {
            return a * b+1;

        }

    }

    public class Numbers
    {
       public decimal A
        {
            get; set;

        }

       public decimal B { get; set; }
    }
}
