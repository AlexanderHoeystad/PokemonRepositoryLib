using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PokemonRepositoryLib
{
    public class Pokemon
    {
        [Key]
        public int pokedexID { get; set; }
        public string? name { get; set; }
        public string? type { get; set; }


        public override string ToString()
        {
            return $"PokedexID: {pokedexID}, Name: {name}, Type: {type}";
        }

        public void ValidateName()
        {
            if (name == null)
            {
                throw new ArgumentNullException("Name is null");
            }
            if (name.Length < 2)
            {
                throw new ArgumentException("Name must be atleast 2 character" + name);
            }
        }

        public void ValidateType()
        {
            switch (type.ToLower()) // Gør så det ikke er case sensitive
            {
                case "fire":
                case "water":
                case "grass":
                case "electric":
                case "psychic":
                case "rock":
                case "ground":
                case "normal":
                case "fighting":
                case "flying":
                case "poison":
                case "bug":
                case "ghost":
                case "steel":
                case "ice":
                case "dragon":
                case "fairy":
                    break;
                default:
                    throw new ArgumentException("Invalid type: " + type);
            }
        }

        public void Validate()
        {
            ValidateName();
            ValidateType();
        }


    }
}
