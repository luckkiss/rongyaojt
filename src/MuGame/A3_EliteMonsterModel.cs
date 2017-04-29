using Cross;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class A3_EliteMonsterModel : ModelBase<A3_EliteMonsterModel>
	{
		public Dictionary<uint, EliteMonsterInfo> dicEMonInfo;

		private List<uint> listSortedEMonId;

		public string s10 = "";

		public string s11 = "";

		public string s12 = "";

		public string s20 = "";

		public string s21 = "";

		public string s22 = "";

		public string s30 = "";

		public string s31 = "";

		public string s32 = "";

		public List<int> s13 = new List<int>();

		public List<int> s23 = new List<int>();

		public List<int> s33 = new List<int>();

		public int i11;

		public int i12;

		public int i21;

		public int i22;

		public int i31;

		public int i32;

		public int[] bossid = new int[20];

		public int[] boss_status = new int[20];

		public List<uint> ListSortedEMonId
		{
			get
			{
				return this.listSortedEMonId ?? this.GetSortedMonInfoIdList();
			}
			set
			{
				this.listSortedEMonId = value;
			}
		}

		public A3_EliteMonsterModel()
		{
			this.MWLR_strf(1, ref this.s10, ref this.s11, ref this.s12, ref this.s13, ref this.i11, ref this.i12);
			this.MWLR_strf(2, ref this.s20, ref this.s21, ref this.s22, ref this.s23, ref this.i21, ref this.i22);
			this.MWLR_strf(3, ref this.s30, ref this.s31, ref this.s32, ref this.s33, ref this.i31, ref this.i32);
			this.dicEMonInfo = new Dictionary<uint, EliteMonsterInfo>();
			this.ListSortedEMonId = new List<uint>();
		}

		private void MWLR_strf(int i, ref string s0, ref string s1, ref string s2, ref List<int> s3, ref int i1, ref int i2)
		{
			SXML sXML = XMLMgr.instance.GetSXML("worldboss", "");
			SXML node = sXML.GetNode("droplist", "id==" + i);
			s0 = sXML.GetNode("boss", "id==" + i).getString("name");
			string[] array = sXML.GetNode("boss", "id==" + i).getString("time").Split(new char[]
			{
				','
			});
			i1 = int.Parse(array[0]);
			i2 = int.Parse(array[1]);
			s1 = node.getString("intro1");
			s2 = node.getString("intro2");
			List<SXML> nodeList = node.GetNodeList("equip", "");
			s3.Clear();
			foreach (SXML current in nodeList)
			{
				s3.Add(current.getInt("id"));
			}
		}

		public List<uint> GetSortedMonInfoIdList()
		{
			List<uint> list = new List<uint>(this.dicEMonInfo.Keys);
			list.Sort(delegate(uint first, uint second)
			{
				bool flag = this.dicEMonInfo[first].upLv.Value > this.dicEMonInfo[second].upLv.Value;
				int result;
				if (flag)
				{
					result = 1;
				}
				else
				{
					bool flag2 = this.dicEMonInfo[first].upLv.Value == this.dicEMonInfo[second].upLv.Value;
					if (flag2)
					{
						bool flag3 = this.dicEMonInfo[first].lv.Value > this.dicEMonInfo[second].lv.Value;
						if (flag3)
						{
							result = 1;
							return result;
						}
						bool flag4 = this.dicEMonInfo[first].lv.Value == this.dicEMonInfo[second].lv.Value;
						if (flag4)
						{
							result = 0;
							return result;
						}
						bool flag5 = this.dicEMonInfo[first].lv.Value < this.dicEMonInfo[second].lv.Value;
						if (flag5)
						{
							result = -1;
							return result;
						}
					}
					result = -1;
				}
				return result;
			});
			return this.ListSortedEMonId = list;
		}

		public EliteMonsterInfo AddData(Variant data)
		{
			uint @uint = data["mid"]._uint;
			bool flag = !this.dicEMonInfo.ContainsKey(@uint);
			EliteMonsterInfo eliteMonsterInfo;
			if (flag)
			{
				eliteMonsterInfo = new EliteMonsterInfo(data.ContainsKey("kill_tm") ? data["kill_tm"]._uint : 0u, data.ContainsKey("respawntm") ? data["respawntm"]._uint : 0u, data.ContainsKey("killer_name") ? data["killer_name"]._str : "", data.ContainsKey("mapid") ? data["mapid"]._int : 0, (data.ContainsKey("mon_x") && data.ContainsKey("mon_y")) ? new Vector2((float)data["mon_x"]._int, (float)data["mon_y"]._int) : default(Vector2), @uint);
				this.dicEMonInfo.Add(@uint, eliteMonsterInfo);
				this.ReadRewardXml(@uint);
			}
			else
			{
				eliteMonsterInfo = this.dicEMonInfo[@uint];
			}
			return eliteMonsterInfo;
		}

		public void LoadReward(uint monId)
		{
			this.ReadRewardXml(monId);
		}

		private void ReadRewardXml(uint monId)
		{
			List<uint> list = new List<uint>();
			SXML sXML = XMLMgr.instance.GetSXML("worldboss", "");
			SXML node = sXML.GetNode("mdrop", "mid==" + monId);
			list.Clear();
			List<SXML> nodeList = node.GetNodeList("item", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				list.Add(nodeList[i].getUint("id"));
			}
			bool flag = !EliteMonsterInfo.poolItemReward.ContainsKey(monId);
			if (flag)
			{
				EliteMonsterInfo.poolItemReward.Add(monId, list);
			}
		}
	}
}
