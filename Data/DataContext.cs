using Microsoft.EntityFrameworkCore;
using ApiPokemon.Models;

namespace ApiPokemon.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> opitions)
            : base(opitions)
        {
        }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Mestre> Mestres { get; set; }
    }
}