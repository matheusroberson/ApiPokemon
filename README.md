# ApiPokemon
RestApi Asp.Net Core MVC 5 with EF Core 

### Começo

#### Pré-requisitos
- Necessita .Net core configurado em sua máquina.

#### Instalação

- `git clone https://github.com/matheusroberson/ApiPokemon.git`
- `cd ApiPokemon`
- `dotnet run`

> O acesso para as rotas estará localizado no https://localhost:5001/v1/

#### Rotas:

POST - https://localhost:5001/v1/mestre/ -> Cadastra um novo mestre

> Corpo da Requisição:

```json
{
    "age": string,
    "name": string,
    "cpf": string
}
```

---

GET - https://localhost:5001/v1/pokemon/{id} -> Pegar informação de apenas um pokemon

> id: string | number

---

GET - https://localhost:5001/v1/pokemon/random -> Pegar pokemons aleatórios

---

POST - https://localhost:5001/v1/pokemon -> Cadastrar um pokemon capturado

> Corpo da Requisição:


```json
{
    "idmestre": number,
    "idpokemon": number
}
```

GET - https://localhost:5001/v1/pokemon/mestre/{id} Pegar pokemoons capturados por um mestre

> id: number

## Eu aprendi:
- Conceitos de .Net Core
- Design pattern MVC
- Entity Framework
- & e muito mais
