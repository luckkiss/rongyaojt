using Cross;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	public class LGPlatInfo
	{
		private string _query = "http://10.1.8.45/ifacec/sdk_srvlists.php";

		private Variant _srv_lists = null;

		private Variant _curSvrPlyInfo = null;

		private Action<Variant> _notifyFun = null;

		private int _curSid = 0;

		private static LGPlatInfo _inst;

		private string _platId = "";

		private int serverListErrorCount = 0;

		public static bool relogined = false;

		private bool isfirst = true;

		private string SID_UID = "";

		private int m_lastLogAPTime = 0;

		public static LGPlatInfo inst
		{
			get
			{
				bool flag = LGPlatInfo._inst == null;
				if (flag)
				{
					LGPlatInfo._inst = new LGPlatInfo();
				}
				return LGPlatInfo._inst;
			}
		}

		public Variant svlist
		{
			get
			{
				return this._srv_lists;
			}
		}

		public int sid
		{
			get
			{
				return this._curSid;
			}
			set
			{
				this._curSid = value;
			}
		}

		protected connInfo conninfo
		{
			get
			{
				return Globle.game_CrossMono.m.g_netM.getObject("DATA_CONN") as connInfo;
			}
		}

		public void setServerListReqUrl(string url)
		{
			this._query = url;
		}

		public void regNotifiFun(Action<Variant> cbfun)
		{
			this._notifyFun = cbfun;
		}

		public int GetRecommend()
		{
			return 5;
		}

		private void onSelectPlatuid(Variant v)
		{
			bool flag = !v.ContainsKey("data") || !v["data"].ContainsKey("pid") || !v["data"].ContainsKey("avatar") || !v["data"].ContainsKey("uid");
			if (flag)
			{
				DebugTrace.print("Erorr: Variant no`t Find <data>");
				DebugTrace.dumpObj(v);
			}
			else
			{
				string param = string.Concat(new string[]
				{
					"platform=",
					v["data"]["pid"]._str,
					"&sign=",
					v["data"]["avatar"]._str,
					"&platuid=",
					v["data"]["uid"]._str
				});
				HttpAppMgr.POSTSvr(this._query, param, new Action<Variant>(this._getSeverListBack), true, "POST");
			}
		}

		private void _getSeverListBack(Variant data)
		{
			debug.Log("收到服务器列表信息： " + data.dump());
			bool flag = data["r"]._int == 0;
			if (flag)
			{
				bool flag2 = data.ContainsKey("errmsg");
				if (flag2)
				{
					debug.Log("SeverListError::" + StringUtils.unicodeToStr(data["errmsg"]._str));
				}
				this.retryLoadServerList();
			}
			else
			{
				bool flag3 = data["r"]._int == 1;
				if (flag3)
				{
					bool flag4 = data.ContainsKey("data");
					if (flag4)
					{
						this._srv_lists = data["data"]["srv_lists"];
						Variant variant = new Variant();
						variant["svrList"] = this._srv_lists;
						this.notify(variant);
						Globle.initServer(this._srv_lists._arr);
						login.instance.refresh();
					}
				}
			}
		}

		private void retryLoadServerList()
		{
			this.serverListErrorCount++;
			bool flag = this.serverListErrorCount > 3;
			if (flag)
			{
				bool flag2 = Globle.DebugMode == 0;
				if (flag2)
				{
					InterfaceMgr.getInstance().open(InterfaceMgr.DISCONECT, null, false);
				}
			}
			else
			{
				this.loadServerList();
			}
		}

		private int GetSrvSid(int inedx)
		{
			bool flag = inedx < 0;
			int result;
			if (flag)
			{
				result = this._srv_lists.Count - 1;
			}
			else
			{
				bool flag2 = this._srv_lists[inedx]["close"]._int == 0;
				if (flag2)
				{
					result = this._srv_lists[inedx]["sid"]._int;
				}
				else
				{
					result = this.GetSrvSid(inedx - 1);
				}
			}
			return result;
		}

		public void relogin()
		{
			LGPlatInfo.relogined = true;
			bool flag = Globle.DebugMode == 2;
			if (flag)
			{
				NetClient.instance.reConnect(null);
			}
			else
			{
				HttpAppMgr.POSTSvr(Globle.curServerD.login_url, "", new Action<Variant>(this._getPlyInfoBack), true, "GET");
			}
		}

		public void GetPlyInfo(int sid)
		{
			os.sys.loscalStorage.writeInt("sever_id", sid);
			for (int i = 0; i < this._srv_lists.Count; i++)
			{
				bool flag = this._srv_lists[i]["sid"]._int == sid;
				if (flag)
				{
					HttpAppMgr.POSTSvr(this._srv_lists[i]["login_url"]._str, "", new Action<Variant>(this._getPlyInfoBack), true, "GET");
					break;
				}
			}
		}

		private void _getPlyInfoBack(Variant data)
		{
			bool flag = !data.ContainsKey("r");
			if (flag)
			{
				bool flag2 = disconect.instance != null;
				if (flag2)
				{
					disconect.instance.setErrorType(disconect.ERROR_TYPE_NETWORK);
				}
			}
			else
			{
				bool flag3 = data["r"]._int == 1;
				if (flag3)
				{
					this._curSvrPlyInfo = data["data"];
					Variant variant = new Variant();
					variant["paramters"] = this._curSvrPlyInfo;
					this.notify(variant);
					bool flag4 = this.isfirst;
					if (flag4)
					{
						this.initGame(this._curSvrPlyInfo);
						GRClient.instance.getGraphCamera().visible = false;
						this.isfirst = false;
					}
					else
					{
						Variant variant2 = data["data"];
						Variant variant3 = new Variant();
						Variant variant4 = new Variant();
						variant4["server_ip"] = variant2["server_ip"];
						variant4["uid"] = variant2["uid"];
						variant4["server_port"] = variant2["server_port"];
						variant4["token"] = variant2["skey"];
						variant4["clnt"] = 0;
						variant3["outgamevar"] = variant4;
						variant3["server_id"] = variant2["sid"];
						variant3["server_config_url"] = variant2["config_url"];
						variant3["mainConfig"] = "main";
						this.conninfo.setInfo(variant3);
					}
				}
				else
				{
					debug.Log(" GetPlyInfo failed :" + StringUtils.unicodeToStr(data["r"]._str));
					bool flag5 = login.instance != null;
					if (flag5)
					{
						login.instance.msg.show(true, "服务器维护中..", null);
					}
					bool flag6 = disconect.instance != null;
					if (flag6)
					{
						disconect.instance.setErrorType(disconect.ERROR_TYPE_SERVER);
					}
				}
			}
		}

		private void initGame(Variant data)
		{
			HttpAppMgr.init();
			Globle.game_CrossMono.init(data["config_url"]._str, data["server_ip"]._str, data["sid"]._uint, data["server_port"]._uint, data["uid"]._uint, 0u, data["skey"]._str, "main");
		}

		private void notify(Variant info)
		{
			debug.Log("================================>");
			debug.Log(info.dump());
			bool flag = this._notifyFun == null;
			if (!flag)
			{
				this._notifyFun(info);
			}
		}

		public void sendLogin(string login_url)
		{
			HttpAppMgr.POSTSvr(login_url, "", new Action<Variant>(this._getPlyInfoBack), true, "GET");
		}

		public void firstAnalysisPoint(uint server_id, uint uid)
		{
			this.SID_UID = "sid=" + server_id.ToString() + "&uid=" + uid.ToString();
			string text = "platid=" + Globle.YR_srvlists__platform;
			text = text + "&sid=" + server_id.ToString();
			text = text + "&uid=" + uid.ToString();
			text = text + "&platuid=" + Globle.YR_srvlists__platuid;
			text = text + "&ver=" + Globle.QSMY_game_ver;
			text = text + "&deviceid=" + SystemInfo.deviceUniqueIdentifier;
			text = text + "&model=" + SystemInfo.deviceModel;
			text = text + "&opsys=" + SystemInfo.operatingSystem;
			text = string.Concat(new string[]
			{
				text,
				"&resolution=",
				Screen.width.ToString(),
				"x",
				Screen.height.ToString()
			});
			text = text + "&video=" + SystemInfo.graphicsDeviceName;
			text = text + "&v_changshang=" + SystemInfo.graphicsDeviceVendor;
			text = text + "&v_drivers=" + SystemInfo.graphicsDeviceVersion;
			text = text + "&v_memory=" + SystemInfo.graphicsMemorySize;
			text = text + "&cpu=" + SystemInfo.processorType;
			text = text + "&c_core=" + SystemInfo.processorCount;
			bool flag = Globle.curServerD != null && Globle.curServerD.login_url != null;
			if (flag)
			{
			}
		}

		public void loadServerList()
		{
			bool flag = Globle.QSMY_Platform_Index == ENUM_QSMY_PLATFORM.QSPF_None;
			if (!flag)
			{
				HttpAppMgr.POSTSvr(Globle.YR_srvlists__slurl, string.Concat(new string[]
				{
					"platform=",
					Globle.YR_srvlists__platform,
					"&sign=",
					Globle.YR_srvlists__sign,
					"&platuid=",
					Globle.YR_srvlists__platuid
				}), new Action<Variant>(this._getSeverListBack), true, "POST");
				debug.Log("sl_url = " + Globle.YR_srvlists__slurl);
			}
		}
	}
}
