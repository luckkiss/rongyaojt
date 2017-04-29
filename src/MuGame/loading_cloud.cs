using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using GameFramework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class loading_cloud : LoadingUI
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly loading_cloud.<>c <>9 = new loading_cloud.<>c();

			public static Action <>9__16_0;

			internal void <Update>b__16_0()
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(worldmap.EFFECT_CHUANSONG2);
				gameObject.transform.SetParent(SelfRole._inst.m_curModel, false);
				UnityEngine.Object.Destroy(gameObject, 2f);
				bool flag = a3_expbar.instance != null;
				if (flag)
				{
					a3_expbar.instance.On_Btn_Down();
				}
				InterfaceMgr.doCommandByLua("a3_litemap_btns.setToggle", "ui/interfaces/floatui/a3_litemap_btns", new object[]
				{
					true
				});
				bool flag2 = a3_skillopen.instance != null;
				if (flag2)
				{
					a3_skillopen.instance.refreshInfo();
				}
				bool flag3 = a3_runeopen.instance != null;
				if (flag3)
				{
					a3_runeopen.instance.refreshInfo();
				}
			}
		}

		public static loading_cloud instance;

		public static Action showhandle;

		private Image bga;

		public List<string> tips = new List<string>();

		private Text tiptext;

		private Image loadingMc;

		private Text loadingtext;

		private int curState = 0;

		private List<Sprite> bglist = new List<Sprite>();

		public bool showed = false;

		private int waitingTick = 0;

		public static Action _handle;

		public override void init()
		{
			this.bga = base.getComponentByPath<Image>("bga");
			Sprite[] collection = Resources.LoadAll<Sprite>("loading");
			this.bglist.AddRange(collection);
			this.bga.GetComponent<RectTransform>().sizeDelta = new Vector2(Baselayer.uiWidth, Baselayer.uiHeight);
			SXML sXML = XMLMgr.instance.GetSXML("tips", "");
			List<SXML> nodeList = sXML.GetNodeList("t", "");
			foreach (SXML current in nodeList)
			{
				string @string = current.getString("info");
				this.tips.Add(@string);
			}
			this.tiptext = base.getComponentByPath<Text>("tip/bg/text");
			this.loadingtext = base.getTransformByPath("loadingBar/Text").GetComponent<Text>();
			this.loadingMc = base.getTransformByPath("loadingBar").GetComponent<Image>();
		}

		public override void onShowed()
		{
			bool flag = this.showed;
			if (!flag)
			{
				this.showed = true;
				this.curState = 0;
				loading_cloud.instance = this;
				base.onShowed();
				this.play();
			}
		}

		public override void onClosed()
		{
			bool flag = !this.showed;
			if (!flag)
			{
				this.showed = false;
				loading_cloud.instance = null;
				this.bga.gameObject.SetActive(true);
				this.curState = 0;
				loading_cloud._handle = null;
				UiEventCenter.getInstance().onMapChanged();
				base.onClosed();
			}
		}

		public void play()
		{
			bool flag = this.bga == null;
			if (flag)
			{
				this.init();
			}
			NewbieModel.getInstance().hide();
			this.Randombg();
			this.onAniOver();
			this.RandomTip();
			this.PlayLoading();
		}

		private void Randombg()
		{
			this.bga.sprite = this.bglist[UnityEngine.Random.Range(0, this.bglist.Count)];
		}

		private void Update()
		{
			bool flag = this.curState == 1;
			if (flag)
			{
				this.curState = 0;
				bool flag2 = loading_cloud.showhandle != null;
				if (flag2)
				{
					loading_cloud.showhandle();
				}
				loading_cloud.showhandle = null;
			}
			else
			{
				bool flag3 = this.curState == 2 && debug.instance.async == null;
				if (flag3)
				{
					this.waitingTick++;
					bool flag4 = this.waitingTick < 20;
					if (!flag4)
					{
						this.waitingTick = 0;
						this.curState = 0;
						this.playOver();
					}
				}
				else
				{
					bool flag5 = this.curState == 3;
					if (flag5)
					{
						this.curState = 0;
						InterfaceMgr.getInstance().close(InterfaceMgr.LOADING_CLOUD);
						bool flag6 = a3_mapname.instance != null;
						if (flag6)
						{
							a3_mapname.instance.refreshInfo();
						}
						DoAfterMgr arg_F7_0 = DoAfterMgr.instacne;
						Action arg_F7_1;
						if ((arg_F7_1 = loading_cloud.<>c.<>9__16_0) == null)
						{
							arg_F7_1 = (loading_cloud.<>c.<>9__16_0 = new Action(loading_cloud.<>c.<>9.<Update>b__16_0));
						}
						arg_F7_0.addAfterRender(arg_F7_1);
					}
				}
			}
		}

		public static void showIt(Action onfin)
		{
			bool flag = loading_cloud.instance != null;
			if (!flag)
			{
				loading_cloud.showhandle = onfin;
				InterfaceMgr.getInstance().open(InterfaceMgr.LOADING_CLOUD, null, false);
			}
		}

		private void onAniOver()
		{
			this.curState = 1;
		}

		private void RandomTip()
		{
			this.tiptext.text = this.tips[UnityEngine.Random.Range(0, this.tips.Count)];
		}

		public void PlayLoading()
		{
			this.loadingMc.fillAmount = 0f;
			this.loadingtext.text = 0 + "%";
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => this.loadingMc.fillAmount, delegate(float x)
			{
				this.loadingMc.fillAmount = x;
				this.loadingtext.text = Mathf.Floor(x * 100f) + "%";
			}, 0.9f, 0.4f);
		}

		public void hide(Action handle)
		{
			this.curState = 2;
			loading_cloud._handle = handle;
		}

		private void playOver()
		{
			bool flag = loading_cloud._handle != null;
			if (flag)
			{
				loading_cloud._handle();
			}
			loading_cloud._handle = null;
			TweenerCore<float, float, FloatOptions> tweenerCore = DOTween.To(() => this.loadingMc.fillAmount, delegate(float x)
			{
				this.loadingMc.fillAmount = x;
				this.loadingtext.text = Mathf.Floor(x * 100f) + "%";
			}, 1f, 0.2f).OnComplete(delegate
			{
				this.playOverHandle();
			});
		}

		private void playOverHandle()
		{
			this.curState = 3;
		}
	}
}
