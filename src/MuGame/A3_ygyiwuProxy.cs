using Cross;
using GameFramework;
using System;

namespace MuGame
{
	internal class A3_ygyiwuProxy : BaseProxy<A3_ygyiwuProxy>
	{
		public static uint EVENT_YWINFO = 0u;

		public static uint EVENT_ZHISHIINFO = 1u;

		public A3_ygyiwuProxy()
		{
			this.addProxyListener(87u, new Action<Variant>(this.onGetYGexp));
		}

		public void SendYGinfo(uint val)
		{
			Variant variant = new Variant();
			variant["op"] = val;
			this.sendRPC(87u, variant);
		}

		private void onGetYGexp(Variant data)
		{
			debug.Log("远古经验" + data.dump());
			int num = data["res"];
			bool flag = num == 1;
			if (flag)
			{
				ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id = data["god_remains_id"];
				ModelBase<a3_ygyiwuModel>.getInstance().nowGodFB_id = data["g_levelid"];
				ModelBase<PlayerModel>.getInstance().accent_exp = data["exp"];
				ModelBase<a3_ygyiwuModel>.getInstance().nowPre_id = data["king_remains_id"];
				ModelBase<a3_ygyiwuModel>.getInstance().nowPreFB_id = data["k_levelid"];
				ModelBase<a3_ygyiwuModel>.getInstance().nowPre_needupLvl = data["zhuan"];
				ModelBase<a3_ygyiwuModel>.getInstance().nowPre_needLvl = data["lvl"];
				ModelBase<a3_ygyiwuModel>.getInstance().loadList();
				base.dispatchEvent(GameEvent.Create(A3_ygyiwuProxy.EVENT_YWINFO, this, data, false));
			}
			else
			{
				bool flag2 = num == 2;
				if (flag2)
				{
					ModelBase<a3_ygyiwuModel>.getInstance().yiwuLvl = data["remains_lvl"];
					ModelBase<a3_ygyiwuModel>.getInstance().studyTime = data["time_left"];
					base.dispatchEvent(GameEvent.Create(A3_ygyiwuProxy.EVENT_ZHISHIINFO, this, data, false));
				}
				else
				{
					bool flag3 = num == 3;
					if (flag3)
					{
						ModelBase<a3_ygyiwuModel>.getInstance().studyTime = data["time"];
						bool flag4 = a3_ygyiwu.instan;
						if (flag4)
						{
							a3_ygyiwu.instan.ref_StudyBtn();
						}
					}
					else
					{
						bool flag5 = num == 4;
						if (flag5)
						{
							ModelBase<PlayerModel>.getInstance().accent_exp = data["remains_exp"];
							bool flag6 = a3_liteMinimap.instance;
							if (flag6)
							{
								a3_liteMinimap.instance.refreshYGexp();
							}
						}
						else
						{
							bool flag7 = num == 5;
							if (flag7)
							{
								ModelBase<PlayerModel>.getInstance().accent_exp = data["remains_exp"];
								ModelBase<a3_ygyiwuModel>.getInstance().nowGod_id = data["god_remains_id"];
								ModelBase<a3_ygyiwuModel>.getInstance().nowGodFB_id = data["g_levelid"];
								bool flag8 = a3_liteMinimap.instance;
								if (flag8)
								{
									a3_liteMinimap.instance.refreshYGexp();
								}
							}
							else
							{
								bool flag9 = num == 6;
								if (flag9)
								{
									ModelBase<a3_ygyiwuModel>.getInstance().nowPre_id = data["king_remains_id"];
									ModelBase<a3_ygyiwuModel>.getInstance().nowPreFB_id = data["k_levelid"];
									ModelBase<a3_ygyiwuModel>.getInstance().nowPre_needupLvl = data["zhuan"];
									ModelBase<a3_ygyiwuModel>.getInstance().nowPre_needLvl = data["lvl"];
									base.dispatchEvent(GameEvent.Create(A3_ygyiwuProxy.EVENT_YWINFO, this, data, false));
								}
							}
						}
					}
				}
			}
		}
	}
}
