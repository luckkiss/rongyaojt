using DG.Tweening;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_runestone : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_runestone.<>c <>9 = new a3_runestone.<>c();

			public static Action<GameObject> <>9__65_15;

			internal void <init>b__65_15(GameObject go)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_RUNESTONE);
			}
		}

		private Dictionary<int, a3_RunestoneData> dic_allrune_data = ModelBase<a3_BagModel>.getInstance().allRunestoneData();

		private Dictionary<int, GameObject> dic_compose_showobjs = new Dictionary<int, GameObject>();

		private GameObject compose_obj;

		private string path_str = "compose/left/bg/top/";

		private GameObject image_lv;

		private GameObject contain_lv;

		private GameObject contain_quality;

		private GameObject contain_name;

		private List<GameObject> list_compose_contain;

		private GameObject contain_compose_obj;

		private GameObject image_compose_obj;

		private Text compose_lv_txt;

		private Text compose_quality_txt;

		private Text compose_name_txt;

		private Slider exp_obj;

		private Slider stamin_obj;

		private Text exp;

		private Text stamin;

		private Text money;

		private Text buy_num;

		private BaseButton compose_bymoney_btn;

		private Dictionary<int, GameObject> dic_list_showobjs = new Dictionary<int, GameObject>();

		private GameObject list_obj;

		private GameObject list_contain_lv;

		private GameObject list_contain_quality;

		private GameObject list_contain_name;

		private List<GameObject> list_list_contain;

		private GameObject image_list_obj;

		private GameObject contain_list_obj;

		private Text list_lv_txt;

		private Text list_quality_txt;

		private Text list_name_txt;

		private Dictionary<uint, GameObject> dic_runestones_obj = new Dictionary<uint, GameObject>();

		private GameObject dress_obj;

		private GameObject dress_bg;

		private GameObject decomposeBg;

		private List<GameObject> lst_dresspos = new List<GameObject>();

		private GameObject decompose_obj;

		private Toggle white;

		private Toggle green;

		private Toggle blue;

		private Toggle purple;

		private Toggle orange;

		private GameObject contain_decompose;

		private GameObject image_decompose;

		private BaseButton decompose_btn;

		private GameObject composebtn_image;

		private GameObject dressbtn_image;

		private GameObject contain_haveRunestones;

		private GameObject image_hanvRunestones;

		private RectTransform rct_contain_haveRunestones;

		private RectTransform rct_scrollview_haveRunestones;

		private BaseButton compose_btn;

		private BaseButton dress_btn;

		public static a3_runestone _instance;

		private ScrollControler scrollControler;

		private ScrollControler scrollControler1;

		private ScrollControler scrollControler2;

		private ScrollControler scrollControler3;

		private ScrollControler scrollControler4;

		private ScrollControler scrollControler5;

		private ScrollControler scrollControler6;

		private ScrollControler scrollControler7;

		private ScrollControler scrollControler8;

		private ScrollControler scrollControler9;

		private List<uint> lst_decompose = new List<uint>();

		private Dictionary<uint, GameObject> decompose_runestones = new Dictionary<uint, GameObject>();

		private uint imageInstanceid = 0u;

		private uint old_imageInstanceid = 0u;

		private int lv_type = 0;

		private int quality_type = 0;

		private int name_type = 0;

		private int min_num = 0;

		private int max_num = 0;

		private int needmoney_num = 0;

		private int nub = 0;

		private int this_id = -1;

		private int grids_nub = 40;

		private bool switchbl = false;

		private bool oldswitchbl = false;

		private List<GameObject> list_dic = new List<GameObject>();

		public override void init()
		{
			new BaseButton(base.getTransformByPath("Button"), 1, 1).onClick = delegate(GameObject go)
			{
				this.bttttt();
			};
			this.compose_obj = base.getGameObjectByPath("compose");
			this.image_lv = base.getGameObjectByPath(this.path_str + "lv/scrollview/Image");
			this.contain_lv = base.getGameObjectByPath(this.path_str + "lv/scrollview/grid");
			this.contain_quality = base.getGameObjectByPath(this.path_str + "quality/scrollview/grid");
			this.contain_name = base.getGameObjectByPath(this.path_str + "name/scrollview/grid");
			this.list_compose_contain = new List<GameObject>
			{
				this.contain_lv,
				this.contain_quality,
				this.contain_name
			};
			BaseButton baseButton = new BaseButton(base.getTransformByPath(this.path_str + "lv/Button"), 1, 1);
			BaseButton baseButton2 = new BaseButton(base.getTransformByPath(this.path_str + "quality/Button"), 1, 1);
			BaseButton baseButton3 = new BaseButton(base.getTransformByPath(this.path_str + "name/Button"), 1, 1);
			baseButton.onClick = (baseButton2.onClick = (baseButton3.onClick = delegate(GameObject go)
			{
				this.OpenchoseOnClick(go, this.list_compose_contain);
			}));
			this.image_compose_obj = base.getGameObjectByPath("compose/left/bg/down/scrollview/Image");
			this.contain_compose_obj = base.getGameObjectByPath("compose/left/bg/down/scrollview/contain");
			this.compose_lv_txt = base.getComponentByPath<Text>("compose/left/bg/top/lv/chose");
			this.compose_quality_txt = base.getComponentByPath<Text>("compose/left/bg/top/quality/chose");
			this.compose_name_txt = base.getComponentByPath<Text>("compose/left/bg/top/name/chose");
			this.exp_obj = base.getComponentByPath<Slider>("compose/left/exp/exp");
			this.stamin_obj = base.getComponentByPath<Slider>("compose/left/strength/strength");
			this.exp = base.getComponentByPath<Text>("compose/left/exp/Text");
			this.stamin = base.getComponentByPath<Text>("compose/left/strength/Text");
			this.money = base.getComponentByPath<Text>("compose/left/need_money/num");
			this.buy_num = base.getComponentByPath<Text>("compose/left/nub/num");
			new BaseButton(base.getTransformByPath("compose/left/nub/right_btn"), 1, 1).onClick = delegate(GameObject go)
			{
				this.addnum();
			};
			new BaseButton(base.getTransformByPath("compose/left/nub/left_btn"), 1, 1).onClick = delegate(GameObject go)
			{
				this.reducenum();
			};
			this.compose_bymoney_btn = new BaseButton(base.getTransformByPath("compose/left/need_money"), 1, 1);
			this.compose_bymoney_btn.onClick = delegate(GameObject go)
			{
				this.compose_bymonry();
			};
			this.list_obj = base.getGameObjectByPath("list");
			new BaseButton(base.getTransformByPath("list/close_btn"), 1, 1).onClick = delegate(GameObject go)
			{
				this.openorcloselistOnClock(false);
			};
			new BaseButton(base.getTransformByPath("close/list_btn"), 1, 1).onClick = delegate(GameObject go)
			{
				this.openorcloselistOnClock(true);
			};
			this.list_contain_lv = base.getGameObjectByPath("list/top/lv/scrollview/grid");
			this.list_contain_quality = base.getGameObjectByPath("list/top/quality/scrollview/grid");
			this.list_contain_name = base.getGameObjectByPath("list/top/name/scrollview/grid");
			this.list_list_contain = new List<GameObject>
			{
				this.list_contain_lv,
				this.list_contain_quality,
				this.list_contain_name
			};
			BaseButton baseButton4 = new BaseButton(base.getTransformByPath("list/top/lv/Button"), 1, 1);
			BaseButton baseButton5 = new BaseButton(base.getTransformByPath("list/top/quality/Button"), 1, 1);
			BaseButton baseButton6 = new BaseButton(base.getTransformByPath("list/top/name/Button"), 1, 1);
			baseButton4.onClick = (baseButton5.onClick = (baseButton6.onClick = delegate(GameObject go)
			{
				this.OpenchoseOnClick(go, this.list_list_contain);
			}));
			this.image_list_obj = base.getGameObjectByPath("list/down/scrollview/Image");
			this.contain_list_obj = base.getGameObjectByPath("list/down/scrollview/grid");
			this.list_lv_txt = base.getComponentByPath<Text>("list/top/lv/chose");
			this.list_quality_txt = base.getComponentByPath<Text>("list/top/quality/chose");
			this.list_name_txt = base.getComponentByPath<Text>("list/top/name/chose");
			this.dress_obj = base.getGameObjectByPath("dress");
			this.dress_bg = base.getGameObjectByPath("dress/bg");
			this.decomposeBg = base.getGameObjectByPath("haverunestones/Image");
			this.decompose_obj = base.getGameObjectByPath("decompose");
			new BaseButton(base.getTransformByPath("haverunestones/Image/btn"), 1, 1).onClick = delegate(GameObject go)
			{
				this.openorclsoeDecompose(true);
			};
			new BaseButton(base.getTransformByPath("decompose/close_btn"), 1, 1).onClick = delegate(GameObject go)
			{
				this.openorclsoeDecompose(false);
			};
			this.contain_decompose = base.getGameObjectByPath("decompose/scroll_view/contain");
			this.image_decompose = base.getGameObjectByPath("decompose/scroll_view/icon");
			this.decompose_btn = new BaseButton(base.getTransformByPath("decompose/info_bg/btn"), 1, 1);
			this.decompose_btn.onClick = new Action<GameObject>(this.decomposesOnclick);
			this.white = base.getComponentByPath<Toggle>("decompose/info_bg/Toggle_all/Toggle_white");
			this.white.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.ShowComposeRuneston(1);
				}
				else
				{
					this.DesComposeRuneston(1);
				}
			});
			this.green = base.getComponentByPath<Toggle>("decompose/info_bg/Toggle_all/Toggle_green");
			this.green.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.ShowComposeRuneston(2);
					bool flag = !this.white.isOn;
					if (flag)
					{
						this.white.isOn = true;
					}
				}
				else
				{
					this.DesComposeRuneston(2);
				}
			});
			this.blue = base.getComponentByPath<Toggle>("decompose/info_bg/Toggle_all/Toggle_blue");
			this.blue.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.ShowComposeRuneston(3);
					bool flag = !this.green.isOn;
					if (flag)
					{
						this.green.isOn = true;
					}
				}
				else
				{
					this.DesComposeRuneston(3);
				}
			});
			this.purple = base.getComponentByPath<Toggle>("decompose/info_bg/Toggle_all/Toggle_puple");
			this.purple.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.ShowComposeRuneston(4);
					bool flag = !this.blue.isOn;
					if (flag)
					{
						this.blue.isOn = true;
					}
				}
				else
				{
					this.DesComposeRuneston(4);
				}
			});
			this.orange = base.getComponentByPath<Toggle>("decompose/info_bg/Toggle_all/Toggle_orange");
			this.orange.onValueChanged.AddListener(delegate(bool ison)
			{
				if (ison)
				{
					this.ShowComposeRuneston(5);
					bool flag = !this.purple.isOn;
					if (flag)
					{
						this.purple.isOn = true;
					}
				}
				else
				{
					this.DesComposeRuneston(5);
				}
			});
			this.image_hanvRunestones = base.getGameObjectByPath("haverunestones/scrollview/icon");
			this.contain_haveRunestones = base.getGameObjectByPath("haverunestones/scrollview/contain");
			this.composebtn_image = base.getGameObjectByPath("Panel/compose_btn/Image");
			this.dressbtn_image = base.getGameObjectByPath("Panel/dress_btn/Image");
			this.rct_contain_haveRunestones = this.contain_compose_obj.GetComponent<RectTransform>();
			this.rct_scrollview_haveRunestones = base.getGameObjectByPath("haverunestones/scrollview").GetComponent<RectTransform>();
			BaseButton arg_66B_0 = new BaseButton(base.getTransformByPath("close/close_btn"), 1, 1);
			Action<GameObject> arg_66B_1;
			if ((arg_66B_1 = a3_runestone.<>c.<>9__65_15) == null)
			{
				arg_66B_1 = (a3_runestone.<>c.<>9__65_15 = new Action<GameObject>(a3_runestone.<>c.<>9.<init>b__65_15));
			}
			arg_66B_0.onClick = arg_66B_1;
			this.compose_btn = new BaseButton(base.getTransformByPath("Panel/compose_btn"), 1, 1);
			this.dress_btn = new BaseButton(base.getTransformByPath("Panel/dress_btn"), 1, 1);
			this.dress_btn.onClick = (this.compose_btn.onClick = new Action<GameObject>(this.btnSwitch));
			this.ScrollControler();
			this.InitchosebtnOnclick(this.list_compose_contain, 1);
			this.InitchosebtnOnclick(this.list_list_contain, 2);
			this.LvStaminaInfos();
			this.InitSccrollHaveRunestonsGrids();
			this.InitBagHaveRunestones();
			this.InitDress();
		}

		public override void onShowed()
		{
			a3_runestone._instance = this;
			bool flag = this.uiData != null;
			if (flag)
			{
				this.btnSwitch(this.dress_btn.gameObject);
			}
			this.ClosechoseOnClick(null, 1);
			BaseProxy<a3_RuneStoneProxy>.getInstance().addEventListener(a3_RuneStoneProxy.INFOS, new Action<GameEvent>(this.LvExpInfos));
		}

		public override void onClosed()
		{
			BaseProxy<a3_RuneStoneProxy>.getInstance().removeEventListener(a3_RuneStoneProxy.INFOS, new Action<GameEvent>(this.LvExpInfos));
			this.OpenchoseOnClick(null, this.list_compose_contain);
			this.btnSwitch(base.getGameObjectByPath("Panel/compose_btn"));
			this.switchbl = false;
			this.oldswitchbl = false;
		}

		private void InitDress()
		{
			for (int i = 0; i < this.dress_bg.transform.childCount; i++)
			{
				this.lst_dresspos.Add(this.dress_bg.transform.GetChild(i).gameObject);
			}
			Dictionary<uint, a3_BagItemData> dressup_runestones = ModelBase<A3_RuneStoneModel>.getInstance().dressup_runestones;
			foreach (uint current in dressup_runestones.Keys)
			{
				this.DressUp(dressup_runestones[current], current);
			}
		}

		public void DressDown(int pos)
		{
			UnityEngine.Object.Destroy(this.lst_dresspos[pos - 1].transform.GetChild(0).gameObject);
		}

		public void DressUp(a3_BagItemData data, uint id)
		{
			bool flag = this.lst_dresspos[data.runestonedata.position].transform.childCount > 0;
			if (flag)
			{
				this.DressDown(data.runestonedata.position + 1);
			}
			GameObject icon = IconImageMgr.getInstance().createA3ItemIcon(data, true, -1, 1f, false);
			icon.transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			icon.transform.SetParent(this.lst_dresspos[ModelBase<a3_BagModel>.getInstance().getRunestoneDataByid((int)data.tpid).position - 1].transform, false);
			icon.name = id.ToString();
			BaseButton baseButton = new BaseButton(icon.transform, 1, 1);
			baseButton.onClick = delegate(GameObject go)
			{
				this.onItemClick(data, icon, id, 2);
			};
		}

		private void openorclsoeDecompose(bool isopen)
		{
			if (isopen)
			{
				this.decompose_obj.SetActive(true);
				this.creatrvegrids();
			}
			else
			{
				this.decompose_obj.SetActive(false);
				this.destorygrids();
				this.orange.isOn = false;
				this.purple.isOn = false;
				this.blue.isOn = false;
				this.green.isOn = false;
				this.white.isOn = false;
			}
		}

		private void creatrvegrids()
		{
			int num = this.contain_haveRunestones.transform.childCount;
			num += 7 - num % 7;
			for (int i = 0; i < num; i++)
			{
				this.commonCreatrveImages(this.contain_decompose, this.image_decompose, -1, null, false);
			}
			this.commonScroview(this.contain_decompose, num);
		}

		private void destorygrids()
		{
			for (int i = 0; i < this.contain_decompose.transform.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.contain_decompose.transform.GetChild(i).gameObject);
			}
		}

		private void ShowComposeRuneston(int quality)
		{
			Dictionary<uint, a3_BagItemData> runestonrs = ModelBase<a3_BagModel>.getInstance().getRunestonrs();
			int num = 0;
			foreach (uint current in runestonrs.Keys)
			{
				bool flag = ModelBase<a3_BagModel>.getInstance().getRunestoneDataByid((int)runestonrs[current].tpid).quality == quality && !runestonrs[current].ismark;
				if (flag)
				{
					this.creatrveIcons(runestonrs[current], num);
					num++;
				}
			}
		}

		private void DesComposeRuneston(int qualit)
		{
			Dictionary<uint, a3_BagItemData> runestonrs = ModelBase<a3_BagModel>.getInstance().getRunestonrs();
			for (int i = 0; i < this.lst_decompose.Count; i++)
			{
				bool flag = ModelBase<a3_BagModel>.getInstance().getRunestoneDataByid((int)runestonrs[this.lst_decompose[i]].tpid).quality == qualit;
				if (flag)
				{
					UnityEngine.Object.Destroy(this.decompose_runestones[this.lst_decompose[i]].transform.parent.gameObject);
					this.decompose_runestones.Remove(this.lst_decompose[i]);
					this.commonScroview(this.contain_decompose, 1);
				}
			}
			this.lst_decompose.Clear();
			foreach (uint current in this.decompose_runestones.Keys)
			{
				this.lst_decompose.Add(current);
			}
		}

		private void creatrveIcons(a3_BagItemData data, int index)
		{
			GameObject icon = IconImageMgr.getInstance().createA3ItemIcon(data, true, -1, 0.8f, false);
			icon.transform.SetParent(this.contain_decompose.transform.GetChild(index).transform, false);
			icon.transform.FindChild("iconborder/ismark").gameObject.SetActive(false);
			icon.name = data.id.ToString();
			this.lst_decompose.Add(data.id);
			this.decompose_runestones[data.id] = icon;
			BaseButton baseButton = new BaseButton(icon.transform, 1, 1);
			baseButton.onClick = delegate(GameObject go)
			{
				this.onItemClick(data, icon, data.id, 3);
			};
		}

		public void destoryIcon(uint id)
		{
			bool flag = this.decompose_runestones.ContainsKey(id);
			if (flag)
			{
				UnityEngine.Object.Destroy(this.decompose_runestones[id].transform.parent.gameObject);
				this.decompose_runestones.Remove(id);
				this.lst_decompose.Remove(id);
			}
			this.commonCreatrveImages(this.contain_decompose, this.image_decompose, -1, null, false);
		}

		private void decomposesOnclick(GameObject go)
		{
			bool flag = this.lst_decompose.Count <= 0;
			if (flag)
			{
				flytxt.instance.fly("没有可分解的符石！", 0, default(Color), null);
			}
			else
			{
				BaseProxy<a3_RuneStoneProxy>.getInstance().sendporxy(4, 0, 0, this.lst_decompose);
				this.RefreshDecompose();
			}
		}

		private void RefreshDecompose()
		{
			for (int i = 0; i < this.lst_decompose.Count; i++)
			{
				UnityEngine.Object.Destroy(this.decompose_runestones[this.lst_decompose[i]].transform.parent.gameObject);
				this.decompose_runestones.Remove(this.lst_decompose[i]);
			}
			this.lst_decompose.Clear();
			this.decompose_runestones.Clear();
		}

		private void OpenchoseOnClick(GameObject go, List<GameObject> list_contain)
		{
			for (int i = 0; i < list_contain.Count; i++)
			{
				bool flag = go != null;
				if (flag)
				{
					bool flag2 = list_contain[i].transform.parent.transform.parent == go.transform.parent;
					if (flag2)
					{
						list_contain[i].transform.DOScaleY(1f, 0.1f);
					}
					else
					{
						list_contain[i].transform.DOScaleY(0f, 0.1f);
					}
				}
				else
				{
					list_contain[i].transform.DOScaleY(0f, 0.1f);
				}
			}
		}

		private void ClosechoseOnClick(GameObject go, int type)
		{
			bool flag = go == null;
			if (flag)
			{
				this.lv_type = 0;
				this.quality_type = 0;
				this.name_type = 0;
				bool flag2 = type == 1;
				if (flag2)
				{
					this.compose_lv_txt.text = "全";
					this.compose_quality_txt.text = "全";
					this.compose_name_txt.text = "全";
				}
				else
				{
					bool flag3 = type == 2;
					if (flag3)
					{
						this.list_lv_txt.text = "全";
						this.list_quality_txt.text = "全";
						this.list_name_txt.text = "全";
					}
				}
			}
			else
			{
				go.transform.parent.transform.DOScaleY(0f, 0.1f);
				this.imageInstanceid = (uint)go.GetInstanceID();
				bool flag4 = this.imageInstanceid == this.old_imageInstanceid;
				if (flag4)
				{
					return;
				}
				this.old_imageInstanceid = this.imageInstanceid;
				string text = go.transform.FindChild("Text").GetComponent<Text>().text;
				GameObject gameObject = go.transform.parent.parent.parent.gameObject;
				gameObject.transform.FindChild("chose").GetComponent<Text>().text = text;
				string name = gameObject.name;
				if (!(name == "lv"))
				{
					if (!(name == "quality"))
					{
						if (name == "name")
						{
							this.name_type = int.Parse(go.name);
						}
					}
					else
					{
						this.quality_type = int.Parse(go.name);
					}
				}
				else
				{
					this.lv_type = int.Parse(go.name);
				}
			}
			bool flag5 = type == 1;
			if (flag5)
			{
				this.RefreshRunes(this.contain_compose_obj, this.image_compose_obj, this.dic_compose_showobjs, ModelBase<A3_RuneStoneModel>.getInstance().nowlv);
			}
			else
			{
				bool flag6 = type == 2;
				if (flag6)
				{
					this.RefreshRunes(this.contain_list_obj, this.image_list_obj, this.dic_list_showobjs, 0);
				}
			}
		}

		private void RefreshRunes(GameObject contain, GameObject image, Dictionary<int, GameObject> dic, int lv = 0)
		{
			foreach (int current in dic.Keys)
			{
				UnityEngine.Object.Destroy(dic[current]);
			}
			this.list_dic.Clear();
			dic.Clear();
			Dictionary<int, a3_RunestoneData> dictionary = new Dictionary<int, a3_RunestoneData>();
			bool flag = lv != 0;
			if (flag)
			{
				foreach (int current2 in this.dic_allrune_data.Keys)
				{
					bool flag2 = this.dic_allrune_data[current2].stone_level <= lv;
					if (flag2)
					{
						dictionary[current2] = this.dic_allrune_data[current2];
					}
				}
			}
			else
			{
				dictionary = this.dic_allrune_data;
			}
			bool flag3 = this.lv_type == 0;
			if (flag3)
			{
				bool flag4 = this.quality_type == 0;
				if (flag4)
				{
					bool flag5 = this.name_type == 0;
					if (flag5)
					{
						foreach (int current3 in dictionary.Keys)
						{
							this.commonCreatrveImages(contain, image, current3, dic, lv != 0);
						}
					}
					else
					{
						foreach (int current4 in dictionary.Keys)
						{
							bool flag6 = dictionary[current4].name_type == this.name_type;
							if (flag6)
							{
								this.commonCreatrveImages(contain, image, current4, dic, lv != 0);
							}
						}
					}
				}
				else
				{
					bool flag7 = this.name_type == 0;
					if (flag7)
					{
						foreach (int current5 in dictionary.Keys)
						{
							bool flag8 = dictionary[current5].quality == this.quality_type;
							if (flag8)
							{
								this.commonCreatrveImages(contain, image, current5, dic, lv != 0);
							}
						}
					}
					else
					{
						foreach (int current6 in dictionary.Keys)
						{
							bool flag9 = dictionary[current6].quality == this.quality_type && dictionary[current6].name_type == this.name_type;
							if (flag9)
							{
								this.commonCreatrveImages(contain, image, current6, dic, lv != 0);
							}
						}
					}
				}
			}
			else
			{
				bool flag10 = this.quality_type == 0;
				if (flag10)
				{
					bool flag11 = this.name_type == 0;
					if (flag11)
					{
						foreach (int current7 in dictionary.Keys)
						{
							bool flag12 = dictionary[current7].stone_level == this.lv_type;
							if (flag12)
							{
								this.commonCreatrveImages(contain, image, current7, dic, lv != 0);
							}
						}
					}
					else
					{
						foreach (int current8 in dictionary.Keys)
						{
							bool flag13 = dictionary[current8].stone_level == this.lv_type && dictionary[current8].name_type == this.name_type;
							if (flag13)
							{
								this.commonCreatrveImages(contain, image, current8, dic, lv != 0);
							}
						}
					}
				}
				else
				{
					bool flag14 = this.name_type == 0;
					if (flag14)
					{
						foreach (int current9 in dictionary.Keys)
						{
							bool flag15 = dictionary[current9].stone_level == this.lv_type && dictionary[current9].quality == this.quality_type;
							if (flag15)
							{
								this.commonCreatrveImages(contain, image, current9, dic, lv != 0);
							}
						}
					}
					else
					{
						foreach (int current10 in dictionary.Keys)
						{
							bool flag16 = dictionary[current10].stone_level == this.lv_type && dictionary[current10].quality == this.quality_type && dictionary[current10].name_type == this.name_type;
							if (flag16)
							{
								this.commonCreatrveImages(contain, image, current10, dic, lv != 0);
							}
						}
					}
				}
			}
			this.showInfos(contain, dic);
			bool flag17 = lv != 0;
			if (flag17)
			{
				bool flag18 = this.list_dic.Count > 0;
				if (flag18)
				{
					this.thisbtnOclick(this.list_dic[0], int.Parse(this.list_dic[0].name));
				}
				else
				{
					this.nub = 0;
					this.needmoney_num = 0;
					this.buy_num.text = this.nub.ToString();
					this.money.text = this.needmoney_num.ToString();
				}
			}
		}

		private void showInfos(GameObject contain, Dictionary<int, GameObject> dic)
		{
			bool flag = dic == null;
			if (!flag)
			{
				foreach (int current in dic.Keys)
				{
					bool flag2 = dic[current].transform.FindChild("lock") != null;
					if (flag2)
					{
						Text component = dic[current].transform.FindChild("lock").GetComponent<Text>();
						component.text = "等级到达" + this.dic_allrune_data[current].stone_level + "可开启";
						component.gameObject.SetActive(this.dic_allrune_data[current].stone_level >= ModelBase<A3_RuneStoneModel>.getInstance().nowlv);
					}
					bool flag3 = dic[current].transform.FindChild("this") != null;
					if (flag3)
					{
					}
					GameObject gameObject = IconImageMgr.getInstance().createA3ItemIcon(ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)current), false, -1, 0.6f, false, -1, 0, false, false, false, -1, false, false);
					gameObject.transform.SetParent(dic[current].transform.FindChild("icon").transform, false);
					Text component2 = dic[current].transform.FindChild("name").GetComponent<Text>();
					component2.text = this.dic_allrune_data[current].item_name;
					GameObject gameObject2 = dic[current].transform.FindChild("1").gameObject;
					GameObject gameObject3 = dic[current].transform.FindChild("2").gameObject;
					List<GameObject> list = new List<GameObject>
					{
						gameObject2,
						gameObject3
					};
					gameObject3.SetActive(this.dic_allrune_data[current].compose_data.Count > 1);
					for (int i = 0; i < this.dic_allrune_data[current].compose_data.Count; i++)
					{
						string file = ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.dic_allrune_data[current].compose_data[i].id).file;
						list[i].transform.FindChild("icon").GetComponent<Image>().sprite = (Resources.Load(file, typeof(Sprite)) as Sprite);
						list[i].transform.FindChild("num").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)this.dic_allrune_data[current].compose_data[i].id) + "/" + this.dic_allrune_data[current].compose_data[i].num;
					}
				}
				this.commonScroview(contain, dic.Keys.Count);
			}
		}

		private void compose_bymonry()
		{
			bool flag = this.this_id == -1;
			if (!flag)
			{
				SXML sXML = XMLMgr.instance.GetSXML("item.item", "id==" + this.this_id);
				int @int = sXML.getInt("stamina_use");
				bool flag2 = ModelBase<A3_RuneStoneModel>.getInstance().nowstamina < @int;
				if (flag2)
				{
					flytxt.instance.fly("体力不足！", 1, default(Color), null);
				}
				else
				{
					BaseProxy<a3_RuneStoneProxy>.getInstance().sendporxy(2, this.this_id, this.nub, null);
				}
			}
		}

		public void refreshHvaeMaterialNum()
		{
			bool flag = this.dic_compose_showobjs != null;
			if (flag)
			{
				List<GameObject> list = new List<GameObject>();
				foreach (int current in this.dic_compose_showobjs.Keys)
				{
					GameObject gameObject = this.dic_compose_showobjs[current].transform.FindChild("1").gameObject;
					GameObject gameObject2 = this.dic_compose_showobjs[current].transform.FindChild("2").gameObject;
					list = new List<GameObject>
					{
						gameObject,
						gameObject2
					};
					for (int i = 0; i < this.dic_allrune_data[current].compose_data.Count; i++)
					{
						list[i].transform.FindChild("num").GetComponent<Text>().text = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)this.dic_allrune_data[current].compose_data[i].id) + "/" + this.dic_allrune_data[current].compose_data[i].num;
					}
				}
			}
		}

		public void RefreScrollLv(int lv)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.image_lv);
			gameObject.SetActive(true);
			gameObject.transform.SetParent(this.contain_lv.transform, false);
			gameObject.name = lv.ToString();
			gameObject.transform.FindChild("Text").GetComponent<Text>().text = lv + "级";
			new BaseButton(gameObject.transform, 1, 1).onClick = delegate(GameObject go)
			{
				this.ClosechoseOnClick(go, 1);
			};
			this.commonScroview(this.contain_lv, lv + 1);
		}

		private void addnum()
		{
			bool flag = this.min_num == 0;
			if (!flag)
			{
				this.nub++;
				this.zzzz();
			}
		}

		private void reducenum()
		{
			bool flag = this.min_num == 0;
			if (!flag)
			{
				this.nub--;
				this.zzzz();
			}
		}

		private void zzzz()
		{
			bool flag = this.nub <= 1;
			if (flag)
			{
				this.nub = 1;
			}
			else
			{
				bool flag2 = this.nub >= this.max_num;
				if (flag2)
				{
					this.nub = this.max_num;
				}
			}
			this.buy_num.text = this.nub.ToString();
			this.money.text = (this.needmoney_num * this.nub).ToString();
		}

		private void thisbtnOclick(GameObject go, int id)
		{
			foreach (int current in this.dic_compose_showobjs.Keys)
			{
				this.dic_compose_showobjs[current].transform.FindChild("this").gameObject.SetActive(false);
			}
			go.transform.FindChild("this").gameObject.SetActive(true);
			this.refreBynumAndMoney(id);
		}

		private void refreBynumAndMoney(int id)
		{
			List<int> list = new List<int>();
			bool flag = false;
			for (int i = 0; i < this.dic_allrune_data[id].compose_data.Count; i++)
			{
				bool flag2 = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)this.dic_allrune_data[id].compose_data[i].id) > 0;
				if (!flag2)
				{
					break;
				}
				int num = (int)Mathf.Floor((float)(ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)this.dic_allrune_data[id].compose_data[i].id) / this.dic_allrune_data[id].compose_data[i].num));
				bool flag3 = num <= 0;
				if (flag3)
				{
					flag = false;
					break;
				}
				flag = true;
				list.Add(num);
			}
			list.Sort();
			bool flag4 = flag;
			if (flag4)
			{
				this.min_num = 1;
				this.max_num = ((list[0] > 1) ? list[0] : 1);
				this.nub = 1;
				this.this_id = id;
				this.needmoney_num = ModelBase<A3_RuneStoneModel>.getInstance().getNeedMoney(id);
				base.getComponentByPath<Button>("compose/left/need_money").interactable = true;
			}
			else
			{
				this.min_num = 0;
				this.max_num = 0;
				this.nub = 0;
				this.this_id = -1;
				this.needmoney_num = 0;
				base.getComponentByPath<Button>("compose/left/need_money").interactable = false;
			}
			this.buy_num.text = this.nub.ToString();
			this.money.text = this.needmoney_num.ToString();
		}

		private void openorcloselistOnClock(bool isopen)
		{
			this.list_obj.SetActive(isopen);
			bool flag = !isopen;
			if (flag)
			{
				this.OpenchoseOnClick(null, this.list_list_contain);
			}
			else
			{
				this.ClosechoseOnClick(null, 2);
			}
			this.commomScrollRectPosition(base.getComponentByPath<ScrollRect>("list/down/scrollview"));
		}

		private void LvStaminaInfos()
		{
			int nowstamina = ModelBase<A3_RuneStoneModel>.getInstance().nowstamina;
			this.stamin.text = "体力值：    " + nowstamina;
			this.stamin_obj.value = (float)(nowstamina / 50);
			int nowlv = ModelBase<A3_RuneStoneModel>.getInstance().nowlv;
			int nowexp = ModelBase<A3_RuneStoneModel>.getInstance().nowexp;
			SXML sXML = XMLMgr.instance.GetSXML("rune_stone.compose", "level==" + nowlv);
			bool flag = sXML.getUint("exp") > 0u;
			uint @uint;
			if (flag)
			{
				@uint = sXML.getUint("exp");
			}
			else
			{
				@uint = XMLMgr.instance.GetSXML("rune_stone.compose", "level==" + (nowlv - 1)).getUint("exp");
			}
			this.exp.text = string.Concat(new object[]
			{
				nowlv,
				"级      ",
				nowexp,
				"/",
				@uint
			});
			this.exp_obj.value = (float)nowexp / @uint;
		}

		private void InitchosebtnOnclick(List<GameObject> list_contain, int type)
		{
			Action<GameObject> <>9__0;
			for (int i = 0; i < list_contain.Count; i++)
			{
				for (int j = 0; j < list_contain[i].transform.childCount; j++)
				{
					BaseButton arg_5E_0 = new BaseButton(list_contain[i].transform.GetChild(j).transform, 1, 1);
					Action<GameObject> arg_5E_1;
					if ((arg_5E_1 = <>9__0) == null)
					{
						arg_5E_1 = (<>9__0 = delegate(GameObject go)
						{
							this.ClosechoseOnClick(go, type);
						});
					}
					arg_5E_0.onClick = arg_5E_1;
				}
			}
			int nowlv = ModelBase<A3_RuneStoneModel>.getInstance().nowlv;
			for (int k = 2; k < nowlv + 1; k++)
			{
				this.RefreScrollLv(k);
			}
		}

		private void LvExpInfos(GameEvent e)
		{
			this.LvStaminaInfos();
		}

		private void InitSccrollHaveRunestonsGrids()
		{
			for (int i = 0; i < this.grids_nub; i++)
			{
				this.commonCreatrveImages(this.contain_haveRunestones, this.image_hanvRunestones, -1, null, false);
			}
		}

		private void RefreshSccrollHaveRunestonsGrid()
		{
			int count = ModelBase<A3_RuneStoneModel>.getInstance().getHaveRunestones().Keys.Count;
			bool flag = count <= this.grids_nub;
			if (flag)
			{
				bool flag2 = this.contain_haveRunestones.transform.childCount <= this.grids_nub;
				if (!flag2)
				{
					this.RefreshSccrollHaveRunestonsGrid(false, this.contain_haveRunestones.transform.childCount - this.grids_nub);
				}
			}
			else
			{
				int num = (count - this.grids_nub) % 5;
				int num2 = (num % 5 == 0) ? num : ((int)Mathf.Ceil((float)(num / 5)));
				this.RefreshSccrollHaveRunestonsGrid(true, (num <= 5) ? 5 : (num2 * 5));
			}
		}

		private void RefreshSccrollHaveRunestonsGrid(bool add, int num)
		{
			if (add)
			{
				for (int i = 0; i < num; i++)
				{
					this.commonCreatrveImages(this.contain_haveRunestones, this.image_hanvRunestones, -1, null, false);
				}
				this.commonScroview(this.contain_haveRunestones, this.grids_nub + num);
			}
			else
			{
				int childCount = this.contain_haveRunestones.transform.childCount;
				int num2 = childCount - num;
				for (int j = num2; j < childCount; j++)
				{
					UnityEngine.Object.Destroy(this.contain_haveRunestones.transform.GetChild(j).gameObject);
				}
				this.commonScroview(this.contain_haveRunestones, childCount - num);
			}
		}

		private void InitBagHaveRunestones()
		{
			Dictionary<uint, a3_BagItemData> dictionary = new Dictionary<uint, a3_BagItemData>();
			bool flag = ModelBase<A3_RuneStoneModel>.getInstance().getHaveRunestones() == null || dictionary == ModelBase<A3_RuneStoneModel>.getInstance().getHaveRunestones();
			if (!flag)
			{
				dictionary = ModelBase<A3_RuneStoneModel>.getInstance().getHaveRunestones();
				int num = 0;
				this.RefreshSccrollHaveRunestonsGrid();
				foreach (a3_BagItemData current in ModelBase<A3_RuneStoneModel>.getInstance().getHaveRunestones().Values)
				{
					this.CreatrveIcons(current, num);
					num++;
				}
			}
		}

		public void addHaveRunestones(a3_BagItemData data)
		{
			bool flag = !this.dic_runestones_obj.ContainsKey(data.id);
			if (flag)
			{
				this.RefreshSccrollHaveRunestonsGrid();
				this.CreatrveIcons(data, this.dic_runestones_obj.Count);
			}
		}

		public void removeHaveRunestones(uint id)
		{
			this.RefreshSccrollHaveRunestonsGrid();
			UnityEngine.Object.Destroy(this.dic_runestones_obj[id].transform.parent.gameObject);
			this.dic_runestones_obj.Remove(id);
			this.commonCreatrveImages(this.contain_haveRunestones, this.image_hanvRunestones, -1, null, false);
		}

		public void RefreshMark(uint id, bool ismark)
		{
			bool flag = this.dic_runestones_obj.ContainsKey(id);
			if (flag)
			{
				this.dic_runestones_obj[id].transform.FindChild("iconborder/ismark").gameObject.SetActive(ismark);
			}
		}

		private void CreatrveIcons(a3_BagItemData data, int i)
		{
			GameObject icon = IconImageMgr.getInstance().createA3ItemIcon(data, true, -1, 0.8f, false);
			icon.transform.SetParent(this.contain_haveRunestones.transform.GetChild(i).transform, false);
			icon.name = data.id.ToString();
			this.dic_runestones_obj[data.id] = icon;
			BaseButton baseButton = new BaseButton(icon.transform, 1, 1);
			baseButton.onClick = delegate(GameObject go)
			{
				this.onItemClick(data, icon, data.id, 1);
			};
		}

		private void onItemClick(a3_BagItemData one, GameObject icon, uint id, int type)
		{
			ArrayList arrayList = new ArrayList();
			bool flag = type == 1;
			if (flag)
			{
				a3_BagItemData a3_BagItemData = ModelBase<a3_BagModel>.getInstance().getItems(false)[id];
				arrayList.Add(a3_BagItemData);
				bool activeSelf = this.compose_obj.activeSelf;
				if (activeSelf)
				{
					arrayList.Add(runestone_tipstype.compose_tips);
				}
				else
				{
					arrayList.Add(runestone_tipstype.dress_tip);
				}
			}
			else
			{
				bool flag2 = type == 2;
				if (flag2)
				{
					arrayList.Add(one);
					arrayList.Add(runestone_tipstype.dressdown_tip);
				}
				else
				{
					bool flag3 = type == 3;
					if (flag3)
					{
						a3_BagItemData a3_BagItemData2 = ModelBase<a3_BagModel>.getInstance().getItems(false)[id];
						arrayList.Add(a3_BagItemData2);
						arrayList.Add(runestone_tipstype.decompose_tip);
					}
				}
			}
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_RUNESTONETIP, arrayList, false);
		}

		private void btnSwitch(GameObject go)
		{
			this.switchbl = !(go.name == "compose_btn");
			bool flag = this.oldswitchbl == this.switchbl;
			if (!flag)
			{
				this.compose_obj.SetActive(!this.switchbl);
				this.composebtn_image.SetActive(!this.switchbl);
				this.dress_obj.SetActive(this.switchbl);
				this.dressbtn_image.SetActive(this.switchbl);
				this.decomposeBg.SetActive(this.switchbl);
				this.rct_scrollview_haveRunestones.sizeDelta = new Vector2(400f, (float)(this.switchbl ? 402 : 528));
				this.commomScrollRectPosition(this.rct_scrollview_haveRunestones.GetComponent<ScrollRect>());
				this.oldswitchbl = this.switchbl;
			}
		}

		public void commonCreatrveImages(GameObject contain, GameObject image, int num = -1, Dictionary<int, GameObject> dic = null, bool isbtn = false)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(image);
			gameObject.SetActive(true);
			gameObject.transform.SetParent(contain.transform, false);
			bool flag = num != -1;
			if (flag)
			{
				gameObject.name = num.ToString();
			}
			bool flag2 = dic != null;
			if (flag2)
			{
				dic[num] = gameObject;
			}
			if (isbtn)
			{
				this.list_dic.Add(gameObject);
				new BaseButton(gameObject.transform, 1, 1).onClick = delegate(GameObject go)
				{
					this.thisbtnOclick(go, int.Parse(go.name));
				};
			}
		}

		public void commonScroview(GameObject contain, int childcount)
		{
			RectTransform component = contain.GetComponent<RectTransform>();
			GridLayoutGroup component2 = contain.GetComponent<GridLayoutGroup>();
			int num = (int)Mathf.Floor((component.sizeDelta.x + component2.spacing.x) / (component2.cellSize.x + component2.spacing.x));
			int num2 = (int)Mathf.Ceil((float)childcount / (float)num);
			component.sizeDelta = new Vector2(component.sizeDelta.x, (float)num2 * component2.cellSize.y + (float)(num2 - 1) * component2.spacing.y);
		}

		public void commomScrollRectPosition(ScrollRect sr)
		{
			bool flag = sr != null;
			if (flag)
			{
				sr.verticalNormalizedPosition = 1f;
			}
		}

		private void ScrollControler()
		{
			this.scrollControler = new ScrollControler();
			this.scrollControler1 = new ScrollControler();
			this.scrollControler2 = new ScrollControler();
			this.scrollControler3 = new ScrollControler();
			this.scrollControler4 = new ScrollControler();
			this.scrollControler5 = new ScrollControler();
			this.scrollControler6 = new ScrollControler();
			this.scrollControler7 = new ScrollControler();
			this.scrollControler8 = new ScrollControler();
			this.scrollControler9 = new ScrollControler();
			this.scrollControler.create(base.getComponentByPath<ScrollRect>("compose/left/bg/down/scrollview"), 4);
			this.scrollControler1.create(base.getComponentByPath<ScrollRect>("compose/left/bg/top/lv/scrollview"), 4);
			this.scrollControler2.create(base.getComponentByPath<ScrollRect>("compose/left/bg/top/quality/scrollview"), 4);
			this.scrollControler3.create(base.getComponentByPath<ScrollRect>("compose/left/bg/top/name/scrollview"), 4);
			this.scrollControler4.create(base.getComponentByPath<ScrollRect>("haverunestones/scrollview"), 4);
			this.scrollControler5.create(base.getComponentByPath<ScrollRect>("decompose/scroll_view"), 4);
			this.scrollControler6.create(base.getComponentByPath<ScrollRect>("list/down/scrollview"), 4);
			this.scrollControler7.create(base.getComponentByPath<ScrollRect>("list/top/lv/scrollview"), 4);
			this.scrollControler8.create(base.getComponentByPath<ScrollRect>("list/top/quality/scrollview"), 4);
			this.scrollControler9.create(base.getComponentByPath<ScrollRect>("list/top/name/scrollview"), 4);
		}

		private void bttttt()
		{
			BaseProxy<a3_RuneStoneProxy>.getInstance().sendporxy(6, 1638, 0, null);
		}
	}
}
