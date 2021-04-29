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

namespace SchoolRepairSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("cors")]
    public class RepairController : ControllerBase
    {
        private readonly IReportForRepairService _reportForRepairService;
        private readonly IMapper _mapper;
        private readonly IUserWareHouseService _userWareHouseService;
        private readonly IUsersService _usersService;

        public RepairController(IReportForRepairService reportForRepairService,IMapper mapper,IUserWareHouseService userWareHouseService,IUsersService usersService)
        {
            _reportForRepairService = reportForRepairService;
            _mapper = mapper;
            _userWareHouseService = userWareHouseService;
            _usersService = usersService;
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
        public ResponseMessage<List<ReportForRepairRecordViewModel>> GetUserAllRecord(int pageNum=1,int size=10)
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
        public async Task<ResponseMessage<List<RoleRepairViewModel>>> GetAllRepair(int pageNum=1, int size=10)
        {

            //增加维修人和领取的材料,完成
            ClaimsPrincipal principal = HttpContext.User;
            string value = principal.FindFirst(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            List<ReportForRepair> reportForRepairs = _reportForRepairService.QueryPagingByExp(x=>!x.IsRemove,pageNum,size)?.Result.OrderByDescending(x=>x.DateTime).ToList();
            if (reportForRepairs!=null)
            {
                List<RoleRepairViewModel> repairViewModels = new List<RoleRepairViewModel>();
                foreach (ReportForRepair repair in reportForRepairs)
                {
                    if (repair.WorkerId!=0)
                    {
                        List<UserWareHouse> userWareHouse = _userWareHouseService.QueryList(x => x.UserWareHouseId == repair.Id)?.Result;
                        Users users = await _usersService.Query(x => x.Id == repair.WorkerId);
                        if (userWareHouse!=null)
                        {
                            List<GoodsInfo> list = new List<GoodsInfo>();
                            //将多次的取出记录拿出
                            foreach (UserWareHouse wareHouse in userWareHouse)
                            {
                                list.Add(new GoodsInfo() {Goods = wareHouse.Goods, Number = wareHouse.PickUp});
                            }
                            RoleRepairViewModel roleRepairViewModel = new RoleRepairViewModel()
                            {
                                //userWareHouse.Goods, userWareHouse.PickUp
                                Id = repair.Id,
                                DateTime = repair.DateTime,
                                Desc = repair.Desc,
                                Dorm = repair.Dorm,
                                Evaluate = repair.Evaluate,
                                GoodsInfos = list,
                                Layer = repair.Layer,
                                UserName = users.UserName,
                                LoginUserId = Convert.ToInt64(value),
                                RoleId = repair.RoleId,
                                WorkerId = repair.WorkerId,
                                WaitHandle = repair.WaitHandle,
                                Tung = repair.Tung
                            };
                            repairViewModels.Add(roleRepairViewModel);
                        }
                        else
                        {
                            RoleRepairViewModel roleRepairViewModel = new RoleRepairViewModel()
                            {
                                Id = repair.Id,
                                DateTime = repair.DateTime,
                                Desc = repair.Desc,
                                Dorm = repair.Dorm,
                                Evaluate = repair.Evaluate,
                                GoodsInfos = null,
                                Layer = repair.Layer,
                                UserName = users.UserName,
                                LoginUserId = Convert.ToInt64(value),
                                RoleId = repair.RoleId,
                                WorkerId = repair.WorkerId,
                                WaitHandle = repair.WaitHandle,
                                Tung = repair.Tung
                            };
                            repairViewModels.Add(roleRepairViewModel);
                        }
                        
                    }
                    else
                    {
                        RoleRepairViewModel roleRepairViewModel = new RoleRepairViewModel()
                        {
                            Id = repair.Id,
                            DateTime = repair.DateTime,
                            Desc = repair.Desc,
                            Dorm = repair.Dorm,
                            GoodsInfos = null,
                            Evaluate = repair.Evaluate,
                            Layer = repair.Layer,
                            UserName = "无",
                            LoginUserId = Convert.ToInt64(value),
                            RoleId = repair.RoleId,
                            WorkerId = repair.WorkerId,
                            WaitHandle = repair.WaitHandle,
                            Tung = repair.Tung
                        };
                        repairViewModels.Add(roleRepairViewModel);
                    }
                    //筛选查询完毕
                }
                return new ResponseMessage<List<RoleRepairViewModel>>()
                {
                    Msg = "请求成功",
                    Status = 200,
                    Success = true,
                    ResponseInfo = repairViewModels
                };
            }
            return new ResponseMessage<List<RoleRepairViewModel>>()
            {
                Msg = "请求成功,暂无无数据",
                Status = 200,
                Success = false,
            };
        }
        /// <summary>
        /// 获取该用户接受的所有的任务
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        //
        [HttpGet]
        [Route("workerRepair")]
        [Authorize(Policy = "AdminAndOrdinaryAndElectrician")]
        public ResponseMessage<List<ReportForRepairViewModel>> GetWorkerRepair(int pageNum=1, int size=10)
        {
            ClaimsPrincipal principal = HttpContext.User;
            string value = principal.FindFirst(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            List<ReportForRepair> reportForRepairs = _reportForRepairService.QueryPagingByExp(x => !x.IsRemove&&x.WorkerId==Convert.ToInt64(value), pageNum, size)?.Result;
            if (reportForRepairs != null)
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
