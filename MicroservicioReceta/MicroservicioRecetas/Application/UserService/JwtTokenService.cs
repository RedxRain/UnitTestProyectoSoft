using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.UserService
{
    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Generartoken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])); //Clave convertida en arreglo de bytes, se utiliza para firmar el token, 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //Combina la clave secreta con un algoritmo de firma.

            var claims = new[] //Info adicional, pares-valor, una es para asignar un nombre, la otra asigna un indentificador.
            {
                new Claim(JwtRegisteredClaimNames.Sub, "MicroservicioRecetas"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken( //Configuracion del token.
                issuer: "Recetas", //Emisor.
                audience: "Ingredientes", //Receptor.
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), //Tiempo de vida.
                signingCredentials: creds //Firma.
                );

            return new JwtSecurityTokenHandler().WriteToken(token); //Serializa el token en formato JWT y lo retorna como cadena.
        }
    }
}
