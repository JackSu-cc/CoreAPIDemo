using Common.IService;
using Domain.Cmds;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApl.Filter
{
    public class CusExceptionFilterAttribute : IExceptionFilter
    {
        private readonly IMediatorService _mediator;
        public CusExceptionFilterAttribute(IMediatorService mediator)
        {
            this._mediator = mediator;
        }

        public void OnException(ExceptionContext context)
        {
            //获取当前的访问路径
            var pa = context.HttpContext.Request.Path;
            // IServiceProvider serviceProvider = context.HttpContext.RequestServices.GetService<I>();

            AddErrorLogCommand log = new AddErrorLogCommand()
            {
                CreateBy = "cc",
                Action = pa,
                Module = pa,
                LogInfo = context.Exception.Message,
                Ip = context.HttpContext.Request.Host.Value
            };

            this._mediator.Send<AddErrorLogCommand>(log);
        }
    }
}
