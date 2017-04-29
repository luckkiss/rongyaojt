using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class PlayerInfoProxy : BaseProxy<PlayerInfoProxy>
	{
		public static uint EVENT_ADD_POINT = 0u;

		public static uint EVENT_ON_EXP_CHANGE = 1u;

		public static uint EVENT_ON_LV_CHANGE = 2u;

		public static uint EVENT_SELF_ON_LV_CHANGE = 3u;

		public static uint EVENT_ONGETPLAYERINFO = 211u;

		public bool look_player;

		public PlayerInfoProxy()
		{
			this.addProxyListener(211u, new Action<Variant>(this.onGetPlayerInfo));
			this.addProxyListener(26u, new Action<Variant>(this.onAttChange));
			this.addProxyListener(32u, new Action<Variant>(this.onSelfAttchange));
			this.addProxyListener(40u, new Action<Variant>(this.onDetailInfoChange));
			this.addProxyListener(78u, new Action<Variant>(this.onViewAvatar_Change));
			this.addProxyListener(41u, new Action<Variant>(this.onSkexpChange));
			this.addProxyListener(51u, new Action<Variant>(this.onPlayerShowInfo));
			this.addProxyListener(52u, new Action<Variant>(this.onPlayerDetailInfo));
			this.addProxyListener(148u, new Action<Variant>(this.onPlayerAddPoint));
			this.addProxyListener(60u, new Action<Variant>(this.onLvlUp));
			this.addProxyListener(61u, new Action<Variant>(this.onModeExp));
			this.addProxyListener(251u, new Action<Variant>(this.onGetUserCidRes));
		}

		public void SendGetPlayerFromName(string name)
		{
			this.look_player = false;
			Variant variant = new Variant();
			variant["name"] = name;
			this.sendRPC(211u, variant);
		}

		private void onGetPlayerInfo(Variant msgData)
		{
			bool flag = msgData.ContainsKey("res");
			if (flag)
			{
				bool flag2 = msgData["res"] < 0;
				if (flag2)
				{
					Globle.err_output(msgData["res"]);
				}
			}
			bool flag3 = msgData.ContainsKey("cid");
			if (flag3)
			{
				this.look_player = true;
			}
			base.dispatchEvent(GameEvent.Create(PlayerInfoProxy.EVENT_ONGETPLAYERINFO, this, msgData, false));
		}

		public void sendLoadPlayerDetailInfo(uint cid)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			this.sendRPC(52u, variant);
		}

		public void sendAddPoint(int type, Dictionary<int, int> add_pt)
		{
			Variant variant = new Variant();
			variant["op"] = type;
			bool flag = type == 0;
			if (flag)
			{
				foreach (int current in add_pt.Keys)
				{
					switch (current)
					{
					case 1:
						variant["strpt"] = add_pt[current];
						break;
					case 2:
						variant["intept"] = add_pt[current];
						break;
					case 3:
						variant["agipt"] = add_pt[current];
						break;
					case 4:
						variant["conpt"] = add_pt[current];
						break;
					case 5:
						variant["wispt"] = add_pt[current];
						break;
					}
				}
			}
			bool flag2 = type == 1;
			if (flag2)
			{
			}
			this.sendRPC(148u, variant);
		}

		public void sendAttChange()
		{
			Variant v = new Variant();
			this.sendRPC(26u, v);
		}

		public void sendSelfAttchange()
		{
			Variant v = new Variant();
			this.sendRPC(32u, v);
		}

		public void sendDetailInfoChange()
		{
			Variant v = new Variant();
			this.sendRPC(40u, v);
		}

		public void sendSkexpChange()
		{
			Variant v = new Variant();
			this.sendRPC(41u, v);
		}

		public void sendPlayerShowInfo(Variant cids)
		{
			Variant variant = new Variant();
			variant["cidary"] = cids;
			this.sendRPC(51u, variant);
		}

		public void sendPlayerDetailInfo(uint cid)
		{
			Variant variant = new Variant();
			variant["cid"] = cid;
			this.sendRPC(52u, variant);
		}

		public void sendlvl_up()
		{
			Variant v = new Variant();
			this.sendRPC(60u, v);
		}

		public void sendmod_exp()
		{
			Variant v = new Variant();
			this.sendRPC(61u, v);
		}

		public void sendon_get_user_cid_res(string name, bool ol, uint func)
		{
			Variant variant = new Variant();
			variant["ol"] = ol;
			variant["name"] = name;
			variant["func"] = func;
			this.sendRPC(251u, variant);
		}

		public void sendon_query_ply_info_res(Variant data)
		{
			this.sendRPC(253u, data);
		}

		private void onAttChange(Variant msgData)
		{
			RoleMgr._instance.onAttchange(msgData);
		}

		private void onPlayerAddPoint(Variant msgData)
		{
			int num = msgData["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
			}
			else
			{
				base.dispatchEvent(GameEvent.Create(PlayerInfoProxy.EVENT_ADD_POINT, this, msgData, false));
			}
		}

		private void onSelfAttchange(Variant msgData)
		{
			debug.Log("属性变换" + msgData.dump());
			bool flag = msgData.ContainsKey("mpleft");
			if (flag)
			{
				ModelBase<PlayerModel>.getInstance().modMp(msgData["mpleft"]);
			}
			ModelBase<PlayerModel>.getInstance().attrChangeCheck(msgData);
			ModelBase<PlayerModel>.getInstance().attPointCheck(msgData);
		}

		private void onViewAvatar_Change(Variant v)
		{
			debug.Log("PPP" + v.dump());
			uint num = v["iid"];
			bool flag = v.ContainsKey("serial_kp");
			if (flag)
			{
				bool flag2 = v["iid"] == ModelBase<PlayerModel>.getInstance().iid;
				if (flag2)
				{
					bool flag3 = SelfRole._inst != null;
					if (flag3)
					{
						ModelBase<PlayerModel>.getInstance().serial = v["serial_kp"];
						SelfRole._inst.serial = v["serial_kp"];
						PlayerNameUIMgr.getInstance().refreserialCount(SelfRole._inst, v["serial_kp"]);
					}
				}
			}
			bool flag4 = v.ContainsKey("strike_back_tm");
			if (flag4)
			{
				bool flag5 = v["iid"] == ModelBase<PlayerModel>.getInstance().iid;
				if (flag5)
				{
					bool flag6 = v.ContainsKey("strike_back_tm");
					if (flag6)
					{
						bool flag7 = SelfRole._inst != null;
						if (flag7)
						{
							SelfRole._inst.hidbacktime = v["strike_back_tm"];
							bool flag8 = v["strike_back_tm"] == 0;
							if (flag8)
							{
								ModelBase<PlayerModel>.getInstance().hitBack = 0u;
								PlayerNameUIMgr.getInstance().refresHitback(SelfRole._inst, 0, true);
							}
							else
							{
								ModelBase<PlayerModel>.getInstance().hitBack = SelfRole._inst.hidbacktime - (uint)NetClient.instance.CurServerTimeStamp;
								PlayerNameUIMgr.getInstance().refresHitback(SelfRole._inst, (int)(SelfRole._inst.hidbacktime - (uint)NetClient.instance.CurServerTimeStamp), true);
							}
						}
					}
				}
			}
			else
			{
				OtherPlayerMgr._inst.refreshPlayerInfo(v);
			}
		}

		private void onDetailInfoChange(Variant msgData)
		{
		}

		private void onSkexpChange(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(41u, this, msgData, false));
		}

		private void onPlayerShowInfo(Variant msgData)
		{
			NetClient.instance.dispatchEvent(GameEvent.Create(51u, this, msgData, false));
		}

		private void onPlayerDetailInfo(Variant msgData)
		{
			debug.Log("onPlayerDetailInfo" + msgData.dump());
			Variant variant = msgData["pinfo"];
			bool flag = msgData["res"]._int == 1;
			if (flag)
			{
				OtherPlayerMgr._inst.refreshPlayerInfo(msgData["pinfo"]);
			}
			bool flag2 = variant == null || !variant.ContainsKey("pet_food_last_time");
			if (!flag2)
			{
				bool flag3 = variant["pet_food_last_time"] == 0;
				if (flag3)
				{
					debug.Log("对方玩家宠物没有饲料");
					OtherPlayerMgr._inst.PlayPetAvatarChange(variant["iid"], 0, 0);
				}
				else
				{
					OtherPlayerMgr._inst.PlayPetAvatarChange(variant["iid"], variant["pet"]["id"], 0);
				}
			}
		}

		private void onLvlUp(Variant msgData)
		{
			bool flag = a3_liteMinimap.instance != null;
			if (flag)
			{
				a3_liteMinimap.instance.function_open(a3_liteMinimap.instance.fun_i);
			}
			debug.Log("收到升级或者转生的协议........." + msgData.dump());
			bool flag2 = msgData.ContainsKey("cid");
			if (flag2)
			{
				bool flag3 = msgData["cid"] != ModelBase<PlayerModel>.getInstance().cid;
				if (flag3)
				{
					bool flag4 = OtherPlayerMgr._inst != null && OtherPlayerMgr._inst.m_mapOtherPlayer.Count > 0;
					if (flag4)
					{
						bool flag5 = OtherPlayerMgr._inst.m_mapOtherPlayer.ContainsKey(msgData["iid"]);
						if (flag5)
						{
							OtherPlayerMgr._inst.m_mapOtherPlayer[msgData["iid"]].zhuan = msgData["zhuan"];
						}
					}
					bool flag6 = SelfRole._inst != null && SelfRole._inst.m_LockRole != null && SelfRole._inst.m_LockRole.m_unIID == msgData["iid"];
					if (flag6)
					{
						PkmodelAdmin.RefreshShow(SelfRole._inst.m_LockRole, true, false);
					}
				}
				else
				{
					ModelBase<PlayerModel>.getInstance().lvUp(msgData);
					base.dispatchEvent(GameEvent.Create(PlayerInfoProxy.EVENT_SELF_ON_LV_CHANGE, this, msgData, false));
					bool flag7 = msgData.ContainsKey("pinfo");
					if (flag7)
					{
						InterfaceMgr.doCommandByLua("PlayerModel:getInstance().modExp", "model/PlayerModel", new object[]
						{
							msgData["pinfo"]["exp"]
						});
					}
					bool flag8 = msgData.ContainsKey("mod_exp");
					if (flag8)
					{
						flytxt.instance.fly("EXP+" + msgData["mod_exp"], 3, default(Color), null);
					}
				}
			}
			bool flag9 = a3_QHmaster.instance != null;
			if (flag9)
			{
				a3_QHmaster.instance.refreshDashi();
			}
			base.dispatchEvent(GameEvent.Create(PlayerInfoProxy.EVENT_ON_LV_CHANGE, this, msgData, false));
			BaseProxy<ResetLvLProxy>.getInstance().resetLvL();
		}

		private void onGetUserCidRes(Variant msgData)
		{
			(NetClient.instance.g_gameM as muLGClient).g_MgrPlayerInfoCT.on_get_user_cid_res(msgData);
		}

		private void onModeExp(Variant msgData)
		{
			ModelBase<PlayerModel>.getInstance().exp = ModelBase<PlayerModel>.getInstance().exp + msgData["mod_exp"];
			debug.Log("经验增加：" + msgData["mod_exp"]);
			bool flag = flytxt.instance;
			if (flag)
			{
				flytxt.instance.fly("EXP+" + msgData["mod_exp"], 3, default(Color), null);
			}
			bool flag2 = a3_insideui_fb.instance != null;
			if (flag2)
			{
				a3_insideui_fb.instance.SetInfExp(msgData["mod_exp"]);
			}
			bool flag3 = GameRoomMgr.getInstance().curRoom != null;
			if (flag3)
			{
				GameRoomMgr.getInstance().curRoom.onAddExp(msgData["mod_exp"]);
			}
			bool flag4 = msgData.ContainsKey("cur_exp");
			if (flag4)
			{
				ModelBase<PlayerModel>.getInstance().exp = msgData["cur_exp"];
			}
			InterfaceMgr.doCommandByLua("PlayerModel:getInstance().modExp", "model/PlayerModel", new object[]
			{
				ModelBase<PlayerModel>.getInstance().exp
			});
			base.dispatchEvent(GameEvent.Create(PlayerInfoProxy.EVENT_ON_EXP_CHANGE, this, msgData, false));
		}
	}
}
