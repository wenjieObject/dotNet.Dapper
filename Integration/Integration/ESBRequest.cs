using System;
using System.Collections.Generic;
using System.Text;

namespace Integration
{
	public class ESBRequest<T>
	{
		 
		public string factory_no { get; set; }

		public string datetime { get; set; }

		public string requestid { get; set; }

		public List<T> datas { get; set; }
	}
}
