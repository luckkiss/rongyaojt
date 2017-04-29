using System;
using System.Collections.Generic;

namespace Cross
{
	public class LocalizeManager : IAppPlugin
	{
		protected Variant m_conf = null;

		protected string m_localResUrlPrefix = "";

		protected Dictionary<string, string> m_loadedFile = new Dictionary<string, string>();

		public string localizeURLPrefix
		{
			get
			{
				return this.m_localResUrlPrefix;
			}
		}

		public string pluginName
		{
			get
			{
				return "localize";
			}
		}

		public void onInit()
		{
			ConfigManager configManager = CrossApp.singleton.getPlugin("conf") as ConfigManager;
			configManager.regFormatFunc("localize", new Action<Variant>(this._formatLocalizeConf));
		}

		protected void _formatLocalizeConf(Variant conf)
		{
			this.m_conf = conf;
			this.m_localResUrlPrefix = conf["path"] + "/" + conf["defzone"] + "/";
		}

		public bool isLanguagePackLoaded(string file)
		{
			return this.m_loadedFile.ContainsKey(file);
		}

		public void loadPreloadLanguagePack(Action onFin)
		{
			bool flag = this.m_conf == null;
			if (flag)
			{
				onFin();
			}
			else
			{
				LanguagePack.inst.loadExtendFile(this.m_localResUrlPrefix + this.m_conf["langbase"], delegate
				{
					this.m_loadedFile[this.m_conf["langbase"]._str] = this.m_conf["langbase"]._str;
					onFin();
				});
			}
		}

		public void loadLanguagePack(string file, Action onFin)
		{
			LanguagePack.inst.loadExtendFile(this.m_localResUrlPrefix + file, delegate
			{
				this.m_loadedFile[file] = file;
				onFin();
			});
		}

		public void loadLanguagePacks(List<string> fileAry, Action onFin)
		{
			batchLoader<Action<string, Action>> batchLoader = new batchLoader<Action<string, Action>>(fileAry, new Action<string, Action>(this.loadLanguagePack));
			batchLoader.loadNext(delegate(List<string> assetsLoaded, List<string> assetsFailed)
			{
				bool flag = assetsFailed.Count > 0;
				if (flag)
				{
					DebugTrace.add(Define.DebugTrace.DTT_ERR, "loadLanguagePacks load files[" + assetsFailed.ToString() + "] failed");
				}
				onFin();
			});
		}

		public void onPreInit()
		{
		}

		public void onPostInit()
		{
		}

		public void onFin()
		{
		}

		public void onResize(int width, int height)
		{
		}

		public void onPreRender(float tmSlice)
		{
		}

		public void onRender(float tmSlice)
		{
		}

		public void onPostRender(float tmSlice)
		{
		}

		public void onPreProcess(float tmSlice)
		{
		}

		public void onProcess(float tmSlice)
		{
		}

		public void onPostProcess(float tmSlice)
		{
		}
	}
}
