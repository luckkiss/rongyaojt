using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using GameFramework;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_rank : AchiveSkin
	{
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			public static readonly a3_rank.<>c <>9 = new a3_rank.<>c();

			public static Action<GameObject> <>9__10_0;

			internal void <init>b__10_0(GameObject g)
			{
				InterfaceMgr.getInstance().close(InterfaceMgr.A3_ACHIEVEMENT);
			}
		}

		private Image nowtitle;

		private Slider expslider;

		private Text exptext;

		private Toggle showontoggle;

		private Text showontext;

		private int nowshow;

		private BaseButton addlv;

		private BaseButton addlv1;

		private Transform content;

		public a3_rank(Window win, Transform tran) : base(win, tran)
		{
		}

		public override void init()
		{
			BaseButton arg_37_0 = new BaseButton(base.transform.FindChild("close"), 1, 1);
			Action<GameObject> arg_37_1;
			if ((arg_37_1 = a3_rank.<>c.<>9__10_0) == null)
			{
				arg_37_1 = (a3_rank.<>c.<>9__10_0 = new Action<GameObject>(a3_rank.<>c.<>9.<init>b__10_0));
			}
			arg_37_0.onClick = arg_37_1;
			this.addlv = new BaseButton(base.transform.FindChild("con/upgrade_btn1"), 1, 1);
			this.addlv.onClick = new Action<GameObject>(this.onAddLv);
			this.addlv1 = new BaseButton(base.transform.FindChild("con/upgrade_btn2"), 1, 1);
			this.addlv1.onClick = new Action<GameObject>(this.onAddLv);
			BaseButton baseButton = new BaseButton(base.transform.FindChild("con/showon"), 1, 1);
			baseButton.onClick = new Action<GameObject>(this.onShoeorHideTitile);
			BaseButton baseButton2 = new BaseButton(base.transform.FindChild("con/left"), 1, 1);
			baseButton2.onClick = new Action<GameObject>(this.GoLeft);
			BaseButton baseButton3 = new BaseButton(base.transform.FindChild("con/right"), 1, 1);
			baseButton3.onClick = new Action<GameObject>(this.GoRight);
			this.nowtitle = base.getComponentByPath<Image>("con/header/Image");
			this.expslider = base.getComponentByPath<Slider>("con/exp");
			this.exptext = base.getComponentByPath<Text>("con/exp/Text");
			this.showontoggle = base.getComponentByPath<Toggle>("con/showon");
			this.showontext = base.getComponentByPath<Text>("con/showon/Label");
			this.CreateList();
			this.nowshow = a3_RankModel.now_id;
		}

		public override void onShowed()
		{
			this.Refresh(null);
			BaseProxy<A3_RankProxy>.getInstance().addEventListener(A3_RankProxy.RANKADDLV, new Action<GameEvent>(this.Refresh));
			BaseProxy<A3_RankProxy>.getInstance().addEventListener(A3_RankProxy.RANKREFRESH, new Action<GameEvent>(this.Refresh));
		}

		public override void onClosed()
		{
			BaseProxy<A3_RankProxy>.getInstance().removeEventListener(A3_RankProxy.RANKADDLV, new Action<GameEvent>(this.Refresh));
			BaseProxy<A3_RankProxy>.getInstance().removeEventListener(A3_RankProxy.RANKREFRESH, new Action<GameEvent>(this.Refresh));
		}

		private void CreateList()
		{
			this.content = base.transform.FindChild("con/ranklist/content");
			GameObject gameObject = base.transform.FindChild("con/ranklist/0").gameObject;
			GameObject gameObject2 = base.transform.FindChild("con/ranklist/0/0").gameObject;
			foreach (rankinfos current in ModelBase<a3_RankModel>.getInstance().dicrankinfo.Values)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject3.transform.SetParent(this.content.transform);
				gameObject3.transform.localScale = Vector3.one;
				gameObject3.transform.localPosition = Vector3.zero;
				gameObject3.SetActive(true);
				gameObject3.name = current.title_id.ToString();
				string path = "icon/achievement/title_ui/" + current.title_id;
				gameObject3.transform.FindChild("title/Image").GetComponent<Image>().sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
				gameObject3.transform.FindChild("title/Image").GetComponent<Image>().SetNativeSize();
				gameObject3.transform.FindChild("title/icon").GetComponent<Image>().sprite = U3DAPI.U3DResLoad<Sprite>("icon/achievement/title_ui/t" + current.title_id);
				UIDark uIDark = gameObject3.AddComponent<UIDark>();
				bool flag = current.title_id > a3_RankModel.now_id;
				if (flag)
				{
					uIDark.ADDO();
				}
				Transform transform = gameObject3.transform.FindChild("att");
				foreach (uint current2 in current.nature.Keys)
				{
					GameObject gameObject4 = UnityEngine.Object.Instantiate<GameObject>(gameObject2);
					gameObject4.transform.SetParent(transform.transform);
					gameObject4.transform.localScale = Vector3.one;
					gameObject4.transform.localPosition = Vector3.zero;
					gameObject4.SetActive(true);
					gameObject4.transform.FindChild("1").GetComponent<Text>().text = this.LRAlign(Globle.getAttrNameById((int)current2)) + ":";
					bool flag2 = current2 == 33u;
					if (flag2)
					{
						gameObject4.transform.FindChild("1/2").GetComponent<Text>().text = ((float)current.nature[current2] / 10f).ToString() + "%";
					}
					else
					{
						gameObject4.transform.FindChild("1/2").GetComponent<Text>().text = current.nature[current2].ToString();
					}
				}
			}
			GridLayoutGroup component = this.content.GetComponent<GridLayoutGroup>();
			this.content.GetComponent<RectTransform>().sizeDelta = new Vector2((component.cellSize.x + component.spacing.x + 0.1f) * (float)ModelBase<a3_RankModel>.getInstance().dicrankinfo.Count, 0f);
		}

		private void Refresh(GameEvent gg = null)
		{
			string path = "icon/achievement/title_ui/" + a3_RankModel.now_id;
			this.nowtitle.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			this.nowtitle.SetNativeSize();
			this.SetExpValue();
			this.SetShowonText();
			this.nowshow = Mathf.Max(1, a3_RankModel.now_id);
			bool flag = a3_RankModel.now_id == 0;
			if (flag)
			{
				this.SetFocus(1);
			}
			else
			{
				this.SetFocus(a3_RankModel.now_id);
			}
			this.SetDark();
		}

		private string LRAlign(string str)
		{
			string result = null;
			bool flag = str.Length == 4;
			if (flag)
			{
				result = str;
			}
			else
			{
				bool flag2 = str.Length == 3;
				if (flag2)
				{
					result = string.Concat(new string[]
					{
						str[0].ToString(),
						"   ",
						str[1].ToString(),
						"   ",
						str[2].ToString()
					});
				}
				else
				{
					bool flag3 = str.Length == 2;
					if (flag3)
					{
						result = str[0].ToString() + "        " + str[1].ToString();
					}
					else
					{
						bool flag4 = str.Length == 1;
						if (flag4)
						{
							result = "      " + str[0].ToString() + "      ";
						}
					}
				}
			}
			return result;
		}

		private void SetExpValue()
		{
			bool flag = !ModelBase<a3_RankModel>.getInstance().dicrankinfo.ContainsKey(a3_RankModel.now_id + 1);
			if (flag)
			{
				this.expslider.value = 0f;
				this.exptext.text = "满级";
				this.addlv.interactable = false;
				this.addlv1.interactable = false;
			}
			else
			{
				float endValue = (a3_RankModel.nowexp > 0) ? ((float)a3_RankModel.nowexp / (float)ModelBase<a3_RankModel>.getInstance().dicrankinfo[a3_RankModel.now_id + 1].rankexp) : 0f;
				DOTween.To(() => this.expslider.value, delegate(float s)
				{
					this.expslider.value = s;
				}, endValue, 0.2f).SetEase(Ease.InExpo);
				DOTween.To(() => (int)float.Parse(this.exptext.text.Split(new char[]
				{
					'/'
				})[0]), delegate(int s)
				{
					this.exptext.text = s.ToString() + "/" + ModelBase<a3_RankModel>.getInstance().dicrankinfo[a3_RankModel.now_id + 1].rankexp;
				}, a3_RankModel.nowexp, 0.2f).SetEase(Ease.InExpo);
			}
		}

		private void onAddLv(GameObject go)
		{
			bool flag = ModelBase<PlayerModel>.getInstance().ach_point >= 100u;
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

		private void SetShowonText()
		{
			bool flag = a3_RankModel.now_id > 0;
			if (flag)
			{
				bool flag2 = !a3_RankModel.nowisactive;
				if (flag2)
				{
					this.showontoggle.isOn = false;
					this.showontext.text = "显示称号";
				}
				else
				{
					this.showontoggle.isOn = true;
					this.showontext.text = "显示称号";
				}
			}
		}

		private void onShoeorHideTitile(GameObject go)
		{
			BaseProxy<A3_RankProxy>.getInstance().sendProxy(A3_RankProxy.RANKREFRESH, -1, this.showontoggle.isOn);
			a3_RankModel.nowisactive = this.showontoggle.isOn;
			this.Refresh(null);
		}

		private void SetFocus(int i)
		{
			RectTransform component = base.transform.FindChild("con/ranklist/content").GetComponent<RectTransform>();
			GridLayoutGroup component2 = base.transform.FindChild("con/ranklist/content").GetComponent<GridLayoutGroup>();
			bool flag = ModelBase<a3_RankModel>.getInstance().dicrankinfo.ContainsKey(i + 3);
			if (flag)
			{
				component.DOLocalMoveX((float)(-(float)i - 1) * (component2.cellSize.x + component2.spacing.x), 0.2f, false);
			}
			else
			{
				component.DOLocalMoveX((float)(-(float)ModelBase<a3_RankModel>.getInstance().dicrankinfo.Count + 2) * (component2.cellSize.x + component2.spacing.x), 0.2f, false);
			}
		}

		private void GoLeft(GameObject go = null)
		{
			this.nowshow--;
			bool flag = ModelBase<a3_RankModel>.getInstance().dicrankinfo.ContainsKey(this.nowshow - 1);
			if (flag)
			{
				this.SetFocus(Mathf.Max(this.nowshow, 0));
			}
			bool flag2 = this.nowshow <= 1;
			if (flag2)
			{
				this.nowshow++;
			}
		}

		private void GoRight(GameObject go = null)
		{
			this.nowshow++;
			bool flag = ModelBase<a3_RankModel>.getInstance().dicrankinfo.ContainsKey(this.nowshow);
			if (flag)
			{
				this.SetFocus(Mathf.Max(this.nowshow, 0));
			}
			else
			{
				this.nowshow--;
			}
		}

		private void SetDark()
		{
			Transform transform = this.content.FindChild(a3_RankModel.now_id.ToString());
			bool flag = transform != null;
			if (flag)
			{
				UIDark component = transform.GetComponent<UIDark>();
				bool flag2 = component != null && component.IsDark;
				if (flag2)
				{
					component.REMO();
				}
			}
			Transform[] componentsInChildren = this.content.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform2 = componentsInChildren[i];
				bool flag3 = transform2.parent == this.content.transform;
				if (flag3)
				{
					bool flag4 = int.Parse(transform2.name) <= a3_RankModel.now_id;
					if (flag4)
					{
						UIDark component2 = transform2.GetComponent<UIDark>();
						bool flag5 = component2 != null && component2.IsDark;
						if (flag5)
						{
							component2.REMO();
						}
					}
				}
			}
		}
	}
}
