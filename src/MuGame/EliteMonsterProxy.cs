using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class EliteMonsterProxy : BaseProxy<EliteMonsterProxy>
	{
		public static readonly uint EVENT_ELITEMONSTER = 1u;

		public static readonly uint EVENT_BOSSOPSUCCESS = 3u;

		public EliteMonsterProxy()
		{
			this.addProxyListener(156u, new Action<Variant>(this.EltMon_OnEMOp));
		}

		public void SendProxy()
		{
			this.sendRPC(156u, new Variant());
		}

		public void EltMon_OnEMOp(Variant data)
		{
			Debug.Log("收到156协议" + data.dump());
			bool flag = !data["elite_mon"]._arr[0].ContainsKey("mon_x");
			if (flag)
			{
				this.SendProxy();
			}
			else
			{
				bool flag2 = !data["elite_mon"]._arr[0].ContainsKey("killer_name");
				if (flag2)
				{
					this.SendProxy();
				}
				else
				{
					this.RefreshEliteMonInfo(data);
				}
			}
			base.dispatchEvent(GameEvent.alloc(EliteMonsterProxy.EVENT_ELITEMONSTER, this, data, false, true));
		}

		private void RefreshEliteMonInfo(Variant data)
		{
			Dictionary<uint, EliteMonsterInfo> dicEMonInfo = ModelBase<A3_EliteMonsterModel>.getInstance().dicEMonInfo;
			bool flag = data.ContainsKey("elite_mon");
			if (flag)
			{
				List<Variant> arr = data["elite_mon"]._arr;
				for (int i = 0; i < arr.Count; i++)
				{
					uint @uint = arr[i]["mid"]._uint;
					bool flag2 = !dicEMonInfo.ContainsKey(@uint);
					if (flag2)
					{
						ModelBase<A3_EliteMonsterModel>.getInstance().AddData(arr[i]);
					}
					else
					{
						dicEMonInfo[@uint] = new EliteMonsterInfo(arr[i].ContainsKey("kill_tm") ? arr[i]["kill_tm"]._uint : 0u, arr[i].ContainsKey("respawntm") ? arr[i]["respawntm"]._uint : 0u, arr[i].ContainsKey("killer_name") ? arr[i]["killer_name"]._str : null, arr[i].ContainsKey("mapid") ? arr[i]["mapid"]._int : 0, (arr[i].ContainsKey("mon_x") && arr[i].ContainsKey("mon_y")) ? new Vector2((float)arr[i]["mon_x"]._int, (float)arr[i]["mon_y"]._int) : default(Vector2), arr[i]["mid"]._uint);
					}
				}
				uint up_lvl = ModelBase<PlayerModel>.getInstance().up_lvl;
				uint lvl = ModelBase<PlayerModel>.getInstance().lvl;
				int num = 30001;
				List<int> list = new List<int>();
				bool flag3 = data.ContainsKey("elite_mon");
				if (flag3)
				{
					arr = data["elite_mon"]._arr;
					for (int j = 0; j < arr.Count; j++)
					{
						list.Add(arr[j]["mid"]._int);
					}
				}
				list.Sort();
				for (int k = 0; k < list.Count; k++)
				{
					bool flag4 = up_lvl > XMLMgr.instance.GetSXML("monsters.monsters", "id==" + list[k]).getUint("zhuan");
					if (flag4)
					{
						num = list[k];
					}
					else
					{
						bool flag5 = up_lvl == XMLMgr.instance.GetSXML("monsters.monsters", "id==" + list[k]).getUint("zhuan");
						if (!flag5)
						{
							break;
						}
						bool flag6 = lvl >= XMLMgr.instance.GetSXML("monsters.monsters", "id==" + list[k]).getUint("zhuan");
						if (!flag6)
						{
							num = list[k];
							break;
						}
						num = list[k];
					}
				}
				List<uint> list2 = new List<uint>();
				for (int l = 0; l < list.Count; l++)
				{
					bool flag7 = list[l] <= num;
					if (flag7)
					{
						foreach (Variant current in arr)
						{
							bool flag8 = current["mid"] == list[l];
							if (flag8)
							{
								bool flag9 = current["kill_tm"] == 0;
								if (flag9)
								{
									IconAddLightMgr.getInstance().showOrHideFires("Open_Light_enterElite", null);
									break;
								}
								IconAddLightMgr.getInstance().showOrHideFires("jingyingguai_Light_enterElite", null);
							}
						}
					}
				}
			}
		}
	}
}
