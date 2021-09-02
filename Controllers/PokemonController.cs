using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiPokemon.Data;
using ApiPokemon.Models;
namespace ApiPokemon.Controllers
{
    [ApiController]
    [Route("v1/pokemon")]
    public class PokemonController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PokeApi>> GetPokemon(string id)
        {
            PokemonApiRequest poke = new PokemonApiRequest();

            return await poke.GetApiPoke(id);
        }

        [HttpGet]
        [Route("random")]
        public async Task<ActionResult<List<PokeApi>>> GetManyPokemons()
        {
            PokemonApiRequest poke = new PokemonApiRequest();

            return await poke.GetApiRandomPoke();
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
                try
                {
                    context.Pokemons.Add(model);
                    await context.SaveChangesAsync();
                    return model;
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Something wrong happened in the register database:", ex);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}