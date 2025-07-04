# VisionsHub

**VisionsHub** é um sistema de gerenciamento de biblioteca desenvolvido com **.NET 8**, utilizando arquitetura em camadas. O foco principal é o controle eficiente de empréstimos de livros, gerenciamento de alunos e geração de relatórios detalhados.

---

## 🚀 Funcionalidades Principais

- 📚 Cadastro e consulta de livros  
- 👥 Gestão de alunos  
- 🔄 Empréstimo e devolução de livros  
- ⚙️ Regras de negócio:
  - Aluno pode ter no máximo **3 empréstimos simultâneos**
  - Alunos **inativos** não podem realizar empréstimos
  - Controle de **disponibilidade de exemplares**
- 📊 Relatórios:
  - **Livros mais emprestados** (com dados do primeiro, último e maior tomador de empréstimos)
  - **Alunos com empréstimos em atraso**
  - **Histórico de empréstimos** por período

---

## 🧱 Estrutura do Projeto

| Projeto                | Descrição                                                        |
|------------------------|------------------------------------------------------------------|
| `VisionsHub.Domain`    | Entidades de domínio (`Book`, `Student`, `Loan`, etc.)           |
| `VisionsHub.Infra`     | Repositórios e contexto de dados com **Entity Framework Core**   |
| `VisionsHub.Application` | Serviços de aplicação, DTOs, interfaces e regras de negócio   |
| `VisionsHub.API`       | **Controllers** e **endpoints RESTful**                          |

---

## 🛠️ Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/en-us/download)
- [Entity Framework Core](https://learn.microsoft.com/ef/)
- [ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/web-api/)
- SQL Server (padrão, mas adaptável para outros bancos)

---

## ▶️ Como Executar

1. Clone o repositório:

    ```bash
    git clone https://github.com/seu-usuario/visionshub.git
    ```

2. Configure a string de conexão:

    No arquivo `appsettings.json`, ajuste a string de conexão com seu **SQL Server**.

3. Execute o projeto normalmente pelo Visual Studio ou via CLI:

    ```bash
    dotnet run --project VisionsHub.API
    ```

---

## 📡 Exemplos de Endpoints

| Método | Rota                                              | Descrição                                      |
|--------|---------------------------------------------------|-----------------------------------------------|
| GET    | `/api/books`                                      | Lista livros                                   |
| POST   | `/api/books`                                      | Cadastra um novo livro                         |
| POST   | `/api/loans`                                      | Realiza um empréstimo                          |
| POST   | `/api/loans/return/{id}`                          | Realiza devolução de um livro                  |
| GET    | `/api/report/most-borrowed-books`                 | Relatório de livros mais emprestados           |
| GET    | `/api/report/students-with-overdue-loans`        | Lista alunos com empréstimos em atraso         |
| GET    | `/api/report/loan-history?start=YYYY-MM-DD&end=YYYY-MM-DD` | Histórico de empréstimos por período  |

---

## 🤝 Contribuindo

1. Faça um **fork** do projeto
2. Crie uma branch com sua funcionalidade:

    ```bash
    git checkout -b feature/nova-funcionalidade
    ```

3. Faça commit das alterações:

    ```bash
    git commit -am 'Adiciona nova funcionalidade'
    ```

4. Envie para o repositório remoto:

    ```bash
    git push origin feature/nova-funcionalidade
    ```

5. Abra um **Pull Request**

---

## 📄 Licença

Este projeto está licenciado sob os termos da [MIT License](https://mit-license.org/).

---
