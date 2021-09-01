using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPokemon.Models;
using System;
using System.Net.Http;


namespace ApiPokemon.Controllers
{
    public class PokemonApiRequest
    {
        private HttpClient client;

        public PokemonApiRequest()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        }

        public async Task<PokeApi> GetApiPoke(string id)
        {
            HttpResponseMessage reponseTask = await client.GetAsync("pokemon/" + id);
            HttpResponseMessage reponseSpecie = await client.GetAsync("pokemon-species/" + id);

            if (reponseTask.IsSuccessStatusCode && reponseSpecie.IsSuccessStatusCode)
            {
                PokeApi pokemon = await reponseTask.Content.ReadAsAsync<PokeApi>();
                string url = ((await reponseSpecie.Content.ReadAsAsync<Evolve>()).evolution_chain.url);
                HttpResponseMessage reponseTaskEvo = await client.GetAsync(url);

                if (reponseTaskEvo.IsSuccessStatusCode)
                {
                    PokeApi evos = await reponseTaskEvo.Content.ReadAsAsync<PokeApi>();
                    return new PokeApi()
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
            }
            return new PokeApi();
        }

        public async Task<List<PokeApi>> GetApiRandomPoke()
        {
            Random random = new Random();
            List<PokeApi> pokemons = new List<PokeApi>();
            int offSet = random.Next(0, 1008);

            HttpResponseMessage reponseTask = await client.GetAsync("pokemon/?limit=10&offset=" + offSet);
            PokeResult pk = await reponseTask.Content.ReadAsAsync<PokeResult>();

            if (reponseTask.IsSuccessStatusCode)
            {
                foreach (var item in pk.results)
                {
                    string id = item.url.Split("/").Where(x => int.TryParse(x, out _)).ToList()[0].ToString();

                    pokemons.Add(await this.GetApiPoke(id));
                }
                return pokemons;
            }
            return new List<PokeApi>();
        }
    }
}