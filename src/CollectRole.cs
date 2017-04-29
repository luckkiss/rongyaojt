using MuGame;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CollectRole : MonsterRole
{
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		public static readonly CollectRole.<>c <>9 = new CollectRole.<>c();

		public static Action <>9__8_2;

		public static Action <>9__8_4;

		internal void <onClick>b__8_2()
		{
			BaseProxy<MapProxy>.getInstance().sendStopCollectItem(true);
		}

		internal void <onClick>b__8_4()
		{
			BaseProxy<MapProxy>.getInstance().sendStopCollectItem(true);
		}
	}

	public static CollectRole instance;

	public float collectTime;

	public bool becollected;

	private string collectStr = "";

	private string collectStr2 = "";

	public override void Init(string prefab_path, int layer, Vector3 pos, float roatate = 0f)
	{
		this.m_fNavStoppingDis = 2f;
		base.Init(prefab_path, EnumLayer.LM_COLLECT, pos, roatate);
		M00000_Default_Event m00000_Default_Event = this.m_curModel.gameObject.AddComponent<M00000_Default_Event>();
		m00000_Default_Event.m_monRole = this;
		this.collectTime = this.tempXMl.getFloat("use_time");
		base.maxHp = (base.curhp = 1000);
		this.collectStr = this.tempXMl.getString("show");
	}

	protected override void onRefreshViewType()
	{
		bool flag = this.viewType == BaseRole.VIEW_TYPE_ALL;
		if (flag)
		{
			this.m_moveAgent.avoidancePriority = 0;
			this.m_moveAgent.enabled = false;
			Transform transform = this.m_curModel.transform.FindChild("eff");
			bool flag2 = transform != null;
			if (flag2)
			{
				transform.gameObject.SetActive(true);
			}
		}
	}

	public bool checkCanClick()
	{
		return true;
	}

	public override void onClick()
	{
		bool flag = !ModelBase<A3_TaskModel>.getInstance().IfCurrentCollectItem(this.monsterid);
		if (!flag)
		{
			bool flag2 = Vector3.Distance(this.m_curModel.transform.position, SelfRole._inst.m_curModel.transform.position) > 2f;
			if (flag2)
			{
				SelfRole.moveto(this.m_curModel.transform.position, delegate
				{
					BaseProxy<MapProxy>.getInstance().sendCollectItem(this.m_unIID);
					cd.updateHandle = new Action<cd>(this.onCD);
					Action arg_5E_0 = delegate
					{
						this.m_curAni.SetBool("open", true);
						BaseProxy<MapProxy>.getInstance().sendStopCollectItem(false);
						this.becollected = true;
					};
					float arg_5E_1 = this.collectTime;
					bool arg_5E_2 = true;
					Action arg_5E_3;
					if ((arg_5E_3 = CollectRole.<>c.<>9__8_2) == null)
					{
						arg_5E_3 = (CollectRole.<>c.<>9__8_2 = new Action(CollectRole.<>c.<>9.<onClick>b__8_2));
					}
					cd.show(arg_5E_0, arg_5E_1, arg_5E_2, arg_5E_3, default(Vector3));
				}, true, 2f, true);
			}
			else
			{
				BaseProxy<MapProxy>.getInstance().sendCollectItem(this.m_unIID);
				cd.updateHandle = new Action<cd>(this.onCD);
				Action arg_DC_0 = delegate
				{
					this.m_curAni.SetBool("open", true);
					BaseProxy<MapProxy>.getInstance().sendStopCollectItem(false);
					this.becollected = true;
				};
				float arg_DC_1 = this.collectTime;
				bool arg_DC_2 = true;
				Action arg_DC_3;
				if ((arg_DC_3 = CollectRole.<>c.<>9__8_4) == null)
				{
					arg_DC_3 = (CollectRole.<>c.<>9__8_4 = new Action(CollectRole.<>c.<>9.<onClick>b__8_4));
				}
				cd.show(arg_DC_0, arg_DC_1, arg_DC_2, arg_DC_3, default(Vector3));
			}
		}
	}

	public override void dispose()
	{
		bool @bool = this.m_curAni.GetBool("open");
		if (@bool)
		{
			ConfigUtil.SetTimeout(1400.0, new Action(this.a));
		}
		else
		{
			base.dispose();
		}
	}

	public void a()
	{
		DoAfterMgr.instacne.addAfterRender(new Action(base.dispose));
	}

	public void onCD(cd item)
	{
		int num = (int)(cd.secCD - cd.lastCD) / 100;
		item.txt.text = this.collectStr + ((float)num / 10f).ToString();
	}

	public void onCD2(cd item)
	{
		int num = (int)(cd.secCD - cd.lastCD) / 100;
		this.collectStr2 = "正在打开宝箱";
		item.txt.text = this.collectStr2 + ((float)num / 10f).ToString();
	}

	public override void FrameMove(float delta_time)
	{
	}
}
