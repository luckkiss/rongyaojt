using DG.Tweening;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class SKillCdBt : Skin
	{
		public int idx;

		private GameObject goIcon;

		private GameObject goLock;

		private Image _cdmask;

		private Image skillIcon;

		private skill_a3Data _data;

		public uint maxCD = 0u;

		private float _turns_Interval = 1000f;

		private float _cur_truns_tm = 0f;

		private Text txtCd;

		public bool autofighting = false;

		private Button bt;

		public int m_nCurDownID = 0;

		private float beginScale;

		private float endScale;

		public float lastSkillUseTimer = 0f;

		public uint lastIId = 0u;

		public static bool needAutoNextAttack = false;

		private uint curNormalSkillTurn = 0u;

		public new skill_a3Data data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
				bool flag = this.txtCd != null;
				if (flag)
				{
					this.txtCd.text = "";
					this._cdmask.fillAmount = 0f;
				}
				bool flag2 = this._data != null;
				if (flag2)
				{
					Sprite sprite = Resources.Load("icon/skill/" + this._data.skill_id, typeof(Sprite)) as Sprite;
					bool flag3 = sprite != null;
					if (flag3)
					{
						this.skillIcon.sprite = sprite;
					}
					this.goLock.SetActive(false);
					this.goIcon.SetActive(true);
				}
				else
				{
					this.goIcon.SetActive(false);
					this.goLock.SetActive(true);
				}
			}
		}

		public override bool visiable
		{
			get
			{
				return this.__visiable;
			}
			set
			{
				bool flag = this.__visiable == value;
				if (!flag)
				{
					this.__visiable = value;
					bool flag2 = this.goLock != null;
					if (flag2)
					{
						this.goLock.SetActive(!this.__visiable);
					}
					this.bt.interactable = this.__visiable;
				}
			}
		}

		public SKillCdBt(Transform trans, int i) : base(trans)
		{
			this.idx = i;
			this.init();
		}

		private void init()
		{
			this.bt = this.__mainTrans.gameObject.AddComponent<Button>();
			this.bt.transition = Selectable.Transition.None;
			this.beginScale = this.recTransform.localScale.x;
			this.endScale = this.beginScale * 0.92f;
			EventTriggerListener.Get(this.bt.gameObject).onDown = new EventTriggerListener.VoidDelegate(this.onDrag);
			EventTriggerListener.Get(this.bt.gameObject).onExit = new EventTriggerListener.VoidDelegate(this.onDragout);
			EventTriggerListener.Get(this.bt.gameObject).onUp = new EventTriggerListener.VoidDelegate(this.onDragout);
			this.goIcon = base.getGameObjectByPath("icon");
			this._cdmask = base.getComponentByPath<Image>("icon/cd");
			this.skillIcon = base.getComponentByPath<Image>("icon/icon");
			this._cdmask.fillAmount = 0f;
			bool flag = base.transform.FindChild("txtcd") != null;
			if (flag)
			{
				this.txtCd = base.getComponentByPath<Text>("txtcd");
				this.txtCd.text = "";
			}
			this.goLock = base.getGameObjectByPath("lock");
		}

		public bool canUse()
		{
			bool flag = this._data == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.data.skill_id == skillbar.NORNAL_SKILL_ID;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = ModelBase<PlayerModel>.getInstance().mp < this.data.mp;
					if (flag3)
					{
						flytxt.instance.fly(ContMgr.getCont("skill_outofmana", null), 0, default(Color), null);
						result = false;
					}
					else
					{
						bool flag4 = this.data.endCD > muNetCleint.instance.CurServerTimeStampMS;
						result = !flag4;
					}
				}
			}
			return result;
		}

		public void doCD()
		{
			this.data.doCD();
		}

		public void keepSkill()
		{
			bool flag = SelfRole._inst.m_LockRole != null && !SelfRole._inst.m_LockRole.isDead;
			if (flag)
			{
				SelfRole._inst.m_fAttackCount = 0.5f;
				bool flag2 = !SelfRole._inst.m_LockRole.isfake;
				if (flag2)
				{
					BaseProxy<BattleProxy>.getInstance().send_cast_self_skill(SelfRole._inst.m_LockRole.m_unIID, this._data.skill_id);
				}
			}
			else
			{
				SelfRole._inst.m_fAttackCount = 0.5f;
				BaseProxy<BattleProxy>.getInstance().send_cast_self_skill(0u, this._data.skill_id);
			}
		}

		public void setSelf_LockRole()
		{
			bool flag = SelfRole._inst.m_LockRole != null && SelfRole._inst.m_LockRole.isDead;
			if (flag)
			{
				SelfRole._inst.m_LockRole = null;
			}
			bool flag2 = ModelBase<PlayerModel>.getInstance().now_pkState == 0;
			if (flag2)
			{
				bool flag3 = SelfRole._inst.m_LockRole is ProfessionRole || SelfRole._inst.m_LockRole is MS0000 || SelfRole._inst.m_LockRole is MDC000;
				if (flag3)
				{
					SelfRole._inst.m_LockRole = null;
				}
				bool flag4 = SelfRole._inst.m_LockRole is MDC000 && ((MDC000)SelfRole._inst.m_LockRole).escort_name == ModelBase<A3_LegionModel>.getInstance().myLegion.clname;
				if (flag4)
				{
					SelfRole._inst.m_LockRole = null;
				}
				bool flag5 = SelfRole._inst.m_LockRole is MDC000;
				if (flag5)
				{
					float num = (float)((MDC000)SelfRole._inst.m_LockRole).curhp / (float)((MDC000)SelfRole._inst.m_LockRole).maxHp * 100f;
					bool flag6 = num <= 20f;
					if (flag6)
					{
						SelfRole._inst.m_LockRole = null;
					}
				}
			}
			else
			{
				bool flag7 = ModelBase<PlayerModel>.getInstance().now_pkState == 1;
				if (flag7)
				{
					bool flag8 = SelfRole._inst.m_LockRole is MS0000 && (SelfRole._inst.m_LockRole as MS0000).masterid == ModelBase<PlayerModel>.getInstance().cid;
					if (flag8)
					{
						SelfRole._inst.m_LockRole = null;
					}
					bool flag9 = SelfRole._inst.m_LockRole is MDC000 && ((MDC000)SelfRole._inst.m_LockRole).escort_name == ModelBase<A3_LegionModel>.getInstance().myLegion.clname;
					if (flag9)
					{
						SelfRole._inst.m_LockRole = null;
					}
					bool flag10 = SelfRole._inst.m_LockRole is MDC000;
					if (flag10)
					{
						float num2 = (float)((MDC000)SelfRole._inst.m_LockRole).curhp / (float)((MDC000)SelfRole._inst.m_LockRole).maxHp * 100f;
						bool flag11 = num2 <= 20f;
						if (flag11)
						{
							SelfRole._inst.m_LockRole = null;
						}
					}
				}
				else
				{
					bool flag12 = ModelBase<PlayerModel>.getInstance().now_pkState == 2;
					if (flag12)
					{
						bool flag13 = SelfRole._inst.m_LockRole is MS0000;
						if (flag13)
						{
							bool flag14 = false;
							bool flag15 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
							if (flag15)
							{
								foreach (ItemTeamData current in BaseProxy<TeamProxy>.getInstance().MyTeamData.itemTeamDataList)
								{
									bool flag16 = current.cid == (SelfRole._inst.m_LockRole as MS0000).masterid;
									if (flag16)
									{
										flag14 = true;
										break;
									}
								}
							}
							bool flag17 = !flag14 && ModelBase<A3_LegionModel>.getInstance().members != null;
							if (flag17)
							{
								foreach (A3_LegionMember current2 in ModelBase<A3_LegionModel>.getInstance().members.Values)
								{
									bool flag18 = (long)current2.cid == (long)((ulong)(SelfRole._inst.m_LockRole as MS0000).masterid);
									if (flag18)
									{
										flag14 = true;
										break;
									}
								}
							}
							bool flag19 = flag14 || (SelfRole._inst.m_LockRole as MS0000).masterid == ModelBase<PlayerModel>.getInstance().cid;
							if (flag19)
							{
								SelfRole._inst.m_LockRole = null;
							}
						}
						else
						{
							bool flag20 = SelfRole._inst.m_LockRole is MDC000;
							if (flag20)
							{
								float num3 = (float)((MDC000)SelfRole._inst.m_LockRole).curhp / (float)((MDC000)SelfRole._inst.m_LockRole).maxHp * 100f;
								bool flag21 = ((MDC000)SelfRole._inst.m_LockRole).escort_name == ModelBase<A3_LegionModel>.getInstance().myLegion.clname;
								if (flag21)
								{
									SelfRole._inst.m_LockRole = null;
								}
								bool flag22 = num3 <= 20f;
								if (flag22)
								{
									SelfRole._inst.m_LockRole = null;
								}
							}
							else
							{
								bool flag23 = SelfRole._inst.m_LockRole is ProfessionRole;
								if (flag23)
								{
									bool flag24 = false;
									bool flag25 = BaseProxy<TeamProxy>.getInstance().MyTeamData != null;
									if (flag25)
									{
										foreach (A3_LegionMember current3 in ModelBase<A3_LegionModel>.getInstance().members.Values)
										{
											bool flag26 = (long)current3.cid == (long)((ulong)SelfRole._inst.m_LockRole.m_unCID);
											if (flag26)
											{
												flag24 = true;
												break;
											}
										}
									}
									bool flag27 = !flag24 && ModelBase<A3_LegionModel>.getInstance().members != null;
									if (flag27)
									{
										foreach (A3_LegionMember current4 in ModelBase<A3_LegionModel>.getInstance().members.Values)
										{
											bool flag28 = (long)current4.cid == (long)((ulong)SelfRole._inst.m_LockRole.m_unCID);
											if (flag28)
											{
												flag24 = true;
												break;
											}
										}
									}
									bool flag29 = flag24;
									if (flag29)
									{
										SelfRole._inst.m_LockRole = null;
									}
								}
							}
						}
					}
				}
			}
			bool flag30 = SelfRole._inst.m_LockRole == null;
			if (flag30)
			{
				SelfRole._inst.m_LockRole = PkmodelAdmin.RefreshLockRole();
			}
			bool flag31 = SelfRole._inst.m_LockRole == null;
			if (flag31)
			{
				SelfRole._inst.m_LockRole = MonsterMgr._inst.FindNearestMonster(SelfRole._inst.m_curModel.position, null, false, PK_TYPE.PK_PEACE, false);
			}
			bool flag32 = SelfRole._inst.m_LockRole == null;
			if (flag32)
			{
				SelfRole._inst.m_LockRole = MonsterMgr._inst.FindNearestFakeMonster(SelfRole._inst.m_curModel.position);
			}
		}

		public void onDrag(GameObject go)
		{
			this.recTransform.DOScale(this.endScale, 0.15f);
			base.transform.FindChild("click").gameObject.SetActive(true);
			base.transform.FindChild("click").GetComponent<Animator>().Play("skill_click", -1, 0f);
			base.transform.FindChild("click").GetComponent<Animator>().SetBool("isLoop", true);
			bool flag = SelfRole.fsm.Autofighting;
			if (flag)
			{
				StateInit.Instance.PlaySkillInAutoPlay(this._data.skill_id);
			}
			else
			{
				bool flag2 = this.data == null;
				if (!flag2)
				{
					bool flag3 = !this.canUse();
					if (!flag3)
					{
						bool flag4 = SelfRole._inst.isPlayingSkill && this.data.skill_id != skillbar.NORNAL_SKILL_ID;
						if (!flag4)
						{
							this.m_nCurDownID = this._data.skill_id;
							this.setSelf_LockRole();
							bool flag5 = SelfRole._inst.m_LockRole == null;
							if (flag5)
							{
								bool flag6 = this.data.skill_id == 3006 || this.data.skill_id == 5004 || this.data.skill_id == 5009 || this.data.skill_id == 3004;
								if (flag6)
								{
									bool flag7 = flytxt.instance != null;
									if (flag7)
									{
										flytxt.instance.fly("无锁定的目标!", 0, default(Color), null);
									}
									return;
								}
							}
							else
							{
								bool flag8 = this.data.skill_id == 3006 || this.data.skill_id == 5004 || this.data.skill_id == 5009 || this.data.skill_id == 3004;
								if (flag8)
								{
									bool flag9 = !PkmodelAdmin.RefreshLockSkill(SelfRole._inst.m_LockRole);
									if (flag9)
									{
										bool flag10 = flytxt.instance != null;
										if (flag10)
										{
											flytxt.instance.fly("无锁定的目标!", 0, default(Color), null);
										}
										return;
									}
								}
							}
							bool flag11 = SelfRole._inst.m_LockRole != null;
							if (flag11)
							{
								SelfRole._inst.LeaveHide();
								bool flag12 = this.data.skill_id != 3010;
								if (flag12)
								{
									SelfRole._inst.TurnToRole(SelfRole._inst.m_LockRole, true);
								}
								SelfRole._inst.PlaySkill(this._data.skill_id);
								bool flag13 = !SelfRole._inst.m_LockRole.isfake;
								if (flag13)
								{
									BaseProxy<BattleProxy>.getInstance().send_cast_self_skill(SelfRole._inst.m_LockRole.m_unIID, this._data.skill_id);
								}
							}
							else
							{
								SelfRole._inst.LeaveHide();
								SelfRole._inst.PlaySkill(this._data.skill_id);
								BaseProxy<BattleProxy>.getInstance().send_cast_self_skill(0u, this._data.skill_id);
							}
							bool flag14 = this._data.skillType == 0;
							if (flag14)
							{
								BaseProxy<BattleProxy>.getInstance().sendUseSelfSkill((uint)this._data.skill_id);
							}
							this.doCD();
						}
					}
				}
			}
		}

		public void onDragout(GameObject go)
		{
			base.transform.FindChild("click").GetComponent<Animator>().SetBool("isLoop", false);
			this.recTransform.DOScale(this.beginScale, 0.15f);
			bool flag = this.data == null;
			if (!flag)
			{
				this.m_nCurDownID = 0;
				bool flag2 = this.data.skill_id == skillbar.NORNAL_SKILL_ID;
				if (flag2)
				{
					skillbar.lastNormalSkillTimer = 0L;
				}
			}
		}

		public void update(long timestp)
		{
			bool flag = !this.visiable;
			if (!flag)
			{
				bool flag2 = this.data == null;
				if (!flag2)
				{
					bool flag3 = this.data.endCD == 0L;
					if (!flag3)
					{
						float num = 1f - (float)(timestp / this.data.endCD);
						int num2 = (int)(this.data.endCD - timestp);
						this._cdmask.fillAmount = (float)num2 / this.maxCD;
						bool flag4 = this.idx > 0;
						if (flag4)
						{
							this.txtCd.text = (num2 / 1000 + 1).ToString();
						}
						bool flag5 = this.data.endCD < timestp;
						if (flag5)
						{
							this.data.endCD = 0L;
							this._cdmask.fillAmount = 0f;
							bool flag6 = this.idx > 0;
							if (flag6)
							{
								this.txtCd.text = "";
							}
						}
					}
				}
			}
		}

		public void useSkill(uint skillid, uint sklvl, bool force = false, LGAvatarGameInst mon = null)
		{
			float time = Time.time;
			bool flag = time - this.lastSkillUseTimer < 0.2f;
			if (!flag)
			{
				this.lastSkillUseTimer = time;
				lgSelfPlayer instance = lgSelfPlayer.instance;
				bool flag2 = !instance.canAttack() && !force;
				if (!flag2)
				{
					bool flag3 = mon == null;
					LGAvatarGameInst lGAvatarGameInst;
					if (flag3)
					{
						lGAvatarGameInst = skillbar.getAttackTarget(9999999);
					}
					else
					{
						lGAvatarGameInst = mon;
					}
					bool flag4 = this.autofighting && lGAvatarGameInst == null;
					if (!flag4)
					{
						bool flag5 = skillid != 1001u;
						if (flag5)
						{
							instance.attack(lGAvatarGameInst, false, skillid);
						}
						else
						{
							float num = (float)muNetCleint.instance.CurServerTimeStampMS;
							bool flag6 = this._cur_truns_tm + this._turns_Interval < num || this.curNormalSkillTurn == 2u || (lGAvatarGameInst != null && this.lastIId != lGAvatarGameInst.iid);
							if (flag6)
							{
								bool flag7 = lGAvatarGameInst != null;
								if (flag7)
								{
									this.lastIId = lGAvatarGameInst.iid;
								}
								this.curNormalSkillTurn = 0u;
							}
							else
							{
								this.curNormalSkillTurn += 1u;
							}
							uint skillid2 = skillid + this.curNormalSkillTurn;
							this._cur_truns_tm = num;
							instance.attack(lGAvatarGameInst, false, skillid2);
						}
					}
				}
			}
		}
	}
}
