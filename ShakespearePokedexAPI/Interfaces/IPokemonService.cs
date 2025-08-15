using ShakespearePokedexAPI.Models;


namespace ShakespearePokedexAPI.Services
{

    /// <summary>
    /// Service interface for fetching Pokémon species data.
    ///  </summary>
    public interface IPokemonService
    {
        /// <summary>
        /// Fetches Pokémon species data by name or ID.
        /// </summary>
        /// <param name="nameOrId"></param>
        /// <returns></returns>
        Task<PokeSpecies> GetPokemonSpeciesAsync(string nameOrId);
    }
}
