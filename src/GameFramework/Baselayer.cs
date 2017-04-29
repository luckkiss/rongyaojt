using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameFramework
{
	public class Baselayer : MonoBehaviour
	{
		public static int LAYER_TYPE_WINDOW = 0;

		public static int LAYER_TYPE_FLOATUI = 1;

		public static int LAYER_TYPE_LOADING = 2;

		public static int LAYER_TYPE_STORY = 3;

		public static int LAYER_TYPE_FIGHT_TEXT = 4;

		public static int LAYER_TYPE_WINDOW_3D = 5;

		public string uiName;

		public ArrayList uiData;

		public bool isFunctionBar = false;

		public static RectTransform cemaraRectTran;

		protected GameObject goBg;

		private static float _uiwidth = -1f;

		private static float _uiHeight = -1f;

		public static int scaleWidth = 0;

		public static int scaleHeight = 0;

		private static float _halfuiWidth = -1f;

		private static float _halfuiHeight = -1f;

		private static float _uiRatio = 0f;

		public static float uiWidth
		{
			get
			{
				bool flag = Baselayer._uiwidth < 0f;
				if (flag)
				{
					bool flag2 = Baselayer.cemaraRectTran == null;
					if (flag2)
					{
						Baselayer.cemaraRectTran = GameObject.Find("Canvas_overlay").GetComponent<RectTransform>();
						Baselayer._uiwidth = Baselayer.cemaraRectTran.rect.width;
					}
				}
				return Baselayer._uiwidth;
			}
		}

		public static float uiHeight
		{
			get
			{
				bool flag = Baselayer._uiHeight < 0f;
				if (flag)
				{
					bool flag2 = Baselayer.cemaraRectTran == null;
					if (flag2)
					{
						Baselayer.cemaraRectTran = GameObject.Find("Canvas_overlay").GetComponent<RectTransform>();
					}
					Baselayer._uiHeight = Baselayer.cemaraRectTran.rect.height;
				}
				return Baselayer._uiHeight;
			}
		}

		public static float halfuiWidth
		{
			get
			{
				bool flag = Baselayer._halfuiWidth < 0f;
				if (flag)
				{
					Baselayer._halfuiWidth = Baselayer.uiWidth / 2f;
				}
				return Baselayer._halfuiWidth;
			}
		}

		public static float halfuiHeight
		{
			get
			{
				bool flag = Baselayer._halfuiHeight < 0f;
				if (flag)
				{
					Baselayer._halfuiHeight = Baselayer.uiHeight / 2f;
				}
				return Baselayer._halfuiHeight;
			}
		}

		public static float uiRatio
		{
			get
			{
				bool flag = Baselayer._uiRatio == 0f;
				if (flag)
				{
					RectTransform component = GameObject.Find("canvas_main").GetComponent<RectTransform>();
					Baselayer._uiRatio = component.localScale.x;
				}
				return Baselayer._uiRatio;
			}
		}

		public virtual float type
		{
			get
			{
				return (float)Baselayer.LAYER_TYPE_WINDOW;
			}
		}

		public virtual bool showBG
		{
			get
			{
				return false;
			}
		}

		public virtual bool openAni
		{
			get
			{
				return false;
			}
		}

		public static void setDesignContentScale(int designWidth = 1136, int designHeight = 640)
		{
		}

		public void removeAllChild(Transform trans)
		{
			for (int i = 0; i < trans.childCount; i++)
			{
				UnityEngine.Object.Destroy(trans.GetChild(i).gameObject);
			}
		}

		private void Start()
		{
			this.init();
			this.__open(false);
		}

		public virtual void init()
		{
		}

		public virtual void onShowed()
		{
			UiEventCenter.getInstance().onWinOpen(this.uiName);
		}

		public virtual void onClosed()
		{
			UiEventCenter.getInstance().onWinClosed(this.uiName);
		}

		public virtual void onAfterShow()
		{
		}

		public virtual void dispose()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		public Transform getTransformByPath(string path)
		{
			return base.transform.FindChild(path);
		}

		public GameObject getGameObjectByPath(string path)
		{
			return this.getTransformByPath(path).gameObject;
		}

		public Button getButtonByPath(string path)
		{
			return this.getComponentByPath<Button>(path);
		}

		public T getComponentByPath<T>(string path) where T : Component
		{
			return base.transform.FindChild(path).GetComponent<T>();
		}

		public EventTriggerListener getEventTrigerByPath(string path)
		{
			return EventTriggerListener.Get(this.getGameObjectByPath(path));
		}

		public void clearListenersPath(string path)
		{
			GameObject gameObjectByPath = this.getGameObjectByPath(path);
			EventTriggerListener.Get(gameObjectByPath).clearAllListener();
			UnityEngine.Object.Destroy(gameObjectByPath);
		}

		public virtual void doShowAni()
		{
			base.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
			base.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).OnComplete(new TweenCallback(this.onAfterShow));
		}

		public void __open(bool setactive = true)
		{
			if (setactive)
			{
				base.gameObject.SetActive(true);
			}
			this.onShowed();
			bool openAni = this.openAni;
			if (openAni)
			{
				this.doShowAni();
			}
			else
			{
				this.onAfterShow();
			}
		}

		public void __close()
		{
			this.onClosed();
			bool openAni = this.openAni;
			if (openAni)
			{
				this.doCloseAni();
			}
			else
			{
				this.onCLoseAniOver();
			}
		}

		public virtual void doCloseAni()
		{
			base.transform.DOScale(0.7f, 0.3f).SetEase(Ease.InBack).OnComplete(new TweenCallback(this.onCLoseAniOver));
		}

		public void onCLoseAniOver()
		{
			base.gameObject.SetActive(false);
		}

		public void addBg()
		{
			bool flag = !this.showBG;
			if (!flag)
			{
				this.goBg = new GameObject("ig_bg_bg");
				Image image = this.goBg.AddComponent<Image>();
				RectTransform rectTransform = Baselayer.cemaraRectTran;
				this.goBg.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(rectTransform.rect.width * 2f, rectTransform.rect.height * 2f);
				image.color = new Color(0f, 0f, 0f, 0.5f);
				this.goBg.transform.SetParent(base.transform, false);
				this.goBg.transform.SetSiblingIndex(0);
			}
		}

		protected void alain()
		{
			base.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Baselayer.uiWidth, Baselayer.uiHeight);
		}
	}
}
