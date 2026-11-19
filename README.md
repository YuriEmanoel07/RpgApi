# ⚔️ RpgApi — Guilda de Aventureiros

Uma API desenvolvida em C# para simular o sistema de uma guilda de RPG. O projeto serve para praticar conceitos de backend e construir experiência para oportunidades de estágio em Desenvolvimento de Sistemas.

## 🧰 Tecnologias

- C# e .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Postman
- Git e GitHub

## 🏰 Como funciona a guilda

- 🧙 **Personagens:** representam os aventureiros, com atributos como pontos de vida, força, defesa, inteligência e classe.
- ⚔️ **Armas:** cada personagem pode ter uma arma. Esse relacionamento é **um para um (1:1)**.
- ✨ **Habilidades:** personagens podem possuir várias habilidades, e uma habilidade pode pertencer a vários personagens. O relacionamento **muitos para muitos (N:N)** usa a tabela `TB_PERSONAGENS_HABILIDADES` e uma chave composta.
- 👤 **Usuários:** podem se registrar e autenticar. As senhas são protegidas com hash e salt usando HMACSHA512.
- 🛡️ **Disputas:** personagens podem lutar usando armas ou habilidades. As disputas registram narrações, resultados e atualizações dos pontos de vida.
- 🏆 **Disputa coletiva:** vários personagens participam de rodadas de combate até restar um campeão.

## 📂 Estrutura do projeto

```text
RpgApi/
├── Controllers/
│   ├── ArmasController.cs
│   ├── DisputasController.cs
│   ├── PersonagemHabilidadesController.cs
│   ├── PersonagensControllers.cs
│   ├── PersonagensExemploController.cs
│   ├── PersonagensExercicioController.cs
│   └── UsuariosController.cs
├── Data/
│   └── DataContext.cs
├── Migrations/
├── models/
│   ├── Armas.cs
│   ├── Disputa.cs
│   ├── Habilidade.cs
│   ├── Personagem.cs
│   ├── PersonagemHabilidade.cs
│   ├── Usuario.cs
│   └── Enuns/
│       └── ClasseEnum.cs
├── Utils/
│   └── Criptografia.cs
├── Script06_TabelaDisputas.sql
├── script01_TabelaPersonagens.sql
├── script02_TabelaArmas.sql
├── script03_TabelaUsuarios.sql
├── script04_TabelaArmas_Atualizacao.sql
├── script05_TabelaHabilidades_TabelaPersonagemHabilidades.sql
├── Program.cs
└── RpgApi.csproj
```

## 🗄️ Banco de dados

A classe `DataContext` configura os conjuntos de dados do Entity Framework Core:

- `TB_PERSONAGENS`
- `TB_ARMAS`
- `TB_USUARIOS`
- `TB_HABILIDADES`
- `TB_PERSONAGENS_HABILIDADES`
- `TB_DISPUTAS`

O contexto também configura relacionamentos, chaves, nomes de colunas e dados iniciais para o projeto.

A classe `Disputa` contém informações persistidas, como data, IDs do atacante e do oponente e narração. As propriedades `HabilidadeId`, `ListaIdPersonagens` e `Resultados` usam `[NotMapped]`: são usadas durante as requisições e não viram colunas da tabela.

## 🚀 Como executar

1. Instale o SDK do .NET 10 e tenha acesso a um SQL Server.
2. Configure a connection string `ConexaoSomee`, usada pelo projeto para acessar o banco. Não publique senhas ou outros dados privados no GitHub.
3. Abra o terminal na pasta que contém `RpgApi.csproj` e execute:

```powershell
dotnet restore
dotnet run
```

4. Veja no terminal a mensagem `Now listening on` e use o endereço indicado como base no Postman.
5. Para enviar uma requisição JSON no Postman, selecione **Body → raw → JSON** e confirme o cabeçalho `Content-Type: application/json`.

## 🧱 Migrations e scripts SQL

Para listar as migrations e atualizar o banco usando o Entity Framework Core:

```powershell
dotnet ef migrations list
dotnet ef database update
```

Também é possível gerar o SQL entre duas migrations para revisar ou executar manualmente no SQL Server:

```powershell
dotnet ef migrations script MigracaoMuitosParaMuitos MigracaoDisputas -o ./Script06_TabelaDisputas.sql
```

Antes de executar um script manualmente, confira se o SQL Server Management Studio está conectado ao banco correto. O script gerado inclui as alterações definidas pela migration; ele não exporta automaticamente todos os registros existentes no banco.

## 🧭 Rotas principais

As controllers usam `[Route("[controller]")]`. Por exemplo, as rotas da `DisputasController` começam com `/Disputas`.

| Controller | Exemplos de rotas |
|---|---|
| `DisputasController` | `POST /Disputas/Arma`, `POST /Disputas/Habilidade`, `POST /Disputas/DisputaEmGrupo`, `GET /Disputas/PersonagemRandom`, `GET /Disputas/Listar`, `DELETE /Disputas/ApagarDisputas` |
| `PersonagensController` | `GET /Personagens/GetAll`, `GET /Personagens/{id}`, `POST /Personagens`, `PUT /Personagens`, `DELETE /Personagens/{id}` |
| `ArmasController` | `GET /Armas/GetAll`, `GET /Armas/{id}`, `POST /Armas`, `PUT /Armas`, `DELETE /Armas/{id}` |
| `PersonagemHabilidadesController` | `POST /PersonagemHabilidades`, `GET /PersonagemHabilidades/{personagemId}`, `GET /PersonagemHabilidades/GetHabilidades` |
| `UsuariosController` | Cadastro, autenticação, alteração de senha e consulta de usuários |

As controllers de personagens e usuários também têm rotas complementares. Consulte os atributos `[HttpGet]`, `[HttpPost]`, `[HttpPut]` e `[HttpDelete]` no código para ver os caminhos e parâmetros disponíveis.

### 📬 Exemplo de requisição: disputa coletiva

Troque os IDs por personagens existentes no seu banco:

```json
{
  "ListaIdPersonagens": [1, 2, 3],
  "Resultados": []
}
```

Envie o JSON para `POST /Disputas/DisputaEmGrupo`.


## 🎯 Objetivo

Praticar programação backend com um projeto em evolução, aprender a explicar decisões técnicas e se preparar para entrevistas de estágio em Desenvolvimento de Sistemas.