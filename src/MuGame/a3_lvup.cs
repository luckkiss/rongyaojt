using GameFramework;
using System;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_lvup : FloatUi
	{
		public static a3_lvup instance;

		public override void init()
		{
			a3_lvup.instance = this;
			base.gameObject.SetActive(false);
		}

		public void refreshInfo(uint lv)
		{
			base.gameObject.SetActive(true);
			base.transform.FindChild("lv").GetComponent<Text>().text = lv.ToString();
			base.CancelInvoke("timeGo");
			base.Invoke("timeGo", 3f);
		}

		private void timeGo()
		{
			base.gameObject.SetActive(false);
		}
	}
}
