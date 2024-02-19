﻿using AuthAPI.Models;
using AuthAPI.Services.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthAPI.Services.TokenManager
{
    public class TokenManager : ITokenManager
    {

        private readonly JwtOptions jwtOptions;

        public TokenManager(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
        }

        public string GenerateToken(RegisteredUser registeredUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(jwtOptions.Secret);

            var context = new AuthContext();

            var roles = context.Roles;

            var claimList = new List<Claim>
            {
                //Mi legyen benne a Token-ben
                new Claim("userId",registeredUser.Userid),
                new Claim("username",registeredUser.Username),
                new Claim("userRole",roles.FirstOrDefault(r_id => r_id.Roleid == registeredUser.Roleid)!.RoleName),
                new Claim("userRegdate",registeredUser.Regdate.ToString())
            };

            var tokenDescription = new SecurityTokenDescriptor //Token beállításai
            {
                Audience = jwtOptions.Audience,
                Issuer = jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        public JwtSecurityToken JwtDecode(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            return jwtHandler.ReadJwtToken(token);
        }

        public void blackListToken(string token)
        {
            var context = new AuthContext();

            var tokenData = JwtDecode(token);

            context.Add(new BlacklistedToken
            {
                TokenId = Guid.NewGuid().ToString(),
                Token = token,
                BlacklistedStatusExpires = tokenData.ValidTo
            });
            context.SaveChanges();
        }
    }
}