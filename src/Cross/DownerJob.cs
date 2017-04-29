using System;

namespace Cross
{
	public class DownerJob
	{
		public string url;

		public string method;

		public string dataFmt;

		public string paras;

		public Action<object> onFin;

		public Action<float> onProg;

		public Action<string> onFail;

		public DownerJob(string _url, string _method, string _dataFmt, string _paras, Action<object> _onFin, Action<float> _onProg, Action<string> _onFail)
		{
			this.url = _url;
			this.method = _method;
			this.dataFmt = _dataFmt;
			this.paras = _paras;
			this.onFin = _onFin;
			this.onProg = _onProg;
			this.onFail = _onFail;
		}
	}
}
