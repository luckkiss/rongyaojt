using Cross;
using GameFramework;
using MuGame.Qsmy.model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class BattleProxy : BaseProxy<BattleProxy>
	{
		private HudunModel hudunModel = ModelBase<HudunModel>.getInstance();

		public static uint EVENT_SHIELD_FX = 1u;

		public static uint EVENT_SHIELD_LOST = 2u;

		public static readonly uint EVENT_SELF_KILL_MON = 3u;

		public int skill_id;

		public int star_tm;

		public int end_tm;

		private float waitTime = 0f;

		private float shield_time = 1f;

		private TickItem process;

		private bool ShieldFirst = true;

		private GameObject shield_fx_clon;

		public BattleProxy()
		{
			this.addProxyListener(12u, new Action<Variant>(this.on_monsterHated));
			this.addProxyListener(13u, new Action<Variant>(this.on_cast_target_skill));
			this.addProxyListener(14u, new Action<Variant>(this.on_cast_ground_skill));
			this.addProxyListener(18u, new Action<Variant>(this.on_single_damage));
			this.addProxyListener(22u, new Action<Variant>(this.on_single_skill_res));
			this.addProxyListener(15u, new Action<Variant>(this.on_cast_self_skill));
			this.addProxyListener(38u, new Action<Variant>(this.on_cast_skill_act));
			this.addProxyListener(19u, new Action<Variant>(this.on_bstate_change));
			this.addProxyListener(24u, new Action<Variant>(this.on_add_state));
			this.addProxyListener(25u, new Action<Variant>(this.on_die));
			this.addProxyListener(27u, new Action<Variant>(this.on_cast_skill_res));
			this.addProxyListener(28u, new Action<Variant>(this.on_casting_skill_res));
			this.addProxyListener(29u, new Action<Variant>(this.on_cancel_casting_skill_res));
			this.addProxyListener(31u, new Action<Variant>(this.on_rmv_state));
		}

		public void sendUseSelfSkill(uint skillid)
		{
			Variant v = GameTools.createGroup(new Variant[]
			{
				"sid",
				skillid,
				"start_tm",
				muNetCleint.instance.CurServerTimeStamp
			});
			this.sendRPC(6u, v);
		}

		public void sendcast_target_skill(uint sid, List<uint> list_hitted, int lasthit, int lockid = -1)
		{
			bool flag = list_hitted.Count <= 0;
			if (!flag)
			{
				Variant variant = new Variant();
				variant["sid"] = sid;
				variant["crit"] = lasthit;
				Variant variant2 = new Variant();
				for (int i = 0; i < list_hitted.Count; i++)
				{
					bool flag2 = i > 15;
					if (!flag2)
					{
						variant2.pushBack(list_hitted[i]);
					}
				}
				variant["to_iid"] = variant2;
				bool flag3 = lockid != -1;
				if (flag3)
				{
					variant["lock_id"] = lockid;
				}
				this.sendRPC(4u, variant);
			}
		}

		public void sendcast_ground_skill(uint sid, float grdx, float grdy)
		{
			Variant v = GameTools.createGroup(new Variant[]
			{
				"x",
				grdx,
				"y",
				grdy,
				"sid",
				sid,
				"start_tm",
				muNetCleint.instance.CurServerTimeStampMS
			});
			this.sendRPC(4u, v);
		}

		public void sendcast_self_skill(uint sid)
		{
			this.sendRPC(6u, GameTools.createGroup(new Variant[]
			{
				"sid",
				sid,
				"start_tm",
				muNetCleint.instance.CurServerTimeStampMS
			}));
		}

		public void send_cast_self_skill(uint iid, int skillid)
		{
			if (skillid == 2003 || skillid == 3002 || skillid == 5003)
			{
				string url = "audio/skill/" + skillid;
				MediaClient.instance.PlaySoundUrl(url, false, null);
			}
			bool flag = !SelfRole.s_bStandaloneScene;
			if (flag)
			{
				debug.Log(string.Concat(new object[]
				{
					"id是：",
					iid,
					"技能是：",
					skillid
				}));
				this.sendRPC(15u, GameTools.createGroup(new Variant[]
				{
					"lock_iid",
					iid,
					"skillid",
					skillid
				}));
			}
		}

		public void sendstop_atk()
		{
			this.sendRPC(8u, new Variant());
		}

		public void sendcollectItm(uint iid)
		{
			this.sendRPC(13u, GameTools.createGroup(new Variant[]
			{
				"to_iid",
				iid
			}));
		}

		public void sendcollectAreaItm(uint areaId)
		{
			this.sendRPC(14u, GameTools.createGroup(new Variant[]
			{
				"area_id",
				areaId
			}));
		}

		public void sendgetNpcState(uint npcid, uint state)
		{
			this.sendRPC(107u, GameTools.createGroup(new Variant[]
			{
				"npcid",
				npcid,
				"state",
				state
			}));
		}

		private void on_monsterHated(Variant msgData)
		{
			bool flag = msgData.ContainsKey("hated_iid");
			if (flag)
			{
				RoleMgr._instance.onMonsterHate(msgData["monster_iid"], msgData["hated_iid"]);
			}
			else
			{
				RoleMgr._instance.onMonsterHate(msgData["monster_iid"], 0u);
			}
		}

		private void on_cast_target_skill(Variant msgData)
		{
		}

		private void on_cast_ground_skill(Variant msgData)
		{
			debug.Log("定点放技能：" + msgData.dump());
			uint num = msgData["start_tm"];
			uint @uint = msgData["frm_iid"]._uint;
			BaseRole role = RoleMgr._instance.getRole(@uint);
			bool flag = role == null;
			if (!flag)
			{
				int id = msgData["sid"];
				float x = msgData["x"]._float * 32f / 53.333f;
				float z = msgData["y"]._float * 32f / 53.333f;
				Vector3 effectVec = new Vector3(x, role.m_curModel.position.y, z);
				bool flag2 = role is MS0000;
				if (flag2)
				{
					(role as MS0000).ismapEffect = true;
					(role as MS0000).effectVec = effectVec;
					role.PlaySkill(id);
				}
			}
		}

		private void on_cast_self_skill(Variant msgData)
		{
			uint @uint = msgData["frm_iid"]._uint;
			int @int = msgData["sid"]._int;
			BaseRole role = RoleMgr._instance.getRole(@uint);
			bool flag = role == null;
			if (!flag)
			{
				bool flag2 = msgData.ContainsKey("lock_iid");
				if (flag2)
				{
					uint uint2 = msgData["lock_iid"]._uint;
					BaseRole role2 = RoleMgr._instance.getRole(uint2);
					bool flag3 = role2 != null;
					if (flag3)
					{
						role.TurnToRole(role2, false);
						role.m_LockRole = role2;
					}
					else
					{
						debug.Log("攻击目标为空");
					}
				}
				bool flag4 = msgData.ContainsKey("telep");
				if (flag4)
				{
					debug.Log("boss位移拉！！！！" + msgData.dump());
					float x = msgData["telep"]["to_x"];
					float y = msgData["telep"]["to_y"];
					role.m_curModel.GetComponent<Monster_Base_Event>().onJump(x, y);
				}
				else
				{
					role.OtherSkillShow();
					role.PlaySkill(@int);
				}
			}
		}

		private void on_cast_skill_act(Variant msgData)
		{
			uint iid = msgData["iid"];
			uint iid2 = msgData["to_iid"];
			uint id = msgData["skill_id"];
			BaseRole role = RoleMgr._instance.getRole(iid2);
			BaseRole role2 = RoleMgr._instance.getRole(iid);
			bool flag = role2 != null && role != null;
			if (flag)
			{
				role2.m_LockRole = role;
				role2.TurnToRole(role, false);
				role2.PlaySkill((int)id);
			}
			bool flag2 = role2 is MS0000;
			if (flag2)
			{
				debug.Log("放技能：" + msgData.dump());
			}
		}

		private void on_casting_skill_res(Variant msgData)
		{
			debug.Log("释放怪物的大招技能???????????");
			debug.Log(msgData.dump());
			uint iid = msgData["iid"];
			uint iid2 = msgData["to_iid"];
			uint id = msgData["skid"];
			BaseRole role = RoleMgr._instance.getRole(iid2);
			BaseRole role2 = RoleMgr._instance.getRole(iid);
			bool flag = role2 != null && role != null;
			if (flag)
			{
				role2.m_LockRole = role;
				bool flag2 = msgData.ContainsKey("pos");
				if (flag2)
				{
					float x = msgData["pos"]["x"]._float / 53.333f;
					float z = msgData["pos"]["y"]._float / 53.333f;
					Vector3 pos = new Vector3(x, role2.m_curModel.position.y, z);
					role2.TurnToPos(pos);
				}
				else
				{
					role2.TurnToRole(role, false);
				}
				role2.PlaySkill((int)id);
			}
		}

		private void on_single_damage(Variant msg)
		{
			bool flag = msg.ContainsKey("damages");
			if (flag)
			{
				List<Variant> arr = msg["damages"]._arr;
				foreach (Variant current in arr)
				{
					this.doHurt(current);
				}
			}
			bool flag2 = msg.ContainsKey("link_damage");
			if (flag2)
			{
				bool flag3 = false;
				int num = msg["rune_id"];
				List<Variant> arr2 = msg["link_damage"]._arr;
				foreach (Variant current2 in arr2)
				{
					uint @uint = current2["to_iid"]._uint;
					int @int = current2["dmg"]._int;
					bool @bool = current2["isdie"]._bool;
					int int2 = current2["hprest"]._int;
					uint uint2 = current2["frm_iid"]._uint;
					bool stagger = false;
					flag3 |= (uint2 == ModelBase<PlayerModel>.getInstance().iid);
					bool flag4 = current2.ContainsKey("stagger");
					if (flag4)
					{
						stagger = current2["stagger"];
					}
					BaseRole role = RoleMgr._instance.getRole(@uint);
					bool flag5 = role == null;
					if (flag5)
					{
						return;
					}
					BaseRole role2 = RoleMgr._instance.getRole(uint2);
					this.doHurt(role, role2, @int, @bool, int2, num, false, stagger, 0u);
					GameObject runeEff = EffMgr.getRuneEff(num);
					bool flag6 = runeEff != null;
					if (flag6)
					{
						EffMgr.instance.addEff(role2, role, UnityEngine.Object.Instantiate<GameObject>(runeEff), 0.4f);
						GameObject gameObject = UnityEngine.Object.Instantiate(SceneTFX.m_HFX_Prefabs[1], role.m_curModel.position, role.m_curModel.rotation) as GameObject;
						gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
						UnityEngine.Object.Destroy(gameObject, 2f);
					}
				}
				bool flag7 = flag3;
				if (flag7)
				{
					base.dispatchEvent(GameEvent.Create(BattleProxy.EVENT_SELF_KILL_MON, this, null, false));
				}
			}
			bool flag8 = msg.ContainsKey("random_damage");
			if (flag8)
			{
				int tuneid = msg["rune_id"];
				List<Variant> arr3 = msg["random_damage"]._arr;
				foreach (Variant current3 in arr3)
				{
					uint uint3 = current3["to_iid"]._uint;
					int int3 = current3["dmg"]._int;
					bool bool2 = current3["isdie"]._bool;
					int int4 = current3["hprest"]._int;
					uint uint4 = current3["frm_iid"]._uint;
					bool stagger2 = false;
					bool flag9 = current3.ContainsKey("stagger");
					if (flag9)
					{
						stagger2 = current3["stagger"];
					}
					BaseRole role3 = RoleMgr._instance.getRole(uint3);
					bool flag10 = role3 == null;
					if (flag10)
					{
						break;
					}
					BaseRole role4 = RoleMgr._instance.getRole(uint4);
					this.doHurt(role3, role4, int3, bool2, int4, -1, false, stagger2, 0u);
					GameObject runeEff2 = EffMgr.getRuneEff(tuneid);
					bool flag11 = runeEff2 != null;
					if (flag11)
					{
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(runeEff2);
						UnityEngine.Object.Destroy(gameObject2, 2f);
						gameObject2.transform.SetParent(role3.m_curModel, false);
					}
				}
			}
		}

		private void doHurt(BaseRole toRole, BaseRole frm, int damage, bool isdie, int hprest, int iscrit, bool miss, bool stagger, uint skill_id = 0u)
		{
			toRole.onServerHurt(damage, hprest, isdie, frm, iscrit, miss, stagger);
			bool flag = toRole.m_isMain && !miss;
			if (flag)
			{
				ModelBase<PlayerModel>.getInstance().modHp(hprest);
			}
			bool flag2 = frm != null;
			if (flag2)
			{
				bool flag3 = frm is M000P2 || frm is M000P3 || frm is M000P5 || frm is ohterP2Warrior || frm is ohterP3Mage || frm is ohterP5Assassin;
				if (flag3)
				{
					frm.PlaySkill((int)skill_id);
					frm.m_LockRole = toRole;
				}
			}
			bool flag4 = frm is ProfessionRole && toRole.m_isMain && SelfRole.fsm.Autofighting && ModelBase<AutoPlayModel>.getInstance().AutoPK > 0;
			if (flag4)
			{
				bool flag5 = StatePK.Instance.Enemy != frm && SelfRole.fsm.currentState != StatePK.Instance;
				if (flag5)
				{
					StatePK.Instance.Enemy = (ProfessionRole)frm;
					SelfRole.fsm.ChangeState(StatePK.Instance);
				}
			}
		}

		private void doHurt(Variant v)
		{
			uint @uint = v["to_iid"]._uint;
			int @int = v["dmg"]._int;
			bool @bool = v["isdie"]._bool;
			int int2 = v["hprest"]._int;
			uint uint2 = v["frm_iid"]._uint;
			uint uint3 = v["skill_id"]._uint;
			bool stagger = false;
			bool flag = v.ContainsKey("stagger");
			if (flag)
			{
				stagger = v["stagger"];
			}
			BaseRole role = RoleMgr._instance.getRole(@uint);
			bool flag2 = @bool;
			if (flag2)
			{
				bool flag3 = role is P5Assassin;
				if (flag3)
				{
					MediaClient.instance.PlaySoundUrl("audio/common/assassin_dead", false, null);
				}
				bool flag4 = role is P2Warrior;
				if (flag4)
				{
					MediaClient.instance.PlaySoundUrl("audio/common/warrior_dead", false, null);
				}
				bool flag5 = role is P3Mage;
				if (flag5)
				{
					MediaClient.instance.PlaySoundUrl("audio/common/mage_dead", false, null);
				}
			}
			bool flag6 = role == null;
			if (!flag6)
			{
				BaseRole role2 = RoleMgr._instance.getRole(uint2);
				bool flag7 = v.ContainsKey("invisible_first_atk") && role2 != null && role2.m_isMain;
				if (flag7)
				{
					bool flag8 = v["rune_id"];
					if (flag8)
					{
						int type = v["rune_id"];
						int num = v["invisible_first_atk"];
						FightText.play(FightText.IMG_TEXT, role.getHeadPos(), num, false, type);
					}
				}
				bool flag9 = role2 is MS0000;
				if (flag9)
				{
					debug.Log("伤害" + uint3);
				}
				bool flag10 = v.ContainsKey("hited");
				if (flag10)
				{
					switch (v["hited"])
					{
					case 0:
					{
						bool flag11 = role2 != null && (role.m_isMain || role2.m_isMain);
						if (flag11)
						{
							FightText.play(FightText.MISS_TEXT, role2.getHeadPos(), 0, false, -1);
						}
						break;
					}
					case 1:
						this.doHurt(role, role2, @int, @bool, int2, 1, false, stagger, uint3);
						break;
					case 2:
						this.doHurt(role, role2, @int, @bool, int2, 2, false, stagger, uint3);
						break;
					case 3:
						this.doHurt(role, role2, @int, @bool, int2, 3, false, stagger, uint3);
						break;
					case 4:
					{
						bool flag12 = v["rune_id"] && role2.m_isMain;
						if (flag12)
						{
							int type2 = v["rune_id"];
							FightText.play(FightText.IMG_TEXT, role.getHeadPos(), @int, false, type2);
						}
						else
						{
							bool flag13 = v["rune_id"] && role.m_isMain;
							if (flag13)
							{
								this.doHurt(role, role2, @int, @bool, int2, 4, false, stagger, uint3);
							}
						}
						break;
					}
					case 5:
						this.doHurt(role, role2, @int, @bool, int2, 5, false, stagger, uint3);
						break;
					}
				}
				else
				{
					this.doHurt(role, role2, @int, @bool, int2, -1, true, stagger, 0u);
				}
				bool isMain = role.m_isMain;
				if (isMain)
				{
					this.hudunModel.isNoAttack = false;
					bool flag14 = a3_herohead.instance != null;
					if (flag14)
					{
						a3_herohead.instance.wait_attack(this.hudunModel.noAttackTime);
					}
					bool flag15 = role2 is ProfessionRole;
					if (flag15)
					{
						bool flag16 = a3_lowblood.instance != null;
						if (flag16)
						{
							a3_lowblood.instance.begin();
						}
					}
				}
				bool flag17 = ModelBase<PlayerModel>.getInstance().treasure_num > 0u;
				if (flag17)
				{
					bool flag18 = role2 is ProfessionRole && role.m_isMain;
					if (flag18)
					{
						ModelBase<FindBestoModel>.getInstance().Canfly = false;
						bool flag19 = a3_herohead.instance != null;
						if (flag19)
						{
							a3_herohead.instance.wait_attack_baotu(ModelBase<FindBestoModel>.getInstance().waitTime);
						}
					}
				}
				bool flag20 = v.ContainsKey("dmg_shield");
				if (flag20)
				{
					FightText.play(FightText.SHEILD_TEXT, role.getHeadPos(), v["dmg_shield"], false, -1);
					this.onShield(role);
				}
				bool flag21 = v.ContainsKey("holy_shield");
				if (flag21)
				{
					bool isMain2 = role.m_isMain;
					if (isMain2)
					{
						this.hudunModel.NowCount = v["holy_shield"];
					}
					bool flag22 = v["holy_shield"] <= 0;
					if (flag22)
					{
						this.onShieldLost(role);
					}
				}
			}
		}

		private void onShield(BaseRole Role)
		{
			bool shieldFirst = this.ShieldFirst;
			if (shieldFirst)
			{
				GameObject gameObject = Resources.Load<GameObject>("FX/comFX/fx_player_common/FX_com_dun_ilde");
				bool flag = gameObject != null && this.shield_fx_clon == null;
				if (flag)
				{
					this.shield_fx_clon = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					this.shield_fx_clon.transform.SetParent(Role.m_curModel, false);
					bool flag2 = this.process == null;
					if (flag2)
					{
						this.process = new TickItem(new Action<float>(this.updata_Wite));
						TickMgr.instance.addTick(this.process);
						this.waitTime = 0f;
					}
					this.ShieldFirst = false;
				}
			}
		}

		private void updata_Wite(float s)
		{
			this.waitTime += s;
			bool flag = this.waitTime > this.shield_time;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.shield_fx_clon);
				this.waitTime = 0f;
				TickMgr.instance.removeTick(this.process);
				this.process = null;
				this.shield_fx_clon = null;
				this.ShieldFirst = true;
			}
		}

		private void onShieldLost(BaseRole Role)
		{
			GameObject gameObject = Resources.Load<GameObject>("FX/comFX/fx_player_common/FX_com_dun_broken");
			bool flag = gameObject != null;
			if (flag)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject2.transform.SetParent(Role.m_curModel, false);
				UnityEngine.Object.Destroy(gameObject2, 2f);
				bool flag2 = this.shield_fx_clon != null;
				if (flag2)
				{
					UnityEngine.Object.Destroy(this.shield_fx_clon);
				}
			}
		}

		private void on_bstate_change(Variant msgData)
		{
		}

		private void on_single_skill_res(Variant msgData)
		{
			debug.Log("UUUUUUU" + msgData.dump());
			uint @uint = msgData["to_iid"]._uint;
			uint uint2 = msgData["frm_iid"]._uint;
			BaseRole role = RoleMgr._instance.getRole(@uint);
			BaseRole role2 = RoleMgr._instance.getRole(uint2);
			bool flag = !msgData.ContainsKey("states");
			if (!flag)
			{
				bool flag2 = role != null && role.m_isMain;
				if (flag2)
				{
					Variant variant = msgData["states"];
					bool flag3 = msgData["sid"];
					if (flag3)
					{
						int type = msgData["sid"];
						FightText.play(FightText.BUFF_TEXT, role.getHeadPos(), 0, false, type);
					}
					ModelBase<A3_BuffModel>.getInstance().addBuffList(variant);
					bool flag4 = variant["id"] == 10000;
					if (flag4)
					{
						BaseProxy<A3_ActiveProxy>.getInstance().dispatchEvent(GameEvent.Create(A3_ActiveProxy.EVENT_ONBLESS, this, variant, false));
					}
				}
				else
				{
					ModelBase<A3_BuffModel>.getInstance().addOtherBuff(role, msgData["states"]["id"]);
				}
				bool flag5 = msgData.ContainsKey("states");
				if (flag5)
				{
					SXML sXML = XMLMgr.instance.GetSXML("skill.state", "id==" + msgData["states"]["id"]);
					string @string = sXML.getString("effect");
					bool flag6 = role2 is MonsterRole && (role2 as MonsterRole).issummon && @uint == uint2;
					if (flag6)
					{
						role2.PlaySkill(msgData["sid"]);
					}
					bool flag7 = @string != "null";
					if (flag7)
					{
						float @float = sXML.getFloat("last");
						GameObject original = Resources.Load<GameObject>(@string);
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
						gameObject.transform.SetParent(role.m_curModel, false);
						UnityEngine.Object.Destroy(gameObject, @float);
						bool flag8 = sXML.getFloat("head") > 0f;
						if (flag8)
						{
							gameObject.transform.localPosition = new Vector3(0f, role.headOffset_half.y / 2f + sXML.getFloat("head"), 0f);
						}
					}
				}
			}
		}

		private void on_add_state(Variant msgData)
		{
			debug.Log("添加buff" + msgData.dump());
			bool flag = msgData != null && msgData["iid"] != null;
			if (flag)
			{
				uint @uint = msgData["iid"]._uint;
				BaseRole role = RoleMgr._instance.getRole(@uint);
				bool flag2 = role != null;
				if (flag2)
				{
					bool isMain = role.m_isMain;
					if (isMain)
					{
						bool flag3 = !msgData.ContainsKey("states");
						if (!flag3)
						{
							foreach (Variant current in msgData["states"]._arr)
							{
								Variant variant = current;
								bool flag4 = variant["id"] == 10001 && a3_herohead.instance != null;
								if (flag4)
								{
									this.end_tm = variant["end_tm"];
									this.star_tm = variant["start_tm"];
									a3_herohead.instance.exp_time = this.end_tm - muNetCleint.instance.CurServerTimeStamp;
								}
								ModelBase<A3_BuffModel>.getInstance().addBuffList(variant);
							}
						}
					}
				}
				else
				{
					foreach (Variant current2 in msgData["states"]._arr)
					{
						ModelBase<A3_BuffModel>.getInstance().addOtherBuff(role, current2["id"]);
					}
				}
			}
		}

		private void on_die(Variant msgData)
		{
			uint @uint = msgData["iid"]._uint;
			BaseRole role = RoleMgr._instance.getRole(@uint);
			bool flag = role == null;
			if (!flag)
			{
				this.doHurt(role, null, 0, true, -1, -1, false, false, 0u);
			}
		}

		private void on_cast_skill_res(Variant msgData)
		{
			bool flag = msgData["res"] == -768;
			if (flag)
			{
				SelfRole._inst.m_curModel.position = (SelfRole._inst.m_roleDta.pos = BaseProxy<MoveProxy>.getInstance().GetLastSendXY() / 53.3f + new Vector3(0f, SelfRole._inst.m_roleDta.pos.y, 0f));
			}
		}

		public void grtCoumy()
		{
		}

		private void on_cancel_casting_skill_res(Variant msgData)
		{
		}

		private void on_rmv_state(Variant msgData)
		{
			debug.Log("移除buff" + msgData.dump());
			uint @uint = msgData["iid"]._uint;
			BaseRole role = RoleMgr._instance.getRole(@uint);
			bool flag = role != null && role.m_isMain;
			if (flag)
			{
				using (List<Variant>.Enumerator enumerator = msgData["ids"]._arr.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						uint id = enumerator.Current;
						ModelBase<A3_BuffModel>.getInstance().RemoveBuff(id);
					}
				}
			}
			else
			{
				using (List<Variant>.Enumerator enumerator2 = msgData["ids"]._arr.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						uint id2 = enumerator2.Current;
						ModelBase<A3_BuffModel>.getInstance().removeOtherBuff(role, id2);
					}
				}
			}
		}
	}
}
