using GameFramework;
using System;

namespace MuGame
{
	internal class wait_loading : Window
	{
		public override void init()
		{
		}

		public override void onShowed()
		{
			base.transform.SetAsLastSibling();
			base.onShowed();
		}

		public override void onClosed()
		{
			base.onClosed();
		}
	}
}
