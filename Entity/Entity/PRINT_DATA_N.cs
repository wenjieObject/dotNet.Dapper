using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{

    public class PRINT_DATA_N : BaseEntity
    {
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string CST_ORD_NO { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public string CST_NO { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CST_NAME { get; set; }
        /// <summary>
        /// 客户物料号
        /// </summary>
        public string CST_PN { get; set; }
        /// <summary>
        /// 牌号
        /// </summary>
        public string CARD_NUMB { get; set; }
        /// <summary>
        /// 合金状态
        /// </summary>
        public string ALLOY_STATE { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string MAT_SIZE { get; set; }
        /// <summary>
        /// 批号
        /// </summary>
        public string BATCH_NO { get; set; }
        /// <summary>
        /// 客户批号
        /// </summary>
        public string CST_BATCH_NO { get; set; }
        /// <summary>
        /// 净重
        /// </summary>
        public string WEIGHT { get; set; }
        /// <summary>
        /// 报工日期
        /// </summary>
        public string BG_DATE { get; set; }

    }
}
