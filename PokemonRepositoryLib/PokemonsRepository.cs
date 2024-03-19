using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRepositoryLib
{
    public class PokemonsRepository : IPokemonsRepository
    {
        private int _nextPokedexID = 21;
        public List<Pokemon> _pokemonList = new List<Pokemon>
        {
            new Pokemon { pokedexID = 1, name = "Bulbasaur", type = "Grass" },
            new Pokemon { pokedexID = 2, name = "Ivysaur", type = "Grass" },
            new Pokemon { pokedexID = 3, name = "Venusaur", type = "Grass" },
            new Pokemon { pokedexID = 4, name = "Charmander", type = "Fire" },
            new Pokemon { pokedexID = 5, name = "Charmeleon", type = "Fire" },
            new Pokemon { pokedexID = 6, name = "Charizard", type = "Fire" },
            new Pokemon { pokedexID = 7, name = "Squirtle", type = "Water" },
            new Pokemon { pokedexID = 8, name = "Wartortle", type = "Water" },
            new Pokemon { pokedexID = 9, name = "Blastoise", type = "Water" },
            new Pokemon { pokedexID = 10, name = "Caterpie", type = "Bug" },
            new Pokemon { pokedexID = 11, name = "Metapod", type = "Bug" },
            new Pokemon { pokedexID = 12, name = "Butterfree", type = "Bug" },
            new Pokemon { pokedexID = 13, name = "Weedle", type = "Bug" },
            new Pokemon { pokedexID = 14, name = "Kakuna", type = "Bug" },
            new Pokemon { pokedexID = 15, name = "Beedrill", type = "Bug" },
            new Pokemon { pokedexID = 16, name = "Pidgey", type = "Flying" },
            new Pokemon { pokedexID = 17, name = "Pidgeotto", type = "Flying" },
            new Pokemon { pokedexID = 18, name = "Pidgeot", type = "Flying" },
            new Pokemon { pokedexID = 19, name = "Rattata", type = "Normal" },
            new Pokemon { pokedexID = 20, name = "Raticate", type = "Normal" },
            };

        //public IEnumerable<Pokemon> GetAllPokemons()
        //{
        //    return _pokemonList;
        //}


        public IEnumerable<Pokemon> GetPokemons(string? sortofType = null, string? nameIncludes = null, string? orderBy = null)
        {

            IEnumerable<Pokemon> result = new List<Pokemon>(_pokemonList);
            if (sortofType != null)
            {
                result = result.Where(p => p.type == sortofType);
            }
            if (nameIncludes != null)
            {
                result = result.Where(p => p.name.Contains(nameIncludes));
            }
            if (orderBy != null)
            {
                switch (orderBy.ToLower())
                {
                    case "pokedexid":
                        result = result.OrderBy(p => p.pokedexID);
                        break;
                    case "name":
                        result = result.OrderBy(p => p.name);
                        break;
                    case "type":
                        result = result.OrderBy(p => p.type);
                        break;
                    case "pokedexid_desc":
                        result = result.OrderByDescending(p => p.pokedexID);
                        break;
                    case "name_desc":
                        result = result.OrderByDescending(p => p.name);
                        break;
                    case "type_desc":
                        result = result.OrderByDescending(p => p.type);
                        break;
                    default:
                        break;
                }
            }

            return result;

        }

        public Pokemon? GetPokemonByID(int pokedexID)
        {
            return _pokemonList.Find(p => p.pokedexID == pokedexID);

        }

        public Pokemon AddPokemon(Pokemon pokemon)
        {
            pokemon.Validate();
            pokemon.pokedexID = _nextPokedexID++;
            _pokemonList.Add(pokemon);
            return pokemon;
        }

        public Pokemon? UpdatePokemon(int pokedexID, Pokemon pokemon)
        {
            pokemon.Validate();
            Pokemon? existingPokemon = GetPokemonByID(pokedexID);
            if (existingPokemon == null)
            {
                throw null;
            }
            existingPokemon.name = pokemon.name;
            existingPokemon.type = pokemon.type;
            return existingPokemon;
        }

        public Pokemon? DeletePokemon(int pokedexID)
        {
            Pokemon? existingPokemon = GetPokemonByID(pokedexID);
            if (existingPokemon == null)
            {
                throw null;
            }
            _pokemonList.Remove(existingPokemon);
            return existingPokemon;
        }

        public override string ToString()
        {
            return string.Join("\n", _pokemonList);
        }





    }
}
