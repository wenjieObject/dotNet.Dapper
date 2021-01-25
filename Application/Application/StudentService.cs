using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application
{
    public class StudentService : IContract
    {
        //请求示例
//        <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/">
//   <soapenv:Header/>
//   <soapenv:Body>
//      <tem:Get>
//         <!--Optional:-->
//         <tem:no>1312312312</tem:no>
//      </tem:Get>
//   </soapenv:Body>
//</soapenv:Envelope>

//        /// <summary>
        /// 
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public StudentModel Get(string no)
        {
            return new StudentModel { Sno= no, Name="张三",Grade="0", Birthday =DateTime.Now};
        }
    }
}
