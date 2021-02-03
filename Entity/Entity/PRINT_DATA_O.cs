using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class PRINT_DATA_O : BaseEntity
    {
        public string TRAY_NO { get; set; }
        public string CST_ORD_NO { get; set; }
        public string CST_NO { get; set; }
        public string CST_NAME { get; set; }
        public string CST_PN { get; set; }
        public string CARD_NUMB { get; set; }
        public string ALLOY_STATE { get; set; }
        public string MAT_SIZE { get; set; }
        public string ALL_WEIGHT { get; set; }

        /// <summary>
        /// 子卷批次号
        /// </summary>
        public string BATCH_LIST { get; set; }
        public string HARDNESS { get; set; }
        public string TRAY_FLAG { get; set; }
        /// <summary>
        /// EDITION
        /// </summary>
        public string EDITION { get; set; }

    }
}
