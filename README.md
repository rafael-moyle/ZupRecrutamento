# ZupRecrutamento
Repositório para teste de recrutamento da Zup

A API foi feita utilizando Dapper como ORM e usando banco de dados SQL Server (arquivo para criação da tabela na pasta raiz).

Foi utilizado o padrão Repository para tratar da persistência dos dados.

Foi utilizado o padrão Service para separar a lógica da regra de negócio das entidades.

Para validação dos dados optei por utilizar uma camada separada chamada de Validacao com alguns interceptadores e filtros para devolver a exceção para o cliente.

# Arquivos na pasta raiz
- database.sql
- postman.json
