using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonRepositoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PokemonRepositoryLib.Tests
{
    [TestClass()]
    public class PokemonTests
    {
        private readonly Pokemon _pokemon = new Pokemon { pokedexID = 1, name = "Bulbasaur", type = "Grass" };
        private readonly Pokemon _pokemonNullName = new Pokemon { pokedexID = 1, name = null, type = "Grass" };
        private readonly Pokemon _pokemonShortName = new Pokemon { pokedexID = 1, name = "B", type = "Grass" };
        private readonly Pokemon _pokemonInvalidType = new Pokemon { pokedexID = 1, name = "Bulbasaur", type = "Milk" };

        [TestMethod()]
        public void ToStringTest()
        {
            string str = _pokemon.ToString();
            Assert.AreEqual("PokedexID: 1, Name: Bulbasaur, Type: Grass", str);
           
        }

        [TestMethod()]
        public void ValidateNameTest()
        {
            _pokemon.ValidateName();
            Assert.ThrowsException<ArgumentNullException>(() => _pokemonNullName.ValidateName());
            Assert.ThrowsException<ArgumentException>(() => _pokemonShortName.ValidateName());
        }

        [TestMethod()]
        public void ValidateTypeTest()
        {
            _pokemon.ValidateType();
            Assert.ThrowsException<ArgumentException>(() => _pokemonInvalidType.ValidateType());
        }

        [TestMethod()]
        public void ValidateTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => _pokemonNullName.Validate());
            Assert.ThrowsException<ArgumentException>(() => _pokemonShortName.Validate());
            Assert.ThrowsException<ArgumentException>(() => _pokemonInvalidType.Validate());
        }
    }
}