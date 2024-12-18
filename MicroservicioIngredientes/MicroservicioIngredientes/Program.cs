using Application.Interfaces;
using Application.UseCase;
using Infraestructure.Command;
using Infraestructure.Persistence;
using Infraestructure.Querys;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Custom
var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<IngredientesDBContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddScoped<IIngredienteCommand, IngredienteCommand>();
builder.Services.AddScoped<IIngredienteQuery, IngredienteQuery>();
builder.Services.AddScoped<IIngredienteService, IngredienteService>();

builder.Services.AddScoped<ITipoIngredienteCommand, TipoIngredienteCommand>();
builder.Services.AddScoped<ITipoIngredienteQuery, TipoIngredienteQuery>();
builder.Services.AddScoped<ITipoIngredienteService, TipoIngredienteService>();

builder.Services.AddScoped<ITipoMedidaCommand, TipoMedidaCommand>();
builder.Services.AddScoped<ITipoMedidaQuery, TipoMedidaQuery>();

builder.Services.AddAuthorization();
//Autentificaci�n JsonWebToken (JWT)
builder.Services.AddAuthentication("Bearer") //Establece que la api utilizar� autenticaci�n para proteger los endpoints.
    .AddJwtBearer(options => // Configuraci�n espec�fica para el esquema de autenticaci�n JWT Bearer.
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])); //matriz de baits a partir del Key
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature); //especificacion del algoritmo, uso el por defecto

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Token Invalido: {context.Exception.Message}");
                return Task.CompletedTask;
            }
        };
        options.TokenValidationParameters = new TokenValidationParameters //Reglas que se aplicar�n al validar los tokens.
        {
            ValidateIssuer = true, //Comprobar� que el token provenga de una fuente confiable.
            ValidateAudience = true, //Asegura que el token est� destinado a la aplicaci�n que lo est� recibiendo.
            ValidateLifetime = false, //Comprobar� si el token a�n es v�lido en funci�n de su fecha de expiraci�n.
            ValidateIssuerSigningKey = true, //Asegura que el token fue firmado con la clave que se espera y que no ha sido modificado.
            ValidIssuer = builder.Configuration["JWT:Issuer"], //Establece el valor esperado para el issuer del token.
            ValidAudience = builder.Configuration["JWT:Audience"], //Establece el valor esperado para la audience del token.
            IssuerSigningKey = signingKey //Establece la clave de firma que se usar� para validar el token.
        };
    });

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenLocalhost(5235);
    serverOptions.ListenLocalhost(7192, listenOptions => listenOptions.UseHttps());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); // Asegura que la autenticaci�n est� habilitada
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
