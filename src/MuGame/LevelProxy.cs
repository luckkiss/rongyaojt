using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class LevelProxy : BaseProxy<LevelProxy>
	{
		public List<Rewards> reward = new List<Rewards>();

		public bool is_open;

		public bool open_pic;

		public string[] codes;

		public string[] codess;

		public string icon1;

		public LevelProxy()
		{
			this.addProxyListener(232u, new Action<Variant>(this.clanter_res));
			this.addProxyListener(233u, new Action<Variant>(this.lvl_res));
			this.addProxyListener(236u, new Action<Variant>(this.lvl_broadcast));
			this.addProxyListener(235u, new Action<Variant>(this.on_arena));
			this.addProxyListener(237u, new Action<Variant>(this.lvl_pvpinfo_board_res));
			this.addProxyListener(238u, new Action<Variant>(this.mod_lvl_selfpvpinfo));
			this.addProxyListener(239u, new Action<Variant>(this.lvl_err_msg));
			this.addProxyListener(240u, new Action<Variant>(this.check_in_lvl_res));
			this.addProxyListener(241u, new Action<Variant>(this.create_lvl_res));
			this.addProxyListener(242u, new Action<Variant>(this.enter_lvl_res));
			this.addProxyListener(243u, new Action<Variant>(this.get_associate_lvls_res));
			this.addProxyListener(245u, new Action<Variant>(this.lvl_fin));
			this.addProxyListener(247u, new Action<Variant>(this.lvl_side_info));
			this.addProxyListener(248u, new Action<Variant>(this.close_lvl_res));
			this.addProxyListener(249u, new Action<Variant>(this.lvl_km));
			this.addProxyListener(250u, new Action<Variant>(this.leave_lvl_res));
			this.addProxyListener(230u, new Action<Variant>(this.on_battle_do_res));
			this.addProxyListener(244u, new Action<Variant>(this.get_lvl_info_res));
			this.addProxyListener(246u, new Action<Variant>(this.lvl_get_prize_res));
		}

		public void sendBuyEnergy()
		{
			this.sendRPC(248u, null);
		}

		public void sendGet_clanter_info(Variant data)
		{
			this.sendRPC(232u, data);
		}

		public void sendGet_lvl_info(Variant data)
		{
			this.sendRPC(233u, data);
		}

		public void sendGet_arena_info(Variant data)
		{
			this.sendRPC(235u, data);
		}

		public void sendGet_lvl_pvpinfo_board(Variant data)
		{
			this.sendRPC(239u, data);
		}

		public void sendCheck_in_lvl(Variant data)
		{
			this.sendRPC(240u, data);
		}

		public void sendCreate_lvl(Variant data)
		{
			this.sendRPC(241u, data);
		}

		public void sendEnter_lvl(Variant data)
		{
			debug.Log("!!sendRPC(PKG_NAME.C2S_ENTER_LVL_RES, data)3!! " + debug.count);
			this.sendRPC(242u, data);
		}

		public void sendGet_associate_lvls(Variant data)
		{
			this.sendRPC(243u, data);
		}

		public void sendGet_lvl_cnt_info(int type, int param1 = 0, int param2 = 0)
		{
			Variant variant = new Variant();
			variant["operation"] = type;
			bool flag = param1 > 0;
			if (flag)
			{
				variant["param1"] = param1;
			}
			bool flag2 = param2 > 0;
			if (flag2)
			{
				variant["param2"] = param2;
			}
			this.sendRPC(244u, variant);
		}

		public void sendGet_lvl_prize(Variant data)
		{
			this.sendRPC(245u, data);
		}

		public void sendLeave_lvl()
		{
			this.sendRPC(250u, new Variant());
		}

		public void sendClose_lvl(Variant data)
		{
			this.sendRPC(247u, data);
		}

		public void sendGetLvlmisInfo()
		{
			this.sendRPC(117u, new Variant());
		}

		private void get_lvlmis_res(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(117u, this, GameTools.CreateSwitchData("get_lvlmis_res", msgData), false));
		}

		private void clanter_res(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(232u, this, GameTools.CreateSwitchData("on_clanter_res", msgData), false));
		}

		private void lvl_res(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(233u, this, GameTools.CreateSwitchData("on_lvl_res", msgData), false));
		}

		private void onNPCShop(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(234u, this, GameTools.CreateSwitchData("on_npcshop_res", msgData), false));
		}

		private void on_arena(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(235u, this, GameTools.CreateSwitchData("on_arena_res", msgData), false));
		}

		private void lvl_broadcast(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(236u, this, GameTools.CreateSwitchData("on_lvl_broadcast_res", msgData), false));
		}

		private void lvl_pvpinfo_board_res(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(237u, this, GameTools.CreateSwitchData("lvl_pvpinfo_board_msg", msgData), false));
			BaseProxy<TeamProxy>.getInstance().dispatchEvent(GameEvent.Create(237u, this, msgData, false));
		}

		private void mod_lvl_selfpvpinfo(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(238u, this, GameTools.CreateSwitchData("mod_lvl_selfpvpinfo", msgData), false));
		}

		private void lvl_err_msg(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(239u, this, GameTools.CreateSwitchData("on_lvl_err_msg", msgData), false));
		}

		private void check_in_lvl_res(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(240u, this, GameTools.CreateSwitchData("on_check_in_lvl_res", msgData), false));
		}

		private void create_lvl_res(Variant msgData)
		{
			Debug.Log("Level Create ===============================");
			Debug.Log(msgData.dump());
			NetClient.instance.dispatchEvent(GameEvent.Create(241u, this, GameTools.CreateSwitchData("on_create_lvl_res", msgData), false));
		}

		private void enter_lvl_res(Variant msgData)
		{
			Debug.Log("Level Enter ===============================");
			Debug.Log(msgData.dump());
			uint ltpid = msgData["ltpid"];
			Variant variant = SvrLevelConfig.instacne.get_level_data(ltpid);
			bool flag = variant.ContainsKey("public") && variant["public"] == 1;
			if (flag)
			{
				this.is_open = true;
			}
			else
			{
				this.is_open = false;
			}
			bool flag2 = variant.ContainsKey("shengwu");
			if (flag2)
			{
				this.open_pic = true;
				string text = variant["icon"];
				this.codes = text.Split(new char[]
				{
					','
				});
				this.icon1 = variant["des"];
				this.codess = this.icon1.Split(new char[]
				{
					','
				});
			}
			a3_counterpart.lvl = msgData["diff_lvl"];
			NetClient.instance.dispatchEvent(GameEvent.Create(242u, this, GameTools.CreateSwitchData("on_enter_lvl_res", msgData), false));
		}

		private void get_associate_lvls_res(Variant msgData)
		{
			int num = msgData["res"];
			int num2 = num;
			if (num2 == 3)
			{
				int num3 = msgData["trig_id"];
				SceneCamera.CheckTrrigerCam(num3);
				bool flag = a3_trrigerDialog.instance != null;
				if (flag)
				{
					a3_trrigerDialog.instance.CheckDialog(num3);
				}
			}
			bool flag2 = GameRoomMgr.getInstance().onLevelStatusChanges(msgData);
			if (!flag2)
			{
				NetClient.instance.dispatchEvent(GameEvent.Create(243u, this, GameTools.CreateSwitchData("get_associate_lvls_res", msgData), false));
			}
		}

		private void get_lvl_info_res(Variant msgData)
		{
			int num = msgData["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
			}
			else
			{
				debug.Log("KKKKKKKK" + msgData.dump());
				MapModel instance = MapModel.getInstance();
				switch (num)
				{
				case 1:
				{
					int num2 = 0;
					int num3 = 0;
					bool flag2 = msgData.ContainsKey("lvls");
					if (flag2)
					{
						List<Variant> arr = msgData["lvls"]._arr;
						foreach (Variant current in arr)
						{
							MapData mapData = instance.getMapDta(current["lvlid"]);
							bool flag3 = mapData == null;
							if (flag3)
							{
								mapData = new MapData();
							}
							instance.AddMapDta(current["lvlid"], mapData);
							mapData = instance.getMapDta(current["lvlid"]);
							mapData.starNum = current["score"];
							bool flag4 = current.ContainsKey("last_enter_time");
							if (flag4)
							{
								mapData.enterTime = current["last_enter_time"];
							}
							bool flag5 = current.ContainsKey("enter_times");
							if (flag5)
							{
								mapData.cycleCount = current["enter_times"];
							}
							bool flag6 = current.ContainsKey("limit_tm");
							if (flag6)
							{
								mapData.limit_tm = current["limit_tm"];
							}
							bool flag7 = mapData.type == 1;
							if (flag7)
							{
								bool flag8 = mapData.starNum > 0 && mapData.id > num2;
								if (flag8)
								{
									num2 = mapData.id;
								}
							}
							else
							{
								bool flag9 = mapData.type == 2;
								if (flag9)
								{
									bool flag10 = mapData.starNum > 0 && mapData.id > num3;
									if (flag10)
									{
										num3 = mapData.id;
									}
								}
							}
						}
						instance.setLastMapId(0, num2);
						instance.setLastMapId(1, num3);
						instance.inited = true;
					}
					bool flag11 = msgData.ContainsKey("mlzd_diff");
					if (flag11)
					{
						ModelBase<A3_ActiveModel>.getInstance().nowlvl = msgData["mlzd_diff"];
					}
					MapModel instance2 = MapModel.getInstance();
					break;
				}
				case 2:
				{
					int id = msgData["lvlid"];
					MapData mapDta = MapModel.getInstance().getMapDta(id);
					mapDta.count = msgData["left_times"];
					List<Variant> arr2 = msgData["rewards"]._arr;
					int num4 = arr2.Count / 3;
					int num5 = 0;
					List<List<MapItemData>> list = new List<List<MapItemData>>();
					for (int i = 0; i < num4; i++)
					{
						List<MapItemData> list2 = new List<MapItemData>();
						for (int j = 0; j < 3; j++)
						{
							list2.Add(new MapItemData
							{
								id = arr2[num5]["tpid"],
								count = arr2[num5]["cnt"]
							});
							num5++;
						}
						list.Add(list2);
					}
					break;
				}
				case 3:
				{
					int id = msgData["lvlid"];
					MapData mapDta = MapModel.getInstance().getMapDta(id);
					mapDta.count = msgData["left_times"];
					mapDta.resetCount = msgData["left_reset_times"];
					break;
				}
				case 4:
				{
					int num6 = msgData["lvlid"];
					bool flag12 = instance.containerID(num6);
					if (!flag12)
					{
						MapData mapData = instance.getMapDta(num6);
						bool flag13 = mapData == null;
						if (!flag13)
						{
							mapData.starNum = msgData["score"];
							mapData.count = msgData["left_times"];
							mapData.resetCount = msgData["left_reset_times"];
							bool flag14 = num6 == 104;
							if (flag14)
							{
								bool flag15 = msgData.ContainsKey("diff_lvl");
								if (flag15)
								{
									ModelBase<A3_ActiveModel>.getInstance().nowlvl = msgData["diff_lvl"];
								}
							}
						}
					}
					break;
				}
				}
			}
		}

		private void lvl_fin(Variant msgData)
		{
			this.reward.Clear();
			debug.Log("Fin Level ===============================" + msgData.dump());
			bool flag = msgData != null;
			if (flag)
			{
				bool flag2 = msgData["rewards"] != null;
				if (flag2)
				{
					List<Variant> arr = msgData["rewards"]._arr;
					foreach (Variant current in arr)
					{
						Rewards rewards = new Rewards();
						rewards.tpid = current["tpid"];
						rewards.cnt = current["cnt"];
						this.reward.Add(rewards);
					}
				}
			}
			bool flag3 = GameRoomMgr.getInstance().onLevelFinish(msgData);
			if (!flag3)
			{
				bool flag4 = msgData.ContainsKey("ply_res");
				if (flag4)
				{
					int num = msgData["ply_res"]._arr[0]["score"];
					bool flag5 = num == 0;
					if (flag5)
					{
						InterfaceMgr.getInstance().open(InterfaceMgr.FB_LOSE, null, false);
					}
				}
				else
				{
					BaseProxy<LevelProxy>.getInstance().sendLeave_lvl();
				}
			}
		}

		private void lvl_get_prize_res(Variant msgData)
		{
			bool flag = GameRoomMgr.getInstance().onPrizeFinish(msgData);
			bool flag2 = flag;
			if (!flag2)
			{
				Debug.LogError("KKKKK" + msgData.dump());
				PVPRoom.instan.refGet(msgData["ach_point"], msgData["exp"]);
				List<MapItemData> list = new List<MapItemData>();
				bool flag3 = msgData.ContainsKey("rewards");
				if (flag3)
				{
					List<Variant> arr = msgData["rewards"]._arr;
					foreach (Variant current in arr)
					{
						list.Add(new MapItemData
						{
							count = current["cnt"],
							id = current["tpid"]
						});
					}
				}
			}
		}

		private void lvl_side_info(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(247u, this, GameTools.CreateSwitchData("on_lvl_side_info", msgData), false));
		}

		private void close_lvl_res(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(248u, this, GameTools.CreateSwitchData("on_close_lvl_res", msgData), false));
		}

		private void lvl_km(Variant msgData)
		{
		}

		private void leave_lvl_res(Variant msgData)
		{
			MapModel.getInstance().curLevelId = 0u;
			InterfaceMgr.doCommandByLua("MapModel:getInstance().getcurLevelId", "model/MapModel", new object[]
			{
				0
			});
			NetClient.instance.dispatchEvent(GameEvent.Create(250u, this, GameTools.CreateSwitchData("on_leave_lvl", msgData), false));
		}

		private void on_battle_do_res(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(230u, this, GameTools.CreateSwitchData("on_battle_do_res", msgData), false));
		}
	}
}
