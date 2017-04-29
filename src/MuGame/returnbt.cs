using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class returnbt : StoryUI
	{
		public BaseButton bt;

		public override void init()
		{
			base.alain();
			this.bt = new BaseButton(base.getTransformByPath("Button"), 1, 1);
			this.bt.onClick = new Action<GameObject>(this.onClick);
			base.gameObject.SetActive(false);
			base.CancelInvoke("showui_phone");
			base.Invoke("showui_phone", 0.1f);
		}

		public void showui_phone()
		{
			base.gameObject.SetActive(true);
		}

		private void onClick(GameObject go)
		{
			SceneCamera.ResetAfterLoginCam();
		}
	}
}
