﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace kangfupanda.dataentity.Model
{
    public class Video
    {
        public int id { get; set; }
        /// <summary>
        /// 视频名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 视频作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public int duration { get; set; }

        /// <summary>
        /// 视频存放地址
        /// </summary>
        public string videoUri { get; set; }

        /// <summary>
        /// 视频封面地址
        /// </summary>
        public string posterUri { get; set; }


        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime createdAt { get; set; }

        /// <summary>
        /// 删除日期
        /// </summary>
        public DateTime? expiredAt { get; set; }
    }
}
