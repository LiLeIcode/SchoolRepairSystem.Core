using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using SchoolRepairSystem.Common.Helper;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Repository;
using SchoolRepairSystem.Repository.UnitWork;
using SchoolRepairSystem.Service;
using SchoolRepairSystem.Models;

namespace SchoolRepairSystem.Test
{
    class JoinInfo
    {
        public long Id { get; set; }
        public long QueryId { get; set; }
    }

    class JoinCount
    {
        public long Id { get; set; }
        public string RoleName { get; set; }
    }


    class Program
    {

        static async Task Main(string[] args)
        {
            //UsersService usersService = new UsersService(new UsersRepository());
            //Users query = await usersService.Query(x => x.UserName.Equals("lilei"));
            //Console.WriteLine(query.UserName);

            #region 分页

            ReportForRepairService service = new ReportForRepairService(new ReportForRepairRepository());
            var queryPaging = await service.QueryPaging(4, 5);
            foreach (var repair in queryPaging)
            {
                Console.WriteLine(repair.Id);
            }
            #endregion




            #region 分离字符串

            //string roleName = "admin,Carpentry,Electrician,Ordinary";
            //string[] split = roleName.Split(',');
            //foreach (string s in split)
            //{
            //    Console.WriteLine(s);

            //}

            #endregion


            #region 加点菜单

            //IMenuService menuService = new MenuService(new MenuRepository());
            //long add = await menuService.Add(new List<Menus>()
            //{
            //    new Menus()
            //    {
            //        MenuName = "查看所有报修",
            //        Grade = 1,
            //        Path = "allRepairs"
            //    },new Menus()
            //    {
            //        MenuName = "查看完成报修",
            //        Grade = 1,
            //        Path = "allCompletedRepair"
            //    },
            //    new Menus()
            //    {
            //        MenuName = "进出货",
            //        Grade = 1,
            //        Path = "importAndExport"
            //    },
            //    new Menus()
            //    {
            //        MenuName = "进出货记录",
            //        Grade = 1,
            //        Path = "importAndExportInfo"
            //    },
            //    new Menus()
            //    {
            //        MenuName = "权限管理",
            //        Grade = 1,
            //        Path = "power"
            //    },
            //    new Menus()
            //    {
            //    MenuName = "修改用户信息",
            //    Grade = 1,
            //    Path = "modifyEmployee",
            //},new Menus()
            //    {
            //        MenuName = "修改用户信息",
            //        Grade = 1,
            //        Path = "allEmployeeInfo",
            //    },
            //    new Menus()
            //    {
            //        MenuName = "数据报表",
            //        Grade = 1,
            //        Path = "dataReport",
            //    }
            //});


            #endregion


            #region 测试封装的泛型三表联合查询

            ///

            #endregion


            #region 三表联合查询

            //var db = new SchoolRepairSystemDbContext();
            //List<JoinCount> list = db.Users
            //    .Join(db.UserRoles, userid => userid.Id, userRole => userRole.UserId, (userid, userrole) =>
            //        new JoinInfo
            //        {
            //            Id = userid.Id,
            //            QueryId = userrole.RoleId
            //        })
            //    .Where(x => x.Id == 1).
            //    Join(db.Roles, joinInfo => joinInfo.QueryId, roles => roles.Id, (userroleid, roleid) =>
            //        new JoinCount()
            //        {
            //            Id = userroleid.Id,
            //            RoleName = roleid.RoleName
            //        }).ToList();
            //foreach (var count in list)
            //{
            //    Console.WriteLine(count.Id + ":" + count.RoleName);
            //}

            #endregion


            #region 之前测试curd

            //var userRoleService = new UserRoleService(new UserRoleRepository());
            //IUsersService usersService = new UsersService(new UsersRepository());
            //List<Users> queryAll = await usersService.QueryAll();
            //foreach (var userse in queryAll)
            //{
            //    Console.WriteLine(userse.Id+":"+userse.UserName+":"+userse.Password);
            //}
            //Users users = await usersService.Query(x => x.UserName.Equals("何俐ss"));
            //users.UserName = "heli";
            //Console.WriteLine(await usersService.Update(users));

            //await usersService.Delete(12);
            //await usersService.Delete(13);
            //List<UserRole> longs = new List<UserRole>();
            //UserRole query1 = await userRoleService.Query(x => x.UserId == 12);
            //UserRole query2 = await userRoleService.Query(x => x.UserId == 13);
            //longs.Add(query1);
            //longs.Add(query2);
            //await userRoleService.DeleteList(longs);
            //IUsersService service = new UsersService(new UsersRepository());
            //IUserRoleService userRoleService = new UserRoleService(new UserRoleRepository());
            //List<Users> queryList = await service.QueryList(x => x.UserName.Equals("何俐")&&x.IsRemove);
            //List<long> longs = new List<long>();
            //foreach (Users userse in queryList)
            //{
            //    longs.Add(userse.Id);
            //}

            //foreach (long l in longs)
            //{
            //    Console.WriteLine(l);
            //}
            //bool list = await userRoleService.DeleteList(longs);
            //bool deleteList = await service.DeleteList(queryList);
            //Console.WriteLine(list);
            //Console.WriteLine(deleteList);

            #endregion
        }
    }
}

/*
 *IRolesService service = new RolesService(new RolesRepository());
            IUsersService usersService = new UsersService(new UsersRepository());
            IUserRoleService userRoleService = new UserRoleService(new UserRoleRepository());
            List<Roles> roleList = await service.QueryAll();
            List<Users> userList = await usersService.QueryAll();
            long [] roleIds = new long[roleList.Count];
            long [] userIds = new long[userList.Count];
            for (int i = 0; i < roleIds.Length; i++)
            {
                roleIds[i] = roleList[i].Id;
            }

            for (int i = 0; i < userIds.Length; i++)
            {
                userIds[i] = userList[i].Id;
            }

            for (int i = 0,j=0; i < userIds.Length;j++, i++)
            {
                await userRoleService.Add(new UserRole()
                {
                    UserId = userIds[i],
                    RoleId = roleIds[j],
                });
                if (j==(roleIds.Length-1))
                {
                    j = 0;
                }
            }
 *
 */
