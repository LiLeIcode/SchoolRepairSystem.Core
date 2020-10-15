using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SchoolRepairSystem.Models.ViewModels;

namespace SchoolRepairSystem.Extensions.Authorizations
{
    public class JwtToken
    {
        public static TokenInfoViewModel BuildJwtToken(Claim[] claim,PermissionRequirement permissionRequirement)
        {
            DateTime now = DateTime.Now;
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: permissionRequirement.Issuer,
                audience: permissionRequirement.Audience,
                claims: claim, 
                notBefore: now, 
                expires: now.Add(permissionRequirement.TimeSpan),
                signingCredentials:permissionRequirement.SigningCredentials);

            string buildToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenInfoViewModel()
            {
                token = buildToken,
                expires_in = permissionRequirement.TimeSpan.TotalSeconds,
                token_type = "Bearer"

            };
        }
    }
}