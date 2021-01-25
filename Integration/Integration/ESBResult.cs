using System;
using System.Collections.Generic;
using System.Text;

namespace Integration
{

	public class ESBResult<T>
	{
	 
		public int code { get; set; }

	 
		public string msg { get; set; }

	 
		public DateTime? data_time { get; set; }

 
		public int count { get; set; }

 
		public List<T> datas { get; set; }
	}
}
