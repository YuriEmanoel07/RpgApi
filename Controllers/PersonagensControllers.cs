using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.models;
using RpgApi.models.Enuns;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]

    
    public class PersonagensController : ControllerBase
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
       private readonly DataContext _context;
       public PersonagensController(DataContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Personagem p = await _context.TB_PERSONAGENS
                .Include(ar => ar.Armas) //Carrega  a propriedade Arma do objeto p
                .Include(us => us.Usuario) //Inclui na propriedade Usuario do objeto p
                .Include(ph => ph.PersonagemHabilidades)
                    .ThenInclude(h => h.Habilidade) //Carrega a lista de PersonagemHabilidade de p
                    .FirstOrDefaultAsync(pBusca => pBusca.Id == id);
                return Ok(p);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Personagem> lista = await _context.TB_PERSONAGENS.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(Personagem novoPersonagem)
        {
            try
            {
                if (novoPersonagem.PontosVida > 100)
                {
                    throw new Exception("Pontos de vida não pode ser maior que 100");
                }
                await _context.TB_PERSONAGENS.AddAsync(novoPersonagem);
                await _context.SaveChangesAsync();
                return Ok(novoPersonagem.Id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(Personagem novoPersonagem)
        {
            try
            {
                if (novoPersonagem.PontosVida > 100)
                {
                    throw new System.Exception("Pontos de vida não pode ser maior que 100");
                }

                _context.TB_PERSONAGENS.Update(novoPersonagem);
                int linhasAfetadas = await _context.SaveChangesAsync();
                return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Personagem pRemover = await _context.TB_PERSONAGENS
                    .FirstOrDefaultAsync(p => p.Id == id);
                _context.TB_PERSONAGENS.Remove(pRemover);
                int linhasAfetadas = await _context.SaveChangesAsync();
                return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("RestaurarPontosVida")]
            public async Task<IActionResult> RestaurarPontosVidaAsync(Personagem p)
            {
            try
            {
            int linhasAfetadas = 0;
            Personagem? pEncontrado =
            await _context.TB_PERSONAGENS.FirstOrDefaultAsync(pBusca => pBusca.Id == p.Id);
            pEncontrado.PontosVida = 100;
            bool atualizou = await TryUpdateModelAsync<Personagem>(pEncontrado, "p",
            pAtualizar => pAtualizar.PontosVida);
            // EF vai detectar e atualizar apenas as colunas que foram alteradas.
            if (atualizou)
            linhasAfetadas = await _context.SaveChangesAsync();
            return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
            return BadRequest(ex.Message);
            }
            }

            //Método para alteração da foto
            [HttpPut("AtualizarFoto")]
            public async Task<IActionResult> AtualizarFotoAsync(Personagem p)
            {
            try
            {
            Personagem personagem = await _context.TB_PERSONAGENS
            .FirstOrDefaultAsync(x => x.Id == p.Id);
            personagem.FotoPersonagem = p.FotoPersonagem;
            var attach = _context.Attach(personagem);
            attach.Property(x => x.Id).IsModified = false;
            attach.Property(x => x.FotoPersonagem).IsModified = true;
            int linhasAfetadas = await _context.SaveChangesAsync();
            return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            { return BadRequest(ex.Message);}}

            [HttpPut("ZerarRanking")]
            public async Task<IActionResult> ZerarRankingAsync(Personagem p)
            {
            try
            {
            Personagem pEncontrado =
            await _context.TB_PERSONAGENS.FirstOrDefaultAsync(pBusca => pBusca.Id == p.Id);
            pEncontrado.Disputas = 0;
            pEncontrado.Vitorias = 0;
            pEncontrado.Derrotas = 0;
            int linhasAfetadas = 0;
            bool atualizou = await TryUpdateModelAsync<Personagem>(pEncontrado, "p",
            pAtualizar => pAtualizar.Disputas,
            pAtualizar => pAtualizar.Vitorias,
            pAtualizar => pAtualizar.Derrotas);
            // EF vai detectar e atualizar apenas as colunas que foram alteradas.
            if (atualizou)
            linhasAfetadas = await _context.SaveChangesAsync();
            return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
            return BadRequest(ex.Message);
            }
            } 

             [HttpPut("ZerarRankingRestaurarVidas")]
            public async Task<IActionResult> ZerarRankingRestaurarVidasAsync()
            {
            try
            {
            List<Personagem> lista =
            await _context.TB_PERSONAGENS.ToListAsync();
            foreach (Personagem p in lista)
            {
            await ZerarRankingAsync(p);
            await RestaurarPontosVidaAsync(p);
            }
            return Ok();
            }
            catch (System.Exception ex)
            {
            return BadRequest(ex.Message);
            }
            } 


            [HttpGet("GetByUser/{userId}")]
            public async Task<IActionResult> GetByUserAsync(int userId)
            {
            try
            {
            List<Personagem> lista = await _context.TB_PERSONAGENS
            .Where(u => u.Usuario.Id == userId)
            .ToListAsync();
            return Ok(lista);
            }
            catch (System.Exception ex)
            {
            return BadRequest(ex.Message);
            }
            }

            [HttpGet("GetByNomeAproximado/{nomePersonagem}")] //busca por nome aproximado
            public async Task<IActionResult> GetByNomeAproximado(string nomePersonagem)
            {
            try
            {
            List<Personagem> lista = await _context.TB_PERSONAGENS
            .Where(p => p.Nome.ToLower().Contains(nomePersonagem.ToLower()))
            .ToListAsync();
            return Ok(lista);
            }
            catch (System.Exception ex)
            {
            return BadRequest(ex.Message);
            }
            } 
    }
}


