using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using static Backend.Controllers.People;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        // Puedes añadir endpoints API aquí

        public PeopleController()
        { 
        
        
        }
        [HttpGet("all")]
        public List<People>  GetPeople() =>Repository.People;

        [HttpGet("{id:int}")]// Restricción de tipo para evitar ambigüedad
        public ActionResult<People> GetforId(int id)
        {
            var people = Repository.People.FirstOrDefault(p => p.Id == id);
            if (people ==null)
            {
                return NotFound();
            }
            return Ok(people);
        }
            
            
           
        [HttpGet("{search}")]
        public List<People> GetBusqueda(string search)
        {
              if (string.IsNullOrWhiteSpace(search))
                return null;

            var results = Repository.People
                .Where(p => p.Name.ToUpper().Contains(search.ToUpper()))
                .ToList();

            return results;
        }
        [HttpPost]
        public IActionResult AddPeople([FromBody] People newPeople)
        {
            //*
            //Si esta vacio
            //*/
            if (newPeople == null)
            {
                return BadRequest("El objeto People no puede ser nulo.");
            }
            // Validar que el ID no exista ya
            if (Repository.People.Any(p => p.Id == newPeople.Id))
            {
                return Conflict("Ya existe una persona con el mismo ID.");
            }
            Repository.People.Add(newPeople);
            return NoContent(); //todo fue bien no devuelve nada
        }





    }

    public static class Repository
    {
        public static List<People> People = new List<People>()
        {
            new People(){
                Id = 1,
                Name = "John Doe",
                Birthdate = FechaHelper.CrearFecha(1990, 5, 15)
            },
            new People(){
                Id = 2,
                Name = "Jane Smith",
                Birthdate = FechaHelper.CrearFecha(1985, 8, 22)
            },
            new People(){
                Id = 3,
                Name = "Sam Brown",
                Birthdate = FechaHelper.CrearFecha(1995, 3, 10)
            }
        };
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }

        // Método estático para usar sin instanciar la clase
        public static DateTime CrearFechaStatic(int year, byte mes, byte dia)
        {
            return new DateTime(year, mes, dia);
        }

        // Método de instancia si lo prefieres
        public DateTime CrearFecha(int year, byte mes, byte dia)
        {
            return new DateTime(year, mes, dia);
        }

        public static class FechaHelper
        {
            public static DateTime CrearFecha(int year, byte mes, byte dia)
            {
                return new DateTime(year, mes, dia);
            }
        }


    }

}