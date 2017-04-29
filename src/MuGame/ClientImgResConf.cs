using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class ClientImgResConf : configParser
	{
		private Variant _imgInfo;

		public ClientImgResConf(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new ClientImgResConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("img");
			if (flag)
			{
				this._imgInfo = GameTools.array2Map(conf["img"], "name", 1u);
			}
			return null;
		}

		public string GetImgRes(string name)
		{
			bool flag = this._imgInfo[name] != null;
			string result;
			if (flag)
			{
				result = this._imgInfo[name]["res"];
			}
			else
			{
				result = "";
			}
			return result;
		}
	}
}
