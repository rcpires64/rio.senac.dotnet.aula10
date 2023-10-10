
# Senac Rio - Cadastrado Professores e Alunos

Repositório para a turma de desenvolvimento .net de aplicações back-end para Web - MAF - 2023.1


## Documentação da API

#### Retorna todos os itens

### Aluno Controller
#### Retorna a lista de alunos

```http
  GET /api/aluno
```

### Professor Controller
#### Retorna a lista de professores

```http
  GET /api/professor/lista
```
#### Retorna uma lista de professores filtrando pelo nome

```http
  GET /api/professor/busca?nome={nome}
```

| Parâmetro   | Tipo       | Descrição                                   |
| :---------- | :--------- | :------------------------------------------ |
| `nome`      | `string` | **Obrigatório**. O NOME do professor que você quer |


## Autores

- [@lpjunior](https://www.github.com/lpjunior)


## Referência

 - [.Net](https://learn.microsoft.com/pt-br/dotnet/)
 - [What is REST](https://restfulapi.net)
 - [What is JSON](https://restfulapi.net/introduction-to-json/)

