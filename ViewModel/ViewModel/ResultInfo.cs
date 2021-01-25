using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class ResultInfo
    {
        /// <summary>
        /// 编码 0:执行成功 <>0:执行失败
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 回复描述
        /// </summary>
        public string msg { get; set; }

    }
}
