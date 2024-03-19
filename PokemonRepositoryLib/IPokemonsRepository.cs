
namespace PokemonRepositoryLib
{
    public interface IPokemonsRepository
    {
        Pokemon AddPokemon(Pokemon pokemon);
        Pokemon? DeletePokemon(int pokedexID);
        Pokemon? GetPokemonByID(int pokedexID);
        IEnumerable<Pokemon> GetPokemons(string? sortofType = null, string? nameIncludes = null, string orderBy = null);
        string ToString();
        Pokemon? UpdatePokemon(int pokedexID, Pokemon pokemon);
    }
}