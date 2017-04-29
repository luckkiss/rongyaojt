using Cross;
using System;

namespace MuGame
{
	public interface LGIUINpcDialog
	{
		void OpenNPCFun(uint npcid);

		void OpenNPCMission(uint npcid, uint misid, string misType = "mission", Variant needData = null);

		void CloseNPCDialog();

		bool IsOpenUI();

		void MisInfoBack(Variant npcdata, Variant msgData);
	}
}
