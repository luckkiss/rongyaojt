using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_pet_skin : Window
	{
		private A3_PetModel petmodel;

		private uint feedid;

		private uint levelid;

		private uint stageid;

		private uint stagestep;

		private SXML currentStage;

		private SXML currentLevel;

		private RenderTexture petTexture;

		private GameObject avatarCamera;

		private GameObject pet;

		public Toggle autobuy;

		public Toggle autofeed;

		private GameObject prefab;

		public static a3_pet_skin instan;

		public override void init()
		{
			a3_pet_skin.instan = this;
			this.petmodel = ModelBase<A3_PetModel>.getInstance();
			this.feedid = this.petmodel.GetFeedItemTpid();
			this.levelid = this.petmodel.GetLevelItemTpid();
			this.stageid = this.petmodel.GetStageItemTpid();
			this.currentLevel = this.petmodel.CurrentLevelConf();
			this.currentStage = this.petmodel.CurrentStageConf();
			this.stagestep = this.currentStage.getUint("crystal_step");
			BaseButton baseButton = new BaseButton(base.getTransformByPath("exp_con/upgrade"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.OnUpgrade);
			BaseButton baseButton2 = new BaseButton(base.getTransformByPath("exp_con/onekey"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.OnOnekey);
			BaseButton baseButton3 = new BaseButton(base.getTransformByPath("lampoil"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.OnFeed);
			BaseButton baseButton4 = new BaseButton(base.getTransformByPath("stage_con/improve"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.OnStage);
			BaseButton baseButton5 = new BaseButton(base.getTransformByPath("title/help"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.OnHelp);
			BaseButton baseButton6 = new BaseButton(base.getTransformByPath("close"), 1, 1);
			baseButton6.onClick = new Action<GameObject>(this.onclose);
			this.autofeed = base.getComponentByPath<Toggle>("light_hint/toggle");
			this.autofeed.onValueChanged.AddListener(new UnityAction<bool>(this.OnAutoFeedToggleChange));
			this.autobuy = base.getComponentByPath<Toggle>("light_hint/toggle2");
			this.autobuy.onValueChanged.AddListener(new UnityAction<bool>(this.OnAutoBuyToggleChange));
			this.prefab = base.getGameObjectByPath("att/a3_pet_att");
		}

		public override void onShowed()
		{
			BaseProxy<A3_PetProxy>.getInstance().GetPets();
			A3_PetModel expr_12 = this.petmodel;
			expr_12.OnExpChange = (Action)Delegate.Combine(expr_12.OnExpChange, new Action(this.OnExpChange));
			A3_PetModel expr_39 = this.petmodel;
			expr_39.OnAutoFeedChange = (Action)Delegate.Remove(expr_39.OnAutoFeedChange, new Action(this.OnAutoBuyChange));
			A3_PetModel expr_60 = this.petmodel;
			expr_60.OnLevelChange = (Action)Delegate.Combine(expr_60.OnLevelChange, new Action(this.OnLevelChange));
			A3_PetModel expr_87 = this.petmodel;
			expr_87.OnStageChange = (Action)Delegate.Combine(expr_87.OnStageChange, new Action(this.OnStageChange));
			this.OnExpChange();
			this.OnStarvationChange();
			this.OnAutoFeedChange();
			this.OnAutoBuyChange();
			this.OnLevelChange();
			this.OnStageChange();
			this.OnPetItemChange(null);
			BaseProxy<BagProxy>.getInstance().addEventListener(BagProxy.EVENT_ITEM_CHANGE, new Action<GameEvent>(this.OnPetItemChange));
		}

		public override void onClosed()
		{
			A3_PetModel expr_07 = this.petmodel;
			expr_07.OnExpChange = (Action)Delegate.Remove(expr_07.OnExpChange, new Action(this.OnExpChange));
			A3_PetModel expr_2E = this.petmodel;
			expr_2E.OnStarvationChange = (Action)Delegate.Remove(expr_2E.OnStarvationChange, new Action(this.OnStarvationChange));
			A3_PetModel expr_55 = this.petmodel;
			expr_55.OnLevelChange = (Action)Delegate.Remove(expr_55.OnLevelChange, new Action(this.OnLevelChange));
			A3_PetModel expr_7C = this.petmodel;
			expr_7C.OnStageChange = (Action)Delegate.Remove(expr_7C.OnStageChange, new Action(this.OnStageChange));
			BaseProxy<BagProxy>.getInstance().removeEventListener(BagProxy.EVENT_ITEM_CHANGE, new Action<GameEvent>(this.OnPetItemChange));
			bool flag = this.petTexture != null;
			if (flag)
			{
				UnityEngine.Object.Destroy(this.petTexture);
				this.petTexture = null;
			}
			bool flag2 = this.avatarCamera != null;
			if (flag2)
			{
				UnityEngine.Object.Destroy(this.avatarCamera);
				this.avatarCamera = null;
			}
			bool flag3 = this.pet != null;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.pet);
				this.pet = null;
			}
		}

		private void OnExpChange()
		{
			int maxExp = this.petmodel.GetMaxExp();
			int exp = this.petmodel.Exp;
			Slider componentByPath = base.getComponentByPath<Slider>("exp_con/expbar/slider");
			componentByPath.value = (float)exp / (float)maxExp;
			Text componentByPath2 = base.getComponentByPath<Text>("exp_con/expbar/text");
			componentByPath2.text = exp + "/" + maxExp;
		}

		public void OnStarvationChange()
		{
			int starvation = this.petmodel.Starvation;
			Image componentByPath = base.getComponentByPath<Image>("light1/image");
			componentByPath.fillAmount = (float)starvation / 100f;
		}

		private void OnAutoFeedChange()
		{
			Toggle componentByPath = base.getComponentByPath<Toggle>("light_hint/toggle");
			componentByPath.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnAutoFeedToggleChange));
			componentByPath.isOn = this.petmodel.Auto_feed;
			componentByPath.onValueChanged.AddListener(new UnityAction<bool>(this.OnAutoFeedToggleChange));
		}

		private void OnAutoBuyChange()
		{
			Toggle componentByPath = base.getComponentByPath<Toggle>("light_hint/toggle2");
			componentByPath.onValueChanged.RemoveListener(new UnityAction<bool>(this.OnAutoBuyToggleChange));
			componentByPath.isOn = this.petmodel.Auto_buy;
			componentByPath.onValueChanged.AddListener(new UnityAction<bool>(this.OnAutoBuyToggleChange));
		}

		private void OnLevelChange()
		{
			this.currentLevel = this.petmodel.CurrentLevelConf();
			Text componentByPath = base.getComponentByPath<Text>("lvl");
			componentByPath.text = "Lv " + this.petmodel.Level;
			this.RefreshLevelUpCost();
			this.OnAttChange();
			this.OnExpChange();
			this.CheckShowLevelOrStage();
		}

		private void OnStageChange()
		{
			this.currentStage = this.petmodel.CurrentStageConf();
			this.stagestep = this.currentStage.getUint("crystal_step");
			Text componentByPath = base.getComponentByPath<Text>("stage");
			componentByPath.text = "(" + this.petmodel.Stage.ToString() + "转)";
			Text componentByPath2 = base.getComponentByPath<Text>("name");
			componentByPath2.text = this.petmodel.CurrentStageConf().getString("name");
			Text componentByPath3 = base.getComponentByPath<Text>("stage_con/gold/text");
			componentByPath3.text = this.currentStage.getString("gold_cost");
			Text componentByPath4 = base.getComponentByPath<Text>("exp_con/upgrade/text");
			uint @uint = this.currentStage.getUint("crystal");
			int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.stageid);
			string arg_ED_0 = ((ulong)@uint <= (ulong)((long)itemNumByTpid)) ? "<color=#00ffff>" : "<color=#ff0000>";
			componentByPath4.text = @uint.ToString();
			Text componentByPath5 = base.getComponentByPath<Text>("stage_con/rate");
			uint num = this.currentStage.getUint("rate") / 100u;
			componentByPath5.text = ContMgr.getCont("pet_succ", new List<string>
			{
				num.ToString()
			});
			this.CheckShowLevelOrStage();
			this.RefreshAvatar();
		}

		private void OnAttChange()
		{
			Transform componentByPath = base.getComponentByPath<Transform>("att/grid");
			for (int i = 0; i < componentByPath.childCount; i++)
			{
				UnityEngine.Object.Destroy(componentByPath.GetChild(i).gameObject);
			}
			SXML sXML = this.petmodel.NextLevelConf();
			Dictionary<string, SXMLAttr>.Enumerator enumerator = this.currentLevel.m_dAtttr.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Dictionary<string, int> arg_70_0 = Globle.AttNameIDDic;
				KeyValuePair<string, SXMLAttr> current = enumerator.Current;
				bool flag = arg_70_0.ContainsKey(current.Key);
				if (flag)
				{
					current = enumerator.Current;
					bool flag2 = current.Key == "mp_suck";
					if (!flag2)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.prefab);
						bool flag3 = gameObject == null;
						if (flag3)
						{
							break;
						}
						gameObject.SetActive(true);
						Transform transform = gameObject.transform;
						Dictionary<string, int> arg_EC_0 = Globle.AttNameIDDic;
						current = enumerator.Current;
						int num = arg_EC_0[current.Key];
						current = enumerator.Current;
						string str = current.Value.str;
						bool flag4 = num == 30 || num == 33;
						if (flag4)
						{
							current = enumerator.Current;
							str = (float)int.Parse(current.Value.str) / 10f + "%";
						}
						transform.FindChild("name").GetComponent<Text>().text = Globle.getAttrNameById(num);
						transform.FindChild("cur").GetComponent<Text>().text = "+" + str;
						Text component = transform.FindChild("next").GetComponent<Text>();
						bool flag5 = sXML != null;
						if (flag5)
						{
							bool flag6 = num == 30 || num == 33;
							if (flag6)
							{
								Text arg_203_0 = component;
								object arg_1FE_0 = "+";
								SXML arg_1E3_0 = sXML;
								current = enumerator.Current;
								arg_203_0.text = arg_1FE_0 + (float)int.Parse(arg_1E3_0.getString(current.Key)) / 10f + "%";
							}
							else
							{
								Text arg_22F_0 = component;
								string arg_22A_0 = "+";
								SXML arg_225_0 = sXML;
								current = enumerator.Current;
								arg_22F_0.text = arg_22A_0 + arg_225_0.getString(current.Key);
							}
						}
						else
						{
							component.text = string.Empty;
						}
						transform.SetParent(componentByPath, false);
					}
				}
			}
		}

		private void OnUpgrade(GameObject go)
		{
			Toggle componentByPath = base.getComponentByPath<Toggle>("exp_con/toggle");
			BaseProxy<A3_PetProxy>.getInstance().Bless(componentByPath.isOn);
		}

		private void OnOnekey(GameObject go)
		{
			Toggle componentByPath = base.getComponentByPath<Toggle>("exp_con/toggle");
			BaseProxy<A3_PetProxy>.getInstance().OneKeyBless(componentByPath.isOn);
		}

		private void OnFeed(GameObject go)
		{
			int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.feedid);
			bool flag = itemNumByTpid > 0;
			if (flag)
			{
				bool flag2 = itemNumByTpid == 1;
				if (flag2)
				{
					bool flag3 = !ModelBase<A3_PetModel>.getInstance().Auto_buy;
					if (flag3)
					{
					}
				}
				BaseProxy<A3_PetProxy>.getInstance().Feed();
			}
			else
			{
				a3_store.itm_tpid = this.feedid;
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_STORE, null, false);
			}
		}

		private void OnStage(GameObject go)
		{
			uint @uint = this.currentStage.getUint("crystal");
			BaseProxy<A3_PetProxy>.getInstance().Stage((int)@uint);
		}

		private void OnHelp(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_PET_DESC, null, false);
		}

		private void OnAutoFeedToggleChange(bool isOn)
		{
			if (isOn)
			{
				BaseProxy<A3_PetProxy>.getInstance().SetAutoFeed(1u);
			}
			else
			{
				BaseProxy<A3_PetProxy>.getInstance().SetAutoFeed(0u);
			}
		}

		private void OnAutoBuyToggleChange(bool isOn)
		{
			if (isOn)
			{
				BaseProxy<A3_PetProxy>.getInstance().SetAutoBuy(1u);
			}
			else
			{
				BaseProxy<A3_PetProxy>.getInstance().SetAutoBuy(0u);
			}
		}

		private void OnPetItemChange(GameEvent e)
		{
			int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.feedid);
			base.getComponentByPath<Text>("lampoil/num").text = itemNumByTpid.ToString();
			this.RefreshLevelUpCost();
			Text componentByPath = base.getComponentByPath<Text>("stage_con/stageitm/text");
			uint @uint = this.currentStage.getUint("crystal");
			int itemNumByTpid2 = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.stageid);
			string text = ((ulong)@uint <= (ulong)((long)itemNumByTpid2)) ? "<color=#00ffff>" : "<color=#ff0000>";
			componentByPath.text = string.Concat(new string[]
			{
				@uint.ToString(),
				text,
				"/",
				itemNumByTpid2.ToString(),
				"</color>"
			});
		}

		private void RefreshLevelUpCost()
		{
			Text componentByPath = base.getComponentByPath<Text>("exp_con/gold/text");
			componentByPath.text = this.currentLevel.getString("cost_gold");
			Text componentByPath2 = base.getComponentByPath<Text>("exp_con/upgrade/text");
			int itemNumByTpid = ModelBase<a3_BagModel>.getInstance().getItemNumByTpid(this.levelid);
			uint @uint = this.currentLevel.getUint("cost_item_num");
			string arg_64_0 = ((ulong)@uint <= (ulong)((long)itemNumByTpid)) ? "<color=#00ffff>" : "<color=#ff0000>";
			componentByPath2.text = @uint.ToString();
		}

		private void RefreshAvatar()
		{
			RawImage componentByPath = base.getComponentByPath<RawImage>("avatar");
			bool flag = this.petTexture == null;
			if (flag)
			{
				this.petTexture = new RenderTexture(380, 420, 16);
			}
			bool flag2 = this.avatarCamera == null;
			if (flag2)
			{
				GameObject original = Resources.Load<GameObject>("profession/avatar_ui/avatar_ui_camera");
				this.avatarCamera = UnityEngine.Object.Instantiate<GameObject>(original);
			}
			Camera componentInChildren = this.avatarCamera.GetComponentInChildren<Camera>();
			componentInChildren.targetTexture = this.petTexture;
			componentByPath.texture = this.petTexture;
			string petAvatar = this.petmodel.GetPetAvatar((int)this.petmodel.Tpid, 0);
			GameObject original2 = Resources.Load<GameObject>("profession/" + petAvatar);
			bool flag3 = this.pet != null;
			if (flag3)
			{
				UnityEngine.Object.Destroy(this.pet);
			}
			this.pet = (UnityEngine.Object.Instantiate(original2, new Vector3(-128f, 0f, 0f), Quaternion.identity) as GameObject);
			this.pet.transform.localScale = Vector3.one;
			this.pet.transform.Rotate(new Vector3(20f, 40f, -30f));
			Transform[] componentsInChildren = this.pet.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform = componentsInChildren[i];
				transform.gameObject.layer = EnumLayer.LM_FX;
			}
		}

		private void onclose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_PET_SKIN);
		}

		private void CheckShowLevelOrStage()
		{
			uint @uint = this.currentStage.getUint("to_next_stage_level");
			SXML sXML = this.petmodel.NextLevelConf();
			bool flag = sXML != null && (long)this.petmodel.Level >= (long)((ulong)@uint);
			GameObject gameObjectByPath = base.getGameObjectByPath("exp_con");
			GameObject gameObjectByPath2 = base.getGameObjectByPath("stage_con");
			gameObjectByPath.SetActive(!flag);
			gameObjectByPath2.SetActive(flag);
		}
	}
}
