using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using System.Text.Json.Serialization;
using ServSegFacilitiesAPI.Application.Autenticacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using ServSegFacilitiesAPI.Application.Services;
using ServSegFacilitiesAPI.Contexts;
using ServSegFacilitiesAPI.Interfaces;
using ServSegFacilitiesAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Carregar variáveis do arquivo .env
Env.Load();
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")!;

// Controllers + Solução para evitar loop de JSON (Ciclos de Objeto)

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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

//// Registrar DbContext com a conexão SQL Server scaffolded
builder.Services.AddDbContext<ServSeg_FacilitiesContext>(options =>
    options.UseSqlServer(connectionString));

// Registro de injeção de dependencia de DI
// Repositories
 builder.Services.AddScoped<ITipoRegistro, TipoRegistroRepository>();
builder.Services.AddScoped<IRegistroPonto, RegistroPontoRepository>();
builder.Services.AddScoped<ICargoRepository, CargoRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<ILocalizacaoEmpresaRepository, LocalizacaoEmpresaRepository>();


// Registros de Injeção de Dependência (DI)
// Services
builder.Services.AddScoped<TipoRegistroService>();
builder.Services.AddScoped<RegistroPontoService>();
builder.Services.AddScoped<CargoService>();
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<GeradorTokenJwt>();
builder.Services.AddScoped<AutenticacaoService>();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
// Registrar DbContext
builder.Services.AddDbContext<ServSeg_FacilitiesContext>(options =>
    options.UseSqlServer(connectionString));


// Configuração da Autenticação JWT Bearer
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
builder.Services.AddScoped<LocalizacaoEmpresaService>();

var app = builder.Build();

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();