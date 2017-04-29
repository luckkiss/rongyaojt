using Cross;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GameFramework
{
	public abstract class ClientConfig : clientBase
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly ClientConfig.<>c <>9 = new ClientConfig.<>c();

			public static Action <>9__5_0;

			internal void <formatClientconf>b__5_0()
			{
			}
		}

		private int _loadedFileCnt;

		private int _totalToLoadFileCnt;

		private ConfigManager confM
		{
			get
			{
				return CrossApp.singleton.getPlugin("conf") as ConfigManager;
			}
		}

		public ClientConfig(gameMain m) : base(m)
		{
		}

		public override void init()
		{
			this.onInit();
			this.confM.regFormatFunc("client", new Action<Variant>(this.formatClientconf));
		}

		protected abstract void onInit();

		protected void formatClientconf(Variant conf)
		{
			for (int i = 0; i < conf["conf"].Count; i++)
			{
				Variant variant = conf["conf"][i];
				string str = variant["name"]._str;
				configParser configParser = base.createInst(str, true) as configParser;
				bool flag = configParser == null;
				if (flag)
				{
					DebugTrace.print(" configParser [" + str + "] create failed! ");
				}
				else
				{
					configParser.initSet(variant["file"]._str, variant["preload"]._bool);
				}
			}
			Action arg_CA_1;
			if ((arg_CA_1 = ClientConfig.<>c.<>9__5_0) == null)
			{
				arg_CA_1 = (ClientConfig.<>c.<>9__5_0 = new Action(ClientConfig.<>c.<>9.<formatClientconf>b__5_0));
			}
			this.loadPreloadClientConfig(arg_CA_1);
		}

		private void loadPreloadClientConfig(Action onFin)
		{
			List<configParser> list = new List<configParser>();
			foreach (IObjectPlugin current in this.m_objectPlugins.Values)
			{
				configParser configParser = current as configParser;
				bool preload = configParser.preload;
				if (preload)
				{
					list.Add(configParser);
				}
			}
			this._loadedFileCnt = 0;
			this._totalToLoadFileCnt = list.Count;
			this._loadNextPreloadClientConfig(list, onFin);
		}

		protected void _loadNextPreloadClientConfig(List<configParser> toLoadClientConfigVec, Action onFin)
		{
			bool flag = toLoadClientConfigVec.Count <= 0;
			if (flag)
			{
				DebugTrace.print("configParser ended!");
				onFin();
			}
			else
			{
				this._loadedFileCnt++;
				configParser configParser = toLoadClientConfigVec[toLoadClientConfigVec.Count - 1];
				DebugTrace.print("try configParser[" + configParser.controlId + "]");
				toLoadClientConfigVec.RemoveAt(toLoadClientConfigVec.Count - 1);
				configParser.loadconfig(this.confM, delegate(configParser confbase)
				{
					this._loadNextPreloadClientConfig(toLoadClientConfigVec, onFin);
				});
			}
		}

		public void loadConfigs(string fileName, Action onfin)
		{
			this.confM.loadConfigs(fileName, onfin);
		}
	}
}
