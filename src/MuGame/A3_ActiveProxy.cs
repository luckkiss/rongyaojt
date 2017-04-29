using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class A3_ActiveProxy : BaseProxy<A3_ActiveProxy>
	{
		public static uint EVENT_MLZDOPCUCCESS = 2u;

		public static uint EVENT_PVPSITE_INFO = 4u;

		public static uint EVENT_MWLR_NEW = 1562u;

		public static uint EVENT_ONBLESS = 10000u;

		public A3_ActiveProxy()
		{
			this.addProxyListener(165u, new Action<Variant>(this.Active_MWSLOP));
			this.addProxyListener(166u, new Action<Variant>(this.Active_BOSSOP));
			this.addProxyListener(232u, new Action<Variant>(this.OnBlessing));
			this.addProxyListener(37u, new Action<Variant>(this.OnPVP_site));
		}

		public void SendLoadActivies()
		{
			this.SendGetBossInfo();
		}

		public void SendGetHuntInfo()
		{
			Variant variant = new Variant();
			variant["op"] = 1;
			this.sendRPC(165u, variant);
		}

		public void SendStartHunt()
		{
			Variant variant = new Variant();
			variant["op"] = 2;
			this.sendRPC(165u, variant);
		}

		public void SendGiveUpHunt()
		{
			Variant variant = new Variant();
			variant["op"] = 3;
			this.sendRPC(165u, variant);
		}

		public void ActiveMWSLSearch(Variant data)
		{
			ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info = data["map_info"];
			ModelBase<A3_ActiveModel>.getInstance().mwlr_doubletime = data["double_limit"];
			ModelBase<A3_ActiveModel>.getInstance().mwlr_totaltime = data["lose_tm"];
			ModelBase<A3_ActiveModel>.getInstance().mwlr_map_id.Clear();
			ModelBase<A3_ActiveModel>.getInstance().mwlr_mons_pos.Clear();
			for (int i = 0; i < data["map_info"].Count; i++)
			{
				bool flag = data["map_info"][i].ContainsKey("map_id");
				if (flag)
				{
					int @int = data["map_info"][i]["map_id"]._int;
					Vector3 value = new Vector3(data["map_info"][i]["x"] / 53.333f, 0f, data["map_info"][i]["y"] / 53.333f);
					ModelBase<A3_ActiveModel>.getInstance().mwlr_map_id.Add(@int);
					ModelBase<A3_ActiveModel>.getInstance().mwlr_mons_pos.Add(@int, value);
				}
			}
		}

		public void Active_MWSLOP(Variant data)
		{
			Debug.Log("收到魔物猎人消息" + data.dump());
			int num = data["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
			}
			ModelBase<A3_ActiveModel>.getInstance().mwlr_charges = (data["count"] ?? ModelBase<A3_ActiveModel>.getInstance().mwlr_charges);
			switch (num)
			{
			case 1:
				a3_active_mwlr_kill.initLoginData = data["map_info"];
				this.ActiveMWSLSearch(data);
				break;
			case 2:
				this.ActiveMWSLSearch(data);
				a3_active_mwlr_kill.Instance.Reset();
				ModelBase<A3_ActiveModel>.getInstance().mwlr_giveup = false;
				base.dispatchEvent(GameEvent.Create(A3_ActiveProxy.EVENT_MWLR_NEW, this, data, false));
				break;
			case 3:
			{
				ModelBase<A3_ActiveModel>.getInstance().mwlr_map_id.Clear();
				ModelBase<A3_ActiveModel>.getInstance().mwlr_mons_pos.Clear();
				Variant expr_106 = ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info;
				if (expr_106 != null)
				{
					expr_106._arr.Clear();
				}
				ModelBase<A3_ActiveModel>.getInstance().mwlr_giveup = true;
				a3_active_mwlr_kill.Instance.Clear();
				a3_expbar.instance.DownTip();
				break;
			}
			case 4:
			{
				for (int i = 0; i < ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info.Count; i++)
				{
					bool flag2 = ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info[i]["map_id"] == data["map_id"]._int;
					if (flag2)
					{
						ModelBase<A3_ActiveModel>.getInstance().mwlr_map_info[i]["kill"]._bool = true;
						bool flag3 = !ModelBase<A3_ActiveModel>.getInstance().listKilled.Contains(i);
						if (flag3)
						{
							ModelBase<A3_ActiveModel>.getInstance().listKilled.Add(i);
						}
						break;
					}
				}
				A3_ActiveModel expr_1ED = ModelBase<A3_ActiveModel>.getInstance();
				int mwlr_mon_killed = expr_1ED.mwlr_mon_killed;
				expr_1ED.mwlr_mon_killed = mwlr_mon_killed + 1;
				a3_active_mwlr_kill.Instance.Refresh();
				bool flag4 = SelfRole.fsm.Autofighting && ModelBase<PlayerModel>.getInstance().task_monsterIdOnAttack.Remove(-1);
				if (flag4)
				{
					SelfRole.fsm.Stop();
				}
				ModelBase<A3_ActiveModel>.getInstance().mwlr_on = false;
				break;
			}
			case 5:
				a3_expbar.instance.DownTip();
				a3_active_mwlr_kill.Instance.Clear();
				break;
			}
			base.dispatchEvent(GameEvent.Create(A3_ActiveProxy.EVENT_MLZDOPCUCCESS, this, data, false));
		}

		public void SendGetBossInfo()
		{
			Variant v = new Variant();
			this.sendRPC(166u, v);
		}

		public void Active_BOSSOP(Variant data)
		{
			bool flag = data.ContainsKey("boss_status");
			if (flag)
			{
				for (int i = 0; i < ModelBase<A3_EliteMonsterModel>.getInstance().bossid.Length; i++)
				{
					bool flag2 = data["boss_status"][i] != null;
					if (flag2)
					{
						ModelBase<A3_EliteMonsterModel>.getInstance().bossid[i] = int.Parse(data["boss_status"][i]["index"].dump());
						ModelBase<A3_EliteMonsterModel>.getInstance().boss_status[i] = int.Parse(data["boss_status"][i]["status"].dump());
						Debug.LogWarning(string.Concat(new object[]
						{
							"bossid",
							ModelBase<A3_EliteMonsterModel>.getInstance().bossid[i],
							"+boss_status",
							ModelBase<A3_EliteMonsterModel>.getInstance().boss_status[i]
						}));
					}
				}
				base.dispatchEvent(GameEvent.Create(EliteMonsterProxy.EVENT_BOSSOPSUCCESS, this, data, false));
			}
			debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~" + data.dump());
			uint up_lvl = ModelBase<PlayerModel>.getInstance().up_lvl;
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			bool flag3 = data.ContainsKey("index");
			if (flag3)
			{
				string @string = XMLMgr.instance.GetSXML("worldboss.boss", "id==" + data["index"]).getString("level_limit");
				string[] array = @string.Split(new char[]
				{
					','
				});
				uint num = uint.Parse(array[0]);
				bool flag4 = data["status"] == 1;
				if (flag4)
				{
					bool flag5 = up_lvl >= num;
					bool val;
					if (flag5)
					{
						dictionary[data["index"]] = data["status"];
						val = true;
					}
					else
					{
						bool flag6 = dictionary.Count > 0;
						if (flag6)
						{
							val = true;
						}
						else
						{
							val = false;
							BaseProxy<EliteMonsterProxy>.getInstance().SendProxy();
						}
					}
					IconAddLightMgr.getInstance().showOrHideFires("shijieboss_Light_enterElite", val);
				}
				else
				{
					bool flag7 = data["status"] == 2;
					if (flag7)
					{
						bool flag8 = dictionary.ContainsKey(data["index"]);
						if (flag8)
						{
							dictionary.Remove(data["index"]);
						}
						bool flag9 = dictionary.Count > 0;
						bool val;
						if (flag9)
						{
							val = true;
						}
						else
						{
							val = false;
							BaseProxy<EliteMonsterProxy>.getInstance().SendProxy();
						}
						IconAddLightMgr.getInstance().showOrHideFires("shijieboss_Light_enterElite", val);
					}
				}
			}
		}

		public void SendGetBlessing()
		{
			Variant variant = new Variant();
			variant["res"] = 1;
			variant["state_id"] = A3_ActiveProxy.EVENT_ONBLESS;
			this.sendRPC(232u, variant);
		}

		public void OnBlessing(Variant data)
		{
			uint num = 0u;
			bool flag = data.ContainsKey("state_id");
			if (flag)
			{
				num = data["state_id"];
			}
			bool flag2 = num == A3_ActiveProxy.EVENT_ONBLESS;
			if (flag2)
			{
				base.dispatchEvent(GameEvent.Create(A3_ActiveProxy.EVENT_ONBLESS, this, null, false));
			}
		}

		public void SendPVP(int val)
		{
			Variant variant = new Variant();
			variant["subcmd"] = val;
			this.sendRPC(37u, variant);
		}

		public void OnPVP_site(Variant data)
		{
			int num = data["res"];
			debug.Log("PVP:" + data.dump());
			bool flag = num < 0;
			if (flag)
			{
				flytxt.instance.fly(err_string.get_Err_String(num), 0, default(Color), null);
			}
			else
			{
				switch (num)
				{
				case 1:
					ModelBase<A3_ActiveModel>.getInstance().grade = data["grade"];
					ModelBase<A3_ActiveModel>.getInstance().score = data["score"];
					ModelBase<A3_ActiveModel>.getInstance().lastgrage = data["last_grade"];
					ModelBase<A3_ActiveModel>.getInstance().pvpCount = data["cnt"];
					ModelBase<A3_ActiveModel>.getInstance().buyCount = data["buy_cnt"];
					base.dispatchEvent(GameEvent.Create(A3_ActiveProxy.EVENT_PVPSITE_INFO, this, data, false));
					break;
				case 2:
				{
					bool flag2 = a3_active_pvp.instance != null;
					if (flag2)
					{
						a3_active_pvp.instance.openFind();
					}
					break;
				}
				case 3:
				{
					bool flag3 = a3_active_pvp.instance != null;
					if (flag3)
					{
						a3_active_pvp.instance.CloseFind();
					}
					break;
				}
				case 4:
				{
					ModelBase<A3_ActiveModel>.getInstance().buyCount = data["buy_cnt"];
					bool flag4 = a3_active_pvp.instance != null;
					if (flag4)
					{
						a3_active_pvp.instance.refCount_buy(data["buy_cnt"]);
					}
					break;
				}
				case 6:
				{
					bool open = false;
					bool flag5 = data["open"] == 0;
					if (flag5)
					{
						open = false;
					}
					else
					{
						bool flag6 = data["open"] == 1;
						if (flag6)
						{
							open = true;
						}
					}
					bool flag7 = a3_active_pvp.instance != null;
					if (flag7)
					{
						a3_active_pvp.instance.setbtn(open);
					}
					break;
				}
				case 8:
				{
					ModelBase<A3_ActiveModel>.getInstance().pvpCount = data["cnt"];
					bool flag8 = a3_active_pvp.instance != null;
					if (flag8)
					{
						a3_active_pvp.instance.refCount(data["cnt"]);
					}
					break;
				}
				case 9:
				{
					ModelBase<A3_ActiveModel>.getInstance().grade = data["grade"];
					ModelBase<A3_ActiveModel>.getInstance().score = data["score"];
					bool flag9 = a3_active_pvp.instance != null;
					if (flag9)
					{
						a3_active_pvp.instance.refro_score();
					}
					break;
				}
				}
			}
		}
	}
}
