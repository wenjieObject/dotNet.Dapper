using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Utils;

namespace Integration
{
    public class ESBApiRepository
    {
        public string GetESBOpration(string systemId)
        {
            return ConfigurationUtil.Configuration["ESBApiServerOprationCode:" + systemId];
        }

        public string GetESBUrl(string systemId)
        {
            return ConfigurationUtil.Configuration["ESBApiServerUrls:" + systemId];
        }


		public ESBResult<TResponse> ExcuteApi<TResponse, TRequestDatas>(List<TRequestDatas> requestDatas, string systemId, string serviceName)
		{
			bool flag = string.IsNullOrWhiteSpace(this.GetESBUrl(systemId)) || string.IsNullOrWhiteSpace(this.GetESBOpration(systemId));
			if (flag)
			{
				throw new Exception(systemId + ":url或Opration未在MES配置注册");
			}
			string url = "http://" + this.GetESBUrl(systemId) + "/" + serviceName;
			ESBRequest<TRequestDatas> esbrequest = new ESBRequest<TRequestDatas>();
			string requestid = Guid.NewGuid().ToString();
			esbrequest.requestid = requestid;
			esbrequest.datetime = DateTime.Now.ToString("yyyyMMddHHmmssffff");
			//工厂
			esbrequest.factory_no = "";
			esbrequest.datas = requestDatas;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			//mes的代号
			dictionary.Add("ClientId", "com.boway.esb.mes.mes");
			dictionary.Add("OperationCode", this.GetESBOpration(systemId) + "." + serviceName.Replace("/", "."));
			string text = "";
			bool flag2 = esbrequest != null;
			if (flag2)
			{
				text = RequestUtil.HttpPost(url, JsonConvert.SerializeObject(esbrequest), "application/json", dictionary, 30);
			}
			ESBResponse<TResponse> esbresponse = JsonConvert.DeserializeObject<ESBResponse<TResponse>>(text);
			bool flag3 = esbresponse.code == 1;
			if (flag3)
			{
				throw new Exception("ESB:" + esbresponse.msg);
			}
			return esbresponse.result;
		}

	}
}
