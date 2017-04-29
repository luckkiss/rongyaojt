using System;

namespace Cross
{
	public interface IURLReq
	{
		string url
		{
			get;
			set;
		}

		string contentType
		{
			get;
			set;
		}

		string dataFormat
		{
			get;
			set;
		}

		string data
		{
			get;
			set;
		}

		string method
		{
			get;
			set;
		}

		void load(Action<IURLReq, object> onFin, Action<IURLReq, float> onProg = null, Action<IURLReq, string> onFail = null);

		void close();
	}
}
