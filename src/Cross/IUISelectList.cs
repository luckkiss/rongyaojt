using System;

namespace Cross
{
	public interface IUISelectList : IUIScrollPanel, IUIContainer, IUIBaseControl
	{
		Define.UIDirection direction
		{
			get;
			set;
		}

		int selectedIndex
		{
			get;
			set;
		}

		bool selectFindStyle
		{
			get;
			set;
		}

		float scrollDrag
		{
			get;
			set;
		}
	}
}
