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
    public class WareHouseController : ControllerBase
    {
        private readonly IWareHouseService _wareHouseService;
        private readonly IUserWareHouseService _userWareHouseService;
        private readonly IMapper _mapper;
        private readonly IUsersService _usersService;

        public WareHouseController(IWareHouseService wareHouseService,IUserWareHouseService userWareHouseService,IMapper mapper,IUsersService usersService)
        {
            _wareHouseService = wareHouseService;
            _userWareHouseService = userWareHouseService;
            _mapper = mapper;
            _usersService = usersService;
        }
        /// <summary>
        /// 仓库增加/新增商品
        /// </summary>
        /// <param name="goodsName"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("addGoods")]
        [Authorize(Policy = "Admin")]
        public async Task<ResponseMessage<long>> GetAddGoods(string goodsName, int number)
        {
            WareHouse goods = _wareHouseService.Query(x => x.Goods.Equals(goodsName)&&!x.IsRemove)?.Result;
            string jti = HttpContext.User.FindFirst(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (goods==null)
            {
                long id = await _wareHouseService.Add(new WareHouse()
                {
                    Goods = goodsName,
                    Number = number
                });
                
                await _userWareHouseService.Add(new UserWareHouse()
                {
                    Purchase = number,
                    Goods = goodsName,
                    GoodsId = id,
                    UserId = Convert.ToInt64(jti)
                });
                return new ResponseMessage<long>()
                {
                    Msg = "新增商品成功",
                    Status = 200,
                    Success = true,
                    ResponseInfo = id
                };
            }
            else
            {
                goods.Number += number;
                bool update = await _wareHouseService.Update(goods);
                await _userWareHouseService.Add(new UserWareHouse()
                {
                    Purchase = number,
                    GoodsId = goods.Id,
                    Goods = goods.Goods,
                    UserId = Convert.ToInt64(jti)
                });
                return update?new ResponseMessage<long>()
                {
                    Msg = "添加商品成功",
                    Status = 200,
                    Success = true
                }: new ResponseMessage<long>()
                {
                    Msg = "添加商品失败",
                    Success = false
                };
            }
        }
        /// <summary>
        /// 取出货物
        /// </summary>
        /// <param name="goodsId">货物id</param>
        /// <param name="number"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("takeOutGoods")]
        [Authorize(Policy = "ElectricianAndCarpentry")]//Carpentry Electrician
        public async Task<ResponseMessage<long>> UpdateTakeOutGoods(long goodsId,int number)
        {
            WareHouse wareHouse = _wareHouseService.QueryById(goodsId)?.Result;
            if (wareHouse==null)
            {
                return new ResponseMessage<long>()
                {
                    Msg = "没有该商品",
                    Success = false
                };
            }

            wareHouse.Number -= number;
            if (wareHouse.Number >= 0)
            {
                bool update = await _wareHouseService.Update(wareHouse);
                string jti = HttpContext.User.FindFirst(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                await _userWareHouseService.Add(new UserWareHouse()
                {
                    Goods = wareHouse.Goods,
                    GoodsId = wareHouse.Id,
                    PickUp = number,
                    UserId = Convert.ToInt64(jti)
                });
                return update ? new ResponseMessage<long>()
                {
                    Msg = "取出成功",
                    Success = true,
                    Status = 200
                } : new ResponseMessage<long>()
                {
                    Msg = "取出失败",
                    Success = false
                };
            }
            return new ResponseMessage<long>()
            {
                Msg = "取出失败,取出数量错误",
                Success = false
            };

        }
        /// <summary>
        /// 获取仓库
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("allGoods")]
        [Authorize(Policy = "AdminAndOrdinaryAndElectrician")]//Carpentry Electrician Admin
        public ResponseMessage<List<GoodsInfoViewModel>> GetAllGoods()
        {
            List<WareHouse> wareHouses = _wareHouseService.QueryAll()?.Result;
            if (wareHouses!=null)
            {
                return new ResponseMessage<List<GoodsInfoViewModel>>()
                {
                    Msg = "请求成功",
                    Status = 200,
                    Success = true,
                    ResponseInfo = _mapper.Map<List<GoodsInfoViewModel>>(wareHouses)
                };
            }
            return new ResponseMessage<List<GoodsInfoViewModel>>()
            {
                Msg = "仓库无任何类别货物",
                Success = true,
                Status = 200
            };
        }
        /// <summary>
        /// 所有货物的存取信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AllAccessInfo")]
        [Authorize(Policy = "Admin")]
        public async Task<ResponseMessage<List<UserWareHouseViewModel>>> GetAllAccessInfo()
        {
            List<UserWareHouse> userWareHouses = _userWareHouseService.QueryAll()?.Result;
            List<long> idLongs = new List<long>();
            if (userWareHouses!=null)
            {
                foreach (UserWareHouse house in userWareHouses)
                {
                    idLongs.Add(house.UserId);
                }
                List<Users> users = await _usersService.QueryList(idLongs);
                List<UserWareHouseViewModel> list = new List<UserWareHouseViewModel>();
                for (int i = 0; i < userWareHouses.Count; i++)
                {
                    list.Add(new UserWareHouseViewModel()
                    {
                        UserName = users[i].UserName,
                        Goods = userWareHouses[i].Goods,
                        PickUp = userWareHouses[i].PickUp,
                        Purchase = userWareHouses[i].Purchase,
                        DateTime = userWareHouses[i].DateTime
                    });
                }
                return new ResponseMessage<List<UserWareHouseViewModel>>()
                {
                    Msg = "请求成功",
                    Status = 200,
                    Success = true,
                    ResponseInfo = list
                };
            }
            return new ResponseMessage<List<UserWareHouseViewModel>>()
            {
                Msg = "仓库暂无数据",
                Status = 200,
                Success = true
            };
        }

    }
}
