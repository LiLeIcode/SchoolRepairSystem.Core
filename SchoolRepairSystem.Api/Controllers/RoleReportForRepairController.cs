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
        private readonly IRoleReportForRepairService _roleReportForRepairService;
        private readonly IRolesService _rolesService;
        private readonly IMapper _mapper;

        public RoleReportForRepairController(IReportForRepairService reportForRepairService,IRoleReportForRepairService roleReportForRepairService,IRolesService rolesService,IMapper mapper)
        {
            _reportForRepairService = reportForRepairService;
            _roleReportForRepairService = roleReportForRepairService;
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
            if (reportForRepair.WaitHandle==1||reportForRepair.WaitHandle==2)
            {
                return new ResponseMessage<RoleReportForRepairViewModel>()
                {
                    Msg = "接单失败，该报修已被接受或已完成维修",
                };
            }
            else
            {
                await _roleReportForRepairService.Add(new RoleReportForRepair()
                {
                    WorkerId = Convert.ToInt64(jti),
                    RoleId = roles.Id,
                    RepairId = roleReportForRepairId
                });
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
            
        }
        /// <summary>
        /// 查看自己接了哪些单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("myTask")]
        [Authorize(Policy = "ElectricianAndCarpentry")]
        public async Task<ResponseMessage<List<ReportForRepairViewModel>>> GetMyTask()
        {
            string jti = HttpContext.User.FindFirst(x => x.Type == Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti)
                .Value;
            List<RoleReportForRepair> roleReportForRepairs = _roleReportForRepairService.QueryList(x=>x.WorkerId==Convert.ToInt64(jti)&&!x.IsRemove)?.Result;
            List<long> idLongs = new List<long>();
            if (roleReportForRepairs!=null)
            {
                foreach (RoleReportForRepair roleReportForRepair in roleReportForRepairs)
                {
                    idLongs.Add(roleReportForRepair.RepairId);
                }

                List<ReportForRepair> reportForRepairs = await _reportForRepairService.QueryList(idLongs);
                return new ResponseMessage<List<ReportForRepairViewModel>>()
                {
                    Msg = "请求成功",
                    Status = 200,
                    Success = true,
                    ResponseInfo = _mapper.Map<List<ReportForRepairViewModel>>(reportForRepairs)
                };
            }
            return new ResponseMessage<List<ReportForRepairViewModel>>()
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
                if (reportForRepair.WaitHandle==0)
                {
                    return new ResponseMessage<string>()
                    {
                        Msg = "该任务还未被人接受处理",
                        Success = false
                    };
                    
                }

                if (reportForRepair.WaitHandle==2)
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
                        Status=200
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
    }
}
