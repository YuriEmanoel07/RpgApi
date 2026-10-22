using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpgApi.models
{
    public class Habilidade
    {
        public int Id {get; set ;}
        public string Nome { get; set;} = string.Empty;
        public  int Dano { get; set;}
    }
}