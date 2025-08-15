using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShakespearePokedexAPI.Controllers;
using ShakespearePokedexAPI.Interfaces;
using ShakespearePokedexAPI.Models;
using ShakespearePokedexAPI.Services;


namespace ShakespearePokedexAPI.Tests.Controllers
{
    public class PokemonControllerTests
    {
        private readonly Mock<IPokemonService> _pokemonServiceMock;
        private readonly Mock<ITranslationService> _translationServiceMock;
        private readonly PokemonController _controller;

        public PokemonControllerTests()
        {
            // Mocking the services
            _pokemonServiceMock = new Mock<IPokemonService>();
            _translationServiceMock = new Mock<ITranslationService>();
            _controller = new PokemonController(_pokemonServiceMock.Object, _translationServiceMock.Object);
        }

        //Method given expect convention
        [Fact]
        public async Task GetPokemonSpecies_ReturnsOK_WhenPokemonFound()
        {
            // Arrange
            var Pokemon = new PokeSpecies
            {
                Id = 25,
                Name = "pikachu",
                FlavorText = "Electric mouse Pokémon."
            };

            _pokemonServiceMock
                .Setup(service => service.GetPokemonSpeciesAsync("pikachu"))
                .ReturnsAsync(Pokemon);

            _translationServiceMock
                .Setup(service => service.TranslateToShakespeareAsync(Pokemon.FlavorText))
                .ReturnsAsync("Electric mouse, thou art a Pokémon.");

            // Act
            var result = await _controller.GetPokemonSpecies("pikachu");


            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value;

            var name = value.GetType().GetProperty("Name")?.GetValue(value, null);
            var flavorText = value.GetType().GetProperty("FlavorText")?.GetValue(value, null);

            name.Should().Be("pikachu");
            flavorText.Should().Be("Electric mouse, thou art a Pokémon.");

        }

        [Fact]
        public async Task GetPokemonSpecies_ReturnsNotFound_WhenPokemonNotFound()
        {
            // Arrange
            _pokemonServiceMock
                .Setup(service => service.GetPokemonSpeciesAsync("unknown"))
                .ReturnsAsync((PokeSpecies)null);
            // Act
            var result = await _controller.GetPokemonSpecies("unknown");
            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }


        [Fact]
        public async Task GetPokemonSpecies_ReturnsOk_WhenPokemonFoundById()
        {
            // Arrange
            var pokemon = new PokeSpecies
            {
                Id = 25,
                Name = "pikachu",
                FlavorText = "Electric mouse Pokémon."
            };

            _pokemonServiceMock
                .Setup(s => s.GetPokemonSpeciesAsync("25"))
                .ReturnsAsync(pokemon);

            _translationServiceMock
                .Setup(s => s.TranslateToShakespeareAsync(pokemon.FlavorText))
                .ReturnsAsync("Electric mouse, thou art a Pokémon.");

            // Act
            var result = await _controller.GetPokemonSpecies("25");

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var value = okResult.Value;

            var name = value.GetType().GetProperty("Name")?.GetValue(value, null);
            var flavorText = value.GetType().GetProperty("FlavorText")?.GetValue(value, null);
            var id = value.GetType().GetProperty("Id")?.GetValue(value, null);

            name.Should().Be("pikachu");
            id.Should().Be(25);
            flavorText.Should().Be("Electric mouse, thou art a Pokémon.");
        }
        [Fact]
        public async Task GetPokemonSpecies_ReturnsNotFound_WhenPokemonDoesNotExist()
        {
            // Arrange
            _pokemonServiceMock
                .Setup(s => s.GetPokemonSpeciesAsync("missingpokemon"))
                .ReturnsAsync((PokeSpecies?)null);

            // Act
            var result = await _controller.GetPokemonSpecies("missingpokemon");

            // Assert
            var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            notFoundResult.Value.Should().Be("Your Pokemon Was Not Found.");
        }
    }
}
