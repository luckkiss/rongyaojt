using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	internal class UIOutGameLoading : UIbase
	{
		private IUIProgressBar progLoading;

		public IUIText statusLoading;

		private IUIImageBox uiLight;

		private int _uiLightStartX = 0;

		public UIOutGameLoading(muUIClient m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new UIOutGameLoading(m as muUIClient);
		}

		protected override void _initControl(Dictionary<string, IUIBaseControl> host)
		{
			this.progLoading = (host["progLoading"] as IUIProgressBar);
			this.statusLoading = (host["statusLoading"] as IUIText);
			this.uiLight = (host["uiLight"] as IUIImageBox);
			this.progLoading.maxNum = 100f;
		}

		protected override void _onOpen()
		{
			base._onOpen();
		}

		public void setProgressBar(uint bytesLoaded, uint bytesTotal)
		{
			bool flag = this._uiLightStartX == 0;
			if (flag)
			{
				this._uiLightStartX = (int)this.uiLight.x;
			}
			float num = bytesLoaded / bytesTotal;
			this.progLoading.num = num * 4.22f;
			this.uiLight.x = (float)this._uiLightStartX + num * 728f;
		}

		public string getProgressPercent()
		{
			return this.progLoading.num + "%";
		}

		public void setTipInfo(string info)
		{
			this.statusLoading.text = info;
		}
	}
}
