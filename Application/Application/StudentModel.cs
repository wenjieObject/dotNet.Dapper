using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Application
{
    [DataContract]
    public class StudentModel
    {
        /// <summary>
        /// 学号
        /// </summary>
        [DataMember]
        public string Sno { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 班级
        /// </summary>
        [DataMember]
        public string Grade { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [DataMember]
        public DateTime Birthday { get; set; }
    }
}
