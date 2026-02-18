using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demo_WebAPI_EventAgenda.Presentation.WebAPI.Token
{
    // Utilitary class to generate a JWT(Json Web Token)
    // Token will identify the User
    public class TokenTool
    {
        // ↓ Injection of tools to acess config file

        private readonly IConfiguration _config;

        public TokenTool(IConfiguration config) 
        {
            _config = config;
       
        }

        //↓ Represents the data in the token 
        public class Data 
        { 
            public required long MemberId { get; set; }
            public required string Role { get; set; }

        }


        // ↓ Method to generate the token 

        public string Generate(Data data) 
        {
            // Creation of Claims, which are the data that we want to include in the token
            Claim[] claims = [
               // new Claim("Clef", "La reponse est 42"),
                new Claim(ClaimTypes.NameIdentifier, data.MemberId.ToString()),
                new Claim(ClaimTypes.Role, data.Role)
                    
            ];

            // Creation of Signing Credentials
            string secret = _config["Token:Key"] ?? throw new Exception("Clef du token non défini dans l'env !");
            byte[] key = Encoding.UTF8.GetBytes(secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key); 
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);


            JwtSecurityToken token = new JwtSecurityToken(

                issuer: _config["Token:Issuer"],                          // The issuer of the token, in this case, the name of the application
                audience: _config["Token:Audience"],                              // The audience of the token, in this case, the user who will receive the token
                expires: DateTime.Now.AddMinutes(_config.GetValue<int>("Token:Expire")),       // The expiration time of the token, in this case, 180 minutes (3 hours) from the current time
                claims: claims,                              // The claims of the token, in this case, the data that we want to include in the token
                signingCredentials: signingCredentials       // The signing credentials of the token, in this case, the key and the algorithm that we will use to sign the token
            );

            // Returns the token in a form of a string 
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
            
        
        
        }
    }
}
