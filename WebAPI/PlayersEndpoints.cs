using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using WebAPI.Data;
using WebAPI.Model;
namespace WebAPI;

public static class PlayersEndpoints
{
    public static void MapPlayersEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Players").WithTags(nameof(Players));

        group.MapGet("/", async (WebAPIContext db) =>
        {
            return await db.Players.ToListAsync();
        })
        .WithName("GetAllPlayerss")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Players>, NotFound>> (int id, WebAPIContext db) =>
        {
            return await db.Players.FindAsync(id)
                is Players model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPlayersById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<NotFound, NoContent>> (int id, Players players, WebAPIContext db) =>
        {
            var foundModel = await db.Players.FindAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }
            
            db.Update(players);
            await db.SaveChangesAsync();

            return TypedResults.NoContent();
        })
        .WithName("UpdatePlayers")
        .WithOpenApi();

        group.MapPost("/", async (Players players, WebAPIContext db) =>
        {
            db.Players.Add(players);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Players/{players.Id}",players);
        })
        .WithName("CreatePlayers")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok<Players>, NotFound>> (int id, WebAPIContext db) =>
        {
            if (await db.Players.FindAsync(id) is Players players)
            {
                db.Players.Remove(players);
                await db.SaveChangesAsync();
                return TypedResults.Ok(players);
            }

            return TypedResults.NotFound();
        })
        .WithName("DeletePlayers")
        .WithOpenApi();
    }
}
