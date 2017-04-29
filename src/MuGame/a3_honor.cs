using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_honor : AchiveSkin
	{
		private Transform conTypeTab;

		private Transform conPage;

		private GameObject tempPage;

		private Text textPoint;

		private BaseButton btnUpgrade;

		private BaseButton btnClose;

		private BaseButton btnLeft;

		private BaseButton btnRight;

		private Text textPageIndex;

		private ScrollRect scrollPage;

		private A3_AchievementModel achModel;

		private Dictionary<uint, GameObject> dicPage = new Dictionary<uint, GameObject>();

		private int maxNum = 8;

		private int pageIndex = 0;

		private uint selectType = 0u;

		private int maxPageNum = 0;

		public a3_honor(Window win, Transform tran) : base(win, tran)
		{
		}

		public override void init()
		{
			this.achModel = ModelBase<A3_AchievementModel>.getInstance();
			this.conTypeTab = base.getTransformByPath("con_tab");
			this.conPage = base.getTransformByPath("con_page/view/con");
			this.tempPage = base.getGameObjectByPath("con_page/tempPage");
			this.textPoint = base.getComponentByPath<Text>("Text_point/Text_point");
			this.btnLeft = new BaseButton(base.getTransformByPath("btn_select/btn_left"), 1, 1);
			this.btnLeft.onClick = new Action<GameObject>(this.OnLeftClick);
			this.btnRight = new BaseButton(base.getTransformByPath("btn_select/btn_right"), 1, 1);
			this.btnRight.onClick = new Action<GameObject>(this.OnRightClick);
			this.textPageIndex = base.getComponentByPath<Text>("btn_select/bg/Text");
			this.btnClose = new BaseButton(base.getTransformByPath("btn_close"), 1, 1);
			this.btnClose.onClick = new Action<GameObject>(this.OnCloseClick);
			this.scrollPage = base.getComponentByPath<ScrollRect>("con_page/view");
			this.InitBtnTab();
			base.init();
		}

		public override void onShowed()
		{
			BaseProxy<A3_RankProxy>.getInstance().addEventListener(0u, new Action<GameEvent>(this.OnRefreshPageEvent));
			BaseProxy<A3_RankProxy>.getInstance().addEventListener(1u, new Action<GameEvent>(this.OnShowGetPrize));
			BaseProxy<A3_RankProxy>.getInstance().addEventListener(2u, new Action<GameEvent>(this.OnRefreshPageEvent));
			A3_AchievementModel expr_4F = this.achModel;
			expr_4F.OnAchievementChange = (Action)Delegate.Combine(expr_4F.OnAchievementChange, new Action(this.OnPointChange));
			this.OnPointChange();
			this.OnShowAchievementPage(this.selectType, this.pageIndex);
			base.onShowed();
		}

		public override void onClosed()
		{
			BaseProxy<A3_RankProxy>.getInstance().removeEventListener(0u, new Action<GameEvent>(this.OnRefreshPageEvent));
			BaseProxy<A3_RankProxy>.getInstance().removeEventListener(1u, new Action<GameEvent>(this.OnShowGetPrize));
			BaseProxy<A3_RankProxy>.getInstance().removeEventListener(2u, new Action<GameEvent>(this.OnRefreshPageEvent));
			A3_AchievementModel expr_4F = this.achModel;
			expr_4F.OnAchievementChange = (Action)Delegate.Remove(expr_4F.OnAchievementChange, new Action(this.OnPointChange));
			this.ClearContainer();
			this.scrollPage.StopMovement();
			base.onClosed();
		}

		private void OnShowAchievementPage(uint showType, int pageIndex)
		{
			List<AchievementData> achievenementDataByType = this.achModel.GetAchievenementDataByType(showType);
			int num = this.maxNum * pageIndex;
			bool flag = num >= achievenementDataByType.Count;
			if (!flag)
			{
				this.OnShowSelect(showType, pageIndex);
				this.ClearContainer();
				for (int i = 0; i < this.maxNum; i++)
				{
					int num2 = num + i;
					bool flag2 = num2 >= achievenementDataByType.Count;
					if (flag2)
					{
						return;
					}
					GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.tempPage);
					AchievementData achievementData = achievenementDataByType[num2];
					gameObject.name = achievementData.id.ToString();
					Text component = gameObject.transform.FindChild("Image_point/text_point").GetComponent<Text>();
					Text component2 = gameObject.transform.FindChild("text_1").GetComponent<Text>();
					Text component3 = gameObject.transform.FindChild("text_state").GetComponent<Text>();
					component.text = achievementData.point.ToString();
					component2.text = achievementData.name;
					component3.text = achievementData.desc;
					this.OnRefreshPageState(gameObject, achievementData);
					gameObject.transform.SetParent(this.conPage, false);
					gameObject.SetActive(true);
					this.dicPage[achievementData.id] = gameObject;
				}
				RectTransform component4 = this.conPage.GetComponent<RectTransform>();
				Vector2 anchoredPosition = new Vector2(component4.anchoredPosition.x, 0f);
				component4.anchoredPosition = anchoredPosition;
				this.scrollPage.StopMovement();
			}
		}

		private void ClearContainer()
		{
			foreach (GameObject current in this.dicPage.Values)
			{
				UnityEngine.Object.Destroy(current);
			}
			this.dicPage.Clear();
		}

		private void OnShowSelect(uint showType, int pageIndex)
		{
			int count = this.achModel.GetAchievenementDataByType(showType).Count;
			bool flag = count % this.maxNum != 0;
			if (flag)
			{
				this.maxPageNum = count / this.maxNum + 1;
			}
			else
			{
				this.maxPageNum = count / this.maxNum;
			}
			this.textPageIndex.text = pageIndex + 1 + "/" + this.maxPageNum;
		}

		private void InitBtnTab()
		{
			GameObject gameObject = this.conTypeTab.FindChild("btnTemp").gameObject;
			Transform transform = this.conTypeTab.FindChild("view/con");
			List<uint> listCategory = this.achModel.listCategory;
			for (int i = 0; i < listCategory.Count; i++)
			{
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject2.name = listCategory[i].ToString();
				Text component = gameObject2.transform.FindChild("Text").GetComponent<Text>();
				component.text = this.achModel.GetCategoryName(listCategory[i]);
				gameObject2.transform.SetParent(transform, false);
				gameObject2.SetActive(true);
			}
			TabControl tabControl = new TabControl();
			tabControl.create(transform.gameObject, base.gameObject, 0, 0, false);
			tabControl.onClickHanle = delegate(TabControl t)
			{
				int seletedIndex = t.getSeletedIndex();
				this.selectType = (uint)seletedIndex;
				this.OnShowTypeChange(this.selectType);
			};
		}

		private void OnShowTypeChange(uint showType)
		{
			this.pageIndex = 0;
			this.ClearContainer();
			this.OnShowAchievementPage(this.selectType, this.pageIndex);
		}

		private void OnRefreshPageState(GameObject page, AchievementData data)
		{
			Text component = page.transform.FindChild("text_state/text_plan").GetComponent<Text>();
			Slider component2 = page.transform.FindChild("expbar/slider").GetComponent<Slider>();
			BaseButton baseButton = new BaseButton(page.transform.FindChild("btn_get"), 1, 1);
			Transform transform = page.transform.FindChild("con_prize");
			Text text = null;
			Transform transform2 = page.transform.FindChild("con_prize/panel_0/icon/Text");
			bool flag = transform2 != null;
			if (flag)
			{
				text = transform2.GetComponent<Text>();
			}
			switch (data.state)
			{
			case AchievementState.UNREACHED:
			{
				baseButton.gameObject.SetActive(false);
				transform.GetChild(0).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.SetActive(false);
				bool flag2 = data.bndyb == 0u;
				if (flag2)
				{
					text.transform.parent.gameObject.SetActive(false);
					transform.GetComponent<Image>().enabled = false;
				}
				text.text = data.bndyb.ToString();
				break;
			}
			case AchievementState.REACHED:
			{
				baseButton.gameObject.SetActive(true);
				baseButton.onClick = new Action<GameObject>(this.OnGetPrzieClick);
				transform.GetChild(0).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.SetActive(false);
				bool flag3 = data.bndyb == 0u;
				if (flag3)
				{
					text.transform.parent.gameObject.SetActive(false);
					transform.GetComponent<Image>().enabled = false;
				}
				text.text = data.bndyb.ToString();
				break;
			}
			case AchievementState.RECEIVED:
			{
				baseButton.gameObject.SetActive(false);
				transform.GetChild(0).gameObject.SetActive(false);
				transform.GetChild(1).gameObject.SetActive(true);
				bool flag4 = data.bndyb == 0u;
				if (flag4)
				{
					text.transform.parent.gameObject.SetActive(false);
				}
				text.text = "";
				break;
			}
			}
			component.text = string.Concat(new object[]
			{
				"(",
				(data.degree > data.condition) ? data.condition : data.degree,
				"/",
				data.condition,
				")"
			});
			component2.maxValue = data.condition;
			component2.value = data.degree;
		}

		private void OnPointChange()
		{
			this.textPoint.text = this.achModel.AchievementPoint.ToString();
			Transform transform = base.transform.FindChild("zhuanshi/text");
			bool flag = transform != null;
			if (flag)
			{
				Text component = transform.GetComponent<Text>();
				bool flag2 = component != null;
				if (flag2)
				{
					component.text = ModelBase<PlayerModel>.getInstance().gift.ToString();
				}
			}
		}

		public void OnRefreshPageEvent(GameEvent e)
		{
			List<uint> listAchievementChange = this.achModel.listAchievementChange;
			foreach (uint current in listAchievementChange)
			{
				bool flag = this.dicPage.ContainsKey(current);
				if (flag)
				{
					GameObject page = this.dicPage[current];
					AchievementData achievementDataByID = this.achModel.GetAchievementDataByID(current);
					this.OnRefreshPageState(page, achievementDataByID);
				}
			}
			this.achModel.listAchievementChange.Clear();
		}

		private void OnShowGetPrize(GameEvent e)
		{
			uint getAchievementID = this.achModel.GetAchievementID;
			AchievementData achievementDataByID = this.achModel.GetAchievementDataByID(getAchievementID);
			uint bndyb = achievementDataByID.bndyb;
			bool flag = bndyb > 0u;
			if (flag)
			{
				flytxt.instance.fly("获得绑定钻石 :" + bndyb, 0, default(Color), null);
			}
			bool flag2 = this.dicPage.ContainsKey(getAchievementID);
			if (flag2)
			{
				GameObject page = this.dicPage[getAchievementID];
				this.OnRefreshPageState(page, achievementDataByID);
				this.SortPage(page, achievementDataByID);
			}
		}

		private void SortPage(GameObject page, AchievementData data)
		{
			switch (data.state)
			{
			case AchievementState.REACHED:
				page.transform.SetAsFirstSibling();
				break;
			case AchievementState.RECEIVED:
				page.transform.SetAsLastSibling();
				this.ClearContainer();
				this.scrollPage.StopMovement();
				this.OnPointChange();
				this.OnShowAchievementPage(this.selectType, this.pageIndex);
				break;
			}
		}

		private void OnUpgradeClick(GameObject go)
		{
		}

		private void OnCloseClick(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_ACHIEVEMENT);
		}

		private void OnLeftClick(GameObject go)
		{
			bool flag = this.pageIndex <= 0;
			if (!flag)
			{
				this.pageIndex--;
				this.OnShowAchievementPage(this.selectType, this.pageIndex);
			}
		}

		private void OnRightClick(GameObject go)
		{
			bool flag = this.pageIndex + 1 >= this.maxPageNum;
			if (!flag)
			{
				this.pageIndex++;
				this.OnShowAchievementPage(this.selectType, this.pageIndex);
			}
		}

		private void OnGetPrzieClick(GameObject go)
		{
			uint achievementID = uint.Parse(go.transform.parent.name);
			BaseProxy<A3_RankProxy>.getInstance().GetAchievementPrize(achievementID);
		}
	}
}
