⚔️ RpgApi - Guilda de Aventureiros em Web API

Projeto desenvolvido para entender como funciona o backend de uma Web API em C# (.NET) utilizando o Entity Framework Core e SQL Server, sob a temática de uma Guilda de RPG.

🛡️ A Lógica da Guilda (Arquitetura)

Para entender a API, imagine que ela é a Guilda dos Aventureiros:

Ficha do Aventureiro (Models/Personagem.cs): Define os atributos do herói (Nome, Pontos de Vida, Força, Defesa, Inteligência, Classe) e seu histórico de combate (Disputas, Vitórias e Derrotas).

Livro de Classes (Models/Enums/ClasseEnum.cs): Catálogo fechado de profissões permitidas na guilda (1 = Cavaleiro, 2 = Mago, 3 = Clerigo).

Selo de Proteção (Utils/Criptografia.cs): Mecanismo de segurança responsável pela geração de Hash e Salt (HMACSHA512) para proteger as senhas dos usuários.

Mestre da Guilda (Controllers/): Controladores responsáveis por gerenciar o fluxo de dados, validar regras de negócio e interagir com o banco através do DataContext.

🔗 Relacionamentos Mapeados (Entity Framework Core)

1. Um para Um (One-to-One): Personagem ⚔️ Arma

Cada Personagem possui no máximo uma Arma, e cada Arma pertence obrigatoriamente a um Personagem (PersonagemId).

Configurado com deleção em cascata (Cascade): se o aventureiro for removido, sua arma equipada também é deletada.

2. Um para Muitos (One-to-Many): Usuário 👤 Personagens

Um Usuário (conta) pode possuir múltiplos Personagens, mas cada personagem pertence a um único usuário.

3. Muitos para Muitos (Many-to-Many): Personagem 📜 Habilidade

Um Personagem pode aprender várias Habilidades, e uma Habilidade pode ser dominada por diversos personagens.

Implementado com a tabela de junção PersonagemHabilidade (TB_PERSONAGENS_HABILIDADES) utilizando Chave Primária Composta (PersonagemId + HabilidadeId).

📂 Estrutura das Pastas

RpgApi/
├── Controllers/
│   ├── ArmasController.cs            # Gestão do arsenal de armas
│   ├── PersonagensController.cs      # Gestão dos heróis no banco de dados
│   ├── PersonagensExemploController.cs # Testes em memória
│   └── UsuariosController.cs         # Autenticação e cadastro
├── Data/
│   └── DataContext.cs                # Mapeamento do EF Core, Fluent API e Data Seeding
├── Migrations/                       # Histórico de migrações do EF Core
├── Models/
│   ├── Arma.cs                       # Model de Arma (One-to-One com Personagem)
│   ├── Habilidade.cs                  # Model de Habilidade
│   ├── Personagem.cs                  # Model do Personagem
│   ├── PersonagemHabilidade.cs       # Tabela de junção (Many-to-Many)
│   ├── Usuario.cs                     # Model de Usuário/Autenticação
│   └── Enums/
│       └── ClasseEnum.cs             # Enum de classes
├── Utils/
│   └── Criptografia.cs               # Utilitário de Criptografia de Senhas
├── scripts/                          # Scripts SQL gerados para o banco Somee
└── Program.cs                        # Configurações de inicialização e Injeção de Dependência


🚀 Migrações do Projetos (Migrations Executadas)

InitialCreate: Criação inicial do banco de dados.

MigracaoArma: Mapeamento da tabela TB_ARMAS e Data Seeding inicial.

MigracaoUsuario: Criação da tabela TB_USUARIOS e criptografia de credenciais.

MigracaoUmParaUm: Relacionamento 1:1 entre Personagem e Arma.

MigracaoMuitosParaMuitos: Criação da tabela TB_HABILIDADES e da junção TB_PERSONAGENS_HABILIDADES.

🛠️ Tecnologias Utilizadas

C# / .NET 10

ASP.NET Core Web API

Entity Framework Core (EF Core)

SQL Server / Somee (Hospedagem de Banco Remoto)

Postman Web & Desktop Agent (Testes dos Endpoints)
