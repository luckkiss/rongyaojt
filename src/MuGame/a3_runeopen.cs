using DG.Tweening;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_runeopen : FloatUi
	{
		public static a3_runeopen instance;

		private Image ig_icon;

		private Image ig_icon1;

		public int open_id = -1;

		public float x;

		public float y;

		public override void init()
		{
			a3_runeopen.instance = this;
			base.gameObject.SetActive(false);
			this.x = -76f;
			this.y = -255f;
		}

		public void refreshInfo()
		{
			bool flag = this.open_id < 0;
			if (!flag)
			{
				bool flag2 = !GameRoomMgr.getInstance().checkCityRoom();
				if (!flag2)
				{
					base.gameObject.SetActive(true);
					this.ig_icon = base.transform.FindChild("mover/icon").GetComponent<Image>();
					this.ig_icon1 = base.transform.FindChild("icon").GetComponent<Image>();
					string path = "icon/rune/" + this.open_id;
					this.ig_icon.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
					this.ig_icon1.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
					this.ig_icon.gameObject.SetActive(true);
					base.CancelInvoke("timeGo");
					base.Invoke("timeGo", 3.3f);
					this.runAni(this.x, this.y);
					bool flag3 = a3_expbar.instance != null;
					if (flag3)
					{
						a3_expbar.instance.On_Btn_Up();
					}
					this.open_id = -1;
				}
			}
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
		}
	}
}
