using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SchoolRepairSystem.Api.Controllers
{
   /**
    * 数据库表为建立，独立表，与其他表无关联
    * 根据授权策略分权访问
    */
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

    }
}
