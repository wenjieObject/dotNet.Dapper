using System;
using System.Collections.Generic;
using System.Text;

namespace Integration
{
	public class ESBResponse<T>
	{
		 
		public string esbrequestid { get; set; }

	 
		public int code { get; set; }

	 
		public string msg { get; set; }

		 
		public ESBResult<T> result { get; set; }
	}
}
