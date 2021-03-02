using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Infrastructure.Token
{
    /// <summary>
    /// Helper functions for Token generation
    /// </summary>
    public class TokenFunctions
    {
        /// <summary>
        /// Generate the token for user within the application
        /// </summary>
        /// <param name="userTokenData"></param>
        /// <param name="rememberMe"></param>
        /// <returns></returns>
        public static string generateUserToken(UserTokenData userTokenData, bool rememberMe = false) {
                        // Create token an sent;
            var claims = defaultClaim(userTokenData);

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("AUTHORIZATION_TOKEN")));
        
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = rememberMe ? RememberMeDescriptor(claims, creds) : defaultDescriptor(claims, creds);

            var tokenhandler = new JwtSecurityTokenHandler();

            var token = tokenhandler.CreateToken(tokenDescriptor);

            // var data = new {token = tokenhandler.WriteToken(token)};

            return tokenhandler.WriteToken(token);
        }

        /// <summary>
        /// Default token with a short timeframe because no remember me was enabled
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        public static SecurityTokenDescriptor defaultDescriptor(List<Claim> claims, SigningCredentials creds) {
            return new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
        }

        /// <summary>
        /// Longer timeframe token because remember me was enabled
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="creds"></param>
        /// <returns></returns>
        public static SecurityTokenDescriptor RememberMeDescriptor(List<Claim> claims, SigningCredentials creds) {
            return new SecurityTokenDescriptor(){
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(10),
                SigningCredentials = creds
            };
        }
        

        private static List<Claim> defaultClaim(UserTokenData userTokenData) {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userTokenData.UserId.ToString()),
                // new Claim(AppClaimTypes.userType.ToString(), userTokenData.User.Discriminator.ToString().ToLower()),
                // new Claim(AppClaimTypes.profilePicture.ToString(), userTokenData.ProfilePicture),
                // new Claim(AppClaimTypes.emojiPicture.ToString(), userTokenData.EmojiPicture)
            };



            foreach (var item in userTokenData.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            return claims;
        }
    }

    /// <summary>
    /// Custom claim types
    // /// </summary>
    // public enum AppClaimTypes {

    //     /// Type of user
    //     userType,

    //     /// Roles of user
    //     userRoles,

    //     /// Profile Picture
    //     profilePicture,

    //     /// Emoji Picture
    //     emojiPicture

    // }
}