using GameFramework;
using MuGame;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class SelfRole
{
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		public static readonly SelfRole.<>c <>9 = new SelfRole.<>c();

		public static Action<cd> <>9__30_0;

		internal void <Transmit>b__30_0(cd cdt)
		{
			int num = (int)(cd.secCD - cd.lastCD) / 100;
			cdt.txt.text = ContMgr.getCont("worldmap_cd", new string[]
			{
				((float)num / 10f).ToString()
			});
		}
	}

	public static ProfessionRole _inst;

	public static Transform s_LockFX;

	public static Transform s_LockFX_boss;

	public static bool s_bStandaloneScene;

	public static bool s_bInTransmit;

	private static int lastNpcId;

	private static StateMachine _fsm;

	public static List<MapLinkInfo> line;

	public static bool UnderPlayerAttack
	{
		get;
		set;
	}

	public static bool UnderTaskAutoMove
	{
		get;
		set;
	}

	public static ProfessionRole LastAttackPlayer
	{
		get;
		set;
	}

	public static StateMachine fsm
	{
		get
		{
			bool flag = SelfRole._fsm == null;
			if (flag)
			{
				SelfRole._fsm = new StateMachine();
				SelfRole._fsm.Configure(StateInit.Instance, StateGlobal.Instance, StateProxy.Instance);
			}
			return SelfRole._fsm;
		}
	}

	public static void moveto(Vector3 pos, Action handle = null, bool forcefacetopos = false, float dis = 0.3f, bool forceStop = true)
	{
		bool flag = SelfRole._inst == null;
		if (!flag)
		{
			bool flag2 = !SelfRole._inst.canMove;
			if (!flag2)
			{
				if (forceStop)
				{
					SelfRole.fsm.Stop();
				}
				StateAutoMoveToPos.Instance.handle = handle;
				StateAutoMoveToPos.Instance.pos = pos;
				StateAutoMoveToPos.Instance.forceFaceToPos = forcefacetopos;
				StateAutoMoveToPos.Instance.stopdistance = dis;
				SelfRole.fsm.ChangeState(StateAutoMoveToPos.Instance);
			}
		}
	}

	public static void moveto(int id, Vector3 pos, Action handle = null, float stopDis = 0.3f)
	{
		bool flag = id == GRMap.instance.m_nCurMapID;
		if (flag)
		{
			SelfRole.moveto(pos, handle, false, stopDis, true);
		}
		else
		{
			SelfRole.line = new List<MapLinkInfo>();
			bool flag2 = !worldmap.getMapLine(GRMap.instance.m_nCurMapID, id, SelfRole.line, null);
			if (!flag2)
			{
				StateMoveLine.Instance.handle = handle;
				StateMoveLine.Instance.line = SelfRole.line;
				StateMoveLine.Instance.pos = pos;
				SelfRole.fsm.ChangeState(StateMoveLine.Instance);
			}
		}
	}

	public static void moveToNPc(int mapid, int npcId, List<string> listTalk = null, Action handle = null)
	{
		bool onlyOne = true;
		Vector3 pos = Vector3.zero;
		NpcRole role = NpcMgr.instance.getRole(npcId);
		bool flag = role != null;
		if (flag)
		{
			bool autofighting = SelfRole.fsm.Autofighting;
			if (autofighting)
			{
				SelfRole.fsm.Stop();
			}
			Vector3 position = role.transform.position;
			float num = Vector3.Distance(position, SelfRole._inst.m_curModel.transform.position);
			bool flag2 = num > 2f || SelfRole.lastNpcId != npcId || SelfRole.lastNpcId == 0;
			if (flag2)
			{
				pos = position;
			}
			SelfRole.lastNpcId = npcId;
		}
		Action <>9__1;
		SelfRole.moveto(mapid, pos, delegate
		{
			DoAfterMgr arg_25_0 = DoAfterMgr.instacne;
			Action arg_25_1;
			if ((arg_25_1 = <>9__1) == null)
			{
				arg_25_1 = (<>9__1 = delegate
				{
					bool onlyOne = onlyOne;
					if (onlyOne)
					{
						onlyOne = false;
						NpcRole n = NpcMgr.instance.getRole(npcId);
						bool flag3 = n == null;
						if (!flag3)
						{
							bool flag4 = Vector3.Distance(n.transform.position, SelfRole._inst.m_curModel.transform.position) > 2f;
							if (flag4)
							{
								SelfRole.moveto(n.transform.position, delegate
								{
									bool flag6 = listTalk != null;
									if (flag6)
									{
										n.newDesc = listTalk;
									}
									n.handle = handle;
									n.onClick();
								}, false, 1.5f, true);
							}
							else
							{
								bool flag5 = listTalk != null;
								if (flag5)
								{
									n.newDesc = listTalk;
								}
								n.handle = handle;
								n.onClick();
							}
						}
					}
				});
			}
			arg_25_0.addAfterRender(arg_25_1);
		}, 2f);
	}

	public static void FrameMove(float delta_time)
	{
		bool flag = SelfRole._inst != null;
		if (flag)
		{
			bool flag2 = false;
			try
			{
				BaseRole expr_1E = SelfRole._inst.m_LockRole;
				Transform transform = (expr_1E != null) ? expr_1E.m_curModel : null;
				flag2 = (transform != null && transform);
			}
			catch (Exception var_3_3B)
			{
				SelfRole._inst.m_LockRole = null;
				flag2 = false;
			}
			bool flag3 = flag2;
			if (flag3)
			{
				bool flag4 = SelfRole.s_LockFX != null && SelfRole._inst.m_LockRole.m_circle_type == -1;
				if (flag4)
				{
					PkmodelAdmin.RefreshShow(SelfRole._inst.m_LockRole, false, false);
					SelfRole.s_LockFX.position = SelfRole._inst.m_LockRole.m_curModel.position;
					bool isDead = SelfRole._inst.m_LockRole.isDead;
					if (isDead)
					{
						SelfRole.s_LockFX.position = Vector3.zero;
					}
				}
				bool flag5 = SelfRole.s_LockFX_boss != null && SelfRole._inst.m_LockRole.m_circle_type == 1;
				if (flag5)
				{
					SelfRole.s_LockFX_boss.position = SelfRole._inst.m_LockRole.m_curPhy.position;
					bool isDead2 = SelfRole._inst.m_LockRole.isDead;
					if (isDead2)
					{
						SelfRole.s_LockFX_boss.position = Vector3.zero;
					}
					float circle_scale = SelfRole._inst.m_LockRole.m_circle_scale;
					SelfRole.s_LockFX_boss.localScale = new Vector3(circle_scale, circle_scale, circle_scale);
				}
			}
			else
			{
				bool flag6 = SelfRole.s_LockFX != null;
				if (flag6)
				{
					SelfRole.s_LockFX.position = Vector3.zero;
				}
				bool flag7 = SelfRole.s_LockFX_boss != null;
				if (flag7)
				{
					SelfRole.s_LockFX_boss.position = Vector3.zero;
				}
			}
			SelfRole._inst.FrameMove(delta_time);
			BaseProxy<MoveProxy>.getInstance().TrySyncPos(delta_time);
			try
			{
				long curServerTimeStampMS = muNetCleint.instance.g_netM.CurServerTimeStampMS;
				Vector3 vector = SelfRole._inst.m_curModel.position;
				bool moving = SelfRole._inst.moving;
				if (moving)
				{
					float num = Mathf.Atan2(joystick.instance.MovePosiNorm.z, joystick.instance.MovePosiNorm.x) * 57.29578f;
					num = SceneCamera.m_curCamGo.transform.eulerAngles.y - num;
					vector = Quaternion.Euler(0f, num, 0f) * new Vector3(1f, 0f, 0f) + vector;
					NavMeshHit navMeshHit;
					NavMesh.SamplePosition(vector, out navMeshHit, 3f, NavmeshUtils.allARE);
					vector = navMeshHit.position;
				}
				float num2 = vector.x * 53.333f;
				float num3 = vector.z * 53.333f;
				BaseProxy<MoveProxy>.getInstance().sendstop((uint)num2, (uint)num3, 1u, (float)curServerTimeStampMS, false);
			}
			catch (Exception ex)
			{
				Debug.Log(ex.ToString());
			}
			SelfRole.fsm.Update(delta_time);
			bool flag8 = SelfRole._inst.m_LockRole != null;
			if (flag8)
			{
				try
				{
					float magnitude = (SelfRole._inst.m_LockRole.m_curPhy.position - SelfRole._inst.m_curModel.position).magnitude;
					bool flag9 = magnitude > SelfRole._inst.m_LockDis;
					if (flag9)
					{
						SelfRole._inst.m_LockRole = null;
					}
				}
				catch (Exception)
				{
					SelfRole._inst.m_LockRole = null;
				}
			}
		}
	}

	public static void Init()
	{
		GameObject original = Resources.Load<GameObject>("default/lock_fx");
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
		SelfRole.s_LockFX = gameObject.transform;
		GameObject original2 = Resources.Load<GameObject>("default/lock_fx_boss");
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original2);
		SelfRole.s_LockFX_boss = gameObject2.transform;
		int profession = ModelBase<PlayerModel>.getInstance().profession;
		bool flag = profession == 2;
		if (flag)
		{
			FightText.userText = FightText.WARRIOR_TEXT;
		}
		else
		{
			bool flag2 = profession == 3;
			if (flag2)
			{
				FightText.userText = FightText.MAGE_TEXT;
			}
			else
			{
				bool flag3 = profession == 5;
				if (flag3)
				{
					FightText.userText = FightText.ASSI_TEXT;
				}
			}
		}
		GameObject gameObject3 = GameObject.Find("mainbronpt");
		bool flag4 = gameObject3 != null;
		if (flag4)
		{
			ModelBase<PlayerModel>.getInstance().mapBeginX = gameObject3.transform.position.x;
			ModelBase<PlayerModel>.getInstance().mapBeginY = gameObject3.transform.position.z;
			ModelBase<PlayerModel>.getInstance().mapBeginroatate = gameObject3.transform.eulerAngles.y;
		}
		A3_PROFESSION profession2 = (A3_PROFESSION)ModelBase<PlayerModel>.getInstance().profession;
		bool flag5 = A3_PROFESSION.Warrior == profession2;
		ProfessionRole professionRole;
		if (flag5)
		{
			P2Warrior p2Warrior = new P2Warrior();
			p2Warrior.m_bUserSelf = true;
			p2Warrior.Init(ModelBase<PlayerModel>.getInstance().name, EnumLayer.LM_SELFROLE, Vector3.zero, true);
			professionRole = p2Warrior;
		}
		else
		{
			bool flag6 = A3_PROFESSION.Mage == profession2;
			if (flag6)
			{
				P3Mage p3Mage = new P3Mage();
				p3Mage.m_bUserSelf = true;
				p3Mage.Init(ModelBase<PlayerModel>.getInstance().name, EnumLayer.LM_SELFROLE, Vector3.zero, true);
				professionRole = p3Mage;
			}
			else
			{
				bool flag7 = A3_PROFESSION.Assassin == profession2;
				if (flag7)
				{
					P5Assassin p5Assassin = new P5Assassin();
					p5Assassin.m_bUserSelf = true;
					p5Assassin.Init(ModelBase<PlayerModel>.getInstance().name, EnumLayer.LM_SELFROLE, Vector3.zero, true);
					professionRole = p5Assassin;
				}
				else
				{
					P3Mage p3Mage2 = new P3Mage();
					p3Mage2.m_bUserSelf = true;
					p3Mage2.Init(ModelBase<PlayerModel>.getInstance().name, EnumLayer.LM_SELFROLE, Vector3.zero, true);
					professionRole = p3Mage2;
				}
			}
		}
		professionRole.m_ePK_Type = ((ModelBase<PlayerModel>.getInstance().up_lvl < 1u) ? PK_TYPE.PK_PEACE : PK_TYPE.PK_PKALL);
		professionRole.m_unIID = ModelBase<PlayerModel>.getInstance().iid;
		professionRole.m_unPK_Param = ModelBase<PlayerModel>.getInstance().cid;
		professionRole.refreshmapCount((int)ModelBase<PlayerModel>.getInstance().treasure_num);
		professionRole.refreshserialCount(ModelBase<PlayerModel>.getInstance().serial);
		professionRole.serial = ModelBase<PlayerModel>.getInstance().serial;
		professionRole.refreshVipLvl((uint)ModelBase<A3_VipModel>.getInstance().Level);
		professionRole.m_curPhy.gameObject.layer = EnumLayer.LM_BT_SELF;
		SelfRole._inst = professionRole;
		Transform dEF_TRANSFORM = U3DAPI.DEF_TRANSFORM;
		dEF_TRANSFORM.position = professionRole.m_curModel.position;
		GameObject gameObject4 = GameObject.Find("game_scene/lv3/raffle");
		bool flag8 = gameObject4 != null;
		if (flag8)
		{
			for (int i = 0; i < gameObject4.transform.childCount; i++)
			{
				Transform child = gameObject4.transform.GetChild(i);
				Transform transform = child.Find("whole");
				Transform transform2 = child.Find("boom");
				bool flag9 = transform != null && transform2 != null;
				if (flag9)
				{
					RaffleHurtPoint raffleHurtPoint = child.gameObject.AddComponent<RaffleHurtPoint>();
					raffleHurtPoint.m_Raffle = child.gameObject;
					raffleHurtPoint.m_RafWhole = transform.gameObject;
					raffleHurtPoint.m_RafBoom = transform2.gameObject;
					child.gameObject.layer = EnumLayer.LM_BT_FIGHT;
				}
				else
				{
					UnityEngine.Object.Destroy(child.gameObject);
				}
			}
		}
		GameObject gameObject5 = GameObject.Find("game_scene/lv3/btwall");
		bool flag10 = gameObject5 != null;
		if (flag10)
		{
			for (int j = 0; j < gameObject5.transform.childCount; j++)
			{
				Transform child2 = gameObject5.transform.GetChild(j);
				child2.gameObject.AddComponent<BtWallHurtPoint>();
				child2.gameObject.layer = EnumLayer.LM_BT_FIGHT;
			}
		}
		foreach (a3_BagItemData current in ModelBase<a3_EquipModel>.getInstance().getEquips().Values)
		{
			ModelBase<a3_EquipModel>.getInstance().equipModel_on(current);
		}
		ModelBase<A3_WingModel>.getInstance().OnEquipModelChange();
		bool flag11 = ModelBase<PlayerModel>.getInstance().last_time != 0;
		if (flag11)
		{
			SelfRole.OnPetAvatarChange();
		}
		BaseProxy<A3_PetProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_GET_PET, new Action<GameEvent>(SelfRole.OnPetHaveChange));
		BaseProxy<A3_PetProxy>.getInstance().addEventListener(A3_PetProxy.EVENT_HAVE_PET, new Action<GameEvent>(SelfRole.showPet));
		MonsterMgr._inst.dispatchEvent(GameEvent.Create(MonsterMgr.EVENT_ROLE_BORN, MonsterMgr._inst, SelfRole._inst, false));
		BaseProxy<MoveProxy>.getInstance().resetFirstMove();
		SelfRole._inst.m_LockDis = XMLMgr.instance.GetSXML("comm.lockdis", "").getFloat("dis");
	}

	private static void OnPetAvatarChange()
	{
		A3_PetModel instance = ModelBase<A3_PetModel>.getInstance();
		bool flag = !ModelBase<PlayerModel>.getInstance().havePet;
		if (!flag)
		{
			SelfRole._inst.ChangePetAvatar((int)instance.Tpid, 0);
			debug.Log(instance.Tpid + "::::tpid:::::::::::::::::");
		}
	}

	private static void OnPetHaveChange(GameEvent e)
	{
		A3_PetModel instance = ModelBase<A3_PetModel>.getInstance();
		bool flag = !e.data["hava_pet"] || ModelBase<PlayerModel>.getInstance().last_time == 0;
		if (!flag)
		{
			ModelBase<PlayerModel>.getInstance().havePet = e.data["hava_pet"];
			instance.Stage = 0;
			SelfRole._inst.ChangePetAvatar(e.data["pet"]["id"], 0);
		}
	}

	private static void showPet(GameEvent e)
	{
		bool flag = e.data["iid"] != ModelBase<PlayerModel>.getInstance().iid;
		if (flag)
		{
			bool flag2 = e.data["pet_food_last_time"] == 0;
			if (flag2)
			{
				OtherPlayerMgr._inst.PlayPetAvatarChange(e.data["iid"], 0, 0);
				debug.Log("饲料到期");
			}
			else
			{
				OtherPlayerMgr._inst.PlayPetAvatarChange(e.data["iid"], e.data["pet_id"], 0);
			}
		}
		else
		{
			SelfRole._inst.ChangePetAvatar(0, 0);
			flytxt.instance.fly(ContMgr.getCont("pet_no_feed", null), 0, default(Color), null);
			debug.Log("饲料到期");
		}
	}

	public static void Transmit(int toid, Action after = null, bool transmit = false, bool ontask = false)
	{
		bool flag = !ModelBase<FindBestoModel>.getInstance().Canfly;
		if (flag)
		{
			flytxt.instance.fly(ModelBase<FindBestoModel>.getInstance().nofly_txt, 0, default(Color), null);
		}
		else
		{
			Action<cd> arg_6B_0;
			if ((arg_6B_0 = SelfRole.<>c.<>9__30_0) == null)
			{
				arg_6B_0 = (SelfRole.<>c.<>9__30_0 = new Action<cd>(SelfRole.<>c.<>9.<Transmit>b__30_0));
			}
			cd.updateHandle = arg_6B_0;
			GameObject goeff = UnityEngine.Object.Instantiate<GameObject>(worldmap.EFFECT_CHUANSONG1);
			goeff.transform.SetParent(SelfRole._inst.m_curModel, false);
			SelfRole.s_bInTransmit = true;
			Action <>9__2;
			cd.show(delegate
			{
				BaseProxy<MapProxy>.getInstance().sendBeginChangeMap(toid, true, false);
				DoAfterMgr arg_38_0 = DoAfterMgr.instacne;
				Action arg_38_1;
				if ((arg_38_1 = <>9__2) == null)
				{
					arg_38_1 = (<>9__2 = delegate
					{
						Action expr_07 = after;
						if (expr_07 != null)
						{
							expr_07();
						}
					});
				}
				arg_38_0.addAfterRender(arg_38_1);
				SelfRole.s_bInTransmit = false;
			}, 2.8f, false, delegate
			{
				UnityEngine.Object.Destroy(goeff);
				SelfRole.s_bInTransmit = false;
			}, default(Vector3));
		}
	}

	public static void WalkToMap(int id, Vector3 vec, Action handle = null, float stopDis = 0.3f)
	{
		bool autofighting = SelfRole.fsm.Autofighting;
		if (autofighting)
		{
			SelfRole.fsm.Stop();
		}
		bool flag = id != GRMap.instance.m_nCurMapID;
		if (flag)
		{
			SelfRole.moveto(id, vec, handle, stopDis);
		}
		else
		{
			NavMeshHit navMeshHit;
			NavMesh.SamplePosition(vec, out navMeshHit, 20f, NavmeshUtils.allARE);
			SelfRole.moveto(navMeshHit.position, handle, false, stopDis, true);
		}
	}

	static SelfRole()
	{
		// 注意: 此类型已标记为 'beforefieldinit'.
		SelfRole.<UnderPlayerAttack>k__BackingField = false;
		SelfRole.<UnderTaskAutoMove>k__BackingField = false;
		SelfRole.<LastAttackPlayer>k__BackingField = null;
		SelfRole.s_bStandaloneScene = false;
		SelfRole.s_bInTransmit = false;
		SelfRole._fsm = null;
	}
}
