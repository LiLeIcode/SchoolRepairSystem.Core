using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SchoolRepairSystem.Common.Helper;
using SchoolRepairSystem.Extensions.Authorizations;
using SchoolRepairSystem.IService;
using SchoolRepairSystemModels.ViewModels;

namespace SchoolRepairSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRolesService _rolesService;

        public LoginController(IUsersService usersService,IUserRoleService userRoleService,IRolesService rolesService)
        {
            _usersService = usersService;
            _userRoleService = userRoleService;
            _rolesService = rolesService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("GetToken")]
        [HttpPost]
        public async Task<ResponseMessage<TokenInfoViewModel>> PostToken([FromBody] LoginUser model)
        {
            if (ModelState.IsValid)
            {
                
                var user = _usersService.Query(x => x.UserName.Equals(model.UserName) && x.Password.Equals(model.Password)&&!x.IsRemove)?.Result;
                if (user != null)
                {
                    var userRole = await _userRoleService.Query(x => x.UserId == user.Id);
                    var roles = await _rolesService.Query(x => x.Id == userRole.RoleId);
                    string issuer = Appsettings.app(new[] { "PermissionRequirement", "Issuer" });
                    string audience = Appsettings.app(new[] { "PermissionRequirement", "Audience" });
                    string signingKey = Appsettings.app(new[] { "PermissionRequirement", "SigningCredentials" });
                    var keyByteArray = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(signingKey));
                    var signingCredentials = new SigningCredentials(keyByteArray, SecurityAlgorithms.HmacSha256);
                    Claim[] claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Jti,user.Id.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Role, roles.RoleName)
                    }.ToArray();
                    PermissionRequirement requirement = new PermissionRequirement(roles.RoleName,issuer,audience,ClaimTypes.Role,signingCredentials,TimeSpan.FromMinutes(60*60));

                    TokenInfoViewModel token = JwtToken.BuildJwtToken(claims, requirement);
                    return new ResponseMessage<TokenInfoViewModel>()
                    {
                        Msg = "请求成功",
                        Status = 200,
                        Success = true,
                        ResponseInfo = token
                    };
                }
                
            }
            return new ResponseMessage<TokenInfoViewModel>()
            {
                Msg = "请求失败",
                Success = false,
            };
        }
    }
}
