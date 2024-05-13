using ebooks_dotnet7_api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.AspNetCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("ebooks"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
 config.DocumentName = "eBooksAPI";
 config.Title = "eBooksAPI v1";
 config.Version = "v1";
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
 app.UseOpenApi();
 app.UseSwaggerUi(config =>
 {
 config.DocumentTitle = "eBooksAPI";
 config.Path = "/swagger";
 config.DocumentPath =
"/swagger/{documentName}/swagger.json";
 config.DocExpansion = "list";
 });
}
var ebooks = app.MapGroup("api/ebook");

// TODO: Add more routes    
ebooks.MapPost("", CrearEBookAsync);
ebooks.MapGet("", ObtenerTodosDisponiblesAsync);
ebooks.MapPut("/{id}", ActualizarEBookAsync);
ebooks.MapPut("/{id}/change-availability", CambiarDisponibilidadAsync);
ebooks.MapPut("/{id}/increment-stock", AumentarStockAsync);
ebooks.MapPost("/purchase", RealizarCompraAsync);
ebooks.MapDelete("/{id}", EliminarEBookAsync);


app.Run();

// TODO: Add more methods
async Task<IResult> CrearEBookAsync(EBook eBook, DataContext context)
{
    context.EBooks.Add(eBook);
    await context.SaveChangesAsync();
    return Results.Created($"/todoitems/{eBook.Id}", eBook);
}
async Task<IResult> ActualizarEBookAsync(int id, ActualizarDto actualizarDto, DataContext context)
{
    var eBook = await context.EBooks.FindAsync(id);
    if (eBook is null) 
    return TypedResults.NotFound();

    eBook.Title = actualizarDto.Title;
    await context.SaveChangesAsync();
    return TypedResults.Ok(eBook);
}

async Task<IResult> ObtenerTodosDisponiblesAsync(DataContext context)
{
    return TypedResults.Ok(await context.EBooks.ToArrayAsync());
}
async Task<IResult> CambiarDisponibilidadAsync(int id,DataContext context)
{
    var eBook = await context.EBooks.FindAsync(id);
    if (eBook is null) 
    return TypedResults.NotFound();
    
    if(eBook.IsAvailable == true){
        eBook.IsAvailable = false;
    }else{
        eBook.IsAvailable = true;
    }
    
    await context.SaveChangesAsync();
    return TypedResults.Ok(eBook);
}
async Task<IResult> AumentarStockAsync(int id, IncrementarStockDto incrementarStockDto, DataContext context)
{
    var eBook = await context.EBooks.FindAsync(id);
    if (eBook is null) 
    return TypedResults.NotFound();

    eBook.Stock = incrementarStockDto.Stock;
    await context.SaveChangesAsync();

    return TypedResults.Ok(eBook);
}
async Task<IResult> RealizarCompraAsync(CompraDto compraDto, DataContext context)
{
    return TypedResults.Ok(compraDto);
}
async Task<IResult> EliminarEBookAsync(int id, DataContext context)
{
    if (await context.EBooks.FindAsync(id) is EBook eBook)
    {
    context.EBooks.Remove(eBook);
    await context.SaveChangesAsync();
    return TypedResults.NoContent();
    }
    return TypedResults.NotFound();
}