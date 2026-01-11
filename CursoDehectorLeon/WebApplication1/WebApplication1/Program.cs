var builder = WebApplication.CreateBuilder(args);

// 1. Agregamos los servicios necesarios para Swagger (lo que pide el curso)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Este es el que instalaste recién

var app = builder.Build();

// 2. Configuramos la interfaz visual
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   // Genera el JSON
    app.UseSwaggerUI(); // Crea la página web azul que quieres ver
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();