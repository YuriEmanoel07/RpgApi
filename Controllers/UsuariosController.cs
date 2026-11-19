using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.models;
using RpgApi.Utils;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context)
        {
            _context = context;
        }
        private async Task<bool> UsuarioExistente(string username)
        {
            if(await _context.TB_USUARIOS.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }
        [HttpPost("Registrar")]
        public async Task<ActionResult>Registrousuario(Usuario user)
        {
            try
            {
                if(await UsuarioExistente(user.Username))
                throw new System.Exception("Nome de usuario ja existe");

                Criptografia.CriarPasswordHash(user.PasswordString, out byte[] hash, out byte[] salt);
                user.PasswordString = string.Empty;
                user.PasswordHash = hash;
                user.PasswordSalt = salt;
                await _context.TB_USUARIOS.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(user.Id);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(Usuario credenciais)
        {
            try
            {
                Usuario? usuario = await _context.TB_USUARIOS
                .FirstOrDefaultAsync(x => x.Username.ToLower().Equals(credenciais.Username.ToLower()));

                if (usuario == null)
                {
                    throw new System.Exception("Usuario não encontrado");
                }
                else if (!Criptografia
                .VerificarPasswordHash(credenciais.PasswordString, usuario.PasswordHash, usuario.PasswordSalt))
                {
                    throw new System.Exception("Senha incorreta");
                }
                else
                {
                    usuario.DataAcesso = System.DateTime.Now;
                    _context.TB_USUARIOS.Update(usuario);
                    await _context.SaveChangesAsync(); //Confirma a alteração no banco
                    return Ok (usuario);
                }
            }
            catch (System.Exception ex)
            {
                
                return BadRequest (ex.Message);
            }
        }

        //Método para alteração de Senha    
        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenhaUsuario(Usuario credenciais)
        {
            try
            {
                Usuario? usuario = await _context.TB_USUARIOS //Busca o usuario no banco atraves do login
                    .FirstOrDefaultAsync( x => x.Username.ToLower().Equals(credenciais.Username.ToLower()));

                    if (usuario == null)// Se não achar nenhum usuario pelo login, retorna mensagem.
                    throw new System.Exception("Usuario não encontrado.");

                    Criptografia.CriarPasswordHash(credenciais.PasswordString, out byte[] hash, out byte[] salt);
                    usuario.PasswordHash = hash; //Se o usuario existir, executa a criptografia
                    usuario.PasswordSalt = salt; //guardando o hash e o salt nas propriedades do usuario

                    _context.TB_USUARIOS.Update(usuario);
                    int linhasAfetadas = await _context.SaveChangesAsync(); //Confirma a alteração no banco
                    return Ok(linhasAfetadas); //retorna as linhas afetadas (Geralmente sempre 1 linha msm)
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetUSuarios()
        {
            try
            {
                List<Usuario> lista = await _context.TB_USUARIOS.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}