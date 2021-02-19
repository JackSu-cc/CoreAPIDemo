
//******************************************************************/
// Copyright (C) 2021 All Rights Reserved.
// CLR版本：4.0.30319.42000
// 版本号：V1.0.0
// 文件名：LogService
// 创建者：名字 (cc)
// 创建时间：2021/1/11 22:44:41
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


using Application.IService.ILogService;
using Application.ViewModel.Log;
using Common.IService;
using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.LogService
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IMediatorService _mediator;
        public LogService(
            ILogRepository logRepository,
            IMediatorService mediator)
        {
            this._logRepository = logRepository;
            this._mediator = mediator;
        }



        public async Task AddLog(ErrorLogRequestVM request)
        {
           
        }
    }
}
