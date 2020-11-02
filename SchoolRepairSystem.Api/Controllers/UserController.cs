using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

    public class UserController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;

        public UserController(IUsersService usersService, IMapper mapper, IUserRoleService userRoleService)
        {
            _usersService = usersService;
            _mapper = mapper;
            _userRoleService = userRoleService;
        }
        /// <summary>
        /// 获取所有用户账号信息
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("userList")]
        [Authorize(Policy = "Admin")]
        public async Task<ResponseMessage<List<UserAddRoleViewModel>>> GetUserList(int pageNum, int size)
        {
            List<Users> userList = _usersService.QueryPagingByExp(x => !x.IsRemove, pageNum, size)?.Result;
            List<UserRole> userRoles = await _userRoleService.QueryAll();
            List<UserAddRoleViewModel> response = new List<UserAddRoleViewModel>();
            if (userList != null)
            {
                foreach (Users users in userList)
                {
                    UserRole userRole = userRoles.First(x => x.UserId == users.Id);
                    response.Add(new UserAddRoleViewModel()
                    {
                        Id = users.Id,
                        UserName = users.UserName,
                        Password = users.Password,
                        Mail = users.Mail,
                        Phone = users.Phone,
                        RoleId = userRole.RoleId
                    });
                }
                return new ResponseMessage<List<UserAddRoleViewModel>>()
                {
                    Msg = "请求成功",
                    Status = 200,
                    Success = true,
                    ResponseInfo = response
                };
            }

            return new ResponseMessage<List<UserAddRoleViewModel>>()
            {
                Msg = "请求失败",
                Success = false,
            };
        }
        /// <summary>
        /// 更改user的数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updateUserInfo")]
        [Authorize(Policy = "Admin")]
        public async Task<ResponseMessage<UserViewModel>> UpdateUserInfo([FromBody] UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = await _usersService.QueryById(model.Id);
                user.Password = model.Password;
                user.Phone = model.Phone;
                user.Mail = model.Mail;
                bool update = await _usersService.Update(user);
                if (update)
                {
                    return new ResponseMessage<UserViewModel>()
                    {
                        Msg = "修改成功",
                        Status = 200,
                        Success = true,
                        ResponseInfo = _mapper.Map<UserViewModel>(user)
                    };
                }
                return new ResponseMessage<UserViewModel>()
                {
                    Msg = "修改失败",
                    Success = false
                };

            }
            return new ResponseMessage<UserViewModel>()
            {
                Msg = "验证不通过",
                Success = false
            };
        }


    }
}
