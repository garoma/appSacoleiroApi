# Joker Jeans API

API da loja **Joker Jeans**, desenvolvida em C# com ASP.NET Core (.NET
8), organizada em camadas e integrada ao PostgreSQL.

## Funcionalidades

-   Cadastro e consulta de produtos.
-   Listagem de produtos ativos, com filtro opcional por categoria.
-   Retorno de imagens de produtos pela API.
-   Autenticação JWT para operações protegidas, conforme as regras
    configuradas nos controllers.
-   Endpoint de chatbot integrado à API da OpenAI.
-   Documentação e testes dos endpoints pelo Swagger.

> O conjunto de endpoints e as regras de autorização dependem dos
> controllers existentes no projeto.

## Tecnologias

-   C#
-   ASP.NET Core / .NET 8
-   Entity Framework Core
-   PostgreSQL
-   JWT Bearer Authentication
-   Swagger / OpenAPI
-   OpenAI API (chatbot)

## Organização da solução

A solução está dividida nos projetos:

-   `appSacoleiro.API` --- endpoints HTTP, configuração da aplicação,
    autenticação e Swagger.
-   `appSacoleiro.Application` --- interfaces, DTOs e regras de aplicação.
-   `appSacoleiro.Domain` --- entidades e regras centrais do domínio.
-   `appSacoleiro.Infrastructure` --- acesso ao PostgreSQL, repositórios e
    integração com serviços externos.
-   `appSacoleiro.Shared` --- componentes compartilhados.

## Pré-requisitos

-   .NET SDK 8 instalado.
-   PostgreSQL instalado e em execução.
-   Banco de dados configurado para a aplicação.
-   Chave da API da OpenAI, caso deseje testar as respostas do chatbot.

## Configuração local

### 1. Configure a conexão com o PostgreSQL

Configure a connection string `appSacoleiro` em
`appsettings.Development.json`, User Secrets ou variável de ambiente.
Exemplo ilustrativo:

``` json
{
  "ConnectionStrings": {
    "appSacoleiro": "Host=localhost;Port=5432;Database=joker_jeans;Username=SEU_USUARIO;Password=SUA_SENHA"
  }
}
```

Não envie senhas reais para o GitHub. Use valores locais e seguros no
seu ambiente.

### 2. Configure a chave da OpenAI (opcional)

Na pasta do projeto `appSacoleiro.API`, use User Secrets:

``` bash
dotnet user-secrets init
dotnet user-secrets set "OpenAI:ApiKey" "SUA_CHAVE_DA_OPENAI"
```

Nunca coloque a chave da OpenAI no código-fonte, no frontend ou em
arquivos versionados.

O chatbot depende de uma chave válida e de uma conta da OpenAI com
créditos disponíveis. Sem isso, as chamadas podem falhar mesmo que a API
compile corretamente.

### 3. Restaure e compile

Na pasta da solução:

``` bash
dotnet restore
dotnet build
```

### 4. Aplique as migrations

No Visual Studio, usando o Console do Gerenciador de Pacotes, ou com a
CLI do Entity Framework, aplique as migrations existentes. Antes de
executar migrations em outros ambientes, confira a connection string
selecionada.

### 5. Execute a API

Defina `appSacoleiro.API` como projeto de inicialização e execute pelo
Visual Studio, ou use:

``` bash
dotnet run --project appSacoleiro.API
```

Confira no terminal qual endereço HTTPS/HTTP foi informado. Em
desenvolvimento, o Swagger normalmente fica disponível em `/swagger`.

## Endpoints conhecidos

  -----------------------------------------------------------------------------
  Método                  Rota                          Descrição
  ----------------------- ----------------------------- -----------------------
  `POST`                  `/api/chatbot/mensagem`       Envia uma mensagem para
                                                        a assistente virtual e
                                                        recebe uma resposta.

  `GET`                   `/api/produtos`               Consulte o controller
                                                        para confirmar os
                                                        filtros e a resposta
                                                        exatos da listagem.

  `GET`                   `/api/produtos/{id}/imagem`   Obtém a imagem
                                                        associada ao produto,
                                                        quando disponível.
  -----------------------------------------------------------------------------

Consulte o Swagger da instância em execução para ver todos os endpoints,
parâmetros, exemplos e requisitos de autenticação.

### Exemplo de chamada do chatbot

Requisição:

``` json
{
  "mensagem": "Quais produtos vocês têm?"
}
```

Resposta esperada quando o serviço estiver disponível:

``` json
{
  "resposta": "Resposta gerada pela assistente virtual."
}
```

O chatbot consulta os produtos ativos cadastrados no banco para fornecer
contexto sobre nome, descrição, preço e categoria. O banco informado
atualmente não confirma necessariamente tamanho, cor ou quantidade em
estoque; a assistente não deve inventar esses dados.

## Segurança

-   Não versione `appsettings` com credenciais reais.
-   Não publique chaves, tokens, senhas ou connection strings privadas.
-   Antes de publicar a API, adicione limites de uso e proteção contra
    abuso ao endpoint público do chatbot.
-   Configure CORS apenas para os domínios autorizados no ambiente.
-   Use HTTPS em produção.
-   Revise autenticação e autorização de cada endpoint antes de
    disponibilizar a API publicamente.

## Estado do projeto

O projeto está em desenvolvimento. Teste os endpoints no Swagger e
confirme a configuração de banco de dados e da OpenAI antes de publicar
em produção.
