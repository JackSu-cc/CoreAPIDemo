
//******************************************************************/
// Copyright (C) 2021 All Rights Reserved.
// CLR版本：4.0.30319.42000
// 版本号：V1.0.0
// 文件名：ErrorLog
// 创建者：名字 (cc)
// 创建时间：2021/1/11 22:15:56
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


using Common.BaseInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.LogInfo
{
    public class ErrorLog : IAggregateRoot, ICreate
    {
        public long Id { get; set; }

        public string LogInfo { get; set; }

        public string Module { get; set; }

        public string Action { get; set; }

        public string Ip { get; set; }

         

        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
