using Cross;
using System;
using UnityEngine;

namespace MuGame
{
	internal class TransMapPoint : RoomObj
	{
		public int id;

		public uint mapid;

		public float faceto = 0f;

		public override void init()
		{
			base.init();
			MeshRenderer component = base.gameObject.GetComponent<MeshRenderer>();
			bool flag = component != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(component);
			}
			MeshFilter component2 = base.gameObject.GetComponent<MeshFilter>();
			bool flag2 = component2 != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(component2);
			}
			base.gameObject.name = "RoomObj" + this.id;
		}

		protected override void onTrigger()
		{
			Variant singleMapConf = SvrMapConfig.instance.getSingleMapConf(this.mapid);
			int num = 0;
			bool flag = singleMapConf.ContainsKey("lv_up");
			if (flag)
			{
				num = singleMapConf["lv_up"];
			}
			int num2 = 0;
			bool flag2 = singleMapConf.ContainsKey("lv");
			if (flag2)
			{
				num2 = singleMapConf["lv"];
			}
			bool flag3 = (long)num > (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl);
			if (flag3)
			{
				flytxt.instance.fly(ContMgr.getCont("comm_nolvmap", new string[]
				{
					num.ToString(),
					num2.ToString(),
					singleMapConf["map_name"]
				}), 0, default(Color), null);
			}
			else
			{
				bool flag4 = (long)num == (long)((ulong)ModelBase<PlayerModel>.getInstance().up_lvl);
				if (flag4)
				{
					bool flag5 = (long)num2 > (long)((ulong)ModelBase<PlayerModel>.getInstance().lvl);
					if (flag5)
					{
						flytxt.instance.fly(ContMgr.getCont("comm_nolvmap", new string[]
						{
							num.ToString(),
							num2.ToString(),
							singleMapConf["map_name"]
						}), 0, default(Color), null);
						return;
					}
				}
				joystick.instance.stop();
				BaseProxy<MapProxy>.getInstance().changingMap = true;
				loading_cloud.showIt(delegate
				{
					ModelBase<PlayerModel>.getInstance().mapBeginroatate = this.faceto;
					BaseProxy<MapProxy>.getInstance().sendBeginChangeMap(this.id, false, false);
				});
			}
		}
	}
}
