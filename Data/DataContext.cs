using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client;
using RpgApi.models;
using RpgApi.models.Enuns;
using RpgApi.Utils;

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
         public DbSet<Usuario> TB_USUARIOS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personagem>().ToTable("TB_PERSONAGENS");
            modelBuilder.Entity<Armas>().ToTable("TB_ARMAS");
            modelBuilder.Entity<Usuario>().ToTable("TB_USUARIOS");

            //Relacionando One to Many (um para muitos)
            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.Personagem)
                .WithOne(e => e.Usuario)
                .HasForeignKey(e => e.UsuarioId)
                .IsRequired(false);

            modelBuilder.Entity<Personagem>().HasData
                (
            new Personagem() { Id = 1, Nome = "Frodo", PontosVida=100, Forca=17, Defesa=23, Inteligencia=33, Classe=ClasseEnum.Cavaleiro,UsuarioId=1},
            new Personagem() { Id = 2, Nome = "Sam", PontosVida=100, Forca=15, Defesa=25, Inteligencia=30, Classe=ClasseEnum.Cavaleiro,UsuarioId=1},
            new Personagem() { Id = 3, Nome = "Galadriel", PontosVida=100, Forca=18, Defesa=21, Inteligencia=35, Classe=ClasseEnum.Clerigo,UsuarioId=1 },
            new Personagem() { Id = 4, Nome = "Gandalf", PontosVida=100, Forca=18, Defesa=18, Inteligencia=37, Classe=ClasseEnum.Mago,UsuarioId=1 },
            new Personagem() { Id = 5, Nome = "Hobbit", PontosVida=100, Forca=20, Defesa=17, Inteligencia=31, Classe=ClasseEnum.Cavaleiro,UsuarioId=1  },
            new Personagem() { Id = 6, Nome = "Celeborn", PontosVida=100, Forca=21, Defesa=13, Inteligencia=34, Classe=ClasseEnum.Clerigo,UsuarioId=1  },
            new Personagem() { Id = 7, Nome = "Radagast", PontosVida=100, Forca=25, Defesa=11, Inteligencia=35, Classe=ClasseEnum.Mago,UsuarioId=1  }
        
                );

           
           

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
                //Inicio da criação do usuario padrão.
                Usuario user = new Usuario();
                Criptografia.CriarPasswordHash("123456", out byte[] hash, out byte[]salt);
                user.Id = 1;
                user.Username = "UsuarioAdmin";
                user.PasswordString = string.Empty;
                user.PasswordHash = hash;
                user.PasswordSalt = salt;
                user.Perfil = "Admin";
                user.Email = "seuEmail@gmail.com";

                modelBuilder.Entity<Usuario>().HasData(user);
                //Fim da criação do usuario padrão

                //define que se o Perfil não for informado, o valor padrão será jogador
                modelBuilder.Entity<Usuario>().Property(u => u.Perfil).HasDefaultValue("Jogador");

        }// Fim do método OnModelCreating

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings
            .Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        

        
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("varchar").HaveMaxLength(200);      
            
        }








    }
}