using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRepositoryLib
{
    public class PokemonRepositoryDB : IPokemonsRepository
    {
        private readonly PokemonDbContext _context;

        public PokemonRepositoryDB(PokemonDbContext context)
        {
            _context = context;
        }

        public Pokemon AddPokemon(Pokemon pokemon)
        {
            pokemon.Validate();
            //pokemon.pokedexID = 0;
            _context.Pokemons.Add(pokemon);
            _context.SaveChanges();
            return pokemon;
        }

        public Pokemon? DeletePokemon(int pokedexID)
        {
            Pokemon? pokemon = GetPokemonByID(pokedexID);
            if (pokemon is null)
            {
                return null;
            }
            _context.Pokemons.Remove(pokemon);
            _context.SaveChanges();
            return pokemon;
        }

        public IEnumerable<Pokemon> GetPokemons(string? sortofType = null, string? nameIncludes = null, string? orderBy = null)
        {
            IQueryable<Pokemon> query = _context.Pokemons.AsQueryable();
            if (sortofType != null)
            {
                query = query.Where(p => p.type == sortofType);
            }
            if (nameIncludes != null)
            {
                query = query.Where(p => p.name.Contains(nameIncludes));
            }
            if (orderBy != null)
            {
                switch (orderBy.ToLower())
                {
                    case "pokedexid":
                        query = query.OrderBy(p => p.pokedexID);
                        break;
                    case "name":
                        query = query.OrderBy(p => p.name);
                        break;
                    case "type":
                        query = query.OrderBy(p => p.type);
                        break;
                    case "pokedexid_desc":
                        query = query.OrderByDescending(p => p.pokedexID);
                        break;
                    case "name_desc":
                        query = query.OrderByDescending(p => p.name);
                        break;
                    case "type_desc":
                        query = query.OrderByDescending(p => p.type);
                        break;
                    default:
                        break;
                }
            }

            return query;
            // evt query.ToList();
        }

        public Pokemon? GetPokemonByID(int pokedexID)
        {
            return _context.Pokemons.FirstOrDefault(p => p.pokedexID == pokedexID);
        }

        public Pokemon? UpdatePokemon(int pokedexID, Pokemon p)
        {
            p.Validate();
            Pokemon? pokemonToUpdate = GetPokemonByID(pokedexID);
            if (pokemonToUpdate == null) return null;
            pokemonToUpdate.name = p.name;
            pokemonToUpdate.type = p.type;
            _context.SaveChanges();
            return pokemonToUpdate;
        }

    }
}
