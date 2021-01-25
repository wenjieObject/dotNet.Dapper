using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using ViewModel;

namespace WebService
{
    [ServiceContract]
     public interface IContract
    {
  
        [OperationContract]
        public ResultInfo Get(string jsonData);
    }
}
