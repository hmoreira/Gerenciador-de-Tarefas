using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManager.API.DTO;

namespace TaskManager.API.Authentication
{
    public class TokenGenerator
    {
        public static TokenResponse GenerateJwt(UsuarioPutGet user, AuthenticationData authenticationData)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim("Nome", user.Nome));
            claims.Add(new Claim("Funcao", user.Funcao.ToString()));

            var tokenHandler = new JwtSecurityTokenHandler();
            var signingCredentials = EncryptSigningCredentials(authenticationData.SecretKey);

            var jwt = new JwtSecurityToken(
                issuer: authenticationData.Issuer,
                audience: authenticationData.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(authenticationData.HoursExpiration),
                signingCredentials: signingCredentials);

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(jwt);             

            var response = new TokenResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(authenticationData.HoursExpiration).TotalSeconds,
                User = user                
            };

            return response;
        }        

        private static SigningCredentials EncryptSigningCredentials(string secretKey)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            return new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        }
    }
}
