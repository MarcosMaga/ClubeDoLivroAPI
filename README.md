# API do Clube do Livro

Esta API, feita em C# e SQLServer tem o objetivo de gerenciar um clube do livro, sendo responsável por armazenar os livros em estoque no qual os usuários podem pegar emprestado e fazer avaliações com nota e opinião.

## Preparo

* Para desenvolver esta API foi utilizado uma ORM para facilitar o uso do banco de dados, após baixar a API, no gerenciador de pacotes execute o comando `Update-Database -Context ClubeDoLivroDBContext`.
* Lembre-se de configurar a instancia do seu banco de dados e reconfigura-lá no `appsettings.json`.

## Rotas e Endpoints

A API é baseada em 5 Entidades: <b>Livros, Escritores, Usuário, Avaliação e Emprestimo</b>. Todas estás entidades possuem rotas para criação, deleção, atualização e consulta.

## Escritores

Os Escritores possuem relação direta com os livros. Um livro precisa de um escritor para existir.

### `/api/escritor` Método: GET

Retorna todos os escritores cadastrados.

**Exemplo de resposta:**
```json
[
  {
    "id": 1,
    "nome": "J.K Rowling",
    "ano": 1965
  },
  {
    "id": 2,
    "nome": "Machado de Assis",
    "ano": 1839
  }
]
```

### `/api/escritor` Método: POST

Cadastra um Escritor no Banco de Dados.

**Parâmetros:**
- `nome` (obrigatório, tipo: string): Nome do Escritor.
- `ano` (obrigatório, tipo: int) Ano de nascimento do Escritor.

**Exemplo de solicitação:**
```json
{
  "nome": "Machado de Assis",
  "ano": 1839
}
```

### `/api/escritor/{id}` Método: GET

Retorna os dados de um escritor pelo ID.

**Exemplo de solicitação:**
`GET /api/escritor/32`

**Exemplo de Resposta:**
```json
{
  "id": 32
  "nome": "Carolina Maria de Jesus"
  "ano": 1914
}
```

### `/api/escritor/{id}` Método: DELETE

Deleta um escritor do banco de dado pelo seu ID.

**Exemplo de solicitação:**
`DELETE /api/escritor/3` 

### `/api/escritor/{id}` Método: PUT

Atualiza os dados de um escritor no banco de dados pelo seu ID.

**Parâmetros:**
- `nome` (obrigatório, tipo: string): Nome do Escritor.
- `ano` (obrigatório, tipo: int) Ano de nascimento do Escritor.

**Exemplo de solicitação:**
`PUT /api/escritor/7`

```json
{
  "nome": "Machado de Assis",
  "ano": 1839
}
```

### `/api/escritor/{id}/livros` Método: GET

Retorna todos os livros cadastrado de algum escritor especificado pelo seu ID.

**Exemplo de solicitação:**
`GET /api/escritor/1/livros`

**Exemplo de resposta:**
```json
[
  {
    "id": 1,
    "nome": "Harry Potter: E a Pedra Filosofal",
    "descricao": "Harry Potter parte 1",
    "paginas": 262,
    "escritorId": 1,
    "escritor": {
      "id": 1,
      "nome": "J.K Rowling",
      "ano": 1965
    },
    "userNota": 5
  },
  {
    "id": 2,
    "nome": "Harry Potter: E a Câmara Secreta",
    "descricao": "Harry Potter 2",
    "paginas": 251,
    "escritorId": 1,
    "escritor": {
      "id": 1,
      "nome": "J.K Rowling",
      "ano": 1965
    },
    "userNota": 3.7
  }
]
```

## Livros
Agora que já possuimos o conhecimendo de como criar escritores, é possível criar livros que eles escreveram no banco de dados. Podemos atualizar e deleter estes livros quando for necessário.
























