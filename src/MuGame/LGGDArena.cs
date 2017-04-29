using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class LGGDArena : lgGDBase
	{
		private LGIUIItems _uiItemImpl = null;

		private Action<Variant> _arenachp_info_cb;

		private Variant _arenachp_npc_showinfo = new Variant();

		private Variant _arena_info = new Variant();

		private Variant _history_info = new Variant();

		private double _start_wait_tm = 0.0;

		private int _chphcnt = -1;

		private int _warStartTm = 0;

		private bool _bsmatch = false;

		private LGIUIItems ui_itemImpl
		{
			get
			{
				bool flag = this._uiItemImpl == null;
				if (flag)
				{
				}
				return this._uiItemImpl;
			}
		}

		public int start_wait_tm
		{
			get
			{
				return (int)this._start_wait_tm;
			}
		}

		public LGGDArena(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGGDArena(m as gameManager);
		}

		public override void init()
		{
			this.g_mgr.g_netM.addEventListener(235u, new Action<GameEvent>(this.switchFunc));
		}

		private void switchFunc(GameEvent e)
		{
			Variant data = e.data;
			bool flag = data.ContainsKey("case");
			if (flag)
			{
				string str = data["case"]._str;
				if (!(str == "on_arena_res"))
				{
					GameTools.PrintNotice("switchFunc defanult");
				}
				else
				{
					this.on_arena_res(data["data"]);
				}
			}
			else
			{
				GameTools.PrintNotice("switchFunc no case");
			}
		}

		public void get_arenachp_npc_data(Action<Variant> onfin)
		{
			bool flag = this._arenachp_npc_showinfo != null;
			if (flag)
			{
				onfin(this._arenachp_npc_showinfo);
			}
			else
			{
				this._arenachp_info_cb = onfin;
				this.get_nearest_chp_avatar();
			}
		}

		public void get_arena_info()
		{
			Variant variant = new Variant();
			variant["arenaid"] = 1;
			variant["tp"] = 1;
		}

		public void get_arena_award()
		{
			Variant variant = new Variant();
			variant["arenaid"] = 1;
			variant["tp"] = 2;
		}

		public void get_nearest_chp_avatar()
		{
			Variant variant = new Variant();
			variant["arenaid"] = 1;
			variant["tp"] = 3;
		}

		public void get_arena_chp_cnt()
		{
			Variant variant = new Variant();
			variant["arenaid"] = 1;
			variant["tp"] = 4;
			bool flag = this._chphcnt < 0;
			if (flag)
			{
			}
		}

		public void arena_chp_complete()
		{
			this._history_info = new Variant();
			this._chphcnt++;
			bool flag = this._chphcnt > 10;
			if (flag)
			{
				this._chphcnt = 10;
			}
		}

		public void get_arena_chp_info(int arenaid, int idx)
		{
			bool flag = this._history_info["arenaid"];
			if (flag)
			{
				Variant variant = this._history_info[arenaid];
				bool flag2 = variant["idx"];
				if (flag2)
				{
					return;
				}
			}
			Variant variant2 = new Variant();
			variant2["arenaid"] = 1;
			variant2["tp"] = 5;
			variant2["chphidx"] = idx;
		}

		public void Update_arena_info(int arenaid, Variant data)
		{
		}

		public void Update_arenaex_info(int arenaexid, Variant data)
		{
		}

		public void acancel()
		{
			using (List<Variant>.Enumerator enumerator = this._arena_info._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string key = enumerator.Current;
					bool flag = !this._arena_info[key]["tm"];
					if (!flag)
					{
						this._arena_info[key].RemoveKey("tm");
						break;
					}
				}
			}
		}

		public void on_arena_res(Variant data)
		{
			int num = data["arenaid"];
			int num2 = data["tp"];
			switch (data["tp"]._int)
			{
			case 1:
			{
				this.Update_arena_info(num, data);
				bool flag = data["chpawd"] && data["chpawd"];
				if (flag)
				{
				}
				break;
			}
			case 2:
			{
				bool flag2 = data["chpawd"];
				if (flag2)
				{
				}
				break;
			}
			case 3:
			{
				Variant variant = this._arena_info[num];
				bool flag3 = variant;
				if (flag3)
				{
					int num3 = data["pt"] - variant["pt"];
					string arg_FF_0 = (num3 > 0) ? "arenagetpt" : "arenalosept";
				}
				this.Update_arena_info(num, data);
				bool flag4 = data["chpawd"] && data["chpawd"];
				if (flag4)
				{
				}
				break;
			}
			case 4:
			{
				bool flag5 = data["showinfos"].Count > 0;
				if (flag5)
				{
					bool flag6 = this._arenachp_info_cb != null;
					if (flag6)
					{
						this._arenachp_npc_showinfo = data["showinfos"][0];
						this._arenachp_info_cb(this._arenachp_npc_showinfo);
					}
				}
				break;
			}
			case 5:
				this._chphcnt = data["chphcnt"];
				break;
			case 6:
			{
				bool flag7 = !this._history_info[data["arenaid"]];
				if (flag7)
				{
					this._history_info[data["arenaid"]] = new Variant();
				}
				this._history_info[data["arenaid"]][data["chphidx"]] = data;
				break;
			}
			}
		}

		public void SetWarStartTm(double tm)
		{
			this._warStartTm = (int)tm;
		}

		public double GetWarStartTm()
		{
			return (double)this._warStartTm;
		}

		public void ClearWarTime()
		{
		}

		public void SetBattleMatch(bool flag)
		{
			this._bsmatch = flag;
		}

		public bool IsInMatch()
		{
			return this._bsmatch;
		}

		public Variant get_arena_info_by_id(int arenaid)
		{
			return this._arena_info[arenaid];
		}

		public Variant get_arenaex_info_by_id(int arenaexid)
		{
			return this._arena_info[arenaexid];
		}
	}
}
