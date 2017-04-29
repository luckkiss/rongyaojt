using Cross;
using System;

namespace MuGame
{
	public class UILinksManager
	{
		public muUIClient _uiClient;

		public static UILinksManager singleton = null;

		private lgSelfPlayer selfPlayer
		{
			get
			{
				return (this._uiClient.g_gameM as muLGClient).g_selfPlayer;
			}
		}

		private LGUIMainUIImpl_NEED_REMOVE lguimain
		{
			get
			{
				return this._uiClient.getLGUI("LGUIMainUIImpl") as LGUIMainUIImpl_NEED_REMOVE;
			}
		}

		public UILinksManager(muUIClient m)
		{
			this._uiClient = m;
			UILinksManager.singleton = this;
		}

		public void UILinkEvent(string str)
		{
		}

		public void OpenUI(Variant data)
		{
		}

		public void CloseUI(object data)
		{
		}

		protected void openMobilegift()
		{
		}

		protected void openMicroload()
		{
		}

		private void autoGame()
		{
		}

		protected void showVendor(int itmid)
		{
		}
	}
}
