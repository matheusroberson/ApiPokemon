using System.Reflection;
using System.Net;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiPokemon.Data;
using ApiPokemon.Models;
using System;
using System.Net.Http;

namespace ApiPokemon.Controllers
{
    [ApiController]
    [Route("v1/pokemon")]
    public class PokemonController : ControllerBase
    {
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<PokeApi>> GetPokemon(string id)
        {
            PokeApi pokemon = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
                using (var reponseTask = await client.GetAsync(id))
                {
                    if (reponseTask.IsSuccessStatusCode)
                    {
                        pokemon = await reponseTask.Content.ReadAsAsync<PokeApi>();
                        using (var clientSpecie = new HttpClient())
                        {
                            clientSpecie.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon-species/");
                            using (var reponseTaskSpecie = await clientSpecie.GetAsync(id))
                            {
                                if (reponseTaskSpecie.IsSuccessStatusCode)
                                {
                                    var url = ((await reponseTaskSpecie.Content.ReadAsAsync<Evolve>()).evolution_chain.url);
                                    using (var clientEvo = new HttpClient())
                                    {
                                        using (var reponseTaskeEvo = await clientEvo.GetAsync(url))
                                        {
                                            if (reponseTaskeEvo.IsSuccessStatusCode)
                                            {
                                                var evos = await reponseTaskeEvo.Content.ReadAsAsync<PokeApi>();
                                                pokemon = new PokeApi()
                                                {
                                                    id = pokemon.id,
                                                    name = pokemon.name,
                                                    order = pokemon.order,
                                                    abilities = pokemon.abilities,
                                                    height = pokemon.height,
                                                    sprites = pokemon.sprites,
                                                    stats = pokemon.stats,
                                                    types = pokemon.types,
                                                    weight = pokemon.weight,
                                                    chain = evos.chain
                                                };
                                            }
                                            else
                                            {
                                                pokemon = null;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    pokemon = null;
                                }
                            }
                        }


                    }
                    else
                    {
                        return pokemon = null;
                    }
                }
            }
            return pokemon;
        }

        [HttpGet]
        [Route("random")]
        public async Task<ActionResult<List<PokeApi>>> GetManyPokemons()
        {
            var random = new Random();
            List<PokeApi> pokemons = new List<PokeApi>();
            PokeApi pokemon = null;

            using (var client = new HttpClient())
            {
                int offSet = random.Next(0, 1008);
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
                using (var reponseTask = await client.GetAsync("?limit=10&offset=" + offSet))
                {
                    if (reponseTask.IsSuccessStatusCode)
                    {
                        var pk = await reponseTask.Content.ReadAsAsync<PokeResult>();

                        foreach (var item in pk.results)
                        {
                            using (var reponseTaskStats = await client.GetAsync(item.name))
                            {
                                if (reponseTaskStats.IsSuccessStatusCode)
                                {

                                    pokemon = await reponseTaskStats.Content.ReadAsAsync<PokeApi>();

                                    using (var clientSpecie = new HttpClient())
                                    {
                                        clientSpecie.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon-species/");
                                        using (var reponseTaskSpecie = await clientSpecie.GetAsync(Convert.ToString(pokemon.id)))
                                        {
                                            if (reponseTaskSpecie.IsSuccessStatusCode)
                                            {
                                                var url = ((await reponseTaskSpecie.Content.ReadAsAsync<Evolve>()).evolution_chain.url);
                                                using (var clientEvo = new HttpClient())
                                                {
                                                    using (var reponseTaskeEvo = await clientEvo.GetAsync(url))
                                                    {
                                                        if (reponseTaskeEvo.IsSuccessStatusCode)
                                                        {
                                                            var evos = await reponseTaskeEvo.Content.ReadAsAsync<PokeApi>();
                                                            pokemons.Add(new PokeApi()
                                                            {
                                                                id = pokemon.id,
                                                                name = pokemon.name,
                                                                order = pokemon.order,
                                                                abilities = pokemon.abilities,
                                                                height = pokemon.height,
                                                                sprites = pokemon.sprites,
                                                                stats = pokemon.stats,
                                                                types = pokemon.types,
                                                                weight = pokemon.weight,
                                                                chain = evos.chain
                                                            });
                                                        }
                                                        else
                                                        {
                                                            pokemon = null;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                pokemon = null;
                                            }
                                        }
                                    }


                                }
                                else
                                {
                                    pokemon = null;
                                }
                            }
                        }
                    }
                    else
                    {
                        pokemons = new List<PokeApi>();
                    }
                }
            }
            return pokemons;
        }

        [HttpGet]
        [Route("mestre/{id:int}")]
        public async Task<ActionResult<List<Pokemon>>> GetByCaptured([FromServices] DataContext context, int id)
        {
            var pokemons = await context.Pokemons
                .Include(x => x.Mestre)
                .AsNoTracking()
                .Where(x => x.IdMestre == id)
                .ToListAsync();
            return pokemons;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Pokemon>> Post(
            [FromServices] DataContext context,
            [FromBody] Pokemon model
        )
        {
            if (ModelState.IsValid)
            {
                context.Pokemons.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}