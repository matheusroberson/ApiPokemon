using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiPokemon.Data;
using ApiPokemon.Models;

namespace ApiPokemon.Controllers
{
    [ApiController]
    [Route("v1/mestre")]
    public class MestreController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Mestre>> Post(
            [FromServices] DataContext context,
            [FromBody] Mestre model
        )
        {
            if (ModelState.IsValid)
            {
                context.Mestres.Add(model);
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