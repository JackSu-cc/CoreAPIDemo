using Common.Cmds;
using System;
using System.Collections.Generic;
using System.Text;



/******************************************************
 * 公司名称：       智驾出行
 * 创 建 人：       kongdf
 * 创建时间：       2021/2/19 10:07:06
 * 说    明：
*******************************************************/
namespace Domain.Cmds
{
    /// <summary>
    /// 
    /// </summary>
    public class AddErrorLogCommand : Command
    {
        public override bool IsValid()
        {
            return true;
        }

        public string CreateBy { get; set; }

        public string Action { get; set; }

        public string Ip { get; set; }

        public string LogInfo { get; set; }

        public string Module { get; set; }
    }
}
