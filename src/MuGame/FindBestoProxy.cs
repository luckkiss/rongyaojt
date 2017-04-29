using Cross;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class FindBestoProxy : BaseProxy<FindBestoProxy>
	{
		public static uint EVENT_INFO = 1u;

		public FindBestoProxy()
		{
			this.addProxyListener(150u, new Action<Variant>(this.onMapCount));
		}

		public void sendMap(int count)
		{
			Variant variant = new Variant();
			variant["op"] = 3;
			variant["cost"] = count;
			debug.Log("PPP" + count);
			this.sendRPC(26u, variant);
		}

		public void getinfo()
		{
			Variant variant = new Variant();
			variant["op"] = 1;
			this.sendRPC(26u, variant);
		}

		private void onMapCount(Variant v)
		{
			debug.Log("onMapCount" + v.dump());
			int num = v["res"];
			bool flag = num < 0;
			if (flag)
			{
				Globle.err_output(num);
			}
			else
			{
				bool flag2 = num == 1;
				if (flag2)
				{
					uint iid = v["iid"];
					BaseRole role = RoleMgr._instance.getRole(iid);
					bool flag3 = role != null && !(role is MonsterRole);
					if (flag3)
					{
						bool isMain = role.m_isMain;
						if (isMain)
						{
							bool flag4 = SelfRole._inst != null;
							if (flag4)
							{
								bool flag5 = v["way"] == 2;
								if (flag5)
								{
									int num2 = v["num"];
									bool flag6 = (long)num2 - (long)((ulong)ModelBase<PlayerModel>.getInstance().treasure_num) > 0L;
									if (flag6)
									{
										flytxt.instance.fly("抢夺了" + ((long)num2 - (long)((ulong)ModelBase<PlayerModel>.getInstance().treasure_num)) + "张藏宝图", 0, default(Color), null);
									}
									else
									{
										bool flag7 = (long)num2 - (long)((ulong)ModelBase<PlayerModel>.getInstance().treasure_num) < 0L;
										if (flag7)
										{
											flytxt.instance.fly("丢失了" + (long)((ulong)ModelBase<PlayerModel>.getInstance().treasure_num - (ulong)((long)num2)) + "张藏宝图", 0, default(Color), null);
										}
									}
								}
								ModelBase<PlayerModel>.getInstance().treasure_num = v["num"];
								SelfRole._inst.mapCount = v["num"];
								SelfRole._inst.refreshmapCount(v["num"]);
								bool flag8 = ModelBase<PlayerModel>.getInstance().treasure_num >= 50u;
								if (flag8)
								{
									InterfaceMgr.getInstance().open(InterfaceMgr.A3_BAOTUUI, null, false);
								}
							}
						}
						else
						{
							OtherPlayerMgr._inst.playerMapCountChange(iid, v["num"]);
						}
					}
					bool flag9 = A3_FindBesto.instan;
					if (flag9)
					{
						A3_FindBesto.instan.refreCount();
					}
				}
				bool flag10 = num == 3;
				if (flag10)
				{
					bool flag11 = v["result"] == 0;
					if (flag11)
					{
						flytxt.instance.fly("兑换失败！", 0, default(Color), null);
					}
					else
					{
						flytxt.instance.fly("兑换成功！", 0, default(Color), null);
					}
				}
				bool flag12 = num == 4;
				if (flag12)
				{
					bool flag13 = !v["get_collect"];
					if (flag13)
					{
						flytxt.instance.fly("真倒霉，居然什么都没抢到", 0, default(Color), null);
					}
				}
				bool flag14 = num == 5;
				if (flag14)
				{
					base.dispatchEvent(GameEvent.Create(FindBestoProxy.EVENT_INFO, this, v, false));
				}
			}
		}
	}
}
