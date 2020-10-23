using System.Collections.Generic;
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

        public UserController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
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
        public ResponseMessage<List<UserViewModel>> GetUserList(int pageNum,int size)
        {
            List<Users> userList = _usersService.QueryPagingByExp(x=>!x.IsRemove,pageNum,size)?.Result;
            return new ResponseMessage<List<UserViewModel>>()
            {
                Msg = "请求成功",
                Status = 200,
                Success = true,
                ResponseInfo = _mapper.Map<List<UserViewModel>>(userList)
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
                Users verifyPresenceUser = _usersService.Query(x => x.UserName == model.UserName)?.Result;
                if (verifyPresenceUser!=null)
                {
                    return new ResponseMessage<UserViewModel>()
                    {
                        Msg = "用户名存在！更改失败",
                        Success = false
                    };
                }
                Users user = await _usersService.QueryById(model.Id);
                user.UserName = model.UserName;
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
