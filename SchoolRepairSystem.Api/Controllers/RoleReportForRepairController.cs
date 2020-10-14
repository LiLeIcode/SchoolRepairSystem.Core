using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRepairSystem.IService;
using SchoolRepairSystemModels;
using SchoolRepairSystemModels.ViewModels;

namespace SchoolRepairSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [Authorize(Policy = "Admin")]
        public async Task<ResponseMessage<RoleReportForRepairViewModel>> UpdateRoleReportForRepair(long roleReportForRepairId)
        {
            ClaimsPrincipal principal = HttpContext.User;
            string roleName = principal.FindFirst(x => x.Type == ClaimTypes.Role).Value;
            string name = principal.FindFirst(x => x.Type == ClaimTypes.Name).Value;
            Roles roles = await _rolesService.Query(x => x.RoleName.Equals(roleName));
            await _roleReportForRepairService.Add(new RoleReportForRepair()
            {
                RoleId = roles.Id,
                RepairId = roleReportForRepairId
            });
            ReportForRepair reportForRepair = await _reportForRepairService.QueryById(roleReportForRepairId);
            if (reportForRepair.WaitHandle==1||reportForRepair.WaitHandle==2)
            {
                return new ResponseMessage<RoleReportForRepairViewModel>()
                {
                    Msg = "接单失败，该报修已被接受或已完成维修",
                };
            }
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
        /// <summary>
        /// 查看自己接了哪些单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("myTask")]
        [Authorize(Policy = "Admin")]
        public async Task<ResponseMessage<List<ReportForRepairViewModel>>> GetMyTask()
        {
            string jti = HttpContext.User.FindFirst(x => x.Type == Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti)
                .Value;
            List<RoleReportForRepair> roleReportForRepairs = _roleReportForRepairService.QueryList(x=>x.RoleId==Convert.ToInt64(jti)&&!x.IsRemove)?.Result;
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
    }
}
