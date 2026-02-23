using Backend.Models;
using Backend.Services;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//builder.Services.AddSingleton<IPeopleService, People2Service>();
builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("People2Service");
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("PeopleService");



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");
builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomScoped");
builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomTransient");


builder.Services.AddScoped<IPostService, PostService>();
//HttpClientFactory es una fábrica de clientes HTTP que se utiliza
//para crear instancias de HttpClient de manera eficiente y reutilizable.
//Permite configurar y administrar la creación de clientes HTTP, lo que
//es especialmente útil para aplicaciones que realizan múltiples solicitudes HTTP
//a diferentes servicios o API. Al utilizar HttpClientFactory, se pueden evitar
//problemas comunes asociados con la gestión manual de instancias de HttpClient,
//como el agotamiento de sockets debido a la falta de liberación adecuada de recursos.
//Además, HttpClientFactory facilita la configuración centralizada de políticas
//de reintentos, tiempos de espera y otras opciones relacionadas con las solicitudes HTTP.
builder.Services.AddHttpClient<IPostService, PostService>(
    c =>
{
    // Es preferible apuntar a la raíz del servicio
    c.BaseAddress = new Uri(builder.Configuration["BaseurlPosts"]);
}
);

builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"))
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
