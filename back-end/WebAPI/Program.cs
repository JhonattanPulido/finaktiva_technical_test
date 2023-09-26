using Data;
using Models;
using Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region Mongo DB setup
builder.Services.AddScoped(_ => new MongoClient(
    connectionString: builder.Configuration["Database:ConnectionString"]));

builder.Services.AddScoped(provider =>
    provider.GetRequiredService<MongoClient>().GetDatabase(
        name: builder.Configuration["Database:Name"]));

builder.Services.AddScoped(provider =>
    provider.GetRequiredService<IMongoDatabase>().GetCollection<Log>(
        name: builder.Configuration["Database:Collections:Logs"])); // Logs collection injection
#endregion

builder.Services.AddScoped<ILogsData, LogsData>(); // Data layer injection
builder.Services.AddScoped<ILogsServices, LogsServices>(); // Services layer injection

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Auto Mapper

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

