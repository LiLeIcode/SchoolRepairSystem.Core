using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models.ViewModels;

namespace SchoolRepairSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public RegisterController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        /// <summary>
        /// 普通注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<ResponseMessage<long>> PostAddUser([FromBody] RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                long id = await _usersService.CheckAdd(model.UserName, model.Password, model.Mail, model.Phone);
                if (id != -1)
                {
                    return new ResponseMessage<long>()
                    {
                        Msg = "注册成功",
                        Status = 200,
                        Success = true,
                        ResponseInfo = id
                    };
                }
                else
                {
                    return new ResponseMessage<long>()
                    {
                        Msg = "注册失败，当前用户名已存在",
                        Success = false,
                        ResponseInfo = id
                    };
                }
            }
            return new ResponseMessage<long>()
            {
                Msg = "注册失败，校验不通过",
                Success = false
            };
        }
    }
}
