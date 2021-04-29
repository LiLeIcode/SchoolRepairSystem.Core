/*用户、维修与仓库表*/
CREATE TABLE [dbo].[UserWareHouses] (
    [Id]                BIGINT        IDENTITY (1, 1) NOT NULL,
    [DateTime]          DATETIME2 (7) NOT NULL,
    [IsRemove]          BIT           NOT NULL,
    [UserId]            BIGINT        NOT NULL,
    [GoodsId]           BIGINT        NOT NULL,
    [Goods]             NVARCHAR (20) NOT NULL,
    [Purchase]          INT           NOT NULL,
    [PickUp]            INT           NOT NULL,
    [ReportForRepairId] BIGINT        DEFAULT (CONVERT([bigint],(0))) NOT NULL,
    [UserWareHouseId]   BIGINT        DEFAULT (CONVERT([bigint],(0))) NOT NULL,
    CONSTRAINT [PK_RoleWareHouses] PRIMARY KEY CLUSTERED ([Id] ASC)
);

/*仓库表*/
CREATE TABLE [dbo].[WareHouses] (
    [Id]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [DateTime] DATETIME2 (7) NOT NULL,
    [IsRemove] BIT           NOT NULL,
    [Goods]    NVARCHAR (20) NOT NULL,
    [Number]   INT           NOT NULL,
    CONSTRAINT [PK_WareHouses] PRIMARY KEY CLUSTERED ([Id] ASC)
);



/*用户表*/
CREATE TABLE [dbo].[Users] (
    [Id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [DateTime] DATETIME2 (7)  NOT NULL,
    [IsRemove] BIT            NOT NULL,
    [UserName] NVARCHAR (20)  NOT NULL,
    [Password] NVARCHAR (20)  NOT NULL,
    [Phone]    NVARCHAR (MAX) NULL,
    [Mail]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);


/*用户与角色表*/
CREATE TABLE [dbo].[UserRoles] (
    [Id]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [DateTime] DATETIME2 (7) NOT NULL,
    [IsRemove] BIT           NOT NULL,
    [UserId]   BIGINT        NOT NULL,
    [RoleId]   BIGINT        NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


/*角色表*/
CREATE TABLE [dbo].[Roles] (
    [Id]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [DateTime] DATETIME2 (7) NOT NULL,
    [IsRemove] BIT           NOT NULL,
    [RoleName] NVARCHAR (15) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);


/*角色与报修表*/
CREATE TABLE [dbo].[RoleReportForRepairs] (
    [Id]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [DateTime] DATETIME2 (7) NOT NULL,
    [IsRemove] BIT           NOT NULL,
    [RepairId] BIGINT        NOT NULL,
    [RoleId]   BIGINT        NOT NULL,
    [WorkerId] BIGINT        DEFAULT (CONVERT([bigint],(0))) NOT NULL,
    CONSTRAINT [PK_RoleReportForRepairs] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工人的id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RoleReportForRepairs', @level2type = N'COLUMN', @level2name = N'RoleId';



/*保修表*/
CREATE TABLE [dbo].[ReportForRepairs] (
    [Id]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [DateTime]   DATETIME2 (7) NOT NULL,
    [IsRemove]   BIT           NOT NULL,
    [UserId]     BIGINT        NOT NULL,
    [Layer]      NVARCHAR (10) NOT NULL,
    [Tung]       NVARCHAR (10) NOT NULL,
    [Dorm]       NVARCHAR (10) NOT NULL,
    [Desc]       NVARCHAR (30) NOT NULL,
    [WaitHandle] INT           NOT NULL,
    [Evaluate]   INT           NOT NULL,
    [RoleId]     BIGINT        NOT NULL,
    [WorkerId]   BIGINT        NOT NULL,
    CONSTRAINT [PK_ReportForRepairs] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'报修者的Id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ReportForRepairs', @level2type = N'COLUMN', @level2name = N'UserId';



/*菜单表*/
CREATE TABLE [dbo].[Menus] (
    [Id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [DateTime] DATETIME2 (7)  NOT NULL,
    [IsRemove] BIT            NOT NULL,
    [MenuName] NVARCHAR (14)  NOT NULL,
    [Grade]    INT            NOT NULL,
    [Path]     NVARCHAR (MAX) DEFAULT (N'') NOT NULL,
    CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED ([Id] ASC)
);



/*菜单表数据*/
SET IDENTITY_INSERT [dbo].[Menus] ON
INSERT INTO [dbo].[Menus] ([Id], [DateTime], [IsRemove], [MenuName], [Grade], [Path]) VALUES (1, N'2020-10-15 11:10:39', 0, N'我要报修', 3, N'reportForRepair')
INSERT INTO [dbo].[Menus] ([Id], [DateTime], [IsRemove], [MenuName], [Grade], [Path]) VALUES (2, N'2020-10-15 11:10:39', 0, N'查看报修处理', 3, N'handle')
INSERT INTO [dbo].[Menus] ([Id], [DateTime], [IsRemove], [MenuName], [Grade], [Path]) VALUES (3, N'2020-10-15 11:19:22', 0, N'报修任务', 2, N'repairTask')
INSERT INTO [dbo].[Menus] ([Id], [DateTime], [IsRemove], [MenuName], [Grade], [Path]) VALUES (4, N'2020-10-15 11:19:22', 0, N'我的任务', 2, N'myTask')
INSERT INTO [dbo].[Menus] ([Id], [DateTime], [IsRemove], [MenuName], [Grade], [Path]) VALUES (5, N'2020-10-15 11:19:22', 0, N'维修取货', 2, N'maintenancePickUp')
INSERT INTO [dbo].[Menus] ([Id], [DateTime], [IsRemove], [MenuName], [Grade], [Path]) VALUES (6, N'2020-10-15 12:51:33', 0, N'查看所有报修', 1, N'allRepairs')
INSERT INTO [dbo].[Menus] ([Id], [DateTime], [IsRemove], [MenuName], [Grade], [Path]) VALUES (8, N'2020-10-15 12:51:33', 0, N'进出货', 1, N'importAndExport')
INSERT INTO [dbo].[Menus] ([Id], [DateTime], [IsRemove], [MenuName], [Grade], [Path]) VALUES (9, N'2020-10-15 12:51:33', 0, N'进出货记录', 1, N'importAndExportInfo')
INSERT INTO [dbo].[Menus] ([Id], [DateTime], [IsRemove], [MenuName], [Grade], [Path]) VALUES (10, N'2020-10-15 12:51:33', 0, N'权限管理', 1, N'power')
INSERT INTO [dbo].[Menus] ([Id], [DateTime], [IsRemove], [MenuName], [Grade], [Path]) VALUES (11, N'2020-10-15 12:51:33', 0, N'修改用户信息', 1, N'modifyEmployee')
INSERT INTO [dbo].[Menus] ([Id], [DateTime], [IsRemove], [MenuName], [Grade], [Path]) VALUES (13, N'2020-10-15 12:51:33', 0, N'数据报表', 1, N'dataReport')
SET IDENTITY_INSERT [dbo].[Menus] OFF


/*报修表数据*/
SET IDENTITY_INSERT [dbo].[ReportForRepairs] ON
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (1, N'2020-10-26 10:14:13', 0, 50, N'3层', N'三栋', N'333', N'333', 2, 1, 2, 48)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (2, N'2020-10-26 10:23:06', 0, 50, N'4层', N'四栋', N'444', N'325615', 2, 3, 2, 48)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (3, N'2020-10-26 10:23:16', 0, 50, N'5层', N'五栋', N'555', N'55555', 2, 2, 2, 48)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (4, N'2020-10-26 10:23:25', 0, 50, N'6层', N'六栋', N'666', N'78666666', 1, 0, 2, 48)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (5, N'2020-10-26 10:23:37', 0, 50, N'3层', N'五栋', N'433', N'3333333333333', 0, 0, 0, 0)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (6, N'2020-10-26 10:23:45', 0, 50, N'2层', N'一栋', N'111', N'1111111111111', 2, 2, 3, 49)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (7, N'2020-10-26 10:23:55', 0, 50, N'4层', N'四栋', N'417', N'777777777777777', 1, 0, 3, 49)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (8, N'2020-10-26 10:24:03', 0, 50, N'4层', N'四栋', N'417', N'rrrrrrrrrrrrrrrr', 1, 0, 2, 48)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (9, N'2020-10-26 10:24:12', 0, 50, N'3层', N'四栋', N'234', N'332222222222', 0, 0, 0, 0)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (10, N'2020-10-26 10:24:24', 0, 50, N'2层', N'二栋', N'222', N'44fffff', 0, 0, 0, 0)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (11, N'2020-10-26 10:24:37', 0, 50, N'2层', N'二栋', N'3ed', N'333333333333333333', 0, 0, 0, 0)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (12, N'2020-10-29 16:27:49', 0, 50, N'1层', N'三栋', N'111', N'1111111', 0, 0, 0, 0)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (13, N'2020-10-29 16:28:08', 0, 50, N'2层', N'四栋', N'111', N'1222', 0, 0, 0, 0)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (14, N'2020-10-29 16:28:15', 0, 50, N'3层', N'五栋', N'2', N'3fsda', 2, 1, 2, 48)
INSERT INTO [dbo].[ReportForRepairs] ([Id], [DateTime], [IsRemove], [UserId], [Layer], [Tung], [Dorm], [Desc], [WaitHandle], [Evaluate], [RoleId], [WorkerId]) VALUES (15, N'2020-11-04 09:05:31', 0, 60, N'4层', N'三栋', N'22', N'2222', 0, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[ReportForRepairs] OFF


/*角色表数据、由于后端代码有地方写死，请勿修改角色表数据*/
SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT INTO [dbo].[Roles] ([Id], [DateTime], [IsRemove], [RoleName]) VALUES (1, N'2020-10-10 08:58:47', 0, N'管理员')
INSERT INTO [dbo].[Roles] ([Id], [DateTime], [IsRemove], [RoleName]) VALUES (2, N'2020-10-10 08:58:49', 0, N'电工')
INSERT INTO [dbo].[Roles] ([Id], [DateTime], [IsRemove], [RoleName]) VALUES (3, N'2020-10-10 08:58:49', 0, N'木工')
INSERT INTO [dbo].[Roles] ([Id], [DateTime], [IsRemove], [RoleName]) VALUES (4, N'2020-10-10 08:58:49', 0, N'普通用户')
SET IDENTITY_INSERT [dbo].[Roles] OFF


/*用户与角色表数据*/
SET IDENTITY_INSERT [dbo].[UserRoles] ON
INSERT INTO [dbo].[UserRoles] ([Id], [DateTime], [IsRemove], [UserId], [RoleId]) VALUES (1, N'2020-10-10 09:12:25', 0, 1, 1)
INSERT INTO [dbo].[UserRoles] ([Id], [DateTime], [IsRemove], [UserId], [RoleId]) VALUES (47, N'2020-10-15 16:40:05', 0, 48, 2)
INSERT INTO [dbo].[UserRoles] ([Id], [DateTime], [IsRemove], [UserId], [RoleId]) VALUES (48, N'2020-10-15 16:40:17', 0, 49, 3)
INSERT INTO [dbo].[UserRoles] ([Id], [DateTime], [IsRemove], [UserId], [RoleId]) VALUES (49, N'2020-10-15 16:40:23', 0, 50, 4)
INSERT INTO [dbo].[UserRoles] ([Id], [DateTime], [IsRemove], [UserId], [RoleId]) VALUES (50, N'2020-10-21 15:01:52', 0, 53, 4)
INSERT INTO [dbo].[UserRoles] ([Id], [DateTime], [IsRemove], [UserId], [RoleId]) VALUES (51, N'2020-10-21 15:02:02', 0, 54, 4)
INSERT INTO [dbo].[UserRoles] ([Id], [DateTime], [IsRemove], [UserId], [RoleId]) VALUES (52, N'2020-10-21 15:04:21', 0, 55, 4)
INSERT INTO [dbo].[UserRoles] ([Id], [DateTime], [IsRemove], [UserId], [RoleId]) VALUES (53, N'2020-10-21 15:06:20', 0, 56, 3)
INSERT INTO [dbo].[UserRoles] ([Id], [DateTime], [IsRemove], [UserId], [RoleId]) VALUES (54, N'2020-10-21 15:06:53', 0, 57, 3)
INSERT INTO [dbo].[UserRoles] ([Id], [DateTime], [IsRemove], [UserId], [RoleId]) VALUES (55, N'2020-10-21 15:07:13', 0, 58, 4)
INSERT INTO [dbo].[UserRoles] ([Id], [DateTime], [IsRemove], [UserId], [RoleId]) VALUES (56, N'2020-10-21 15:08:23', 0, 59, 3)
INSERT INTO [dbo].[UserRoles] ([Id], [DateTime], [IsRemove], [UserId], [RoleId]) VALUES (67, N'2020-11-04 09:05:20', 0, 60, 4)
SET IDENTITY_INSERT [dbo].[UserRoles] OFF



/*用户表数据*/
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([Id], [DateTime], [IsRemove], [UserName], [Password], [Phone], [Mail]) VALUES (1, N'2020-10-09 16:04:29', 0, N'111', N'111', N'111', N'111')
INSERT INTO [dbo].[Users] ([Id], [DateTime], [IsRemove], [UserName], [Password], [Phone], [Mail]) VALUES (48, N'2020-10-15 14:01:57', 0, N'王大锤', N'111111', N'13345698722', N'13345698722@163.com')
INSERT INTO [dbo].[Users] ([Id], [DateTime], [IsRemove], [UserName], [Password], [Phone], [Mail]) VALUES (49, N'2020-10-15 14:02:26', 0, N'张三', N'222222', N'13533336666', N'13533336666@163.com')
INSERT INTO [dbo].[Users] ([Id], [DateTime], [IsRemove], [UserName], [Password], [Phone], [Mail]) VALUES (50, N'2020-10-15 14:02:48', 0, N'李四', N'444444', N'18048564444', N'18048564444@163.com')
INSERT INTO [dbo].[Users] ([Id], [DateTime], [IsRemove], [UserName], [Password], [Phone], [Mail]) VALUES (53, N'2020-10-21 15:01:52', 0, N'ijnhub', N'xxxxxx', N'18307001845', N'2424117373@qq.com')
INSERT INTO [dbo].[Users] ([Id], [DateTime], [IsRemove], [UserName], [Password], [Phone], [Mail]) VALUES (54, N'2020-10-21 15:02:02', 0, N'lilei', N'123456', N'18307001845', N'2424117373@qq.com')
INSERT INTO [dbo].[Users] ([Id], [DateTime], [IsRemove], [UserName], [Password], [Phone], [Mail]) VALUES (55, N'2020-10-21 15:04:21', 0, N'dfsfsdf', N'fsdfsdfsfsdf', N'18307001845', N'2424117373@qq.com')
INSERT INTO [dbo].[Users] ([Id], [DateTime], [IsRemove], [UserName], [Password], [Phone], [Mail]) VALUES (56, N'2020-10-21 15:06:20', 0, N'kfjdskf', N'adsasdaqqq', N'18307001845', N'2424117373@qq.com')
INSERT INTO [dbo].[Users] ([Id], [DateTime], [IsRemove], [UserName], [Password], [Phone], [Mail]) VALUES (57, N'2020-10-21 15:06:53', 0, N'128665gfd', N'sdasddd', N'18307001845', N'2424117373@qq.com')
INSERT INTO [dbo].[Users] ([Id], [DateTime], [IsRemove], [UserName], [Password], [Phone], [Mail]) VALUES (58, N'2020-10-21 15:07:13', 0, N'cxvfsedf', N'sd222', N'18307001845', N'2424117373@qq.com')
INSERT INTO [dbo].[Users] ([Id], [DateTime], [IsRemove], [UserName], [Password], [Phone], [Mail]) VALUES (59, N'2020-10-21 15:08:23', 0, N'45tgde', N'xxxxxx', N'18307001845', N'2424117373@qq.com')
INSERT INTO [dbo].[Users] ([Id], [DateTime], [IsRemove], [UserName], [Password], [Phone], [Mail]) VALUES (60, N'2020-11-04 09:05:20', 0, N'rrr', N'rrr', N'18307001845', N'2424117373@qq.com')
SET IDENTITY_INSERT [dbo].[Users] OFF

/*角色与报修与仓库表数据*/
SET IDENTITY_INSERT [dbo].[UserWareHouses] ON
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (8, N'2020-10-15 16:42:52', 0, 1, 3, N'水管', 100, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (9, N'2020-10-15 16:42:54', 0, 1, 3, N'水管', 100, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (10, N'2020-10-15 16:43:13', 0, 1, 4, N'烟囱', 50, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (11, N'2020-10-15 16:43:18', 0, 1, 5, N'锤子', 50, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (12, N'2020-10-15 16:43:24', 0, 1, 6, N'铁板', 50, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (13, N'2020-10-15 16:43:27', 0, 1, 7, N'木板', 50, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (14, N'2020-10-16 09:09:19', 0, 48, 3, N'水管', 0, 99, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (15, N'2020-10-29 17:00:12', 0, 1, 5, N'锤子', 5, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (16, N'2020-10-29 17:00:25', 0, 1, 3, N'水管', 5, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (17, N'2020-10-29 17:00:33', 0, 1, 8, N'铁管', 5, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (18, N'2020-11-02 09:23:57', 0, 1, 3, N'水管', 0, 6, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (19, N'2020-11-02 09:28:20', 0, 1, 3, N'水管', 0, 2, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (20, N'2020-11-02 09:28:24', 0, 1, 3, N'水管', 0, 2, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (21, N'2020-11-02 09:28:57', 0, 1, 3, N'水管', 0, 2, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (22, N'2020-11-02 09:29:00', 0, 1, 3, N'水管', 0, 2, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (23, N'2020-11-02 09:39:36', 0, 1, 6, N'铁板', 0, 25, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (24, N'2020-11-02 09:40:38', 0, 1, 9, N'钉子', 30, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (25, N'2020-11-02 09:41:41', 0, 1, 9, N'钉子', 10, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (26, N'2020-11-02 09:41:57', 0, 1, 9, N'钉子', 10, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (27, N'2020-11-02 09:42:50', 0, 1, 7, N'木板', 20, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (28, N'2020-11-02 09:43:23', 0, 1, 10, N'电线', 50, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (29, N'2020-11-02 09:43:27', 0, 1, 10, N'电线', 50, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (30, N'2020-11-02 09:43:40', 0, 1, 11, N'钢筋', 50, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (31, N'2020-11-02 09:43:49', 0, 1, 12, N'混泥土', 50, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (32, N'2020-11-02 09:44:02', 0, 1, 13, N'百草枯', 50, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (33, N'2020-11-02 09:53:39', 0, 1, 13, N'百草枯', 0, 2, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (34, N'2020-11-02 09:53:51', 0, 1, 14, N'玻璃杯', 1, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (35, N'2020-11-02 15:11:35', 0, 48, 3, N'水管', 0, 10, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (36, N'2020-11-02 15:11:52', 0, 48, 10, N'电线', 0, 10, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (37, N'2020-11-04 08:58:25', 0, 49, 12, N'混泥土', 0, 50, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (38, N'2020-11-04 08:58:50', 0, 1, 12, N'混泥土', 500, 0, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (39, N'2020-11-04 20:44:20', 0, 1, 3, N'水管', 0, 1, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (40, N'2020-11-04 20:55:23', 0, 1, 3, N'水管', 0, 1, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (41, N'2020-11-04 20:55:28', 0, 1, 4, N'烟囱', 0, 1, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (42, N'2020-11-04 20:55:34', 0, 1, 5, N'锤子', 0, 1, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (43, N'2020-11-04 20:55:48', 0, 1, 3, N'水管', 0, 1, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (44, N'2020-11-04 20:56:36', 0, 1, 3, N'水管', 0, 1, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (45, N'2020-11-04 20:57:17', 0, 1, 3, N'水管', 0, 1, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (46, N'2020-11-04 20:57:35', 0, 1, 3, N'水管', 0, 1, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (47, N'2020-11-04 21:00:00', 0, 1, 3, N'水管', 0, 1, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (48, N'2020-11-04 21:00:06', 0, 1, 4, N'烟囱', 0, 1, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (49, N'2020-11-04 21:00:10', 0, 1, 5, N'锤子', 0, 1, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (50, N'2020-11-10 15:51:58', 0, 48, 3, N'水管', 0, 5, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (51, N'2020-11-10 15:52:04', 0, 48, 3, N'水管', 0, 5, 0, 0)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (52, N'2020-11-10 15:53:39', 0, 48, 3, N'水管', 0, 10, 0, 8)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (53, N'2020-11-10 15:54:31', 0, 48, 7, N'木板', 0, 10, 0, 8)
INSERT INTO [dbo].[UserWareHouses] ([Id], [DateTime], [IsRemove], [UserId], [GoodsId], [Goods], [Purchase], [PickUp], [ReportForRepairId], [UserWareHouseId]) VALUES (54, N'2020-11-11 09:59:30', 0, 48, 3, N'水管', 0, 50, 0, 8)
SET IDENTITY_INSERT [dbo].[UserWareHouses] OFF




/*仓库表数据*/

SET IDENTITY_INSERT [dbo].[WareHouses] ON
INSERT INTO [dbo].[WareHouses] ([Id], [DateTime], [IsRemove], [Goods], [Number]) VALUES (3, N'2020-10-15 16:42:52', 0, N'水管', 5)
INSERT INTO [dbo].[WareHouses] ([Id], [DateTime], [IsRemove], [Goods], [Number]) VALUES (4, N'2020-10-15 16:43:13', 0, N'烟囱', 48)
INSERT INTO [dbo].[WareHouses] ([Id], [DateTime], [IsRemove], [Goods], [Number]) VALUES (5, N'2020-10-15 16:43:18', 0, N'锤子', 53)
INSERT INTO [dbo].[WareHouses] ([Id], [DateTime], [IsRemove], [Goods], [Number]) VALUES (6, N'2020-10-15 16:43:24', 0, N'铁板', 25)
INSERT INTO [dbo].[WareHouses] ([Id], [DateTime], [IsRemove], [Goods], [Number]) VALUES (7, N'2020-10-15 16:43:27', 0, N'木板', 60)
INSERT INTO [dbo].[WareHouses] ([Id], [DateTime], [IsRemove], [Goods], [Number]) VALUES (8, N'2020-10-29 17:00:33', 0, N'铁管', 5)
INSERT INTO [dbo].[WareHouses] ([Id], [DateTime], [IsRemove], [Goods], [Number]) VALUES (9, N'2020-11-02 09:40:38', 0, N'钉子', 50)
INSERT INTO [dbo].[WareHouses] ([Id], [DateTime], [IsRemove], [Goods], [Number]) VALUES (10, N'2020-11-02 09:43:23', 0, N'电线', 90)
INSERT INTO [dbo].[WareHouses] ([Id], [DateTime], [IsRemove], [Goods], [Number]) VALUES (11, N'2020-11-02 09:43:40', 0, N'钢筋', 50)
INSERT INTO [dbo].[WareHouses] ([Id], [DateTime], [IsRemove], [Goods], [Number]) VALUES (12, N'2020-11-02 09:43:49', 0, N'混泥土', 500)
INSERT INTO [dbo].[WareHouses] ([Id], [DateTime], [IsRemove], [Goods], [Number]) VALUES (13, N'2020-11-02 09:44:02', 0, N'百草枯', 48)
INSERT INTO [dbo].[WareHouses] ([Id], [DateTime], [IsRemove], [Goods], [Number]) VALUES (14, N'2020-11-02 09:53:51', 0, N'玻璃杯', 1)
SET IDENTITY_INSERT [dbo].[WareHouses] OFF














