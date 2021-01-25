
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    [Table("SSD_DEPARTMENT")]
    public class SSD_DEPARTMENT:BaseEntity
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        [Column("DEPARTMENT_CODE")]
        public string DEPARTMENT_CODE { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Column("DEPARTMENT_NAME")]
        public string DEPARTMENT_NAME { get; set; }
        /// <summary>
        /// 上级部门
        /// </summary>
        [Column("HIGHER_DEPARTMENT")]
        public string HIGHER_DEPARTMENT { get; set; }
 
     
 

    }
}
