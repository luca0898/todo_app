# Aplicativo de lista de tarefas

Você deve estar pensando _"Ah, mais um repo de lista de tarefas"_ e realmente esse tema já está saturado nos portifólios, então ... por fazer fazer mais um?

Esse tipo de projeto funciona mais para agrupar alguns conhecimentos do que provar alguma coisa. Isso cria uma base de conhecimento para revisisar e relembrar esses conceitos quando for necessário.

Realmente não há necessidade de ficar avaliando cada aspecto do código, mas é interessante ver que mesmo depois de algum tempo isso funciona perfeitamente bem.

## Como funciona?

O projeto conta com duas partes. O back-end centraliza as regras de negocio do CRUD e o front-end cria uma interface para facilitar o uso dessas funcionalidades.

### BACKEND

Criado utilizando Dotnet 6.0, esse expoe uma API com os endpoints de criação, edição, exclusão e listagem dos dados de tarefas. Armazena os dados utilizando um banco em memória e controla o fluxo dos dados usando Entity Framework.

## FRONTEND

Utilizando o backend, cria uma tela com funcionalidade de criação e listagem das tarefas, além de permitir edita-las e exclui-las. Criada utilizando ReactJS e estilização SCSS

## Como rodar?

Primeiramente irá precisar do Dotnet 6.0 e o NodeJS 18 (ou mais novo) instalados no seu ambiente

Com seu terminal favorito, rode o backend utilizando o comando a seguir:

```bash
$ dotnet run ./backend/1.PRESENTATION/Todo.API/TodoApp.API.csproj
```

Agora será possível acessar o swagger pelo endereço https://localhost:5001/swagger

Com o backend rodando, abra um novo terminal na raiz do projeto e rode o seguinte comando:

```bash
$ cd frontend
$ npm install && npm start
```

Agora será possível acessar a interface frontend pelo endereço http://localhost:3000