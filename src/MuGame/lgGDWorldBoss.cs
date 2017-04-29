using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class lgGDWorldBoss : lgGDBase
	{
		private Variant _bossdata;

		public lgGDWorldBoss(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new lgGDWorldBoss(m as gameManager);
		}

		public override void init()
		{
		}

		public Variant GetAllBossInfo()
		{
			bool flag = this._bossdata == null;
			Variant bossdata;
			if (flag)
			{
				this._bossdata = "";
				this.send_get_wrdboss_respawntm();
				bossdata = this._bossdata;
			}
			else
			{
				bossdata = this._bossdata;
			}
			return bossdata;
		}

		public Variant GetBossData(uint mid)
		{
			bool flag = this._bossdata;
			Variant result;
			if (flag)
			{
				foreach (Variant current in this._bossdata._arr)
				{
					bool flag2 = mid == current["mid"];
					if (flag2)
					{
						result = current;
						return result;
					}
				}
			}
			result = null;
			return result;
		}

		public void on_monster_spawn(Variant data)
		{
			bool flag = this._bossdata == null;
			if (!flag)
			{
				bool flag2 = data.ContainsKey("ltpid");
				bool flag3 = false;
				uint num = 0u;
				bool flag4 = flag2;
				if (flag4)
				{
					Variant variant = data["mons"];
					foreach (Variant current in variant._arr)
					{
						bool flag5 = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.getWorldBossConfById(current["mid"]._uint, (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.carr) == null;
						if (flag5)
						{
							break;
						}
						flag3 = false;
						foreach (Variant current2 in this._bossdata._arr)
						{
							bool flag6 = current2["mid"] == num;
							if (flag6)
							{
								using (Dictionary<string, Variant>.KeyCollection.Enumerator enumerator3 = data.Keys.GetEnumerator())
								{
									while (enumerator3.MoveNext())
									{
										Variant key = enumerator3.Current;
										current2[key] = data[key];
									}
								}
								flag3 = true;
								break;
							}
						}
						bool flag7 = !flag3;
						if (flag7)
						{
							this._bossdata._arr.Add(current);
						}
						bool in_level = (this.g_mgr as muLGClient).g_levelsCT.in_level;
						if (in_level)
						{
							bool flag8 = data["ltpid"] == (this.g_mgr as muLGClient).g_levelsCT.current_lvl;
							if (flag8)
							{
								foreach (Variant current3 in data["mons"]._arr)
								{
								}
							}
						}
					}
				}
				else
				{
					num = data["mid"]._uint;
					bool flag9 = (this.g_mgr.g_gameConfM as muCLientConfig).localGeneral.getWorldBossConfById(num, (this.g_mgr.g_netM as muNetCleint).joinWorldInfoInst.carr) == null;
					if (!flag9)
					{
						foreach (Variant current4 in this._bossdata._arr)
						{
							bool flag10 = current4["mid"] == num;
							if (flag10)
							{
								using (Dictionary<string, Variant>.KeyCollection.Enumerator enumerator6 = data.Keys.GetEnumerator())
								{
									while (enumerator6.MoveNext())
									{
										Variant key2 = enumerator6.Current;
										current4[key2] = data[key2];
									}
								}
								flag3 = true;
								break;
							}
						}
						bool flag11 = !flag3;
						if (flag11)
						{
							this._bossdata._arr.Add(data);
						}
					}
				}
			}
		}

		public void send_get_wrdboss_respawntm()
		{
			(this.g_mgr.g_netM as muNetCleint).igMapMsgs.get_wrdboss_respawntm(false);
			(this.g_mgr.g_netM as muNetCleint).igMapMsgs.get_wrdboss_respawntm(true);
		}
	}
}
