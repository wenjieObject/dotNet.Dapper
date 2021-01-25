using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;

namespace Application
{
    [ServiceContract]
     interface IContract
    {
        /// <summary>
        /// 查询学生信息
        /// </summary>
        /// <param name="sno">学号</param>
        /// <returns>学生信息</returns>
        [OperationContract]
        StudentModel Get(string no);
    }
}
