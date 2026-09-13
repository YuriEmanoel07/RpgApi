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

        [HttpGet("GetClerigoMago")]
        public IActionResult GetClerigoMago()
        {
            List<Personagem> ListaFiltrada = personagens.FindAll(p => p.Classe != ClasseEnum.Cavaleiro).OrderByDescending(p => p.PontosVida).ToList();

            return Ok(ListaFiltrada);
        }
        
        // METODO C
      [HttpGet("GetEstatisticas")]
        public IActionResult GetEstatisticas()
        {
            int quantidade = personagens.Count;
            int somaInteligencia = personagens.Sum(p => p.Inteligencia);

            return Ok(new { Quantidade = quantidade, SomaInteligencia = somaInteligencia });
        }
        [HttpPost("PostValidacao")]
        public IActionResult PostValidacao(Personagem novoPersonagem)
        {
            if (novoPersonagem.Defesa < 10 || novoPersonagem.Inteligencia > 30)
            {
                return BadRequest("Defesa não pode ser menor que 10 e Inteligência não pode ser maior que 30.");
            }

            personagens.Add(novoPersonagem);
            return Ok(personagens);
}
            [HttpPost("PostValidacaoMago")]
            public IActionResult PostValidacaoMago(Personagem novoPersonagem)
            {
                if (novoPersonagem.Classe == ClasseEnum.Mago && novoPersonagem.Inteligencia < 35)
                {
                    return BadRequest("Personagens da classe Mago não podem ter Inteligência menor que 35.");
                }

                personagens.Add(novoPersonagem);
                 return Ok(personagens);
}
        [HttpGet("GetByClasse/{id}")]
        public IActionResult GetByClasse(int id)
        {
            ClasseEnum classeBuscada = (ClasseEnum)id;
            List<Personagem> listaFiltrada = personagens.FindAll(p => p.Classe == classeBuscada);

            return Ok(listaFiltrada);
        }
        






    }
}