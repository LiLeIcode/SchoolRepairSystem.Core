# SchoolRepairSystem.Core
项目启动需要先做的步骤

1、数据库生成，本项目使用的是**SqlServer**

​	在SchoolRepairSystem.Models中的SchoolRepairSyatemDbContext.cs中修改连接配置

```
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)=>
            optionsBuilder.UseLoggerFactory(MyLoggerFactory).
                UseSqlServer(@"Data Source=.;database=SchoolRepairSystem;Integrated Security=True;uid=sa;pwd=root");
```

Data Source：表示数据库的来源地址：. 表示本地

database：表示要连接的数据库

uid：表示数据库的登录名，sqlserver默认sa

pwd：表示数据库的登陆密码

2、迁移数据库

在程序包管理器控制台输入指令

  一、添加迁移：add-migration 'CreateDB'

 二、更新数据库：update-database

然后等待迁移完成

