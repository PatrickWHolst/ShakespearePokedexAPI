using ShakespearePokedexAPI.Models;
using ShakespearePokedexAPI.Interfaces;
using System.Text.Json;

namespace ShakespearePokedexAPI.Services
{
    /// <summary>
    /// Implementation of the IPokemonService interface for fetching Pokémon species data.
    /// </summary>
    public class PokemonService : IPokemonService
    {
        private readonly HttpClient _client;
        private readonly ITranslationService _translationService;

        public PokemonService(HttpClient client, ITranslationService translationService)
        {
            _client = client;
            _translationService = translationService;
        }

        public async Task<PokeSpecies> GetPokemonSpeciesAsync(string nameOrId)
        {
            // Call the PokeAPI to get the Pokémon species data by name or ID
            var response = await _client.GetAsync($"pokemon-species/{nameOrId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jason = await response.Content.ReadAsStringAsync();
            var root = JsonDocument.Parse(jason).RootElement;

            // Extract the flavor text entries from the JSON response
            var flavorEntries = root.GetProperty("flavor_text_entries");
            var flavorText = flavorEntries
                .EnumerateArray()
                .FirstOrDefault(e => e.GetProperty("language").GetProperty("name").GetString() == "en")
                .GetProperty("flavor_text").GetString();

            // Clean up the flavor text by removing newlines and form feeds
            flavorText = flavorText.Replace("\n", " ").Replace("\f", " ").Trim();

            // Translate the flavor text to Shakespearean English
            var translatedText = await _translationService.TranslateToShakespeareAsync(flavorText);

            return new PokeSpecies
            {
                Id = root.GetProperty("id").GetInt32(),
                Name = root.GetProperty("name").GetString(),
                FlavorText = translatedText
            };
        }
    }
}