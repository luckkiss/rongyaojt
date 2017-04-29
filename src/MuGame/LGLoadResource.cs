using Cross;
using GameFramework;
using System;
using System.Collections;
using System.IO;

namespace MuGame
{
	public class LGLoadResource : lgGDBase, IObjectPlugin
	{
		public int m_nLoaded_MapID = -1;

		public static LGLoadResource _instance;

		public LGLoadResource(gameManager m) : base(m)
		{
			LGLoadResource._instance = this;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGLoadResource(m as gameManager);
		}

		public override void init()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add("正在连接服务器");
			bool flag = login.instance;
			if (flag)
			{
				login.instance.showUILoading();
			}
			else
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.BEGIN_LOADING, null, false);
			}
			this.g_mgr.g_netM.addEventListenerCL("DATA_CONN", 3014u, new Action<GameEvent>(this.onServerVer));
			this.g_mgr.g_netM.addEventListenerCL("DATA_JOIN_WORLD", 58u, new Action<GameEvent>(this.onMapChgLoad));
		}

		private void onServerVer(GameEvent e)
		{
			bool flag = beginloading.instance != null;
			if (flag)
			{
				beginloading.instance.setText("load main.xml");
			}
			this.loadMainConf();
		}

		private void loadMainConf()
		{
			byte[] d = File.ReadAllBytes(LoaderBehavior.DATA_PATH + "OutAssets/staticdata.dat");
			ByteArray byteArray = new ByteArray(d);
			byteArray.uncompress();
			XMLMgr.instance.init(byteArray);
			LoaderBehavior.ms_AllLoadedFin = new Action(this.onloadUIConf);
			string mainConfFile = (this.g_mgr.g_netM.getObject("DATA_CONN") as connInfo).mainConfFile;
			this.g_mgr.g_gameConfM.loadConfigs(mainConfFile, new Action(this.onloadMainConf));
		}

		private void onloadMainConf()
		{
			this.onloadLan();
		}

		protected void onloadLan()
		{
			bool flag = beginloading.instance != null;
			if (flag)
			{
				beginloading.instance.setText("Wait Xml Parsing");
			}
		}

		protected void onloadUIConf()
		{
			base.dispatchEvent(GameEvent.Create(3022u, this, null, false));
		}

		private void onMapChgLoad(GameEvent e)
		{
			this._onMapChgLoad();
		}

		public void _onMapChgLoad()
		{
			bool flag = a3_expbar.instance != null;
			if (flag)
			{
				a3_expbar.instance.CloseAgainst();
			}
			uint mapid = (this.g_mgr.g_netM.getObject("DATA_JOIN_WORLD") as joinWorldInfo).mapid;
			bool flag2 = mapid <= 0u;
			if (!flag2)
			{
				bool flag3 = (long)this.m_nLoaded_MapID == (long)((ulong)mapid);
				if (flag3)
				{
					bool flag4 = GRMap.CUR_POLTOVER_CB != null;
					if (flag4)
					{
						GRMap.CUR_POLTOVER_CB();
						GRMap.CUR_POLTOVER_CB = null;
					}
				}
				else
				{
					this.loadMapRes(mapid.ToString());
				}
			}
		}

		private void loadMapRes(string mapid)
		{
			this._loadMapResource();
		}

		private void _loadMapResource()
		{
			base.dispatchEvent(GameEvent.Create(3024u, this, null, false));
		}
	}
}
