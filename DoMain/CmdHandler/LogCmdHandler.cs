
//******************************************************************/
// Copyright (C) 2021 All Rights Reserved.
// CLR版本：4.0.30319.42000
// 版本号：V1.0.0
// 文件名：LogCmdHandler
// 创建者：名字 (cc)
// 创建时间：2021/1/11 22:51:24
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


using Common.BaseInterfaces.IBaseRepository;
using Common.Cmds;
using Common.IService;
using Common.Notice;
using Domain.Cmds; 
using Domain.IRepository;
using Domain.Models.LogInfo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.CmdHandler
{
    public class LogCmdHandler : CommandHandler
          , IRequestHandler<AddErrorLogCommand, Unit>
    {
        private readonly ILogRepository _logRepository;
        public LogCmdHandler(
            ILogRepository logRepository,
            IUnitOfWork unitOfWork,
            IMediatorService mediatorService) : base(unitOfWork, mediatorService)
        {
            this._logRepository = logRepository;
        }


        public Task<Unit> Handle(AddErrorLogCommand request, CancellationToken cancellationToken)
        {
            ErrorLog log = new ErrorLog()
            {
                CreateBy = request.CreateBy,
                Action = request.Action,
                Ip = request.Ip,
                LogInfo = request.LogInfo,
                Module = request.Module,
                CreateTime = DateTime.Now
            };

            _logRepository.Add(log);

            if (Commit())
            {
                //执行成功后发送事件

            }
            else
            {
                _mediator.Publish(new Notification("", "发生异常！"));
            }

            return Task.FromResult(new Unit());
        }
    }
}
