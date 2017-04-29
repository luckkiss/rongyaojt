using System;

namespace Cross
{
	public interface IUIContainer : IUIBaseControl
	{
		int numChildren
		{
			get;
		}

		void addChild(IUIBaseControl ui);

		void removeChild(IUIBaseControl ui, bool dis = false);

		void removeChild(string id, bool dis = false);

		void clearChildren();

		IUIBaseControl getChild(int index);

		IUIBaseControl getChild(string id);

		int indexOf(IUIBaseControl child);

		void disposeHirachy();
	}
}
