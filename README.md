## XPE Final Project

### 📝 Resumo do Projeto

Este é uma API de delivery que permite o cadastro e gerenciamento de entregadores, incluindo a geração de arquivos em diversos formatos como PDF e Excel. A API é construída com .NET 8 e utiliza diversas bibliotecas para facilitar o desenvolvimento e a documentação.

## Features

- API RESTful para gerenciamento de entregadores
- Documentação da API com Scalar
- Teste unitário para validação de regras de negócio
- CRUD Usuários e motoboys
- Geração de arquivos em PDF e Excel
- Validação de dados com Fluent Validation
- Mapeamento de objetos com AutoMapper
- Docker para containerização
- Banco de dados Postgres
- Autenticação JWT
- Logs com Serilog e seq

## Techs

- ![badge-dot-net] - .NET 8
- Auto mapper
- Fluent validation
- Scalar
- Serilog
- Seq
- Docker
- Postgres
- JWT Authentication
- Unit Tests
- Docker Compose
- xUnit
- Sentry

## Primeiros passos

1. Clone o repositório
```sh
    git clone https://github.com/JefferssonSemin/.net-example.git
```
2. Rode o aquivo docker-compose 
```sh
    docker-compose up -d
```
   - Isso irá iniciar os containers necessários, incluindo o banco de dados Postgres e a aplicação .NET.

3. Rode a solução .net xpe-final
```sh
    dotnet run --project src/XPE.Final/XPE.Final.csproj
```
    - A aplicação estará disponível em `http://localhost:5289`.
4. Acesse a documentação da API em `http://localhost:{sua porta}/scalar`.
5. Para testar a API, você pode usar ferramentas como Postman ou diretamente pelo Scalar. A documentação também está em delivery-doc utilizando Bruno.


## Diagramas

UML 
![Uml](https://github.com/JefferssonSemin/xpe-final/blob/main/delivery-doc/UML.png)

DIagrama de Contexto
![Contexto](https://github.com/JefferssonSemin/xpe-final/blob/main/delivery-doc/Contexto.png)

Diagrama de Entidade Relacionamento
![Container](https://github.com/JefferssonSemin/xpe-final/blob/main/delivery-doc/Container.png)

Arquitetura de Software
![Arquitetura de software](https://github.com/JefferssonSemin/xpe-final/blob/main/delivery-doc/Software.png)