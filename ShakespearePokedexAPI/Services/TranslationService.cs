using Microsoft.Extensions.Caching.Memory;
using ShakespearePokedexAPI.Interfaces;
using System.Text;
using System.Text.Json;




namespace ShakespearePokedexAPI.Services
{
    /// <summary>
    /// Translation service for converting text to Shakespearean English using an external API.
    /// </summary>
    public class TranslationService : ITranslationService
    {
        // cache for the translation
        private readonly HttpClient _client;
        private readonly IMemoryCache _cache;

        public TranslationService(HttpClient client, IMemoryCache cache)
        {
            _client = client;
            _cache = cache;
        }


        public async Task<string> TranslateToShakespeareAsync(string text)
        {
            // Check if the translation is already cached
            if (_cache.TryGetValue(text, out string cachedTranslation))
            {
                return cachedTranslation!;
            }

            var payload = new { text = text };
            var jsonContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            // send the request to the external API to translate the text to Shakespearean English
            var response = await _client.PostAsync("translate/shakespeare.json", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonDoc = JsonDocument.Parse(jsonResponse);
                var translated = jsonDoc.RootElement.GetProperty("contents").GetProperty("translated").GetString();

                // Cache the translated text for 1 hour
                _cache.Set(text, translated, TimeSpan.FromHours(1));

                return translated!;
            }
            else
            {
                return text; // stuff failed, return to default text
            }

        }
    }


}
