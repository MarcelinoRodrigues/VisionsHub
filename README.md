---
VisionsHub
VisionsHub é um sistema de gerenciamento de biblioteca desenvolvido em .NET 8, com arquitetura em camadas, focado em controle de empréstimos de livros, alunos e relatórios detalhados.
Funcionalidades Principais

•	Cadastro e consulta de livros
•	Gestão de alunos
•	Empréstimo e devolução de livros
•	Regras de negócio:
•	Aluno pode ter no máximo 3 empréstimos simultâneos
•	Alunos inativos não podem realizar empréstimos
•	Controle de disponibilidade de exemplares
•	Relatórios
•	Livros mais emprestados (com informações do primeiro, último e maior tomador de empréstimos)
•	Listagem de alunos com empréstimos em atraso
•	Histórico de empréstimos por período

Estrutura do Projeto

•	VisionsHub.Domain: Entidades de domínio (Book, Student, Loan, etc.)
•	VisionsHub.Infra: Repositórios e contexto de dados (Entity Framework Core)
•	VisionsHub.Aplication: Serviços de aplicação, DTOs, interfaces e regras de negócio
•	VisionsHub.API: Controllers e endpoints RESTful

Tecnologias Utilizadas

•	.NET 8
•	Entity Framework Core
•	ASP.NET Core Web API
•	SQL Server (padrão, mas adaptável)

Como Executar

1.	Clone o repositório
git clone https://github.com/seu-usuario/visionshub.git

2.	Configure a string de conexão
No arquivo appsettings.json, ajuste a conexão com seu banco SQL Server.

3.	Execute o projeto

Exemplos de Endpoints

•	GET /api/books — Lista livros
•	POST /api/books — Cadastra livro
•	POST /api/loans — Realiza empréstimo
•	POST /api/loans/return/{id} — Devolve livro
•	GET /api/report/most-borrowed-books — Relatório de livros mais emprestados
•	GET /api/report/students-with-overdue-loans — Alunos com empréstimos em atraso
•	GET /api/report/loan-history?start=2024-01-01&end=2024-12-31 — Histórico de empréstimos por período

Contribuição
1.	Faça um fork do projeto
2.	Crie uma branch (git checkout -b feature/nova-funcionalidade)
3.	Commit suas alterações (git commit -am 'Adiciona nova funcionalidade')
4.	Push para o branch (git push origin feature/nova-funcionalidade)
5.	Abra um Pull Request

Licença
Este projeto está sob a licença MIT.

---
