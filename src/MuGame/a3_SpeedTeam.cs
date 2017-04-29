using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_SpeedTeam : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_SpeedTeam.<>c <>9 = new a3_SpeedTeam.<>c();

			public static Action<GameObject> <>9__11_1;

			internal void <init>b__11_1(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_SPEEDTEAM);
			}
		}

		private List<GameObject> tab = new List<GameObject>();

		public static a3_SpeedTeam instance;

		private int tabIdx;

		private Transform team_infopanel;

		private Transform pre;

		public uint begin_index = 0u;

		public uint end_index = 20u;

		public List<ItemTeamData> itdList = new List<ItemTeamData>();

		public int limited_dj = 0;

		private int lvl;

		private int up_lvl;

		public override void init()
		{
			a3_SpeedTeam.instance = this;
			for (int i = 0; i < base.transform.FindChild("panelTab2/con").childCount; i++)
			{
				this.tab.Add(base.transform.FindChild("panelTab2/con").GetChild(i).gameObject);
			}
			for (int j = 0; j < this.tab.Count; j++)
			{
				int tag = j;
				new BaseButton(this.tab[j].transform, 1, 1).onClick = delegate(GameObject go)
				{
					this.onTab(tag);
				};
			}
			this.team_infopanel = base.transform.FindChild("team_tab1/panel/scroll_rect/contain");
			this.pre = base.transform.FindChild("team_tab1/panel/scroll_rect/team_pre");
			BaseProxy<TeamProxy>.getInstance().SendGetMapTeam(this.begin_index, this.end_index);
			BaseButton arg_129_0 = new BaseButton(base.transform.FindChild("btn_close"), 1, 1);
			Action<GameObject> arg_129_1;
			if ((arg_129_1 = a3_SpeedTeam.<>c.<>9__11_1) == null)
			{
				arg_129_1 = (a3_SpeedTeam.<>c.<>9__11_1 = new Action<GameObject>(a3_SpeedTeam.<>c.<>9.<init>b__11_1));
			}
			arg_129_0.onClick = arg_129_1;
			BaseProxy<TeamProxy>.getInstance().addEventListener(TeamProxy.EVENT_AFFIRMINVITE, new Action<GameEvent>(this.getTeamPanel));
		}

		public override void onShowed()
		{
			bool flag = this.uiData == null;
			if (flag)
			{
				this.onTab(0);
			}
			else
			{
				int tag = (int)this.uiData[0];
				this.onTab(tag);
			}
		}

		private void getTeamPanel(GameEvent obj)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_SPEEDTEAM);
			ArrayList arrayList = new ArrayList();
			arrayList.Add(2);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_SHEJIAO, arrayList, false);
		}

		public void GetTeam_info(ItemTeamMemberData itm)
		{
			this.itdList.Clear();
			for (int i = 0; i < itm.itemTeamDataList.Count; i++)
			{
				this.itdList.Add(itm.itemTeamDataList[i]);
			}
			this.Set_teaminfo();
		}

		private void onTab(int tag)
		{
			this.tabIdx = tag;
			for (int i = 0; i < this.tab.Count; i++)
			{
				this.tab[i].GetComponent<Button>().interactable = true;
			}
			this.tab[this.tabIdx].GetComponent<Button>().interactable = false;
			this.Clear_con();
			switch (this.tabIdx)
			{
			case 0:
				BaseProxy<TeamProxy>.getInstance().SendGetPageTeam(0u, this.begin_index, this.end_index);
				this.limited_dj = this.Set_Limited_dj(5);
				break;
			case 1:
				BaseProxy<TeamProxy>.getInstance().SendGetPageTeam(1u, this.begin_index, this.end_index);
				this.limited_dj = this.Set_Limited_dj(4);
				break;
			case 2:
				BaseProxy<TeamProxy>.getInstance().SendGetPageTeam(2u, this.begin_index, this.end_index);
				this.limited_dj = this.Set_Limited_dj(3);
				break;
			case 3:
				BaseProxy<TeamProxy>.getInstance().SendGetPageTeam(108u, this.begin_index, this.end_index);
				this.limited_dj = this.Set_Limited_dj(1);
				break;
			case 4:
				BaseProxy<TeamProxy>.getInstance().SendGetPageTeam(105u, this.begin_index, this.end_index);
				this.limited_dj = this.Set_Limited_dj(2);
				break;
			}
		}

		private void Clear_con()
		{
			bool flag = this.team_infopanel.childCount == 0;
			if (!flag)
			{
				for (int i = 0; i < this.team_infopanel.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.team_infopanel.GetChild(i).gameObject);
				}
				this.limited_dj = 0;
			}
		}

		public int Set_Limited_dj(int i)
		{
			this.lvl = 0;
			this.up_lvl = 0;
			SXML sXML = XMLMgr.instance.GetSXML("func_open.team_lv_limit", "id==" + i);
			this.lvl = sXML.getInt("lv");
			this.up_lvl = sXML.getInt("zhuan");
			return this.up_lvl * 100 + this.lvl;
		}

		public void Set_teaminfo()
		{
			int count = this.itdList.Count;
			for (int i = 0; i < count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.pre.gameObject);
				gameObject.transform.SetParent(this.team_infopanel);
				gameObject.transform.localScale = Vector3.one;
				gameObject.SetActive(true);
				this.Set_Line(gameObject.transform, this.itdList[i]);
				this.Set_apply(gameObject.transform, this.itdList[i]);
			}
		}

		private void Set_apply(Transform go, ItemTeamData data)
		{
			uint num = ModelBase<PlayerModel>.getInstance().up_lvl * 100u + ModelBase<PlayerModel>.getInstance().lvl;
			bool flag = data.curcnt >= 5u;
			if (flag)
			{
				go.transform.FindChild("4/full").gameObject.SetActive(true);
				go.transform.FindChild("4/apply").gameObject.SetActive(false);
				go.transform.FindChild("4/applyed").gameObject.SetActive(false);
			}
			bool flag2 = data.curcnt < 5u && !go.transform.FindChild("4/applyed").gameObject.activeInHierarchy && (ulong)num >= (ulong)((long)this.limited_dj);
			if (flag2)
			{
				go.transform.FindChild("4/full").gameObject.SetActive(false);
				go.transform.FindChild("4/apply").gameObject.SetActive(true);
				go.transform.FindChild("4/applyed").gameObject.SetActive(false);
				new BaseButton(go.transform.FindChild("4/apply"), 1, 1).onClick = delegate(GameObject oo)
				{
					bool flag3 = BaseProxy<TeamProxy>.getInstance().MyTeamData == null;
					if (flag3)
					{
						BaseProxy<TeamProxy>.getInstance().SendApplyJoinTeam(data.teamId);
						go.transform.FindChild("4/apply").gameObject.SetActive(false);
						go.transform.FindChild("4/applyed").gameObject.SetActive(true);
					}
					else
					{
						flytxt.instance.fly("请先退出当前队伍", 0, default(Color), null);
					}
				};
			}
		}

		private void Set_Line(Transform go, ItemTeamData data)
		{
			go.transform.FindChild("1/" + data.carr).gameObject.SetActive(true);
			go.transform.FindChild("2/name").GetComponent<Text>().text = data.name;
			go.transform.FindChild("2/dj").GetComponent<Text>().text = string.Concat(new object[]
			{
				data.zhuan,
				"转",
				data.lvl,
				"级"
			});
			go.transform.FindChild("2/zy/" + data.carr).gameObject.SetActive(true);
			for (int i = 0; i < data.members.Count; i++)
			{
				go.transform.FindChild(string.Concat(new object[]
				{
					"3/zy/teamer/",
					i + 1,
					"/",
					data.members[i]
				})).gameObject.SetActive(true);
			}
			bool flag = this.limited_dj == 0;
			if (flag)
			{
				go.transform.FindChild("3/dj").GetComponent<Text>().text = "无等级限制";
			}
			else
			{
				go.transform.FindChild("3/dj").GetComponent<Text>().text = string.Concat(new object[]
				{
					this.up_lvl,
					"转",
					this.lvl,
					"级"
				});
			}
		}
	}
}
