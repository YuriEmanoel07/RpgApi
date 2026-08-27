# ⚔️ RpgApi - Guilda de Aventureiros em Web API

Projeto desenvolvido para entender como funciona o backend de uma **Web API em C# (.NET)** utilizando o universo de RPG como temática central.

---

## 🛡️ A Lógica da Guilda (Arquitetura)

Para entender a API, imagine que ela é a **Guilda dos Aventureiros**:

* **Ficha do Aventureiro (`models/Personagem.cs`):** 
  Define quais atributos todo herói precisa ter para ser registrado (Nome, Pontos de Vida, Força, Defesa, Inteligência e Classe).
* **Livro de Classes (`models/Enums/ClasseEnum.cs`):** 
  Um catálogo fechado de profissões permitidas na guilda (`1 = Cavaleiro`, `2 = Mago`, `3 = Clerigo`). Garante que nenhum herói com classe inválida seja registrado.
* **Mestre da Guilda / Atendente (`Controllers/PersonagensExemploController.cs`):** 
  É o controlador responsável por receber os pedidos do mundo exterior, consultar os dados dos heróis e devolver as informações organizadas em formato **JSON**.

---

## 📂 Estrutura das Pastas

```text
RpgApi/
├── Controllers/
│   └── PersonagensExemploController.cs  # Mestre da Guilda: Controla as requisições
├── models/
│   ├── Personagem.cs                    # Ficha: Modelo dos heróis
│   └── Enums/
│       └── ClasseEnum.cs               # Livro de Classes: Restrições de profissão
└── Program.cs                          # Configurações de inicialização da Guilda