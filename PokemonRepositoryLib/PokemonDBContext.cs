using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PokemonRepositoryLib
{
    public class PokemonDbContext : DbContext
    {
        
        public PokemonDbContext(DbContextOptions<PokemonDbContext> options) : base(options)
        {
        }

        public DbSet<Pokemon> Pokemons { get; set; }

    }
}

