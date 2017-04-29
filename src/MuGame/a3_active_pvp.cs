using Cross;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_active_pvp : a3BaseActive
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_active_pvp.<>c <>9 = new a3_active_pvp.<>c();

			public static Action<GameObject> <>9__15_1;

			public static Action<GameObject> <>9__15_4;

			public static Action<GameObject> <>9__15_7;

			internal void <init>b__15_1(GameObject go)
			{
			}

			internal void <init>b__15_4(GameObject go)
			{
				BaseProxy<A3_ActiveProxy>.getInstance().SendPVP(5);
			}

			internal void <init>b__15_7(GameObject go)
			{
				BaseProxy<A3_ActiveProxy>.getInstance().SendPVP(3);
			}
		}

		public static a3_active_pvp instance;

		private Text findCount;

		private Text buyCount;

		private Text duanwei;

		private Text top_saiji;

		private int findid = 0;

		private Image rank;

		private Text giftzuanshi;

		private Text giftmingwang;

		private Text box;

		private Text buy_Count_zuan;

		private GameObject tip;

		public GameObject no_open;

		public GameObject yes_open;

		private bool b = true;

		private bool frist = true;

		public a3_active_pvp(Window win, string pathStr) : base(win, pathStr)
		{
		}

		public override void init()
		{
			this.tip = base.transform.FindChild("tip").gameObject;
			this.findCount = base.transform.FindChild("find_info/day_count/text").GetComponent<Text>();
			this.buyCount = base.transform.FindChild("find_info/buy_count/text").GetComponent<Text>();
			this.giftzuanshi = base.transform.FindChild("GetReward/tem1/zuan/bg_text/tex").GetComponent<Text>();
			this.giftmingwang = base.transform.FindChild("GetReward/tem1/mingwang/bg_text/tex").GetComponent<Text>();
			this.box = base.transform.FindChild("GetReward/tem1/box_name/tex").GetComponent<Text>();
			this.buy_Count_zuan = base.transform.FindChild("find_info/find/text2/text").GetComponent<Text>();
			this.rank = base.transform.FindChild("info/icon").GetComponent<Image>();
			this.duanwei = base.transform.FindChild("info/duanwei").GetComponent<Text>();
			this.top_saiji = base.transform.FindChild("top_text").GetComponent<Text>();
			this.no_open = base.transform.FindChild("find_info/no_open").gameObject;
			this.yes_open = base.transform.FindChild("find_info/find").gameObject;
			new BaseButton(base.getTransformByPath("reward"), 1, 1).onClick = delegate(GameObject go)
			{
				this.refGiff();
				base.transform.FindChild("GetReward").gameObject.SetActive(true);
			};
			BaseButton arg_19A_0 = new BaseButton(base.getTransformByPath("rank"), 1, 1);
			Action<GameObject> arg_19A_1;
			if ((arg_19A_1 = a3_active_pvp.<>c.<>9__15_1) == null)
			{
				arg_19A_1 = (a3_active_pvp.<>c.<>9__15_1 = new Action<GameObject>(a3_active_pvp.<>c.<>9.<init>b__15_1));
			}
			arg_19A_0.onClick = arg_19A_1;
			new BaseButton(base.getTransformByPath("find_info/find"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag = this.findid == 0;
				if (flag)
				{
					BaseProxy<A3_ActiveProxy>.getInstance().SendPVP(2);
				}
				else
				{
					bool flag2 = ModelBase<A3_ActiveModel>.getInstance().buy_cnt <= ModelBase<A3_ActiveModel>.getInstance().buyCount;
					if (flag2)
					{
						flytxt.instance.fly("可购买次数不足", 0, default(Color), null);
					}
					else
					{
						BaseProxy<A3_ActiveProxy>.getInstance().SendPVP(4);
					}
				}
			};
			new BaseButton(base.getTransformByPath("GetReward/tem1/back"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("GetReward").gameObject.SetActive(false);
			};
			BaseButton arg_219_0 = new BaseButton(base.getTransformByPath("GetReward/tem1/Get"), 1, 1);
			Action<GameObject> arg_219_1;
			if ((arg_219_1 = a3_active_pvp.<>c.<>9__15_4) == null)
			{
				arg_219_1 = (a3_active_pvp.<>c.<>9__15_4 = new Action<GameObject>(a3_active_pvp.<>c.<>9.<init>b__15_4));
			}
			arg_219_0.onClick = arg_219_1;
			new BaseButton(base.getTransformByPath("GetReward/tem1/help"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("GetReward/tem1").gameObject.SetActive(false);
				base.transform.FindChild("GetReward/tem2").gameObject.SetActive(true);
				this.intoTab();
			};
			new BaseButton(base.getTransformByPath("GetReward/tem2/back"), 1, 1).onClick = delegate(GameObject go)
			{
				base.transform.FindChild("GetReward/tem1").gameObject.SetActive(true);
				base.transform.FindChild("GetReward/tem2").gameObject.SetActive(false);
			};
			BaseButton arg_298_0 = new BaseButton(base.getTransformByPath("Finding/back"), 1, 1);
			Action<GameObject> arg_298_1;
			if ((arg_298_1 = a3_active_pvp.<>c.<>9__15_7) == null)
			{
				arg_298_1 = (a3_active_pvp.<>c.<>9__15_7 = new Action<GameObject>(a3_active_pvp.<>c.<>9.<init>b__15_7));
			}
			arg_298_0.onClick = arg_298_1;
			this.ref_zuan_count();
		}

		public override void onShowed()
		{
			this.tip.SetActive(false);
			BaseProxy<A3_ActiveProxy>.getInstance().addEventListener(A3_ActiveProxy.EVENT_PVPSITE_INFO, new Action<GameEvent>(this.Refresh));
			bool flag = this.b;
			if (flag)
			{
				BaseProxy<A3_ActiveProxy>.getInstance().SendPVP(1);
				this.b = false;
			}
			a3_active_pvp.instance = this;
			this.refro_score();
			BaseProxy<A3_ActiveProxy>.getInstance().SendPVP(6);
			this.refCount();
		}

		public override void onClose()
		{
			this.refInto();
			a3_active_pvp.instance = null;
			BaseProxy<A3_ActiveProxy>.getInstance().removeEventListener(A3_ActiveProxy.EVENT_PVPSITE_INFO, new Action<GameEvent>(this.Refresh));
		}

		private void refresh_opentime()
		{
			List<SXML> nodeList = XMLMgr.instance.GetSXML("jjc", "").GetNodeList("t", "");
			int i = 0;
			int num = (DateTime.Now - DateTime.UtcNow).Hours;
			bool flag = num < 0;
			if (flag)
			{
				num += 12;
			}
			int num2 = (muNetCleint.instance.CurServerTimeStamp / 3600 + num) % 24;
			while (i < nodeList.Count)
			{
				int num3 = int.Parse(nodeList[i].getString("opnetime").Split(new char[]
				{
					','
				})[0]);
				bool flag2 = num2 < num3;
				if (flag2)
				{
					this.no_open.transform.FindChild("text1").GetComponent<Text>().text = string.Format("{0}点开放", num3);
					break;
				}
				i++;
			}
			bool flag3 = nodeList.Count > 0 && i == nodeList.Count;
			if (flag3)
			{
				this.no_open.transform.FindChild("text1").GetComponent<Text>().text = string.Format("明日{0}点开放", int.Parse(nodeList[0].getString("opnetime").Split(new char[]
				{
					','
				})[0]));
			}
		}

		public void setbtn(bool open)
		{
			this.no_open.SetActive(!open);
			this.yes_open.SetActive(open);
			this.refresh_opentime();
		}

		private void intoTab()
		{
			bool flag = this.frist;
			if (flag)
			{
				Transform transform = base.transform.FindChild("GetReward/tem2/scrollview/con");
				bool flag2 = transform.childCount > 0;
				if (flag2)
				{
					for (int i = 0; i < transform.childCount; i++)
					{
						UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
					}
				}
				GameObject gameObject = base.transform.FindChild("GetReward/tem2/scrollview/item").gameObject;
				List<SXML> sXMLList = XMLMgr.instance.GetSXMLList("jjc.reward", "");
				int count = sXMLList.Count;
				for (int j = count; j > 0; j--)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					SXML sXML = XMLMgr.instance.GetSXML("jjc.reward", "grade==" + j);
					string @string = sXML.getString("name");
					Image component = gameObject2.transform.FindChild("icon").GetComponent<Image>();
					bool flag3 = j < 10;
					if (flag3)
					{
						component.sprite = (Resources.Load("icon/rank/00" + j, typeof(Sprite)) as Sprite);
					}
					else
					{
						component.sprite = (Resources.Load("icon/rank/0" + j, typeof(Sprite)) as Sprite);
					}
					string str = "";
					bool flag4 = j >= 10;
					if (flag4)
					{
						str = "<color=#FFA500>";
					}
					else
					{
						bool flag5 = j < 10 && j >= 7;
						if (flag5)
						{
							str = "<color=#FF00FF>";
						}
						else
						{
							bool flag6 = j < 7;
							if (flag6)
							{
								str = "<color=#00BFFF>";
							}
						}
					}
					bool flag7 = j == 1;
					if (flag7)
					{
						gameObject2.transform.FindChild("2/Text").GetComponent<Text>().text = "<color=#00BFFF>绑钻</color>" + 0;
						gameObject2.transform.FindChild("3/Text").GetComponent<Text>().text = "<color=#00BFFF>名望</color>" + 0;
						gameObject2.transform.FindChild("4/Text").GetComponent<Text>().text = str + "无</color>";
						gameObject2.transform.FindChild("4/icon").gameObject.SetActive(false);
					}
					else
					{
						int @int = sXML.GetNode("gem", "").getInt("num");
						int int2 = sXML.GetNode("rep", "").getInt("num");
						int box = sXML.GetNode("box", "").getInt("id");
						SXML sXML2 = XMLMgr.instance.GetSXML("item.item", "id==" + box);
						string string2 = sXML2.getString("item_name");
						gameObject2.transform.FindChild("2/Text").GetComponent<Text>().text = "<color=#00BFFF>绑钻</color>" + @int;
						gameObject2.transform.FindChild("3/Text").GetComponent<Text>().text = "<color=#00BFFF>名望</color>" + int2;
						gameObject2.transform.FindChild("4/Text").GetComponent<Text>().text = str + string2 + "</color>";
						gameObject2.transform.FindChild("4/icon").GetComponent<Image>().sprite = (Resources.Load("icon/item/" + sXML2.getString("icon_file"), typeof(Sprite)) as Sprite);
						new BaseButton(gameObject2.transform.FindChild("4"), 1, 1).onClick = delegate(GameObject go)
						{
							this.showtip((uint)box);
						};
					}
					gameObject2.transform.FindChild("1/Text").GetComponent<Text>().text = str + @string + "</color>";
					gameObject2.transform.SetParent(transform, false);
					gameObject2.SetActive(true);
				}
			}
			this.frist = false;
		}

		private void showtip(uint id)
		{
			this.tip.SetActive(true);
			a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(id);
			this.tip.transform.FindChild("text_bg/name/namebg").GetComponent<Text>().text = itemDataById.item_name;
			this.tip.transform.FindChild("text_bg/name/namebg").GetComponent<Text>().color = Globle.getColorByQuality(itemDataById.quality);
			bool flag = itemDataById.use_limit <= 0;
			if (flag)
			{
				this.tip.transform.FindChild("text_bg/name/dengji").GetComponent<Text>().text = "无限制";
			}
			else
			{
				this.tip.transform.FindChild("text_bg/name/dengji").GetComponent<Text>().text = itemDataById.use_limit + "转";
			}
			this.tip.transform.FindChild("text_bg/text").GetComponent<Text>().text = StringUtils.formatText(itemDataById.desc);
			this.tip.transform.FindChild("text_bg/iconbg/icon").GetComponent<Image>().sprite = (Resources.Load(itemDataById.file, typeof(Sprite)) as Sprite);
			new BaseButton(this.tip.transform.FindChild("close_btn"), 1, 1).onClick = delegate(GameObject oo)
			{
				this.tip.SetActive(false);
			};
		}

		public void refGiff()
		{
			bool flag = ModelBase<A3_ActiveModel>.getInstance().lastgrage <= 1;
			if (flag)
			{
				this.giftzuanshi.text = "0";
				this.giftmingwang.text = "0";
				this.box.text = "无";
			}
			else
			{
				SXML sXML = XMLMgr.instance.GetSXML("jjc.reward", "grade==" + ModelBase<A3_ActiveModel>.getInstance().lastgrage);
				this.giftzuanshi.text = sXML.GetNode("gem", "").getInt("num").ToString();
				this.giftmingwang.text = sXML.GetNode("rep", "").getInt("num").ToString();
				this.box.text = sXML.GetNode("box", "").getInt("id").ToString();
			}
		}

		public void openFind()
		{
			base.transform.FindChild("find_info").gameObject.SetActive(false);
			base.transform.FindChild("Finding").gameObject.SetActive(true);
		}

		public void CloseFind()
		{
			base.transform.FindChild("find_info").gameObject.SetActive(true);
			base.transform.FindChild("Finding").gameObject.SetActive(false);
		}

		public void refCount_buy(int Buy_Count)
		{
			this.refCount();
			this.reffind();
		}

		private void ref_zuan_count()
		{
			this.buy_Count_zuan.text = ModelBase<A3_ActiveModel>.getInstance().buy_zuan_count.ToString();
		}

		public void refStar(int Count)
		{
			bool flag = ModelBase<A3_ActiveModel>.getInstance().grade <= 0;
			if (!flag)
			{
				SXML sXML = XMLMgr.instance.GetSXML("jjc.reward", "grade==" + ModelBase<A3_ActiveModel>.getInstance().grade);
				int @int = sXML.getInt("star");
				bool flag2 = @int <= 0;
				if (!flag2)
				{
					Transform transform = base.transform.FindChild("info/star");
					for (int i = 0; i < transform.childCount; i++)
					{
						transform.GetChild(i).FindChild("this").gameObject.SetActive(false);
						transform.GetChild(i).gameObject.SetActive(false);
					}
					for (int j = @int; j > 0; j--)
					{
						transform.GetChild(j - 1).gameObject.SetActive(true);
					}
					for (int k = 0; k < Count; k++)
					{
						transform.GetChild(k).FindChild("this").gameObject.SetActive(true);
					}
				}
			}
		}

		public void refCount(int Count)
		{
			this.findCount.text = ModelBase<A3_ActiveModel>.getInstance().callenge_cnt - Count + ModelBase<A3_ActiveModel>.getInstance().buyCount + "/" + ModelBase<A3_ActiveModel>.getInstance().callenge_cnt;
			this.reffind();
		}

		public void refInto()
		{
			base.transform.FindChild("GetReward/tem1").gameObject.SetActive(true);
			base.transform.FindChild("GetReward/tem2").gameObject.SetActive(false);
			base.transform.FindChild("GetReward").gameObject.SetActive(false);
			base.transform.FindChild("find_info").gameObject.SetActive(true);
			base.transform.FindChild("Finding").gameObject.SetActive(false);
		}

		private void Refresh(GameEvent e = null)
		{
			Variant data = e.data;
			bool flag = data["grade"] <= 0;
			if (!flag)
			{
				this.top_saiji.text = "第" + data["tour_time"] + "赛季";
				SXML sXML = XMLMgr.instance.GetSXML("jjc.reward", "grade==" + data["grade"]);
				this.duanwei.text = "段位：" + sXML.getString("name");
				bool flag2 = data["grade"] < 10;
				if (flag2)
				{
					this.rank.sprite = (Resources.Load("icon/rank/00" + data["grade"], typeof(Sprite)) as Sprite);
				}
				else
				{
					this.rank.sprite = (Resources.Load("icon/rank/0" + data["grade"], typeof(Sprite)) as Sprite);
				}
				this.refCount();
				bool flag3 = data["rec_rwd"] == 0;
				if (flag3)
				{
					base.transform.FindChild("reward/has").gameObject.SetActive(false);
					base.transform.FindChild("GetReward/tem1/Get").GetComponent<Button>().interactable = false;
				}
				else
				{
					base.transform.FindChild("reward/has").gameObject.SetActive(true);
					base.transform.FindChild("GetReward/tem1/Get").GetComponent<Button>().interactable = true;
				}
				this.refStar(data["score"]);
				this.reffind();
			}
		}

		public void refCount()
		{
			this.findCount.text = ModelBase<A3_ActiveModel>.getInstance().callenge_cnt - ModelBase<A3_ActiveModel>.getInstance().pvpCount + ModelBase<A3_ActiveModel>.getInstance().buyCount + "/" + ModelBase<A3_ActiveModel>.getInstance().callenge_cnt;
			this.buyCount.text = ModelBase<A3_ActiveModel>.getInstance().buyCount + "/ " + ModelBase<A3_ActiveModel>.getInstance().buy_cnt;
		}

		private void reffind()
		{
			bool flag = ModelBase<A3_ActiveModel>.getInstance().callenge_cnt - ModelBase<A3_ActiveModel>.getInstance().pvpCount + ModelBase<A3_ActiveModel>.getInstance().buyCount <= 0;
			if (flag)
			{
				this.findid = 1;
				base.transform.FindChild("find_info/find/text1").gameObject.SetActive(false);
				base.transform.FindChild("find_info/find/text2").gameObject.SetActive(true);
			}
			else
			{
				base.transform.FindChild("find_info/find/text1").gameObject.SetActive(true);
				base.transform.FindChild("find_info/find/text2").gameObject.SetActive(false);
				this.findid = 0;
			}
		}

		public void refro_score()
		{
			bool flag = ModelBase<A3_ActiveModel>.getInstance().grade <= 0;
			if (!flag)
			{
				SXML sXML = XMLMgr.instance.GetSXML("jjc.reward", "grade==" + ModelBase<A3_ActiveModel>.getInstance().grade);
				this.duanwei.text = "段位：" + sXML.getString("name");
				bool flag2 = ModelBase<A3_ActiveModel>.getInstance().grade < 10;
				if (flag2)
				{
					this.rank.sprite = (Resources.Load("icon/rank/00" + ModelBase<A3_ActiveModel>.getInstance().grade, typeof(Sprite)) as Sprite);
				}
				else
				{
					this.rank.sprite = (Resources.Load("icon/rank/0" + ModelBase<A3_ActiveModel>.getInstance().grade, typeof(Sprite)) as Sprite);
				}
				this.refStar(ModelBase<A3_ActiveModel>.getInstance().score);
			}
		}
	}
}
