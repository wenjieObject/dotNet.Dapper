using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using IRepositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ViewModel;

namespace Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrintController : Controller
    {
        private IPrintRepository _iPrintRepository;

        private IPrintOutRepository _iPrintOutRepository;

        public PrintController(IPrintRepository iPrintRepository, IPrintOutRepository iPrintOutRepository)
        {
            _iPrintRepository = iPrintRepository;
            _iPrintOutRepository = iPrintOutRepository;
        }

        [HttpPost]
        [Route("inLabel")]
        public ResultInfo GetPrintInLabel([FromBody] PRINT_DATA_N jsonData)
        {
            try
            {
                PRINT_DATA_N printData = jsonData;

                printData.GUID = Guid.NewGuid().ToString();
                printData.FACTORY = "3100";
                printData.CREATOR = "user";
                printData.CREATE_TIME = DateTime.Now;
                printData.MODIFY_TIME = DateTime.Now;

                var isSuccess = _iPrintRepository.InsertOne(printData) > 0;
                if (isSuccess)
                {
                    return new ResultInfo { code = 0, msg = "执行成功" };
                }

                return new ResultInfo { code = 500, msg = "执行失败" };
            }
            catch (Exception e)
            {
                return new ResultInfo { code = 500, msg = e.Message };
            }
        }

        [HttpPost]
        [Route("outLabel")]
        public ResultInfo GetPrintOutLabel([FromBody] PrintDataOut jsonData)
        {
            try
            {
                PRINT_DATA_O printData = new PRINT_DATA_O() ;
                CopyModel(jsonData, printData);

                printData.BATCH_LIST= string.Join(",", jsonData.BATCH_LIST);

                printData.GUID = Guid.NewGuid().ToString();
                printData.FACTORY = "3100";
                printData.CREATOR = "user";
                printData.CREATE_TIME = DateTime.Now;
                printData.MODIFY_TIME = DateTime.Now;

                var isSuccess = _iPrintOutRepository.InsertOne(printData) > 0;
                if (isSuccess)
                {
                    return new ResultInfo { code = 0, msg = "执行成功" };
                }

                return new ResultInfo { code = 500, msg = "执行失败" };
            }
            catch (Exception e)
            {
                return new ResultInfo { code = 500, msg = e.Message };
            }
        }

        public static void CopyModel(object from, object to)
        {
            if (from == null || to == null)
            {
                return;
            }

            PropertyDescriptorCollection fromProperties = TypeDescriptor.GetProperties(from);
            PropertyDescriptorCollection toProperties = TypeDescriptor.GetProperties(to);

            foreach (PropertyDescriptor fromProperty in fromProperties)
            {
                PropertyDescriptor toProperty = toProperties.Find(fromProperty.Name, true /* ignoreCase */);
                if (toProperty != null && !toProperty.IsReadOnly)
                {
                    // Can from.Property reference just be assigned directly to to.Property reference?
                    bool isDirectlyAssignable = toProperty.PropertyType.IsAssignableFrom(fromProperty.PropertyType);
                    // Is from.Property just the nullable form of to.Property?
                    bool liftedValueType = (isDirectlyAssignable) ? false : (Nullable.GetUnderlyingType(fromProperty.PropertyType) == toProperty.PropertyType);

                    if (isDirectlyAssignable || liftedValueType)
                    {
                        object fromValue = fromProperty.GetValue(from);
                        if (isDirectlyAssignable || (fromValue != null && liftedValueType))
                        {
                            toProperty.SetValue(to, fromValue);
                        }
                    }
                }
            }
        }


    }

}
