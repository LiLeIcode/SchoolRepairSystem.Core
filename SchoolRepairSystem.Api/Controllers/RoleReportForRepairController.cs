using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models;
using SchoolRepairSystem.Models.ViewModels;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace SchoolRepairSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    public class RoleReportForRepairController : ControllerBase
    {
        private readonly IReportForRepairService _reportForRepairService;
        private readonly IRolesService _rolesService;
        private readonly IMapper _mapper;

        public RoleReportForRepairController(IReportForRepairService reportForRepairService, IRolesService rolesService, IMapper mapper)
        {
            _reportForRepairService = reportForRepairService;
            _rolesService = rolesService;
            _mapper = mapper;
        }
        /// <summary>
        /// 职工接受报修
        /// </summary>
        /// <param name="roleReportForRepairId"></param>
        /// <returns></returns>
        [Route("acceptRepairApplication")]
        [HttpGet]
        [Authorize(Policy = "ElectricianAndCarpentry")]//Carpentry Electrician
        public async Task<ResponseMessage<RoleReportForRepairViewModel>> UpdateRoleReportForRepair(long roleReportForRepairId)
        {
            ClaimsPrincipal principal = HttpContext.User;
            string roleName = principal.FindFirst(x => x.Type == ClaimTypes.Role).Value;
            string name = principal.FindFirst(x => x.Type == ClaimTypes.Name).Value;
            string jti = principal.FindFirst(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            Roles roles = await _rolesService.Query(x => x.RoleName.Equals(roleName));
            ReportForRepair reportForRepair = await _reportForRepairService.QueryById(roleReportForRepairId);
            if (reportForRepair.WaitHandle == 0)
            {
                reportForRepair.WorkerId = Convert.ToInt64(jti);
                reportForRepair.RoleId = roles.Id;
                reportForRepair.WaitHandle = 1;//1标志表示接受处理
                bool update = await _reportForRepairService.Update(reportForRepair);
                return update
                    ? new ResponseMessage<RoleReportForRepairViewModel>()
                    {
                        Msg = "接单成功",
                        Success = true,
                        Status = 200,
                        ResponseInfo = new RoleReportForRepairViewModel()
                        {
                            UserName = name,
                            Dorm = reportForRepair.Dorm,
                            Tung = reportForRepair.Tung,
                            Desc = reportForRepair.Desc,
                            Layer = reportForRepair.Layer
                        }
                    }
                    : new ResponseMessage<RoleReportForRepairViewModel>()
                    {
                        Msg = "接单失败",
                        Success = false
                    };


            }
            else
            {
                return new ResponseMessage<RoleReportForRepairViewModel>()
                {
                    Msg = "接单失败，该报修已被接受或已完成维修",
                };
            }

        }
        /// <summary>
        /// 查看自己接了哪些单
        /// </summary>
        /// <param name="pageNum">第几页</param>
        /// <param name="size">看几个</param>
        /// <returns></returns>
        [HttpGet]
        [Route("myTask")]
        [Authorize(Policy = "ElectricianAndCarpentry")]
        public ResponseMessage<List<ReportForRepairViewModel>> GetMyTask(int pageNum = 1, int size = 10)
        {
            string jti = HttpContext.User.FindFirst(x => x.Type == Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti)
                .Value;
            List<ReportForRepair> repairs = _reportForRepairService.QueryPagingByExp(x => x.WorkerId == Convert.ToInt64(jti) && !x.IsRemove, pageNum, size)?.Result;
            if (repairs!=null)
            {
                return new ResponseMessage<List<ReportForRepairViewModel>>()
                {
                    Msg = "请求成功",
                    Status = 200,
                    Success = true,
                    ResponseInfo = _mapper.Map<List<ReportForRepairViewModel>>(repairs)
                };
            }
            return new ResponseMessage<List<ReportForRepairViewModel>>()
            {
                Msg = "你没有任何任务",
                Success = true
            };
        }
        /// <summary>
        /// 查看自己接了哪些单未完成
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("myHITATask")]
        [Authorize(Policy = "ElectricianAndCarpentry")]
        public ResponseMessage<List<HITATaskViewModel>> GetMyHITATask()
        {
            string jti = HttpContext.User.FindFirst(x => x.Type == Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti)
                .Value;
            List<ReportForRepair> repairs = _reportForRepairService.QueryList(x => x.WorkerId == Convert.ToInt64(jti) && !x.IsRemove&&x.WaitHandle==1)?.Result;
            if (repairs != null)
            {
                return new ResponseMessage<List<HITATaskViewModel>>()
                {
                    Msg = "请求成功",
                    Status = 200,
                    Success = true,
                    ResponseInfo = _mapper.Map<List<HITATaskViewModel>>(repairs)
                };
            }
            return new ResponseMessage<List<HITATaskViewModel>>()
            {
                Msg = "你没有任何任务",
                Success = true
            };
        }




        /// <summary>
        /// 完成任务
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        [Route("completeTask")]
        [HttpGet]
        [Authorize(Policy = "ElectricianAndCarpentry")]
        public async Task<ResponseMessage<string>> UpdateMyTask(long repairId)
        {
            ReportForRepair reportForRepair = _reportForRepairService.QueryById(repairId)?.Result;
            if (reportForRepair != null)
            {
                if (reportForRepair.WaitHandle == 0)
                {
                    return new ResponseMessage<string>()
                    {
                        Msg = "该任务还未被人接受处理",
                        Success = false
                    };

                }

                if (reportForRepair.WaitHandle == 2)
                {
                    return new ResponseMessage<string>()
                    {
                        Msg = "该任务已被完成",
                        Success = false
                    };
                }

                reportForRepair.WaitHandle = 2;
                bool update = await _reportForRepairService.Update(reportForRepair);
                return update
                    ? new ResponseMessage<string>()
                    {
                        Msg = "成功提交，已完成",
                        Success = true,
                        Status = 200
                    }
                    : new ResponseMessage<string>()
                    {
                        Msg = "提交失败",
                        Success = false
                    };
            }
            return new ResponseMessage<string>()
            {
                Msg = "没有该任务",
                Success = false
            };
        }


        /// <summary>
        ///  放弃任务
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("abortMission")]
        [Authorize(Policy = "ElectricianAndCarpentry")]
        public async Task<ResponseMessage<bool>> UpdateWaitHandle(long repairId)
        {
            ReportForRepair repair = _reportForRepairService.QueryById(repairId)?.Result;
            if (repair!=null&& repair.WaitHandle == 1)
            {
                repair.WaitHandle = 0;
                repair.RoleId = 0;
                repair.WorkerId = 0;
                var update = await _reportForRepairService.Update(repair);
                return new ResponseMessage<bool>()
                {
                    Status = 200,
                    Msg = "弃单成功",
                    Success = true,
                    ResponseInfo = update
                };
            }
            else
            {
                return new ResponseMessage<bool>()
                {
                    Msg = "弃单失败，没有该单或者没有接此单",
                    Success = false,
                };
            }
            
            
        }
    }
}
