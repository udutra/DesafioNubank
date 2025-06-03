# Desafio Nubank API (Clientes e Contatos)

Esta é uma API REST desenvolvida em ASP.NET Core para gerenciar clientes e seus respectivos contatos, como parte de um desafio técnico. A aplicação utiliza Entity Framework Core para interação com um banco de dados PostgreSQL, que é configurado para rodar em um container Docker.

## Tecnologias Utilizadas 🛠️
- .NET 9 (ou a versão configurada no seu projeto)
- ASP.NET Core (para construção da API REST)
- Entity Framework Core (ORM para interação com o banco de dados)
- PostgreSQL (Banco de dados relacional)
- Docker (Para conteinerização do banco de dados PostgreSQL)
- AutoMapper (Para mapeamento entre DTOs e Entidades)
- FluentValidation (Para validação de dados de entrada)
- Swashbuckle (Swagger/OpenAPI) (Para documentação e teste da API)

## Pré-requisitos 📋
- .NET SDK 9.0 (ou a versão especificada no seu projeto)
- Docker Desktop ou Docker Engine instalado e em execução
- Um editor de código de sua preferência (ex: VS Code, Visual Studio, JetBrains Rider)
- Git (para clonar o repositório)

## Configuração do Ambiente de Desenvolvimento ⚙️

Siga os passos abaixo para configurar e rodar o projeto localmente.

### 1. Clonar o Repositório
```bash
git clone <URL_DO_SEU_REPOSITORIO_GIT>
cd <NOME_DA_PASTA_DO_PROJETO>
```

### 2. Configurar o Banco de Dados PostgreSQL com Docker

Esta API utiliza um banco de dados PostgreSQL. Para facilitar a configuração, ele será executado em um container Docker.

Abra seu terminal ou PowerShell e execute o seguinte comando para baixar a imagem do PostgreSQL e iniciar um container:

```bash
docker run --name postgres-desafio-nubank -e POSTGRES_USER=seu_usuario_pg -e POSTGRES_PASSWORD=sua_senha_pg_forte -e POSTGRES_DB=TesteNubank -p 5432:5432 -d postgres:latest
```

**Explicação do comando Docker:**
- `--name postgres-desafio-nubank`: Define um nome para o seu container (facilita o gerenciamento)
- `-e POSTGRES_USER=seu_usuario_pg`: Define o usuário padrão do PostgreSQL. Substitua `seu_usuario_pg` pelo nome de usuário desejado
- `-e POSTGRES_PASSWORD=sua_senha_pg_forte`: Define a senha para o usuário. Substitua `sua_senha_pg_forte` por uma senha segura
- `-e POSTGRES_DB=TesteNubank`: Cria um banco de dados inicial chamado `TesteNubank`. Este nome deve corresponder ao usado na sua string de conexão
- `-p 5432:5432`: Mapeia a porta 5432 do container para a porta 5432 da sua máquina local
- `-d`: Executa o container em modo "detached" (em segundo plano)
- `postgres:latest`: Especifica a imagem Docker oficial do PostgreSQL (versão mais recente)

> **Importante:** Anote o `POSTGRES_USER`, `POSTGRES_PASSWORD` e `POSTGRES_DB` que você definiu, pois eles serão usados na string de conexão da API.

### 3. Configurar a String de Conexão da Aplicação

A aplicação precisa saber como se conectar ao banco de dados PostgreSQL. Isso é feito através da string de conexão.

**Usando User Secrets (Recomendado para dados sensíveis em desenvolvimento):**

Navegue até o diretório do projeto da API (onde está o arquivo `.csproj`, por exemplo, `DesafioNubank.Api`).

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=TesteNubank;Username=seu_usuario_pg;Password=sua_senha_pg_forte;Include Error Detail=true"
```

Substitua `seu_usuario_pg` e `sua_senha_pg_forte` pelos valores que você usou no comando Docker. O `Include Error Detail=true` pode ser útil para diagnósticos.

**Alternativamente, editando `appsettings.Development.json` (Menos seguro para senhas):**

Crie ou edite o arquivo `appsettings.Development.json` na raiz do seu projeto API e adicione/modifique a seção ConnectionStrings:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=TesteNubank;Username=seu_usuario_pg;Password=sua_senha_pg_forte;Include Error Detail=true"
  },
  "AllowedHosts": "*"
}
```

Novamente, substitua `seu_usuario_pg` e `sua_senha_pg_forte` pelos valores corretos.

### 4. Restaurar Dependências NuGet

No diretório raiz da solução ou do projeto API, execute:

```bash
dotnet restore
```

### 5. Aplicar Migrations do Entity Framework Core

Para criar a estrutura de tabelas no banco de dados `TesteNubank` conforme definido pelas suas entidades e `AppDbContext`:

Navegue até o diretório do projeto da API (onde o arquivo `.csproj` está).

Se você ainda não criou as migrations (arquivos que descrevem as alterações no esquema do banco):

```bash
dotnet ef migrations add InitialCreate -o Infrastructure/Data/Migrations
```

(O `-o Infrastructure/Data/Migrations` especifica o diretório de saída para as migrations, ajuste se sua estrutura for diferente).

Aplique as migrations ao banco de dados:

```bash
dotnet ef database update
```

## Como Executar o Projeto ▶️

Após a configuração do banco de dados e da aplicação:

Navegue até o diretório do projeto da API (ex: `DesafioNubank.Api`).

Execute o comando:

```bash
dotnet run
```

(Ou pressione F5/Ctrl+F5 no seu editor se estiver configurado para iniciar o projeto API).

A API estará disponível em `https://localhost:<porta_https>` e `http://localhost:<porta_http>`. A porta exata será exibida no terminal (geralmente algo como 7xxx para HTTPS e 5xxx para HTTP em projetos .NET recentes).

### Acessar a Documentação Swagger UI

Abra seu navegador e vá para:

```
https://localhost:<porta_https>/swagger
```

(Substitua `<porta_https>` pela porta HTTPS correta informada no terminal). Lá você poderá ver todos os endpoints da API e testá-los diretamente.

## Testes Automatizados 🧪

O projeto possui uma suíte de testes unitários cobrindo as entidades de domínio (`Cliente`, `Contato`) e os principais serviços de aplicação (`ClienteService`).

### Como rodar os testes

No diretório raiz do projeto, execute:

```bash
dotnet test
```

Isso irá compilar a solução e executar todos os testes unitários. O resultado será exibido no terminal, mostrando quais testes passaram ou falharam.

### O que está coberto
- Testes de entidades: validação de construtores, propriedades e relacionamentos.
- Testes de serviços: cenários de sucesso, falha, argumentos inválidos, exceções e regras de negócio.

### Observações
- Os testes utilizam o framework xUnit e a biblioteca Moq para simulação de dependências.
- Para rodar os testes, é necessário apenas o .NET SDK (não precisa de banco de dados ou serviços externos).

## Integração Contínua (CI) com GitHub Actions 🚦

O projeto está configurado para rodar testes automatizados a cada push ou pull request usando o GitHub Actions.

Sempre que um novo código é enviado para os branches principais (`main`, `master` ou `develop`), o workflow executa:
- Checkout do código
- Instalação do .NET
- Restauração das dependências
- Build da solução
- Execução dos testes unitários

O arquivo de configuração do workflow está localizado em `.github/workflows/dotnet-test.yml`.

Assim, garantimos que o código enviado para o repositório está sempre validado pelos testes automatizados!

## Endpoints da API 🧭

A API expõe os seguintes endpoints principais (baseado nas interfaces de serviço que definimos):

### Clientes
- `POST /api/Clientes`: Cadastra um novo cliente.
  - **Corpo da Requisição (exemplo):**
    ```json
    { "nome": "João Silva" }
    ```
- `GET /api/Clientes`: Lista todos os clientes com seus respectivos contatos.
- `GET /api/Clientes/{id}`: Busca um cliente específico pelo ID (incluindo seus contatos).
- `PUT /api/Clientes/{id}`: Atualiza os dados de um cliente existente.
  - **Corpo da Requisição (exemplo):**
    ```json
    { "nome": "João Silva Atualizado" }
    ```
- `DELETE /api/Clientes/{id}`: Remove um cliente.

### Contatos
- `POST /api/Contatos`: Cadastra um novo contato associado a um cliente existente.
  - **Corpo da Requisição (exemplo):**
    ```json
    {
      "nome": "Contato Principal",
      "email": "contato@example.com",
      "ddd": 11,
      "telefone": "987654321",
      "idCliente": "guid-do-cliente-existente"
    }
    ```
- `GET /api/Clientes/{clienteId}/contatos`: Lista todos os contatos de um cliente específico.
- `GET /api/Contatos/{id}`: Busca um contato específico pelo ID.
- `PUT /api/Contatos/{id}`: Atualiza os dados de um contato existente.
  - **Corpo da Requisição (exemplo):**
    ```json
    {
      "nome": "Contato Principal Editado",
      "email": "contato.editado@example.com",
      "ddd": 11,
      "telefone": "912345678"
    }
    ```
- `DELETE /api/Contatos/{id}`: Remove um contato.

> **Obs:** Ajuste os nomes dos controllers nos caminhos dos endpoints (`/api/Clientes`, `/api/Contatos`) e os exemplos de corpo de requisição conforme a sua implementação final e os nomes dos seus DTOs.

## Estrutura do Projeto (Visão Geral) 📂

O projeto segue uma arquitetura em camadas para melhor organização e separação de responsabilidades, inspirada em princípios de Clean Architecture:

- **DesafioNubank.Api:** Camada de Apresentação. Responsável por expor a API REST para o mundo externo. Contém:
  - Controllers (pontos de entrada HTTP)
  - Configuração da API (Program.cs, Startup, etc.)
  - Injeção de dependências dos serviços e repositórios
  - Configuração e exposição da documentação Swagger
- **DesafioNubank.Application:** Camada de Aplicação. Contém a lógica de casos de uso (Serviços), interfaces de serviço, DTOs (Data Transfer Objects), validadores (FluentValidation), e perfis de mapeamento (AutoMapper).
- **DesafioNubank.Domain:** Camada de Domínio. Contém as entidades de negócio (Modelos como Cliente, Contato) e as interfaces dos repositórios.
- **DesafioNubank.Infrastructure:** Camada de Infraestrutura. Contém as implementações concretas dos repositórios, o AppDbContext do Entity Framework Core, as migrations do banco de dados, e quaisquer outros serviços de infraestrutura (ex: clientes HTTP para serviços externos, etc.).
- **DesafioNubank.Tests:** Projeto de testes unitários. Contém os testes automatizados das entidades de domínio e dos serviços de aplicação, utilizando xUnit e Moq para simulação de dependências. Os testes garantem a qualidade e o correto funcionamento das regras de negócio e dos modelos do sistema.

---

Sinta-se à vontade para contribuir ou reportar issues!