using System;
using System.Collections.Generic;

namespace Cross
{
	public class ConfigManager : IAppPlugin
	{
		protected List<Variant> m_toLoadConfigs = new List<Variant>();

		protected Dictionary<string, Variant> m_configs = new Dictionary<string, Variant>();

		protected int m_configLoaded = 0;

		protected int m_configFailed = 0;

		protected Dictionary<string, Action<Variant>> m_confFormatFuncMap = new Dictionary<string, Action<Variant>>();

		protected int m_status;

		protected string m_curProcessConf = "";

		protected string m_errMsg = "";

		protected Define.Ccm_status m_ccm = Define.Ccm_status.NONE;

		protected Dictionary<string, ConfigLoadingData> m_loadingExtendConfigMap = new Dictionary<string, ConfigLoadingData>();

		public int status
		{
			get
			{
				return this.m_status;
			}
		}

		public string curProcessConf
		{
			get
			{
				return this.m_curProcessConf;
			}
		}

		public string errMsg
		{
			get
			{
				return this.m_errMsg;
			}
		}

		public int numLoaded
		{
			get
			{
				return this.m_configLoaded;
			}
		}

		public int numFailed
		{
			get
			{
				return this.m_configFailed;
			}
		}

		public int numWaitingToLoad
		{
			get
			{
				return this.m_toLoadConfigs.Count;
			}
		}

		public int numTotalConfigs
		{
			get
			{
				bool flag = this.m_toLoadConfigs != null;
				int result;
				if (flag)
				{
					result = this.m_configLoaded + this.m_configFailed + this.m_toLoadConfigs.Count;
				}
				else
				{
					result = this.m_configLoaded + this.m_configFailed;
				}
				return result;
			}
		}

		public string pluginName
		{
			get
			{
				return "conf";
			}
		}

		public void regFormatFunc(string confName, Action<Variant> func)
		{
			bool flag = !this.m_confFormatFuncMap.ContainsValue(func);
			if (flag)
			{
				this.m_confFormatFuncMap[confName] = func;
			}
		}

		public void loadExtendConfig(string file, Action<Variant> onFin)
		{
			bool flag = file.IndexOf('.') < 0;
			if (flag)
			{
				file += ".xml";
			}
			ConfigLoadingData loadingData = new ConfigLoadingData();
			bool flag2 = this.m_loadingExtendConfigMap.ContainsKey(file);
			if (flag2)
			{
				loadingData = this.m_loadingExtendConfigMap[file];
				loadingData.onFinCBs.Add(onFin);
			}
			else
			{
				loadingData.url = file;
				loadingData.onFinCBs.Add(onFin);
				this.m_loadingExtendConfigMap[file] = loadingData;
				os.sys.parseXMLFromFile(file, delegate(Variant extendConf1)
				{
					bool flag3 = extendConf1 != null;
					if (flag3)
					{
						this.m_loadingExtendConfigMap.Remove(loadingData.url);
						foreach (Action<Variant> current in loadingData.onFinCBs)
						{
							current(extendConf1);
						}
					}
					else
					{
						DebugTrace.add(Define.DebugTrace.DTT_ERR, "Load Extend config file[" + file + "] error! msg:" + file);
						this.m_loadingExtendConfigMap.Remove(loadingData.url);
						Variant obj = new Variant();
						foreach (Action<Variant> current2 in loadingData.onFinCBs)
						{
							current2(obj);
						}
					}
				});
			}
		}

		public void loadConfigs(string mainConfFile, Action onFin)
		{
			bool flag = mainConfFile.IndexOf('.') < 0;
			if (flag)
			{
				mainConfFile += ".xml";
			}
			os.sys.parseXMLFromFile(mainConfFile, delegate(Variant mainConf1)
			{
				bool flag2 = mainConf1 != null;
				if (flag2)
				{
					List<string> toLoadNames = new List<string>();
					List<string> list = new List<string>();
					foreach (string current in mainConf1.Keys)
					{
						Variant variant = mainConf1[current][0];
						bool flag3 = variant.ContainsKey("file");
						if (flag3)
						{
							toLoadNames.Add(current);
							list.Add(variant["file"]._str);
						}
						else
						{
							bool flag4 = this.m_confFormatFuncMap.ContainsKey(current);
							if (flag4)
							{
								this.m_ccm = Define.Ccm_status.FORMAT_CONF;
								this._setValue(this.m_ccm);
								Action<Variant> action = this.m_confFormatFuncMap[current];
								action(variant);
							}
							this.m_configs[current] = variant;
							this.m_configLoaded++;
						}
					}
					this.m_ccm = Define.Ccm_status.LOADING_CONF;
					this._setValue(this.m_ccm);
					for (int i = 0; i < toLoadNames.Count; i++)
					{
						string key = toLoadNames[i];
						string url = list[i];
						os.sys.parseXMLFromFile(url, delegate(Variant confObj1)
						{
							bool flag5 = confObj1 != null;
							if (flag5)
							{
								bool flag6 = this.m_confFormatFuncMap.ContainsKey(key);
								if (flag6)
								{
									this.m_ccm = Define.Ccm_status.FORMAT_CONF;
									this._setValue(this.m_ccm);
									Action<Variant> action2 = this.m_confFormatFuncMap[key];
									action2(confObj1);
								}
								this.m_configs[key] = confObj1;
								this.m_configLoaded++;
							}
							else
							{
								string text = "Config Manager load config file[" + url + "] error! msg:" + key;
								DebugTrace.add(Define.DebugTrace.DTT_ERR, text);
								this.m_errMsg = this.m_errMsg + text + "\n";
								this.m_configFailed++;
							}
							bool flag7 = this.m_configLoaded + this.m_configFailed == toLoadNames.Count;
							if (flag7)
							{
								this.m_ccm = Define.Ccm_status.LOADED;
								this._setValue(this.m_ccm);
								this.m_curProcessConf = null;
								onFin();
							}
						});
					}
				}
				else
				{
					this.m_errMsg = "Config Manager load main config file[] error! msg:" + mainConfFile;
					DebugTrace.add(Define.DebugTrace.DTT_ERR, this.m_errMsg);
					this.m_ccm = Define.Ccm_status.ERR;
					this._setValue(this.m_ccm);
					onFin();
				}
				this.m_ccm = Define.Ccm_status.LOADING_MAINFILE;
				this._setValue(this.m_ccm);
			});
		}

		public Variant getConfig(string confName)
		{
			bool flag = !this.m_configs.ContainsKey(confName);
			Variant result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.m_configs[confName];
			}
			return result;
		}

		public void onPreInit()
		{
		}

		public void onInit()
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

		private void _setValue(Define.Ccm_status ccm)
		{
			switch (ccm)
			{
			case Define.Ccm_status.NONE:
				this.m_status = 0;
				break;
			case Define.Ccm_status.LOADING_MAINFILE:
				this.m_status = 1;
				break;
			case Define.Ccm_status.LOADING_CONF:
				this.m_status = 2;
				break;
			case Define.Ccm_status.FORMAT_CONF:
				this.m_status = 3;
				break;
			case Define.Ccm_status.LOADED:
				this.m_status = 4;
				break;
			case Define.Ccm_status.ERR:
				this.m_status = 5;
				break;
			}
		}
	}
}
