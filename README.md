# Teste Desenvolvedor Full Stack - Target Sistemas

## Regras de Negócio Fornecidas pela Target

### Descrição
O objetivo deste projeto é criar uma tela de extrato de conta corrente que permita listar os lançamentos da conta corrente, independentemente de serem avulsos ou não. Além disso, os usuários devem ter a capacidade de inserir, alterar e cancelar lançamentos avulsos válidos nessa tela.

### Critérios de Aceitação de Negócio

- Cada lançamento deve possuir as seguintes propriedades: Id, descrição, data, valor, avulso e status.
- O status pode ser "Válido" ou "Cancelado".
- O Id deve ser o identificador único do lançamento.
- A descrição deve ser alfanumérica para ajudar os usuários a identificar o lançamento.
- A data deve corresponder à data em que o lançamento foi feito na conta corrente.
- O valor do lançamento pode ser positivo ou negativo.
- O lançamento pode ser avulso (lançamento manual do usuário) ou não (lançamento automático por algum processo).
- A tela de extrato deve incluir um filtro de intervalo de datas, com o intervalo inicial configurado para 2 dias. Isso significa que a tela inicialmente exibirá os dados do extrato dos últimos 2 dias.
- Ao alterar as datas no filtro, a tela deve atualizar os lançamentos de acordo com as novas datas.
- Deve ser possível incluir um lançamento válido no extrato de forma avulsa, identificando-o como avulso no extrato.
- Deve ser possível alterar um lançamento avulso e válido no extrato, permitindo alterar apenas o valor e a data.
- Deve ser possível cancelar um lançamento válido e avulso no extrato.
- Deve existir um totalizador mostrando o valor total de todos os lançamentos listados no extrato.
- Deve existir uma rota na API para inserir um lançamento NÃO AVULSO na conta corrente, incluindo descrição, valor e data. Esse lançamento deve ser gerado como "Não Avulso" e "Válido".

### Critérios de Aceitação Técnicos

- Frontend deve ser desenvolvido em Angular.
- Backend deve ser desenvolvido em .NET, preferencialmente na versão .NET 6.
- Recomenda-se a utilização de uma Single Page Application (SPA) para criar um serviço comum para a API e o aplicativo (o Visual Studio já cria um modelo desse tipo para a API).
- É obrigatório o uso do Entity Framework como ORM para persistência de dados.
- Recomenda-se a utilização do Material Angular para criar elementos de interface do usuário, como inputs, tabelas e grids.
- A comunicação entre o aplicativo e a API deve ser RESTful, utilizando JSON para as operações que envolvem objetos.
- Não há restrições quanto à arquitetura, mas é recomendável adotar alguma arquitetura para organizar o código do frontend e do backend.

## Inicialização do Projeto

Para iniciar o projeto, siga os seguintes passos:

1. Crie o projeto da API:

```shell
dotnet new webapp --output "./finance-app" --framework "net6.0" --language "C#"
```

3. Adicione os pacotes necessários:

```shell
dotnet add package Microsoft.EntityFrameworkCore --version 6.0.10
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.0.10
#dotnet add package Pomelo.EntityFrameworkCore.MySql --version 6.0.2
dotnet add package AutoMapper --version 12.0.1
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 6.0.10
dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables --version 7.0.0
dotnet add package DotNetEnv --version 2.5.0
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design


```

2. Execute a migração do banco de dados:

```shell
dotnet tool install --global dotnet-ef
dotnet ef migrations add CreateTransactionTable
dotnet ef database update
```

## Documentação do Swagger

A documentação do Swagger pode ser acessada em:

[http://localhost:5028/swagger/](http://localhost:5028/swagger/)

### Rotas da API

1. **`POST http://[HOST]/Transaction`**
   - **Descrição**: Adiciona uma transação ao banco de dados.
   - **Parâmetros de entrada**: Um objeto JSON com os campos necessários para criar uma transação (transactionDTO).
   - **Resposta de sucesso (Código 201)**: Retorna os detalhes da transação adicionada.

2. **`GET http://[HOST]/Transaction`**
   - **Descrição**: Recupera uma lista de transações do banco de dados.
   - **Parâmetros de consulta**: Você pode usar os parâmetros de consulta `skip` (para pular um número específico de registros) e `take` (para limitar o número de registros retornados).

3. **`GET http://[HOST]/Transaction/{ID}`**
   - **Descrição**: Obtém os detalhes de uma transação específica com base no ID fornecido.
   - **Parâmetros de rota**: O ID da transação desejada.
   - **Resposta de sucesso**: Retorna os detalhes da transação correspondente.

4. **`PUT http://[HOST]/Transaction/{ID}`**
   - **Descrição**: Atualiza os detalhes de uma transação específica com base no ID fornecido.
   - **Parâmetros de rota**: O ID da transação que você deseja atualizar.
   - **Parâmetros de entrada**: Um objeto JSON com os campos que você deseja atualizar (transactionDTO).
   - **Resposta de sucesso**: Retorna uma resposta vazia (NoContent).

5. **`PATCH http://[HOST]/Transaction/{ID}`**
   - **Descrição**: Atualiza parcialmente os detalhes de uma transação com base no ID fornecido, aplicando um patch JSON.
   - **Parâmetros de rota**: O ID da transação que você deseja atualizar parcialmente.
   - **Parâmetros de entrada**: Um patch JSON com as alterações desejadas (patch).
   - **Resposta de sucesso**: Retorna uma resposta vazia (NoContent) se a atualização for bem-sucedida ou um problema de validação se houver erros nos dados fornecidos.

6. **`DELETE http://[HOST]/Transaction/{ID}`**
   - **Descrição**: Remove uma transação com base no ID fornecido.
   - **Parâmetros de rota**: O ID da transação que você deseja excluir.
   - **Resposta de sucesso**: Retorna uma resposta vazia (NoContent) após a exclusão bem-sucedida ou um NotFound se a transação não for encontrada.


6. **`GET http://[HOST]/health`**
   - **Descrição**: Faça uma solicitação GET para verificar a saúde do sistema.
