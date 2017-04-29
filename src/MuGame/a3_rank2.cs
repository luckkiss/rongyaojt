using DG.Tweening;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_rank2 : AchiveSkin
	{
		private Dictionary<int, rankinfos> dic = ModelBase<a3_RankModel>.getInstance().dicrankinfo;

		private Dictionary<int, GameObject> dicGo = new Dictionary<int, GameObject>();

		private GameObject nowgo = null;

		private GameObject conatin;

		private GameObject image;

		private GameObject contain_nowinfo;

		private GameObject image_nowinfo;

		private GameObject contain_nextinfo;

		private GameObject image_nextinfo;

		private Image now_image;

		private Image next_image;

		private GameObject panel;

		private Image last_Image;

		private Text onShoeorHideTitileTxt;

		private Text ach_num;

		private Text exp;

		private Slider expimg;

		private GameObject ruledes;

		private int runeid;

		private int runeexp;

		public static a3_rank2 _instance;

		private ScrollControler scrollControer0;

		private ScrollControler scrollControer1;

		private ScrollControler scrollControer2;

		private bool isaddlv = false;

		public a3_rank2(Window win, Transform tran) : base(win, tran)
		{
		}

		public override void init()
		{
			a3_rank2._instance = this;
			this.scrollControer0 = new ScrollControler();
			ScrollRect componentByPath = base.getComponentByPath<ScrollRect>("panel_top/ScrollRect");
			this.scrollControer0.create(componentByPath, 4);
			this.scrollControer1 = new ScrollControler();
			ScrollRect componentByPath2 = base.getComponentByPath<ScrollRect>("panel_centre/now_attributeinfos/Scroll_rect");
			this.scrollControer1.create(componentByPath2, 4);
			this.scrollControer2 = new ScrollControler();
			ScrollRect componentByPath3 = base.getComponentByPath<ScrollRect>("panel_centre/next_attributeinfos/Scroll_rect");
			this.scrollControer2.create(componentByPath3, 4);
			this.ach_num = base.getComponentByPath<Text>("panel_down/point");
			this.ruledes = base.transform.FindChild("ruledes_bg").gameObject;
			this.now_image = base.getComponentByPath<Image>("panel_centre/now_rankinfos/panel/now/now");
			this.next_image = base.getComponentByPath<Image>("panel_centre/now_rankinfos/panel/next/next");
			this.panel = base.transform.FindChild("panel_centre/now_rankinfos/panel/next").gameObject;
			this.exp = base.getComponentByPath<Text>("panel_centre/exp/Text");
			this.expimg = base.getComponentByPath<Slider>("panel_centre/exp");
			this.onShoeorHideTitileTxt = base.getComponentByPath<Text>("panel_down/showorhide_btn/Text");
			this.conatin = base.transform.FindChild("panel_top/ScrollRect/Contain").gameObject;
			this.image = base.transform.FindChild("panel_top/ScrollRect/Image").gameObject;
			this.contain_nowinfo = base.transform.FindChild("panel_centre/now_attributeinfos/Scroll_rect/contain").gameObject;
			this.image_nowinfo = base.transform.FindChild("panel_centre/now_attributeinfos/Scroll_rect/Image").gameObject;
			this.contain_nextinfo = base.transform.FindChild("panel_centre/next_attributeinfos/Scroll_rect/contain").gameObject;
			this.image_nextinfo = base.transform.FindChild("panel_centre/next_attributeinfos/Scroll_rect/Image").gameObject;
			BaseButton baseButton = new BaseButton(base.transform.FindChild("panel_down/rule_btn"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onShowdes);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("ruledes_bg/ruledes/close"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.onClosedes);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("close"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.onClose);
			BaseButton baseButton4 = new BaseButton(base.transform.FindChild("panel_down/upgrade_btn1"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.onAddLv);
			BaseButton baseButton5 = new BaseButton(base.transform.FindChild("panel_down/upgrade_btn2"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.onAddLv);
			BaseButton baseButton6 = new BaseButton(base.transform.FindChild("panel_down/showorhide_btn"), 1, 1);
			baseButton6.onClick = new Action<GameObject>(this.onShoeorHideTitile);
			this.cteatrve();
			this.SetSliderValue(0f);
		}

		public override void onShowed()
		{
			this.RefreshInfos(a3_RankModel.now_id);
			this.refreshAch_point();
			BaseProxy<A3_RankProxy>.getInstance().addEventListener(A3_RankProxy.RANKADDLV, new Action<GameEvent>(this.onAddLvOver));
			BaseProxy<A3_RankProxy>.getInstance().addEventListener(A3_RankProxy.RANKREFRESH, new Action<GameEvent>(this.OnRefresh));
		}

		public override void onClosed()
		{
			BaseProxy<A3_RankProxy>.getInstance().removeEventListener(A3_RankProxy.RANKADDLV, new Action<GameEvent>(this.onAddLvOver));
			BaseProxy<A3_RankProxy>.getInstance().removeEventListener(A3_RankProxy.RANKREFRESH, new Action<GameEvent>(this.OnRefresh));
		}

		private void onAddLvOver(GameEvent e)
		{
			this.isaddlv = true;
			this.RefreshInfos(e.data["title"]);
		}

		private void OnRefresh(GameEvent e)
		{
			this.refreshAch_point();
		}

		private void Update()
		{
		}

		public void RefreshInfos(int now_id)
		{
			this.onShoeorHideTitileTxt.text = (a3_RankModel.nowisactive ? "隐藏称号" : "显示称号");
			int count = this.dic.Keys.Count;
			bool flag = now_id == 0;
			if (flag)
			{
				this.GetImage(now_id, this.now_image);
				this.GetImage(now_id + 1, this.next_image);
				this.exp.text = a3_RankModel.nowexp + "/" + ModelBase<a3_RankModel>.getInstance().dicrankinfo[now_id + 1].rankexp;
				this.SetSliderValue((a3_RankModel.nowexp > 0) ? ((float)a3_RankModel.nowexp / (float)ModelBase<a3_RankModel>.getInstance().dicrankinfo[now_id + 1].rankexp) : 0f);
				this.GetAttr(now_id + 1, this.contain_nextinfo, this.image_nextinfo);
			}
			else
			{
				bool flag2 = now_id == count;
				if (flag2)
				{
					this.panel.SetActive(false);
					base.transform.FindChild("background/att_bg_right").gameObject.SetActive(false);
					base.transform.FindChild("panel_centre/exp").gameObject.SetActive(false);
					base.transform.FindChild("background/exp_point").gameObject.SetActive(false);
					this.GetImage(now_id, this.now_image);
					this.GetAttr(now_id, this.contain_nowinfo, this.image_nowinfo);
					this.deleteAttr(this.contain_nextinfo, this.image_nextinfo);
					this.SetSliderValue(1f);
					this.exp.text = "max";
				}
				else
				{
					this.GetImage(now_id, this.now_image);
					this.GetAttr(now_id, this.contain_nowinfo, this.image_nowinfo);
					this.GetImage(now_id + 1, this.next_image);
					this.GetAttr(now_id + 1, this.contain_nextinfo, this.image_nextinfo);
					this.exp.text = a3_RankModel.nowexp + "/" + ModelBase<a3_RankModel>.getInstance().dicrankinfo[now_id + 1].rankexp;
					this.SetSliderValue((a3_RankModel.nowexp > 0) ? ((float)a3_RankModel.nowexp / (float)ModelBase<a3_RankModel>.getInstance().dicrankinfo[now_id + 1].rankexp) : 0f);
				}
			}
			bool flag3 = this.nowgo != null;
			if (flag3)
			{
				this.nowgo.transform.FindChild("bg").GetComponent<Image>().enabled = false;
			}
			bool flag4 = this.dicGo.ContainsKey(now_id);
			if (flag4)
			{
				this.dicGo[now_id].transform.FindChild("bg").GetComponent<Image>().enabled = true;
				this.nowgo = this.dicGo[now_id];
			}
		}

		private void SetSliderValue(float a)
		{
			DOTween.To(() => this.expimg.value, delegate(float s)
			{
				this.expimg.value = s;
			}, a, 0.5f);
		}

		private void GetImage(int id, Image image)
		{
			string path = "icon/achievement/title_ui/" + id;
			image.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			image.SetNativeSize();
		}

		private void GetAttr(int id, GameObject conatin, GameObject image)
		{
			this.deleteAttr(conatin, image);
			SXML sXML = XMLMgr.instance.GetSXML("achievement.title", "title_id==" + id);
			List<SXML> nodeList = sXML.GetNodeList("nature", "");
			for (int i = 0; i < nodeList.Count; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(image);
				gameObject.SetActive(true);
				gameObject.transform.SetParent(conatin.transform, false);
				gameObject.transform.FindChild("Text").GetComponent<Text>().text = Globle.getAttrNameById(nodeList[i].getInt("att_type")) + ":" + nodeList[i].getInt("att_value");
			}
			this.refreshs(conatin, image);
		}

		private void deleteAttr(GameObject conatin, GameObject image)
		{
			bool flag = conatin.transform.childCount > 0;
			if (flag)
			{
				for (int i = 0; i < conatin.transform.childCount; i++)
				{
					UnityEngine.Object.Destroy(conatin.transform.GetChild(i).gameObject);
				}
			}
		}

		private void Destory(GameObject gameObject)
		{
			throw new NotImplementedException();
		}

		private void cteatrve()
		{
			foreach (int current in this.dic.Keys)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.image);
				gameObject.SetActive(true);
				gameObject.transform.SetParent(this.conatin.transform, false);
				Image component = gameObject.transform.FindChild("Image").GetComponent<Image>();
				string path = "icon/achievement/title_ui/" + current;
				component.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
				component.SetNativeSize();
				this.dicGo[current] = gameObject;
			}
			this.refresh();
		}

		private void refresh()
		{
			int childCount = this.conatin.transform.childCount;
			RectTransform component = this.conatin.GetComponent<RectTransform>();
			RectTransform component2 = this.image.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(component2.sizeDelta.x * (float)childCount, component2.sizeDelta.y);
		}

		private void refreshs(GameObject contains, GameObject images)
		{
			int childCount = contains.transform.childCount;
			RectTransform component = contains.GetComponent<RectTransform>();
			RectTransform component2 = images.GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(component2.sizeDelta.x, component2.sizeDelta.y * (float)childCount);
		}

		private void onShowdes(GameObject go)
		{
			this.ruledes.SetActive(true);
		}

		private void onClosedes(GameObject go)
		{
			this.ruledes.SetActive(false);
		}

		private void onAddLv(GameObject go)
		{
			this.runeid = a3_RankModel.now_id;
			this.runeexp = a3_RankModel.nowexp;
			bool flag = ModelBase<PlayerModel>.getInstance().ach_point >= 500u;
			if (flag)
			{
				bool flag2 = go.name == "upgrade_btn1";
				if (flag2)
				{
					BaseProxy<A3_RankProxy>.getInstance().sendProxy(A3_RankProxy.RANKADDLV, 1, false);
				}
				else
				{
					bool flag3 = go.name == "upgrade_btn2";
					if (flag3)
					{
						BaseProxy<A3_RankProxy>.getInstance().sendProxy(A3_RankProxy.RANKADDLV, 2, false);
					}
				}
			}
			else
			{
				flytxt.instance.fly("名望值不够！", 0, default(Color), null);
			}
		}

		private void onShoeorHideTitile(GameObject go)
		{
			bool flag = a3_RankModel.now_id > 0;
			if (flag)
			{
				bool nowisactive = a3_RankModel.nowisactive;
				if (nowisactive)
				{
					a3_RankModel.nowisactive = false;
					this.onShoeorHideTitileTxt.text = "显示称号";
				}
				else
				{
					a3_RankModel.nowisactive = true;
					this.onShoeorHideTitileTxt.text = "隐藏称号";
				}
				BaseProxy<A3_RankProxy>.getInstance().sendProxy(A3_RankProxy.RANKREFRESH, -1, a3_RankModel.nowisactive);
			}
			else
			{
				flytxt.instance.fly("称号未激活！", 1, default(Color), null);
			}
		}

		private void onClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_ACHIEVEMENT);
		}

		public void refreshAch_point()
		{
			BaseProxy<A3_RankProxy>.getInstance().sendProxy(A3_RankProxy.RANKREFRESH, -1, false);
			this.ach_num.text = (Globle.getBigText(ModelBase<PlayerModel>.getInstance().ach_point) ?? "");
		}
	}
}
