using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonRepositoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonRepositoryLib.Tests
{
    [TestClass()]
    public class PokemonsRepositoryTests
    {

        PokemonsRepository _pokemonList = new PokemonsRepository();
        Pokemon pokemon = new Pokemon { pokedexID = 21, name = "Mewtwo", type = "Psychic" };

        [TestMethod()]
        public void GetPokemonsTest()
        {
            var pokemons = _pokemonList.GetPokemons();
            Assert.AreEqual(20, pokemons.Count());

            var pokemonsWithB = _pokemonList.GetPokemons(nameIncludes: "B");
            Assert.AreEqual(4, pokemonsWithB.Count());

            var pokemonsNameSort = _pokemonList.GetPokemons(orderBy: "name");
            Assert.AreEqual("Beedrill", pokemonsNameSort.First().name);

            var pokemonsNameSortDesc = _pokemonList.GetPokemons(orderBy: "name desc");
            Assert.AreEqual("Bulbasaur", pokemonsNameSortDesc.First().name);

            var pokemonsTypeSort = _pokemonList.GetPokemons(sortofType: "Fire");
            Assert.AreEqual(3, pokemonsTypeSort.Count());

            var pokemonsTypeSortDesc = _pokemonList.GetPokemons(sortofType: "Fire", orderBy: "name desc");
            Assert.AreEqual("Charmander", pokemonsTypeSortDesc.First().name);

            var pokemonsPokedexIDSort = _pokemonList.GetPokemons(orderBy: "pokedexid");
            Assert.AreEqual(1, pokemonsPokedexIDSort.First().pokedexID);

            // Tjek evt for pokedexid_desc vs pokedexid desc alstå om _ er glemt 

            var pokemonsPokedexIDSortDesc = _pokemonList.GetPokemons(orderBy: "pokedexid_desc");
            Assert.AreEqual(20, pokemonsPokedexIDSortDesc.First().pokedexID);


    
        }

        [TestMethod()]
        public void GetPokemonByIDTest()
        {
            Pokemon? p = _pokemonList.GetPokemonByID(1);
            Assert.IsNotNull(p);
            Assert.AreEqual("Bulbasaur", p.name);
        }

        [TestMethod()]
        public void AddPokemonTest()
        {
            _pokemonList.AddPokemon(pokemon);
            IEnumerable<Pokemon> teams = _pokemonList.GetPokemons();
            Assert.AreEqual(21, teams.Count());
        }

        [TestMethod()]
        public void UpdatePokemonTest()
        {
            Pokemon? p = _pokemonList.UpdatePokemon(1, pokemon);
            Assert.IsNotNull(p);
            Assert.AreEqual("Mewtwo", p.name);
            Assert.AreEqual("Psychic", p.type);
        }

        [TestMethod()]
        public void DeletePokemonTest()
        {
            _pokemonList.DeletePokemon(1);
            IEnumerable<Pokemon> pokemons = _pokemonList.GetPokemons();
            Assert.AreEqual(19, pokemons.Count());
        }

        [TestMethod()]
        public void ToStringTest()
        {
           IEnumerable<Pokemon> pokemons = _pokemonList.GetPokemons();
            Assert.AreEqual("PokedexID: 1, Name: Bulbasaur, Type: Grass", pokemons.First().ToString());
        }
    }
}