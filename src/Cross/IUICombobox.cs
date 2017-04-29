using System;

namespace Cross
{
	public interface IUICombobox : IUIBaseControl
	{
		uint rowCount
		{
			get;
			set;
		}

		int selectedIndex
		{
			get;
			set;
		}

		IUIBaseControl selectedItem
		{
			get;
			set;
		}

		void addItem(Variant conf);

		void addChild(IUIBaseControl child);

		void removeChild(IUIBaseControl child, bool dis);

		void removeAllItems();
	}
}
