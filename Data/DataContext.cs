using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RpgApi.models;
using RpgApi.models.Enuns;

namespace RpgApi.Data
{
    public class DataContext : DbContext
    {
        //ctor ----> Cria um construtor (ATALHO)
        public DataContext(DbContextOptions<DataContext>options) : base(options)
        {
            
        }

        public DbSet<Personagem> TB_PERSONAGENS { get; set; }
         public DbSet<Armas> TB_ARMAS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personagem>().ToTable("TB_PERSONAGENS");

            modelBuilder.Entity<Personagem>().HasData
                (
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago }
        
                );

           
            modelBuilder.Entity<Armas>().ToTable("TB_ARMAS");

            modelBuilder.Entity<Armas>().HasData
                (
                    
                    new Armas(){ Id = 1, Nome = "Espada longa", Dano = 30 },
                    new Armas(){ Id = 2, Nome = "Machado de Guerra", Dano = 35 },
                    new Armas(){ Id = 3, Nome = "Arco longo", Dano = 25 },
                    new Armas(){ Id = 4, Nome = "Cajado Magico", Dano = 20 },
                    new Armas(){ Id = 5, Nome = "Adaga Obscura", Dano = 18 },
                    new Armas(){ Id = 6, Nome = "Martelo Trovao", Dano = 40 },
                    new Armas(){ Id = 7, Nome = "Lança Real", Dano = 28 }
                    
        
        
        
                );
               

        
        }

        

        
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("varchar").HaveMaxLength(200);      
            
        }








    }
}