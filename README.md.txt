# Shakespeare Pokedex API

A .NET 9 Web API that fetches Pokémon species and translates their flavor text into Shakespearean English. 

---

## Features

- Fetch Pokémon species by **name** or **ID**.
- Translate flavor text into **Shakespearean English**.
- Dockerized for easy deployment.
- Swagger UI for interactive API documentation.
- Unit tests included using **Moq**.

## API Endpoints

### Get Pokémon by Name or ID

**Examples:**
```bash
# Using ID
GET http://localhost:5010/api/pokemon/25

# Using name
GET http://localhost:5010/api/pokemon/pikachu

# Dockerized
GET http://localhost:8080/api/pokemon/charmander


#Json Sample Response: 
{
  "Id": 25,
  "Name": "pikachu",
  "FlavorText": "Electric mouse, thou art a Pokémon."
}

Running Locally

    Install .NET 9 SDK.

    Navigate to the project folder.

    Restore dependencies:

bash:

dotnet restore
dotnet run --project ShakespearePokedexAPI
http://localhost:5010 // The API will be available at:
http://localhost:5010/swagger // Access Swagger UI for documentation at:


////

Running via Docker:

Build the Docker image:
docker build -t shakespeare-pokedex-api 

Run the container:
docker run -p 8080:8080 shakespeare-pokedex-api

API will be available at:
http://localhost:8080

##
Making Requests Using the HTTP File:
The provided HTTP file contains pre-defined requests to test the Shakespeare Pokedex API both locally and when running in Docker.
Simply open the .http file, click the “Send Request” button above any request, and you’ll see the JSON response from the API without having to manually type the URL or headers.
