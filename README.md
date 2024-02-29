# API do Clube do Livro

Esta API, feita em C# e SQLServer tem o objetivo de gerenciar um clube do livro, sendo responsável por armazenar os livros em estoque no qual os usuários podem pegar emprestado e fazer avaliações com nota e opinião.

## Preparo

* Para desenvolver esta API foi utilizado uma ORM para facilitar o uso do banco de dados, após baixar a API, no gerenciador de pacotes execute o comando `Update-Database -Context ClubeDoLivroDBContext`.
* Lembre-se de configurar a instancia do seu banco de dados e reconfigura-lá no appsettings.json.

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

### `/api/livro` Método: GET
Retorna todos os livros cadastrados no banco de dados.

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
    "userNota": 3.5
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
    "userNota": 5
  },
  {
    "id": 3,
    "nome": "Dom Casmurro",
    "descricao": "O narrador Bento Santiago retoma a infância que passou na Rua de Matacavalos",
    "paginas": 208,
    "escritorId": 2,
    "escritor": {
      "id": 2,
      "nome": "Machado de Assis",
      "ano": 1839
    },
    "userNota": 0
  }
]
```

### `/api/livro` Método: POST
Cadastra um Livro no Banco de Dados

**Parâmetros:**
- `nome` (obrigatório, tipo: string): Nome do Livro
- `descricao` (tipo: string): Sinopse ou descrição do Livro
- `paginas` (tipo: int): Quantidade de paginas do livro
- `escritorId` (obrigatório, tipo: int): ID do Escritor do Livro

**Exemplo de solicitação:**
`POST /api/livro`
```json
{
  "nome": "Quarto de Despejo",
  "descricao": "Diário de uma favelada é um livro autobiográfico de Carolina Maria de Jesus",
  "paginas": 200,
  "escritorId": 32
}
```

### `/api/livro/{id}` Método GET
Retorna o Livro especificado pelo ID

**Exemplo de solicitação:**
`GET /api/livro/7`

**Exemplo de resposta:**
```json
{
  "id": 3,
  "nome": "Dom Casmurro",
  "descricao": "O narrador Bento Santiago retoma a infância que passou na Rua de Matacavalos e conta a história do amor e das desventuras que vive",
  "paginas": 208,
  "escritorId": 2,
  "escritor": {
    "id": 2,
    "nome": "Machado de Assis",
    "ano": 1839
  },
  "userNota": 0
}
```

### `/api/livro/{id}` Método: DELETE
Deleta do Banco de Dados um Livro especificado pelo seu ID.

**Exemplo de solicitação:**
`DELETE /api/livro/2`

### `/api/livro/{id}` Método: PUT
Atualiza os dados de um Livro especificado pelo seu ID.

**Exemplo de solicitação:**
`PUT /api/livro/3`
```json
{
  "nome": "Dom Casmurro: Uma Obra de Machado de Assis",
  "descricao": "O narrador Bento Santiago conta a história do amor e das desventuras que vive",
  "paginas": 208,
  "escritorId": 2
}
```

### `/api/livro/{id}/status` Método: GET
Retorna se o Livro especificado pelo ID está disponível ou indisponível.

**Exemplo de solicitação:**
`GET /api/livro/1/status`

**Exemplo de resposta:**
```json
{
  "status": "Disponível"
}
```

### `/api/livro/{id}/avaliacoes` Método: GET
Retorna todas as avaliações do Livro especificado pelo ID.

**Exemplo de solicitação:**
`GET /api/livro/1/avaliacoes`

**Exemplo de resposta:**
```json
[
  {
    "id": 1,
    "usuarioId": 1,
    "livroId": 1,
    "nota": 5,
    "opiniao": "Harry poti é bom",
    "livro": {
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
      "userNota": 0
    },
    "usuario": {
      "id": 1,
      "nome": "Marcos Magalhães",
      "email": "marcos@teste.com"
    }
  },
  {
    "id": 5,
    "usuarioId": 2,
    "livroId": 1,
    "nota": 2,
    "opiniao": "Não gosto tanto de fantasia",
    "livro": {
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
      "userNota": 0
    },
    "usuario": {
      "id": 2,
      "nome": "Teste",
      "email": "teste@teste.com"
    }
  }
]
```

## Usuários
No sistema, os Usuários são os participantes do Clube do Livro. Um Usuário tem direito de pegar Livros Emprestado e também pode fazer Avaliações dos Livros que ele leu dando sua nota e opinião.

### `/api/usuario` Método: GET
Retorna todos os Usuários cadastrados no Banco de Dados.

**Exemplo de solicitação:**
`GET /api/usuario`

**Exemplo de resposta:**
```json
[
  {
    "id": 1,
    "nome": "Marcos Magalhães",
    "email": "marcos@teste.com"
  },
  {
    "id": 2,
    "nome": "Teste",
    "email": "teste@teste.com"
  }
]
```

### `/api/usuario` Método POST
Cadastra um Usuário no Banco de Dados.

**Parâmetros:**
- `nome` (obrigatório, tipo: string): Nome do Usuário.
- `email` (obrigatório, tipo: int) Email do Usuário.

**Exemplo de solicitação:**
`POST /api/usuario`

```json
{
  "nome": "Eduardo",
  "email": "eduardo@gmail.com"
}
```

### `/api/usuario/{id}` Método GET
Retorna um Usuário especificado pelo seu ID.

**Exemplo de solicitação:**
`GET /api/usuario/3`

**Exemplo de resposta:**
```json
{
  "id": 3,
  "nome": "Eduardo",
  "email": "eduardo@gmail.com"
}
```

### `/api/usuario/{id}` Método: DELETE
Deleta os dados um Usuário especificado pelo seu ID.

**Exemplo de solicitação:**
``DELETE /api/usuario/2`

### `/api/usuario/{id}` Método: PUT
Atualiza os dados de um Usuário especificado pelo seu ID.

**Exemplo de solicitação**
`PUT /api/usuario/3`
```json
{
  "nome": "Eduardo da Silva",
  "email": "eduardo@gmail.com"
}
```

### `/api/usuario/{id}/avaliacoes` Método GET
Retorna todas as avaliações feita pelo Usuário especificado pelo ID.

**Exemplo de solicitação:**
`GET /api/usuario/1/avaliacoes`

**Exemplo de resposta:**
```json
[
  {
    "id": 1,
    "usuarioId": 1,
    "livroId": 1,
    "nota": 5,
    "opiniao": "Harry potter é bom",
    "livro": {
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
      "userNota": 0
    },
    "usuario": {
      "id": 1,
      "nome": "Marcos Magalhães",
      "email": "marcos@teste.com"
    }
  },
  {
    "id": 4,
    "usuarioId": 1,
    "livroId": 2,
    "nota": 5,
    "opiniao": "Harry Potter 2 é melhor ainda",
    "livro": {
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
      "userNota": 0
    },
    "usuario": {
      "id": 1,
      "nome": "Marcos Magalhães",
      "email": "marcos@teste.com"
    }
  }
]
```

### `/api/usuario/{id}/emprestimos` Método: GET
Retorna todos os Emprestimos de Livros feito por o Usuário especifiado pelo ID.

**Exemplo de solicitação:**
`GET /api/usuario/1/emprestimos`

**Exemplo de resposta:**
```json
[
  {
    "id": 1,
    "usuarioId": 1,
    "livroId": 1,
    "dataEmprestimo": "2023-07-15T20:47:48.121",
    "dataEstimada": "2023-07-22T20:47:48.121",
    "dataDevolucao": "2023-07-15T21:21:52.063",
    "livro": {
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
      "userNota": 0
    },
    "usuario": {
      "id": 1,
      "nome": "Marcos Magalhães",
      "email": "marcos@teste.com"
    }
  }
]
```

## Avaliações
Quando os participantes do Clube do Livro lerem algum exemplar, eles podem expor sua nota e opinião para que outros Usuários possam ver e decidir se vão ou não querer pegar aquele livro emprestado. Uma nota pode conter o valor máximo de 5 e mínimo de 0.

### `/api/avaliacao` Método GET
Retorna todas as Avaliações cadastradas no Banco de Dados.

**Exemplo de solicitação:**
`GET /api/avaliacao`

**Exemplo de resposta:**
```json
[
  {
    "id": 1,
    "usuarioId": 1,
    "livroId": 1,
    "nota": 5,
    "opiniao": "Harry poti é bom",
    "livro": {
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
      "userNota": 0
    },
    "usuario": {
      "id": 1,
      "nome": "Marcos Magalhães",
      "email": "marcos@teste.com"
    }
  },
  {
    "id": 4,
    "usuarioId": 1,
    "livroId": 2,
    "nota": 5,
    "opiniao": "Harry Potter 2 é melhor ainda",
    "livro": {
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
      "userNota": 0
    },
    "usuario": {
      "id": 1,
      "nome": "Marcos Magalhães",
      "email": "marcos@teste.com"
    }
  },
  {
    "id": 5,
    "usuarioId": 2,
    "livroId": 1,
    "nota": 2,
    "opiniao": "Não gosto tanto de fantasia",
    "livro": {
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
      "userNota": 0
    },
    "usuario": {
      "id": 2,
      "nome": "Teste",
      "email": "teste@teste.com"
    }
  }
]
```

### `/api/avaliacao` Método: POST
Cadastra uma Avaliação de um Livro feita por um Usuário no Banco de Dados.

**Parâmetros:**
- `usuarioId` (obrigatório, tipo: int): ID do Usuário.
- `livroID` (obrigatório, tipo: int) ID do Livro.
- `nota` (obrigatório, tipo: float, 0 <= nota >= 5 ) ID do Livro.
- `opiniao` (tipo: string) Opinião sobre o Livro.

**Exemplo de solicitação:**
```json
{
  "usuarioId": 1,
  "livroId": 1,
  "nota": 4,
  "opiniao": "Muito bom o Livro!!"
}
```

### `/api/avaliacao/{id}` Método: GET
Retorna uma avaliação especificada pelo seu ID.

**Exemplo de solicitação:**
`GET /api/avaliacao/3`

**Exemplo de resposta:**
```json
{
  "id": 5,
  "usuarioId": 2,
  "livroId": 1,
  "nota": 2,
  "opiniao": "Não gosto tanto de fantasia",
  "livro": {
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
    "userNota": 0
  },
  "usuario": {
    "id": 2,
    "nome": "Teste",
    "email": "teste@teste.com"
  }
}
```

### `/api/avaliacao/{id}` Método: DELETE
Deleta os dados de uma Avaliação do Banco de Dados.

**Exemplo de solicitação:**
`DELETE /api/avaliacao/4`

### `/api/avaliacao/{id}` Método: PUT
Atualiza uma Avaliação especificada pelo ID no Banco de Dados.

**Exemplo de solicitação**
`PUT /api/avaliacao/5`
```json
{
  "usuarioId": 2,
  "livroId": 1,
  "nota": 2,
  "opiniao": "Não gosto tanto de fantasia",
}
```

## Emprestimos
Agora podemos cadastrar vários Livros de diferentes Escritores, nossos Usuários estão prontos para Avalia-los, mas para isso, é preciso ler o livro e Emprestando eles é uma forma de fazer isso. Para isso, um Usuário deverá escolher o Livro que deseja pegar Emprestado, definir o dia que pegará o Livro. O sistema dára 7 dias para que ele possa ler e devolver.

### `/api/emprestimo` Método: GET
Retorna todos os Emprestimos já registrados no Banco de Dados.

**Exemplo de solicitação:**
`GET /api/emprestimo`

**Exemplo de respposta:**
```json
[
  {
    "id": 1,
    "usuarioId": 1,
    "livroId": 1,
    "dataEmprestimo": "2023-07-15T20:47:48.121",
    "dataEstimada": "2023-07-22T20:47:48.121",
    "dataDevolucao": "2023-07-15T21:21:52.063",
    "livro": {
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
      "userNota": 0
    },
    "usuario": {
      "id": 1,
      "nome": "Marcos Magalhães",
      "email": "marcosvmagalhaes123@gmail.com"
    }
  }
]
```

### `/api/emprestimo` Método: POST
Cadastra um Emprestimo de um Livro no Banco de Dados.

**Parâmetros:**
- `usuarioId` (obrigatório, tipo: int): ID do Usuário.
- `livroID` (obrigatório, tipo: int) ID do Livro.
- `dataEmprestimo` (obrigatório, tipo: DateTime) Data que foi realizada o Emprestimo.

**Exemplo de solicitação:**
```json
{
  "usuarioId": 1,
  "livroId": 5,
  "dataEmprestimo": "2023-07-16T00:13:52.492Z",
}
```

### `/api/emprestimo/{id}` Método: GET
Retorna um Emprestimo especificado pelo seu ID.

**Exemplo de solicitação:**
`GET /api/emprestimo/1`

**Exemplo de resposta:**
```json
{
  "id": 1,
  "usuarioId": 1,
  "livroId": 1,
  "dataEmprestimo": "2023-07-15T20:47:48.121",
  "dataEstimada": "2023-07-22T20:47:48.121",
  "dataDevolucao": "2023-07-15T21:21:52.063",
  "livro": {
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
    "userNota": 0
  },
  "usuario": {
    "id": 1,
    "nome": "Marcos Magalhães",
    "email": "marcosvmagalhaes123@gmail.com"
  }
}
```

### `/api/emprestimo/{id}` Método: DELETE
Deleta os dados de um Emprestimo do Banco de Dados.

**Exemplo de solicitação:**
`DELETE /api/emprestimo/1`

### `api/emprestimo/{id}` Método: PATCH
Define a Data de devolução de um emprestimo

**Exemplo de solicitação:**
`PATCH /api/emprestimo/1 BODY: 2023-07-15T21:21:52.063`










































