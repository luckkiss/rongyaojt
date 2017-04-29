using GameFramework;
using System;

namespace MuGame
{
	internal class sdkloading : LoadingUI
	{
		public override void init()
		{
			base.init();
		}

		public override void onShowed()
		{
			InterfaceMgr.getInstance().showLoadingBg(true);
			base.onShowed();
		}

		public override void onClosed()
		{
			InterfaceMgr.getInstance().showLoadingBg(false);
			base.onClosed();
		}
	}
}
