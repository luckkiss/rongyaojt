using MuGame.Qsmy.model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class StatePick : StateBase
	{
		public static StatePick Instance = new StatePick();

		private Vector3 calc_ori;

		private Vector3 calc_tar;

		private Vector3 calc_cur;

		private DropItem pickTarget;

		private DropItem nearest = null;

		private bool passed;

		private int priority;

		public override void Enter()
		{
			this.passed = false;
			this.nearest = (this.pickTarget = null);
			this.priority = SelfRole._inst.m_moveAgent.avoidancePriority;
		}

		public override void Execute(float delta_time)
		{
			SelfRole._inst.m_moveAgent.avoidancePriority = 1;
			bool flag = this.passed;
			if (flag)
			{
				this.nearest = (this.pickTarget = null);
			}
			bool flag2 = this.nearest == null || !this.nearest.gameObject;
			if (flag2)
			{
				float num = 3.40282347E+38f;
				Dictionary<uint, DropItem>.Enumerator enumerator = BaseRoomItem.instance.dDropItem_own.GetEnumerator();
				while (enumerator.MoveNext())
				{
					KeyValuePair<uint, DropItem> current = enumerator.Current;
					DropItem value = current.Value;
					bool flag3 = value != null && value.gameObject;
					if (flag3)
					{
						bool flag4 = value.itemdta.ownerId > 0u;
						bool flag5 = value.itemdta.ownerId != ModelBase<PlayerModel>.getInstance().cid || (BaseProxy<TeamProxy>.getInstance().MyTeamData != null && value.itemdta.ownerId == BaseProxy<TeamProxy>.getInstance().MyTeamData.teamId);
						bool flag6 = flag4 & flag5;
						if (!flag6)
						{
							bool flag7 = Vector3.Distance(value.transform.position.ConvertToGamePosition(), SelfRole._inst.m_curModel.transform.position.ConvertToGamePosition()) > StateInit.Instance.PickDistance;
							if (!flag7)
							{
								bool flag8 = ModelBase<a3_BagModel>.getInstance().curi <= ModelBase<a3_BagModel>.getInstance().getItems(false).Count && (ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)value.itemdta.tpid) == 0 || ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)value.itemdta.tpid) >= ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)value.itemdta.tpid).maxnum) && value.itemdta.tpid != 0;
								if (!flag8)
								{
									bool flag9 = value.canPick && ModelBase<AutoPlayModel>.getInstance().WillPick((uint)value.itemdta.tpid);
									if (flag9)
									{
										this.pickTarget = value;
										bool flag10 = this.nearest == null;
										if (flag10)
										{
											this.nearest = this.pickTarget;
										}
										float num2 = Vector3.Distance(this.pickTarget.transform.position.ConvertToGamePosition(), SelfRole._inst.m_curModel.transform.position.ConvertToGamePosition());
										bool flag11 = num2 < num;
										if (flag11)
										{
											num = num2;
											this.nearest = this.pickTarget;
										}
									}
								}
							}
						}
					}
				}
				this.calc_ori = SelfRole._inst.m_curModel.transform.position;
				bool flag12 = this.nearest != null;
				if (flag12)
				{
					SelfRole._inst.SetDestPos(this.nearest.transform.position);
					this.calc_tar = this.nearest.transform.position;
					return;
				}
				this.calc_tar = Vector3.zero;
			}
			else
			{
				bool flag13 = this.nearest != null;
				if (flag13)
				{
					bool flag14 = this.nearest.gameObject != null && Vector3.Distance(SelfRole._inst.m_curModel.position.ConvertToGamePosition(), this.nearest.transform.position.ConvertToGamePosition()) < 0.5f;
					if (flag14)
					{
						this.nearest.PickUpItem();
					}
					bool flag15 = Vector3.Distance(this.nearest.transform.position.ConvertToGamePosition(), SelfRole._inst.m_curModel.transform.position.ConvertToGamePosition()) > StateInit.Instance.PickDistance;
					bool flag16 = !this.passed && !flag15;
					if (flag16)
					{
						SelfRole._inst.TurnToPos(this.nearest.transform.position);
						SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, true);
					}
					else
					{
						this.nearest = null;
						SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, false);
					}
				}
			}
			this.calc_cur = SelfRole._inst.m_curModel.transform.position;
			this.passed = this.CheckPass(this.calc_ori, this.calc_cur, this.calc_tar);
			bool flag17 = this.nearest == null || this.nearest.gameObject == null;
			if (flag17)
			{
				SelfRole._inst.m_moveAgent.Stop();
				SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, false);
				bool autofighting = SelfRole.fsm.Autofighting;
				if (autofighting)
				{
					SelfRole.fsm.ChangeState(StateAttack.Instance);
				}
				else
				{
					SelfRole.fsm.ChangeState(StateIdle.Instance);
				}
			}
		}

		public override void Exit()
		{
			SelfRole._inst.m_moveAgent.Stop();
			SelfRole._inst.m_curAni.SetBool(EnumAni.ANI_RUN, false);
			SelfRole._inst.m_moveAgent.avoidancePriority = this.priority;
		}

		public void AutoEquipProcess(a3_BagItemData itmdata)
		{
			AutoPlayModel instance = ModelBase<AutoPlayModel>.getInstance();
			bool flag = !SelfRole.fsm.Autofighting;
			if (!flag)
			{
				bool flag2 = instance.EqpType == 0 || ModelBase<A3_VipModel>.getInstance().Level < 3;
				if (!flag2)
				{
					int eqpProc = instance.EqpProc;
					int quality = itmdata.confdata.quality;
					int num = 1 << quality - 1;
					bool flag3 = (eqpProc & num) == 0;
					if (!flag3)
					{
						bool flag4 = instance.EqpType == 1;
						if (flag4)
						{
							BaseProxy<BagProxy>.getInstance().sendSellItems(itmdata.id, itmdata.num);
							flytxt.instance.fly("自动出售 " + Globle.getColorStrByQuality(itmdata.confdata.item_name, itmdata.confdata.quality), 0, default(Color), null);
						}
						else
						{
							bool flag5 = instance.EqpType == 2;
							if (flag5)
							{
								List<uint> list = new List<uint>();
								list.Add(itmdata.id);
								BaseProxy<EquipProxy>.getInstance().sendsell(list);
								flytxt.instance.fly("自动分解 " + Globle.getColorStrByQuality(itmdata.confdata.item_name, itmdata.confdata.quality), 0, default(Color), null);
							}
						}
					}
				}
			}
		}

		private bool CheckPass(Vector3 origin, Vector3 current, Vector3 target)
		{
			Vector3 vector = origin.ConvertToGamePosition();
			Vector3 vector2 = current.ConvertToGamePosition();
			Vector3 vector3 = target.ConvertToGamePosition();
			bool flag = vector3 == Vector3.zero;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				float x = vector.x;
				float z = vector.z;
				float x2 = vector2.x;
				float z2 = vector2.z;
				float x3 = vector3.x;
				float z3 = vector3.z;
				bool flag2 = x < x3;
				bool flag3;
				if (flag2)
				{
					flag3 = (x2 > x3);
				}
				else
				{
					bool flag4 = x == x3;
					if (flag4)
					{
						flag3 = (x2 != x3);
					}
					else
					{
						flag3 = (x2 < x3);
					}
				}
				bool flag5 = z > z3;
				bool flag6;
				if (flag5)
				{
					flag6 = (z2 < z3);
				}
				else
				{
					bool flag7 = z == z3;
					if (flag7)
					{
						flag6 = (z2 != z3);
					}
					else
					{
						flag6 = (z2 > z3);
					}
				}
				result = (flag3 | flag6);
			}
			return result;
		}
	}
}
