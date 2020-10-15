using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Models;
using SchoolRepairSystem.Models.ViewModels;

namespace SchoolRepairSystem.Api.Controllers
{
    /**
     * 数据库表为建立，独立表，与其他表无关联
     * 根据授权策略分权访问
     */
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminAndCarpentryAndElectrician")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IRolesService _rolesService;

        public MenuController(IMenuService menuService, IRolesService rolesService)
        {
            _menuService = menuService;
            _rolesService = rolesService;
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [Route("menu")]
        [HttpGet]
        public async Task<ResponseMessage<List<MenuViewModel>>> GetMenu()
        {
            ClaimsPrincipal principal = HttpContext.User;
            string roleName = principal.FindFirst(x => x.Type == ClaimTypes.Role).Value;
            List<Roles> roles = await _rolesService.QueryAll();
            List<Menus> menus = await _menuService.QueryAll();
            List<MenuViewModel> data = new List<MenuViewModel>();
            if (roleName.Equals(roles[0].RoleName))
            {

                foreach (Menus menu in menus)
                {
                    if (menu.Grade==1)
                    {
                        data.Add(new MenuViewModel()
                        {
                            MenuName = menu.MenuName,
                            Grade = 1,
                            Path = menu.Path
                        });
                    }
                    
                }
            }
            if (roleName.Equals(roles[1].RoleName)|| roleName.Equals(roles[2].RoleName))
            {

                foreach (Menus menu in menus)
                {
                    if (menu.Grade == 2)
                    {
                        data.Add(new MenuViewModel()
                        {
                            MenuName = menu.MenuName,
                            Grade = 2,
                            Path = menu.Path
                        });
                    }

                }
            }
            if (roleName.Equals(roles[3].RoleName))
            {

                foreach (Menus menu in menus)
                {
                    if (menu.Grade == 3)
                    {
                        data.Add(new MenuViewModel()
                        {
                            MenuName = menu.MenuName,
                            Grade = 3,
                            Path = menu.Path
                        });
                    }

                }
            }
            return new ResponseMessage<List<MenuViewModel>>()
            {
                Msg = "请求成功",
                Status = 200,
                Success = true,
                ResponseInfo = data
            };
        }
    }
}
