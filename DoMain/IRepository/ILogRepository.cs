
//******************************************************************/
// Copyright (C) 2021 All Rights Reserved.
// CLR版本：4.0.30319.42000
// 版本号：V1.0.0
// 文件名：ILogRepository
// 创建者：名字 (cc)
// 创建时间：2021/1/11 22:34:05
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


using Common.BaseInterfaces.IBaseRepository.IRepository;
using Domain.Models.LogInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IRepository
{
    public interface ILogRepository : IEFRepository<ErrorLog>
    {
    }
}
