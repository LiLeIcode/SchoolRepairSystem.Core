using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace SchoolRepairSystem.Extensions.Authorizations
{
    public class PermissionRequirement: IAuthorizationRequirement
    {

        /// <summary>
        /// 只验证一个role
        /// </summary>
        public string RoleName { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string ClaimType { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public TimeSpan TimeSpan { get; set; }

        public PermissionRequirement(string roleName,string issuer,string audience, string claimType, SigningCredentials signingCredentials, TimeSpan timeSpan)
        {
            RoleName = roleName;
            ClaimType = claimType;
            Issuer = issuer;
            Audience = audience;
            SigningCredentials = signingCredentials;
            TimeSpan = timeSpan;
        }
    }
}