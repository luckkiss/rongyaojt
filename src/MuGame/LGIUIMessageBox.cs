using Cross;
using System;

namespace MuGame
{
	public interface LGIUIMessageBox
	{
		bool isMisPromptTrans
		{
			set;
		}

		void OpenRespawnUI(uint resSec, Variant data);

		void CloseRespawnUI();

		void pkg_add_items(Variant items);

		void RefreshUplvlgift();

		void OnLevelUp(int level);

		bool IsNeedMissionPrompt(uint id);

		void ShowMissionPromptMsg(uint id);
	}
}
