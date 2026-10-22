# ⚔️ RpgApi - Guilda de Aventureiros em Web API

Projeto desenvolvido para entender como funciona o backend de uma Web API em C# (.NET) utilizando o universo de RPG como temática central.

## 🛡️ A Lógica da Guilda (Arquitetura)

Para entender a API, imagine que ela é a Guilda dos Aventureiros:

* **Ficha do Aventureiro (`Models/Personagem.cs`):** Define quais atributos todo herói precisa ter para ser registrado (Nome, Pontos de Vida, Força, Defesa, Inteligência e Classe).
* **Livro de Classes (`Models/Enums/ClasseEnum.cs`):** Um catálogo fechado de profissões permitidas na guilda (1 = Cavaleiro, 2 = Mago, 3 = Clerigo).
* **Mestre da Guilda / Atendente (`Controllers/PersonagensController.cs`):** Controla os pedidos do mundo exterior e devolve informações em JSON.
* **Arsenal da Guilda (`Models/Arma.cs` - Relacionamento 1:1):** Toda arma pertence obrigatoriamente a um personagem (`PersonagemId`) e cada personagem possui no máximo uma arma.
* **Livro de Habilidades (`Models/Habilidade.cs` & `PersonagemHabilidade.cs` - Relacionamento N:N):** Tabela de junção que permite que um herói aprenda várias habilidades e uma habilidade pertença a vários heróis.
* **Cartório da Guilda (`Utils/Criptografia.cs`):** Utilitário de segurança responsável por gerar Hash e Salt HMACSHA512 para a proteção de senhas dos usuários.
## 📂 Estrutura das Pastas

```text
RpgApi/
├── Controllers/
│   ├── ArmasController.cs
│   ├── PersonagensController.cs
│   └── UsuariosController.cs
├── Data/
│   └── DataContext.cs                  # Mapeamento do EF Core, Seed Data e Foreign Keys
├── Migrations/                         # Histórico de Migrações do Entity Framework
├── Models/
│   ├── Arma.cs                         # Modelo 1:1 com Personagem
│   ├── Habilidade.cs                   # Modelo N:N com Personagem
│   ├── Personagem.cs                   # Modelo principal do herói
│   ├── PersonagemHabilidade.cs          # Tabela de junção (Chave composta)
│   ├── Usuario.cs                      # Modelo com autenticação Hash/Salt
│   └── Enums/
│       └── ClasseEnum.cs               # Restrições de classe do herói
├── Utils/
│   └── Criptografia.cs                 # Métodos estáticos CriarPasswordHash
├── script03_TabelaUsuarios.sql         # Script para deploy no Somee
├── script04_MuitosParaMuitos.sql       # Script de atualização da Aula 11
└── Program.cs                          # Configurações de serviços e injeção de dependência
Para facilitar a cópia sem o texto quebrar ou ficar truncado pela interface, dividi o conteúdo do `README.md` em duas caixas de texto limpas.

Copie a **Parte 1**, cole no editor e em seguida copie a **Parte 2** colando logo abaixo.

---

### Parte 1 (Introdução e Arquitetura)

```markdown
# ⚔️ RpgApi - Guilda de Aventureiros em Web API

Projeto desenvolvido para entender como funciona o backend de uma Web API em C# (.NET) utilizando o universo de RPG como temática central.

## 🛡️ A Lógica da Guilda (Arquitetura)

Para entender a API, imagine que ela é a Guilda dos Aventureiros:

* **Ficha do Aventureiro (`Models/Personagem.cs`):** Define quais atributos todo herói precisa ter para ser registrado (Nome, Pontos de Vida, Força, Defesa, Inteligência e Classe).
* **Livro de Classes (`Models/Enums/ClasseEnum.cs`):** Um catálogo fechado de profissões permitidas na guilda (1 = Cavaleiro, 2 = Mago, 3 = Clerigo).
* **Mestre da Guilda / Atendente (`Controllers/PersonagensController.cs`):** Controla os pedidos do mundo exterior e devolve informações em JSON.
* **Arsenal da Guilda (`Models/Arma.cs` - Relacionamento 1:1):** Toda arma pertence obrigatoriamente a um personagem (`PersonagemId`) e cada personagem possui no máximo uma arma.
* **Livro de Habilidades (`Models/Habilidade.cs` & `PersonagemHabilidade.cs` - Relacionamento N:N):** Tabela de junção que permite que um herói aprenda várias habilidades e uma habilidade pertença a vários heróis.
* **Cartório da Guilda (`Utils/Criptografia.cs`):** Utilitário de segurança responsável por gerar Hash e Salt HMACSHA512 para a proteção de senhas dos usuários.

```

---

### Parte 2 (Estrutura de Pastas)

```markdown
## 📂 Estrutura das Pastas

```text
RpgApi/
├── Controllers/
│   ├── ArmasController.cs
│   ├── PersonagensController.cs
│   └── UsuariosController.cs
├── Data/
│   └── DataContext.cs                  # Mapeamento do EF Core, Seed Data e Foreign Keys
├── Migrations/                         # Histórico de Migrações do Entity Framework
├── Models/
│   ├── Arma.cs                         # Modelo 1:1 com Personagem
│   ├── Habilidade.cs                   # Modelo N:N com Personagem
│   ├── Personagem.cs                   # Modelo principal do herói
│   ├── PersonagemHabilidade.cs          # Tabela de junção (Chave composta)
│   ├── Usuario.cs                      # Modelo com autenticação Hash/Salt
│   └── Enums/
│       └── ClasseEnum.cs               # Restrições de classe do herói
├── Utils/
│   └── Criptografia.cs                 # Métodos estáticos CriarPasswordHash
├── script03_TabelaUsuarios.sql         # Script para deploy no Somee
├── script04_MuitosParaMuitos.sql       # Script de atualização da Aula 11
└── Program.cs                          # Configurações de serviços e injeção de dependência

```

