using Microsoft.AspNetCore.Mvc;
using ShakespearePokedexAPI.Interfaces;
using ShakespearePokedexAPI.Services;

namespace ShakespearePokedexAPI.Controllers
{

    /// <summary>
    /// Handles requests related to Pokémon species. including fetching species data and translating flavor text to Shakespearean English.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {

        private readonly IPokemonService _pokemonService;
        private readonly ITranslationService _translationService;

        public PokemonController(IPokemonService pokemonService, ITranslationService translationService)
        {
            _pokemonService = pokemonService;
            _translationService = translationService;
        }


        /// <summary>
        /// Handels the request to get a Pokémon species by its name or ID.
        /// </summary>
        /// <param name="nameOrId"></param>
        /// <returns></returns>
        [HttpGet("{nameOrId}")]
        public async Task<IActionResult> GetPokemonSpecies(string nameOrId)
        {

            // fetch the pokemon species by name or id
            var pokemon = await _pokemonService.GetPokemonSpeciesAsync(nameOrId);
            if (pokemon == null)
            {
                // if the pokemon is not found
                return NotFound("Your Pokemon Was Not Found.");
            }

            // translate the flavor text to Shakespearean English
            var translatedFlavorText = await _translationService.TranslateToShakespeareAsync(pokemon.FlavorText);
            //return the pokemon details along with the translated flavor text
            return Ok(new
            {
                pokemon.Id,
                pokemon.Name,
                FlavorText = translatedFlavorText
            });
        }

    }

}
