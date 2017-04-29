using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class joinWorldInfo : LGDataBase
	{
		protected Variant _blessArr = new Variant();

		public static joinWorldInfo instance;

		private Variant mapChangeD;

		private long lasttm = 0L;

		public Variant rmis_fin
		{
			get
			{
				return base.m_data["rmis_fin"];
			}
		}

		public Variant misacept
		{
			get
			{
				return base.m_data["misacept"];
			}
		}

		public uint carrlvl
		{
			get
			{
				return base.m_data["carrlvl"];
			}
		}

		public uint level
		{
			get
			{
				return base.m_data["level"];
			}
		}

		public uint mapid
		{
			get
			{
				return base.m_data["mpid"];
			}
		}

		public string name
		{
			get
			{
				return base.m_data["name"];
			}
		}

		public Variant mainPlayerInfo
		{
			get
			{
				return base.m_data;
			}
		}

		public uint cid
		{
			get
			{
				return base.m_data["cid"];
			}
		}

		public int carr
		{
			get
			{
				return base.m_data["carr"];
			}
		}

		private lgSelfPlayer lgMain
		{
			get
			{
				return base.g_mgr.g_gameM.getObject("LG_MAIN_PLAY") as lgSelfPlayer;
			}
		}

		public Variant GetBless()
		{
			return this._blessArr;
		}

		public joinWorldInfo(muNetCleint m) : base(m)
		{
			joinWorldInfo.instance = this;
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new joinWorldInfo(m as muNetCleint);
		}

		public override void init()
		{
			base.m_data["state"] = 20;
			base.m_data["iid"] = 0;
			base.m_data["mapid"] = 0;
			base.m_data["x"] = 0;
			base.m_data["y"] = 0;
			base.m_data["hit"] = 0;
			base.m_data["name"] = 0;
			base.m_data["sex"] = 0;
			base.m_data["carr"] = 0;
			base.m_data["speed"] = 0;
			base.m_data["speedTrans"] = 0;
			base.m_data["level"] = 0;
			base.m_data["gm"] = 0;
			base.m_data["vip"] = 0;
			base.m_data["yb"] = 0;
			base.m_data["gold"] = 0;
			base.m_data["exp"] = 0;
			base.m_data["hp"] = 0;
			base.m_data["mp"] = 0;
			base.m_data["dp"] = 0;
			base.m_data["max_hp"] = 0;
			base.m_data["max_mp"] = 0;
			base.m_data["max_dp"] = 0;
			base.m_data["eqp"] = null;
			base.m_data["misacept"] = null;
			base.m_data["rmis_fin"] = null;
			base.m_data["level"] = 0;
			base.m_data["roll_pt"] = 0;
			base.m_data["att_pt"] = 0;
			base.m_data["mount"] = 0;
			base.m_data["cid"] = 0;
			base.m_data["carr"] = 0;
			base.m_data["carrlvl"] = 0;
			base.m_data["resetlvl"] = 0;
			base.m_data["clanid"] = 0;
			base.m_data["str"] = 0;
			base.m_data["ori"] = 0;
			base.g_mgr.addEventListener(50u, new Action<GameEvent>(this.onJoinWorldRes));
			base.g_mgr.addEventListener(58u, new Action<GameEvent>(this.onMapChg));
		}

		private void onMapChg(GameEvent e)
		{
			bool flag = a3_expbar.instance != null;
			if (flag)
			{
				a3_expbar.instance.CloseAgainst();
			}
			this.mapChangeD = e.data;
			int num = base.m_data.ContainsKey("mpid") ? base.m_data["mpid"]._int : 0;
			int num2 = e.data["mpid"];
			bool flag2 = num != 0 && num2 == 1 && loading_cloud.instance == null;
			if (flag2)
			{
				LGUIMainUIImpl_NEED_REMOVE.TO_LEVEL = false;
				loading_cloud.showhandle = new Action(this.doChangeMap);
				InterfaceMgr.getInstance().open(InterfaceMgr.LOADING_CLOUD, null, false);
			}
			else
			{
				bool flag3 = num != 0;
				if (flag3)
				{
					LGUIMainUIImpl_NEED_REMOVE.TO_LEVEL = true;
				}
				this.doChangeMap();
			}
		}

		private void doChangeMap()
		{
			this.updataDetial(this.mapChangeD);
			base.dispatchEvent(GameEvent.Create(58u, this, null, false));
			this.mapChangeD = null;
		}

		private void onJoinWorldRes(GameEvent e)
		{
			this.updataDetial(e.data);
			foreach (string current in base.m_data.Keys)
			{
				bool flag = current == "bstates";
				if (flag)
				{
					this._blessArr = base.m_data["bstates"];
				}
			}
			base.g_mgr.g_processM.addProcess(processStruct.create(new Action<float>(this.updateProcess), "joinWorldInfo", false, false), false);
			base.dispatchEvent(GameEvent.Create(50u, this, null, false));
		}

		public void updataDetial(Variant info)
		{
			foreach (string current in info.Keys)
			{
				bool flag = current == "mpid";
				if (flag)
				{
					base.m_data["mpid"] = info[current];
				}
				else
				{
					bool flag2 = current == "x";
					if (flag2)
					{
						base.m_data[current] = info[current] / 53.333f;
					}
					else
					{
						bool flag3 = current == "y";
						if (flag3)
						{
							base.m_data[current] = info[current] / 53.333f;
						}
						else
						{
							base.m_data[current] = info[current];
						}
					}
				}
			}
		}

		public void onSelfAttChange(Variant data)
		{
		}

		public void setPos(double x, double y)
		{
			long curServerTimeStampMS = base.g_mgr.g_netM.CurServerTimeStampMS;
			double @double = base.m_data["x"]._double;
			double double2 = base.m_data["y"]._double;
			base.m_data["x"]._double = x;
			base.m_data["y"]._double = y;
			float num = (float)Math.Sqrt((@double - x) * (@double - x) + (double2 - y) * (double2 - y));
			base.dispatchEvent(GameEvent.Createimmedi(4049u, this, base.m_data, false));
			this.lasttm = curServerTimeStampMS;
		}

		private void updateBless()
		{
			bool flag = this._blessArr.Count <= 0;
			if (!flag)
			{
				long num = (base.g_mgr.g_netM as muNetCleint).CurServerTimeStampMS / 1000L;
				bool flag2 = false;
				for (int i = this._blessArr.Count - 1; i > 0; i--)
				{
					Variant variant = this._blessArr._arr[i];
					bool flag3 = variant["end_tm"] < num;
					if (flag3)
					{
						this._blessArr._arr.RemoveAt(i);
						flag2 = true;
					}
				}
				bool flag4 = flag2;
				if (flag4)
				{
				}
			}
		}

		protected Variant getBlessById(int id)
		{
			Variant result = new Variant();
			foreach (Variant current in this._blessArr.Values)
			{
				bool flag = current["id"] == id;
				if (flag)
				{
					result = current;
					break;
				}
			}
			return result;
		}

		protected void removeBless(int id)
		{
			for (int i = 0; i < this._blessArr.Count; i++)
			{
				bool flag = this._blessArr._arr[i][id] == id;
				if (flag)
				{
					this._blessArr._arr.RemoveAt(i);
					break;
				}
			}
		}

		public void BlessChange(Variant data)
		{
			bool flag = data.ContainsKey("mod");
			Variant variant;
			if (flag)
			{
				variant = data["mod"];
			}
			else
			{
				variant = data;
			}
			bool flag2 = variant.ContainsKey("rmvid");
			if (flag2)
			{
				this.removeBless(variant["rmvid"]);
			}
			else
			{
				Variant blessById = this.getBlessById(variant["id"]);
				bool flag3 = blessById != null;
				if (flag3)
				{
					foreach (string current in variant.Keys)
					{
						blessById[current] = variant[current];
					}
				}
				else
				{
					this._blessArr._arr.Add(variant);
				}
			}
		}

		public void updateProcess(float tmSlice)
		{
			bool flag = this._blessArr.Count > 0;
			if (flag)
			{
				this.updateBless();
			}
		}
	}
}
