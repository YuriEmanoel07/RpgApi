using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RpgApi.Data;
using RpgApi.models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DisputasController : ControllerBase
    {
     private readonly DataContext _context;
     public DisputasController(DataContext context)
        {
            _context = context;
        }   


    [HttpPost("Arma")]
    public async Task<IActionResult> AtaqueComArmaAsync (Disputa d){
    try
    {
        Personagem? atacante = await _context.TB_PERSONAGENS
              .Include(p => p.Armas)
             .FirstOrDefaultAsync(p => p.Id == d.AtacanteId);

        Personagem? oponente = await _context.TB_PERSONAGENS
            .FirstOrDefaultAsync(p => p.Id == d.OponenteId);
        

        int dano = atacante.Armas.Dano + (new Random().Next(atacante.Forca));

        dano = dano - new Random().Next(oponente.Defesa);

        if(dano > 0)
            oponente.PontosVida = oponente.PontosVida - (int)dano;
        if(oponente.PontosVida <= 0)
            d.Narracao = $"{oponente.Nome} foi derrotado!";
        _context.TB_PERSONAGENS.Update(oponente);
        await _context.SaveChangesAsync();

        StringBuilder dados = new StringBuilder();
        dados. AppendFormat(" Atacante: (0). ", atacante. Nome);
        dados. AppendFormat(" Oponente: {0}. ", oponente.Nome);
        dados. AppendFormat(" Pontos de vida do atacante: {0}. ", atacante.PontosVida);
        dados. AppendFormat(" Pontos de vida do oponente: {0}. ", oponente.PontosVida);
        dados.AppendFormat(" Arma Utilizada: (0). ", atacante.Armas.Nome);
        dados. AppendFormat(" Dano: (0). ", dano);

        d.Narracao += dados. ToString();
        d.DataDisputa = DateTime.Now;
        _context.TB_DISPUTAS.Add(d);
        _context.SaveChanges();

        return Ok(d);
    }
    catch (System.Exception ex)
    {
        
        return BadRequest(ex.Message);
    }
    }
    [HttpPost("Habilidade")]
    public async Task<IActionResult> AtaqueComHabilidadeAsync(Disputa d)
        {
            try
            {

                Personagem? atacante = await _context.TB_PERSONAGENS
                .Include(p => p.PersonagemHabilidades).ThenInclude (ph => ph.Habilidade)
                .FirstOrDefaultAsync(p => p.Id == d.AtacanteId);

                Personagem? oponente = await _context.TB_PERSONAGENS
                .FirstOrDefaultAsync(p => p.Id == d.OponenteId);

                PersonagemHabilidade? ph = await _context.TB_PERSONAGENS_HABILIDADES
                .Include(p => p.Habilidade).FirstOrDefaultAsync(phBusca => phBusca.HabilidadeId == d.HabilidadeId
                && phBusca.PersonagemId == d.AtacanteId);

                if (ph == null)
                    d.Narracao = $"{atacante.Nome} não possui esta habilidade";
                else
                {
                    int dano = ph.Habilidade.Dano + (new Random().Next(atacante.Inteligencia));
                    dano = dano - new Random().Next(oponente.Defesa);

                    if(dano > 0)
                    oponente.PontosVida = oponente.PontosVida - dano;

                    if(oponente.PontosVida <= 0)
                    d.Narracao += $"{oponente.Nome} foi derrotado!";
                
                _context.TB_PERSONAGENS.Update(oponente);
                await _context.SaveChangesAsync();

                StringBuilder dados = new StringBuilder();
                dados.AppendFormat("Atacante: {0}. ", atacante.Nome);
                dados.AppendFormat("Oponente: {0}. ", oponente.Nome);
                dados.AppendFormat("Pontos de vida do atacante: {0}. ", atacante.PontosVida);
                dados.AppendFormat("Pontos de vida do oponente: {0}. ", oponente.PontosVida);
                dados.AppendFormat("Habildade Utilizada: {0}. ", ph.Habilidade.Nome);
                dados.AppendFormat("Dano: {0}. ", dano);   

                d.Narracao += dados.ToString();
                d.DataDisputa = DateTime.Now;
                _context.TB_DISPUTAS.Add(d);
                _context.SaveChanges();
                }
                return Ok(d);
            }
            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
            [HttpGet("PersonagemRandom")]
            public async Task<IActionResult> Sorteio()
            {
            List<Personagem> personagens =
            await _context.TB_PERSONAGENS.ToListAsync();
            // Sorteio com numero da quantidade de personagens
            int sorteio = new Random(). Next (personagens.Count);
            // busca na lista pelo indice sorteado (Não é o ID)
            Personagem p = personagens [sorteio];
            string msg =
            string.Format("Nº Sorteado (0). Personagem: (1)", sorteio, p.Nome);
            return Ok(msg);
            }
          [HttpPost("DisputaEmGrupo")]
            public async Task<IActionResult> DisputaEmGrupoAsync(Disputa d)
            {
                try
                {
                    d.Resultados = new List<string>(); // Instancia a lista de resultados

                    // Busca no banco os personagens informados no parâmetro,
                    // incluindo armas e habilidades
                    List<Personagem> personagens = await _context.TB_PERSONAGENS
                        .Include(p => p.Armas)
                        .Include(p => p.PersonagemHabilidades)
                            .ThenInclude(ph => ph.Habilidade)
                        .Where(p => d.ListaIdPersonagens.Contains(p.Id))
                        .ToListAsync();

                    // Conta os personagens vivos na lista obtida do banco
                    int qtdPersonagensVivos =
                        personagens.FindAll(p => p.PontosVida > 0).Count;

                    // Enquanto houver mais de um personagem vivo, haverá disputa
                    while (qtdPersonagensVivos > 1)
                    {
                        // Seleciona personagens com pontos de vida positivos e sorteia um atacante.
                        List<Personagem> atacantes = personagens
                            .Where(p => p.PontosVida > 0)
                            .ToList();

                        Personagem atacante = atacantes[new Random().Next(atacantes.Count)];
                        d.AtacanteId = atacante.Id;

                        // Seleciona personagens vivos, exceto o atacante, e sorteia um oponente.
                        List<Personagem> oponentes = personagens
                            .Where(p => p.Id != atacante.Id && p.PontosVida > 0)
                            .ToList();

                        Personagem oponente = oponentes[new Random().Next(oponentes.Count)];
                        d.OponenteId = oponente.Id;

                        // Reinicia estas variáveis a cada rodada.
                        int dano = 0;
                        string ataqueUsado = string.Empty;
                        string resultado = string.Empty;

                        // Sorteia entre 0 e 1: 0 tenta ataque com arma; 1 tenta ataque com habilidade.
                        // Next(2) gera 0 ou 1.
                        bool ataqueUsaArma = new Random().Next(2) == 0;
                        // Ataque com arma, caso o atacante possua uma.
                        if (ataqueUsaArma && atacante.Armas != null)
                        {
                            // Sorteia a força do atacante e subtrai a defesa do oponente.
                            dano = atacante.Armas.Dano + new Random().Next(atacante.Forca);
                            dano -= new Random().Next(oponente.Defesa);

                            ataqueUsado = atacante.Armas.Nome;

                            if (dano > 0)
                                oponente.PontosVida -= dano;

                            // Monta a mensagem do ataque.
                            resultado = string.Format(
                                "{0} atacou {1} usando {2} com dano {3}.",
                                atacante.Nome,
                                oponente.Nome,
                                ataqueUsado,
                                dano);

                            d.Narracao += resultado; // Acrescenta à narração existente.
                            d.Resultados.Add(resultado); // Guarda o resultado na lista.
                        }
                       else if (atacante.PersonagemHabilidades.Count != 0) // Verifica se o personagem tem habilidades
                    {
                        // Programa o ataque com habilidade.

                        // Sorteia uma das habilidades do atacante.
                        int sorteioHabilidadeId =
                            new Random().Next(atacante.PersonagemHabilidades.Count);

                        Habilidade habilidadeEscolhida =
                            atacante.PersonagemHabilidades[sorteioHabilidadeId].Habilidade;

                        ataqueUsado = habilidadeEscolhida.Nome;

                        // Soma o dano da habilidade a um valor aleatório baseado na inteligência
                        // e subtrai um valor aleatório baseado na defesa do oponente.
                        dano = habilidadeEscolhida.Dano
                            + new Random().Next(atacante.Inteligencia);

                        dano -= new Random().Next(oponente.Defesa);

                        if (dano > 0)
                            oponente.PontosVida -= dano;    

                        // Monta e registra a narração do ataque.
                        resultado = string.Format(
                            "{0} atacou {1} usando {2} com o dano {3}.",
                            atacante.Nome,
                            oponente.Nome,
                            ataqueUsado,
                            dano);

                        d.Narracao += resultado;
                        d.Resultados.Add(resultado);
                    }
                        // Só registra a disputa se algum ataque foi realizado.
                        if (!string.IsNullOrEmpty(ataqueUsado))
                        {
                            // Incrementa os dados dos combates.
                            atacante.Vitorias++;
                            oponente.Derrotas++;
                            atacante.Disputas++;
                            oponente.Disputas++;

                            d.Id = 0; // Zera o ID para salvar uma nova disputa.
                            d.DataDisputa = DateTime.Now;
                            _context.TB_DISPUTAS.Add(d);
                            await _context.SaveChangesAsync();
                        }

                        // Atualiza a quantidade de personagens ainda vivos.
                        qtdPersonagensVivos =
                            personagens.FindAll(p => p.PontosVida > 0).Count;

                        // Se restou apenas um personagem, ele é o campeão.
                        if (qtdPersonagensVivos == 1)
                        {
                            string resultadoFinal =
                                $"{atacante.Nome.ToUpper()} é CAMPEÃO com " +
                                $"{atacante.PontosVida} pontos de vida restantes!";

                            d.Narracao += resultadoFinal;
                            d.Resultados.Add(resultadoFinal);

                            break; // Sai do while.
                        }
                    }

                    // Atualiza os pontos de vida, disputas, vitórias e derrotas
                    // dos personagens ao final das batalhas
                    _context.TB_PERSONAGENS.UpdateRange(personagens);
                    await _context.SaveChangesAsync();

                    return Ok(d); // Retorna os dados da disputa
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        [HttpDelete("ApagarDisputas")]
            public async Task<IActionResult> DeleteAsync()
            {
            try
            {
            List<Disputa> disputas = await _context.TB_DISPUTAS.ToListAsync();
            _context.TB_DISPUTAS.RemoveRange(disputas);
            await _context.SaveChangesAsync();
            return Ok("Disputas apagadas");
            }
            catch (System.Exception ex)
            { return BadRequest(ex.Message); }
            } 

            [HttpGet("Listar")]
            public async Task<IActionResult> ListarAsync()
            {
            try
            {
            List<Disputa> disputas =
            await _context.TB_DISPUTAS.ToListAsync();
            return Ok(disputas);
            }
            catch (System.Exception ex)
            {
            return BadRequest(ex.Message);
            }
            } 

            
    }    
}