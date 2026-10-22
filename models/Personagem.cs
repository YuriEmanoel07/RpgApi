using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using RpgApi.models.Enuns;

namespace RpgApi.models
{
    public class Personagem
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public int PontosVida { get; set; }
        public int Forca { get; set; }
        public int Defesa { get; set; }
        public int Inteligencia { get; set; }
        public ClasseEnum Classe { get; set; }
        public int? UsuarioId { get; set; }
        public byte[]? FotoPersonagem { get; set; }
        [JsonIgnore]
        public Usuario? Usuario { get; set; }
        [JsonIgnore]
        public Armas? Armas { get; set; }
        public int Disputas { get; set; }
        public int Vitorias { get; set; }
        public int Derrotas { get; set; }     
        public List<PersonagemHabilidade> PersonagemHabilidades { get; set;} = [];  
    }
}