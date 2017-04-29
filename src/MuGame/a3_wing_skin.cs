using Cross;
using DG.Tweening;
using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_wing_skin : Window
	{
		public enum ExpBarState
		{
			init,
			addExp,
			expBarup,
			point,
			star
		}

		public static a3_wing_skin instance;

		private Text textName;

		private Text textLevel;

		private Text textStage;

		private Transform conIcon;

		private BaseButton btnTurnLeft;

		private BaseButton btnTurnRight;

		private Transform TouchPanel;

		private BaseButton btnHelp;

		private GameObject tempPgaeAtt;

		private Transform conAtt;

		private Transform conLevelTable;

		private Text textLevelCostItemSum;

		private Text textSliderState;

		private BaseButton btnLevelUpgrade;

		private BaseButton btnLevelOneKey;

		private Transform conStar;

		private Slider sliderExpBar;

		private Transform conCompleteTable;

		private GameObject iconTemp;

		private Transform conStageTable;

		private Text textStageCostItemSum;

		private Text textStageRate;

		private Slider sliderStage;

		private BaseButton btnStageUp;

		private Transform conHelpPanel;

		private BaseButton btnCloseHelp;

		private A3_WingModel wingModel;

		private SXML wingXML;

		public GameObject wingAvatar;

		private GameObject avatarCamera;

		private float wingIconSizeX = 0f;

		private float wingIconSizeY = 0f;

		private float boundaryLeft = 0f;

		private float boundaryRight = 0f;

		private Transform aniStarTrans;

		private Transform conStarPoint;

		public int ShowStage_yuxuan = 0;

		private Animator success;

		private Animator fail;

		private processStruct process;

		private int needobj_id;

		private uint neednum = 0u;

		private int needobjid_stage;

		private int neednum_stage;

		private const int LEVEL_INDEX = 0;

		private const int STAGE_INDEX = 1;

		private const int COMPLETE_INDEX = 2;

		public int pageIndex;

		private WingsData curWing = null;

		private int MaxLevel = 10;

		private Animator aniExp;

		private GameObject aniLevelUp;

		private float getExp = 0f;

		private float expSpeed = 1f;

		private Vector3 oriPos;

		private Tween curT;

		private a3_wing_skin.ExpBarState currentState = a3_wing_skin.ExpBarState.init;

		private Action ExpBarHandle = null;

		private int costItem;

		private ProfessionAvatar m_proAvatar;

		private GameObject scene_Obj;

		private Dictionary<int, GameObject> dicIcon = new Dictionary<int, GameObject>();

		private int iconIndex = 0;

		private bool canRun = true;

		private float speed = 0.5f;

		public override void init()
		{
			this.wingModel = ModelBase<A3_WingModel>.getInstance();
			this.wingXML = this.wingModel.WingXML;
			this.textName = base.getComponentByPath<Text>("Text_name");
			this.textLevel = base.getComponentByPath<Text>("Text_name/lvl");
			this.textStage = base.getComponentByPath<Text>("Text_name/stage");
			this.btnHelp = new BaseButton(base.getTransformByPath("title/help"), 1, 1);
			this.btnHelp.onClick = new Action<GameObject>(this.OnOpenHelp);
			this.tempPgaeAtt = base.getGameObjectByPath("att_temp");
			this.conAtt = base.getTransformByPath("att/grid");
			this.conLevelTable = base.getTransformByPath("con_level");
			this.textSliderState = base.getComponentByPath<Text>("con_level/expbar/text");
			this.btnLevelUpgrade = new BaseButton(base.getTransformByPath("con_level/upgrade"), 1, 1);
			this.btnLevelUpgrade.onClick = new Action<GameObject>(this.OnUpgradeClick);
			this.btnLevelOneKey = new BaseButton(base.getTransformByPath("con_level/onekey"), 1, 1);
			this.btnLevelOneKey.onClick = new Action<GameObject>(this.OnUpgradeOneKey);
			this.conStar = base.getTransformByPath("con_level/con_star");
			this.sliderExpBar = base.getComponentByPath<Slider>("con_level/expbar/slider");
			this.conCompleteTable = base.getTransformByPath("con_complete");
			this.conIcon = base.getTransformByPath("panel_icon/mask/scroll_rect/con_icon");
			this.iconTemp = base.getGameObjectByPath("panel_icon/icon_temp");
			this.btnTurnLeft = new BaseButton(base.getTransformByPath("panel_icon/btn_left"), 1, 1);
			this.btnTurnLeft.onClick = new Action<GameObject>(this.OnTurnLeftClick);
			this.btnTurnRight = new BaseButton(base.getTransformByPath("panel_icon/btn_right"), 1, 1);
			this.btnTurnRight.onClick = new Action<GameObject>(this.OnTurnRightClick);
			BaseButton baseButton = new BaseButton(base.getTransformByPath("btn_close"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onClose);
			this.textLevelCostItemSum = base.getComponentByPath<Text>("con_level/upgrade/text");
			this.conStageTable = base.getTransformByPath("con_stage");
			this.textStageRate = base.getComponentByPath<Text>("con_stage/rate");
			this.textStageCostItemSum = base.getComponentByPath<Text>("con_stage/improve/text");
			this.btnStageUp = new BaseButton(base.getTransformByPath("con_stage/improve"), 1, 1);
			this.btnStageUp.onClick = new Action<GameObject>(this.OnStageUpClick);
			this.sliderStage = base.getComponentByPath<Slider>("con_stage/slider");
			this.sliderStage.onValueChanged.AddListener(new UnityAction<float>(this.OnSliderValueChange));
			this.conHelpPanel = base.getTransformByPath("panel_help");
			this.btnCloseHelp = new BaseButton(base.getTransformByPath("panel_help/closeBtn"), 1, 1);
			this.btnCloseHelp.onClick = new Action<GameObject>(this.OnCloseHelp);
			this.aniExp = this.conLevelTable.GetComponent<Animator>();
			this.aniLevelUp = base.getGameObjectByPath("ani_lvlUP");
			this.process = new processStruct(new Action<float>(this.Update_wing), "a3_wing_skin", false, false);
			base.getEventTrigerByPath("panel_icon").onDrag = new EventTriggerListener.VectorDelegate(this.onDragIcon);
			this.aniStarTrans = base.getTransformByPath("con_level/con_star/ani_star");
			this.conStarPoint = base.getTransformByPath("con_level/point");
			base.getEventTrigerByPath("con_avatar/avatar_touch").onDrag = new EventTriggerListener.VectorDelegate(this.OnDrag);
			BaseButton baseButton2 = new BaseButton(base.getTransformByPath("btnWing"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.OnEquWing);
			this.success = base.transform.FindChild("ani_success").GetComponent<Animator>();
			this.fail = base.transform.FindChild("ani_fail").GetComponent<Animator>();
			this.needobjid_stage = XMLMgr.instance.GetSXML("wings.stage_item", "").getInt("item_id");
			this.needobj_id = XMLMgr.instance.GetSXML("wings.level_item", "").getInt("item_id");
		}

		public void RefreshPanel(int stage, int level)
		{
			bool flag = level >= this.wingModel.GetStageMaxLevel(stage);
			if (flag)
			{
				this.pageIndex = 1;
				bool flag2 = stage >= this.wingModel.GetXmlMaxStage();
				if (flag2)
				{
					this.pageIndex = 2;
				}
			}
			else
			{
				this.pageIndex = 0;
			}
		}

		public override void onShowed()
		{
			a3_wing_skin.instance = this;
			(CrossApp.singleton.getPlugin("processManager") as processManager).addProcess(this.process, false);
			BaseProxy<A3_WingProxy>.getInstance().addEventListener(0u, new Action<GameEvent>(this.OnLevelExpChange));
			BaseProxy<A3_WingProxy>.getInstance().addEventListener(1u, new Action<GameEvent>(this.OnStageChange));
			BaseProxy<A3_WingProxy>.getInstance().addEventListener(2u, new Action<GameEvent>(this.OnLevelExpChange));
			BaseProxy<A3_WingProxy>.getInstance().addEventListener(3u, new Action<GameEvent>(this.OnShowStageChange));
			BaseProxy<A3_WingProxy>.getInstance().addEventListener(4u, new Action<GameEvent>(this.OnStageNO));
			int stage = this.wingModel.Stage;
			int level = this.wingModel.Level;
			int showStage = this.wingModel.ShowStage;
			this.ShowStage_yuxuan = 0;
			this.curWing = this.wingModel.dicWingsData[stage];
			WingsData data = this.wingModel.dicWingsData[stage];
			this.InitExpSlider(this.wingModel.Stage, this.wingModel.Level, this.wingModel.Exp);
			this.CreatAllWingsIcon(stage);
			this.ShowTitle(data);
			this.RefreshAtt(stage, level);
			this.RefreshStar(level);
			this.RefreshCostInfo(this.wingModel.Stage, this.wingModel.Level);
			this.OnSetIconBGImage(showStage);
			this.OnStageSliderSetting(this.curWing);
			this.RefreshPanel(stage, level);
			this.ShowPage(this.pageIndex);
			this.InitExpState();
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_FUNCTIONBAR);
			base.transform.FindChild("ig_bg_bg").gameObject.SetActive(false);
			base.onShowed();
			GRMap.GAME_CAMERA.SetActive(false);
			this.CreatWingAvatar();
		}

		public override void onClosed()
		{
			a3_wing_skin.instance = null;
			(CrossApp.singleton.getPlugin("processManager") as processManager).removeProcess(this.process, false);
			BaseProxy<A3_WingProxy>.getInstance().removeEventListener(0u, new Action<GameEvent>(this.OnLevelExpChange));
			BaseProxy<A3_WingProxy>.getInstance().removeEventListener(1u, new Action<GameEvent>(this.OnStageChange));
			BaseProxy<A3_WingProxy>.getInstance().removeEventListener(2u, new Action<GameEvent>(this.OnLevelExpChange));
			BaseProxy<A3_WingProxy>.getInstance().removeEventListener(3u, new Action<GameEvent>(this.OnShowStageChange));
			this.DisposeAvatar();
			this.DisposeIcon();
			this.fail.gameObject.SetActive(false);
			this.success.gameObject.SetActive(false);
			InterfaceMgr.getInstance().changeState(InterfaceMgr.STATE_NORMAL);
			base.transform.FindChild("con_level/expbar/expUp").gameObject.SetActive(false);
			GRMap.GAME_CAMERA.SetActive(true);
		}

		private void Update_wing(float deltaTime)
		{
			bool flag = this.ExpBarHandle != null;
			if (flag)
			{
				this.ExpBarHandle();
			}
		}

		private void RefreshStar(int goalLevle)
		{
			bool flag = goalLevle > this.MaxLevel;
			if (flag)
			{
				goalLevle = this.MaxLevel;
			}
			int i;
			for (i = 0; i < goalLevle; i++)
			{
				this.conStar.GetChild(i).GetChild(0).gameObject.SetActive(true);
				this.conStar.GetChild(i).GetChild(1).gameObject.SetActive(false);
			}
			for (int j = i; j < this.MaxLevel; j++)
			{
				this.conStar.GetChild(j).GetChild(0).gameObject.SetActive(false);
				this.conStar.GetChild(j).GetChild(1).gameObject.SetActive(true);
			}
		}

		private void OnLevelExpChange(GameEvent e)
		{
			int lastLevel = this.wingModel.lastLevel;
			int level = this.wingModel.Level;
			bool flag = level > lastLevel;
			if (flag)
			{
				this.getExp += this.sliderExpBar.maxValue - this.sliderExpBar.value;
				this.getExp += (float)this.wingModel.Exp;
			}
			else
			{
				this.getExp += (float)(this.wingModel.Exp - this.wingModel.lastExp);
			}
			bool flag2 = this.getExp > 0f;
			if (flag2)
			{
				this.ChangeState(a3_wing_skin.ExpBarState.addExp);
			}
		}

		private void EnterAddExp()
		{
			this.aniExp.SetBool("Add", true);
			this.ExpBarHandle = (Action)Delegate.Combine(this.ExpBarHandle, new Action(this.OnExpValueChane));
		}

		private void ExitAddExp()
		{
			this.aniExp.SetBool("Add", false);
			this.ExpBarHandle = null;
		}

		private void OnExpValueChane()
		{
			this.expSpeed = 20f;
			bool flag = this.getExp < this.expSpeed;
			if (flag)
			{
				this.expSpeed = this.getExp;
			}
			bool flag2 = this.sliderExpBar.maxValue - this.sliderExpBar.value < this.expSpeed;
			if (flag2)
			{
				this.expSpeed = this.sliderExpBar.maxValue - this.sliderExpBar.value;
			}
			this.getExp -= this.expSpeed;
			this.sliderExpBar.value += this.expSpeed;
			float num = 0f;
			bool flag3 = this.sliderExpBar.value > this.sliderExpBar.maxValue;
			if (flag3)
			{
				num = this.sliderExpBar.value - this.sliderExpBar.maxValue;
			}
			this.getExp += num;
			this.textSliderState.text = this.sliderExpBar.value + "/" + this.sliderExpBar.maxValue;
			bool flag4 = this.sliderExpBar.normalizedValue >= 1f;
			if (flag4)
			{
				this.ChangeState(a3_wing_skin.ExpBarState.expBarup);
			}
			else
			{
				bool flag5 = this.getExp <= 0f;
				if (flag5)
				{
					this.ChangeState(a3_wing_skin.ExpBarState.init);
				}
			}
		}

		private void EnterInit()
		{
			bool flag = this.getExp > 0f;
			if (flag)
			{
				this.ChangeState(a3_wing_skin.ExpBarState.addExp);
			}
			else
			{
				this.btnLevelUpgrade.interactable = true;
				this.btnLevelOneKey.interactable = true;
				this.ExpBarHandle = null;
				this.aniLevelUp.SetActive(false);
			}
		}

		private void ExitInit()
		{
			this.btnLevelUpgrade.interactable = false;
			this.btnLevelOneKey.interactable = false;
		}

		private void EnterExpBarUp()
		{
			this.aniExp.SetBool("Up", true);
			this.ExpBarHandle = (Action)Delegate.Combine(this.ExpBarHandle, new Action(this.CheckExpBarUp));
		}

		private void ExitExpBarUp()
		{
			this.aniExp.SetBool("Up", false);
			this.ExpBarHandle = null;
			bool flag = this.wingModel.Level != 0;
			if (flag)
			{
				this.aniStarTrans.localPosition = this.conStar.GetChild(this.wingModel.Level - 1).localPosition;
			}
		}

		private void CheckExpBarUp()
		{
			AnimatorStateInfo currentAnimatorStateInfo = this.aniExp.GetCurrentAnimatorStateInfo(0);
			bool flag = currentAnimatorStateInfo.normalizedTime >= 1f && currentAnimatorStateInfo.IsName("ExpBarUp");
			if (flag)
			{
				this.ChangeState(a3_wing_skin.ExpBarState.point);
			}
		}

		private void EnterPoint()
		{
			this.InitExpSlider(this.wingModel.Stage, this.wingModel.Level, 0);
			this.aniLevelUp.SetActive(true);
			this.ShowPoint();
			this.ExpBarHandle = null;
		}

		private void ExitPoint()
		{
			this.ExpBarHandle = null;
			this.aniLevelUp.SetActive(false);
		}

		private void ShowPoint()
		{
			this.conStarPoint.gameObject.SetActive(true);
			this.oriPos = this.conStarPoint.position;
			this.curT = this.conStarPoint.DOMove(this.aniStarTrans.position, 0.9f, false).SetEase(Ease.Linear).OnComplete(new TweenCallback(this.OnComplete));
		}

		private void OnComplete()
		{
			debug.Log(" on complete");
			this.conStarPoint.position = this.oriPos;
			this.conStarPoint.gameObject.SetActive(false);
			this.curT.Kill(false);
			this.ChangeState(a3_wing_skin.ExpBarState.star);
		}

		private void EnterStar()
		{
			this.aniExp.SetBool("Star", true);
			this.ExpBarHandle = (Action)Delegate.Combine(this.ExpBarHandle, new Action(this.CheckStar));
		}

		private void ExitStar()
		{
			this.aniExp.SetBool("Star", false);
			int stage = this.wingModel.Stage;
			WingsData data = this.wingModel.dicWingsData[stage];
			this.ShowTitle(data);
			this.RefreshStar(this.wingModel.Level);
			this.RefreshAtt(this.wingModel.Stage, this.wingModel.Level);
			this.RefreshCostInfo(this.wingModel.Stage, this.wingModel.Level);
			this.RefreshPanel(this.wingModel.Stage, this.wingModel.Level);
			this.ShowPage(this.pageIndex);
			this.ExpBarHandle = null;
		}

		private void CheckStar()
		{
			AnimatorStateInfo currentAnimatorStateInfo = this.aniExp.GetCurrentAnimatorStateInfo(0);
			bool flag = currentAnimatorStateInfo.normalizedTime >= 1f && currentAnimatorStateInfo.IsName("Star");
			if (flag)
			{
				this.ChangeState(a3_wing_skin.ExpBarState.init);
			}
		}

		private void InitExpSlider(int stage, int level, int exp)
		{
			this.sliderExpBar.maxValue = this.wingModel.GetLevelUpMaxExp(stage, level);
			this.sliderExpBar.value = (float)exp;
			this.textSliderState.text = this.sliderExpBar.value + "/" + this.sliderExpBar.maxValue;
		}

		private void OnUpgradeClick(GameObject go)
		{
			bool flag = (long)ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)this.needobj_id) < (long)((ulong)this.neednum);
			if (flag)
			{
				this.addtoget(ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.needobj_id));
			}
			BaseProxy<A3_WingProxy>.getInstance().SendUpgradeLevel(false);
		}

		private void OnUpgradeOneKey(GameObject go)
		{
			bool flag = (long)ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)this.needobj_id) < (long)((ulong)this.neednum);
			if (flag)
			{
				this.addtoget(ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.needobj_id));
			}
			BaseProxy<A3_WingProxy>.getInstance().SendAutoUpgradeLevel();
		}

		private void addtoget(a3_ItemData item)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(item);
			arrayList.Add(InterfaceMgr.A3_WIBG_SKIN);
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_ITEMLACK, arrayList, false);
		}

		private void InitExpState()
		{
			this.ChangeState(a3_wing_skin.ExpBarState.init);
		}

		private void ChangeState(a3_wing_skin.ExpBarState newState)
		{
			switch (this.currentState)
			{
			case a3_wing_skin.ExpBarState.init:
				this.ExitInit();
				break;
			case a3_wing_skin.ExpBarState.addExp:
				this.ExitAddExp();
				break;
			case a3_wing_skin.ExpBarState.expBarup:
				this.ExitExpBarUp();
				break;
			case a3_wing_skin.ExpBarState.point:
				this.ExitPoint();
				break;
			case a3_wing_skin.ExpBarState.star:
				this.ExitStar();
				break;
			}
			this.currentState = newState;
			switch (newState)
			{
			case a3_wing_skin.ExpBarState.init:
				this.EnterInit();
				break;
			case a3_wing_skin.ExpBarState.addExp:
				this.EnterAddExp();
				break;
			case a3_wing_skin.ExpBarState.expBarup:
				this.EnterExpBarUp();
				break;
			case a3_wing_skin.ExpBarState.point:
				this.EnterPoint();
				break;
			case a3_wing_skin.ExpBarState.star:
				this.EnterStar();
				break;
			}
		}

		private void OnStageChange(GameEvent e)
		{
			int stage = this.wingModel.Stage;
			WingsData wingsData = this.wingModel.dicWingsData[stage];
			this.ShowTitle(wingsData);
			this.RefreshCostInfo(this.wingModel.Stage, this.wingModel.Level);
			this.RefreshPanel(this.wingModel.ShowStage, this.wingModel.Level);
			this.ShowPage(this.pageIndex);
			this.RefreshAtt(this.wingModel.Stage, this.wingModel.Level);
			this.RefreshStar(this.wingModel.Level);
			this.OnUnLockNweStage();
			this.InitExpSlider(this.wingModel.Stage, this.wingModel.Level, 0);
			this.costItem = (int)wingsData.stageCrystalMin;
			this.textStageCostItemSum.text = this.costItem.ToString();
			this.neednum_stage = this.costItem;
			this.success.gameObject.SetActive(true);
			this.sliderStage.value = 0f;
			this.success.Play("wing_jjsuccess", -1, 0f);
			this.curWing = this.wingModel.dicWingsData[stage];
			this.OnStageSliderSetting(this.curWing);
			this.DisposeAvatar();
			this.CreatWingAvatar();
		}

		private void OnStageNO(GameEvent e)
		{
			this.success.gameObject.SetActive(false);
			this.fail.gameObject.SetActive(true);
			this.fail.Play("wing_jjfail", -1, 0f);
		}

		private void RefreshCostInfo(int stage, int level)
		{
			bool flag = this.pageIndex == 0;
			if (flag)
			{
				this.textLevelCostItemSum.text = Globle.getBigText(this.wingModel.GetLevelUpCostItemSum(stage, level));
				this.neednum = this.wingModel.GetLevelUpCostItemSum(stage, level);
			}
			else
			{
				bool flag2 = this.pageIndex == 1;
				if (flag2)
				{
				}
			}
		}

		private void ShowPage(int pageIndex)
		{
			switch (pageIndex)
			{
			case 0:
				this.conLevelTable.gameObject.SetActive(true);
				this.conStageTable.gameObject.SetActive(false);
				this.conCompleteTable.gameObject.SetActive(false);
				break;
			case 1:
				this.conLevelTable.gameObject.SetActive(false);
				this.conStageTable.gameObject.SetActive(true);
				this.conCompleteTable.gameObject.SetActive(false);
				break;
			case 2:
				this.conLevelTable.gameObject.SetActive(false);
				this.conStageTable.gameObject.SetActive(false);
				this.conCompleteTable.gameObject.SetActive(true);
				break;
			}
		}

		private void RefreshAtt(int curStage, int curLevel)
		{
			SXML node = this.wingXML.GetNode("wing_stage", "stage_id==" + curStage);
			SXML node2 = node.GetNode("wing_level", "level_id==" + curLevel);
			List<SXML> nodeList = node2.GetNodeList("att", null);
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			string[] array = new string[2];
			for (int i = 0; i < nodeList.Count; i++)
			{
				int @int = nodeList[i].getInt("att_type");
				float @float = nodeList[i].getFloat("att_value");
				bool flag = @int == 5;
				if (flag)
				{
					array[1] = @float.ToString();
					dictionary.Add(@int, "");
				}
				else
				{
					bool flag2 = @int == 38;
					if (flag2)
					{
						array[0] = @float.ToString();
					}
					else
					{
						dictionary.Add(@int, @float.ToString());
					}
				}
			}
			bool flag3 = dictionary.ContainsKey(5);
			if (flag3)
			{
				dictionary[5] = array[0] + "-" + array[1];
			}
			int childCount = this.conAtt.childCount;
			List<int> list = dictionary.Keys.ToList<int>();
			int j;
			for (j = 0; j < list.Count; j++)
			{
				bool flag4 = j >= childCount;
				GameObject gameObject;
				if (flag4)
				{
					gameObject = UnityEngine.Object.Instantiate<GameObject>(this.tempPgaeAtt);
					gameObject.transform.SetParent(this.conAtt, false);
				}
				else
				{
					gameObject = this.conAtt.GetChild(j).gameObject;
				}
				gameObject.gameObject.SetActive(true);
				Text component = gameObject.transform.FindChild("text_name").GetComponent<Text>();
				Text component2 = gameObject.transform.FindChild("text_value").GetComponent<Text>();
				bool flag5 = list[j] == 5;
				if (flag5)
				{
					component.text = "攻击力";
				}
				else
				{
					component.text = Globle.getAttrNameById(list[j]);
				}
				component2.text = dictionary[list[j]];
			}
			int num = j;
			while (j < this.conAtt.childCount)
			{
				this.conAtt.GetChild(num).gameObject.SetActive(false);
				num++;
			}
		}

		private void ShowTitle(WingsData data)
		{
			this.textName.text = data.wingName;
			this.textLevel.text = "LV " + this.wingModel.Level;
			this.textStage.text = "(" + this.wingModel.Stage + "阶)";
		}

		private void OnStageSliderSetting(WingsData data)
		{
			this.OnStageUpRateChange(this.sliderStage.value);
		}

		private void OnSliderValueChange(float scale)
		{
			bool flag = this.pageIndex != 1;
			if (!flag)
			{
				this.OnStageUpRateChange(scale);
			}
		}

		private void OnStageUpRateChange(float scale)
		{
			float num = this.curWing.stageCrystalMin;
			float num2 = this.curWing.stageCrystalMax;
			float num3 = this.curWing.stageRateMin;
			float num4 = this.curWing.stageRateMax;
			float num5 = this.curWing.stageCrystalStep;
			debug.Log(scale.ToString());
			int num6 = (int)Math.Round((double)((scale * (num4 - num3) + num3) / 100f));
			bool flag = num6 < 10;
			if (flag)
			{
				scale = 0f;
				num6 = 0;
				this.sliderStage.value = 0f;
			}
			else
			{
				bool flag2 = num6 >= 10 && num6 < 20;
				if (flag2)
				{
					scale = 0.1f;
					num6 = 10;
					this.sliderStage.value = 0.1f;
				}
				else
				{
					bool flag3 = num6 >= 20 && num6 < 40;
					if (flag3)
					{
						scale = 0.2f;
						num6 = 20;
						this.sliderStage.value = 0.2f;
					}
					else
					{
						bool flag4 = num6 >= 40 && num6 < 60;
						if (flag4)
						{
							scale = 0.4f;
							num6 = 40;
							this.sliderStage.value = 0.4f;
						}
						else
						{
							bool flag5 = num6 >= 60 && num6 < 80;
							if (flag5)
							{
								scale = 0.6f;
								num6 = 60;
								this.sliderStage.value = 0.6f;
							}
							else
							{
								bool flag6 = num6 >= 80 && num6 < 100;
								if (flag6)
								{
									scale = 0.8f;
									num6 = 80;
									this.sliderStage.value = 0.8f;
								}
								else
								{
									scale = 1f;
									num6 = 100;
									this.sliderStage.value = 1f;
								}
							}
						}
					}
				}
			}
			this.costItem = (int)(scale * num * 10f);
			this.textStageCostItemSum.text = this.costItem.ToString();
			this.neednum_stage = this.costItem;
			this.textStageRate.text = num6 + "%";
		}

		private void OnStageUpClick(GameObject go)
		{
			Debug.Log("OnStageUpClick");
			bool flag = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid((uint)this.needobjid_stage) < this.neednum_stage;
			if (flag)
			{
				this.addtoget(ModelBase<a3_BagModel>.getInstance().getItemDataById((uint)this.needobjid_stage));
			}
			BaseProxy<A3_WingProxy>.getInstance().SendUpgradeStage(this.costItem);
		}

		private void OnShowStageChange(GameEvent e)
		{
		}

		private void CreatWingAvatar()
		{
			this.DisposeAvatar();
			bool flag = this.wingAvatar == null;
			if (flag)
			{
				bool flag2 = SelfRole._inst is P2Warrior;
				GameObject gameObject;
				if (flag2)
				{
					string path = "profession/avatar_ui/warrior_avatar";
					gameObject = Resources.Load<GameObject>(path);
					bool flag3 = gameObject != null;
					if (flag3)
					{
						this.wingAvatar = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-77.38f, -0.602f, 14.934f), new Quaternion(0f, 90f, 0f, 0f)) as GameObject);
					}
				}
				else
				{
					bool flag4 = SelfRole._inst is P3Mage;
					if (flag4)
					{
						string path = "profession/avatar_ui/mage_avatar";
						gameObject = Resources.Load<GameObject>(path);
						bool flag5 = gameObject != null;
						if (flag5)
						{
							this.wingAvatar = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-77.38f, -0.602f, 14.934f), new Quaternion(0f, 90f, 0f, 0f)) as GameObject);
						}
					}
					else
					{
						bool flag6 = SelfRole._inst is P5Assassin;
						if (!flag6)
						{
							return;
						}
						string path = "profession/avatar_ui/assa_avatar";
						gameObject = Resources.Load<GameObject>(path);
						bool flag7 = gameObject != null;
						if (flag7)
						{
							this.wingAvatar = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-77.38f, -0.602f, 14.934f), new Quaternion(0f, 90f, 0f, 0f)) as GameObject);
						}
					}
				}
				Transform transform = this.wingAvatar.transform.FindChild("model");
				Transform[] componentsInChildren = this.wingAvatar.GetComponentsInChildren<Transform>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					Transform transform2 = componentsInChildren[i];
					transform2.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
				}
				bool flag8 = SelfRole._inst is P3Mage;
				if (flag8)
				{
					Transform parent = transform.FindChild("R_Finger1");
					gameObject = Resources.Load<GameObject>("profession/avatar_ui/mage_r_finger_fire");
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.transform.SetParent(parent, false);
				}
				this.m_proAvatar = new ProfessionAvatar();
				this.m_proAvatar.Init(SelfRole._inst.m_strAvatarPath, "h_", EnumLayer.LM_ROLE_INVISIBLE, EnumMaterial.EMT_EQUIP_H, transform, SelfRole._inst.m_strEquipEffPath);
				bool flag9 = ModelBase<a3_EquipModel>.getInstance().active_eqp.Count >= 10;
				if (flag9)
				{
					this.m_proAvatar.set_equip_eff(ModelBase<a3_EquipModel>.getInstance().GetEqpIdbyType(3), true);
				}
				this.m_proAvatar.set_body(SelfRole._inst.get_bodyid(), SelfRole._inst.get_bodyfxid());
				this.m_proAvatar.set_weaponl(SelfRole._inst.get_weaponl_id(), SelfRole._inst.get_weaponl_fxid());
				this.m_proAvatar.set_weaponr(SelfRole._inst.get_weaponr_id(), SelfRole._inst.get_weaponr_fxid());
				this.m_proAvatar.set_wing(SelfRole._inst.get_wingid(), SelfRole._inst.get_windfxid());
				this.m_proAvatar.set_equip_color(SelfRole._inst.get_equip_colorid());
				gameObject = Resources.Load<GameObject>("profession/avatar_ui/show_scene");
				this.scene_Obj = (UnityEngine.Object.Instantiate(gameObject, new Vector3(-77.38f, -0.49f, 15.1f), new Quaternion(0f, 180f, 0f, 0f)) as GameObject);
				Transform[] componentsInChildren2 = this.scene_Obj.GetComponentsInChildren<Transform>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					Transform transform3 = componentsInChildren2[j];
					bool flag10 = transform3.gameObject.name == "scene_ta";
					if (flag10)
					{
						transform3.gameObject.layer = EnumLayer.LM_ROLE_INVISIBLE;
					}
					else
					{
						transform3.gameObject.layer = EnumLayer.LM_FX;
					}
				}
				gameObject = Resources.Load<GameObject>("profession/avatar_ui/scene_ui_camera");
				this.avatarCamera = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				bool flag11 = this.m_proAvatar != null;
				if (flag11)
				{
					this.m_proAvatar.FrameMove();
				}
			}
		}

		private void DisposeAvatar()
		{
			bool flag = this.wingAvatar != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.wingAvatar);
			}
			bool flag2 = this.avatarCamera != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.avatarCamera);
			}
			bool flag3 = this.scene_Obj != null;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.scene_Obj);
			}
			this.wingAvatar = null;
			this.avatarCamera = null;
			this.scene_Obj = null;
		}

		private void OnWingAvatarChange()
		{
			this.DisposeAvatar();
		}

		private void OnUnLockNweStage()
		{
			int stage = this.wingModel.Stage;
			this.conIcon.GetChild(stage - 1).transform.FindChild("image_lock").gameObject.SetActive(false);
		}

		private void CreatAllWingsIcon(int curStage)
		{
			Dictionary<int, WingsData> dicWingsData = this.wingModel.dicWingsData;
			RectTransform component = this.iconTemp.GetComponent<RectTransform>();
			this.wingIconSizeX = component.sizeDelta.x;
			this.wingIconSizeY = component.sizeDelta.y;
			foreach (WingsData current in dicWingsData.Values)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.iconTemp);
				Image component2 = gameObject.transform.FindChild("icon_bg/icon").GetComponent<Image>();
				component2.sprite = (Resources.Load(current.spriteFile, typeof(Sprite)) as Sprite);
				GameObject gameObject2 = gameObject.transform.FindChild("image_lock").gameObject;
				bool flag = !current.isUnlock(curStage);
				if (flag)
				{
					gameObject2.SetActive(true);
				}
				else
				{
					gameObject2.SetActive(false);
				}
				gameObject.transform.SetParent(this.conIcon, false);
				gameObject.name = current.stage.ToString();
				Text component3 = gameObject.transform.FindChild("lvl").GetComponent<Text>();
				component3.text = current.stage + "阶";
				gameObject.SetActive(true);
				BaseButton baseButton = new BaseButton(gameObject.transform, 1, 1);
				baseButton.onClick = new Action<GameObject>(this.OnSelectWingByIcon);
				this.dicIcon[current.stage] = gameObject;
			}
			RectTransform component4 = this.conIcon.GetComponent<RectTransform>();
			int count = dicWingsData.Count;
			Vector2 sizeDelta = new Vector2(this.wingIconSizeX * (float)count, this.wingIconSizeY);
			component4.sizeDelta = sizeDelta;
			this.boundaryLeft = 0f;
			this.boundaryRight = -(this.wingIconSizeX * (float)(count - 1));
		}

		private void DisposeIcon()
		{
			foreach (GameObject current in this.dicIcon.Values)
			{
				UnityEngine.Object.Destroy(current);
			}
			this.dicIcon.Clear();
		}

		private void OnSelectWingByIcon(GameObject go)
		{
			int stage = this.wingModel.Stage;
			int num = int.Parse(go.name);
			this.changeWing(num);
			bool flag = num > stage;
			if (flag)
			{
				this.refor_icon();
				go.transform.FindChild("icon_bg/yuxuan_on").gameObject.SetActive(true);
			}
			else
			{
				this.wingModel.ShowStage = num;
				this.OnSetIconBGImage(num);
				BaseProxy<A3_WingProxy>.getInstance().SendShowStage(num);
				base.transform.FindChild("btnWing/Text").GetComponent<Text>().text = "隐藏飞翼";
			}
		}

		private void changeWing(int curStage)
		{
			bool flag = this.wingAvatar != null;
			if (flag)
			{
				bool flag2 = this.wingAvatar.transform.FindChild("model/Plus_B").transform.childCount > 0;
				if (flag2)
				{
					UnityEngine.Object.Destroy(this.wingAvatar.transform.FindChild("model/Plus_B").transform.GetChild(0).gameObject);
				}
			}
			Transform cur_model = this.wingAvatar.transform.FindChild("model");
			this.m_proAvatar = new ProfessionAvatar();
			this.m_proAvatar.Init(SelfRole._inst.m_strAvatarPath, "h_", EnumLayer.LM_ROLE_INVISIBLE, EnumMaterial.EMT_EQUIP_H, cur_model, SelfRole._inst.m_strEquipEffPath);
			this.m_proAvatar.set_wing(curStage, curStage);
		}

		private void refor_icon()
		{
			foreach (int current in this.dicIcon.Keys)
			{
				Transform transform = this.dicIcon[current].transform.FindChild("icon_bg");
				transform.GetChild(2).gameObject.SetActive(false);
			}
		}

		public void OnSetIconBGImage(int showStage)
		{
			foreach (int current in this.dicIcon.Keys)
			{
				Transform transform = this.dicIcon[current].transform.FindChild("icon_bg");
				bool flag = current == showStage;
				if (flag)
				{
					bool flag2 = ModelBase<A3_WingModel>.getInstance().Stage >= showStage;
					if (flag2)
					{
						transform.GetChild(0).gameObject.SetActive(false);
						transform.GetChild(1).gameObject.SetActive(true);
						transform.GetChild(2).gameObject.SetActive(false);
					}
				}
				else
				{
					transform.GetChild(0).gameObject.SetActive(true);
					transform.GetChild(1).gameObject.SetActive(false);
					transform.GetChild(2).gameObject.SetActive(false);
				}
			}
		}

		private void OnEquWing(GameObject go)
		{
			A3_WingModel a3_WingModel = ModelBase<A3_WingModel>.getInstance();
			bool flag = a3_WingModel.ShowStage > 0;
			if (flag)
			{
				bool flag2 = this.wingAvatar != null;
				if (flag2)
				{
					bool flag3 = this.wingAvatar.transform.FindChild("model/Plus_B").transform.childCount > 0;
					if (flag3)
					{
						UnityEngine.Object.Destroy(this.wingAvatar.transform.FindChild("model/Plus_B").transform.GetChild(0).gameObject);
					}
				}
				BaseProxy<A3_WingProxy>.getInstance().SendShowStage(0);
				a3_WingModel.LastShowState = a3_WingModel.ShowStage;
				go.transform.FindChild("Text").GetComponent<Text>().text = "显示飞翼";
			}
			else
			{
				this.changeWing(a3_WingModel.LastShowState);
				BaseProxy<A3_WingProxy>.getInstance().SendShowStage(a3_WingModel.LastShowState);
				go.transform.FindChild("Text").GetComponent<Text>().text = "隐藏飞翼";
			}
		}

		private void OnTurnLeftClick(GameObject go)
		{
			this.TurnRight();
		}

		private void OnTurnRightClick(GameObject go)
		{
			this.TurnLeft();
		}

		private void TurnLeft()
		{
			RectTransform component = this.conIcon.GetComponent<RectTransform>();
			Vector2 anchoredPosition = component.anchoredPosition;
			float num = anchoredPosition.x - this.wingIconSizeX * 3f;
			bool flag = (float)((int)num) < this.boundaryRight || !this.canRun;
			if (!flag)
			{
				Vector3 localPosition = this.conIcon.localPosition;
				float endValue = localPosition.x - this.wingIconSizeX;
				Tween t = this.conIcon.DOLocalMoveX(endValue, this.speed, false);
				t.SetEase(Ease.Linear);
				this.canRun = false;
				t.OnComplete(delegate
				{
					this.OnTweeComplete();
					this.iconIndex++;
				});
			}
		}

		private void TurnRight()
		{
			RectTransform component = this.conIcon.GetComponent<RectTransform>();
			Vector2 anchoredPosition = component.anchoredPosition;
			float num = anchoredPosition.x + this.wingIconSizeX;
			bool flag = (float)((int)num) > this.boundaryLeft || !this.canRun;
			if (!flag)
			{
				Vector3 localPosition = this.conIcon.localPosition;
				float endValue = localPosition.x + this.wingIconSizeX;
				Tween t = this.conIcon.DOLocalMoveX(endValue, this.speed, false);
				t.SetEase(Ease.Linear);
				this.canRun = false;
				t.OnComplete(delegate
				{
					this.OnTweeComplete();
					this.iconIndex--;
				});
			}
		}

		private void OnTweeComplete()
		{
			this.canRun = true;
		}

		private void onDragIcon(GameObject go, Vector2 delta)
		{
			float y = delta.y;
			bool flag = y > 0f;
			if (flag)
			{
				this.TurnLeft();
			}
			else
			{
				bool flag2 = y < 0f;
				if (flag2)
				{
					this.TurnRight();
				}
			}
		}

		private void OnDrag(GameObject go, Vector2 delta)
		{
			bool flag = this.wingAvatar != null;
			if (flag)
			{
				this.wingAvatar.transform.Rotate(Vector3.up, -delta.x);
			}
		}

		private void OnOpenHelp(GameObject go)
		{
			debug.Log("OnRulerWindowClick");
			this.conHelpPanel.gameObject.SetActive(true);
		}

		private void OnCloseHelp(GameObject go)
		{
			this.conHelpPanel.gameObject.SetActive(false);
			this.CreatWingAvatar();
		}

		private void onClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_WIBG_SKIN);
		}
	}
}
