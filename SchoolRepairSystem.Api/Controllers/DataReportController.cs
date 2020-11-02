using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models;
using SchoolRepairSystem.Models.ViewModels;

namespace SchoolRepairSystem.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    [Authorize(Policy = "Admin")]
    public class DataReportController : ControllerBase
    {
        private readonly IReportForRepairService _reportForRepairService;
        private readonly IUserRoleService _userRoleService;

        public DataReportController(IReportForRepairService reportForRepairService,IUserRoleService userRoleService)
        {
            _reportForRepairService = reportForRepairService;
            _userRoleService = userRoleService;
        }
        /// <summary>
        /// 完成量对比
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("dataQC")]
        public async Task<ResponseMessage<DataQCResponse<int[]>>> GetDataQC()
        {
            List<ReportForRepair> reportForRepairs = await _reportForRepairService.QueryAll();
            //总量
            int count = reportForRepairs.Count;
            //完成量
            int repairsWaitHandle2 = reportForRepairs.Where(x => x.WaitHandle == 2).ToArray().Length;
            //未处理量
            int repairsWaitHandle0 = reportForRepairs.Where(x => x.WaitHandle == 0).ToArray().Length;
            //正在处理量
            int repairsWaitHandle1 = reportForRepairs.Where(x => x.WaitHandle == 1).ToArray().Length;

            return new ResponseMessage<DataQCResponse<int[]>>()
            {
                Msg = "请求成功",
                Status = 200,
                Success = true,
                ResponseInfo = new DataQCResponse<int[]>()
                {
                    Count = count,
                    RepairsWaitHandle0 = repairsWaitHandle0,
                    RepairsWaitHandle1 = repairsWaitHandle1,
                    RepairsWaitHandle2 = repairsWaitHandle2,
                    ArrayT = new []{ count,repairsWaitHandle0, repairsWaitHandle1, repairsWaitHandle2 }
                }
            };

        }

        /// <summary>
        /// 用户比例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("userProp")]
        public async Task<ResponseMessage<int[]>> GetUserProportion()
        {
            List<UserRole> userRoles = await _userRoleService.QueryAll();
            int pt = 0;
            int admin = 0;
            int carpentry = 0;//mu 3
            int electrician = 0;//电 2
            foreach (UserRole userRole in userRoles)
            {
                if (userRole.RoleId==1)
                {
                    ++admin;
                }

                if (userRole.RoleId==2)
                {
                    ++electrician;
                }
                if (userRole.RoleId == 3)
                {
                    ++carpentry;
                }
                if (userRole.RoleId == 4)
                {
                    ++pt;
                }
            }
            return new ResponseMessage<int[]>()
            {
                Msg = "请求成功",
                Status = 200,
                Success = true,
                ResponseInfo = new int[] { admin, electrician, carpentry,pt}
            };
        }
        //分角色好坏评报表
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         [HttpGet]
        [Route("ptComment")]
        public async Task<ResponseMessage<int[]>> GetComment()
        {
            List<ReportForRepair> reportForRepairs = await _reportForRepairService.QueryList(x => x.WaitHandle == 2);
            int none = 0, good = 0, medium = 0, poor = 0;
            foreach (ReportForRepair reportForRepair in reportForRepairs)
            {
                if (reportForRepair.Evaluate==0)
                {
                    ++none;
                }
                if (reportForRepair.Evaluate == 1)
                {
                    ++good;
                }
                if (reportForRepair.Evaluate == 2)
                {
                    ++medium;
                }
                if (reportForRepair.Evaluate == 3)
                {
                    ++poor;
                }
            }
            return new ResponseMessage<int[]>()
            {
                Msg = "请求成功",
                Status = 200,
                Success = true,
                ResponseInfo = new []{ none , good, medium, poor }
            };
        }
    }

   

    
}
