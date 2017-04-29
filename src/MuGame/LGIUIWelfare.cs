using Cross;
using System;

namespace MuGame
{
	public interface LGIUIWelfare
	{
		void SetSigninawd(Variant data);

		void SetClogawd(Variant data);

		void UpdateRegister(Variant data);

		void SetDmisInfo(Variant data);

		void DmisChange(int type, Variant data);

		void GetDmisRes(int awdid);

		void FreshloginAward();

		void GetloginAward(Variant data = null);

		void lvlUpShowCard();

		void RefOffLineExpList();

		void OpenWelfare(string str);

		bool isOpen();
	}
}
