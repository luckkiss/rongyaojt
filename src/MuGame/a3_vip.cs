using DG.Tweening;
using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_vip : Window
	{
		private BaseButton btnClose;

		private Image imageCurLevel;

		private Image ExpImg;

		private Text textBuyNum;

		private Text textNextLevel;

		private BaseButton btnRecharge;

		private GameObject isManLvl;

		private List<GameObject> tableList;

		private Transform conBtnTable;

		private Transform conGiftTable;

		private Transform conPermissionTable;

		private BaseButton btnGetGift;

		private Transform giftlvl;

		private Text lvl;

		private BaseButton up_btn;

		private BaseButton dow_btn;

		private Transform viplnl_view;

		private BaseButton vipGiftBtn;

		private BaseButton vipPriBtn;

		private GameObject tab1;

		private GameObject tab2;

		private Vector2 v;

		private Vector2 v1;

		private Vector2 v2;

		private GameObject tip;

		private Transform conState;

		private A3_VipModel vipModel;

		private List<SXML> vipXml;

		public static a3_vip instan;

		private BaseButton Up_btn;

		private BaseButton down_btn;

		private RectTransform con;

		private Transform conBtn;

		private RectTransform gftCon;

		private int nowlook_viplvl = 1;

		private Dictionary<int, GameObject> VipBtnList = new Dictionary<int, GameObject>();

		private bool Toclose = false;

		private int iconIndex = 0;

		private bool canRun = true;

		private float speed = 0.5f;

		private float wingIconSizeX = 0f;

		private float wingIconSizeY = 0f;

		private float boundaryLeft = 0f;

		private float boundaryRight = 0f;

		public override void init()
		{
			a3_vip.instan = this;
			bool flag = this.uiData != null;
			if (flag)
			{
				int type = (int)this.uiData[0];
				this.setopen(type);
			}
			this.tip = base.transform.FindChild("tip").gameObject;
			this.btnClose = new BaseButton(base.getTransformByPath("btn_close"), 1, 1);
			this.btnClose.onClick = new Action<GameObject>(this.OnCLoseClick);
			this.isManLvl = base.transform.FindChild("topCon/isMaxLvl").gameObject;
			this.imageCurLevel = base.getComponentByPath<Image>("topCon/Image_level");
			this.ExpImg = base.getComponentByPath<Image>("topCon/Image_exp");
			this.textBuyNum = base.getComponentByPath<Text>("topCon/Text_bg_1/Text_num");
			this.textNextLevel = base.getComponentByPath<Text>("topCon/Text_level");
			this.btnRecharge = new BaseButton(base.getTransformByPath("btn_recharge"), 1, 1);
			this.btnRecharge.onClick = new Action<GameObject>(this.OnRechargeBtnClick);
			this.vipGiftBtn = new BaseButton(base.getTransformByPath("bottomCon/top_btn/btn_1"), 1, 1);
			this.vipGiftBtn.onClick = new Action<GameObject>(this.OpenVipGift);
			this.vipPriBtn = new BaseButton(base.getTransformByPath("bottomCon/top_btn/btn_2"), 1, 1);
			this.vipPriBtn.onClick = new Action<GameObject>(this.OpenVipPri);
			this.tab1 = base.getGameObjectByPath("bottomCon/tab_1");
			this.tab2 = base.getGameObjectByPath("bottomCon/tab_2");
			this.conBtnTable = base.getTransformByPath("bottomCon/tab_1/con_btn");
			this.viplnl_view = this.conBtnTable.FindChild("scrollview/con");
			this.conGiftTable = base.getTransformByPath("bottomCon/tab_1/con_gift");
			this.conPermissionTable = base.getTransformByPath("bottomCon/tab_1/con_permission");
			this.btnGetGift = new BaseButton(base.getTransformByPath("bottomCon/tab_1/con_gift/btn_get"), 1, 1);
			this.giftlvl = base.getTransformByPath("bottomCon/tab_1/con_gift/Text_level");
			this.lvl = this.giftlvl.GetComponent<Text>();
			this.btnGetGift.onClick = new Action<GameObject>(this.OnGetBtnClick);
			this.up_btn = new BaseButton(base.getTransformByPath("bottomCon/tab_1/con_btn/up_btn"), 1, 1);
			this.dow_btn = new BaseButton(base.getTransformByPath("bottomCon/tab_1/con_btn/dow_btn"), 1, 1);
			this.up_btn.onClick = new Action<GameObject>(this.onUp_btn);
			this.dow_btn.onClick = new Action<GameObject>(this.onDow_btn);
			this.con = this.conPermissionTable.FindChild("view/con").GetComponent<RectTransform>();
			this.conBtn = this.conBtnTable.FindChild("scrollview/con");
			this.gftCon = this.conGiftTable.FindChild("view/con").GetComponent<RectTransform>();
			base.transform.FindChild("Image_left").GetComponent<CanvasGroup>().blocksRaycasts = false;
			base.transform.FindChild("Image_right").GetComponent<CanvasGroup>().blocksRaycasts = false;
			this.conState = base.getTransformByPath("bottomCon/tab_2");
			this.vipModel = ModelBase<A3_VipModel>.getInstance();
			this.vipXml = this.vipModel.VipLevelXML;
			this.v = this.con.position;
			this.v1 = this.conBtn.position;
			this.v2 = this.gftCon.position;
			this.InitBtnList();
			this.InitVip_priList();
			this.vipGiftBtn.interactable = false;
			base.init();
		}

		public override void onShowed()
		{
			this.Toclose = false;
			this.OnVipTabHanle(1);
			A3_VipModel expr_16 = this.vipModel;
			expr_16.OnLevelChange = (Action)Delegate.Combine(expr_16.OnLevelChange, new Action(this.OnVipLevelChange));
			A3_VipModel expr_3D = this.vipModel;
			expr_3D.OnExpChange = (Action)Delegate.Combine(expr_3D.OnExpChange, new Action(this.OnExpChange));
			BaseProxy<A3_VipProxy>.getInstance().GetVip();
			this.OnVipLevelChange();
			this.OnExpChange();
			this.OnGiftBtnRefresh();
			bool flag = this.VipBtnList[0] != null;
			if (flag)
			{
				this.BtnList();
				this.VipBtnList[0].GetComponent<Button>().interactable = false;
			}
			this.con.position = this.v;
			this.conBtn.position = this.v1;
			this.gftCon.position = this.v2;
			this.lvl.text = "1";
			this.nowlook_viplvl = 1;
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			base.onShowed();
			this.tip.SetActive(false);
			GRMap.GAME_CAMERA.SetActive(false);
			bool flag2 = a3_relive.instans;
			if (flag2)
			{
				base.transform.SetAsLastSibling();
				a3_relive.instans.FX.SetActive(false);
			}
		}

		public override void onClosed()
		{
			A3_VipModel expr_07 = this.vipModel;
			expr_07.OnLevelChange = (Action)Delegate.Remove(expr_07.OnLevelChange, new Action(this.OnVipLevelChange));
			A3_VipModel expr_2E = this.vipModel;
			expr_2E.OnExpChange = (Action)Delegate.Remove(expr_2E.OnExpChange, new Action(this.OnExpChange));
			GRMap.GAME_CAMERA.SetActive(true);
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			bool flag = a3_relive.instans;
			if (flag)
			{
				a3_relive.instans.FX.SetActive(true);
			}
			base.onClosed();
			InterfaceMgr.getInstance().itemToWin(this.Toclose, this.uiName);
		}

		private void showtip()
		{
		}

		private void OnVipLevelChange()
		{
			Text component = this.imageCurLevel.transform.FindChild("Text").GetComponent<Text>();
			int level = this.vipModel.Level;
			component.text = level.ToString();
			bool flag = level >= this.vipModel.GetMaxVipLevel();
			if (flag)
			{
				this.textNextLevel.gameObject.SetActive(false);
				this.textBuyNum.gameObject.transform.parent.gameObject.SetActive(false);
				this.isManLvl.SetActive(true);
			}
			else
			{
				this.textNextLevel.text = "VIP" + (level + 1);
				this.isManLvl.SetActive(false);
				this.textNextLevel.gameObject.SetActive(true);
				this.textBuyNum.gameObject.transform.parent.gameObject.SetActive(true);
			}
		}

		private void OnExpChange()
		{
			int nextLvlMaxExp = this.vipModel.GetNextLvlMaxExp();
			int num = nextLvlMaxExp - this.vipModel.Exp;
			this.textBuyNum.text = num.ToString();
			bool flag = nextLvlMaxExp > 0;
			if (flag)
			{
				this.ExpImg.fillAmount = (float)this.vipModel.Exp / (float)nextLvlMaxExp;
			}
			else
			{
				this.ExpImg.fillAmount = 1f;
			}
		}

		private void InitBtnList()
		{
			GameObject gameObject = this.conBtnTable.FindChild("btnTemp").gameObject;
			Transform parent = this.conBtnTable.FindChild("scrollview/con");
			int maxVipLevel = this.vipModel.GetMaxVipLevel();
			this.lvl.text = "1";
			for (int i = 0; i < maxVipLevel; i++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				Text component = gameObject2.transform.FindChild("Text").GetComponent<Text>();
				component.text = "VIP" + (i + 1);
				gameObject2.name = (i + 1).ToString();
				this.VipBtnList[i] = gameObject2;
				BaseButton baseButton = new BaseButton(gameObject2.transform, 1, 1);
				baseButton.onClick = delegate(GameObject go)
				{
					int level = int.Parse(go.name);
					this.nowlook_viplvl = level;
					this.OnGiftBtnRefresh();
					this.OnVipTabHanle(level);
					this.BtnList();
					go.GetComponent<Button>().interactable = false;
					this.lvl.text = level.ToString();
				};
				gameObject2.transform.SetParent(parent, false);
				gameObject2.SetActive(true);
			}
		}

		private void BtnList()
		{
			Transform transform = this.conBtnTable.FindChild("scrollview/con");
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).GetComponent<Button>().interactable = true;
			}
		}

		private void InitVip_priList()
		{
			GameObject gameObject = this.conState.FindChild("scrollview/con/item").gameObject;
			Transform parent = this.conState.FindChild("scrollview/con");
			int priMum = this.vipModel.GetPriMum();
			for (int i = 0; i < priMum; i++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject2.SetActive(true);
				Text component = gameObject2.transform.FindChild("per_tip/Text").GetComponent<Text>();
				component.text = this.vipModel.Gettype_Name(0, i);
				for (int j = 1; j <= gameObject2.transform.FindChild("con").childCount; j++)
				{
					int showType = this.vipModel.GetShowType(i);
					bool flag = showType == 1;
					if (flag)
					{
						GameObject gameObject3 = gameObject2.transform.FindChild("con/" + j + "/Text").gameObject;
						gameObject3.SetActive(false);
						string value = this.vipModel.GetValue(j, i);
						string a = value;
						if (!(a == "0"))
						{
							if (a == "1")
							{
								gameObject2.transform.FindChild("con/" + j + "/Yes").gameObject.SetActive(true);
							}
						}
						else
						{
							gameObject2.transform.FindChild("con/" + j + "/No").gameObject.SetActive(true);
						}
					}
					else
					{
						bool flag2 = showType == 2;
						if (flag2)
						{
							Text component2 = gameObject2.transform.FindChild("con/" + j + "/Text").GetComponent<Text>();
							string value2 = this.vipModel.GetValue(j, i);
							component2.text = value2;
						}
						else
						{
							bool flag3 = showType == 3;
							if (flag3)
							{
								Text component3 = gameObject2.transform.FindChild("con/" + j + "/Text").GetComponent<Text>();
								component3.text = double.Parse(this.vipModel.GetValue(j, i)) * 100.0 + "%";
							}
						}
					}
				}
				gameObject2.transform.SetParent(parent, false);
			}
		}

		private void OnVipTabHanle(int level)
		{
			this.OnContainerRefresh(this.conPermissionTable, level);
			this.OnVipGiftRefresh(this.conGiftTable, level);
		}

		private void OnContainerRefresh(Transform conTab, List<int> list)
		{
			GameObject gameObject = conTab.FindChild("ImageTemp").gameObject;
			RectTransform component = conTab.FindChild("view/con").GetComponent<RectTransform>();
			float x = gameObject.transform.GetComponent<RectTransform>().sizeDelta.x;
			Vector2 sizeDelta = new Vector2((float)list.Count * x, component.sizeDelta.y);
			component.sizeDelta = sizeDelta;
			int childCount = component.childCount;
			int i;
			for (i = 0; i < list.Count; i++)
			{
				int id = list[i];
				bool flag = i >= childCount;
				if (flag)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.transform.SetParent(component, false);
					this.OnRefreshObjByID(gameObject2, id);
				}
				else
				{
					Transform child = component.GetChild(i);
					this.OnRefreshObjByID(child.gameObject, id);
				}
			}
			for (int j = i; j < childCount; j++)
			{
				component.GetChild(j).gameObject.SetActive(false);
			}
		}

		private void OnContainerRefresh(Transform conTab, int lvl)
		{
			bool flag = lvl <= 0;
			if (!flag)
			{
				GameObject gameObject = conTab.FindChild("ImageTemp").gameObject;
				RectTransform component = conTab.FindChild("view/con").GetComponent<RectTransform>();
				for (int i = 0; i < component.childCount; i++)
				{
					UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
				}
				int num = 0;
				for (int j = 0; j < this.vipModel.GetPriMum(); j++)
				{
					bool flag2 = this.vipModel.GetValue(lvl, j) != this.vipModel.GetValue(lvl - 1, j);
					if (flag2)
					{
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
						gameObject2.transform.SetParent(component, false);
						Sprite sprite = Resources.Load("icon/type/" + this.vipModel.GetType(lvl, j), typeof(Sprite)) as Sprite;
						bool flag3 = sprite != null;
						if (flag3)
						{
							gameObject2.transform.FindChild("icon").GetComponent<Image>().sprite = sprite;
						}
						Text component2 = gameObject2.transform.FindChild("pri_text").GetComponent<Text>();
						component2.text = this.vipModel.Gettype_Name(lvl, j);
						gameObject2.SetActive(true);
						num++;
					}
				}
				float x = gameObject.transform.GetComponent<RectTransform>().sizeDelta.x;
				Vector2 sizeDelta = new Vector2((float)num * x, component.sizeDelta.y);
				component.sizeDelta = sizeDelta;
			}
		}

		private void OnVipGiftRefresh(Transform conTab, int lvl)
		{
			bool flag = lvl <= 0;
			if (!flag)
			{
				GameObject gameObject = conTab.FindChild("ImageTemp").gameObject;
				RectTransform component = conTab.FindChild("view/con").GetComponent<RectTransform>();
				for (int i = 0; i < component.childCount; i++)
				{
					UnityEngine.Object.Destroy(component.GetChild(i).gameObject);
				}
				int num = 0;
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				dictionary = this.vipModel.giftdata[this.vipModel.GetVipGiftListByLevel(lvl)];
				foreach (int current in dictionary.Keys)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.transform.SetParent(component, false);
					Text component2 = gameObject2.transform.FindChild("pri_text").GetComponent<Text>();
					uint id = (uint)current;
					component2.text = ModelBase<a3_BagModel>.getInstance().getItemDataById(id).item_name + "x" + dictionary[current];
					GameObject gameObject3 = gameObject2.transform.FindChild("icon/icon_Img").gameObject;
					GameObject gameObject4 = IconImageMgr.getInstance().createA3ItemIcon(ModelBase<a3_BagModel>.getInstance().getItemDataById(id), false, -1, 0.8f, false, -1, 0, false, false, false, -1, false, false);
					bool flag2 = ModelBase<a3_BagModel>.getInstance().getItemDataById(id).item_type == 2;
					if (flag2)
					{
						gameObject4.transform.FindChild("iconborder/equip_canequip").gameObject.SetActive(false);
						gameObject4.transform.FindChild("iconborder/equip_self").gameObject.SetActive(false);
					}
					gameObject4.transform.SetParent(gameObject3.transform, false);
					gameObject2.SetActive(true);
					new BaseButton(gameObject2.transform, 1, 1).onClick = delegate(GameObject go)
					{
						this.tip.SetActive(true);
						a3_ItemData itemDataById = ModelBase<a3_BagModel>.getInstance().getItemDataById(id);
						this.tip.transform.FindChild("text_bg/name/namebg").GetComponent<Text>().text = itemDataById.item_name;
						this.tip.transform.FindChild("text_bg/name/namebg").GetComponent<Text>().color = Globle.getColorByQuality(itemDataById.quality);
						bool flag3 = itemDataById.use_limit <= 0;
						if (flag3)
						{
							this.tip.transform.FindChild("text_bg/name/dengji").GetComponent<Text>().text = "无限制";
						}
						else
						{
							this.tip.transform.FindChild("text_bg/name/dengji").GetComponent<Text>().text = itemDataById.use_limit + "转";
						}
						this.tip.transform.FindChild("text_bg/text").GetComponent<Text>().text = StringUtils.formatText(itemDataById.desc);
						this.tip.transform.FindChild("text_bg/iconbg/icon").GetComponent<Image>().sprite = (Resources.Load(itemDataById.file, typeof(Sprite)) as Sprite);
						new BaseButton(this.tip.transform.FindChild("close_btn"), 1, 1).onClick = new Action<GameObject>(this.<OnVipGiftRefresh>b__48_1);
					};
					num++;
				}
				float x = gameObject.transform.GetComponent<RectTransform>().sizeDelta.x;
				Vector2 sizeDelta = new Vector2((float)num * x, component.sizeDelta.y);
				component.sizeDelta = sizeDelta;
			}
		}

		public void OnGiftBtnRefresh()
		{
			foreach (uint current in this.vipModel.isGetVipGift)
			{
				bool flag = (long)this.nowlook_viplvl == (long)((ulong)current);
				if (flag)
				{
					this.btnGetGift.interactable = false;
					this.btnGetGift.transform.FindChild("text").GetComponent<Text>().text = "已领取";
					break;
				}
				this.btnGetGift.interactable = true;
				this.btnGetGift.transform.FindChild("text").GetComponent<Text>().text = "领取";
			}
		}

		private void OnGetBtnClick(GameObject go)
		{
			Debug.Log("On_Get_Btn_Click");
			BaseProxy<A3_VipProxy>.getInstance().GetVipGift(this.nowlook_viplvl);
			BaseProxy<A3_VipProxy>.getInstance().GetVip();
			bool flag = this.vipModel.Level < this.nowlook_viplvl;
			if (flag)
			{
				flytxt.instance.fly("vip等级不足！！", 1, default(Color), null);
			}
			else
			{
				flytxt.instance.fly("领取成功", 1, default(Color), null);
			}
		}

		private void OnRefreshObjByID(GameObject obj, int id)
		{
			Debug.Log("OnRefreshObjByID");
			obj.SetActive(true);
		}

		private void OnRechargeBtnClick(GameObject go)
		{
			Debug.Log("On_Recharge_Btn_Click");
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_RECHARGE, null, false);
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_VIP);
		}

		private void OnCLoseClick(GameObject go)
		{
			this.Toclose = true;
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_VIP);
		}

		private void onUp_btn(GameObject go)
		{
			RectTransform component = this.viplnl_view.GetComponent<RectTransform>();
			Vector2 anchoredPosition = component.anchoredPosition;
			float num = anchoredPosition.y - this.wingIconSizeY;
			bool flag = num < this.boundaryRight || !this.canRun;
			if (!flag)
			{
				Vector3 localPosition = this.viplnl_view.localPosition;
				float endValue = localPosition.y - this.wingIconSizeY;
				Tween t = this.viplnl_view.DOLocalMoveX(endValue, this.speed, false);
				t.SetEase(Ease.Linear);
				this.canRun = false;
				t.OnComplete(delegate
				{
					this.OnTweeComplete();
					this.iconIndex++;
				});
			}
		}

		private void OnTweeComplete()
		{
			this.canRun = true;
		}

		private void onDow_btn(GameObject go)
		{
			this.viplnl_view.position = new Vector2(this.viplnl_view.position.x, this.viplnl_view.position.y - 10f);
		}

		private void OpenVipGift(GameObject go)
		{
			this.tab1.SetActive(true);
			this.tab2.SetActive(false);
			this.vipGiftBtn.interactable = false;
			this.vipPriBtn.interactable = true;
		}

		private void OpenVipPri(GameObject go)
		{
			this.tab1.SetActive(false);
			this.tab2.SetActive(true);
			this.vipGiftBtn.interactable = true;
			this.vipPriBtn.interactable = false;
		}

		public void setopen(int type)
		{
			bool flag = type == 1;
			if (flag)
			{
				this.OpenVipGift(null);
			}
			else
			{
				bool flag2 = type == 2;
				if (flag2)
				{
					this.OpenVipPri(null);
				}
			}
		}
	}
}
