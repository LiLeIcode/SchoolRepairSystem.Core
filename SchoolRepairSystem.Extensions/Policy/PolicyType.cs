using System;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SchoolRepairSystem.Common.Helper;
using SchoolRepairSystem.Extensions.Authorizations;

namespace SchoolRepairSystem.Extensions.Policy
{
    /// <summary>
    /// 这里是写死了，如果想修改rolename，需要更改
    /// </summary>
    public class PolicyType
    {
        static string issuer = Appsettings.app(new[] { "PermissionRequirement", "Issuer" });
        static string audience = Appsettings.app(new[] { "PermissionRequirement", "Audience" });
        static string signingKey = Appsettings.app(new[] { "PermissionRequirement", "SigningCredentials" });
        static SymmetricSecurityKey keyByteArray = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKey));
        static SigningCredentials signingCredentials = new SigningCredentials(keyByteArray, SecurityAlgorithms.HmacSha256);
        //roleName对应数据库的roleName
        /// <summary>
        /// 管理员
        /// </summary>
        /// <returns></returns>
        public static PermissionRequirement AdminPolicy()
        {
            PermissionRequirement permissionRequirement = new PermissionRequirement(
                roleName: "管理员",
                claimType: ClaimTypes.Role,
                audience: audience,
                issuer: issuer,
                signingCredentials: signingCredentials,
                timeSpan: TimeSpan.FromSeconds(60 * 60));
            return permissionRequirement;
        }
        /// <summary>
        /// 木工
        /// </summary>
        /// <returns></returns>
        public static PermissionRequirement CarpentryPolicy()
        {
            PermissionRequirement permissionRequirement = new PermissionRequirement(
                roleName: "木工",
                claimType: ClaimTypes.Role,
                audience: audience,
                issuer: issuer,
                signingCredentials: signingCredentials,
                timeSpan: TimeSpan.FromSeconds(60 * 60));
            return permissionRequirement;
        }

        /// <summary>
        /// 电工
        /// </summary>
        /// <returns></returns>
        public static PermissionRequirement ElectricianPolicy()
        {
            PermissionRequirement permissionRequirement = new PermissionRequirement(
                roleName: "电工",
                claimType: ClaimTypes.Role,
                audience: audience,
                issuer: issuer,
                signingCredentials: signingCredentials,
                timeSpan: TimeSpan.FromSeconds(60 * 60));
            return permissionRequirement;
        }
        /// <summary>
        /// 电工木工
        /// </summary>
        /// <returns></returns>
        public static PermissionRequirement ElectricianAndCarpentryPolicy()
        {
            PermissionRequirement permissionRequirement = new PermissionRequirement(
                roleName: "电工,木工",
                claimType: ClaimTypes.Role,
                audience: audience,
                issuer: issuer,
                signingCredentials: signingCredentials,
                timeSpan: TimeSpan.FromSeconds(60 * 60));
            return permissionRequirement;
        }

        /// <summary>
        /// 普通用户
        /// </summary>
        /// <returns></returns>
        public static PermissionRequirement OrdinaryPolicy()
        {
            PermissionRequirement permissionRequirement = new PermissionRequirement(
                roleName: "普通用户",
                claimType: ClaimTypes.Role,
                audience: audience,
                issuer: issuer,
                signingCredentials: signingCredentials,
                timeSpan: TimeSpan.FromSeconds(60 * 60));
            return permissionRequirement;
        }

        /// <summary>
        /// 测试，管理员和普通用户
        /// </summary>
        /// <returns></returns>
        public static PermissionRequirement AdminAndOrdinaryPolicy()
        {
            PermissionRequirement permissionRequirement = new PermissionRequirement(
                roleName: "普通用户,管理员",
                claimType: ClaimTypes.Role,
                audience: audience,
                issuer: issuer,
                signingCredentials: signingCredentials,
                timeSpan: TimeSpan.FromSeconds(60 * 60));
            return permissionRequirement;
        }


        /// <summary>
        /// 管理员，两个职工
        /// </summary>
        /// <returns></returns>
        public static PermissionRequirement AdminAndOrdinaryAndElectricianPolicy()
        {
            PermissionRequirement permissionRequirement = new PermissionRequirement(
                roleName: "电工,木工,管理员",
                claimType: ClaimTypes.Role,
                audience: audience,
                issuer: issuer,
                signingCredentials: signingCredentials,
                timeSpan: TimeSpan.FromSeconds(60 * 60));
            return permissionRequirement;
        }
    }
}