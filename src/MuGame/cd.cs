using Cross;
using GameFramework;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	public class cd : Window
	{
		public static Action cdhandle;

		public static Action forceStophandle;

		public static Vector3 pos;

		public static long lastTimer;

		public static long secCD;

		public static long lastCD;

		public static cd instance;

		public static Action<cd> updateHandle;

		public Text txt;

		private Transform transLine;

		public override bool showBG
		{
			get
			{
				return false;
			}
		}

		public static void show(Action handle, float sec, bool isMS = false, Action stopHandle = null, Vector3 v = default(Vector3))
		{
			SelfRole.fsm.Stop();
			cd.cdhandle = handle;
			cd.forceStophandle = stopHandle;
			cd.secCD = (isMS ? ((long)sec) : ((long)(sec * 1000f)));
			cd.lastTimer = cd.secCD + NetClient.instance.CurServerTimeStampMS;
			cd.pos = v;
			InterfaceMgr.getInstance().open(InterfaceMgr.CD, null, false);
		}

		public static void hide()
		{
			bool flag = cd.instance == null;
			if (!flag)
			{
				bool flag2 = cd.forceStophandle != null && cd.lastTimer > NetClient.instance.CurServerTimeStampMS;
				if (flag2)
				{
					cd.forceStophandle();
				}
				InterfaceMgr.getInstance().close(InterfaceMgr.CD);
			}
		}

		public override void init()
		{
			this.transLine = base.getTransformByPath("line");
			this.txt = base.getComponentByPath<Text>("txt");
		}

		public override void onShowed()
		{
			cd.instance = this;
			base.transform.SetAsLastSibling();
			RectTransform component = base.gameObject.GetComponent<RectTransform>();
			component.position = SelfRole._inst.getHeadPos();
			bool flag = cd.pos == Vector3.zero;
			if (flag)
			{
				component.position = SelfRole._inst.getHeadPos();
			}
			else
			{
				component.localPosition = cd.pos;
			}
			MediaClient.instance.PlaySoundUrl("audio/common/trance", false, null);
			base.StartCoroutine(this.runcd());
		}

		public override void onClosed()
		{
			cd.instance = null;
			cd.cdhandle = null;
			cd.updateHandle = null;
			bool flag = this.txt != null;
			if (flag)
			{
				this.txt.text = "";
			}
			Variant variant = new Variant();
			variant._arr.Add("audio/common/trance");
			MediaClient.instance.StopSoundUrls(variant);
			base.StopCoroutine(this.runcd());
		}

		private IEnumerator runcd()
		{
			cd.<runcd>d__17 expr_06 = new cd.<runcd>d__17(0);
			expr_06.<>4__this = this;
			return expr_06;
		}
	}
}
