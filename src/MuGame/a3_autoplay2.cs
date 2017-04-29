using GameFramework;
using MuGame.Qsmy.model;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_autoplay2 : Window
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_autoplay2.<>c <>9 = new a3_autoplay2.<>c();

			public static EventTriggerListener.VectorDelegate <>9__18_1;

			internal void <init>b__18_1(GameObject g, Vector2 d)
			{
				Transform transform = g.transform.FindChild("mask");
				transform.transform.position += new Vector3(d.x, d.y, 0f);
			}
		}

		private Text nhpText;

		private Slider nhpSlider;

		private Text nmpText;

		private Slider nmpSlider;

		private Toggle buyToggle;

		private Toggle avoidToggle;

		private Toggle pkToggle;

		private Toggle respawnToggle;

		private Toggle goldRespawnToggle;

		private Toggle upboundToggle;

		private InputField timesInputField;

		private GameObject[] skillChoosed;

		private Transform skList;

		private Vector3[] corners;

		private GameObject openListSkil;

		private AutoPlayModel apModel;

		private int dragup;

		private int dragdown;

		public override void init()
		{
			this.apModel = ModelBase<AutoPlayModel>.getInstance();
			this.nhpText = base.getComponentByPath<Text>("nhptxt");
			this.nhpSlider = base.getComponentByPath<Slider>("nhpSlider");
			this.nmpText = base.getComponentByPath<Text>("nmptxt");
			this.nmpSlider = base.getComponentByPath<Slider>("nmpSlider");
			this.buyToggle = base.getComponentByPath<Toggle>("buy");
			this.nhpSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnNhpSliderChange));
			this.nmpSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnNmpSliderChange));
			this.buyToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnBuyToggleChange));
			this.avoidToggle = base.getComponentByPath<Toggle>("avoid");
			this.pkToggle = base.getComponentByPath<Toggle>("pk");
			this.respawnToggle = base.getComponentByPath<Toggle>("respawn");
			this.goldRespawnToggle = base.getComponentByPath<Toggle>("goldrespawn");
			this.upboundToggle = base.getComponentByPath<Toggle>("upbound");
			this.timesInputField = base.getComponentByPath<InputField>("times");
			this.avoidToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnAvoidToggleChange));
			this.pkToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnPkToggleChange));
			this.respawnToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnRespawnToggleChange));
			this.goldRespawnToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnGoldRespawnToggleChange));
			this.upboundToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnUpboundToggleChange));
			this.timesInputField.onValueChanged.AddListener(new UnityAction<string>(this.OnTimesInputField));
			this.skillChoosed = new GameObject[4];
			for (int i = 0; i < 4; i++)
			{
				this.skillChoosed[i] = base.getGameObjectByPath("skill/" + i);
				this.skillChoosed[i].transform.FindChild("mask").localPosition = Vector3.zero;
				EventTriggerListener eventTriggerListener = EventTriggerListener.Get(this.skillChoosed[i].gameObject);
				eventTriggerListener.onDown = delegate(GameObject g)
				{
					this.dragdown = int.Parse(g.name);
					this.dragup = int.Parse(g.name);
				};
				EventTriggerListener arg_27C_0 = eventTriggerListener;
				EventTriggerListener.VectorDelegate arg_27C_1;
				if ((arg_27C_1 = a3_autoplay2.<>c.<>9__18_1) == null)
				{
					arg_27C_1 = (a3_autoplay2.<>c.<>9__18_1 = new EventTriggerListener.VectorDelegate(a3_autoplay2.<>c.<>9.<init>b__18_1));
				}
				arg_27C_0.onDrag = arg_27C_1;
				eventTriggerListener.onEnter = delegate(GameObject g)
				{
					this.dragdown = int.Parse(g.name);
				};
				Transform transformByPath = base.getTransformByPath("skill/cbg");
				Vector3[] cornerskk = new Vector3[4];
				bool flag = transformByPath != null;
				if (flag)
				{
					transformByPath.GetComponent<RectTransform>().GetWorldCorners(cornerskk);
				}
				eventTriggerListener.onDragEnd = delegate(GameObject g, Vector2 d)
				{
					Vector3 mousePosition = Input.mousePosition;
					bool flag2 = mousePosition.x < cornerskk[0].x || mousePosition.x > cornerskk[2].x || mousePosition.y < cornerskk[0].y || mousePosition.y > cornerskk[2].y;
					if (flag2)
					{
						this.dragdown = -1;
					}
					bool flag3 = this.dragup != this.dragdown;
					if (flag3)
					{
						bool flag4 = this.dragdown == -1;
						if (flag4)
						{
							bool flag5 = this.openListSkil != null;
							if (flag5)
							{
								this.openListSkil.transform.FindChild("mask").gameObject.SetActive(false);
								this.apModel.Skills[int.Parse(g.name)] = 0;
								this.openListSkil = null;
								this.skList.parent.gameObject.SetActive(false);
							}
						}
					}
					Transform transform = g.transform.FindChild("mask");
					transform.transform.localPosition = Vector3.zero;
				};
				eventTriggerListener.onClick = new EventTriggerListener.VoidDelegate(this.OnSkillChoosedClick);
			}
			BaseButton baseButton = new BaseButton(base.getTransformByPath("closeBtn"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.OnClose);
			BaseButton baseButton2 = new BaseButton(base.getTransformByPath("eqp"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.OnEqp);
			BaseButton baseButton3 = new BaseButton(base.getTransformByPath("pick"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.OnPick);
			BaseButton baseButton4 = new BaseButton(base.getTransformByPath("start"), 1, 1);
			baseButton4.onClick = new Action<GameObject>(this.OnStart);
			BaseButton baseButton5 = new BaseButton(base.getTransformByPath("stop"), 1, 1);
			baseButton5.onClick = new Action<GameObject>(this.OnStop);
		}

		public override void onShowed()
		{
			this.RefreshRestore();
			this.RefreshSkillList();
			this.RefreshBattle();
			this.RefreshScope();
			this.OnFSMStartStop(SelfRole.fsm.Autofighting);
			StateMachine expr_33 = SelfRole.fsm;
			expr_33.OnFSMStartStop = (Action<bool>)Delegate.Combine(expr_33.OnFSMStartStop, new Action<bool>(this.OnFSMStartStop));
		}

		private void Update()
		{
			bool mouseButtonDown = Input.GetMouseButtonDown(0);
			if (mouseButtonDown)
			{
				Vector3 mousePosition = Input.mousePosition;
				bool flag = mousePosition.x < this.corners[0].x || mousePosition.x > this.corners[2].x || mousePosition.y < this.corners[0].y || mousePosition.y > this.corners[2].y;
				if (flag)
				{
					this.skList.parent.gameObject.SetActive(false);
					this.openListSkil = null;
				}
			}
		}

		public override void onClosed()
		{
			this.apModel.WriteLocalData();
			StateMachine expr_12 = SelfRole.fsm;
			expr_12.OnFSMStartStop = (Action<bool>)Delegate.Remove(expr_12.OnFSMStartStop, new Action<bool>(this.OnFSMStartStop));
		}

		private void RefreshScope()
		{
		}

		private void RefreshBattle()
		{
			this.avoidToggle.isOn = (this.apModel.Avoid > 0);
			this.pkToggle.isOn = (this.apModel.AutoPK > 0);
			this.respawnToggle.isOn = (this.apModel.StoneRespawn > 0);
			this.goldRespawnToggle.isOn = (this.apModel.GoldRespawn > 0);
			this.upboundToggle.isOn = (this.apModel.RespawnLimit > 0);
			this.timesInputField.text = this.apModel.RespawnUpBound.ToString();
		}

		private void RefreshRestore()
		{
			this.nhpSlider.value = (float)this.apModel.NHpLower;
			this.nhpText.text = ContMgr.getCont("autoplay_restore_0", new List<string>
			{
				this.apModel.NHpLower.ToString()
			});
			this.nmpSlider.value = (float)this.apModel.NMpLower;
			this.nmpText.text = ContMgr.getCont("autoplay_restore_1", new List<string>
			{
				this.apModel.NMpLower.ToString()
			});
			this.buyToggle.isOn = (this.apModel.BuyDrug > 0);
		}

		private void RefreshSkillList()
		{
			this.skList = base.getTransformByPath("skillist/skillist");
			this.corners = new Vector3[4];
			this.skList.GetComponent<RectTransform>().GetWorldCorners(this.corners);
			int i = 0;
			foreach (skill_a3Data current in ModelBase<Skill_a3Model>.getInstance().skilldic.Values)
			{
				bool flag = current.carr != ModelBase<PlayerModel>.getInstance().profession || current.skill_id == skillbar.NORNAL_SKILL_ID || current.now_lv == 0 || current.skillType2 == 1;
				if (!flag)
				{
					int skill_id = current.skill_id;
					Transform child = this.skList.GetChild(i);
					child.name = skill_id.ToString();
					child.FindChild("bg/mask").gameObject.SetActive(true);
					child.FindChild("bg").gameObject.SetActive(true);
					child.FindChild("bg/mask/icon").GetComponent<Image>().sprite = (Resources.Load("icon/skill/" + skill_id.ToString(), typeof(Sprite)) as Sprite);
					BaseButton baseButton = new BaseButton(child.FindChild("bg"), 1, 1);
					baseButton.onClick = new Action<GameObject>(this.OnSkillListClick);
					i++;
				}
			}
			for (int j = i; j < this.skList.childCount; j++)
			{
				Transform child2 = this.skList.GetChild(j);
				child2.name = "0";
				child2.FindChild("bg/mask").gameObject.SetActive(false);
				child2.FindChild("bg").gameObject.SetActive(false);
				BaseButton baseButton2 = new BaseButton(child2.FindChild("bg"), 1, 1);
				baseButton2.onClick = new Action<GameObject>(this.OnSkillListClick);
			}
			for (i = 0; i < 4; i++)
			{
				bool flag2 = this.apModel.Skills[i] == 0;
				if (flag2)
				{
					this.skillChoosed[i].transform.FindChild("mask").gameObject.SetActive(false);
				}
				else
				{
					this.skillChoosed[i].transform.FindChild("mask").gameObject.SetActive(true);
					this.skillChoosed[i].transform.FindChild("mask/icon").GetComponent<Image>().sprite = (Resources.Load("icon/skill/" + this.apModel.Skills[i].ToString(), typeof(Sprite)) as Sprite);
				}
			}
		}

		private void OnNhpSliderChange(float v)
		{
			this.apModel.NHpLower = (int)v;
			this.nhpText.text = ContMgr.getCont("autoplay_restore_0", new List<string>
			{
				this.apModel.NHpLower.ToString()
			});
		}

		private void OnNmpSliderChange(float v)
		{
			this.apModel.NMpLower = (int)v;
			this.nmpText.text = ContMgr.getCont("autoplay_restore_1", new List<string>
			{
				this.apModel.NMpLower.ToString()
			});
		}

		private void OnMhpSliderChange(float v)
		{
		}

		private void OnBuyToggleChange(bool v)
		{
			this.apModel.BuyDrug = (v ? 1 : 0);
		}

		private void OnAvoidToggleChange(bool v)
		{
			this.apModel.Avoid = (v ? 1 : 0);
		}

		private void OnPkToggleChange(bool v)
		{
			this.apModel.AutoPK = (v ? 1 : 0);
		}

		private void OnRespawnToggleChange(bool v)
		{
			this.apModel.StoneRespawn = (v ? 1 : 0);
		}

		private void OnGoldRespawnToggleChange(bool v)
		{
			this.apModel.GoldRespawn = (v ? 1 : 0);
		}

		private void OnUpboundToggleChange(bool v)
		{
			this.apModel.RespawnLimit = (v ? 1 : 0);
		}

		private void OnTimesInputField(string v)
		{
			bool flag = !string.IsNullOrEmpty(v);
			if (flag)
			{
				StateInit.Instance.RespawnTimes = (this.apModel.RespawnUpBound = int.Parse(v));
			}
		}

		private void OnSkillChoosedClick(GameObject go)
		{
			this.skList.parent.gameObject.SetActive(true);
			this.openListSkil = go;
		}

		private void OnSkillListClick(GameObject go)
		{
			bool flag = this.openListSkil == null;
			if (!flag)
			{
				int num = int.Parse(this.openListSkil.name);
				int num2 = int.Parse(go.transform.parent.name);
				bool flag2 = num2 == 0;
				if (flag2)
				{
					this.openListSkil.transform.FindChild("mask").gameObject.SetActive(false);
				}
				else
				{
					this.openListSkil.transform.FindChild("mask").gameObject.SetActive(true);
					this.openListSkil.transform.FindChild("mask/icon").GetComponent<Image>().sprite = (Resources.Load("icon/skill/" + num2.ToString(), typeof(Sprite)) as Sprite);
					for (int i = 0; i < this.apModel.Skills.Length; i++)
					{
						bool flag3 = num2 == this.apModel.Skills[i] && i != num;
						if (flag3)
						{
							this.apModel.Skills[i] = 0;
							this.skillChoosed[i].transform.FindChild("mask").gameObject.SetActive(false);
						}
					}
				}
				this.apModel.Skills[num] = num2;
				this.openListSkil = null;
				this.skList.parent.gameObject.SetActive(false);
			}
		}

		private void OnClose(GameObject go)
		{
			InterfaceMgr.getInstance().close(InterfaceMgr.A3_AUTOPLAY2);
		}

		private void OnEqp(GameObject go)
		{
			bool flag = ModelBase<A3_VipModel>.getInstance().Level >= 3;
			if (flag)
			{
				InterfaceMgr.getInstance().open(InterfaceMgr.A3_AUTOPLAY_EQP, null, false);
			}
			else
			{
				flytxt.instance.fly("VIP等级不足!", 0, default(Color), null);
			}
		}

		private void OnPick(GameObject go)
		{
			InterfaceMgr.getInstance().open(InterfaceMgr.A3_AUTOPLAY_PICK, null, false);
		}

		private void OnStart(GameObject go)
		{
			bool flag = GRMap.curSvrConf != null && GRMap.curSvrConf.ContainsKey("id") && GRMap.curSvrConf["id"] == 10;
			if (flag)
			{
				flytxt.instance.fly("无法在主城挂机！", 0, default(Color), null);
			}
			else
			{
				SelfRole.fsm.StartAutofight();
				this.OnClose(null);
				flytxt.flyUseContId("autoplay_start", null, 0);
			}
		}

		private void OnStop(GameObject go)
		{
			SelfRole.fsm.Stop();
			this.OnClose(null);
			flytxt.flyUseContId("autoplay_stop", null, 0);
		}

		private void OnFSMStartStop(bool isRunning)
		{
			base.getGameObjectByPath("start").SetActive(!isRunning);
			base.getGameObjectByPath("stop").SetActive(isRunning);
		}
	}
}
