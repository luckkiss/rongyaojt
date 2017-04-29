using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MuGame
{
	internal class skillbar : FloatUi
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly skillbar.<>c <>9 = new skillbar.<>c();

			public static Action <>9__39_0;

			public static Func<ProfessionRole, bool> <>9__41_0;

			public static Func<ProfessionRole, bool> <>9__41_1;

			public static Func<MonsterRole, bool> <>9__41_3;

			internal void <OnFSMStartStop>b__39_0()
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_AUTOPLAY2, null, false);
			}

			internal bool <OnChangeLock>b__41_0(ProfessionRole otherPlayer)
			{
				return otherPlayer.m_isMarked;
			}

			internal bool <OnChangeLock>b__41_1(ProfessionRole otherPlayer)
			{
				skillbar.<>c__DisplayClass41_0 <>c__DisplayClass41_ = new skillbar.<>c__DisplayClass41_0();
				<>c__DisplayClass41_.otherPlayer = otherPlayer;
				bool arg_44_0;
				if (!<>c__DisplayClass41_.otherPlayer.m_isMarked)
				{
					List<ItemTeamData> expr_29 = BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList;
					arg_44_0 = (expr_29 != null && expr_29.Exists(new Predicate<ItemTeamData>(<>c__DisplayClass41_.<OnChangeLock>b__2)));
				}
				else
				{
					arg_44_0 = true;
				}
				return arg_44_0;
			}

			internal bool <OnChangeLock>b__41_3(MonsterRole mon)
			{
				return mon.m_isMarked;
			}
		}

		private List<SKillCdBt> vecSkillBt;

		public static skillbar instance;

		public static int NORNAL_SKILL_ID = 0;

		public static long lastNormalSkillTimer = 0L;

		public int skillsetIdx = 1;

		private BaseButton hp_Add_btn;

		private Text hpNum_text;

		private Image cd_image;

		private Transform tfCombat;

		private BaseButton btnBuff;

		private Transform transChangeLock;

		private Text txtBuff;

		private BaseButton btLeaveNewbie;

		private BaseButton btChange;

		private Animator ani;

		private Animator on_5008_1;

		private Animator on_5008_2;

		private GameObject on_5008_3;

		public int curTick = 0;

		public static bool Cd_lose = true;

		public static bool canClick = false;

		public override void init()
		{
			base.alain();
			skillbar.instance = this;
			this.vecSkillBt = new List<SKillCdBt>();
			this.btnBuff = new BaseButton(base.getTransformByPath("btnBuff"), 1, 1);
			this.txtBuff = base.getTransformByPath("btnBuff/Text").GetComponent<Text>();
			this.btnBuff.onClick = new Action<GameObject>(this.onBtnBuffClick);
			this.tfCombat = base.getTransformByPath("combat");
			this.btChange = new BaseButton(base.getTransformByPath("combat/btn_change"), 1, 1);
			this.btChange.onClick = new Action<GameObject>(this.onChange);
			this.btLeaveNewbie = new BaseButton(base.getTransformByPath("leavenewbie"), 1, 1);
			this.btLeaveNewbie.onClick = new Action<GameObject>(this.onLeaveNewbie);
			for (int i = 0; i < 5; i++)
			{
				this.vecSkillBt.Add(new SKillCdBt(base.getTransformByPath("combat/bt" + i), i));
			}
			this.refreshAllSkills(SelfRole.s_bStandaloneScene ? 0 : -1);
			base.getComponentByPath<Button>("combat/apbtn").onClick.AddListener(new UnityAction(this.OnSwitchAutoPlay));
			StateMachine expr_143 = SelfRole.fsm;
			expr_143.OnFSMStartStop = (Action<bool>)Delegate.Combine(expr_143.OnFSMStartStop, new Action<bool>(this.OnFSMStartStop));
			this.hp_Add_btn = new BaseButton(base.getTransformByPath("combat/hpbtn"), 1, 1);
			this.hp_Add_btn.onClick = new Action<GameObject>(this.Add_Hp);
			this.hpNum_text = base.getComponentByPath<Text>("combat/hpbtn/num");
			this.updata_hpNum();
			this.cd_image = base.getComponentByPath<Image>("combat/hpbtn/icon/cd");
			this.on_5008_1 = base.transform.FindChild("combat").GetComponent<Animator>();
			this.on_5008_2 = base.transform.FindChild("ani_dsp").GetComponent<Animator>();
			this.on_5008_3 = base.transform.FindChild("ani_hold").gameObject;
			this.ani = base.transform.GetComponent<Animator>();
			base.transform.FindChild("ani_dsp").SetParent(base.transform.FindChild("combat"), false);
			base.transform.FindChild("ani_hold").SetParent(base.transform.FindChild("combat"), false);
			this.transChangeLock = this.tfCombat.FindChild("bt_changeLock");
			new BaseButton(this.transChangeLock, 1, 1).onClick = new Action<GameObject>(this.OnChangeLock);
			base.transform.FindChild("combat/mark1").gameObject.SetActive(true);
			base.transform.FindChild("combat/mark2").gameObject.SetActive(false);
			bool flag = GRMap.curSvrConf != null && GRMap.curSvrConf["id"] == 10;
			if (flag)
			{
				this.ShowCombatUI(false);
			}
			else
			{
				this.ShowCombatUI(true);
			}
		}

		public override void onShowed()
		{
			bool isyinsh = MapProxy.isyinsh;
			if (isyinsh)
			{
				this.forSkill_5008(MapProxy.isyinsh);
			}
			bool flag = GRMap.instance.m_nCurMapID == 3333;
			if (flag)
			{
				GameObject expr_36 = base.getGameObjectByPath("combat/apbtn");
				if (expr_36 != null)
				{
					expr_36.SetActive(false);
				}
			}
		}

		private void onBtnBuffClick(GameObject go)
		{
			BaseProxy<welfareProxy>.getInstance().sendWelfare(welfareProxy.ActiveType.onLineTimeAward, 4294967295u);
		}

		private void onLeaveNewbie(GameObject go)
		{
			this.btLeaveNewbie.interactable = false;
			ModelBase<PlayerModel>.getInstance().LeaveStandalone_CreateChar();
		}

		public void onoffAni(bool onoff)
		{
			this.ani.SetBool("onoff", onoff);
		}

		public void onChange(GameObject go)
		{
			bool flag = this.skillsetIdx == 1;
			if (flag)
			{
				base.transform.FindChild("combat/mark1").gameObject.SetActive(false);
				base.transform.FindChild("combat/mark2").gameObject.SetActive(true);
				this.skillsetIdx = 2;
			}
			else
			{
				base.transform.FindChild("combat/mark1").gameObject.SetActive(true);
				base.transform.FindChild("combat/mark2").gameObject.SetActive(false);
				this.skillsetIdx = 1;
			}
			this.refreshAllSkills(-1);
		}

		public void refreshAllSkills(int fakeType = -1)
		{
			skillbar.NORNAL_SKILL_ID = ModelBase<PlayerModel>.getInstance().profession * 1000 + 1;
			this.refreSkill(0, ModelBase<Skill_a3Model>.getInstance().skilldic[skillbar.NORNAL_SKILL_ID]);
			bool flag = fakeType >= 0;
			if (flag)
			{
				this.btChange.transform.GetComponent<Button>().interactable = false;
				this.btLeaveNewbie.gameObject.SetActive(true);
				for (int i = 1; i < 5; i++)
				{
					this.refreSkill(i, null);
				}
				bool flag2 = fakeType > 3;
				if (flag2)
				{
					bool flag3 = 2 == ModelBase<PlayerModel>.getInstance().profession;
					if (flag3)
					{
						this.refreSkill(4, ModelBase<Skill_a3Model>.getInstance().skilldic[skillbar.NORNAL_SKILL_ID + 5]);
					}
					else
					{
						this.refreSkill(4, ModelBase<Skill_a3Model>.getInstance().skilldic[skillbar.NORNAL_SKILL_ID + 4]);
					}
				}
				bool flag4 = fakeType > 2;
				if (flag4)
				{
					this.refreSkill(3, ModelBase<Skill_a3Model>.getInstance().skilldic[skillbar.NORNAL_SKILL_ID + 3]);
				}
				bool flag5 = fakeType > 1;
				if (flag5)
				{
					this.refreSkill(2, ModelBase<Skill_a3Model>.getInstance().skilldic[skillbar.NORNAL_SKILL_ID + 2]);
				}
				bool flag6 = fakeType > 0;
				if (flag6)
				{
					bool flag7 = 3 == ModelBase<PlayerModel>.getInstance().profession;
					if (flag7)
					{
						this.refreSkill(1, ModelBase<Skill_a3Model>.getInstance().skilldic[skillbar.NORNAL_SKILL_ID + 6]);
					}
					else
					{
						this.refreSkill(1, ModelBase<Skill_a3Model>.getInstance().skilldic[skillbar.NORNAL_SKILL_ID + 1]);
					}
				}
			}
			else
			{
				this.btChange.transform.GetComponent<Button>().interactable = true;
				this.btLeaveNewbie.gameObject.SetActive(false);
				int[] array = (this.skillsetIdx == 1) ? ModelBase<Skill_a3Model>.getInstance().idsgroupone : ModelBase<Skill_a3Model>.getInstance().idsgrouptwo;
				int num = 1;
				int[] array2 = array;
				for (int j = 0; j < array2.Length; j++)
				{
					int num2 = array2[j];
					bool flag8 = num2 > 0;
					if (flag8)
					{
						this.refreSkill(num, ModelBase<Skill_a3Model>.getInstance().skilldic[num2]);
					}
					else
					{
						this.refreSkill(num, null);
					}
					num++;
				}
			}
			bool isyinsh = MapProxy.isyinsh;
			if (isyinsh)
			{
				this.forSkill_5008(MapProxy.isyinsh);
				SelfRole._inst.invisible = MapProxy.isyinsh;
			}
		}

		public void forSkill_5008(bool b)
		{
		}

		public void refreSkill(int idx, skill_a3Data d)
		{
			this.vecSkillBt[idx].data = d;
		}

		public int getEmptyfreSkill()
		{
			int num = -1;
			for (int i = 1; i < this.vecSkillBt.Count; i++)
			{
				bool flag = this.vecSkillBt[i].data == null && num <= 0;
				if (flag)
				{
					num = i;
					break;
				}
			}
			return num;
		}

		private void Add_Hp(GameObject go)
		{
			ModelBase<a3_BagModel>.getInstance().useItemByTpid(1533u, 1);
		}

		public void updata_hpNum()
		{
			bool flag = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(1533u) > 9999;
			if (flag)
			{
				this.hpNum_text.text = "9999";
			}
			else
			{
				this.hpNum_text.text = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(1533u).ToString();
			}
		}

		public void refreSkill(int idx, int skillid)
		{
			this.vecSkillBt[idx].data = ModelBase<Skill_a3Model>.getInstance().skilldic[skillid];
			List<int> list = new List<int>();
			for (int i = 0; i < 4; i++)
			{
				list.Add(ModelBase<Skill_a3Model>.getInstance().idsgroupone[i]);
			}
			list.AddRange(ModelBase<Skill_a3Model>.getInstance().idsgrouptwo);
			BaseProxy<Skill_a3Proxy>.getInstance().sendProxy(0, list);
		}

		private void Update()
		{
			bool flag = SelfRole._inst == null || muNetCleint.instance == null;
			if (!flag)
			{
				this.updata_hpNum();
				bool flag2 = ModelBase<a3_BagModel>.getInstance().getItemCds().Count > 0 && ModelBase<a3_BagModel>.getInstance() != null;
				if (flag2)
				{
					foreach (int current in ModelBase<a3_BagModel>.getInstance().getItemCds().Keys)
					{
						bool flag3 = current == 4;
						if (flag3)
						{
							bool flag4 = ModelBase<a3_BagModel>.getInstance().getItemCds()[current] <= 0f;
							if (flag4)
							{
								this.cd_image.fillAmount = 0f;
							}
							else
							{
								this.cd_image.fillAmount = ModelBase<a3_BagModel>.getInstance().getItemCds()[current] / ModelBase<a3_BagModel>.getInstance().getItemDataById(1533u).cd_time;
							}
						}
					}
				}
				bool flag5 = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
				bool mouseButtonDown = Input.GetMouseButtonDown(0);
				bool flag6 = false;
				Vector3 position = Vector3.zero;
				bool flag7 = mouseButtonDown || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
				if (flag7)
				{
					bool flag8 = flag5 && EventSystem.current.IsPointerOverGameObject();
					if (flag8)
					{
						return;
					}
					bool flag9 = !flag5 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
					if (flag9)
					{
						return;
					}
					bool flag10 = mouseButtonDown;
					if (flag10)
					{
						flag6 = true;
						position = Input.mousePosition;
					}
				}
				bool flag11 = flag6;
				if (flag11)
				{
					Ray ray = SceneCamera.m_curCamera.ScreenPointToRay(position);
					RaycastHit raycastHit;
					bool flag12 = Physics.Raycast(ray, out raycastHit, 65535f, (1 << EnumLayer.LM_BT_FIGHT) + (1 << EnumLayer.LM_NPC));
					if (flag12)
					{
						SelfRole._inst.m_LockRole = OtherPlayerMgr._inst.FindWhoPhy(raycastHit.collider.transform);
						bool flag13 = SelfRole._inst.m_LockRole == null;
						if (flag13)
						{
							SelfRole._inst.m_LockRole = MonsterMgr._inst.FindWhoPhy(raycastHit.collider.transform, true);
							bool flag14 = SelfRole._inst.m_LockRole != null;
							if (flag14)
							{
								MonsterRole monsterRole = SelfRole._inst.m_LockRole as MonsterRole;
								monsterRole.onClick();
							}
						}
						bool flag15 = SelfRole._inst.m_LockRole == null;
						if (flag15)
						{
							bool flag16 = !skillbar.canClick;
							if (flag16)
							{
								skillbar.canClick = true;
								NpcRole npc = raycastHit.collider.transform.GetComponent<NpcRole>();
								bool flag17 = npc != null;
								if (flag17)
								{
									bool flag18 = Vector3.Distance(npc.transform.position, SelfRole._inst.m_curModel.transform.position) > 2f;
									if (flag18)
									{
										SelfRole.moveto(npc.transform.position, delegate
										{
											npc.onClick();
										}, true, 2f, true);
									}
									else
									{
										npc.onClick();
									}
								}
							}
						}
					}
				}
				long curServerTimeStampMS = muNetCleint.instance.CurServerTimeStampMS;
				this.vecSkillBt[0].update(curServerTimeStampMS);
				this.vecSkillBt[1].update(curServerTimeStampMS);
				this.vecSkillBt[2].update(curServerTimeStampMS);
				this.vecSkillBt[3].update(curServerTimeStampMS);
				this.vecSkillBt[4].update(curServerTimeStampMS);
				int curSkillId = SelfRole._inst.m_curSkillId;
				bool flag19 = this.vecSkillBt[0].m_nCurDownID != 0 && curSkillId == skillbar.NORNAL_SKILL_ID;
				if (flag19)
				{
					SelfRole._inst.m_fAttackCount = 0.5f;
					bool flag20 = skillbar.lastNormalSkillTimer == 0L;
					if (flag20)
					{
						skillbar.lastNormalSkillTimer = curServerTimeStampMS + 500L;
					}
					else
					{
						bool flag21 = skillbar.lastNormalSkillTimer < curServerTimeStampMS;
						if (flag21)
						{
							this.vecSkillBt[0].keepSkill();
							skillbar.lastNormalSkillTimer = curServerTimeStampMS + 500L;
						}
					}
				}
				bool flag22 = 3010 == curSkillId && skillbar.Cd_lose;
				if (flag22)
				{
					long num = (long)ModelBase<Skill_a3Model>.getInstance().skilldic[curSkillId].xml.GetNode("skill_att", "skill_lv==" + ModelBase<Skill_a3Model>.getInstance().skilldic[curSkillId].now_lv.ToString()).getInt("time");
					bool cd_lose = skillbar.Cd_lose;
					if (cd_lose)
					{
						foreach (int current2 in ModelBase<Skill_a3Model>.getInstance().skilldic.Keys)
						{
							bool flag23 = ModelBase<Skill_a3Model>.getInstance().skilldic[current2].endCD > 0L && current2 != 3010;
							if (flag23)
							{
								ModelBase<Skill_a3Model>.getInstance().skilldic[current2].endCD -= num;
							}
						}
						skillbar.Cd_lose = false;
					}
				}
				bool flag24 = ModelBase<Skill_a3Model>.getInstance().skilldic[3010].endCD <= 0L;
				if (flag24)
				{
					skillbar.Cd_lose = true;
				}
			}
		}

		public bool playSkillById(int skillid, bool pkmode = false)
		{
			bool flag = SelfRole._inst == null || muNetCleint.instance == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				skill_a3Data skill_a3Data = ModelBase<Skill_a3Model>.getInstance().skilldic[skillid];
				bool flag2 = skill_a3Data.cdTime > 0;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = SelfRole._inst.m_LockRole == null || SelfRole._inst.m_LockRole.isDead;
					if (flag3)
					{
						Vector3 position = SelfRole._inst.m_curModel.position;
						if (pkmode)
						{
							SelfRole._inst.m_LockRole = OtherPlayerMgr._inst.FindNearestEnemyOne(position, false, null, false, PK_TYPE.PK_PEACE);
						}
						bool flag4 = SelfRole._inst.m_LockRole == null;
						if (flag4)
						{
							SelfRole._inst.m_LockRole = MonsterMgr._inst.FindNearestMonster(position, null, false, PK_TYPE.PK_PEACE, false);
						}
						bool flag5 = SelfRole._inst.m_LockRole == null;
						if (flag5)
						{
							result = false;
							return result;
						}
					}
					bool flag6 = SelfRole._inst.m_fAttackCount > 0f;
					if (flag6)
					{
						result = false;
					}
					else
					{
						SelfRole._inst.LeaveHide();
						bool flag7 = SelfRole._inst.m_curSkillId == skillid && skillid == skillbar.NORNAL_SKILL_ID;
						if (flag7)
						{
							long curServerTimeStampMS = muNetCleint.instance.CurServerTimeStampMS;
							bool flag8 = skillbar.lastNormalSkillTimer == 0L;
							if (flag8)
							{
								skillbar.lastNormalSkillTimer = curServerTimeStampMS + 500L;
							}
							else
							{
								bool flag9 = skillbar.lastNormalSkillTimer < curServerTimeStampMS;
								if (flag9)
								{
									this.vecSkillBt[0].keepSkill();
									skillbar.lastNormalSkillTimer = curServerTimeStampMS + 500L;
								}
							}
						}
						else
						{
							SelfRole._inst.TurnToRole(SelfRole._inst.m_LockRole, true);
							SelfRole._inst.PlaySkill(skillid);
							bool flag10 = !SelfRole._inst.m_LockRole.isfake;
							if (flag10)
							{
								BaseProxy<BattleProxy>.getInstance().send_cast_self_skill(SelfRole._inst.m_LockRole.m_unIID, skillid);
							}
							else if (skillid == 2003 || skillid == 5003)
							{
								MediaClient.instance.PlaySoundUrl("audio/skill/" + skillid, false, null);
							}
							skill_a3Data.doCD();
						}
						bool flag11 = skill_a3Data.skillType == 0;
						if (flag11)
						{
							BaseProxy<BattleProxy>.getInstance().sendUseSelfSkill((uint)skill_a3Data.skill_id);
						}
						result = true;
					}
				}
			}
			return result;
		}

		public int getSkillCanUse()
		{
			int result;
			for (int i = this.vecSkillBt.Count - 1; i > -1; i--)
			{
				SKillCdBt sKillCdBt = this.vecSkillBt[i];
				bool flag = sKillCdBt.canUse();
				if (flag)
				{
					result = sKillCdBt.data.skill_id;
					return result;
				}
			}
			result = -1;
			return result;
		}

		public static LGAvatarGameInst getAttackTarget(int range = 1000)
		{
			LGAvatarGameInst lGAvatarGameInst = null;
			bool flag = ModelBase<PlayerModel>.getInstance().checkPK();
			if (flag)
			{
				lGAvatarGameInst = LGOthers.instance.getNearPlayer(range);
			}
			bool flag2 = lGAvatarGameInst == null;
			if (flag2)
			{
				lGAvatarGameInst = LGMonsters.instacne.getNearMon(range);
			}
			return lGAvatarGameInst;
		}

		private void OnSwitchAutoPlay()
		{
			bool s_bStandaloneScene = SelfRole.s_bStandaloneScene;
			if (!s_bStandaloneScene)
			{
				bool autofighting = SelfRole.fsm.Autofighting;
				bool flag = autofighting;
				if (flag)
				{
					SelfRole.fsm.Stop();
					flytxt.flyUseContId("autoplay_stop", null, 0);
				}
				else
				{
					SelfRole.fsm.StartAutofight();
					SelfRole.fsm.ClearAutoConfig();
					flytxt.flyUseContId("autoplay_start", null, 0);
				}
			}
		}

		private void OnFSMStartStop(bool running)
		{
			base.getGameObjectByPath("combat/apbtn/on").SetActive(!running);
			base.getGameObjectByPath("combat/apbtn/off").SetActive(running);
			base.getGameObjectByPath("combat/apbtn/fire").SetActive(running);
			if (running)
			{
				ArrayList arrayList = new ArrayList();
				Action arg_65_0;
				if ((arg_65_0 = skillbar.<>c.<>9__39_0) == null)
				{
					arg_65_0 = (skillbar.<>c.<>9__39_0 = new Action(skillbar.<>c.<>9.<OnFSMStartStop>b__39_0));
				}
				Action value = arg_65_0;
				arrayList.Add(value);
				arrayList.Add("自动战斗中...");
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_DOING, arrayList, false);
			}
			else
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_DOING);
			}
			A3_BeStronger.Instance.ContentShown.gameObject.SetActive(false);
			A3_BeStronger.Instance.ClickPanel.gameObject.SetActive(false);
		}

		public void ShowCombatUI(bool show)
		{
			Vector3 localScale = show ? Vector3.one : Vector3.zero;
			this.tfCombat.localScale = localScale;
		}

		private void OnChangeLock(GameObject go)
		{
			Vector3 position = SelfRole._inst.m_curModel.position;
			switch (ModelBase<PlayerModel>.getInstance().pk_state)
			{
			case PK_TYPE.PK_PEACE:
				goto IL_139;
			case PK_TYPE.PK_PKALL:
				break;
			case PK_TYPE.PK_TEAM:
			{
				bool flag = BaseProxy<TeamProxy>.getInstance().MyTeamData == null;
				if (!flag)
				{
					BaseRole arg_F5_0 = SelfRole._inst;
					OtherPlayerMgr arg_F0_0 = OtherPlayerMgr._inst;
					Vector3 arg_F0_1 = position;
					bool arg_F0_2 = false;
					Func<ProfessionRole, bool> arg_F0_3;
					if ((arg_F0_3 = skillbar.<>c.<>9__41_1) == null)
					{
						arg_F0_3 = (skillbar.<>c.<>9__41_1 = new Func<ProfessionRole, bool>(skillbar.<>c.<>9.<OnChangeLock>b__41_1));
					}
					arg_F5_0.m_LockRole = arg_F0_0.FindNearestEnemyOne(arg_F0_1, arg_F0_2, arg_F0_3, true, PK_TYPE.PK_TEAM);
					bool flag2 = SelfRole._inst.m_LockRole == null;
					if (flag2)
					{
						SelfRole._inst.m_LockRole = MonsterMgr._inst.FindNearestSummon(position);
					}
					bool flag3 = SelfRole._inst.m_LockRole == null;
					if (flag3)
					{
						goto IL_139;
					}
					return;
				}
				break;
			}
			default:
				goto IL_139;
			}
			BaseRole arg_65_0 = SelfRole._inst;
			OtherPlayerMgr arg_60_0 = OtherPlayerMgr._inst;
			Vector3 arg_60_1 = position;
			bool arg_60_2 = false;
			Func<ProfessionRole, bool> arg_60_3;
			if ((arg_60_3 = skillbar.<>c.<>9__41_0) == null)
			{
				arg_60_3 = (skillbar.<>c.<>9__41_0 = new Func<ProfessionRole, bool>(skillbar.<>c.<>9.<OnChangeLock>b__41_0));
			}
			arg_65_0.m_LockRole = arg_60_0.FindNearestEnemyOne(arg_60_1, arg_60_2, arg_60_3, true, PK_TYPE.PK_PKALL);
			bool flag4 = SelfRole._inst.m_LockRole == null;
			if (flag4)
			{
				SelfRole._inst.m_LockRole = MonsterMgr._inst.FindNearestSummon(position);
			}
			bool flag5 = SelfRole._inst.m_LockRole == null;
			if (!flag5)
			{
				return;
			}
			IL_139:
			BaseRole arg_16B_0 = SelfRole._inst;
			MonsterMgr arg_166_0 = MonsterMgr._inst;
			Vector3 arg_166_1 = position;
			Func<MonsterRole, bool> arg_166_2;
			if ((arg_166_2 = skillbar.<>c.<>9__41_3) == null)
			{
				arg_166_2 = (skillbar.<>c.<>9__41_3 = new Func<MonsterRole, bool>(skillbar.<>c.<>9.<OnChangeLock>b__41_3));
			}
			arg_16B_0.m_LockRole = arg_166_0.FindNearestMonster(arg_166_1, arg_166_2, true, PK_TYPE.PK_PEACE, false);
		}
	}
}
