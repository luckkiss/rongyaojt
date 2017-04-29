using GameFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class maploading : LoadingUI
	{
		public static maploading instance;

		public static maploading isshow;

		public List<string> tips = new List<string>();

		private Text tiptext;

		private Image bgimage;

		private Image loadingMc;

		private Text loadingtext;

		private GameObject go_info;

		private List<Sprite> bglist = new List<Sprite>();

		private float cur = 0f;

		private bool b_close = false;

		private bool b_load = true;

		public override void init()
		{
			maploading.instance = this;
			Sprite[] collection = Resources.LoadAll<Sprite>("loading");
			this.bglist.AddRange(collection);
			this.go_info = base.transform.FindChild("info").gameObject;
			this.go_info.SetActive(true);
			this.loadingtext = base.getTransformByPath("info/loadingBar/Text").GetComponent<Text>();
			this.loadingMc = base.getTransformByPath("info/loadingBar").GetComponent<Image>();
			this.bgimage = base.transform.FindChild("info/bg_r").GetComponent<Image>();
			bool flag = Baselayer.cemaraRectTran == null;
			if (flag)
			{
				Baselayer.cemaraRectTran = GameObject.Find("Canvas_overlay").GetComponent<RectTransform>();
			}
			RectTransform cemaraRectTran = Baselayer.cemaraRectTran;
			RectTransform component = base.getTransformByPath("info/bg_r").GetComponent<RectTransform>();
			component.sizeDelta = new Vector2(cemaraRectTran.rect.width, cemaraRectTran.rect.height);
			SXML sXML = XMLMgr.instance.GetSXML("tips", "");
			List<SXML> nodeList = sXML.GetNodeList("t", "");
			foreach (SXML current in nodeList)
			{
				string @string = current.getString("info");
				this.tips.Add(@string);
			}
			this.tiptext = base.getComponentByPath<Text>("info/tip/bg/text");
			base.gameObject.SetActive(true);
			base.transform.localScale = base.transform.localScale;
		}

		public void showui_phone()
		{
			bool flag = maploading.isshow;
			if (flag)
			{
				base.gameObject.SetActive(true);
			}
		}

		public void setPercent(float cur, float max)
		{
		}

		public void setTips(string str)
		{
		}

		public override void onShowed()
		{
			base.onShowed();
			this.RandomTip();
			this.Randombg();
			maploading.isshow = this;
			base.StartCoroutine(this.loading());
		}

		private IEnumerator loading()
		{
			this.loadingMc.fillAmount = 0f;
			this.loadingtext.text = 0 + "%";
			this.cur = 0f;
			this.b_close = false;
			bool flag = true;
			while (flag)
			{
				bool flag2 = this.b_close && this.b_load;
				if (flag2)
				{
					bool flag3 = this.cur >= 50f;
					if (flag3)
					{
						this.cur += 2f;
					}
					else
					{
						this.cur += 1f;
					}
					bool flag4 = this.cur >= 100f;
					if (flag4)
					{
						InterfaceMgr.getInstance().close(InterfaceMgr.MAP_LOADING);
						flag = false;
						yield break;
					}
				}
				else
				{
					bool flag5 = this.cur > 97f;
					if (flag5)
					{
						this.cur = 98f;
					}
					else
					{
						bool flag6 = this.cur >= 50f;
						if (flag6)
						{
							this.cur += 0.3f;
						}
						else
						{
							this.cur += 3f;
						}
					}
				}
				this.loadingMc.fillAmount = this.cur / 100f;
				this.loadingtext.text = Math.Ceiling((double)this.cur) + "%";
				yield return new WaitForEndOfFrame();
			}
			yield break;
		}

		private void Randombg()
		{
			this.bgimage.sprite = this.bglist[UnityEngine.Random.Range(0, this.bglist.Count)];
		}

		private void RandomTip()
		{
			this.tiptext.text = this.tips[UnityEngine.Random.Range(0, this.tips.Count)];
		}

		public override void onClosed()
		{
			base.onClosed();
			maploading.isshow = null;
		}

		public void closeLoadWait(float time)
		{
			base.CancelInvoke("wait_close");
			base.Invoke("wait_close", time);
		}

		private void wait_close()
		{
			this.b_close = true;
			InterfaceMgr.getInstance().closeUiFirstTime();
		}

		public void loadingUi(List<string> m_first_ui, List<string> m_first_ui_lua, bool b_proxy = false)
		{
			this.b_load = false;
			base.StartCoroutine(this.openFirstUi(m_first_ui, m_first_ui_lua, b_proxy));
		}

		private IEnumerator openFirstUi(List<string> m_first_ui, List<string> m_first_ui_lua, bool b_proxy = false)
		{
			int num;
			for (int i = 0; i < m_first_ui_lua.Count; i = num + 1)
			{
				InterfaceMgr.openByLua(m_first_ui_lua[i], null);
				yield return new WaitForSeconds(0.1f);
				num = i;
			}
			for (int j = 0; j < m_first_ui.Count; j = num + 1)
			{
				InterfaceMgr.getInstance().open(m_first_ui[j], null, false);
				yield return new WaitForSeconds(0.1f);
				num = j;
			}
			if (b_proxy)
			{
				this.initproxy_ui();
			}
			yield break;
		}

		public void initproxy_ui()
		{
			this.b_load = true;
			IconHintMgr.getInsatnce().inituiisok = true;
			IconHintMgr.getInsatnce().initui();
			InterfaceMgr.getInstance().closeUiFirstTime();
			BaseProxy<A3_signProxy>.getInstance().sendproxy(1, 0);
			BaseProxy<LotteryProxy>.getInstance().sendlottery(1);
			BaseProxy<ExchangeProxy>.getInstance().GetExchangeInfo();
			BaseProxy<welfareProxy>.getInstance().sendWelfare(welfareProxy.ActiveType.selfWelfareInfo, 4294967295u);
			SceneCamera.CheckLoginCam();
		}
	}
}
