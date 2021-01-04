
//******************************************************************/
// Copyright (C) 2021 All Rights Reserved.
// CLR版本：4.0.30319.42000
// 版本号：V1.0.0
// 文件名：MyExceptionFilterAttribute
// 创建者：名字 (cc)
// 创建时间：2021/1/3 19:08:11
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


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Filter
{
    public class CusExceptionFilterAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //获取当前的访问路径
            var pa = context.HttpContext.Request.Path;
            IServiceProvider serviceProvider= context.HttpContext.RequestServices.GetService<IUserService>();
        }


        /// <summary>
        /// 判断是否为ajax请求(MVC使用)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool IsAjaxRequest(HttpRequest request)
        {
            string header = request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(header);
        }

    }
}
