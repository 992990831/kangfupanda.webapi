using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kangfupanda.webapi.Common
{
    public enum AuditStatus
    {
        /// <summary>
        /// 未审核
        /// </summary>
        Pending=0,
        /// <summary>
        /// 审核通过
        /// </summary>
        Approved=1,
        /// <summary>
        /// 审核不通过
        /// </summary>
        Rejected=2
    }
}