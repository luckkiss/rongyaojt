using Cross;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class a3_activeDegreeProxy : BaseProxy<a3_activeDegreeProxy>
	{
		public const uint EVENT_GET_ALLPOINT = 0u;

		public const uint ON_GET_ACTIVEDEGREE_PRIZE = 1u;

		public Dictionary<uint, ActiveDegreeData> itd = new Dictionary<uint, ActiveDegreeData>();

		public List<int> point = new List<int>();

		public int huoyue_point;

		public a3_activeDegreeProxy()
		{
			this.addProxyListener(167u, new Action<Variant>(this.onActivedegree_info));
		}

		public void SendGetPoint(int op)
		{
			Variant variant = new Variant();
			variant["op"] = op;
			this.sendRPC(27u, variant);
		}

		public void SendGetReward(uint op, uint point)
		{
			Variant variant = new Variant();
			variant["op"] = op;
			variant["point"] = point;
			this.sendRPC(27u, variant);
		}

		private void onActivedegree_info(Variant data)
		{
			bool flag = !data.ContainsKey("huoyue_point");
			if (!flag)
			{
				debug.Log("活跃度::" + data.dump());
				bool flag2 = SelfRole._inst != null;
				if (flag2)
				{
					SelfRole._inst.m_LockRole = null;
				}
				bool flag3 = data.ContainsKey("res");
				if (flag3)
				{
					int num = data["res"];
					int num2 = num;
					if (num2 != 0)
					{
						if (num2 == 1)
						{
							this.point.Clear();
							this.itd.Clear();
							this.huoyue_point = data["huoyue_point"];
							foreach (Variant current in data["huoyues"]._arr)
							{
								uint num3 = current["active_id"];
								uint count = current["count"];
								ActiveDegreeData activeDegreeData = new ActiveDegreeData();
								activeDegreeData.id = num3;
								activeDegreeData.count = count;
								this.itd.Add(num3, activeDegreeData);
							}
							using (List<Variant>.Enumerator enumerator2 = data["huoyue_reward"]._arr.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									int item = enumerator2.Current;
									this.point.Add(item);
								}
							}
							bool flag4 = a3_activeDegree.instance != null;
							if (flag4)
							{
								a3_activeDegree.instance.do_Active();
								a3_activeDegree.instance.onLoad_Change();
								a3_activeDegree.instance.onActive_Load();
							}
							this.point.Sort();
							bool flag5 = this.huoyue_point < 20;
							bool flag6;
							if (flag5)
							{
								flag6 = false;
							}
							else
							{
								bool flag7 = this.point.Count > 0;
								if (flag7)
								{
									int num4 = this.point[this.point.Count - 1];
									bool flag8 = num4 == 100;
									flag6 = (!flag8 && this.huoyue_point >= num4 + 20);
								}
								else
								{
									flag6 = true;
								}
							}
							IconAddLightMgr.getInstance().showOrHideFire(flag6 ? "open_light_huoyue" : "close_light_huoyue", null);
						}
					}
				}
			}
		}
	}
}
