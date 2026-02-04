using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : Controller
    {
        


        [HttpGet("sync")]
        public IActionResult Index()
        {
            Stopwatch cronometro= Stopwatch.StartNew();
            Thread.Sleep(2000); // Simula una operación síncrona que toma tiempo
            Console.WriteLine("Conexion a BD terminada");
            Thread.Sleep(2000); // Simula otra operación síncrona que toma tiempo
            Console.WriteLine("Envio de Mail terminado");
            cronometro.Stop();
             

            return Ok(cronometro.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch cronometro = Stopwatch.StartNew();

            var task1 = new Task<int>(
                () =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Conecxion a BD terminada");
                    return 1;
                }


                );
            var task2 = new Task<int>(
                () =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Envio de mail terminado");
                    return 2;
                }


                );




            task1.Start();
            task2.Start();

            Console.WriteLine("hago otra cosa");
            var result1 = await task1;
            var result2 = await task2;


            Console.WriteLine("Todo ha terminado");

            cronometro.Stop();
            return Ok($"{result1}----{result2}----{cronometro.Elapsed}");
        }
    }
}
