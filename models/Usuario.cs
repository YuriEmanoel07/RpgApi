using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RpgApi.models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? Foto { get; set; }
        public DateTime? DataAcesso { get; set; }

        [NotMapped] // Para não guardar essa informação no Banco de Dados
        public string PasswordString { get; set; } = string.Empty;
        public string? Perfil { get; set; }
        public string? Email { get; set; }
        public List<Personagem> Personagem { get; set; }
        =new List<Personagem>();
    }
}