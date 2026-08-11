using DotNetEnv;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Carregar env
Env.Load();

string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

// Configura política de CORS ampla para acesso do frontend
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configura Swagger para suportar JWT Bearer Token nos testes manuais
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT desta forma: Bearer {seu_token}"
    });
    c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        // Nova sintaxe obrigatória para o Swashbuckle v10+
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

//// Registrar DbContext com a conexão SQL Server scaffolded
//builder.Services.AddDbContext<ChamaJussaContext>(options =>
//    options.UseSqlServer(connectionString));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
