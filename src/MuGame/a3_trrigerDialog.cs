using Cross;
using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_trrigerDialog : FloatUi
	{
		public static a3_trrigerDialog instance;

		private Text txt_dialog;

		private Image ig_icon;

		private GameObject go_dialog;

		public override void init()
		{
			a3_trrigerDialog.instance = this;
			this.go_dialog = base.transform.FindChild("dialog").gameObject;
			this.go_dialog.SetActive(false);
			this.txt_dialog = base.transform.FindChild("dialog/txt").GetComponent<Text>();
			this.ig_icon = base.transform.FindChild("dialog/icon").GetComponent<Image>();
		}

		public void CheckDialog(int trrigerid)
		{
			uint curLevelId = MapModel.getInstance().curLevelId;
			int curDiff = (int)MapModel.getInstance().curDiff;
			Variant variant = SvrLevelConfig.instacne.get_level_data(curLevelId);
			Variant variant2 = variant["diff_lvl"][curDiff]["map"][0];
			foreach (Variant current in variant2["trigger"]._arr)
			{
				int num = current["id"];
				bool flag = num == trrigerid;
				if (flag)
				{
					bool flag2 = current.ContainsKey("dialog");
					if (flag2)
					{
						this.doDialog(current["dialog"][0]["icon"], current["dialog"][0]["des"], current["dialog"][0]["last"]);
					}
					break;
				}
			}
		}

		public void doDialog(int icon, string txt, float time)
		{
			this.go_dialog.SetActive(true);
			string path = "icon/bosshead/" + icon;
			this.ig_icon.sprite = (Resources.Load(path, typeof(Sprite)) as Sprite);
			this.txt_dialog.text = txt;
			base.CancelInvoke("onshowend");
			base.Invoke("onshowend", time);
		}

		private void onshowend()
		{
			this.go_dialog.SetActive(false);
		}
	}
}
