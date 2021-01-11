
//******************************************************************/
// Copyright (C) 2021 All Rights Reserved.
// CLR版本：4.0.30319.42000
// 版本号：V1.0.0
// 文件名：ILogService
// 创建者：名字 (cc)
// 创建时间：2021/1/11 22:45:09
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


using Application.ViewModel.Log;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService.ILogService
{
    public interface ILogService
    {

        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task AddLog(ErrorLogRequestVM request);
    }
}
