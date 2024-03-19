using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokemonRepositoryLib;
using PokemonRepositoryLibTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;

namespace PokemonRepositoryLib.Tests
{
    [TestClass()]
    public class PokemonRepositoryDBTests
    {
        private const bool useDatabase = true;
        private static IPokemonsRepository _repo;


        [ClassInitialize]
        public static void InitOnce(TestContext context)
        {
            if (useDatabase)
            {
                var optionsBuilder = new DbContextOptionsBuilder<PokemonDbContext>();

                optionsBuilder.UseSqlServer(Secrets.ConnectionStringSimply);

                PokemonDbContext _dbContext = new(optionsBuilder.Options);
                // clean database table: remove all rows
                _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Pokemons");
                _repo = new PokemonRepositoryDB(_dbContext);
            }
            //else
            //{
            //    _repo = new PokemonRepository();
            //}
        }

        [TestMethod()]
        public void PokemonRepositoryDBTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddPokemonTest()
        {
            _repo.AddPokemon(new Pokemon { name = "Vu", type = "Fire" });
            Pokemon gengar = _repo.AddPokemon(new Pokemon { name = "Gengar", type = "Ghost" });
            Assert.IsTrue(gengar.pokedexID >= 0);
            IEnumerable<Pokemon> all = _repo.GetPokemons();
            Assert.AreEqual(2, all.Count());

            Assert.ThrowsException<ArgumentNullException>(
                () => _repo.AddPokemon(new Pokemon { name = null, type = "Fire" }));
            Assert.ThrowsException<ArgumentException>(
                () => _repo.AddPokemon(new Pokemon { name = "", type = "Fire" }));
            //Assert.ThrowsException<ArgumentOutOfRangeException>(
            //    () => _repo.AddPokemon(new Pokemon { name = "Bu", type = "Water" }));
        }

        [TestMethod()]
        public void DeletePokemonTest()
        {
            Pokemon p = _repo.AddPokemon(new Pokemon { name = "Magma", type = "Fire" });
            Pokemon? pokemon = _repo.DeletePokemon(p.pokedexID);
            Assert.IsNotNull(pokemon);
            Assert.AreEqual("Magma", pokemon.name);

            Pokemon? pokemon2 = _repo.DeletePokemon(p.pokedexID);
            Assert.IsNull(pokemon2);
        }

        [TestMethod()]
        public void GetPokemonsTest()
        {
            IEnumerable<Pokemon> pokemons = _repo.GetPokemons(orderBy: "name");

            Assert.AreEqual(pokemons.First().name, "Gengar");

            pokemons = _repo.GetPokemons(orderBy: "type");
            Assert.AreEqual(pokemons.First().name, "Vu");

            pokemons = _repo.GetPokemons(nameIncludes: "ngar");
            Assert.AreEqual(1, pokemons.Count());
            Assert.AreEqual(pokemons.First().name, "Gengar");
        }

        [TestMethod()]
        public void GetPokemonByIDTest()
        {
            Pokemon p = _repo.AddPokemon(new Pokemon { name = "Alakazam", type = "Psychic" });
            Pokemon? pokemon = _repo.GetPokemonByID(p.pokedexID);
            Assert.IsNotNull(pokemon);
            Assert.AreEqual("Alakazam", pokemon.name);
            Assert.AreEqual("Psychic", pokemon.type);

            Assert.IsNull(_repo.GetPokemonByID(-1));
        }

        [TestMethod()]
        public void UpdatePokemonTest()
        {
            Pokemon p = _repo.AddPokemon(new Pokemon { name = "Metapod", type = "Bug" });
            Pokemon? pokemon = _repo.UpdatePokemon(p.pokedexID, new Pokemon { name = "Geodude", type = "Rock" });
            Assert.IsNotNull(pokemon);
            Pokemon? pokemon2 = _repo.GetPokemonByID(p.pokedexID);
            Assert.AreEqual("Geodude", pokemon.name);

            Assert.IsNull(
                _repo.UpdatePokemon(-1, new Pokemon { name = "Starmie", type = "Water" }));
            Assert.ThrowsException<ArgumentException>(
                () => _repo.UpdatePokemon(p.pokedexID, new Pokemon { name = "", type = "Electric" }));
        }
    }
}