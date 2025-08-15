using ShakespearePokedexAPI.Interfaces;
using ShakespearePokedexAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on both local and container ports
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5010); // Local HTTP
    options.ListenAnyIP(8080); // Container HTTP
});

// Register services
builder.Services.AddHttpClient<IPokemonService, PokemonService>(client =>
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
});
builder.Services.AddHttpClient<ITranslationService, TranslationService>(client =>
{
    client.BaseAddress = new Uri("https://api.funtranslations.com/");
});


builder.Services.AddMemoryCache();
builder.Services.AddControllers();

// Register Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger middleware for all environments (can remove `true` in production)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shakespeare Pokedex API V1");
    c.RoutePrefix = string.Empty; // Swagger available at root
});

//app.UseHttpsRedirection(); // optional
app.UseAuthorization();

app.MapControllers();

app.Run();