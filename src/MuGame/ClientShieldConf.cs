using Cross;
using GameFramework;
using System;
using System.Text.RegularExpressions;

namespace MuGame
{
	public class ClientShieldConf : configParser
	{
		private Variant _strArr = null;

		public ClientShieldConf(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new ClientShieldConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			return conf;
		}

		private void _formatStr()
		{
			this._strArr = GameTools.split(this.m_conf["shield"][0]["text"], ",", 1u);
			for (int i = 0; i < this._strArr.Length; i++)
			{
				this._strArr[i] = Regex.Replace(this._strArr[i]._str, "\\s", "");
			}
		}

		public Variant isHaveWord(string str)
		{
			bool flag = this._strArr == null;
			if (flag)
			{
				this._formatStr();
			}
			string text = Regex.Replace(str, "\\s", "");
			Variant variant = new Variant();
			bool val = false;
			for (int i = 0; i < this._strArr.Length; i++)
			{
				string text2 = this._strArr[i];
				bool flag2 = text2 == "";
				if (!flag2)
				{
					while (true)
					{
						bool flag3 = text.IndexOf(text2) != -1;
						if (!flag3)
						{
							break;
						}
						text = text.Replace(text2, "*");
						val = true;
					}
				}
			}
			variant._arr.Add(val);
			variant._arr.Add(text);
			return variant;
		}
	}
}
