using Entity;
using IRepositories;
using Newtonsoft.Json;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;

namespace WebService
{
    public class PrintService : IContract
    {
 
        public ResultInfo Get(string jsonData)
        {
            try
            {
                PRINT_DATA_N printData = JsonConvert.DeserializeObject<PRINT_DATA_N>(jsonData);

                printData.GUID = Guid.NewGuid().ToString();
                printData.FACTORY = "3100";
                printData.CREATOR = "user";
                printData.CREATE_TIME = DateTime.Now;
                printData.MODIFY_TIME = DateTime.Now;


                IPrintRepository printRepository = new PrintRepository();


                var isSuccess =  printRepository.InsertOne(printData)>0;
                if (isSuccess)
                {
                    return new ResultInfo { code = 0, msg = "执行成功" };
                }
                
                return new ResultInfo { code = 500, msg = "执行失败" };
            }
            catch(Exception e)
            {
                return new ResultInfo { code = 500,msg=e.Message };
            }
            
        }
    }
}
