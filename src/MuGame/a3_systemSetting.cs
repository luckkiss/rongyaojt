using GameFramework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_systemSetting : Window
	{
		private class SystemPanel : BaseSystemSetting
		{
			public enum SystemItemConfigLvL
			{
				high = 1,
				middle,
				low,
				on = 1,
				off
			}

			public static a3_systemSetting.SystemPanel mInstance;

			private Slider scrbMusic;

			private Slider scrbMusicEffect;

			private Transform tfVideoQualit;

			private Transform tfSkillEffectToggles;

			private Transform tfShadowVolumeToggles;

			private Transform tfRoleShadowToggles;

			private Transform tfSceneDetailToggles;

			public int musicValue;

			public int musicEffect;

			public float videoQualityValue;

			public a3_systemSetting.SystemPanel.SystemItemConfigLvL skillEffectType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.high;

			public a3_systemSetting.SystemPanel.SystemItemConfigLvL shadowVolumeType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.middle;

			public a3_systemSetting.SystemPanel.SystemItemConfigLvL roleShadowType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.middle;

			public a3_systemSetting.SystemPanel.SystemItemConfigLvL sceneDetailType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.high;

			private Text txtMusicValue;

			private Text txtMusicEffectValue;

			private List<Toggle> videoQualityValueToggles;

			public SystemPanel(Transform trans) : base(trans)
			{
				this.videoQualityValueToggles = new List<Toggle>();
				a3_systemSetting.SystemPanel.mInstance = this;
				this.scrbMusic = base.getGameObjectByPath("music/Slider").GetComponent<Slider>();
				this.txtMusicValue = this.scrbMusic.transform.FindChild("txtValue").GetComponent<Text>();
				this.scrbMusicEffect = base.getGameObjectByPath("soundEffect/Slider").GetComponent<Slider>();
				this.txtMusicEffectValue = this.scrbMusicEffect.transform.FindChild("txtValue").GetComponent<Text>();
				this.tfVideoQualit = base.getGameObjectByPath("videoQuality/toggles").transform;
				this.tfSkillEffectToggles = base.getGameObjectByPath("skillEffect/toggles").transform;
				this.tfShadowVolumeToggles = base.getGameObjectByPath("shadowVolume/toggles").transform;
				this.tfRoleShadowToggles = base.getGameObjectByPath("roleShadow/toggles").transform;
				this.tfSceneDetailToggles = base.getGameObjectByPath("sceneDetail/toggles").transform;
				this.scrbMusic.onValueChanged.AddListener(new UnityAction<float>(this.onScrbMusicValueChange));
				this.scrbMusicEffect.onValueChanged.AddListener(new UnityAction<float>(this.onScrbMusicEffectValueChange));
				for (int i = 0; i < this.tfVideoQualit.childCount; i++)
				{
					Toggle component = this.tfVideoQualit.GetChild(i).GetComponent<Toggle>();
					this.videoQualityValueToggles.Add(component);
				}
			}

			public override void onShowed()
			{
				this.initData();
			}

			private void initData()
			{
				this.scrbMusic.value = MediaClient.getInstance().getMusicVolume();
				this.txtMusicValue.text = ((int)(this.scrbMusic.value * 100f)).ToString();
				this.scrbMusicEffect.value = MediaClient.getInstance().getSoundVolume();
				this.txtMusicEffectValue.text = ((int)(this.scrbMusicEffect.value * 100f)).ToString();
				int num = (int)(SceneCamera.m_fScreenGQ_Level * 10f);
				int nLightGQ_Level = SceneCamera.m_nLightGQ_Level;
				int nShadowGQ_Level = SceneCamera.m_nShadowGQ_Level;
				int nSceneGQ_Level = SceneCamera.m_nSceneGQ_Level;
				int nSkillEff_Level = SceneCamera.m_nSkillEff_Level;
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 != 5)
					{
						if (num2 == 10)
						{
							this.videoQualityValueToggles[2].isOn = true;
						}
					}
					else
					{
						this.videoQualityValueToggles[1].isOn = true;
					}
				}
				else
				{
					this.videoQualityValueToggles[0].isOn = true;
				}
				for (int i = 1; i <= this.tfShadowVolumeToggles.childCount; i++)
				{
					bool flag = nLightGQ_Level == i;
					if (flag)
					{
						this.tfShadowVolumeToggles.GetChild(i - 1).GetComponent<Toggle>().isOn = true;
					}
					else
					{
						this.tfShadowVolumeToggles.GetChild(i - 1).GetComponent<Toggle>().isOn = false;
					}
				}
				for (int j = 1; j <= this.tfRoleShadowToggles.childCount; j++)
				{
					bool flag2 = nShadowGQ_Level == j;
					if (flag2)
					{
						this.tfRoleShadowToggles.GetChild(j - 1).GetComponent<Toggle>().isOn = true;
					}
					else
					{
						this.tfRoleShadowToggles.GetChild(j - 1).GetComponent<Toggle>().isOn = false;
					}
				}
				for (int k = 1; k <= this.tfSceneDetailToggles.childCount; k++)
				{
					bool flag3 = nSceneGQ_Level == k;
					if (flag3)
					{
						this.tfSceneDetailToggles.GetChild(k - 1).GetComponent<Toggle>().isOn = true;
					}
					else
					{
						this.tfSceneDetailToggles.GetChild(k - 1).GetComponent<Toggle>().isOn = false;
					}
				}
				for (int l = 1; l <= this.tfSkillEffectToggles.childCount; l++)
				{
					bool flag4 = nSkillEff_Level == l;
					if (flag4)
					{
						this.tfSkillEffectToggles.GetChild(l - 1).GetComponent<Toggle>().isOn = true;
					}
					else
					{
						this.tfSkillEffectToggles.GetChild(l - 1).GetComponent<Toggle>().isOn = false;
					}
				}
			}

			private void onScrbMusicValueChange(float f)
			{
				MediaClient.getInstance().setMusicVolume(f);
				int num = (int)(float.Parse(f.ToString("F2")) * 100f);
				this.txtMusicValue.text = num.ToString();
				this.musicValue = num;
			}

			private void onScrbMusicEffectValueChange(float f)
			{
				SceneCamera.Set_Sound_Effect(f);
				MediaClient.getInstance().setSoundVolume(f);
				int num = (int)(float.Parse(f.ToString("F2")) * 100f);
				this.txtMusicEffectValue.text = num.ToString();
				this.musicEffect = num;
			}

			public a3_systemSetting.SystemPanel GetValue()
			{
				this.getVideoQualityValue();
				this.getSkillEffectValue();
				this.getShadowVolumeValue();
				this.getRoleShadowValue();
				this.getSceneDetailValue();
				return this;
			}

			private void getVideoQualityValue()
			{
				for (int i = 0; i < this.videoQualityValueToggles.Count; i++)
				{
					bool isOn = this.videoQualityValueToggles[i].isOn;
					if (isOn)
					{
						switch (i)
						{
						case 0:
							this.videoQualityValue = 0f;
							break;
						case 1:
							this.videoQualityValue = 0.5f;
							break;
						case 2:
							this.videoQualityValue = 1f;
							break;
						}
						break;
					}
				}
			}

			private void getSkillEffectValue()
			{
				for (int i = 0; i < this.tfSkillEffectToggles.transform.childCount; i++)
				{
					Toggle component = this.tfSkillEffectToggles.GetChild(i).GetComponent<Toggle>();
					bool isOn = component.isOn;
					if (isOn)
					{
						switch (i)
						{
						case 0:
							this.skillEffectType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.high;
							break;
						case 1:
							this.skillEffectType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.middle;
							break;
						case 2:
							this.skillEffectType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.low;
							break;
						}
					}
				}
			}

			private void getShadowVolumeValue()
			{
				for (int i = 0; i < this.tfShadowVolumeToggles.transform.childCount; i++)
				{
					Toggle component = this.tfShadowVolumeToggles.GetChild(i).GetComponent<Toggle>();
					bool isOn = component.isOn;
					if (isOn)
					{
						int num = i;
						if (num != 0)
						{
							if (num == 1)
							{
								this.shadowVolumeType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.middle;
							}
						}
						else
						{
							this.shadowVolumeType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.high;
						}
					}
				}
			}

			private void getRoleShadowValue()
			{
				for (int i = 0; i < this.tfRoleShadowToggles.transform.childCount; i++)
				{
					Toggle component = this.tfRoleShadowToggles.GetChild(i).GetComponent<Toggle>();
					bool isOn = component.isOn;
					if (isOn)
					{
						int num = i;
						if (num != 0)
						{
							if (num == 1)
							{
								this.roleShadowType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.middle;
							}
						}
						else
						{
							this.roleShadowType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.high;
						}
					}
				}
			}

			private void getSceneDetailValue()
			{
				for (int i = 0; i < this.tfSceneDetailToggles.transform.childCount; i++)
				{
					Toggle component = this.tfSceneDetailToggles.GetChild(i).GetComponent<Toggle>();
					bool isOn = component.isOn;
					if (isOn)
					{
						switch (i)
						{
						case 0:
							this.sceneDetailType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.high;
							break;
						case 1:
							this.sceneDetailType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.middle;
							break;
						case 2:
							this.sceneDetailType = a3_systemSetting.SystemPanel.SystemItemConfigLvL.low;
							break;
						}
					}
				}
			}
		}

		private class GamePanel : BaseSystemSetting
		{
			public static a3_systemSetting.GamePanel mInstance;

			private Transform root;

			public bool refuseTeamInvite;

			public bool ignorePrivateInfo;

			public bool ignorePaladinInvite;

			public bool ignoreAddFirendHint;

			public bool ignoreOtherEffect;

			public bool ignoreOther;

			public bool ignoreOtherPet;

			public GamePanel(Transform trans) : base(trans)
			{
				this.root = trans;
				a3_systemSetting.GamePanel.mInstance = this;
			}

			public a3_systemSetting.GamePanel GetValue()
			{
				for (int i = 0; i < this.root.childCount; i++)
				{
					switch (i)
					{
					case 0:
						this.refuseTeamInvite = this.root.GetChild(i).GetComponent<Toggle>().isOn;
						break;
					case 1:
						this.ignorePrivateInfo = this.root.GetChild(i).GetComponent<Toggle>().isOn;
						break;
					case 2:
						this.ignorePaladinInvite = this.root.GetChild(i).GetComponent<Toggle>().isOn;
						break;
					case 3:
						this.ignoreAddFirendHint = this.root.GetChild(i).GetComponent<Toggle>().isOn;
						break;
					case 4:
						this.ignoreOtherEffect = this.root.GetChild(i).GetComponent<Toggle>().isOn;
						break;
					case 5:
						this.ignoreOther = this.root.GetChild(i).GetComponent<Toggle>().isOn;
						break;
					case 6:
						this.ignoreOtherPet = this.root.GetChild(i).GetComponent<Toggle>().isOn;
						break;
					}
				}
				return this;
			}

			public override void onShowed()
			{
				for (int i = 0; i < this.root.childCount; i++)
				{
					switch (i)
					{
					case 0:
						this.root.GetChild(i).GetComponent<Toggle>().isOn = GlobleSetting.REFUSE_TEAM_INVITE;
						break;
					case 1:
						this.root.GetChild(i).GetComponent<Toggle>().isOn = GlobleSetting.IGNORE_PRIVATE_INFO;
						break;
					case 2:
						this.root.GetChild(i).GetComponent<Toggle>().isOn = GlobleSetting.IGNORE_KNIGHTAGE_INVITE;
						break;
					case 3:
						this.root.GetChild(i).GetComponent<Toggle>().isOn = GlobleSetting.IGNORE_FRIEND_ADD_REMINDER;
						break;
					case 4:
						this.root.GetChild(i).GetComponent<Toggle>().isOn = GlobleSetting.IGNORE_OTHER_EFFECT;
						break;
					case 5:
						this.root.GetChild(i).GetComponent<Toggle>().isOn = GlobleSetting.IGNORE_OTHER_PLAYER;
						break;
					case 6:
						this.root.GetChild(i).GetComponent<Toggle>().isOn = GlobleSetting.IGNORE_OTHER_PET;
						break;
					}
				}
			}
		}

		private class GifBagPane : BaseSystemSetting
		{
			private InputField iptGift;

			public GifBagPane(Transform trans) : base(trans)
			{
				this.iptGift = base.getGameObjectByPath("InputField").GetComponent<InputField>();
				new BaseButton(base.getGameObjectByPath("but_convert").transform, 1, 1).onClick = delegate(GameObject go)
				{
					HttpAppMgr.instance.sendInputGiftCode(this.iptGift.text);
					this.iptGift.text = "";
				};
			}
		}

		private TabControl tab;

		private Transform con;

		private BaseSystemSetting m_current;

		private BaseSystemSetting m_system;

		private BaseSystemSetting m_game;

		private BaseSystemSetting m_giftBag;

		private Transform tfSystemPanel;

		private Transform tfGamePanel;

		private Transform tfGiftBagPanel;

		public override void init()
		{
			this.con = base.transform.FindChild("main/panels");
			this.tfSystemPanel = base.transform.FindChild("main/panels/systemPanel");
			this.tfGamePanel = base.transform.FindChild("main/panels/gamePanel");
			this.tfGiftBagPanel = base.transform.FindChild("main/panels/giftBagPanel");
			this.tab = new TabControl();
			this.tab.onClickHanle = new Action<TabControl>(this.OnSwitch);
			this.tab.create(base.getGameObjectByPath("main/btns"), base.gameObject, 0, 0, false);
			new BaseButton(base.transform.FindChild("title/btnClose"), 1, 1).onClick = new Action<GameObject>(this.onBtnClose);
			new BaseButton(base.transform.FindChild("main/btnBackRoleList"), 1, 1).onClick = new Action<GameObject>(this.onBtnBackRoleList);
			new BaseButton(base.transform.FindChild("main/btnBackLogin"), 1, 1).onClick = new Action<GameObject>(this.onBtnBackLogin);
			new BaseButton(base.transform.FindChild("main/btnQuitGame"), 1, 1).onClick = new Action<GameObject>(this.onBtnQuitGame);
		}

		private void onBtnBackRoleList(GameObject go)
		{
			MsgBoxMgr.getInstance().showConfirm("确定要返回角色列表吗?", new UnityAction(this.backRoleListHandle), null, 0);
		}

		private void backRoleListHandle()
		{
		}

		private void onBtnBackLogin(GameObject go)
		{
			MsgBoxMgr.getInstance().showConfirm("确定要返回登录界面吗?", new UnityAction(this.backLoginHandle), null, 0);
		}

		private void backLoginHandle()
		{
		}

		private void onBtnQuitGame(GameObject go)
		{
			MsgBoxMgr.getInstance().showConfirm("确定要退出游戏吗?", new UnityAction(this.quitGameHandle), null, 0);
		}

		private void quitGameHandle()
		{
			AnyPlotformSDK.Call_Cmd("close", null, null, true);
		}

		private void onBtnClose(GameObject go)
		{
			a3_systemSetting.SystemPanel value = a3_systemSetting.SystemPanel.mInstance.GetValue();
			SceneCamera.SetGameScreenPow(value.videoQualityValue);
			SceneCamera.SetGameLight((int)value.shadowVolumeType);
			SceneCamera.SetGameShadow((int)value.roleShadowType);
			SceneCamera.SetGameScene((int)value.sceneDetailType);
			SceneCamera.SetSikillEff((int)value.skillEffectType);
			PlayeLocalInfo.saveInt(PlayeLocalInfo.SYS_MUSIC, value.musicValue);
			PlayeLocalInfo.saveInt(PlayeLocalInfo.SYS_SOUND, value.musicEffect);
			PlayeLocalInfo.saveString(PlayeLocalInfo.VIDEO_QUALITY, value.videoQualityValue.ToString());
			PlayeLocalInfo.saveInt(PlayeLocalInfo.DYNAM_LIGHT, (int)value.shadowVolumeType);
			PlayeLocalInfo.saveInt(PlayeLocalInfo.ROLE_SHADOW, (int)value.roleShadowType);
			PlayeLocalInfo.saveInt(PlayeLocalInfo.SCENE_DETAIL, (int)value.sceneDetailType);
			PlayeLocalInfo.saveInt(PlayeLocalInfo.SKILL_EFFECT, (int)value.skillEffectType);
			bool flag = a3_systemSetting.GamePanel.mInstance != null;
			if (flag)
			{
				a3_systemSetting.GamePanel value2 = a3_systemSetting.GamePanel.mInstance.GetValue();
				GlobleSetting.REFUSE_TEAM_INVITE = value2.refuseTeamInvite;
				GlobleSetting.IGNORE_PRIVATE_INFO = value2.ignorePrivateInfo;
				GlobleSetting.IGNORE_KNIGHTAGE_INVITE = value2.ignorePaladinInvite;
				GlobleSetting.IGNORE_FRIEND_ADD_REMINDER = value2.ignoreAddFirendHint;
				GlobleSetting.IGNORE_OTHER_EFFECT = value2.ignoreOtherEffect;
				GlobleSetting.IGNORE_OTHER_PLAYER = value2.ignoreOther;
				GlobleSetting.IGNORE_OTHER_PET = value2.ignoreOtherPet;
				PlayeLocalInfo.saveInt(PlayeLocalInfo.REFUSE_TEAM_INVITE, GlobleSetting.REFUSE_TEAM_INVITE ? 1 : 0);
				PlayeLocalInfo.saveInt(PlayeLocalInfo.IGNORE_PRIVATE_INFO, GlobleSetting.IGNORE_PRIVATE_INFO ? 1 : 0);
				PlayeLocalInfo.saveInt(PlayeLocalInfo.IGNORE_KNIGHTAGE_INVITE, GlobleSetting.IGNORE_KNIGHTAGE_INVITE ? 1 : 0);
				PlayeLocalInfo.saveInt(PlayeLocalInfo.IGNORE_FRIEND_ADD_REMINDER, GlobleSetting.IGNORE_FRIEND_ADD_REMINDER ? 1 : 0);
				PlayeLocalInfo.saveInt(PlayeLocalInfo.IGNORE_OTHER_EFFECT, GlobleSetting.IGNORE_OTHER_EFFECT ? 1 : 0);
				PlayeLocalInfo.saveInt(PlayeLocalInfo.IGNORE_OTHER_PLAYER, GlobleSetting.IGNORE_OTHER_PLAYER ? 1 : 0);
				PlayeLocalInfo.saveInt(PlayeLocalInfo.IGNORE_OTHER_PET, GlobleSetting.IGNORE_OTHER_PET ? 1 : 0);
			}
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_SYSTEM_SETTING);
		}

		public override void onShowed()
		{
			InterfaceMgr.getInstance().floatUI.localScale = Vector3.zero;
			bool flag = this.m_current != null;
			if (flag)
			{
				this.m_current.onShowed();
			}
			else
			{
				this.tab.setSelectedIndex(0, false);
				this.OnSwitch(this.tab);
			}
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().floatUI.localScale = Vector3.one;
			bool flag = this.m_current != null;
			if (flag)
			{
				this.m_current.onClose();
			}
		}

		private void OnSwitch(TabControl tb)
		{
			int seletedIndex = tb.getSeletedIndex();
			bool flag = this.m_current != null;
			if (flag)
			{
				this.m_current.onClose();
				this.m_current.gameObject.SetActive(false);
			}
			switch (seletedIndex)
			{
			case 0:
				this.ShowSystemPanel();
				break;
			case 1:
				this.ShowGamePanel();
				break;
			case 2:
				this.ShowGiftBagPanel();
				break;
			}
			bool flag2 = this.m_current != null;
			if (flag2)
			{
				this.m_current.onShowed();
				this.m_current.visiable = true;
			}
		}

		private void ShowSystemPanel()
		{
			bool flag = this.m_system == null;
			if (flag)
			{
				this.m_system = new a3_systemSetting.SystemPanel(this.tfSystemPanel);
				this.m_system.setPerent(this.con);
			}
			this.m_current = this.m_system;
		}

		private void ShowGamePanel()
		{
			bool flag = this.m_game == null;
			if (flag)
			{
				this.m_game = new a3_systemSetting.GamePanel(this.tfGamePanel);
				this.m_game.setPerent(this.con);
			}
			this.m_current = this.m_game;
		}

		private void ShowGiftBagPanel()
		{
			bool flag = this.m_giftBag == null;
			if (flag)
			{
				this.m_giftBag = new a3_systemSetting.GifBagPane(this.tfGiftBagPanel);
				this.m_giftBag.setPerent(this.con);
			}
			this.m_current = this.m_giftBag;
		}
	}
}
