using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models;
using SchoolRepairSystem.Models.ViewModels;

namespace SchoolRepairSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    public class UserRoleController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRolesService _rolesService;

        public UserRoleController(IUsersService usersService,IUserRoleService userRoleService,IRolesService rolesService)
        {
            _usersService = usersService;
            _userRoleService = userRoleService;
            _rolesService = rolesService;
        }
        /// <summary>
        /// 赋角色，改角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("empowerment")]
        [Authorize(Policy = "Admin")]
        public async Task<ResponseMessage<bool>> UpdateUserRole([FromBody]UserRoleViewModel model)
        {
            //改，有bug,2个id为0，需要先检索userid再修改roleid
            if (!ModelState.IsValid)
            {
                return new ResponseMessage<bool>()
                {
                    Msg = "请求失败,参数错误",
                    Success = false,
                };
            }
            else
            {
                UserRole userRole =  _userRoleService.Query(x => x.UserId == model.UserId && !x.IsRemove)?.Result;
                if (userRole != null)
                {
                    userRole.RoleId = model.RoleId;
                    bool update = await _userRoleService.Update(userRole);
                    return new ResponseMessage<bool>()
                    {
                        Msg = "请求成功",
                        Status = 200,
                        Success = true,
                        ResponseInfo = update
                    };
                }
                return new ResponseMessage<bool>()
                {
                    Msg = "修改失败，找不到该用户",
                    Success = false
                };
                
            }
            
        }
        /// <summary>
        /// 删除用户的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("SeizingPower")]
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ResponseMessage<bool>> DeleteUserRole(long userId)
        {
            UserRole userRole = _userRoleService.Query(x => x.UserId == userId&&!x.IsRemove)?.Result;
            if (userRole!=null)
            {
                userRole.IsRemove = false;
                bool update = await _userRoleService.Update(userRole);
                return update
                    ? new ResponseMessage<bool>()
                    {
                        Msg = "删除成功",
                        Success = true,
                        Status = 200,
                        ResponseInfo = true
                    }
                    : new ResponseMessage<bool>()
                    {
                        Msg = "删除失败",
                        Success = false
                    };
            }
            return new ResponseMessage<bool>()
            {
                Msg = "未找到，可能已删除",
                Success = false,
            };
            
        }


    }
}
