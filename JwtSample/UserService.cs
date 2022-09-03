using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtSample
{
    internal class UserService
    {
        private IDictionary<string, string> _users = new Dictionary<string, string>()
        {
            {"root1", "test"},
            {"root2", "test"},
            {"root3", "test"},
            {"root4", "test"}
        };

        private const string SecretCode = "jEw5g8qc+2B?F)G-";

        public string Authenticate(string user, string password)
        {
            if (string.IsNullOrWhiteSpace(user) ||
           string.IsNullOrWhiteSpace(password))
            {
                return string.Empty;
            }

            int i = 0;
            foreach (KeyValuePair<string, string> pair in _users)
            {
                i++;
                if (string.CompareOrdinal(pair.Key, user) == 0 &&
                string.CompareOrdinal(pair.Value, password) == 0)
                {
                    return GenerateJwtToken(i);
                }
            }

            return String.Empty;
        }

        private string GenerateJwtToken(int id)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(SecretCode);

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, id.ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);


        }



    }
}
