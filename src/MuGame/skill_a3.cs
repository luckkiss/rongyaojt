using Cross;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class skill_a3 : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly skill_a3.<>c <>9 = new skill_a3.<>c();

			public static Action<GameObject> <>9__67_4;

			internal void <init>b__67_4(GameObject go)
			{
				flytxt.instance.fly(ContMgr.getCont("func_limit_43", null), 0, default(Color), null);
			}
		}

		private GameObject[] skillgroupsone = new GameObject[4];

		private GameObject[] skillgrouptwo = new GameObject[4];

		private Dictionary<int, skill_a3Data> datas = ModelBase<Skill_a3Model>.getInstance().skilldic;

		private Dictionary<int, GameObject> dicobj = new Dictionary<int, GameObject>();

		private int times = 1;

		private GameObject skill_image;

		private GameObject commonimage = null;

		private GameObject image;

		private GameObject contain;

		private GameObject teackAni_go;

		private Text skill_name;

		private Text skill_lv;

		private Text skill_des;

		private Text skill_nowdes;

		private Text skill_nextdes;

		private Text needlvandzhuan;

		private Text needmoneys;

		private Button addlvbtn;

		private Text havepoint;

		private GameObject group1;

		private GameObject group2;

		private int nowid = 0;

		private Dictionary<int, runeData> runedatas = ModelBase<Skill_a3Model>.getInstance().runedic;

		private Dictionary<int, GameObject> runeobj = new Dictionary<int, GameObject>();

		private Dictionary<int, int> info_time = new Dictionary<int, int>();

		private bool istimein = false;

		private Dictionary<int, int> info_times = new Dictionary<int, int>();

		private GameObject imagerune0;

		private GameObject panel0;

		private GameObject imagerune1;

		private GameObject panel1;

		private GameObject imagerune2;

		private GameObject panel2;

		private GameObject runeinfosObj;

		private Text runename;

		private Text runelv;

		private Text runedes;

		private Text term;

		private Text term_time;

		private GameObject trem_timeImage;

		private GameObject studybtn;

		private GameObject costgoldbtn;

		private GameObject costgoldbtn_free;

		private GameObject costgoldbtn_nofree;

		private GameObject go;

		private GameObject gobefor;

		private GameObject gotext;

		private GameObject fx_sjcg;

		private Transform father;

		private Transform father2;

		private Text costgold_num;

		private EventTriggerListener tab1;

		private EventTriggerListener tab2;

		private GameObject rune_open;

		public static List<int> skills = new List<int>();

		public static Variant showdic = new Variant();

		public static Variant showdic2 = new Variant();

		private int sid;

		private int nowlv;

		public static skill_a3 _instance;

		public static bool show_teack_ani = false;

		public static int upgold;

		public static int upmojing;

		private int needmojingnum;

		private bool isfull = false;

		private uint needobj_skillId;

		private int neednum_skill;

		private GameObject gos = null;

		private bool isdragimage = false;

		private GameObject goss;

		private int num = -1;

		private bool lvorzhuan = false;

		private bool needmoney = false;

		private bool needmojing = false;

		private bool canlevel = false;

		private bool canmoney = false;

		private bool canmojing = false;

		private int oldmojing;

		private Dictionary<int, int> skill = new Dictionary<int, int>();

		private int sumMonry = 0;

		private int sumMojing = 0;

		private int fristid = 0;

		private Dictionary<int, int> canStudySkill = new Dictionary<int, int>();

		public bool toygyw = false;

		private bool islv = true;

		private bool ispre_lv = true;

		private bool isotherRuneAll_lv = true;

		public override void init()
		{
			skill_a3._instance = this;
			this.contain = base.transform.FindChild("skill/panel_left/skill_list/contain").gameObject;
			this.image = base.transform.FindChild("skill/panel_left/skill_list/skill").gameObject;
			this.skill_lv = base.getComponentByPath<Text>("skill/panel_right/bg/lv");
			this.skill_name = base.transform.FindChild("skill/panel_right/bg/name").GetComponent<Text>();
			this.skill_des = base.transform.FindChild("skill/panel_right/bg/Image00/des").GetComponent<Text>();
			this.skill_nowdes = base.transform.FindChild("skill/panel_right/bg/Image00/now/des").GetComponent<Text>();
			this.skill_nextdes = base.transform.FindChild("skill/panel_right/bg/Image00/next/des").GetComponent<Text>();
			this.needlvandzhuan = base.getComponentByPath<Text>("skill/panel_right/bg/Button/need0/lvandzhuan");
			this.needmoneys = base.getComponentByPath<Text>("skill/panel_right/bg/Button/need1/money");
			this.addlvbtn = base.getComponentByPath<Button>("skill/panel_right/bg/Button");
			this.havepoint = base.transform.FindChild("skill/panel_right/bg/Image02/point").GetComponent<Text>();
			this.group1 = base.transform.FindChild("skill/panel_right/skillgroupsone").gameObject;
			this.group2 = base.transform.FindChild("skill/panel_right/skillgroupstwo").gameObject;
			this.rune_open = base.transform.FindChild("rune/tishi_bg").gameObject;
			this.teackAni_go = base.transform.FindChild("teach_ani").gameObject;
			this.go = base.getGameObjectByPath("onekeyInfo/body");
			this.gobefor = base.getGameObjectByPath("showOneKey/body");
			this.father = base.getTransformByPath("onekeyInfo/bg/scroll/cont");
			this.father2 = base.getTransformByPath("showOneKey/bg/scroll/cont");
			this.fx_sjcg = Resources.Load<GameObject>("FX/uiFX/FX_ui_sjcg");
			for (int i = 0; i < 4; i++)
			{
				this.skillgroupsone[i] = base.transform.FindChild("skill/panel_right/skillgroupsone/" + i).gameObject;
				EventTriggerListener.Get(this.skillgroupsone[i]).onDown = new EventTriggerListener.VoidDelegate(this.OnBeginDragimage);
				EventTriggerListener.Get(this.skillgroupsone[i]).onUp = delegate(GameObject oe)
				{
					this.onDragEndimage(oe, Vector2.zero);
				};
				EventTriggerListener.Get(this.skillgroupsone[i]).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterimage);
				EventTriggerListener.Get(this.skillgroupsone[i]).onExit = new EventTriggerListener.VoidDelegate(this.OnExitimage);
				EventTriggerListener.Get(this.skillgroupsone[i]).onDrag = new EventTriggerListener.VectorDelegate(this.onDragimage);
				EventTriggerListener.Get(this.skillgroupsone[i]).onDragEnd = new EventTriggerListener.VectorDelegate(this.onDragEndimage);
			}
			for (int j = 0; j < 4; j++)
			{
				this.skillgrouptwo[j] = base.transform.FindChild("skill/panel_right/skillgroupstwo/" + j).gameObject;
				EventTriggerListener.Get(this.skillgrouptwo[j]).onDown = new EventTriggerListener.VoidDelegate(this.OnBeginDragimage);
				EventTriggerListener.Get(this.skillgrouptwo[j]).onUp = delegate(GameObject oe)
				{
					this.onDragEndimage(oe, Vector2.zero);
				};
				EventTriggerListener.Get(this.skillgrouptwo[j]).onEnter = new EventTriggerListener.VoidDelegate(this.OnEnterimage);
				EventTriggerListener.Get(this.skillgrouptwo[j]).onExit = new EventTriggerListener.VoidDelegate(this.OnExitimage);
				EventTriggerListener.Get(this.skillgrouptwo[j]).onDrag = new EventTriggerListener.VectorDelegate(this.onDragimage);
				EventTriggerListener.Get(this.skillgrouptwo[j]).onDragEnd = new EventTriggerListener.VectorDelegate(this.onDragEndimage);
			}
			BaseButton baseButton = new BaseButton(base.transform.FindChild("close"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onClose);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("panel/skill"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onOpskill);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("panel/rune"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onOprune);
			this.tab1 = base.getEventTrigerByPath("skill/panel_right/tab1");
			this.tab1.onClick = new EventTriggerListener.VoidDelegate(this.onTabone);
			this.tab2 = base.getEventTrigerByPath("skill/panel_right/tab2");
			this.tab2.onClick = new EventTriggerListener.VoidDelegate(this.onTabtwo);
			this.creatrve();
			this.imagerune0 = base.transform.FindChild("rune/bg00/rune_attack").gameObject;
			this.panel0 = base.transform.FindChild("rune/bg00/panel").gameObject;
			this.imagerune1 = base.transform.FindChild("rune/bg01/rune_defense").gameObject;
			this.panel1 = base.transform.FindChild("rune/bg01/panel").gameObject;
			this.imagerune2 = base.transform.FindChild("rune/bg02/rune_life").gameObject;
			this.panel2 = base.transform.FindChild("rune/bg02/panel").gameObject;
			this.runeinfosObj = base.transform.FindChild("rune/runeinfo").gameObject;
			this.runename = this.runeinfosObj.transform.FindChild("name").GetComponent<Text>();
			this.runelv = this.runeinfosObj.transform.FindChild("lv").GetComponent<Text>();
			this.runedes = this.runeinfosObj.transform.FindChild("Image/Image/effect/des").GetComponent<Text>();
			this.term = this.runeinfosObj.transform.FindChild("Image/Image/term/term_txt").GetComponent<Text>();
			this.term_time = this.runeinfosObj.transform.FindChild("Image/Image/term/termtime_txt").GetComponent<Text>();
			this.trem_timeImage = this.runeinfosObj.transform.FindChild("Image/Image/term/Image").gameObject;
			this.studybtn = this.runeinfosObj.transform.FindChild("studybtn").gameObject;
			this.costgoldbtn = this.runeinfosObj.transform.FindChild("costgoldbtn").gameObject;
			this.costgoldbtn_free = this.costgoldbtn.transform.FindChild("free").gameObject;
			this.costgoldbtn_nofree = this.costgoldbtn.transform.FindChild("nofree").gameObject;
			this.costgold_num = this.costgoldbtn_nofree.transform.FindChild("num").GetComponent<Text>();
			this.creatrverune();
			BaseButton baseButton4 = new BaseButton(this.runeinfosObj.transform.FindChild("costgoldbtn").transform, 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onAddspeedClick);
			BaseButton baseButton5 = new BaseButton(this.runeinfosObj.transform.FindChild("studybtn").transform, 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.onStudyRuneClick);
			int job = ModelBase<PlayerModel>.getInstance().profession * 1000;
			this.nils(skill_a3.skills);
			new BaseButton(base.getTransformByPath("showOneKey/yes"), 1, 1).onClick = delegate(GameObject go)
			{
				this.oldmojing = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(1540u);
				Variant variant = new Variant();
				for (int k = 0; k < skill_a3.skills.Count; k++)
				{
					bool flag2 = skill_a3.skills[k] != job + 1;
					if (flag2)
					{
						this.getGameObjectByPath("skill/panel_left/skill_list/contain/" + skill_a3.skills[k] + "/addlv").SetActive(false);
					}
				}
				int num = 0;
				foreach (KeyValuePair<int, int> current in this.skill)
				{
					Variant variant2 = new Variant();
					variant2["skill_id"] = current.Key;
					variant2["add_lvl"] = current.Value;
					variant.pushBack(variant2);
					num++;
				}
				BaseProxy<Skill_a3Proxy>.getInstance().sendSkillsneed(variant);
				this.getGameObjectByPath("showOneKey").SetActive(false);
				this.getTransformByPath("skill/panel_left/onekey").gameObject.SetActive(true);
			};
			new BaseButton(base.getTransformByPath("skill/panel_left/onekey"), 1, 1).onClick = delegate(GameObject go)
			{
				Variant variant = new Variant();
				skill_a3.skills.Sort();
				for (int k = 0; k < skill_a3.skills.Count; k++)
				{
					variant[k] = skill_a3.skills[k];
				}
				this.oneKey(skill_a3.skills);
				bool flag2 = this.isfull;
				if (flag2)
				{
					flytxt.instance.fly("已是最大等级", 0, default(Color), null);
				}
				else
				{
					bool flag3 = !this.canlevel;
					if (flag3)
					{
						flytxt.instance.fly("等级不足", 0, default(Color), null);
					}
					else
					{
						bool flag4 = !this.canmojing;
						if (flag4)
						{
							flytxt.instance.fly("魔晶不足", 0, default(Color), null);
							this.addtoget(ModelBase<a3_BagModel>.getInstance().getItemDataById(1540u));
						}
						else
						{
							bool flag5 = !this.canmoney;
							if (flag5)
							{
								flytxt.instance.fly("金币不足", 0, default(Color), null);
							}
							else
							{
								this.onekeyUp(skill_a3.skills);
								this.showUP(this.skill);
								base.getTransformByPath("skill/panel_left/onekey").gameObject.SetActive(false);
							}
						}
					}
				}
			};
			BaseProxy<Skill_a3Proxy>.getInstance().sendProxys(1, 0, false);
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.RUNE, false);
			if (flag)
			{
				this.setRune_open();
				base.getGameObjectByPath("panel/lockp").SetActive(false);
			}
			else
			{
				base.getGameObjectByPath("panel/lockp").SetActive(true);
			}
			BaseButton arg_833_0 = new BaseButton(base.getTransformByPath("panel/lockp"), 1, 1);
			Action<GameObject> arg_833_1;
			if ((arg_833_1 = skill_a3.<>c.<>9__67_4) == null)
			{
				arg_833_1 = (skill_a3.<>c.<>9__67_4 = new Action<GameObject>(skill_a3.<>c.<>9.<init>b__67_4));
			}
			arg_833_0.onClick = arg_833_1;
			BaseButton baseButton6 = new BaseButton(base.transform.FindChild("newbie_btn_skill"), 1, 1);
			baseButton6.onClick = new Action<GameObject>(this.onNewbie);
			BaseProxy<Skill_a3Proxy>.getInstance().addEventListener(Skill_a3Proxy.SKILLUPINFO, new Action<GameEvent>(this.upSkillInfo));
			BaseProxy<Skill_a3Proxy>.getInstance().addEventListener(Skill_a3Proxy.SKILLINFO, new Action<GameEvent>(this.skillInfo));
			new BaseButton(base.getTransformByPath("onekeyInfo"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("onekeyInfo").SetActive(false);
			};
			new BaseButton(base.getTransformByPath("showOneKey/back"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("showOneKey").SetActive(false);
				base.getTransformByPath("skill/panel_left/onekey").gameObject.SetActive(true);
			};
			new BaseButton(base.getTransformByPath("onekeyInfo/yes"), 1, 1).onClick = delegate(GameObject go)
			{
				base.getGameObjectByPath("onekeyInfo").SetActive(false);
			};
			this.needobj_skillId = (uint)XMLMgr.instance.GetSXML("skill.skill_learn_item_id", "").getInt("item_id");
		}

		private void upSkillInfo(GameEvent e)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Screen.size:(",
				Screen.width,
				",",
				Screen.height,
				")"
			}));
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.fx_sjcg);
			float x = (float)Screen.width / 831f;
			float y = (float)Screen.height / 459f;
			Transform transform = base.transform.FindChild("skill/panel_left/skill_list/contain");
			gameObject.transform.SetParent(base.transform);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localScale = new Vector3(x, y, 1f);
			UnityEngine.Object.Destroy(gameObject, 2f);
			this.nils(skill_a3.skills);
			foreach (KeyValuePair<int, int> current in this.skill)
			{
				int key = current.Key;
				SXML sXML = XMLMgr.instance.GetSXML("skill.skill", "id==" + current.Key.ToString());
				int num = this.datas[current.Key].now_lv + current.Value;
				this.datas[current.Key].now_lv = num;
				bool flag = sXML.GetNode("skill_att", "skill_lv==" + num.ToString()) != null && transform.FindChild(key + "/isthis").gameObject.activeSelf;
				if (flag)
				{
					this.skill_lv.text = string.Concat(num);
					bool flag2 = sXML.GetNode("skill_att", "skill_lv==" + (num + 1).ToString()) != null;
					if (flag2)
					{
						this.havepoint.text = string.Concat(sXML.GetNode("skill_att", "skill_lv==" + (num + 1).ToString()).getInt("item_num"));
						this.needmoneys.text = sXML.GetNode("skill_att", "skill_lv==" + (num + 1).ToString()).getInt("money").ToString();
						this.skill_nextdes.text = sXML.GetNode("skill_att", "skill_lv==" + (num + 1).ToString()).getString("descr2");
						bool flag3 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl > (ulong)((long)sXML.GetNode("skill_att", "skill_lv==" + (num + 1).ToString()).getInt("open_zhuan"));
						if (flag3)
						{
							this.needmoneys.transform.parent.gameObject.SetActive(true);
							this.needlvandzhuan.transform.parent.gameObject.SetActive(false);
							this.lvorzhuan = false;
							this.moneyormojingEnough(sXML, current.Key);
						}
						else
						{
							bool flag4 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl == (ulong)((long)sXML.GetNode("skill_att", "skill_lv==" + (num + 1).ToString()).getInt("open_zhuan"));
							if (flag4)
							{
								bool flag5 = (ulong)ModelBase<PlayerModel>.getInstance().lvl >= (ulong)((long)sXML.GetNode("skill_att", "skill_lv==" + (num + 1).ToString()).getInt("open_lvl"));
								if (flag5)
								{
									this.needmoneys.transform.parent.gameObject.SetActive(true);
									this.needlvandzhuan.transform.parent.gameObject.SetActive(false);
									this.lvorzhuan = false;
									this.moneyormojingEnough(sXML, current.Key);
								}
								else
								{
									this.needmoneys.transform.parent.gameObject.SetActive(false);
									this.needlvandzhuan.transform.parent.gameObject.SetActive(true);
									this.lvorzhuan = true;
									this.needlvandzhuan.text = string.Concat(new object[]
									{
										"<color=#ff0000>",
										sXML.GetNode("skill_att", "skill_lv==" + (num + 1).ToString()).getInt("open_zhuan"),
										"转",
										sXML.GetNode("skill_att", "skill_lv==" + (num + 1).ToString()).getInt("open_lvl"),
										"级</color>"
									});
								}
							}
							else
							{
								this.needmoneys.transform.parent.gameObject.SetActive(false);
								this.needlvandzhuan.transform.parent.gameObject.SetActive(true);
								this.lvorzhuan = true;
								this.needlvandzhuan.text = string.Concat(new object[]
								{
									"<color=#ff0000>",
									sXML.GetNode("skill_att", "skill_lv==" + (num + 1).ToString()).getInt("open_zhuan"),
									"转",
									sXML.GetNode("skill_att", "skill_lv==" + (num + 1).ToString()).getInt("open_lvl"),
									"级</color>"
								});
							}
						}
					}
					else
					{
						this.needmoneys.transform.parent.gameObject.SetActive(false);
						this.needlvandzhuan.transform.parent.gameObject.SetActive(true);
						this.skill_nextdes.text = "已经是最大等级";
						this.needlvandzhuan.text = "已经是最大等级";
						this.havepoint.text = "已经是最大等级";
					}
				}
				else
				{
					bool flag6 = sXML.GetNode("skill_att", "skill_lv==" + num.ToString()) == null && transform.FindChild(key + "/isthis").gameObject.activeSelf;
					if (flag6)
					{
						this.needmoneys.transform.parent.gameObject.SetActive(false);
						this.needlvandzhuan.transform.parent.gameObject.SetActive(true);
						this.skill_nextdes.text = "已经是最大等级";
						this.needlvandzhuan.text = "已经是最大等级";
						this.havepoint.text = "已经是最大等级";
						this.neednum_skill = -1;
					}
				}
			}
		}

		private void showUP(Dictionary<int, int> canskills)
		{
			base.getGameObjectByPath("showOneKey").SetActive(true);
			bool flag = this.father2.childCount > 1;
			if (flag)
			{
				for (int i = 0; i < this.father2.childCount; i++)
				{
					UnityEngine.Object.Destroy(this.father2.GetChild(i).gameObject);
				}
			}
			Transform transform = base.transform.FindChild("skill/panel_left/skill_list/contain");
			this.father2.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(300f, (float)(canskills.Count + 2) * this.father2.transform.GetComponent<GridLayoutGroup>().cellSize.y);
			foreach (KeyValuePair<int, int> current in canskills)
			{
				SXML sXML = XMLMgr.instance.GetSXML("skill.skill", "id==" + current.Key.ToString());
				this.gotext = UnityEngine.Object.Instantiate<GameObject>(this.gobefor);
				this.gotext.transform.SetParent(this.father2);
				this.gotext.transform.localScale = Vector3.one;
				this.gotext.SetActive(true);
				this.gotext.transform.GetComponent<Text>().text = string.Concat(new object[]
				{
					this.datas[current.Key].skill_name,
					": 提升",
					current.Value,
					"级"
				});
			}
			this.gotext = UnityEngine.Object.Instantiate<GameObject>(this.gobefor);
			this.gotext.transform.SetParent(this.father2);
			this.gotext.transform.localScale = Vector3.one;
			this.gotext.SetActive(true);
			this.gotext.transform.GetComponent<Text>().text = "升级花费金币：" + this.sumMonry;
			this.gotext = UnityEngine.Object.Instantiate<GameObject>(this.gobefor);
			this.gotext.transform.SetParent(this.father2);
			this.gotext.transform.localScale = Vector3.one;
			this.gotext.SetActive(true);
			this.gotext.transform.GetComponent<Text>().text = "升级花费魔晶：" + this.sumMojing;
			this.needmojingnum = this.sumMojing;
		}

		private void skillInfo(GameEvent e)
		{
			int key = e.data["skid"];
			this.datas[key].now_lv = e.data["sklvl"];
		}

		private void onNewbie(GameObject go)
		{
			bool flag = this.contain.transform.childCount > 1;
			if (flag)
			{
				bool flag2 = ModelBase<PlayerModel>.getInstance().lvl > 5u;
				if (flag2)
				{
					this.OnBeginDrag(this.contain.transform.GetChild(1).gameObject);
				}
				else
				{
					this.OnBeginDrag(this.contain.transform.GetChild(0).gameObject);
				}
			}
		}

		public override void onShowed()
		{
			this.onOpskill(null);
			this.openrefreshskillinfo();
			this.showCanStudy();
			this.onTabone(base.transform.FindChild("skill/panel_right/tab1").gameObject);
			this.showLevelupImage();
			BaseProxy<Skill_a3Proxy>.getInstance().addEventListener(Skill_a3Proxy.RUNEINFOS, new Action<GameEvent>(this.RuneInfos));
			BaseProxy<Skill_a3Proxy>.getInstance().addEventListener(Skill_a3Proxy.RUNERESEARCH, new Action<GameEvent>(this.onResearch));
			BaseProxy<Skill_a3Proxy>.getInstance().addEventListener(Skill_a3Proxy.RUNEADDSPEED, new Action<GameEvent>(this.onAddspeed));
			BaseProxy<Skill_a3Proxy>.getInstance().addEventListener(Skill_a3Proxy.RUNERESEARCHOVER, new Action<GameEvent>(this.onResearchOver));
			this.RefreshLockInfo();
			this.onshoeInfos(100);
			this.showskilldatas(this.fristid, 0);
			this.isthisImage();
			this.dicobj[this.fristid].transform.FindChild("isthis").gameObject.SetActive(true);
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			GRMap.GAME_CAMERA.SetActive(false);
			bool flag = FunctionOpenMgr.instance.Check(FunctionOpenMgr.RUNE, false);
			if (flag)
			{
				base.getGameObjectByPath("panel/lockp").SetActive(false);
			}
			this.showTeachAni(skill_a3.show_teack_ani);
		}

		public override void onClosed()
		{
			this.isthisImage();
			this.sendgroups();
			this.canStudySkill.Clear();
			BaseProxy<Skill_a3Proxy>.getInstance().removeEventListener(Skill_a3Proxy.RUNEINFOS, new Action<GameEvent>(this.RuneInfos));
			BaseProxy<Skill_a3Proxy>.getInstance().removeEventListener(Skill_a3Proxy.RUNERESEARCH, new Action<GameEvent>(this.onResearch));
			BaseProxy<Skill_a3Proxy>.getInstance().removeEventListener(Skill_a3Proxy.RUNEADDSPEED, new Action<GameEvent>(this.onAddspeed));
			BaseProxy<Skill_a3Proxy>.getInstance().removeEventListener(Skill_a3Proxy.RUNERESEARCHOVER, new Action<GameEvent>(this.onResearchOver));
			bool flag = this.commonimage != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.commonimage.gameObject);
			}
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			GRMap.GAME_CAMERA.SetActive(true);
			bool flag2 = a3_expbar.instance != null;
			if (flag2)
			{
				a3_expbar.instance.RemoveLightTip("newskill");
			}
			skill_a3.show_teack_ani = false;
			A3_BeStronger.Instance.CheckUpItem();
		}

		public void uprefreshskillinfo(int id, int lv)
		{
			this.dicobj[id].transform.FindChild("lv").GetComponent<Text>().text = string.Concat(this.datas[id].now_lv);
			this.showskilldatas(id, 0);
			EventTriggerListener.Get(this.dicobj[id]).onDown = new EventTriggerListener.VoidDelegate(this.OnBeginDrag);
			EventTriggerListener.Get(this.dicobj[id]).onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
			EventTriggerListener.Get(this.dicobj[id]).onDragEnd = new EventTriggerListener.VectorDelegate(this.OnDragEnd);
			EventTriggerListener.Get(this.dicobj[id]).onUp = new EventTriggerListener.VoidDelegate(this.OnUpiamge);
			bool activeSelf = base.transform.FindChild("skill/panel_right/skillgroupsone").gameObject.activeSelf;
			if (activeSelf)
			{
				for (int i = 0; i < 4; i++)
				{
					bool flag = this.skillgroupsone[i].transform.childCount > 0 && this.skillgroupsone[i].transform.GetChild(0).name == id.ToString();
					if (flag)
					{
						base.getComponentByPath<Text>("skill/panel_right/lv" + i).text = string.Concat(lv);
					}
				}
			}
			else
			{
				for (int j = 0; j < 4; j++)
				{
					bool flag2 = this.skillgrouptwo[j].transform.childCount > 0 && this.skillgrouptwo[j].transform.GetChild(0).name == id.ToString();
					if (flag2)
					{
						base.getComponentByPath<Text>("skill/panel_right/lv" + j).text = string.Concat(lv);
					}
				}
			}
		}

		public void showTeachAni(bool show)
		{
			if (show)
			{
				this.teackAni_go.SetActive(true);
			}
			else
			{
				this.teackAni_go.SetActive(false);
			}
		}

		private void OnBeginDrag(GameObject go)
		{
			bool flag = this.commonimage != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.commonimage);
				this.commonimage = null;
			}
			this.isthisImage();
			go.transform.FindChild("isthis").gameObject.SetActive(true);
			this.commonimage = UnityEngine.Object.Instantiate<GameObject>(go.transform.FindChild("skill_image").gameObject);
			this.commonimage.name = go.name;
			this.commonimage.transform.SetParent(base.transform, false);
			this.commonimage.SetActive(false);
			UIMoveToPoint component = go.transform.FindChild("skill_image").GetComponent<UIMoveToPoint>();
			bool flag2 = component != null;
			if (flag2)
			{
				component.Kill();
			}
			bool flag3 = this.datas[int.Parse(this.commonimage.name)].now_lv <= 0;
			if (flag3)
			{
				bool flag4 = this.commonimage != null;
				if (flag4)
				{
					this.showskilldatas(int.Parse(this.commonimage.name), 0);
				}
				UnityEngine.Object.Destroy(this.commonimage);
				this.commonimage = null;
			}
			else
			{
				this.commonimage.SetActive(true);
				this.commonimage.GetComponent<CanvasGroup>().interactable = false;
				this.commonimage.GetComponent<CanvasGroup>().blocksRaycasts = false;
				this.commonimage.transform.localPosition = new Vector3((Input.mousePosition.x - 0.5f * (float)Screen.width) * Baselayer.cemaraRectTran.rect.width / (float)Screen.width, (Input.mousePosition.y - 0.5f * (float)Screen.height) * Baselayer.cemaraRectTran.rect.height / (float)Screen.height, 0f);
			}
		}

		private void OnDrag(GameObject go, Vector2 delta)
		{
			bool flag = this.commonimage == null;
			if (!flag)
			{
				this.commonimage.transform.localPosition = new Vector3((Input.mousePosition.x - 0.5f * (float)Screen.width) * Baselayer.cemaraRectTran.rect.width / (float)Screen.width, (Input.mousePosition.y - 0.5f * (float)Screen.height) * Baselayer.cemaraRectTran.rect.height / (float)Screen.height, 0f);
			}
		}

		private void OnDragEnd(GameObject go, Vector2 delta)
		{
			bool flag = this.commonimage != null;
			if (flag)
			{
				bool flag2 = this.num != -1;
				if (flag2)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.commonimage);
					gameObject.name = this.commonimage.name;
					bool flag3 = this.num <= 3;
					if (flag3)
					{
						this.deletesame(this.num, this.skillgroupsone, gameObject);
					}
					else
					{
						this.deletesame(this.num - 4, this.skillgrouptwo, gameObject);
					}
					UiEventCenter.getInstance().onSkillDrawEnd();
					skill_a3.show_teack_ani = false;
					this.showTeachAni(false);
				}
				else
				{
					this.showskilldatas(int.Parse(this.commonimage.name), 0);
				}
			}
			UnityEngine.Object.Destroy(this.commonimage);
			this.commonimage = null;
		}

		private void OnBeginDragimage(GameObject go)
		{
			bool flag = this.commonimage != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.commonimage);
				this.commonimage = null;
			}
			bool flag2 = go.transform.childCount > 0;
			if (flag2)
			{
				this.gos = go.transform.GetChild(0).gameObject;
				this.commonimage = UnityEngine.Object.Instantiate<GameObject>(go.transform.GetChild(0).gameObject);
				this.commonimage.name = go.transform.GetChild(0).name;
				this.commonimage.transform.SetParent(base.transform, false);
				this.showskilldatas(int.Parse(this.commonimage.name), 0);
				this.commonimage.SetActive(true);
				this.isdragimage = true;
				this.commonimage.GetComponent<CanvasGroup>().interactable = false;
				this.commonimage.GetComponent<CanvasGroup>().blocksRaycasts = false;
				this.commonimage.transform.localPosition = new Vector3((Input.mousePosition.x - 0.5f * (float)Screen.width) * Baselayer.cemaraRectTran.rect.width / (float)Screen.width, (Input.mousePosition.y - 0.5f * (float)Screen.height) * Baselayer.cemaraRectTran.rect.height / (float)Screen.height, 0f);
			}
		}

		private void onDragimage(GameObject go, Vector2 delta)
		{
			bool flag = go.transform.childCount == 0;
			if (!flag)
			{
				this.commonimage.transform.localPosition = new Vector3((Input.mousePosition.x - 0.5f * (float)Screen.width) * Baselayer.cemaraRectTran.rect.width / (float)Screen.width, (Input.mousePosition.y - 0.5f * (float)Screen.height) * Baselayer.cemaraRectTran.rect.height / (float)Screen.height, 0f);
			}
		}

		private void OnUpiamge(GameObject go)
		{
			this.OnDragEnd(go, Vector2.zero);
		}

		private void onDragEndimage(GameObject go, Vector2 delta)
		{
			bool flag = go.transform.childCount <= 0;
			if (!flag)
			{
				bool flag2 = this.gos != null;
				if (flag2)
				{
					this.goss = this.gos.transform.parent.gameObject;
				}
				bool flag3 = this.isdragimage;
				if (flag3)
				{
					this.isdragimage = false;
					bool flag4 = this.num != -1;
					if (flag4)
					{
						bool flag5 = this.num < 4;
						if (flag5)
						{
							bool flag6 = this.skillgroupsone[this.num].transform.childCount > 0;
							if (flag6)
							{
								this.swapposition(this.skillgroupsone, this.num);
							}
							else
							{
								this.groupids(this.skillgroupsone, int.Parse(this.goss.name), false, null);
								base.getComponentByPath<Text>("skill/panel_right/lv" + this.goss.name).text = "";
								this.gos.transform.SetParent(this.skillgroupsone[this.num].transform, false);
								base.getComponentByPath<Text>("skill/panel_right/lv" + this.num).text = string.Concat(ModelBase<Skill_a3Model>.getInstance().skilldic[int.Parse(this.gos.name)].now_lv);
								this.gos.transform.localPosition = new Vector3(0f, 0f, 0f);
								this.groupids(this.skillgroupsone, this.num, true, this.gos);
							}
						}
						else
						{
							this.num -= 4;
							bool flag7 = this.skillgrouptwo[this.num].transform.childCount > 0;
							if (flag7)
							{
								this.swapposition(this.skillgrouptwo, this.num);
							}
							else
							{
								this.groupids(this.skillgrouptwo, int.Parse(this.goss.name), false, null);
								base.getComponentByPath<Text>("skill/panel_right/lv" + this.goss.name).text = "";
								this.gos.transform.SetParent(this.skillgrouptwo[this.num].transform, false);
								base.getComponentByPath<Text>("skill/panel_right/lv" + this.num).text = string.Concat(ModelBase<Skill_a3Model>.getInstance().skilldic[int.Parse(this.gos.name)].now_lv);
								this.gos.transform.localPosition = new Vector3(0f, 0f, 0f);
								this.groupids(this.skillgrouptwo, this.num, true, this.gos);
							}
						}
					}
					else
					{
						bool activeSelf = this.group1.gameObject.activeSelf;
						if (activeSelf)
						{
							this.groupids(this.skillgroupsone, int.Parse(this.goss.name), false, null);
							base.getComponentByPath<Text>("skill/panel_right/lv" + this.goss.name).text = "";
						}
						else
						{
							this.groupids(this.skillgrouptwo, int.Parse(this.goss.name), false, null);
							base.getComponentByPath<Text>("skill/panel_right/lv" + this.goss.name).text = "";
						}
						UnityEngine.Object.Destroy(this.gos);
						this.gos = null;
					}
				}
				else
				{
					bool flag8 = this.commonimage != null;
					if (flag8)
					{
						this.showskilldatas(int.Parse(this.commonimage.name), 0);
					}
					bool flag9 = this.commonimage != null;
					if (flag9)
					{
						UnityEngine.Object.Destroy(this.commonimage.gameObject);
					}
				}
				UnityEngine.Object.Destroy(this.commonimage);
				this.commonimage = null;
			}
		}

		private void OnEnterimage(GameObject go)
		{
			bool flag = go == this.skillgroupsone[0];
			if (flag)
			{
				this.num = 0;
			}
			else
			{
				bool flag2 = go == this.skillgroupsone[1];
				if (flag2)
				{
					this.num = 1;
				}
				else
				{
					bool flag3 = go == this.skillgroupsone[2];
					if (flag3)
					{
						this.num = 2;
					}
					else
					{
						bool flag4 = go == this.skillgroupsone[3];
						if (flag4)
						{
							this.num = 3;
						}
						else
						{
							bool flag5 = go == this.skillgrouptwo[0];
							if (flag5)
							{
								this.num = 4;
							}
							else
							{
								bool flag6 = go == this.skillgrouptwo[1];
								if (flag6)
								{
									this.num = 5;
								}
								else
								{
									bool flag7 = go == this.skillgrouptwo[2];
									if (flag7)
									{
										this.num = 6;
									}
									else
									{
										bool flag8 = go == this.skillgrouptwo[3];
										if (flag8)
										{
											this.num = 7;
										}
										else
										{
											this.num = -1;
										}
									}
								}
							}
						}
					}
				}
			}
		}

		private void OnExitimage(GameObject go)
		{
			this.num = -1;
		}

		private void moneyormojingEnough(SXML xml, int id)
		{
			bool flag = (ulong)ModelBase<PlayerModel>.getInstance().money >= (ulong)((long)xml.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()).getInt("money"));
			if (flag)
			{
				this.needmoney = true;
			}
			else
			{
				this.needmoney = false;
			}
			this.needmoneys.text = xml.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()).getInt("money").ToString();
		}

		private void showskilldatas(int id, int j = 0)
		{
			int ids = id;
			this.skill_name.text = this.datas[id].skill_name.ToString();
			this.skill_lv.text = string.Concat(this.datas[id].now_lv);
			this.skill_des.text = this.datas[id].des;
			SXML sXML = XMLMgr.instance.GetSXML("skill.skill", "id==" + id.ToString());
			bool flag = this.datas[id].now_lv != 0;
			if (flag)
			{
				this.skill_nowdes.text = sXML.GetNode("skill_att", "skill_lv==" + this.datas[id].now_lv.ToString()).getString("descr2");
			}
			else
			{
				this.skill_nowdes.text = "-";
			}
			int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(1540u);
			bool flag2 = sXML.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()) != null;
			if (flag2)
			{
				bool flag3 = itemNumByTpid >= sXML.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()).getInt("item_num");
				if (flag3)
				{
					this.needmojing = true;
				}
				else
				{
					this.needmojing = false;
				}
				this.havepoint.text = string.Concat(sXML.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()).getInt("item_num"));
				this.neednum_skill = sXML.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()).getInt("item_num");
				this.skill_nextdes.text = sXML.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()).getString("descr2");
				bool flag4 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl > (ulong)((long)sXML.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()).getInt("open_zhuan"));
				if (flag4)
				{
					this.needmoneys.transform.parent.gameObject.SetActive(true);
					this.needlvandzhuan.transform.parent.gameObject.SetActive(false);
					this.lvorzhuan = false;
					this.moneyormojingEnough(sXML, id);
				}
				else
				{
					bool flag5 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl == (ulong)((long)sXML.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()).getInt("open_zhuan"));
					if (flag5)
					{
						bool flag6 = (ulong)ModelBase<PlayerModel>.getInstance().lvl >= (ulong)((long)sXML.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()).getInt("open_lvl"));
						if (flag6)
						{
							this.needmoneys.transform.parent.gameObject.SetActive(true);
							this.needlvandzhuan.transform.parent.gameObject.SetActive(false);
							this.lvorzhuan = false;
							this.moneyormojingEnough(sXML, id);
						}
						else
						{
							this.needmoneys.transform.parent.gameObject.SetActive(false);
							this.needlvandzhuan.transform.parent.gameObject.SetActive(true);
							this.lvorzhuan = true;
							this.needlvandzhuan.text = string.Concat(new object[]
							{
								"<color=#ff0000>",
								sXML.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()).getInt("open_zhuan"),
								"转",
								sXML.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()).getInt("open_lvl"),
								"级</color>"
							});
						}
					}
					else
					{
						this.needmoneys.transform.parent.gameObject.SetActive(false);
						this.needlvandzhuan.transform.parent.gameObject.SetActive(true);
						this.lvorzhuan = true;
						this.needlvandzhuan.text = string.Concat(new object[]
						{
							"<color=#ff0000>",
							sXML.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()).getInt("open_zhuan"),
							"转",
							sXML.GetNode("skill_att", "skill_lv==" + (this.datas[id].now_lv + 1).ToString()).getInt("open_lvl"),
							"级</color>"
						});
					}
				}
			}
			else
			{
				this.needmoneys.transform.parent.gameObject.SetActive(false);
				this.needlvandzhuan.transform.parent.gameObject.SetActive(true);
				this.skill_nextdes.text = "已经是最大等级";
				this.needlvandzhuan.text = "已经是最大等级";
				this.havepoint.text = "已经是最大等级";
				this.neednum_skill = -1;
			}
			BaseButton baseButton = new BaseButton(base.transform.FindChild("skill/panel_right/bg/Button"), 1, 1);
			baseButton.onClick = delegate(GameObject go)
			{
				bool flag7 = this.datas[id].now_lv <= 0;
				if (flag7)
				{
					flytxt.instance.fly("未获得该技能！", 1, default(Color), null);
				}
				bool flag8 = this.lvorzhuan;
				if (flag8)
				{
					flytxt.instance.fly("等级不足！", 1, default(Color), null);
				}
				else
				{
					bool flag9 = !this.needmoney;
					if (flag9)
					{
						flytxt.instance.fly("金钱不足！", 1, default(Color), null);
					}
					else
					{
						bool flag10 = !this.needmojing;
						if (flag10)
						{
							this.addtoget(ModelBase<a3_BagModel>.getInstance().getItemDataById(this.needobj_skillId));
							flytxt.instance.fly("魔晶不足！", 1, default(Color), null);
						}
						else
						{
							BaseProxy<Skill_a3Proxy>.getInstance().sendProxy(ids, null);
						}
					}
				}
			};
		}

		private void addtoget(a3_ItemData item)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(item);
			arrayList.Add(InterfaceMgr.SKILL_A3);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMLACK, arrayList, false);
		}

		private void nils(List<int> skillss)
		{
			bool flag = skillss.Count == 10;
			if (flag)
			{
				int num = 0;
				for (int i = 0; i < skillss.Count; i++)
				{
					SXML sXML = XMLMgr.instance.GetSXML("skill.skill", "id==" + skillss[i].ToString());
					SXML node = sXML.GetNode("skill_att", "skill_lv==" + (this.datas[skillss[i]].now_lv + 1).ToString());
					bool flag2 = node == null;
					if (flag2)
					{
						num++;
					}
				}
				bool flag3 = num == 10;
				if (flag3)
				{
					base.getButtonByPath("skill/panel_left/onekey").interactable = false;
					base.getTransformByPath("skill/panel_left/onekey/Text").GetComponent<Text>().text = "已是最大等级";
					this.isfull = true;
				}
			}
		}

		private void oneKey(List<int> skillss)
		{
			this.canlevel = false;
			this.canmoney = false;
			this.canmojing = false;
			int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(1540u);
			int money = (int)ModelBase<PlayerModel>.getInstance().money;
			int num = itemNumByTpid + 1;
			int num2 = money + 1;
			int num3 = (int)(ModelBase<PlayerModel>.getInstance().up_lvl + 1u);
			int num4 = (int)(ModelBase<PlayerModel>.getInstance().lvl + 1u);
			for (int i = 0; i < skillss.Count; i++)
			{
				SXML sXML = XMLMgr.instance.GetSXML("skill.skill", "id==" + skillss[i].ToString());
				SXML node = sXML.GetNode("skill_att", "skill_lv==" + (this.datas[skillss[i]].now_lv + 1).ToString());
				bool flag = node != null;
				if (flag)
				{
					bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl > (ulong)((long)node.getInt("open_zhuan"));
					if (flag2)
					{
						num3 = node.getInt("open_zhuan");
						bool flag3 = itemNumByTpid >= (this.datas[skillss[i]].now_lv + 1) * 2;
						if (flag3)
						{
							num = (this.datas[skillss[i]].now_lv + 1) * 2;
						}
						bool flag4 = money >= (this.datas[skillss[i]].now_lv + 1) * 1000;
						if (flag4)
						{
							num2 = node.getInt("money");
						}
						num4 = 0;
					}
					else
					{
						bool flag5 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl == (ulong)((long)node.getInt("open_zhuan"));
						if (flag5)
						{
							num3 = node.getInt("open_zhuan");
							bool flag6 = (ulong)ModelBase<PlayerModel>.getInstance().lvl >= (ulong)((long)node.getInt("open_lvl"));
							if (flag6)
							{
								num4 = node.getInt("open_lvl");
								bool flag7 = itemNumByTpid >= (this.datas[skillss[i]].now_lv + 1) * 2;
								if (flag7)
								{
									num = (this.datas[skillss[i]].now_lv + 1) * 2;
								}
								bool flag8 = money >= (this.datas[skillss[i]].now_lv + 1) * 1000;
								if (flag8)
								{
									num2 = node.getInt("money");
								}
							}
						}
					}
				}
			}
			bool flag9 = num3 <= (int)ModelBase<PlayerModel>.getInstance().up_lvl && num4 <= (int)ModelBase<PlayerModel>.getInstance().lvl;
			if (flag9)
			{
				this.canlevel = true;
			}
			bool flag10 = num <= itemNumByTpid && this.canlevel;
			if (flag10)
			{
				this.canmojing = true;
			}
			bool flag11 = num2 <= money && this.canlevel && this.canmojing;
			if (flag11)
			{
				this.canmoney = true;
			}
			this.nils(skillss);
		}

		private void onekeyUp(List<int> skillss)
		{
			this.sumMonry = 0;
			this.sumMojing = 0;
			this.skill.Clear();
			int num = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(1540u);
			int num2 = (int)ModelBase<PlayerModel>.getInstance().money;
			for (int i = 1; i < 275; i++)
			{
				for (int j = 0; j < skillss.Count; j++)
				{
					SXML sXML = XMLMgr.instance.GetSXML("skill.skill", "id==" + skillss[j].ToString());
					SXML node = sXML.GetNode("skill_att", "skill_lv==" + (this.datas[skillss[j]].now_lv + i).ToString());
					bool flag = node != null;
					if (flag)
					{
						int @int = node.getInt("open_zhuan");
						int int2 = node.getInt("open_lvl");
						bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl > (ulong)((long)@int);
						if (flag2)
						{
							bool flag3 = num >= (this.datas[skillss[j]].now_lv + i) * 2 && num2 >= (this.datas[skillss[j]].now_lv + i) * 1000;
							if (flag3)
							{
								num -= (this.datas[skillss[j]].now_lv + i) * 2;
								this.sumMojing += (this.datas[skillss[j]].now_lv + i) * 2;
								num2 -= (this.datas[skillss[j]].now_lv + i) * 1000;
								this.sumMonry += (this.datas[skillss[j]].now_lv + i) * 1000;
								bool flag4 = this.skill.ContainsKey(skillss[j]);
								if (flag4)
								{
									this.skill[skillss[j]] = i;
								}
								else
								{
									this.skill.Add(skillss[j], i);
								}
							}
						}
						else
						{
							bool flag5 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl == (ulong)((long)@int);
							if (flag5)
							{
								bool flag6 = (ulong)ModelBase<PlayerModel>.getInstance().lvl >= (ulong)((long)int2);
								if (flag6)
								{
									bool flag7 = num >= (this.datas[skillss[j]].now_lv + i) * 2 && num2 >= (this.datas[skillss[j]].now_lv + i) * 1000;
									if (flag7)
									{
										num -= (this.datas[skillss[j]].now_lv + i) * 2;
										this.sumMojing += (this.datas[skillss[j]].now_lv + i) * 2;
										num2 -= (this.datas[skillss[j]].now_lv + i) * 1000;
										this.sumMonry += (this.datas[skillss[j]].now_lv + i) * 1000;
										bool flag8 = this.skill.ContainsKey(skillss[j]);
										if (flag8)
										{
											this.skill[skillss[j]] = i;
										}
										else
										{
											this.skill.Add(skillss[j], i);
										}
									}
								}
							}
						}
					}
				}
			}
			debug.Log(string.Concat(new object[]
			{
				this.sumMonry,
				"::money",
				this.sumMojing,
				"::mojing"
			}));
		}

		private void deletesame(int num, GameObject[] go, GameObject objClone)
		{
			for (int i = 0; i < 4; i++)
			{
				bool flag = go[i].transform.childCount > 0 && go[i].transform.GetChild(0).name == objClone.name;
				if (flag)
				{
					UnityEngine.Object.Destroy(go[i].transform.GetChild(0).gameObject);
					base.getComponentByPath<Text>("skill/panel_right/lv" + i).text = "";
					this.groupids(go, i, false, go[i].transform.GetChild(0).gameObject);
				}
			}
			bool flag2 = go[num].transform.childCount > 0;
			if (flag2)
			{
				UnityEngine.Object.Destroy(go[num].transform.GetChild(0).gameObject);
				base.getComponentByPath<Text>("skill/panel_right/lv" + num).text = "";
				this.groupids(go, num, false, go[num].transform.GetChild(0).gameObject);
			}
			objClone.transform.SetParent(go[num].transform, false);
			objClone.transform.localPosition = new Vector3(0f, 0f, 0f);
			objClone.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
			base.getComponentByPath<Text>("skill/panel_right/lv" + num).text = string.Concat(ModelBase<Skill_a3Model>.getInstance().skilldic[int.Parse(objClone.name)].now_lv);
			this.groupids(go, num, true, objClone);
		}

		private void swapposition(GameObject[] go, int num)
		{
			GameObject gameObject = go[num].transform.GetChild(0).gameObject;
			gameObject.transform.SetParent(this.gos.transform.parent.transform, false);
			gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
			this.groupids(go, int.Parse(this.gos.transform.parent.name), true, gameObject);
			base.getComponentByPath<Text>("skill/panel_right/lv" + this.gos.transform.parent.name).text = string.Concat(ModelBase<Skill_a3Model>.getInstance().skilldic[int.Parse(gameObject.name)].now_lv);
			this.gos.transform.SetParent(go[num].transform, false);
			this.gos.transform.localPosition = new Vector3(0f, 0f, 0f);
			this.groupids(go, int.Parse(this.gos.transform.parent.name), true, this.gos);
			base.getComponentByPath<Text>("skill/panel_right/lv" + this.gos.transform.parent.name).text = string.Concat(ModelBase<Skill_a3Model>.getInstance().skilldic[int.Parse(this.gos.name)].now_lv);
		}

		private void groupids(GameObject[] go, int num, bool deletoradd, GameObject gos)
		{
			if (deletoradd)
			{
				bool flag = go == this.skillgroupsone;
				if (flag)
				{
					ModelBase<Skill_a3Model>.getInstance().idsgroupone[num] = int.Parse(gos.name);
					bool flag2 = skillbar.instance != null && skillbar.instance.skillsetIdx == 1;
					if (flag2)
					{
						skillbar.instance.refreSkill(num + 1, int.Parse(gos.name));
					}
				}
				else
				{
					ModelBase<Skill_a3Model>.getInstance().idsgrouptwo[num] = int.Parse(gos.name);
					bool flag3 = skillbar.instance != null && skillbar.instance.skillsetIdx == 2;
					if (flag3)
					{
						skillbar.instance.refreSkill(num + 1, int.Parse(gos.name));
					}
				}
			}
			else
			{
				bool flag4 = go == this.skillgroupsone;
				if (flag4)
				{
					ModelBase<Skill_a3Model>.getInstance().idsgroupone[num] = 0;
					bool flag5 = skillbar.instance != null && skillbar.instance.skillsetIdx == 1;
					if (flag5)
					{
						skillbar.instance.refreSkill(num + 1, null);
					}
				}
				else
				{
					ModelBase<Skill_a3Model>.getInstance().idsgrouptwo[num] = 0;
					bool flag6 = skillbar.instance != null && skillbar.instance.skillsetIdx == 2;
					if (flag6)
					{
						skillbar.instance.refreSkill(num + 1, null);
					}
				}
			}
		}

		public void moveAni(int skillid, int arr, int index)
		{
			Transform transform = this.dicobj[skillid].transform.FindChild("skill_image");
			Transform toobj = null;
			bool flag = false;
			bool flag2 = (arr == 1 && this.group1.activeSelf) || (arr == 2 && this.group2.activeSelf);
			if (flag2)
			{
				flag = true;
				bool flag3 = !this.group1.activeSelf;
				if (flag3)
				{
					toobj = this.group2.transform.FindChild(index.ToString());
				}
				else
				{
					toobj = this.group1.transform.FindChild(index.ToString());
				}
			}
			else
			{
				bool activeSelf = this.group1.activeSelf;
				if (activeSelf)
				{
					toobj = this.tab2.transform;
				}
				else
				{
					toobj = this.tab1.transform;
				}
			}
			UIMoveToPoint uIMoveToPoint = UIMoveToPoint.Get(transform.gameObject);
			bool flag4 = uIMoveToPoint.dutime == 0f;
			if (flag4)
			{
				uIMoveToPoint.dutime = 1f;
			}
			bool flag5 = flag;
			if (flag5)
			{
				uIMoveToPoint.endscale = Vector3.one;
				toobj.gameObject.SetActive(false);
				uIMoveToPoint.Move(toobj.transform, delegate
				{
					toobj.gameObject.SetActive(true);
				});
			}
			else
			{
				uIMoveToPoint.endscale = Vector3.zero;
				uIMoveToPoint.Move(toobj.transform, null);
			}
		}

		private void creatrve()
		{
			foreach (int current in this.datas.Keys)
			{
				bool flag = ModelBase<PlayerModel>.getInstance().profession == this.datas[current].carr;
				if (flag)
				{
					bool flag2 = this.datas[current].open_lvl == 1 && this.datas[current].open_zhuan == 0;
					if (!flag2)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.image);
						gameObject.SetActive(true);
						gameObject.transform.SetParent(this.contain.transform, false);
						gameObject.name = current.ToString();
						gameObject.transform.FindChild("close_skill").gameObject.SetActive(true);
						gameObject.transform.FindChild("close_skill/lv").GetComponent<Text>().text = string.Concat(new object[]
						{
							this.datas[current].open_zhuan,
							"转",
							this.datas[current].open_lvl,
							"级开启"
						});
						this.skill_image = gameObject.transform.FindChild("skill_image/skill").gameObject;
						string path = "icon/skill/" + current;
						this.skill_image.GetComponent<Image>().sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
						gameObject.transform.FindChild("lv").GetComponent<Text>().text = "0";
						gameObject.transform.FindChild("name").GetComponent<Text>().text = this.datas[current].skill_name;
						bool flag3 = this.fristid == 0;
						if (flag3)
						{
							this.fristid = current;
						}
						this.dicobj[current] = gameObject;
					}
				}
			}
		}

		public void showCanStudy()
		{
			foreach (int current in this.datas.Keys)
			{
				bool flag = ModelBase<PlayerModel>.getInstance().profession == this.datas[current].carr;
				if (flag)
				{
					bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl > (ulong)((long)this.datas[current].open_zhuan);
					if (flag2)
					{
						bool flag3 = this.datas[current].open_lvl == 1 && this.datas[current].open_zhuan == 0;
						if (!flag3)
						{
							bool flag4 = this.datas[current].now_lv == 0;
							if (flag4)
							{
								this.dicobj[current].transform.FindChild("canStudy").gameObject.SetActive(true);
							}
							else
							{
								this.dicobj[current].transform.FindChild("canStudy").gameObject.SetActive(false);
							}
						}
					}
					else
					{
						bool flag5 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl == (ulong)((long)this.datas[current].open_zhuan);
						if (flag5)
						{
							bool flag6 = (ulong)ModelBase<PlayerModel>.getInstance().lvl >= (ulong)((long)this.datas[current].open_lvl);
							if (flag6)
							{
								bool flag7 = this.datas[current].open_lvl == 1 && this.datas[current].open_zhuan == 0;
								if (!flag7)
								{
									bool flag8 = this.datas[current].now_lv == 0;
									if (flag8)
									{
										this.dicobj[current].transform.FindChild("canStudy").gameObject.SetActive(true);
									}
									else
									{
										this.dicobj[current].transform.FindChild("canStudy").gameObject.SetActive(false);
									}
								}
							}
						}
					}
				}
			}
		}

		public void openrefreshskillinfo()
		{
			foreach (int current in this.datas.Keys)
			{
				bool flag = ModelBase<PlayerModel>.getInstance().profession == this.datas[current].carr;
				if (flag)
				{
					bool flag2 = this.datas[current].open_lvl == 1 && this.datas[current].open_zhuan == 0;
					if (!flag2)
					{
						bool flag3 = this.datas[current].now_lv != 0;
						if (flag3)
						{
							this.lockinfos(current);
						}
					}
				}
			}
			for (int i = 0; i < 4; i++)
			{
				base.getComponentByPath<Text>("skill/panel_right/lv" + i).text = "";
			}
			for (int j = 0; j < 4; j++)
			{
				bool flag4 = ModelBase<Skill_a3Model>.getInstance().idsgroupone[j] != 0;
				if (flag4)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.image.transform.FindChild("skill_image").gameObject);
					string path = "icon/skill/" + ModelBase<Skill_a3Model>.getInstance().idsgroupone[j];
					gameObject.transform.FindChild("skill").GetComponent<Image>().sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
					gameObject.SetActive(true);
					bool flag5 = this.skillgroupsone[j].transform.childCount > 0;
					if (flag5)
					{
						UnityEngine.Object.Destroy(this.skillgroupsone[j].transform.GetChild(0).gameObject);
					}
					gameObject.name = ModelBase<Skill_a3Model>.getInstance().idsgroupone[j].ToString();
					gameObject.transform.SetParent(this.skillgroupsone[j].transform, false);
					gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
					gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
					base.getComponentByPath<Text>("skill/panel_right/lv" + j).text = string.Concat(ModelBase<Skill_a3Model>.getInstance().skilldic[ModelBase<Skill_a3Model>.getInstance().idsgroupone[j]].now_lv);
				}
				bool flag6 = ModelBase<Skill_a3Model>.getInstance().idsgrouptwo[j] != 0;
				if (flag6)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.image.transform.FindChild("skill_image").gameObject);
					string path2 = "icon/skill/" + ModelBase<Skill_a3Model>.getInstance().idsgrouptwo[j];
					gameObject2.transform.FindChild("skill").GetComponent<Image>().sprite = (Resources.Load(path2, typeof(Sprite)) as Sprite);
					bool flag7 = this.skillgrouptwo[j].transform.childCount > 0;
					if (flag7)
					{
						UnityEngine.Object.Destroy(this.skillgrouptwo[j].transform.GetChild(0).gameObject);
					}
					gameObject2.name = ModelBase<Skill_a3Model>.getInstance().idsgrouptwo[j].ToString();
					gameObject2.transform.SetParent(this.skillgrouptwo[j].transform, false);
					gameObject2.transform.localPosition = new Vector3(0f, 0f, 0f);
					gameObject2.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
				}
			}
		}

		private void lockinfos(int id)
		{
			bool flag = this.datas[id].skill_name == ModelBase<Skill_a3Model>.getInstance().skilllst[1].skill_name || this.datas[id].skill_name == ModelBase<Skill_a3Model>.getInstance().skilllst[11].skill_name || this.datas[id].skill_name == ModelBase<Skill_a3Model>.getInstance().skilllst[21].skill_name;
			if (flag)
			{
				this.dicobj[id].transform.FindChild("isthis").gameObject.SetActive(true);
				this.showskilldatas(id, 0);
			}
			this.dicobj[id].transform.FindChild("close_skill").gameObject.SetActive(false);
			this.dicobj[id].transform.FindChild("lv").gameObject.SetActive(true);
			this.dicobj[id].transform.FindChild("lv").GetComponent<Text>().text = string.Concat(this.datas[id].now_lv);
			this.dicobj[id].transform.FindChild("name").GetComponent<Text>().text = this.datas[id].skill_name;
			this.canStudySkill[id] = this.datas[id].now_lv;
			EventTriggerListener.Get(this.dicobj[id]).onDown = new EventTriggerListener.VoidDelegate(this.OnBeginDrag);
			EventTriggerListener.Get(this.dicobj[id]).onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
			EventTriggerListener.Get(this.dicobj[id]).onDragEnd = new EventTriggerListener.VectorDelegate(this.OnDragEnd);
			EventTriggerListener.Get(this.dicobj[id]).onUp = new EventTriggerListener.VoidDelegate(this.OnUpiamge);
		}

		public void showLevelupImage()
		{
			uint up_lvl = ModelBase<PlayerModel>.getInstance().up_lvl;
			uint lvl = ModelBase<PlayerModel>.getInstance().lvl;
			foreach (int current in this.datas.Keys)
			{
				bool flag = ModelBase<PlayerModel>.getInstance().profession == this.datas[current].carr;
				if (flag)
				{
					bool flag2 = this.datas[current].open_lvl == 1 && this.datas[current].open_zhuan == 0;
					if (!flag2)
					{
						List<SXML> nodeList = this.datas[current].xml.GetNodeList("skill_att", "");
						bool flag3 = this.datas[current].now_lv >= nodeList.Count;
						if (flag3)
						{
							this.dicobj[current].transform.FindChild("addlv").gameObject.SetActive(false);
						}
						else
						{
							SXML node = this.datas[current].xml.GetNode("skill_att", "skill_lv==" + (this.datas[current].now_lv + 1));
							bool flag4 = (ulong)up_lvl > (ulong)((long)node.getInt("open_zhuan"));
							if (flag4)
							{
								bool flag5 = !this.dicobj[current].transform.FindChild("canStudy").gameObject.activeSelf;
								if (flag5)
								{
									this.dicobj[current].transform.FindChild("addlv").gameObject.SetActive(true);
								}
								else
								{
									this.dicobj[current].transform.FindChild("addlv").gameObject.SetActive(false);
								}
							}
							else
							{
								bool flag6 = (ulong)up_lvl == (ulong)((long)node.getInt("open_zhuan"));
								if (flag6)
								{
									bool flag7 = (ulong)lvl >= (ulong)((long)node.getInt("open_lvl"));
									if (flag7)
									{
										bool flag8 = !this.dicobj[current].transform.FindChild("canStudy").gameObject.activeSelf;
										if (flag8)
										{
											this.dicobj[current].transform.FindChild("addlv").gameObject.SetActive(true);
										}
										else
										{
											this.dicobj[current].transform.FindChild("addlv").gameObject.SetActive(false);
										}
									}
									else
									{
										this.dicobj[current].transform.FindChild("addlv").gameObject.SetActive(false);
									}
								}
								else
								{
									this.dicobj[current].transform.FindChild("addlv").gameObject.SetActive(false);
								}
							}
						}
					}
				}
			}
		}

		private void onClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.SKILL_A3);
		}

		private void onTabone(GameObject go)
		{
			base.getGameObjectByPath("skill/panel_right/tab1/Image").SetActive(true);
			base.getGameObjectByPath("skill/panel_right/tab2/Image").SetActive(false);
			this.shoeCanvasGroup();
			this.group1.SetActive(true);
			this.group2.SetActive(false);
			for (int i = 0; i < 4; i++)
			{
				bool flag = this.skillgroupsone[i].transform.childCount != 0;
				if (flag)
				{
					base.getComponentByPath<Text>("skill/panel_right/lv" + i).text = string.Concat(ModelBase<Skill_a3Model>.getInstance().skilldic[int.Parse(this.skillgroupsone[i].transform.GetChild(0).name)].now_lv);
				}
				else
				{
					base.getComponentByPath<Text>("skill/panel_right/lv" + i).text = "";
				}
			}
		}

		private void setRune_open()
		{
			base.transform.FindChild("rune/tishi_bg/openlvl/Text").GetComponent<Text>().text = ContMgr.getCont("func_limit_43", null);
			new BaseButton(base.transform.FindChild("rune/tishi_bg/goget"), 1, 1).onClick = delegate(GameObject go)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_YGYIWU, null, false);
				this.toygyw = true;
				InterfaceMgr.getInstance().close(InterfaceMgr.SKILL_A3);
			};
		}

		private void onTabtwo(GameObject go)
		{
			base.getGameObjectByPath("skill/panel_right/tab1/Image").SetActive(false);
			base.getGameObjectByPath("skill/panel_right/tab2/Image").SetActive(true);
			this.shoeCanvasGroup();
			this.group1.SetActive(false);
			this.group2.SetActive(true);
			for (int i = 0; i < 4; i++)
			{
				bool flag = this.skillgrouptwo[i].transform.childCount != 0;
				if (flag)
				{
					base.getComponentByPath<Text>("skill/panel_right/lv" + i).text = string.Concat(ModelBase<Skill_a3Model>.getInstance().skilldic[int.Parse(this.skillgrouptwo[i].transform.GetChild(0).name)].now_lv);
				}
				else
				{
					base.getComponentByPath<Text>("skill/panel_right/lv" + i).text = "";
				}
			}
		}

		private void onOpskill(GameObject go)
		{
			this.shoeCanvasGroup();
			base.getGameObjectByPath("panel/skill/Image0").SetActive(false);
			base.getGameObjectByPath("panel/skill/Image1").SetActive(true);
			base.getGameObjectByPath("panel/rune/Image0").SetActive(true);
			base.getGameObjectByPath("panel/rune/Image1").SetActive(false);
			base.transform.FindChild("skill").gameObject.SetActive(true);
			base.transform.FindChild("rune").gameObject.SetActive(false);
		}

		private void onOprune(GameObject go)
		{
			base.getGameObjectByPath("panel/skill/Image0").SetActive(true);
			base.getGameObjectByPath("panel/skill/Image1").SetActive(false);
			base.getGameObjectByPath("panel/rune/Image0").SetActive(false);
			base.getGameObjectByPath("panel/rune/Image1").SetActive(true);
			base.transform.FindChild("skill").gameObject.SetActive(false);
			base.transform.FindChild("rune").gameObject.SetActive(true);
			bool flag = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl < (ulong)((long)ModelBase<Skill_a3Model>.getInstance().openuplvl);
			if (flag)
			{
				this.rune_open.SetActive(true);
				this.setTishi(1);
			}
			else
			{
				bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl == (ulong)((long)ModelBase<Skill_a3Model>.getInstance().openuplvl);
				if (flag2)
				{
					bool flag3 = (ulong)ModelBase<PlayerModel>.getInstance().lvl < (ulong)((long)ModelBase<Skill_a3Model>.getInstance().openlvl);
					if (flag3)
					{
						this.rune_open.SetActive(true);
						this.setTishi(1);
					}
					else
					{
						bool flag4 = this.ishasRune();
						if (flag4)
						{
							this.rune_open.SetActive(false);
						}
						else
						{
							this.rune_open.SetActive(true);
							this.setTishi(2);
						}
					}
				}
				else
				{
					bool flag5 = this.ishasRune();
					if (flag5)
					{
						this.rune_open.SetActive(false);
					}
					else
					{
						this.rune_open.SetActive(true);
						this.setTishi(2);
					}
				}
			}
		}

		private void setTishi(int type)
		{
			bool flag = type == 1;
			if (flag)
			{
				base.transform.FindChild("rune/tishi_bg/openlvl").gameObject.SetActive(true);
				base.transform.FindChild("rune/tishi_bg/goget").gameObject.SetActive(false);
			}
			else
			{
				bool flag2 = type == 2;
				if (flag2)
				{
					base.transform.FindChild("rune/tishi_bg/openlvl").gameObject.SetActive(false);
					base.transform.FindChild("rune/tishi_bg/goget").gameObject.SetActive(true);
				}
			}
		}

		private bool ishasRune()
		{
			bool result = false;
			foreach (runeData current in ModelBase<Skill_a3Model>.getInstance().runedic.Values)
			{
				bool flag = current.now_lv > 0;
				if (flag)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		private void sendgroups()
		{
			List<int> list = new List<int>();
			for (int i = 0; i < 4; i++)
			{
				list.Add(ModelBase<Skill_a3Model>.getInstance().idsgroupone[i]);
			}
			list.AddRange(ModelBase<Skill_a3Model>.getInstance().idsgrouptwo);
			BaseProxy<Skill_a3Proxy>.getInstance().sendProxy(0, list);
		}

		private void shoeCanvasGroup()
		{
			for (int i = 0; i < 4; i++)
			{
				base.getComponentByPath<CanvasGroup>("skill/panel_right/skillgroupstwo/image" + i).interactable = false;
				base.getComponentByPath<CanvasGroup>("skill/panel_right/skillgroupstwo/image" + i).blocksRaycasts = false;
			}
			for (int j = 0; j < 4; j++)
			{
				base.getComponentByPath<CanvasGroup>("skill/panel_right/skillgroupsone/image" + j).interactable = false;
				base.getComponentByPath<CanvasGroup>("skill/panel_right/skillgroupsone/image" + j).blocksRaycasts = false;
			}
		}

		private void isthisImage()
		{
			foreach (int current in this.dicobj.Keys)
			{
				this.dicobj[current].transform.FindChild("isthis").gameObject.SetActive(false);
			}
		}

		private void creatrverune()
		{
			foreach (int current in this.runedatas.Keys)
			{
				bool flag = ModelBase<PlayerModel>.getInstance().profession == this.runedatas[current].carr || this.runedatas[current].carr == 1;
				if (flag)
				{
					bool flag2 = this.runedatas[current].type == 1;
					if (flag2)
					{
						this.creatrvedatas(this.imagerune0, this.panel0, this.runedatas[current].id);
					}
					else
					{
						bool flag3 = this.runedatas[current].type == 2;
						if (flag3)
						{
							this.creatrvedatas(this.imagerune1, this.panel1, this.runedatas[current].id);
						}
						else
						{
							bool flag4 = this.runedatas[current].type == 3;
							if (flag4)
							{
								this.creatrvedatas(this.imagerune2, this.panel2, this.runedatas[current].id);
							}
						}
					}
				}
			}
		}

		private void creatrvedatas(GameObject obj, GameObject panel, int id)
		{
			GameObject objClone = UnityEngine.Object.Instantiate<GameObject>(obj);
			objClone.name = id.ToString();
			objClone.SetActive(true);
			objClone.transform.SetParent(panel.transform, false);
			string path = "icon/rune/" + id;
			objClone.transform.FindChild("icon").GetComponent<Image>().sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			BaseButton baseButton = new BaseButton(objClone.transform.FindChild("Image").transform, 1, 1);
			baseButton.onClick = delegate(GameObject go)
			{
				this.onshoeInfos(int.Parse(objClone.name));
			};
			this.runeobj[id] = objClone;
		}

		private void onshoeInfos(int rune_id)
		{
			this.runeinfosObj.SetActive(true);
			this.RefreshInfos(rune_id);
			this.showthisrune(rune_id);
		}

		private void RefreshLockInfo()
		{
			foreach (int current in this.runeobj.Keys)
			{
				bool flag = this.runedatas[current].now_lv <= 0;
				if (flag)
				{
					this.runeobj[current].transform.FindChild("lock").gameObject.SetActive(true);
				}
				else
				{
					this.runeobj[current].transform.FindChild("lock").gameObject.SetActive(false);
				}
			}
		}

		private void RuneInfos(GameEvent e)
		{
			bool flag = e.data["runes"].Length > 0;
			if (flag)
			{
				foreach (Variant current in e.data["runes"]._arr)
				{
					ModelBase<Skill_a3Model>.getInstance().Reshreinfos(current["id"], current["lvl"], current["upgrade_count_down"]);
					bool flag2 = current["upgrade_count_down"] > 0;
					if (flag2)
					{
						this.info_time[current["id"]] = current["upgrade_count_down"] + 1;
						SXML sXML = XMLMgr.instance.GetSXML("rune.rune", "id==" + current["id"]);
						int @int = sXML.GetNode("level", "lv==" + (current["lvl"] + 1)).getInt("time_cost");
						this.info_times[current["id"]] = @int;
						MonoBehaviour.print("给我的id和等级:" + current["id"] + "和" + current["lvl"]);
						bool flag3 = this.runeobj.ContainsKey(current["id"]);
						if (flag3)
						{
							this.runeobj[current["id"]].transform.FindChild("image").gameObject.SetActive(true);
							base.InvokeRepeating("timess", 0f, 1f);
							this.istimein = true;
						}
						else
						{
							this.runeobj[current["id"]].transform.FindChild("image").gameObject.SetActive(false);
						}
					}
				}
			}
			this.onshoeInfos(100);
			this.RefreshLockInfo();
		}

		private void onResearch(GameEvent e)
		{
			this.info_time[e.data["id"]] = e.data["upgrade_count_down"] + 1;
			this.info_times[e.data["id"]] = e.data["upgrade_count_down"] + 1;
			this.RefreshInfos(e.data["id"]);
			bool flag = !this.istimein;
			if (flag)
			{
				base.InvokeRepeating("timess", 0f, 1f);
				this.runeobj[e.data["id"]].transform.FindChild("image").gameObject.SetActive(true);
				this.istimein = true;
			}
		}

		private void onAddspeed(GameEvent e)
		{
			this.info_time[e.data["id"]] = 0;
			this.info_time.Remove(e.data["id"]);
			this.runeobj[e.data["id"]].transform.FindChild("progress").GetComponent<Image>().fillAmount = 0f;
			this.RefreshInfos(e.data["id"]);
			this.runeobj[e.data["id"]].transform.FindChild("image").gameObject.SetActive(false);
			this.studybtn.SetActive(true);
			this.costgoldbtn.SetActive(false);
		}

		private void onResearchOver(GameEvent e)
		{
			this.info_time[e.data["id"]] = 0;
			this.info_time.Remove(e.data["id"]);
			this.runeobj[e.data["id"]].transform.FindChild("progress").GetComponent<Image>().fillAmount = 0f;
			this.runeobj[e.data["id"]].transform.FindChild("image").gameObject.SetActive(false);
			bool activeSelf = this.term_time.gameObject.activeSelf;
			if (activeSelf)
			{
				this.term_time.gameObject.SetActive(false);
				this.trem_timeImage.SetActive(false);
				this.RefreshInfos(e.data["id"]);
			}
			this.studybtn.SetActive(true);
			this.costgoldbtn.SetActive(false);
		}

		private void timess()
		{
			int[] array = new int[this.info_time.Keys.Count];
			int[] array2 = new int[this.info_time.Keys.Count];
			int num = 0;
			bool flag = this.info_time.Keys.Count == 0;
			if (flag)
			{
				base.CancelInvoke("timess");
				this.istimein = false;
			}
			else
			{
				foreach (int current in this.info_time.Keys)
				{
					this.info_time.TryGetValue(current, out array[num]);
					array2[num] = current;
					num++;
				}
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i]--;
				MonoBehaviour.print("id是：" + array2[i]);
				float num2 = 1f / (float)this.info_times[array2[i]];
				float num3 = (float)this.info_times[array2[i]] - (float)array[i];
				bool flag2 = array[i] > 0;
				if (flag2)
				{
					double value = (double)(num2 * num3);
					this.runeobj[array2[i]].transform.FindChild("progress").GetComponent<Image>().fillAmount = (float)Math.Round(value, 3);
				}
				bool flag3 = array[i] <= 0;
				if (flag3)
				{
					array[i] = 0;
					this.runeobj[array2[i]].transform.FindChild("image").gameObject.SetActive(false);
					this.runeobj[array2[i]].transform.FindChild("progress").GetComponent<Image>().fillAmount = 0f;
				}
				this.info_time[array2[i]] = array[i];
				ModelBase<Skill_a3Model>.getInstance().runedic[array2[i]].time = array[i];
			}
			bool flag4 = this.term_time.gameObject.activeSelf && this.info_time.ContainsKey(int.Parse(this.term_time.gameObject.name));
			if (flag4)
			{
				this.trem_timeImage.SetActive(true);
				this.term_time.text = Globle.formatTime(this.info_time[int.Parse(this.term_time.gameObject.name)], true);
				bool flag5 = this.info_time[int.Parse(this.term_time.gameObject.name)] > 10;
				if (flag5)
				{
					this.costgoldbtn_free.SetActive(false);
					this.costgoldbtn_nofree.SetActive(true);
					int num4 = this.info_time[int.Parse(this.term_time.gameObject.name)] / 600;
					bool flag6 = num4 < 10;
					if (flag6)
					{
						num4 = 10;
					}
					this.costgold_num.text = num4.ToString();
				}
				else
				{
					bool flag7 = this.info_time[int.Parse(this.term_time.gameObject.name)] <= 10;
					if (flag7)
					{
						this.costgoldbtn_free.SetActive(true);
						this.costgoldbtn_nofree.SetActive(false);
					}
				}
			}
		}

		private void RefreshInfos(int id)
		{
			this.nowid = id;
			Dictionary<int, runeData> runedic = ModelBase<Skill_a3Model>.getInstance().runedic;
			this.runelv.text = "Lv:" + runedic[id].now_lv.ToString();
			this.runename.text = ModelBase<Skill_a3Model>.getInstance().runedic[id].name;
			this.runedes.text = ModelBase<Skill_a3Model>.getInstance().runedic[id].desc;
			SXML sXML = XMLMgr.instance.GetSXML("rune.rune", "id==" + id);
			SXML node = sXML.GetNode("level", "lv==" + (runedic[id].now_lv + 1).ToString());
			bool flag = node != null;
			if (flag)
			{
				this.term.text = "";
				List<SXML> nodeList = node.GetNodeList("require", "");
				foreach (SXML current in nodeList)
				{
					bool flag2 = current.getInt("role_zhuan") != -1;
					if (flag2)
					{
						bool flag3 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl > (ulong)((long)current.getInt("role_zhuan"));
						if (flag3)
						{
							Text text = this.term;
							text.text = string.Concat(new object[]
							{
								text.text,
								"<color=#00FF00>",
								current.getInt("role_zhuan"),
								"转",
								current.getInt("role_level"),
								"级</color>\n"
							});
							this.islv = true;
						}
						else
						{
							bool flag4 = (ulong)ModelBase<PlayerModel>.getInstance().up_lvl == (ulong)((long)current.getInt("role_zhuan"));
							if (flag4)
							{
								bool flag5 = (ulong)ModelBase<PlayerModel>.getInstance().lvl >= (ulong)((long)current.getInt("role_level"));
								if (flag5)
								{
									this.islv = true;
									Text text = this.term;
									text.text = string.Concat(new object[]
									{
										text.text,
										"<color=#00FF00>",
										current.getInt("role_zhuan"),
										"转",
										current.getInt("role_level"),
										"级</color>\n"
									});
								}
								else
								{
									this.islv = false;
									Text text = this.term;
									text.text = string.Concat(new object[]
									{
										text.text,
										"<color=#ff0000>",
										current.getInt("role_zhuan"),
										"转",
										current.getInt("role_level"),
										"级</color>\n"
									});
								}
							}
							else
							{
								this.islv = false;
								Text text = this.term;
								text.text = string.Concat(new object[]
								{
									text.text,
									"<color=#ff0000>",
									current.getInt("role_zhuan"),
									"转",
									current.getInt("role_level"),
									"级</color>\n"
								});
							}
						}
					}
					bool flag6 = current.getInt("pre_rune_id") != -1;
					if (flag6)
					{
						bool flag7 = runedic[current.getInt("pre_rune_id")].now_lv < current.getInt("pre_run_level");
						if (flag7)
						{
							this.ispre_lv = false;
							Text text = this.term;
							text.text = string.Concat(new object[]
							{
								text.text,
								"<color=#ff0000>",
								runedic[current.getInt("pre_rune_id")].name,
								"LV:",
								current.getInt("pre_run_level"),
								"</color>\n"
							});
						}
						else
						{
							Text text = this.term;
							text.text = string.Concat(new object[]
							{
								text.text,
								"<color=#00FF00>",
								runedic[current.getInt("pre_rune_id")].name,
								"LV:",
								current.getInt("pre_run_level"),
								"</color>\n"
							});
							this.ispre_lv = true;
						}
					}
					int num = 0;
					bool flag8 = current.getInt("other_rune_total_level") != -1;
					if (flag8)
					{
						foreach (int current2 in runedic.Keys)
						{
							bool flag9 = runedic[current2].type == current.getInt("type");
							if (flag9)
							{
								num += runedic[current2].now_lv;
							}
						}
						bool flag10 = num >= current.getInt("other_rune_total_level");
						if (flag10)
						{
							this.isotherRuneAll_lv = true;
							bool flag11 = current.getInt("type") == 1;
							if (flag11)
							{
								Text text = this.term;
								text.text = string.Concat(new object[]
								{
									text.text,
									"<color=#00FF00>攻击符文总等级大于",
									current.getInt("other_rune_total_level"),
									"</color>\n"
								});
							}
							else
							{
								bool flag12 = current.getInt("type") == 2;
								if (flag12)
								{
									Text text = this.term;
									text.text = string.Concat(new object[]
									{
										text.text,
										"<color=#00FF00>防御符文总等级大于",
										current.getInt("other_rune_total_level"),
										"</color>\n"
									});
								}
								else
								{
									bool flag13 = current.getInt("type") == 3;
									if (flag13)
									{
										Text text = this.term;
										text.text = string.Concat(new object[]
										{
											text.text,
											"<color=#00FF00>生存符文总等级大于",
											current.getInt("other_rune_total_level"),
											"</color>\n"
										});
									}
								}
							}
						}
						else
						{
							this.isotherRuneAll_lv = false;
							bool flag14 = current.getInt("type") == 1;
							if (flag14)
							{
								Text text = this.term;
								text.text = string.Concat(new object[]
								{
									text.text,
									"<color=#ff0000>攻击符文总等级大于",
									current.getInt("other_rune_total_level"),
									"</color>\n"
								});
							}
							else
							{
								bool flag15 = current.getInt("type") == 2;
								if (flag15)
								{
									Text text = this.term;
									text.text = string.Concat(new object[]
									{
										text.text,
										"<color=#ff0000>防御符文总等级大于",
										current.getInt("other_rune_total_level"),
										"</color>\n"
									});
								}
								else
								{
									bool flag16 = current.getInt("type") == 3;
									if (flag16)
									{
										Text text = this.term;
										text.text = string.Concat(new object[]
										{
											text.text,
											"<color=#ff0000>生存符文总等级大于",
											current.getInt("other_rune_total_level"),
											"</color>\n"
										});
									}
								}
							}
						}
					}
				}
			}
			else
			{
				this.runelv.text = "Lv:" + runedic[id].now_lv.ToString();
				this.term.text = "已经是最大等级";
			}
			bool flag17 = runedic[id].time == -1 || runedic[id].time == 0;
			if (flag17)
			{
				this.term_time.gameObject.SetActive(false);
				this.trem_timeImage.SetActive(false);
				this.studybtn.SetActive(true);
				this.costgoldbtn.SetActive(false);
			}
			else
			{
				MonoBehaviour.print("现在的时间是：" + runedic[id].time);
				this.showTime(runedic[id].time, id);
			}
		}

		private void showTime(int time, int id)
		{
			this.term_time.gameObject.SetActive(true);
			this.trem_timeImage.SetActive(true);
			this.term_time.gameObject.name = id.ToString();
			this.studybtn.SetActive(false);
			this.costgoldbtn.SetActive(true);
		}

		private void onAddspeedClick(GameObject go)
		{
			bool flag = !this.costgoldbtn_free.activeSelf;
			if (flag)
			{
				bool flag2 = (ulong)ModelBase<PlayerModel>.getInstance().gold < (ulong)((long)int.Parse(this.costgold_num.text));
				if (flag2)
				{
					flytxt.instance.fly("宝石不足！", 1, default(Color), null);
				}
				else
				{
					BaseProxy<Skill_a3Proxy>.getInstance().sendProxys(3, this.nowid, false);
				}
			}
			else
			{
				bool activeSelf = this.costgoldbtn_free.activeSelf;
				if (activeSelf)
				{
					BaseProxy<Skill_a3Proxy>.getInstance().sendProxys(3, this.nowid, true);
				}
			}
		}

		private void onStudyRuneClick(GameObject go)
		{
			bool flag = !this.ispre_lv;
			if (flag)
			{
				flytxt.instance.fly("前置符文等级不足！", 1, default(Color), null);
			}
			else
			{
				bool flag2 = !this.isotherRuneAll_lv;
				if (flag2)
				{
					flytxt.instance.fly("其他符文总等级不足！", 1, default(Color), null);
				}
				else
				{
					bool flag3 = !this.islv;
					if (flag3)
					{
						flytxt.instance.fly("角色等级不足！", 1, default(Color), null);
					}
				}
			}
			bool flag4 = this.islv && this.isotherRuneAll_lv && this.ispre_lv;
			if (flag4)
			{
				BaseProxy<Skill_a3Proxy>.getInstance().sendProxys(2, this.nowid, false);
			}
		}

		private void showthisrune(int id)
		{
			foreach (int current in this.runeobj.Keys)
			{
				bool flag = current == id;
				if (flag)
				{
					this.runeobj[current].transform.FindChild("this").gameObject.SetActive(true);
				}
				else
				{
					this.runeobj[current].transform.FindChild("this").gameObject.SetActive(false);
				}
			}
		}
	}
}
