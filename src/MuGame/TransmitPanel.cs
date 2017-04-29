using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class TransmitPanel : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly TransmitPanel.<>c <>9 = new TransmitPanel.<>c();

			public static Action<GameObject> <>9__18_3;

			internal void <init>b__18_3(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.TRANSMIT_PANEL);
			}
		}

		private static TransmitPanel instance;

		private TransmitData data;

		public int curNeedMoney;

		private Text textCostMoney;

		private Text textTargetDesc;

		private int transmitMapPoint;

		private int targetMapId;

		public Dictionary<int, int> dicMappoint;

		public static TransmitPanel Instance
		{
			get
			{
				return TransmitPanel.instance;
			}
			set
			{
				TransmitPanel.instance = value;
			}
		}

		public int TargetMapId
		{
			get
			{
				return this.targetMapId;
			}
			set
			{
				this.targetMapId = value;
				this.transmitMapPoint = this.targetMapId * 100 + 1;
			}
		}

		public Vector3 currentTargetPosition
		{
			get;
			set;
		}

		public override void init()
		{
			TransmitPanel.instance = this;
			this.textCostMoney = base.transform.FindChild("bt1/cost").GetComponent<Text>();
			this.textTargetDesc = base.transform.FindChild("desc").GetComponent<Text>();
			new BaseButton(base.transform.FindChild("bt0"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag2 = this.data.handle_customized_afterTransmit != null;
				if (flag2)
				{
					this.data.handle_customized_afterTransmit();
				}
				else
				{
					bool flag3 = this.data.targetPosition != Vector3.zero;
					if (flag3)
					{
						SelfRole.WalkToMap(this.data.mapId, this.data.targetPosition, this.data.after_arrive, 0.3f);
					}
				}
				Action expr_76 = this.data.after_clickBtnWalk;
				if (expr_76 != null)
				{
					expr_76();
				}
				InterfaceMgr.getInstance().close(InterfaceMgr.TRANSMIT_PANEL);
			};
			new BaseButton(base.transform.FindChild("bt1"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag2 = ModelBase<PlayerModel>.getInstance().vip < 3u && (ulong)ModelBase<PlayerModel>.getInstance().money < (ulong)((long)this.curNeedMoney);
				if (flag2)
				{
					flytxt.instance.fly(ContMgr.getCont("comm_nomoney", null), 0, default(Color), null);
				}
				else
				{
					bool autofighting = SelfRole.fsm.Autofighting;
					if (autofighting)
					{
						SelfRole.fsm.Stop();
					}
					bool flag3 = this.data.closeWinName != null;
					if (flag3)
					{
						for (int j = 0; j < this.data.closeWinName.Length; j++)
						{
							InterfaceMgr.getInstance().close(this.data.closeWinName[j]);
						}
					}
					SelfRole.Transmit(this.dicMappoint[this.data.mapId], delegate
					{
						bool flag4 = this.data.handle_customized_afterTransmit != null;
						if (flag4)
						{
							this.data.handle_customized_afterTransmit();
						}
						else
						{
							bool flag5 = this.data.targetPosition != Vector3.zero;
							if (flag5)
							{
								SelfRole.WalkToMap(this.data.mapId, this.data.targetPosition, this.data.after_arrive, 0.3f);
							}
						}
					}, false, false);
					Action expr_EB = this.data.after_clickBtnTransmit;
					if (expr_EB != null)
					{
						expr_EB();
					}
					InterfaceMgr.getInstance().close(InterfaceMgr.TRANSMIT_PANEL);
				}
			};
			BaseButton arg_C5_0 = new BaseButton(base.transform.FindChild("btclose"), 1, 1);
			Action<GameObject> arg_C5_1;
			if ((arg_C5_1 = TransmitPanel.<>c.<>9__18_3) == null)
			{
				arg_C5_1 = (TransmitPanel.<>c.<>9__18_3 = new Action<GameObject>(TransmitPanel.<>c.<>9.<init>b__18_3));
			}
			arg_C5_0.onClick = arg_C5_1;
			this.dicMappoint = new Dictionary<int, int>();
			List<SXML> nodeList = XMLMgr.instance.GetSXML("mappoint", "").GetNodeList("trans_remind", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				int @int;
				int int2;
				bool flag = (@int = nodeList[i].getInt("map_id")) != -1 && (int2 = nodeList[i].getInt("trance_id")) != -1;
				if (flag)
				{
					this.dicMappoint.Add(@int, 1 + int2 * 100);
				}
			}
		}

		public override void onShowed()
		{
			this.data = null;
			bool flag = this.uiData.Count == 1;
			if (flag)
			{
				this.data = (TransmitData)this.uiData[0];
				bool flag2 = this.data.check_beforeShow && !this.CheckPoint();
				if (flag2)
				{
					bool flag3 = this.data.handle_customized_afterTransmit != null;
					if (flag3)
					{
						this.data.handle_customized_afterTransmit();
					}
					else
					{
						bool flag4 = this.data.targetPosition != Vector3.zero;
						if (flag4)
						{
							SelfRole.WalkToMap(this.dicMappoint[this.data.mapId], this.data.targetPosition, this.data.after_arrive, 0.3f);
						}
					}
					Action expr_FD = this.data.after_clickBtnWalk;
					if (expr_FD != null)
					{
						expr_FD();
					}
					InterfaceMgr.getInstance().close(InterfaceMgr.TRANSMIT_PANEL);
				}
				else
				{
					bool flag5 = ModelBase<PlayerModel>.getInstance().vip >= 3u;
					this.TargetMapId = this.data.mapId;
					SXML sXML = XMLMgr.instance.GetSXML("mappoint.p", "id==" + this.dicMappoint[this.data.mapId]);
					Variant singleMapConf = SvrMapConfig.instance.getSingleMapConf(sXML.getUint("mapid"));
					string text = singleMapConf.ContainsKey("map_name") ? singleMapConf["map_name"]._str : "--";
					int @int = sXML.getInt("cost");
					this.curNeedMoney = @int / 10 * (int)(ModelBase<PlayerModel>.getInstance().lvl / 10f) + @int;
					this.textCostMoney.text = this.curNeedMoney.ToString();
					this.textTargetDesc.text = text;
					base.gameObject.SetActive(true);
					base.transform.SetAsLastSibling();
				}
			}
			else
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.TRANSMIT_PANEL);
				Debug.Log("invalid uidata");
			}
		}

		public override void onClosed()
		{
			this.uiData.Clear();
		}

		public virtual bool CheckPoint()
		{
			bool flag = GRMap.instance == null;
			bool result;
			if (flag)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_TASKOPT);
				result = false;
			}
			else
			{
				bool flag2 = this.dicMappoint.Count == 0;
				if (flag2)
				{
					result = true;
				}
				else
				{
					bool flag3 = !this.dicMappoint.ContainsKey(GRMap.instance.m_nCurMapID);
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = this.GetMapIdByMappoint(this.dicMappoint[this.data.mapId]) != GRMap.instance.m_nCurMapID && GRMap.instance.m_nCurMapID != this.data.mapId && this.dicMappoint[this.data.mapId] != this.dicMappoint[GRMap.instance.m_nCurMapID];
						result = flag4;
					}
				}
			}
			return result;
		}

		private int GetMapIdByMappoint(int mapId)
		{
			return (mapId - 1) / 100;
		}
	}
}
