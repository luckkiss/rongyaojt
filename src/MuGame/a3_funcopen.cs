using DG.Tweening;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_funcopen : FloatUi
	{
		public static a3_funcopen instance;

		private Image ig_icon;

		private Image ig_icon1;

		public bool is_show = false;

		public override void init()
		{
			a3_funcopen.instance = this;
			base.gameObject.SetActive(false);
		}

		public void refreshInfo(int id, float x, float y)
		{
			base.gameObject.SetActive(true);
			this.ig_icon = base.transform.FindChild("mover/icon").GetComponent<Image>();
			this.ig_icon1 = base.transform.FindChild("icon").GetComponent<Image>();
			string path = "icon/func_open/" + id;
			this.ig_icon.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			this.ig_icon1.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			this.ig_icon.gameObject.SetActive(true);
			base.CancelInvoke("timeGo");
			base.Invoke("timeGo", 3.3f);
			this.runAni(x, y);
			bool flag = a3_expbar.instance != null;
			if (flag)
			{
				a3_expbar.instance.On_Btn_Down();
			}
			InterfaceMgr.doCommandByLua("a3_litemap_btns.setToggle", "ui/interfaces/floatui/a3_litemap_btns", new object[]
			{
				true
			});
			bool autofighting = SelfRole.fsm.Autofighting;
			if (autofighting)
			{
				bool flag2 = StateInit.Instance.IsOutOfAutoPlayRange();
				if (flag2)
				{
					SelfRole.fsm.Stop();
				}
				else
				{
					SelfRole.fsm.Resume();
				}
			}
			else
			{
				SelfRole.fsm.Stop();
			}
			a3_task_auto.instance.stopAuto = true;
			this.is_show = true;
			InterfaceMgr.getInstance().close(InterfaceMgr.DIALOG);
		}

		private void runAni(float x, float y)
		{
			this.ig_icon1.gameObject.SetActive(false);
			GameObject txtclone = UnityEngine.Object.Instantiate<GameObject>(this.ig_icon1.gameObject);
			txtclone.gameObject.SetActive(true);
			txtclone.transform.SetParent(base.transform, false);
			txtclone.transform.SetAsFirstSibling();
			Tweener t = txtclone.transform.DOLocalMove(new Vector3(x, y, 0f), 0.5f, false).SetDelay(2.8f);
			t.SetEase(Ease.InOutCirc);
			t.OnComplete(delegate
			{
				UnityEngine.Object.Destroy(txtclone);
			});
		}

		private void timeGo()
		{
			base.gameObject.SetActive(false);
			UiEventCenter.getInstance().onWinClosed(this.uiName);
			a3_task_auto.instance.stopAuto = false;
			this.is_show = false;
		}
	}
}
