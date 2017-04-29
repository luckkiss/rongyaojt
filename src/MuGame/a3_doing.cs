using GameFramework;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal class a3_doing : FloatUi
	{
		private BaseButton btn;

		public Action btn_event;

		private Text text;

		private string str = null;

		public override void init()
		{
			this.btn = new BaseButton(base.getTransformByPath("msg/text/btn_bg/btn"), 1, 1);
			this.btn.onClick = delegate(GameObject g)
			{
				bool flag = this.btn_event != null;
				if (flag)
				{
					this.btn_event();
				}
			};
			this.text = base.getComponentByPath<Text>("msg/text");
		}

		public override void onShowed()
		{
			base.transform.SetAsFirstSibling();
			bool flag = this.uiData != null;
			if (flag)
			{
				bool flag2 = this.uiData.Count > 0;
				if (flag2)
				{
					this.btn_event = (Action)this.uiData[0];
				}
				bool flag3 = this.uiData.Count > 1;
				if (flag3)
				{
					this.str = (string)this.uiData[1];
				}
			}
			this.Refresh();
		}

		public override void onClosed()
		{
			base.onClosed();
		}

		private void Refresh()
		{
			this.text.text = this.str;
		}
	}
}
