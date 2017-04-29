using Cross;
using System;

namespace GameFramework
{
	public interface IUI : IGameEventDispatcher, IUIBase
	{
		void onOpen(Variant data);

		void onClose();

		void setCtrl(BaseLGUI uc);

		BaseLGUI getCtrl();

		void setBaseCtrl(IUIBaseControl ui, bool clickBack);

		IUIBaseControl getBaseCtrl();

		void dispose();
	}
}
