using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models;
using SchoolRepairSystem.Models.ViewModels;

namespace SchoolRepairSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairController : ControllerBase
    {
        private readonly IReportForRepairService _reportForRepairService;
        private readonly IMapper _mapper;

        public RepairController(IReportForRepairService reportForRepairService,IMapper mapper)
        {
            _reportForRepairService = reportForRepairService;
            _mapper = mapper;
        }
        /// <summary>
        /// 报修
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("repair")]
        [HttpPost]
        [Authorize(Policy = "Ordinary")]
        public async Task<ResponseMessage<long>> GetReportForPair([FromBody] RepairViewModel model)
        {
            if (ModelState.IsValid)
            {
                string jti = HttpContext.User.FindFirst(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                long id = await _reportForRepairService.Add(new ReportForRepair()
                {
                    UserId = Convert.ToInt64(jti),
                    Layer = model.Layer,
                    Tung = model.Tung,
                    Dorm = model.Dorm,
                    Desc = model.Desc
                });
                return new ResponseMessage<long>()
                {
                    Msg = "报修已提交",
                    Status = 200,
                    Success = true,
                    ResponseInfo = id
                };
            }
            return new ResponseMessage<long>()
            {
                Msg = "填写信息不完全",
                Success = false
            };

        }
        /// <summary>
        /// 获取个人报修记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("allRepairRecord")]
        [Authorize(Policy = "Ordinary")]
        public ResponseMessage<List<ReportForRepairRecordViewModel>> GetUserAllRecord()
        {
            string jti = HttpContext.User.FindFirst(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            List<ReportForRepair> reportForRepairs = _reportForRepairService.QueryList(x => x.UserId == Convert.ToInt64(jti))?.Result;
            if (reportForRepairs!=null)
            {
                return new ResponseMessage<List<ReportForRepairRecordViewModel>>()
                {
                    Msg = "请求成功",
                    Status = 200,
                    Success = true,
                    ResponseInfo = _mapper.Map<List<ReportForRepairRecordViewModel>>(reportForRepairs)
                };
            }
            return new ResponseMessage<List<ReportForRepairRecordViewModel>>()
            {
                Msg = "无报修记录"
            };
        }
        /// <summary>
        /// 填写已完成报修的评论
        /// </summary>
        /// <param name="reportForRepairId"></param>
        /// <param name="evaluate">0无,1好,2中,3差</param>
        /// <returns></returns>
        [HttpPost]
        [Route("comment")]
        [Authorize(Policy = "Ordinary")]
        //[Authorize(Policy = "Admin")]
        public async Task<ResponseMessage<bool>> PostComment([FromQuery]long reportForRepairId, [FromQuery]int evaluate=0)
        {
            //string jti = HttpContext.User.FindFirst(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            //long id = Convert.ToInt64(jti);
            ReportForRepair reportForRepair = _reportForRepairService.QueryById(reportForRepairId)?.Result;
            if (reportForRepair!=null)
            {
                if (reportForRepair.WaitHandle == 2 && reportForRepair.Evaluate == 0)
                {
                    reportForRepair.Evaluate = evaluate;
                    bool update = await _reportForRepairService.Update(reportForRepair);
                    if (update)
                    {
                        return new ResponseMessage<bool>()
                        {
                            Msg = "提交评论成功",
                            Status = 200,
                            Success = true,
                            ResponseInfo = update
                        };
                    }

                }
                return new ResponseMessage<bool>()
                {
                    Msg = "提交评论失败。可能的原因：维修未完成、已提交过评论",
                    Success = false
                };
            }
            return new ResponseMessage<bool>() {
                Msg = "没有该任务",
                Success = false
            };
           
            

        }
        /// <summary>
        /// 获取所有的报修记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("allRepair")]
        [Authorize(Policy = "Admin")]
        public ResponseMessage<List<RepairViewModel>> GetAllRepair()
        {
            List<ReportForRepair> reportForRepairs = _reportForRepairService.QueryAll()?.Result;
            if (reportForRepairs!=null)
            {
                return new ResponseMessage<List<RepairViewModel>>()
                {
                    Msg = "请求成功",
                    Status = 200,
                    Success = false,
                    ResponseInfo = _mapper.Map<List<RepairViewModel>>(reportForRepairs)
                };
            }
            return new ResponseMessage<List<RepairViewModel>>()
            {
                Msg = "请求成功,暂无无数据",
                Status = 200,
                Success = false,
            };
        }
     }
}
