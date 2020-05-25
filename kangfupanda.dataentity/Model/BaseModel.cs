using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.Model
{
    public abstract class BaseModel
    {
        public int id { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime createdAt { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? updatedAt { get; set; }

        /// <summary>
        /// 删除日期
        /// </summary>
        public DateTime? expiredAt { get; set; }
    }
}
