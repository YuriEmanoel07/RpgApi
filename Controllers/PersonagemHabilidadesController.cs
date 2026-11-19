using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonagemHabilidadesController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonagemHabilidadesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonagemHabilidadeAsync(PersonagemHabilidade novoPersonagemHabilidade)
        {
            try
            {
                Personagem personagem = await _context.TB_PERSONAGENS
                .Include(p => p.Armas)
                .Include(p => p.PersonagemHabilidades).ThenInclude(ps => ps.Habilidade)
                .FirstOrDefaultAsync(p => p.Id == novoPersonagemHabilidade.PersonagemId);

            if (personagem == null)
                throw new System.Exception("Personagem não encontrado para o Id Informado.");

            Habilidade habilidade = await _context.TB_HABILIDADES
                .FirstOrDefaultAsync(h => h.Id == novoPersonagemHabilidade.HabilidadeId);

            if (habilidade == null)
                throw new System.Exception("Habilidade não encontrada.");

            PersonagemHabilidade ph = new PersonagemHabilidade();
            ph.Personagem = personagem;
            ph.Habilidade = habilidade;
            await _context.TB_PERSONAGENS_HABILIDADES.AddAsync(ph);
            int linhasAfetadas = await _context.SaveChangesAsync();

            return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{personagemId}")] //Busca da listagem de PersonagemHabilidades de um determinado personagem passando o id
        public async Task<IActionResult> GetHabilidadesPersonagem(int personagemId)
        {
                try
                {
                    List<PersonagemHabilidade> phLista = new List<PersonagemHabilidade>();
                    phLista = await _context.TB_PERSONAGENS_HABILIDADES
                    .Include(p => p.Personagem)
                    .Include(p => p.Habilidade)
                    .Where(p => p.Personagem.Id == personagemId)
                    .ToListAsync();
                    return Ok(phLista);
                }
                catch (System.Exception ex)
                {
                    
                    return BadRequest(ex.Message);
                }
        }
        [HttpGet("GetHabilidades")] //Busca de todas as habilidades cadastradas na tabela de Habilidades através da controller

        public async Task<IActionResult> GetHabilidades()
        {
            try
            {
                List<Habilidade> habilidades = new List<Habilidade>();
                habilidades = await _context.TB_HABILIDADES.ToListAsync();
                return Ok(habilidades);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("DeletePersonagemHabilidade")] //Remoção da habilidade do personagem em PersonagensController. A busca é feita através do Id do
//personagem e do Id da Habilidade presente no objeto ph. 
        public async Task<IActionResult> DeleteAsync(PersonagemHabilidade ph)
        {
            try
            {
                PersonagemHabilidade? phRemover = await _context.TB_PERSONAGENS_HABILIDADES
                .FirstOrDefaultAsync(phBusca => phBusca.PersonagemId == ph.PersonagemId
                && phBusca.HabilidadeId == ph.HabilidadeId);
                if (phRemover == null)
                throw new System.Exception("Personagem ou Habilidade não encontrados");

                _context.TB_PERSONAGENS_HABILIDADES.Remove(phRemover);
                int linhasAfetadas = await _context.SaveChangesAsync();
                return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
    }
}