using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using System.Text.Json.Serialization;
using ServSegFacilitiesAPI.Application.Autenticacao;
using ServSegFacilitiesAPI.Application.Services;
using ServSegFacilitiesAPI.Contexts;
using ServSegFacilitiesAPI.Interfaces;
using ServSegFacilitiesAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Carregar variáveis de ambiente do .env
Env.Load();
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")!;

// Add Controllers + Tratamento de ciclo de referência JSON
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configura Swagger com suporte a Token JWT Bearer (Compatível com Swashbuckle v10+)
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
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

// Registrar DbContext
builder.Services.AddDbContext<ServSeg_FacilitiesContext>(options =>
    options.UseSqlServer(connectionString));

// Injeção de Dependência - Repositórios
builder.Services.AddScoped<ICargoRepository, CargoRepository>();

// Injeção de Dependência - Serviços e Utilitários
builder.Services.AddScoped<CargoService>();
builder.Services.AddScoped<GeradorTokenJwt>();
builder.Services.AddScoped<AutenticacaoService>();

// Configuração do JWT Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var chave = builder.Configuration["Jwt:Key"]!;
        var issuer = builder.Configuration["Jwt:Issuer"]!;
        var audience = builder.Configuration["Jwt:Audience"]!;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave))
        };
    });

var app = builder.Build();

// Pipeline de Requisições HTTP
app.UseCors("CorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Ativação dos middlewares de segurança
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();