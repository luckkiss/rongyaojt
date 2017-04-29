using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class MapProxy : BaseProxy<MapProxy>
	{
		public static bool isyinsh = false;

		public List<int> drop = new List<int>();

		public string openWin = null;

		public string Win_uiData = null;

		public bool nul;

		public bool changingMap = false;

		public MapProxy()
		{
			this.addProxyListener(7u, new Action<Variant>(this.on_trig_eff));
			this.addProxyListener(20u, new Action<Variant>(this.on_monster_spawn));
			this.addProxyListener(21u, new Action<Variant>(this.on_player_respawn));
			this.addProxyListener(30u, new Action<Variant>(this.on_sprite_invisible));
			this.addProxyListener(53u, new Action<Variant>(this.on_sprite_hp_info_res));
			this.addProxyListener(54u, new Action<Variant>(this.on_player_enter_zone));
			this.addProxyListener(55u, new Action<Variant>(this.on_monster_enter_zone));
			this.addProxyListener(56u, new Action<Variant>(this.on_sprite_leave_zone));
			this.addProxyListener(57u, new Action<Variant>(this.on_begin_change_map_res));
			this.addProxyListener(58u, new Action<Variant>(this.onMapChange));
			this.addProxyListener(70u, new Action<Variant>(this.on_other_eqp_change));
			this.addProxyListener(77u, new Action<Variant>(this.on_pickitem));
			this.addProxyListener(76u, new Action<Variant>(this.on_map_dpitem_res));
			this.addProxyListener(108u, new Action<Variant>(this.onbeginCollect));
			this.addProxyListener(199u, new Action<Variant>(this.Collect_Box));
		}

		private void Collect_Box(Variant data)
		{
			bool flag = data == null;
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.CD);
			}
			else
			{
				int num = data["res"];
				int num2 = num;
				if (num2 != 1)
				{
					if (num2 != 2)
					{
						InterfaceMgr.getInstance().close(InterfaceMgr.CD);
					}
					else
					{
						InterfaceMgr.getInstance().close(InterfaceMgr.CD);
						flytxt.instance.fly("打开失败！", 0, default(Color), null);
					}
				}
				else
				{
					this.nul = false;
					List<Variant> arr = data["itm_ary"]._arr;
					flytxt.instance.fly("开启宝箱成功！", 0, default(Color), null);
					for (int i = 0; i < arr.Count; i++)
					{
						Variant variant = arr[i];
						bool flag2 = variant.ContainsKey("id") && variant.ContainsKey("cnt");
						if (flag2)
						{
							bool flag3 = variant["cnt"] != 0;
							if (flag3)
							{
								this.nul = true;
								a3_ItemData a3_ItemData = default(a3_ItemData);
								a3_ItemData = ModelBase<a3_BagModel>.getInstance().getItemDataById(variant["id"]);
								string str = "";
								switch (a3_ItemData.quality)
								{
								case 1:
									str = a3_ItemData.item_name;
									break;
								case 2:
									str = a3_ItemData.item_name;
									break;
								case 3:
									str = a3_ItemData.item_name;
									break;
								case 4:
									str = a3_ItemData.item_name;
									break;
								case 5:
									str = a3_ItemData.item_name;
									break;
								}
								string txt = "获得道具：" + str + "x" + variant["cnt"];
								flytxt.instance.fly(txt, 0, default(Color), null);
							}
						}
					}
					bool flag4 = !this.nul;
					if (flag4)
					{
						flytxt.instance.fly("此宝箱为空宝箱", 0, default(Color), null);
					}
				}
			}
		}

		public void sendBeginChangeMap(int linkid, bool transmit = false, bool ontask = false)
		{
			bool canfly = ModelBase<FindBestoModel>.getInstance().Canfly;
			if (canfly)
			{
				Variant variant = new Variant();
				variant["gto"] = linkid;
				variant["transmit"] = transmit;
				variant["ontask"] = ontask;
				debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>sendBeginChangeMap>" + variant.dump());
				this.changingMap = true;
				this.sendRPC(57u, variant);
			}
			else
			{
				flytxt.instance.fly(ModelBase<FindBestoModel>.getInstance().nofly_txt, 0, default(Color), null);
			}
		}

		public void on_begin_change_map_res(Variant v)
		{
			debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>on_begin_change_map_res>" + v.dump());
			bool flag = v["res"] == 1;
			if (flag)
			{
				Variant var = new Variant();
				var["gto"] = v["gto"];
				var["gate"] = v["gate"];
				bool flag2 = loading_cloud.instance == null;
				if (flag2)
				{
					loading_cloud.showhandle = delegate
					{
						this.sendRPC(58u, var);
					};
					InterfaceMgr.getInstance().open(InterfaceMgr.LOADING_CLOUD, null, false);
				}
				else
				{
					this.sendRPC(58u, var);
				}
			}
			else
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.LOADING_CLOUD);
				this.changingMap = false;
			}
		}

		public void sendGoto_map(Variant data)
		{
			this.sendRPC(12u, data);
		}

		public void sendShowMapObj()
		{
			debug.Log("!!sendShowMapObj!! " + debug.count);
			this.sendRPC(55u, new Variant());
		}

		public void sendNpcTrans(uint npcid, uint trid)
		{
			Variant variant = new Variant();
			variant["npcid"] = npcid;
			variant["trid"] = trid;
			this.sendRPC(19u, variant);
		}

		public void send_get_wrdboss_respawntm(bool islvl = false)
		{
			Variant variant = new Variant();
			variant["islvl"] = islvl;
			this.sendRPC(20u, variant);
		}

		public void sendRespawn(bool useGolden = false)
		{
			Variant variant = new Variant();
			variant["immediate"] = useGolden;
			this.sendRPC(21u, variant);
		}

		public void sendGet_sprite_hp_info(uint iid)
		{
			Variant variant = new Variant();
			variant["iid"] = iid;
			this.sendRPC(53u, variant);
		}

		public void sendChange_map(uint mapid)
		{
			Variant variant = new Variant();
			variant["gto"] = mapid;
			this.sendRPC(57u, variant);
		}

		public void sendEnd_change_map()
		{
			this.sendRPC(58u, new Variant());
		}

		private void on_trig_eff(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(7u, this, msgData, false));
		}

		private void on_monster_spawn(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(20u, this, msgData, false));
		}

		private void on_player_respawn(Variant msgData)
		{
			bool flag = msgData.ContainsKey("back_town_tm");
			if (flag)
			{
				uint num = msgData["back_town_tm"];
				long num2 = (long)((ulong)num - (ulong)((long)NetClient.instance.CurServerTimeStamp));
				bool flag2 = num2 < 0L;
				if (flag2)
				{
					num2 = 0L;
				}
				a3_relive.backtown_end_tm = (int)num2;
			}
			bool flag3 = !msgData.ContainsKey("iid");
			if (!flag3)
			{
				int max_hp = msgData["battleAttrs"]["max_hp"];
				uint @uint = msgData["iid"]._uint;
				bool flag4 = @uint == SelfRole._inst.m_unIID;
				if (flag4)
				{
					SelfRole._inst.can_buff_move = true;
					SelfRole._inst.can_buff_skill = true;
					SelfRole._inst.can_buff_ani = true;
					SelfRole._inst.onRelive(max_hp);
					InterfaceMgr.getInstance().close(InterfaceMgr.A3_RELIVE);
				}
				else
				{
					ProfessionRole otherPlayer = OtherPlayerMgr._inst.GetOtherPlayer(@uint);
					bool flag5 = otherPlayer != null;
					if (flag5)
					{
						otherPlayer.onRelive(max_hp);
					}
				}
			}
		}

		private void on_sprite_invisible(Variant msgData)
		{
			debug.Log("on_sprite_invisible::" + msgData.dump());
			uint @uint = msgData["iid"]._uint;
			bool flag = msgData["invisible"] > 0;
			bool flag2 = @uint == SelfRole._inst.m_unIID;
			if (flag2)
			{
				SelfRole._inst.invisible = flag;
				bool flag3 = skillbar.instance != null;
				if (flag3)
				{
					skillbar.instance.forSkill_5008(flag);
				}
				MapProxy.isyinsh = flag;
				SelfRole._inst.refreshmapCount((int)ModelBase<PlayerModel>.getInstance().treasure_num);
				SelfRole._inst.refreshVipLvl((uint)ModelBase<A3_VipModel>.getInstance().Level);
			}
			else
			{
				ProfessionRole otherPlayer = OtherPlayerMgr._inst.GetOtherPlayer(@uint);
				bool flag4 = otherPlayer != null;
				if (flag4)
				{
					otherPlayer.invisible = flag;
					bool flag5 = flag && SelfRole._inst.m_LockRole == otherPlayer;
					if (flag5)
					{
						SelfRole._inst.m_LockRole = null;
					}
				}
			}
		}

		private void on_sprite_hp_info_res(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(53u, this, msgData, false));
		}

		private void on_player_enter_zone(Variant msgData)
		{
			Debug.Log("玩家进入视野 " + msgData.dump());
			foreach (Variant current in msgData["pary"]._arr)
			{
				OtherPlayerMgr._inst.AddOtherPlayer(current);
			}
		}

		private void on_monster_enter_zone(Variant msgData)
		{
			debug.Log("++++++++++++++++++++++++++monster+" + msgData.dump());
			foreach (Variant current in msgData["monsters"]._arr)
			{
				bool flag = current.ContainsKey("carr");
				if (flag)
				{
					MonsterMgr._inst.AddMonster_PVP(current);
				}
				else
				{
					bool flag2 = current.ContainsKey("owner_cid");
					if (flag2)
					{
						MonsterMgr._inst.AddSummon(current);
					}
					else
					{
						bool flag3 = current.ContainsKey("owner_name");
						if (flag3)
						{
							string text = current["owner_name"];
							bool flag4 = BaseProxy<TeamProxy>.getInstance().MyTeamData == null && !ModelBase<PlayerModel>.getInstance().name.Equals(text);
							if (flag4)
							{
								MonsterRole monsterRole = MonsterMgr._inst.AddMonster(current, false);
							}
							else
							{
								bool flag5 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null && !BaseProxy<TeamProxy>.getInstance().MyTeamData.IsInMyTeam(text);
								if (flag5)
								{
									MonsterRole monsterRole = MonsterMgr._inst.AddMonster(current, false);
								}
								else
								{
									MonsterRole monsterRole = MonsterMgr._inst.AddMonster(current, true);
								}
							}
						}
						else
						{
							MonsterRole monsterRole = MonsterMgr._inst.AddMonster(current, true);
						}
					}
				}
				bool flag6 = current.ContainsKey("escort_name");
				if (flag6)
				{
					bool flag7 = ModelBase<PlayerModel>.getInstance().up_lvl >= 1u;
					if (flag7)
					{
						MonsterMgr._inst.AddDartCar(current);
					}
					else
					{
						MonsterMgr._inst.RemoveMonster(current["iid"]);
					}
				}
			}
		}

		private void on_sprite_leave_zone(Variant msgData)
		{
			using (List<Variant>.Enumerator enumerator = msgData["iidary"]._arr.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					uint iid = enumerator.Current;
					OtherPlayerMgr._inst.RemoveOtherPlayer(iid);
					MonsterMgr._inst.RemoveMonster(iid);
				}
			}
		}

		private void onMapChange(Variant msgData)
		{
			bool flag = a3_expbar.instance != null;
			if (flag)
			{
				a3_expbar.instance.CloseAgainst();
			}
			debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>onMapChange>" + msgData.dump());
			A3_BuffModel expr_38 = ModelBase<A3_BuffModel>.getInstance();
			bool flag2 = ((expr_38 != null) ? expr_38.BuffCd : null) != null;
			if (flag2)
			{
				ModelBase<A3_BuffModel>.getInstance().BuffCd.Clear();
				a3_buff expr_61 = a3_buff.instance;
				if (expr_61 != null)
				{
					expr_61.resh_buff();
				}
			}
			bool flag3 = msgData.ContainsKey("states");
			if (flag3)
			{
				Variant variant = msgData["states"];
				foreach (Variant current in variant["state_par"]._arr)
				{
					Variant data = current;
					ModelBase<A3_BuffModel>.getInstance().addBuffList(data);
				}
			}
			bool flag4 = msgData.ContainsKey("pk_state");
			if (flag4)
			{
				ModelBase<PlayerModel>.getInstance().now_pkState = msgData["pk_state"];
				switch (ModelBase<PlayerModel>.getInstance().now_pkState)
				{
				case 0:
					ModelBase<PlayerModel>.getInstance().pk_state = PK_TYPE.PK_PEACE;
					break;
				case 1:
					ModelBase<PlayerModel>.getInstance().pk_state = PK_TYPE.PK_PKALL;
					ModelBase<PlayerModel>.getInstance().m_unPK_Param = ModelBase<PlayerModel>.getInstance().cid;
					ModelBase<PlayerModel>.getInstance().m_unPK_Param2 = ModelBase<PlayerModel>.getInstance().cid;
					break;
				case 2:
					ModelBase<PlayerModel>.getInstance().pk_state = PK_TYPE.PK_TEAM;
					ModelBase<PlayerModel>.getInstance().m_unPK_Param = ModelBase<PlayerModel>.getInstance().teamid;
					ModelBase<PlayerModel>.getInstance().m_unPK_Param2 = ModelBase<PlayerModel>.getInstance().clanid;
					break;
				}
				bool flag5 = a3_pkmodel._instance;
				if (flag5)
				{
					a3_pkmodel._instance.ShowThisImage(msgData["pk_state"]);
				}
				InterfaceMgr.doCommandByLua("PlayerModel:getInstance().modPkState", "model/PlayerModel", new object[]
				{
					ModelBase<PlayerModel>.getInstance().now_pkState,
					true
				});
			}
			GRMap.loading = true;
			bool flag6 = a3_liteMinimap.instance;
			if (flag6)
			{
				a3_liteMinimap.instance.clear();
			}
			ModelBase<PlayerModel>.getInstance().refreshByChangeMap(msgData);
			GRMap.curSvrMsg = msgData;
			NetClient.instance.dispatchEvent(GameEvent.Create(58u, this, msgData, false));
			bool autofighting = SelfRole.fsm.Autofighting;
			if (autofighting)
			{
				SelfRole.fsm.Stop();
			}
			bool flag7 = msgData["hp"] <= 0;
			if (flag7)
			{
				SelfRole._inst.onDead(true, null);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_RELIVE, null, false);
			}
		}

		private void on_other_eqp_change(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(70u, this, msgData, false));
		}

		private void on_map_dpitem_res(Variant msg_data)
		{
			debug.Log("!!!!!!!!!!on_map_dpitem_res!" + msg_data.dump());
			float x = msg_data["x"]._float / 53.333f;
			float z = msg_data["y"]._float / 53.333f;
			bool flag = msg_data.ContainsKey("owner_see");
			if (flag)
			{
				bool flag2 = msg_data["owner_see"] != ModelBase<PlayerModel>.getInstance().cid;
				if (flag2)
				{
					return;
				}
			}
			Vector3 pos = new Vector3(x, 0f, z);
			List<Variant> arr = msg_data["itms"]._arr;
			List<DropItemdta> list = new List<DropItemdta>();
			long curServerTimeStampMS = NetClient.instance.CurServerTimeStampMS;
			foreach (Variant current in arr)
			{
				DropItemdta dropItemdta = new DropItemdta();
				dropItemdta.init(current, curServerTimeStampMS);
				list.Add(dropItemdta);
			}
			bool flag3 = BaseRoomItem.instance != null;
			if (flag3)
			{
				BaseRoomItem.instance.showDropItem(pos, list, false);
			}
		}

		public void sendPickUpItem(uint dpid)
		{
			Variant variant = new Variant();
			variant["id"] = dpid;
			this.sendRPC(77u, variant);
		}

		private void on_pickitem(Variant msg_data)
		{
			int num = msg_data["res"];
			debug.Log("dddddd-------------" + msg_data.dump());
			bool flag = num == 1;
			if (flag)
			{
				bool flag2 = BaseRoomItem.instance != null;
				if (flag2)
				{
					bool flag3 = msg_data["cid"] == ModelBase<PlayerModel>.getInstance().cid;
					if (flag3)
					{
						BaseRoomItem.instance.flyGetItmTxt(msg_data["id"], false);
					}
					BaseRoomItem.instance.removeDropItm(msg_data["id"], false);
				}
			}
			else
			{
				bool flag4 = num == -824;
				if (flag4)
				{
					flytxt.instance.fly(ContMgr.getCont("worldmap_cangetitem", null), 0, default(Color), null);
				}
				else
				{
					bool flag5 = num == -1101;
					if (flag5)
					{
						flytxt.instance.fly(ContMgr.getCont("worldmap_fullbag", null), 0, default(Color), null);
						DropItem.cantGetTimer = NetClient.instance.CurServerTimeStampMS + 1500L;
					}
					else
					{
						bool flag6 = num == -825;
						if (flag6)
						{
							int num2 = msg_data["tm"];
							bool flag7 = num2 <= 0;
							if (flag7)
							{
								num2 = 1;
							}
							flytxt.instance.fly(ContMgr.getCont("worldmap_cangetitem_tm", new List<string>
							{
								num2.ToString()
							}), 0, default(Color), null);
						}
					}
				}
			}
		}

		public void sendCollectItem(uint dpid)
		{
			Variant variant = new Variant();
			variant["iid"] = dpid;
			this.sendRPC(108u, variant);
		}

		public void sendCollectBox(uint dpid)
		{
			Variant variant = new Variant();
			variant["op"] = 1;
			variant["iid"] = dpid;
			this.sendRPC(199u, variant);
		}

		public void sendStopCollectBox(bool forcestop)
		{
			Variant variant = new Variant();
			variant["op"] = (forcestop ? 2 : 3);
			this.sendRPC(199u, variant);
		}

		public void sendStopCollectItem(bool forcestop)
		{
			Variant variant = new Variant();
			variant["end_tp"] = (forcestop ? 2 : 1);
			this.sendRPC(109u, variant);
			A3_TaskModel instance = ModelBase<A3_TaskModel>.getInstance();
			a3_task_auto.instance.RunTask(instance.curTask, false, false);
		}

		public void onbeginCollect(Variant msg)
		{
			debug.Log("onbeginCollect:::" + msg.dump());
			int num = msg["res"];
			bool flag = num < 0;
			if (flag)
			{
				flytxt.instance.fly(ContMgr.getError(num.ToString()), 0, default(Color), null);
				InterfaceMgr.getInstance().close(InterfaceMgr.CD);
				A3_TaskModel instance = ModelBase<A3_TaskModel>.getInstance();
				a3_task_auto.instance.RunTask(instance.curTask, false, false);
			}
		}
	}
}
