using Cross;
using GameFramework;
using System;
using System.Runtime.CompilerServices;

namespace MuGame
{
	internal class LGOutGame : lgGDBase, IObjectPlugin
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly LGOutGame.<>c <>9 = new LGOutGame.<>c();

			public static Action <>9__20_0;

			internal void <tryOpenUI>b__20_0()
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SELECTCHA, null, false);
			}
		}

		private uint _selectCharCid = 0u;

		private bool _setConnFlag = false;

		private bool _loadMinResourceFlag = false;

		private bool _selectSidFalg = false;

		public static LGOutGame instance;

		private bool _open_sec_flag = false;

		public LGOutGame(muLGClient m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGOutGame(m as muLGClient);
		}

		public override void init()
		{
			base.addEventListener(3060u, new Action<GameEvent>(this.onStart));
			(this.g_mgr.g_netM as muNetCleint).addEventListenerCL("DATA_CONN", 3012u, new Action<GameEvent>(this.onConnected));
			this.g_mgr.g_netM.addEventListenerCL("DATA_CONN", 3011u, new Action<GameEvent>(this.onConnSet));
			this.g_mgr.g_netM.addEventListenerCL("DATA_CHARS", 3021u, new Action<GameEvent>(this.onLogin));
			this.g_mgr.g_gameM.addEventListenerCL("LG_LOAD_RESOURCE", 3022u, new Action<GameEvent>(this.onLoadMin));
			this.g_mgr.g_netM.addEventListener(3015u, new Action<GameEvent>(this.onConnectLost));
			this.g_mgr.g_uiM.addEventListener(4031u, new Action<GameEvent>(this.createChar));
			this.g_mgr.g_uiM.addEventListener(4032u, new Action<GameEvent>(this.deleteChar));
			this.g_mgr.g_uiM.addEventListener(4033u, new Action<GameEvent>(this.selectChar));
			this.g_mgr.g_uiM.addEventListener(4034u, new Action<GameEvent>(this.actEnterGame));
			this.g_mgr.g_uiM.addEventListener(4015u, new Action<GameEvent>(this.onSelectSid));
			LGOutGame.instance = this;
		}

		private void onSelectSid(GameEvent e)
		{
			this._selectSidFalg = true;
		}

		public void onConnectLost(GameEvent e)
		{
			this._setConnFlag = false;
			this._selectSidFalg = false;
			InterfaceMgr.getInstance().open(InterfaceMgr.DISCONECT, null, false);
		}

		public void onStart(GameEvent e)
		{
			bool flag = !this._setConnFlag;
			if (flag)
			{
				this._connect();
			}
		}

		public void reStart()
		{
			this._connect();
		}

		private void removeListener()
		{
			base.addEventListener(3060u, new Action<GameEvent>(this.onStart));
			this.g_mgr.g_netM.removeEventListenerCL("DATA_CONN", 3012u, new Action<GameEvent>(this.onConnected));
			this.g_mgr.g_netM.removeEventListenerCL("DATA_CONN", 3011u, new Action<GameEvent>(this.onConnSet));
			this.g_mgr.g_netM.removeEventListenerCL("DATA_CHARS", 3021u, new Action<GameEvent>(this.onLogin));
			this.g_mgr.g_gameM.removeEventListenerCL("LG_LOAD_RESOURCE", 3022u, new Action<GameEvent>(this.onLoadMin));
			this.g_mgr.g_uiM.removeEventListener(4031u, new Action<GameEvent>(this.createChar));
			this.g_mgr.g_uiM.removeEventListener(4032u, new Action<GameEvent>(this.deleteChar));
			this.g_mgr.g_uiM.removeEventListener(4033u, new Action<GameEvent>(this.selectChar));
			this.g_mgr.g_uiM.removeEventListener(4034u, new Action<GameEvent>(this.actEnterGame));
			this.g_mgr.g_uiM.removeEventListener(4015u, new Action<GameEvent>(this.onSelectSid));
		}

		private void createChar(GameEvent e)
		{
			(this.g_mgr.g_netM as muNetCleint).outGameMsgsInst.createCha(e.data["name"]._str, e.data["carr"]._uint, e.data["sex"]._uint);
		}

		private void deleteChar(GameEvent e)
		{
			(this.g_mgr.g_netM as muNetCleint).outGameMsgsInst.deleteCha(e.data["cid"]);
		}

		private void selectChar(GameEvent e)
		{
			Variant data = e.data;
			this._selectCharCid = data["cid"];
		}

		private void actEnterGame(GameEvent e)
		{
			bool flag = this._selectCharCid <= 0u;
			if (!flag)
			{
				(this.g_mgr.g_netM as muNetCleint).outGameMsgsInst.selectCha(this._selectCharCid);
				this.g_mgr.g_uiM.dispatchEvent(GameEvent.Create(4020u, this, null, false));
			}
		}

		private void onLoadMin(GameEvent e)
		{
			this._loadMinResourceFlag = true;
			this.tryOpenUI();
		}

		private void onLogin(GameEvent e)
		{
			this.tryOpenUI();
		}

		private void tryOpenUI()
		{
			bool flag = this._selectCharCid > 0u;
			if (flag)
			{
				(this.g_mgr.g_netM as muNetCleint).outGameMsgsInst.selectCha(this._selectCharCid);
				this.g_mgr.g_uiM.dispatchEvent(GameEvent.Create(4020u, this, null, false));
			}
			else
			{
				bool flag2 = !this._loadMinResourceFlag;
				if (!flag2)
				{
					bool flag3 = (this.g_mgr.g_netM as muNetCleint).charsInfoInst.getChas() == null;
					if (!flag3)
					{
						bool flag4 = login.instance != null;
						if (flag4)
						{
							login arg_BA_0 = login.instance;
							Action arg_BA_1;
							if ((arg_BA_1 = LGOutGame.<>c.<>9__20_0) == null)
							{
								arg_BA_1 = (LGOutGame.<>c.<>9__20_0 = new Action(LGOutGame.<>c.<>9.<tryOpenUI>b__20_0));
							}
							arg_BA_0.onBeginLoading(arg_BA_1);
						}
						else
						{
							InterfaceMgr.getInstance().open(InterfaceMgr.A3_SELECTCHA, null, false);
						}
					}
				}
			}
		}

		private void onConnSet(GameEvent e)
		{
			this._connect();
		}

		private void onConnected(GameEvent e)
		{
			this.g_mgr.g_netM.reqServerVersion();
		}

		private void _connect()
		{
			this._setConnFlag = true;
			connInfo connInfo = this.g_mgr.g_netM.getObject("DATA_CONN") as connInfo;
			this.g_mgr.g_netM.connect(connInfo.server_ip, connInfo.server_port, connInfo.uid, connInfo.token, connInfo.clnt, connInfo.keyt);
			debug.Log(string.Concat(new object[]
			{
				"链接服务器server_id=",
				connInfo.server_ip,
				" server_port=",
				connInfo.server_port,
				" uid=",
				connInfo.uid
			}));
		}
	}
}
