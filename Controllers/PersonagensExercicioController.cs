using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RpgApi.models;
using RpgApi.models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagensExercicioController : ControllerBase
    {
         private static List<Personagem> personagens = new List<Personagem>()
        {
            //Personagens aqui
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        };
       
        [HttpGet("Ordenar")]
        public IActionResult Ordenar()
        {
            personagens = personagens.OrderBy(pers => pers.Nome).ToList();
            return Ok(personagens);
        }



// METODO A
        [HttpGet("GetByNome/{nome}")]
        public IActionResult GetByNome(string nome)
        {
          List<Personagem> resultado = personagens.FindAll(x => x.Nome.ToLower().Contains(nome.ToLower()));
           
            if (resultado.Count == 0 )
            {
              return NotFound("Nenhum personagem encontrado");
            }

            return Ok(resultado);
        }

        //METODO B
        

    }
}