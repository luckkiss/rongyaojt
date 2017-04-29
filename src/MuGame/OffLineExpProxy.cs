using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class OffLineExpProxy : BaseProxy<OffLineExpProxy>
	{
		public static uint EVENT_OFFLINE_EXP_GET = 118u;

		private const uint C2S_OFFLINE_EXP = 118u;

		private const uint S2C_OFFLINE_EXP = 118u;

		public List<a3_BagItemData> eqp = new List<a3_BagItemData>();

		public OffLineExpProxy()
		{
			this.addProxyListener(118u, new Action<Variant>(this.OnGetRes));
			this.sendType(0, false);
		}

		public void Send_Off_Line(int type)
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = 118;
			variant["option"] = (uint)type;
			this.sendRPC(118u, variant);
		}

		public void sendType(int type, bool fenjie)
		{
			Variant variant = new Variant();
			variant["buddy_cmd"] = 118;
			variant["option"] = (uint)type;
			variant["decompose"] = (fenjie ? 1 : 0);
			this.sendRPC(118u, variant);
		}

		private void OnGetRes(Variant data)
		{
			debug.Log("获得离线经验信息::" + data.dump());
			OffLineModel instance = ModelBase<OffLineModel>.getInstance();
			int num = data["res"];
			bool flag = data["eqp"] != null;
			if (flag)
			{
				foreach (Variant current in data["eqp"]._arr)
				{
					a3_BagItemData a3_BagItemData = default(a3_BagItemData);
					a3_BagItemData.equipdata.gem_att = new Dictionary<int, int>();
					a3_BagItemData.equipdata.subjoin_att = new Dictionary<int, int>();
					a3_BagItemData.tpid = current["tpid"];
					a3_BagItemData.bnd = current["bnd"];
					a3_BagItemData.id = current["id"];
					a3_BagItemData.equipdata.intensify_lv = current["intensify_lv"];
					a3_BagItemData.equipdata.combpt = current["combpt"];
					Variant variant = current["gem_att"];
					foreach (Variant current2 in variant._arr)
					{
						int key = current2["att_type"];
						int value = current2["att_value"];
						a3_BagItemData.equipdata.gem_att[key] = value;
					}
					a3_BagItemData.equipdata.add_exp = current["add_exp"];
					a3_BagItemData.equipdata.add_level = current["add_level"];
					a3_BagItemData.equipdata.blessing_lv = current["blessing_lv"];
					Variant variant2 = current["subjoin_att"];
					foreach (Variant current3 in variant2._arr)
					{
						int key2 = current3["att_type"];
						int value2 = current3["att_value"];
						a3_BagItemData.equipdata.subjoin_att[key2] = value2;
					}
					a3_BagItemData.confdata = ModelBase<a3_BagModel>.getInstance().getItemDataById(a3_BagItemData.tpid);
					this.eqp.Add(a3_BagItemData);
				}
			}
			bool flag2 = num < 0;
			if (flag2)
			{
				Debug.Log("off_line_exp_erro::" + num);
				Globle.err_output(num);
			}
			else
			{
				bool flag3 = data.ContainsKey("offline_tm") && num > 4;
				if (flag3)
				{
					instance.OffLineTime = data["offline_tm"];
				}
				switch (num)
				{
				case 1:
				case 2:
				case 3:
				case 4:
					base.dispatchEvent(GameEvent.Create(OffLineExpProxy.EVENT_OFFLINE_EXP_GET, this, data, false));
					break;
				default:
					instance.BaseExp = num;
					break;
				}
			}
		}
	}
}
