using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
                bool count = context.Mestres.Count() != 0;

                if (count)
                {
                    bool mestre = await context.Mestres.FirstOrDefaultAsync(x => x.Cpf == "0" + model.Cpf) == null;

                    if (!mestre)
                        return BadRequest(ModelState);
                }

                try
                {
                    context.Mestres.Add(model);
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