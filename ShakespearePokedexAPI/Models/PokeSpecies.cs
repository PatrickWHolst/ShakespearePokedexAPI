namespace ShakespearePokedexAPI.Models
{

    /// <summary>
    /// Represents a Pokémon species with its ID, name, and flavor text.
    /// </summary>
    public record PokeSpecies
    {
        public int Id { get; init; } // init da man ikke skal kunne ændre værdierne efter kald af pokemon
        public string Name { get; init; }
        public string FlavorText { get; init; }

    }
}
