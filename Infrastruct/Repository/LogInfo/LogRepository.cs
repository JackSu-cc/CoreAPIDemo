
//******************************************************************/
// Copyright (C) 2021 All Rights Reserved.
// CLR版本：4.0.30319.42000
// 版本号：V1.0.0
// 文件名：LogRepository
// 创建者：名字 (cc)
// 创建时间：2021/1/11 22:33:11
//
// 描述：
//
// 
//=================================================================
// 修改人：
// 时间：
// 修改说明：
// 
//******************************************************************/


using Domain.IRepository;
using Domain.Models.LogInfo;
using Infrastruct.Context;
using Infrastruct.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastruct.Repository.LogInfo
{
    public class LogRepository : EFRepository<ErrorLog>, ILogRepository
    {
        private readonly CoreDemoDBContext _context;
        public LogRepository(CoreDemoDBContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }
    }
}
