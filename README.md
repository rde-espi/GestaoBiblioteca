# 📚 GestãoBiblioteca

## Descrição do Projeto

A **GestãoBiblioteca** é uma aplicação Web desenvolvida em **ASP.NET Core MVC**, destinada à gestão de uma biblioteca.

O projeto foi desenvolvido no âmbito da **UFCD 5417**, do curso **CET 105**, no CINEL, com o objetivo de aplicar os conhecimentos adquiridos em desenvolvimento Web, bases de dados relacionais, autenticação e organização de código.

## Tecnologias Utilizadas

- C# e ASP.NET Core MVC (.NET 5)
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Repository Pattern
- Razor Views, HTML, CSS e Bootstrap
- Git e GitHub

## Funcionalidades

### Autenticação e Gestão de Utilizadores
- Registo de utilizadores
- Início e fim de sessão
- Edição de perfil
- Alteração de palavra-passe
- Recuperação de palavra-passe por email
- Proteção de páginas através de autenticação

### Gestão da Biblioteca
- **Categorias:** criação, consulta, edição e eliminação de categorias
- **Autores:** criação, consulta, edição e eliminação de autores
- **Livros:** gestão dos livros e respetivas associações a categorias e autores
- **Leitores:** registo e gestão dos leitores da biblioteca
- **Empréstimos:** registo de empréstimos, consulta de detalhes e devolução de livros

A aplicação controla a disponibilidade dos livros de acordo com os empréstimos e as devoluções realizados.

## Estrutura da Base de Dados

A aplicação utiliza as seguintes entidades principais:

- **Categoria:** classificação dos livros
- **Autor:** informação sobre os autores
- **Livro:** informação e disponibilidade dos livros
- **LivroAutor:** associação entre livros e autores
- **Leitor:** informação sobre os leitores
- **Emprestimo:** registo dos empréstimos e devoluções
- **User:** utilizadores autenticados através do ASP.NET Core Identity

### Relacionamentos

- Uma categoria pode conter vários livros (1:N).
- Um livro pode ter vários autores e um autor pode estar associado a vários livros (N:N).
- Um leitor pode realizar vários empréstimos (1:N).
- Um livro pode estar associado a vários registos de empréstimo (1:N).

## Arquitetura

O projeto segue o padrão **Model-View-Controller (MVC)**, separando a apresentação, o tratamento dos pedidos e a lógica de acesso aos dados.

O acesso à base de dados utiliza o **Entity Framework Core**, com aplicação do **Repository Pattern** para organizar as operações sobre as entidades.

A estrutura inclui controladores, modelos, entidades, vistas, repositórios e componentes auxiliares para autenticação e envio de emails.

## Base de Dados

A base de dados é gerida através do **SQL Server** e do **Entity Framework Core**, utilizando migrations para criar e atualizar a estrutura das tabelas e os respetivos relacionamentos.

## Execução do Projeto

1. Clonar o repositório.
2. Abrir a solução `GestaoBiblioteca.sln` no Visual Studio.
3. Restaurar os pacotes NuGet.
4. Configurar a ligação ao SQL Server no ficheiro `appsettings.json`.
5. Configurar o serviço de email para testar a recuperação de palavra-passe.
6. Aplicar as migrations através do Package Manager Console:

   `Update-Database`

7. Executar a aplicação.

**Nota:** as credenciais de email devem ser configuradas localmente e não devem ser partilhadas no repositório.

## Autor e Contexto Académico

**Curso:** CET 105  
**UFCD:** 5417  
**Entidade formadora:** CINEL  
**Projeto:** Gestão de uma Biblioteca  
**Ano:** 2026

Projeto desenvolvido para fins académicos e de avaliação.
