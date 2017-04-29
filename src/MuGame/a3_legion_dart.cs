using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_legion_dart : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_legion_dart.<>c <>9 = new a3_legion_dart.<>c();

			public static Action<GameObject> <>9__17_3;

			public static Action<GameObject> <>9__17_4;

			public static Action<GameObject> <>9__17_5;

			public static Action<GameObject> <>9__17_6;

			internal void <init>b__17_3(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_LEGION_DART);
			}

			internal void <init>b__17_4(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_LEGION_DART);
			}

			internal void <init>b__17_5(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_LEGION_DART);
			}

			internal void <init>b__17_6(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_LEGION_DART);
			}
		}

		public static a3_legion_dart instance;

		private string str = "押 送";

		private string city = "candodart/scroll_view_dart/contain/UndergroundCity/enter/Text";

		private string dark = "candodart/scroll_view_dart/contain/darkPalace/enter/Text";

		private string wind = "candodart/scroll_view_dart/contain/coldWind/enter/Text";

		private int legionLvl;

		private int length;

		private int one;

		private int three;

		private int five;

		private SXML ss;

		private List<SXML> listXml = new List<SXML>();

		private Dictionary<int, clans> dicClan = new Dictionary<int, clans>();

		public bool isme = false;

		private uint item_id;

		public bool isopen = false;

		public bool ltes = false;

		public override void init()
		{
			a3_legion_dart.instance = this;
			this.ss = XMLMgr.instance.GetSXML("clan_escort", "");
			this.listXml = this.ss.GetNodeList("line", "");
			this.length = this.listXml.Count;
			clans value = default(clans);
			for (int i = 0; i < this.length; i++)
			{
				value.open_lv_clan = this.listXml[i].getInt("clan_lvl");
				value.pathid = this.listXml[i].getUint("id");
				value.target_map = this.listXml[i].getUint("target_map");
				value.add_money_num = this.listXml[i].getInt("clan_money");
				value.item_id = this.listXml[i].getUint("item_id");
				value.item_num = this.listXml[i].getInt("item_num");
				bool flag = !this.dicClan.ContainsKey(this.listXml[i].getInt("id"));
				if (flag)
				{
					this.dicClan.Add(this.listXml[i].getInt("id"), value);
				}
			}
			this.one = this.dicClan[1].open_lv_clan;
			this.three = this.dicClan[2].open_lv_clan;
			this.five = this.dicClan[3].open_lv_clan;
			string str = "candodart/scroll_view_dart/contain";
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("item.item", "id==" + this.dicClan[1].item_id);
			this.item_id = this.dicClan[1].item_id;
			base.getTransformByPath(str + "/UndergroundCity/award/2/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/item/" + sXMLList[0].getInt("icon_file"));
			Text arg_272_0 = base.getTransformByPath(str + "/UndergroundCity/award/1/Text").GetComponent<Text>();
			clans clans = this.dicClan[1];
			arg_272_0.text = clans.add_money_num.ToString();
			Text arg_2A8_0 = base.getTransformByPath(str + "/UndergroundCity/award/2/Text").GetComponent<Text>();
			clans = this.dicClan[1];
			arg_2A8_0.text = clans.item_num.ToString();
			new BaseButton(base.getTransformByPath(str + "/UndergroundCity/award/2"), 1, 1).onClick = delegate(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this.dicClan[1].item_id);
				arrayList.Add(1);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_MINITIP, arrayList, false);
			};
			sXMLList = XMLMgr.instance.GetSXMLList("item.item", "id==" + this.dicClan[2].item_id);
			this.item_id = this.dicClan[2].item_id;
			base.getTransformByPath(str + "/darkPalace/award/2/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/item/" + sXMLList[0].getInt("icon_file"));
			Text arg_390_0 = base.getTransformByPath(str + "/darkPalace/award/1/Text").GetComponent<Text>();
			clans = this.dicClan[2];
			arg_390_0.text = clans.add_money_num.ToString();
			Text arg_3C6_0 = base.getTransformByPath(str + "/darkPalace/award/2/Text").GetComponent<Text>();
			clans = this.dicClan[2];
			arg_3C6_0.text = clans.item_num.ToString();
			new BaseButton(base.getTransformByPath(str + "/darkPalace/award/2"), 1, 1).onClick = delegate(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this.dicClan[2].item_id);
				arrayList.Add(1);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_MINITIP, arrayList, false);
			};
			sXMLList = XMLMgr.instance.GetSXMLList("item.item", "id==" + this.dicClan[3].item_id);
			this.item_id = this.dicClan[3].item_id;
			base.getTransformByPath(str + "/coldWind/award/2/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/item/" + sXMLList[0].getInt("icon_file"));
			Text arg_4AE_0 = base.getTransformByPath(str + "/coldWind/award/1/Text").GetComponent<Text>();
			clans = this.dicClan[3];
			arg_4AE_0.text = clans.add_money_num.ToString();
			Text arg_4E4_0 = base.getTransformByPath(str + "/coldWind/award/2/Text").GetComponent<Text>();
			clans = this.dicClan[3];
			arg_4E4_0.text = clans.item_num.ToString();
			new BaseButton(base.getTransformByPath(str + "/coldWind/award/2"), 1, 1).onClick = delegate(GameObject go)
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(this.dicClan[3].item_id);
				arrayList.Add(1);
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_MINITIP, arrayList, false);
			};
			BaseButton arg_545_0 = new BaseButton(base.getTransformByPath("candodart/btn_close"), 1, 1);
			Action<GameObject> arg_545_1;
			if ((arg_545_1 = a3_legion_dart.<>c.<>9__17_3) == null)
			{
				arg_545_1 = (a3_legion_dart.<>c.<>9__17_3 = new Action<GameObject>(a3_legion_dart.<>c.<>9.<init>b__17_3));
			}
			arg_545_0.onClick = arg_545_1;
			BaseButton arg_57C_0 = new BaseButton(base.getTransformByPath("bg"), 1, 1);
			Action<GameObject> arg_57C_1;
			if ((arg_57C_1 = a3_legion_dart.<>c.<>9__17_4) == null)
			{
				arg_57C_1 = (a3_legion_dart.<>c.<>9__17_4 = new Action<GameObject>(a3_legion_dart.<>c.<>9.<init>b__17_4));
			}
			arg_57C_0.onClick = arg_57C_1;
			BaseButton arg_5B3_0 = new BaseButton(base.getTransformByPath("cantdart/close"), 1, 1);
			Action<GameObject> arg_5B3_1;
			if ((arg_5B3_1 = a3_legion_dart.<>c.<>9__17_5) == null)
			{
				arg_5B3_1 = (a3_legion_dart.<>c.<>9__17_5 = new Action<GameObject>(a3_legion_dart.<>c.<>9.<init>b__17_5));
			}
			arg_5B3_0.onClick = arg_5B3_1;
			BaseButton arg_5EA_0 = new BaseButton(base.getTransformByPath("cantdart/bg/back"), 1, 1);
			Action<GameObject> arg_5EA_1;
			if ((arg_5EA_1 = a3_legion_dart.<>c.<>9__17_6) == null)
			{
				arg_5EA_1 = (a3_legion_dart.<>c.<>9__17_6 = new Action<GameObject>(a3_legion_dart.<>c.<>9.<init>b__17_6));
			}
			arg_5EA_0.onClick = arg_5EA_1;
			new BaseButton(base.getTransformByPath("cantdart/bg/go"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag2 = ModelBase<PlayerModel>.getInstance().mapid == 10u;
				if (!flag2)
				{
					SelfRole.Transmit(1001, null, false, false);
				}
				this.ltes = true;
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_LEGION_DART);
			};
			new BaseButton(base.getTransformByPath("candodart/scroll_view_dart/contain/UndergroundCity/enter"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag2 = !BaseProxy<a3_dartproxy>.getInstance().canOpenDart;
				if (flag2)
				{
					flytxt.instance.fly(ContMgr.getCont("clan_12", null), 0, default(Color), null);
				}
				else
				{
					bool flag3 = ModelBase<A3_LegionModel>.getInstance().myLegion.clanc < 3;
					if (flag3)
					{
						flytxt.instance.fly(ContMgr.getCont("clan_10", null), 0, default(Color), null);
						InterfaceMgr.getInstance().close(InterfaceMgr.A3_LEGION_DART);
					}
					else
					{
						BaseProxy<a3_dartproxy>.getInstance().sendDartStart(this.dicClan[1].pathid);
						BaseProxy<a3_dartproxy>.getInstance().isme = true;
						this.ltes = true;
						InterfaceMgr.getInstance().close(InterfaceMgr.A3_LEGION_DART);
					}
				}
			};
			new BaseButton(base.getTransformByPath("candodart/scroll_view_dart/contain/darkPalace/enter"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag2 = !BaseProxy<a3_dartproxy>.getInstance().canOpenDart;
				if (flag2)
				{
					flytxt.instance.fly(ContMgr.getCont("clan_12", null), 0, default(Color), null);
				}
				else
				{
					bool flag3 = ModelBase<A3_LegionModel>.getInstance().myLegion.clanc < 3 || ModelBase<A3_LegionModel>.getInstance().myLegion.lvl < this.three;
					if (flag3)
					{
						flytxt.instance.fly(ContMgr.getCont("clan_10", null), 0, default(Color), null);
						InterfaceMgr.getInstance().close(InterfaceMgr.A3_LEGION_DART);
					}
					else
					{
						bool flag4 = ModelBase<A3_LegionModel>.getInstance().myLegion.clanc >= 3 && ModelBase<A3_LegionModel>.getInstance().myLegion.lvl >= this.three;
						if (flag4)
						{
							BaseProxy<a3_dartproxy>.getInstance().sendDartStart(this.dicClan[2].pathid);
							BaseProxy<a3_dartproxy>.getInstance().isme = true;
							this.ltes = true;
							InterfaceMgr.getInstance().close(InterfaceMgr.A3_LEGION_DART);
						}
					}
				}
			};
			new BaseButton(base.getTransformByPath("candodart/scroll_view_dart/contain/coldWind/enter"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag2 = !BaseProxy<a3_dartproxy>.getInstance().canOpenDart;
				if (flag2)
				{
					flytxt.instance.fly(ContMgr.getCont("clan_12", null), 0, default(Color), null);
				}
				else
				{
					bool flag3 = ModelBase<A3_LegionModel>.getInstance().myLegion.clanc < 3 || ModelBase<A3_LegionModel>.getInstance().myLegion.lvl < this.five;
					if (flag3)
					{
						flytxt.instance.fly(ContMgr.getCont("clan_10", null), 0, default(Color), null);
						InterfaceMgr.getInstance().close(InterfaceMgr.A3_LEGION_DART);
					}
					else
					{
						bool flag4 = ModelBase<A3_LegionModel>.getInstance().myLegion.clanc >= 3 && ModelBase<A3_LegionModel>.getInstance().myLegion.lvl >= this.five;
						if (flag4)
						{
							BaseProxy<a3_dartproxy>.getInstance().sendDartStart(this.dicClan[3].pathid);
							BaseProxy<a3_dartproxy>.getInstance().isme = true;
							this.ltes = true;
							InterfaceMgr.getInstance().close(InterfaceMgr.A3_LEGION_DART);
						}
					}
				}
			};
			bool show = BaseProxy<a3_dartproxy>.getInstance().show2;
			if (show)
			{
				base.getGameObjectByPath("candodart").SetActive(false);
				base.getGameObjectByPath("cantdart").SetActive(true);
			}
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(2u, new Action<GameEvent>(this.creatLegion));
			BaseProxy<A3_LegionProxy>.getInstance().addEventListener(5u, new Action<GameEvent>(this.upLegion));
			BaseProxy<a3_dartproxy>.getInstance().addEventListener(1u, new Action<GameEvent>(this.info));
		}

		public override void onShowed()
		{
			BaseProxy<a3_dartproxy>.getInstance().sendDartGo();
			this.legionLvl = ModelBase<A3_LegionModel>.getInstance().myLegion.lvl;
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			this.initText(this.legionLvl);
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
		}

		private void creatLegion(GameEvent e)
		{
			base.getTransformByPath(this.city).GetComponent<Text>().text = this.str;
		}

		private void upLegion(GameEvent e)
		{
			this.legionLvl = ModelBase<A3_LegionModel>.getInstance().myLegion.lvl;
			this.initText(this.legionLvl);
		}

		private void info(GameEvent e)
		{
			this.isopen = e.data["finish"];
			bool flag = this.isopen;
			if (flag)
			{
				a3_liteMinimap.instance.getGameObjectByPath("goonDart").SetActive(false);
			}
		}

		private void getNum(GameEvent e)
		{
			List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("item.item", "id==" + this.item_id);
			string @string = sXMLList[0].getString("item_name");
			flytxt.instance.fly(ContMgr.getCont("clan_11", new string[]
			{
				e.data["item_num"]
			}) + @string, 0, default(Color), null);
		}

		private void initText(int lvl)
		{
			bool flag = lvl >= 1 && lvl < 3;
			if (flag)
			{
				base.getTransformByPath(this.city).GetComponent<Text>().text = this.str;
				base.getTransformByPath(this.dark).GetComponent<Text>().text = this.three + "级军团开启";
				base.getTransformByPath(this.wind).GetComponent<Text>().text = this.five + "级军团开启";
				base.getTransformByPath("candodart/scroll_view_dart/contain/darkPalace/enter").GetComponent<Button>().interactable = false;
				base.getTransformByPath("candodart/scroll_view_dart/contain/coldWind/enter").GetComponent<Button>().interactable = false;
			}
			bool flag2 = lvl <= 4 && lvl >= 3;
			if (flag2)
			{
				base.getTransformByPath(this.city).GetComponent<Text>().text = this.str;
				base.getTransformByPath(this.dark).GetComponent<Text>().text = this.str;
				base.getTransformByPath(this.wind).GetComponent<Text>().text = this.five + "级军团开启";
				base.getTransformByPath("candodart/scroll_view_dart/contain/darkPalace/enter").GetComponent<Button>().interactable = true;
				base.getTransformByPath("candodart/scroll_view_dart/contain/coldWind/enter").GetComponent<Button>().interactable = false;
			}
			else
			{
				bool flag3 = lvl <= 6 && lvl >= 5;
				if (flag3)
				{
					base.getTransformByPath(this.city).GetComponent<Text>().text = this.str;
					base.getTransformByPath(this.dark).GetComponent<Text>().text = this.str;
					base.getTransformByPath(this.wind).GetComponent<Text>().text = this.str;
					base.getTransformByPath("candodart/scroll_view_dart/contain/darkPalace/enter").GetComponent<Button>().interactable = true;
					base.getTransformByPath("candodart/scroll_view_dart/contain/coldWind/enter").GetComponent<Button>().interactable = true;
				}
				else
				{
					bool flag4 = lvl > 6;
					if (flag4)
					{
						base.getTransformByPath(this.city).GetComponent<Text>().text = this.str;
						base.getTransformByPath(this.dark).GetComponent<Text>().text = this.str;
						base.getTransformByPath(this.wind).GetComponent<Text>().text = this.str;
						base.getTransformByPath("candodart/scroll_view_dart/contain/darkPalace/enter").GetComponent<Button>().interactable = true;
						base.getTransformByPath("candodart/scroll_view_dart/contain/coldWind/enter").GetComponent<Button>().interactable = true;
					}
				}
			}
		}
	}
}
