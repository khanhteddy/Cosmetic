using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Cosmetic.Services
{
    public class BaoKimAPI
    {
        const string API_KEY = "a18ff78e7a9e44f38de372e093d87ca1";
        const string API_SECRET = "9623ac03057e433f95d86cf4f3bef5cc";
        const int TOKEN_EXPIRE = 60; //token expire time in seconds
        private static string jwt = null;

        public static string CreateTokenString()
        {
            Random rd = new Random();
            byte[] b = new byte[32];
            rd.NextBytes(b);
            string tokenID = Convert.ToBase64String(b);
            long issuedAt = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            long notBefore = issuedAt;
            long expire = notBefore + TOKEN_EXPIRE;

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(API_SECRET));
            // Also note that securityKey length should be > 256bit
            // so you have to make sure that your private key has a proper length

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a Header
            JwtHeader header = new JwtHeader(credentials);

            //Some PayLoad that contain information about the customer
            JwtPayload payload = new JwtPayload()
            {
                {"iat", issuedAt},
                {"jti", tokenID},
                {"iss", API_KEY},
                {"nbf", notBefore},
                {"exp", expire},
                {"form_params", new Dictionary<string, Object>(){

                }}
            };

            //Create a token
            JwtSecurityToken securityToken = new JwtSecurityToken(header, payload);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            // Token to String so you can use it in your client
            jwt = handler.WriteToken(securityToken);
            return jwt;
        }

        public static JwtSecurityToken GetToken()
        {
            // And finally when  you received token from client
            // you can either validate it or try to  read
            if (jwt == null)
            {
                CreateTokenString();
            }

            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                return handler.CreateJwtSecurityToken(jwt);
            }
            catch (Exception ex)
            {
                CreateTokenString();
                throw ex;
            }

        }
    }
}

