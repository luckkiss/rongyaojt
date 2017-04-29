using Cross;
using DG.Tweening;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_fb_finish : FloatUi
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_fb_finish.<>c <>9 = new a3_fb_finish.<>c();

			public static Action<GameObject> <>9__42_1;

			public static Action<GameObject> <>9__42_2;

			public static Action<GameObject> <>9__42_3;

			public static Action<GameObject> <>9__42_4;

			internal void <init>b__42_1(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_BAG, null, false);
			}

			internal void <init>b__42_2(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.SKILL_A3, null, false);
			}

			internal void <init>b__42_3(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_SUMMON, null, false);
			}

			internal void <init>b__42_4(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_EQUIP, null, false);
			}
		}

		public static a3_fb_finish instance;

		private Text closeTime;

		private Text finishTiem;

		private Text kmNum;

		private Text getNum;

		private Text goldNum;

		private Text text1;

		private Text text2;

		private double closetime;

		private bool _NewOne;

		private double finishtime;

		private int kmnum;

		private uint getnum;

		private uint goldnum;

		private int getach;

		private int getmoney;

		private int shengwu;

		public double close_time = 0.0;

		private string icon;

		private string icon1;

		private bool closefb_way;

		private GameObject m_SelfObj;

		private GameObject m_Self_Camera;

		private ProfessionAvatar m_proAvatar;

		private Transform model;

		private GameObject bgwin;

		private GameObject bgdefet;

		private GameObject image_S;

		private GameObject image_A;

		private GameObject image_B;

		private GameObject reward;

		private GameObject contain;

		private GameObject pic_icon;

		private GameObject ar_result;

		private GameObject itempicc;

		private GameObject jjc;

		private GameObject yiwufb_defet;

		private GameObject icon_star;

		public List<int> pos_i = new List<int>();

		private int score;

		private uint ltpid;

		public static BaseRoomItem room;

		public override void init()
		{
			this.closefb_way = false;
			this.yiwufb_defet = base.transform.FindChild("yiwufb_defet").gameObject;
			this.bgwin = base.transform.FindChild("result_bg/bgwin").gameObject;
			this.bgdefet = base.transform.FindChild("result_bg/bgdefet").gameObject;
			this.image_S = base.transform.FindChild("win/success/icon/Images").gameObject;
			this.image_A = base.transform.FindChild("win/success/icon/Imagea").gameObject;
			this.image_B = base.transform.FindChild("win/success/icon/Imageb").gameObject;
			this.reward = base.transform.FindChild("win/success/gift/reward").gameObject;
			this.contain = base.transform.FindChild("win/success/gift/contain").gameObject;
			this.pic_icon = base.transform.FindChild("ar_result/icon").gameObject;
			this.ar_result = base.transform.FindChild("ar_result").gameObject;
			this.text1 = this.ar_result.transform.FindChild("Text1").GetComponent<Text>();
			this.text2 = this.ar_result.transform.FindChild("Text2").GetComponent<Text>();
			this.jjc = base.transform.FindChild("jjc").gameObject;
			this.icon_star = this.jjc.transform.FindChild("iocn").gameObject;
			this.close_time = 0.0;
			this.model = base.transform.FindChild("model");
			new BaseButton(base.transform.FindChild("btn_close"), 1, 1).onClick = delegate(GameObject go)
			{
				bool flag = !this.closefb_way;
				if (flag)
				{
					a3_insideui_fb.instance.light_biu.gameObject.SetActive(true);
					a3_insideui_fb.instance.exittime.gameObject.SetActive(true);
					a3_insideui_fb.instance.close_time = this.closetime;
				}
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_FB_FINISH);
			};
			this.closeTime = base.transform.FindChild("btn_close/closeTime").GetComponent<Text>();
			base.transform.FindChild("btn_close/closeTime").gameObject.SetActive(false);
			BaseButton arg_24C_0 = new BaseButton(base.transform.FindChild("win/fail/icon/bag"), 1, 1);
			Action<GameObject> arg_24C_1;
			if ((arg_24C_1 = a3_fb_finish.<>c.<>9__42_1) == null)
			{
				arg_24C_1 = (a3_fb_finish.<>c.<>9__42_1 = new Action<GameObject>(a3_fb_finish.<>c.<>9.<init>b__42_1));
			}
			arg_24C_0.onClick = arg_24C_1;
			BaseButton arg_288_0 = new BaseButton(base.transform.FindChild("win/fail/icon/skill"), 1, 1);
			Action<GameObject> arg_288_1;
			if ((arg_288_1 = a3_fb_finish.<>c.<>9__42_2) == null)
			{
				arg_288_1 = (a3_fb_finish.<>c.<>9__42_2 = new Action<GameObject>(a3_fb_finish.<>c.<>9.<init>b__42_2));
			}
			arg_288_0.onClick = arg_288_1;
			BaseButton arg_2C4_0 = new BaseButton(base.transform.FindChild("win/fail/icon/zhs"), 1, 1);
			Action<GameObject> arg_2C4_1;
			if ((arg_2C4_1 = a3_fb_finish.<>c.<>9__42_3) == null)
			{
				arg_2C4_1 = (a3_fb_finish.<>c.<>9__42_3 = new Action<GameObject>(a3_fb_finish.<>c.<>9.<init>b__42_3));
			}
			arg_2C4_0.onClick = arg_2C4_1;
			BaseButton arg_300_0 = new BaseButton(base.transform.FindChild("win/fail/icon/dz"), 1, 1);
			Action<GameObject> arg_300_1;
			if ((arg_300_1 = a3_fb_finish.<>c.<>9__42_4) == null)
			{
				arg_300_1 = (a3_fb_finish.<>c.<>9__42_4 = new Action<GameObject>(a3_fb_finish.<>c.<>9.<init>b__42_4));
			}
			arg_300_0.onClick = arg_300_1;
			this.jjc.SetActive(false);
		}

		public override void onShowed()
		{
			InterfaceMgr.getInstance().closeAllWin("");
			a3_insideui_fb expr_23 = a3_insideui_fb.instance;
			if (expr_23 != null)
			{
				expr_23.enter_pic2.SetActive(false);
			}
			this.jjc.SetActive(false);
			this.ar_result.SetActive(false);
			base.getGameObjectByPath("win/success").SetActive(false);
			base.getGameObjectByPath("state_successed").SetActive(false);
			base.transform.FindChild("state_successed/bg/goldNum").gameObject.SetActive(false);
			base.transform.FindChild("state_successed/bg/getNum").gameObject.SetActive(false);
			this.bgdefet.SetActive(false);
			this.bgwin.SetActive(false);
			this.yiwufb_defet.SetActive(false);
			base.transform.FindChild("btn_close/closeTime").gameObject.SetActive(false);
			this.closetime = 0.0;
			this.close_time = 0.0;
			Variant variant = (Variant)this.uiData[0];
			bool flag = variant.ContainsKey("ltpid");
			if (flag)
			{
				this.ltpid = variant["ltpid"];
			}
			bool flag2 = variant.ContainsKey("score");
			if (flag2)
			{
				this.score = variant["score"];
			}
			else
			{
				this.score = 0;
			}
			bool flag3 = variant.ContainsKey("close_tm");
			if (flag3)
			{
				double num = variant["close_tm"];
				this.closetime = num;
			}
			bool flag4 = variant.ContainsKey("win");
			if (flag4)
			{
				int num2 = variant["win"];
				Transform transformByPath = base.getTransformByPath("win");
				transformByPath.gameObject.SetActive(true);
				bool flag5 = num2 > 0 && transformByPath != null;
				if (flag5)
				{
					transformByPath.FindChild("success").gameObject.SetActive(true);
					transformByPath.FindChild("fail").gameObject.SetActive(false);
				}
				else
				{
					bool flag6 = transformByPath != null;
					if (flag6)
					{
						this.closeWindow();
						bool flag7 = GameObject.Find("GAME_CAMERA/myCamera");
						if (flag7)
						{
							GameObject gameObject = GameObject.Find("GAME_CAMERA/myCamera");
							bool flag8 = !gameObject.GetComponent<DeathShader>();
							if (flag8)
							{
								gameObject.AddComponent<DeathShader>();
							}
							else
							{
								gameObject.GetComponent<DeathShader>().enabled = true;
							}
						}
						transformByPath.FindChild("success").gameObject.SetActive(false);
						transformByPath.FindChild("fail").gameObject.SetActive(true);
						base.getGameObjectByPath("state_successed").SetActive(false);
					}
				}
				bool flag9 = variant.ContainsKey("item_drop");
				if (flag9)
				{
					num2 = variant["item_drop"]._arr.Count;
				}
				else
				{
					num2 = 0;
				}
				bool flag10 = num2 >= 0;
				if (flag10)
				{
					base.getGameObjectByPath("state_successed").SetActive(true);
					this.finishTiem = base.getComponentByPath<Text>("state_successed/bg/fnTime/time");
					this.kmNum = base.getComponentByPath<Text>("state_successed/bg/kmNum/num");
					this.getNum = base.getComponentByPath<Text>("state_successed/bg/getNum/num");
					this.goldNum = base.getComponentByPath<Text>("state_successed/bg/goldNum/num");
				}
				else
				{
					base.getGameObjectByPath("state_successed").SetActive(false);
				}
			}
			this.finishtime = 0.0;
			bool flag11 = this.uiData.Count > 1;
			if (flag11)
			{
				this.finishtime = (double)this.uiData[1];
			}
			float tss = 0f;
			uint tkn = 0u;
			uint ten = 0u;
			uint tgn = 0u;
			int ach = 0;
			int mon = 0;
			DOTween.To(() => tss, delegate(float s)
			{
				TimeSpan timeSpan = new TimeSpan(0, 0, (int)s);
				this.finishTiem.text = string.Concat(new object[]
				{
					(int)timeSpan.TotalHours,
					":",
					timeSpan.Minutes,
					":",
					timeSpan.Seconds
				});
			}, (float)this.finishtime, 1f);
			this.evaluation(this.score);
			this.kmnum = 0;
			bool flag12 = this.uiData.Count > 2;
			if (flag12)
			{
				this.kmnum = (int)this.uiData[2];
			}
			DOTween.To(() => (int)tkn, delegate(int s)
			{
				tkn = (uint)s;
				this.kmNum.text = tkn.ToString();
			}, this.kmnum, 1f);
			bool flag13 = a3_fb_finish.room is MoneyRoom;
			if (flag13)
			{
				bool flag14 = a3_fb_finish.room != null;
				if (flag14)
				{
					base.transform.FindChild("state_successed/bg/goldNum").gameObject.SetActive(true);
				}
			}
			bool flag15 = a3_fb_finish.room is ExpRoom;
			if (flag15)
			{
				bool flag16 = a3_fb_finish.room != null;
				if (flag16)
				{
					base.transform.FindChild("state_successed/bg/getNum").gameObject.SetActive(true);
				}
			}
			bool flag17 = a3_fb_finish.room is PVPRoom;
			if (flag17)
			{
				bool flag18 = a3_fb_finish.room != null;
				if (flag18)
				{
					this.getach = a3_fb_finish.room.getach;
				}
				DOTween.To(() => ach, delegate(int s)
				{
					ach = s;
					this.getNum.text = ach.ToString();
				}, this.getach, 1f);
				bool flag19 = a3_fb_finish.room != null;
				if (flag19)
				{
					this.getmoney = a3_fb_finish.room.getExp;
				}
				DOTween.To(() => mon, delegate(int s)
				{
					mon = s;
					this.goldNum.text = mon.ToString();
				}, this.getmoney, 1f);
				BaseProxy<MapProxy>.getInstance().Win_uiData = "pvp";
				BaseProxy<MapProxy>.getInstance().openWin = InterfaceMgr.A3_ACTIVE;
			}
			else
			{
				bool flag20 = a3_fb_finish.room != null;
				if (flag20)
				{
					this.getnum = a3_fb_finish.room.expnum;
				}
				DOTween.To(() => ten, delegate(uint s)
				{
					ten = s;
					this.getNum.text = ten.ToString();
				}, this.getnum, 1f);
				bool flag21 = a3_fb_finish.room != null;
				if (flag21)
				{
					this.goldnum = a3_fb_finish.room.goldnum;
				}
				DOTween.To(() => tgn, delegate(uint s)
				{
					tgn = s;
					this.goldNum.text = tgn.ToString();
				}, this.goldnum, 1f);
			}
			this._NewOne = true;
			a3_fb_finish.instance = this;
			a3_fb_finish.room.getExp = 0;
			a3_fb_finish.room.getach = 0;
			Variant variant2 = SvrLevelConfig.instacne.get_level_data(this.ltpid);
			bool flag22 = variant["win"] == 0 || a3_fb_finish.room is PVPRoom || variant2.ContainsKey("shengwu") || a3_fb_finish.room is PlotRoom;
			if (flag22)
			{
				a3_liteMinimap expr_6EB = a3_liteMinimap.instance;
				if (expr_6EB != null)
				{
					GameObject expr_6F6 = expr_6EB.taskinfo;
					if (expr_6F6 != null)
					{
						expr_6F6.SetActive(true);
					}
				}
				a3_insideui_fb expr_708 = a3_insideui_fb.instance;
				if (expr_708 != null)
				{
					GameObject expr_713 = expr_708.enter_pic2;
					if (expr_713 != null)
					{
						expr_713.SetActive(false);
					}
				}
				BaseProxy<LevelProxy>.getInstance().open_pic = false;
				this.closefb_way = true;
			}
			else
			{
				this.closefb_way = false;
			}
			bool flag23 = this.closefb_way;
			if (flag23)
			{
				this.close_time = 0.0;
				base.transform.FindChild("btn_close/closeTime").gameObject.SetActive(true);
			}
			bool flag24 = !this.closefb_way;
			if (flag24)
			{
				this.close_time = this.closetime - (double)muNetCleint.instance.CurServerTimeStamp - 3.0;
				base.transform.FindChild("btn_close/closeTime").gameObject.SetActive(false);
			}
			bool flag25 = variant2.ContainsKey("shengwu") && variant2.ContainsKey("icon");
			if (flag25)
			{
				this.shengwu = variant2["shengwu"];
				this.icon = variant2["icon"];
				bool flag26 = variant["win"] == 0;
				if (flag26)
				{
					this.jjc.SetActive(false);
					this.ar_result.SetActive(false);
					base.getGameObjectByPath("win").SetActive(false);
					base.getGameObjectByPath("win/success").SetActive(false);
					base.getGameObjectByPath("state_successed").SetActive(false);
					base.transform.FindChild("state_successed/bg/goldNum").gameObject.SetActive(false);
					this.bgdefet.SetActive(false);
					this.bgwin.SetActive(false);
					this.yiwufb_defet.SetActive(true);
					return;
				}
			}
			else
			{
				this.shengwu = 0;
			}
			bool flag27 = variant["win"] == 1 && variant2.ContainsKey("shengwu") && variant2.ContainsKey("des");
			if (flag27)
			{
				this.icon1 = variant2["des"];
				string[] array = this.icon1.Split(new char[]
				{
					','
				});
				List<SXML> list = null;
				bool flag28 = list == null;
				if (flag28)
				{
					list = XMLMgr.instance.GetSXMLList("accent_relic.relic", "");
					for (int i = 0; i < list.Count; i++)
					{
						bool flag29 = list[i].getInt("carr") == ModelBase<PlayerModel>.getInstance().profession && list[i].getString("type") == array[0].ToString();
						if (flag29)
						{
							List<SXML> nodeList = list[i].GetNodeList("relic_god", "id==" + array[1].ToString());
							foreach (SXML current in nodeList)
							{
								this.text1.text = current.getString("des1");
								this.text2.text = current.getString("des2");
							}
						}
					}
				}
			}
			bool flag30 = this.shengwu == 1;
			if (flag30)
			{
				this.ar_result.SetActive(true);
				string[] array2 = this.icon.Split(new char[]
				{
					','
				});
				bool flag31 = ModelBase<PlayerModel>.getInstance().profession == 2;
				if (flag31)
				{
					this.pic_icon.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/ar/" + array2[0]);
				}
				bool flag32 = ModelBase<PlayerModel>.getInstance().profession == 3;
				if (flag32)
				{
					this.pic_icon.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/ar/" + array2[1]);
				}
				bool flag33 = ModelBase<PlayerModel>.getInstance().profession == 5;
				if (flag33)
				{
					this.pic_icon.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/ar/" + array2[2]);
				}
				this.bgwin.SetActive(false);
				base.getGameObjectByPath("win").SetActive(false);
				base.getGameObjectByPath("win/success").SetActive(false);
				base.transform.FindChild("state_successed").gameObject.SetActive(false);
				this.jjc.SetActive(false);
			}
			this.jjc.SetActive(false);
			bool flag34 = a3_fb_finish.room is PVPRoom;
			if (flag34)
			{
				this.jjc.SetActive(false);
				this.ar_result.SetActive(false);
				base.getGameObjectByPath("win").SetActive(false);
				base.getGameObjectByPath("state_successed").SetActive(false);
				this.bgdefet.SetActive(false);
				this.bgwin.SetActive(false);
				bool flag35 = variant.ContainsKey("win");
				if (flag35)
				{
					int num3 = variant["win"];
					bool flag36 = num3 == 0;
					if (flag36)
					{
						this.jjc.SetActive(true);
						this.jjc.transform.FindChild("vector").gameObject.SetActive(false);
						this.jjc.transform.FindChild("defet").gameObject.SetActive(true);
					}
					else
					{
						this.jjc.SetActive(true);
						this.jjc.transform.FindChild("vector").gameObject.SetActive(true);
						this.jjc.transform.FindChild("defet").gameObject.SetActive(false);
					}
					int grade = ModelBase<A3_ActiveModel>.getInstance().grade;
					bool flag37 = grade < 10;
					if (flag37)
					{
						this.icon_star.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/rank/00" + grade);
					}
					else
					{
						this.icon_star.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/rank/0" + grade);
					}
					bool flag38 = ModelBase<A3_ActiveModel>.getInstance().grade <= 0;
					if (!flag38)
					{
						SXML sXML = XMLMgr.instance.GetSXML("jjc.reward", "grade==" + ModelBase<A3_ActiveModel>.getInstance().grade);
						int @int = sXML.getInt("star");
						bool flag39 = @int <= 0;
						if (!flag39)
						{
							Transform transform = base.transform.FindChild("jjc/star");
							for (int j = 0; j < transform.childCount; j++)
							{
								transform.GetChild(j).FindChild("this").gameObject.SetActive(false);
								transform.GetChild(j).gameObject.SetActive(false);
							}
							for (int k = @int; k > 0; k--)
							{
								transform.GetChild(k - 1).gameObject.SetActive(true);
							}
							for (int l = 0; l < ModelBase<A3_ActiveModel>.getInstance().score; l++)
							{
								transform.GetChild(l).FindChild("this").gameObject.SetActive(true);
							}
						}
					}
				}
			}
		}

		public void destorycontain()
		{
			bool flag = this.contain.transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < this.contain.transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.contain.transform.GetChild(i).gameObject);
				}
			}
		}

		private void closeWindow()
		{
			int i = 0;
			while (i < base.transform.parent.childCount)
			{
				bool activeSelf = base.transform.parent.GetChild(i).gameObject.activeSelf;
				if (activeSelf)
				{
					this.pos_i.Add(i);
					bool flag = base.transform.parent.GetChild(i).gameObject == base.gameObject;
					if (!flag)
					{
						base.transform.parent.GetChild(i).gameObject.SetActive(false);
					}
				}
				IL_7A:
				i++;
				continue;
				goto IL_7A;
			}
		}

		private void openWindow()
		{
			for (int i = 0; i < this.pos_i.Count; i++)
			{
				bool flag = !base.transform.parent.GetChild(this.pos_i[i]).gameObject.activeSelf;
				if (flag)
				{
					base.transform.parent.GetChild(this.pos_i[i]).gameObject.SetActive(true);
				}
			}
		}

		public override void onClosed()
		{
			this.openWindow();
			this.disposeAvatar();
			a3_fb_finish.room = null;
			this._NewOne = false;
			a3_fb_finish.instance = null;
			bool flag = this.closefb_way;
			if (flag)
			{
				BaseProxy<LevelProxy>.getInstance().sendLeave_lvl();
			}
			bool flag2 = GameObject.Find("GAME_CAMERA/myCamera");
			if (flag2)
			{
				GameObject gameObject = GameObject.Find("GAME_CAMERA/myCamera");
				bool flag3 = gameObject.GetComponent<DeathShader>();
				if (flag3)
				{
					gameObject.GetComponent<DeathShader>().enabled = false;
				}
			}
		}

		public void SetGetGold(int num)
		{
			this.getNum.text = ((long)num + (long)((ulong)this.getnum)).ToString();
		}

		private void Update()
		{
			bool flag = !this._NewOne;
			if (!flag)
			{
				double num = this.closetime - (double)muNetCleint.instance.CurServerTimeStamp;
				this.closeTime.text = "<color=#ff0000>(" + (int)num + ")</color>";
				bool flag2 = this.closefb_way;
				if (flag2)
				{
					InterfaceMgr.getInstance().close(InterfaceMgr.A3_INSIDEUI_FB);
				}
				bool flag3 = num < this.close_time;
				if (flag3)
				{
					this._NewOne = false;
					bool flag4 = !this.closefb_way;
					if (flag4)
					{
						a3_insideui_fb.instance.light_biu.gameObject.SetActive(true);
						a3_insideui_fb.instance.exittime.gameObject.SetActive(true);
						a3_insideui_fb.instance.close_time = this.closetime;
					}
					InterfaceMgr.getInstance().close(InterfaceMgr.A3_FB_FINISH);
				}
				bool flag5 = this.m_proAvatar != null;
				if (flag5)
				{
					this.m_proAvatar.FrameMove();
				}
			}
		}

		public void OnLvFinish(Variant data)
		{
		}

		public void OnAwd()
		{
		}

		public void itempic()
		{
			this.destorycontain();
			bool flag = !BaseProxy<LevelProxy>.getInstance().is_open;
			if (flag)
			{
				bool flag2 = BaseProxy<LevelProxy>.getInstance().reward != null;
				if (flag2)
				{
					for (int i = 0; i < BaseProxy<LevelProxy>.getInstance().reward.Count; i++)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.reward);
						gameObject.SetActive(true);
						gameObject.transform.SetParent(this.contain.transform, false);
						this.itempicc = gameObject.transform.FindChild("pic").gameObject;
						int tpid = BaseProxy<LevelProxy>.getInstance().reward[i].tpid;
						int quality = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)tpid).quality;
						bool flag3 = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)tpid).equip_type != -1;
						if (flag3)
						{
							gameObject.transform.FindChild("pic/bg").gameObject.SetActive(false);
							gameObject.transform.FindChild("pic/num").gameObject.SetActive(false);
						}
						gameObject.transform.FindChild("quality_bg/" + quality).gameObject.SetActive(true);
						this.itempicc.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/item/" + tpid);
					}
				}
			}
			bool is_open = BaseProxy<LevelProxy>.getInstance().is_open;
			if (is_open)
			{
				bool flag4 = BaseRoomItem.instance.list2.Count != 0;
				if (flag4)
				{
					for (int j = 0; j < BaseRoomItem.instance.list2.Count; j++)
					{
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.reward);
						gameObject2.SetActive(true);
						gameObject2.transform.SetParent(this.contain.transform, false);
						this.itempicc = gameObject2.transform.FindChild("pic").gameObject;
						this.itempicc.transform.FindChild("num").gameObject.SetActive(true);
						this.itempicc.transform.FindChild("num").gameObject.GetComponent<Text>().text = BaseRoomItem.instance.list2[j].num.ToString();
						int tpid2 = BaseRoomItem.instance.list2[j].tpid;
						int quality2 = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)tpid2).quality;
						bool flag5 = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)tpid2).equip_type != -1;
						if (flag5)
						{
							gameObject2.transform.FindChild("pic/bg").gameObject.SetActive(false);
							gameObject2.transform.FindChild("pic/num").gameObject.SetActive(false);
						}
						gameObject2.transform.FindChild("quality_bg/" + quality2).gameObject.SetActive(true);
						this.itempicc.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("icon/item/" + tpid2);
					}
				}
				BaseRoomItem.instance.list2.Clear();
				BaseProxy<LevelProxy>.getInstance().is_open = false;
			}
		}

		public void change_sprite()
		{
			this.bgdefet.SetActive(false);
			this.bgwin.SetActive(true);
			this.image_S.SetActive(false);
			this.image_B.SetActive(false);
			this.image_A.SetActive(false);
			base.getGameObjectByPath("win/success").SetActive(true);
			base.getGameObjectByPath("state_successed").SetActive(true);
		}

		public void evaluation(int score)
		{
			bool flag = score == 3;
			if (flag)
			{
				this.change_sprite();
				this.image_S.SetActive(true);
				this.itempic();
			}
			bool flag2 = score == 2;
			if (flag2)
			{
				this.change_sprite();
				this.image_A.SetActive(true);
				this.itempic();
			}
			bool flag3 = score == 1;
			if (flag3)
			{
				this.change_sprite();
				this.image_B.SetActive(true);
				this.itempic();
			}
			bool flag4 = score == 0;
			if (flag4)
			{
				this.bgwin.SetActive(false);
				this.bgdefet.SetActive(true);
				base.getGameObjectByPath("win/success").SetActive(false);
				base.getGameObjectByPath("win/fail").SetActive(true);
				base.getGameObjectByPath("state_successed").SetActive(false);
			}
		}

		public void createAvatar()
		{
			bool flag = this.m_SelfObj == null;
			if (flag)
			{
				bool flag2 = SelfRole._inst is P2Warrior;
				GameObject original;
				if (flag2)
				{
					original = Resources.Load<GameObject>("profession/avatar_ui/warrior_avatar");
					this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(this.model.localPosition.x, this.model.localPosition.y, this.model.localPosition.z), this.model.localRotation) as GameObject);
				}
				else
				{
					bool flag3 = SelfRole._inst is P3Mage;
					if (flag3)
					{
						original = Resources.Load<GameObject>("profession/avatar_ui/mage_avatar");
						this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(this.model.localPosition.x, this.model.localPosition.y, this.model.localPosition.z), this.model.localRotation) as GameObject);
					}
					else
					{
						bool flag4 = SelfRole._inst is P5Assassin;
						if (!flag4)
						{
							return;
						}
						original = Resources.Load<GameObject>("profession/avatar_ui/assa_avatar");
						this.m_SelfObj = (UnityEngine.Object.Instantiate(original, new Vector3(this.model.localPosition.x, this.model.localPosition.y, this.model.localPosition.z), this.model.localRotation) as GameObject);
					}
				}
				Transform[] componentsInChildren = this.m_SelfObj.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform = componentsInChildren[i];
					transform.gameObject.layer = EnumLayer.LM_FX;
				}
				Transform transform2 = this.m_SelfObj.transform.FindChild("model");
				bool flag5 = SelfRole._inst is P3Mage;
				if (flag5)
				{
					Transform parent = transform2.FindChild("R_Finger1");
					original = Resources.Load<GameObject>("profession/avatar_ui/mage_r_finger_fire");
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
					gameObject.transform.SetParent(parent, false);
				}
				this.m_proAvatar = new ProfessionAvatar();
				this.m_proAvatar.Init(SelfRole._inst.m_strAvatarPath, "h_", EnumLayer.LM_FX, EnumMaterial.EMT_EQUIP_H, transform2, SelfRole._inst.m_strEquipEffPath);
				bool flag6 = ModelBase<a3_EquipModel>.getInstance().active_eqp.Count >= 10;
				if (flag6)
				{
					this.m_proAvatar.set_equip_eff(ModelBase<a3_EquipModel>.getInstance().GetEqpIdbyType(3), true);
				}
				this.m_proAvatar.set_body(SelfRole._inst.get_bodyid(), SelfRole._inst.get_bodyfxid());
				this.m_proAvatar.set_weaponl(SelfRole._inst.get_weaponl_id(), SelfRole._inst.get_weaponl_fxid());
				this.m_proAvatar.set_weaponr(SelfRole._inst.get_weaponr_id(), SelfRole._inst.get_weaponr_fxid());
				this.m_proAvatar.set_wing(SelfRole._inst.get_wingid(), SelfRole._inst.get_windfxid());
				this.m_proAvatar.set_equip_color(SelfRole._inst.get_equip_colorid());
				original = Resources.Load<GameObject>("profession/avatar_ui/roleinfo_ui_camera");
				this.m_Self_Camera = UnityEngine.Object.Instantiate<GameObject>(original);
				Light componentInChildren = this.m_Self_Camera.GetComponentInChildren<Light>();
				bool flag7 = componentInChildren != null;
				if (flag7)
				{
					componentInChildren.color = Color.white;
				}
			}
		}

		public void disposeAvatar()
		{
			this.m_proAvatar = null;
			bool flag = this.m_SelfObj != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.m_SelfObj);
			}
			bool flag2 = this.m_Self_Camera != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.m_Self_Camera);
			}
		}
	}
}
