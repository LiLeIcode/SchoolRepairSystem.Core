using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
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
        public ResponseMessage<List<ReportForRepairRecordViewModel>> GetUserAllRecord(int pageNum,int size)
        {
            string jti = HttpContext.User.FindFirst(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            List<ReportForRepair> reportForRepairs = _reportForRepairService.QueryPagingByExp(x => x.UserId == Convert.ToInt64(jti)&&!x.IsRemove, pageNum, size)?.Result;
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
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("comment")]
        [Authorize(Policy = "Ordinary")]
        //[Authorize(Policy = "Admin")]
        public async Task<ResponseMessage<bool>> PostComment([FromBody]CommentViewModel model)
        {
            //string jti = HttpContext.User.FindFirst(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            //long id = Convert.ToInt64(jti);
            ReportForRepair reportForRepair = _reportForRepairService.QueryById(model.ReportForRepairId)?.Result;
            if (reportForRepair!=null)
            {
                if (reportForRepair.WaitHandle == 2 && reportForRepair.Evaluate == 0)
                {
                    reportForRepair.Evaluate = model.Evaluate;
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
        [Authorize(Policy = "AdminAndOrdinaryAndElectrician")]
        public ResponseMessage<List<ReportForRepairViewModel>> GetAllRepair(int pageNum, int size)
        {
            List<ReportForRepair> reportForRepairs = _reportForRepairService.QueryPagingByExp(x=>!x.IsRemove,pageNum,size)?.Result;
            if (reportForRepairs!=null)
            {
                return new ResponseMessage<List<ReportForRepairViewModel>>()
                {
                    Msg = "请求成功",
                    Status = 200,
                    Success = false,
                    ResponseInfo = _mapper.Map<List<ReportForRepairViewModel>>(reportForRepairs)
                };
            }
            return new ResponseMessage<List<ReportForRepairViewModel>>()
            {
                Msg = "请求成功,暂无无数据",
                Status = 200,
                Success = false,
            };
        }
     }
}
