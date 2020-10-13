using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolRepairSystem.IService;
using SchoolRepairSystem.Repository;
using SchoolRepairSystem.Service;
using SchoolRepairSystemModels;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void TestMethod1()
        {
            var userRoleService = new UserRoleService(new UserRoleRepository());
            IUsersService usersService = new UsersService(new UsersRepository());
            UserRole userRole = await userRoleService?.Query(x => x.UserId == 11);
            Console.WriteLine(userRole);
        }
    }
}
