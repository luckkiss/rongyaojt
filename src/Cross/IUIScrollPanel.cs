using System;

namespace Cross
{
	public interface IUIScrollPanel : IUIContainer, IUIBaseControl
	{
		float tileWidth
		{
			get;
			set;
		}

		float tileHeight
		{
			get;
			set;
		}

		int rowCount
		{
			get;
			set;
		}

		int columnCount
		{
			get;
			set;
		}

		float speed
		{
			get;
			set;
		}

		IUIBaseControl UiMouseSelect
		{
			get;
		}

		float scrollDrag
		{
			get;
			set;
		}

		bool hideScollbar
		{
			set;
		}

		bool canDragScroll
		{
			set;
		}

		void movement(Define.Movement movement, float max, float min);

		void ClipRect(float x, float y, float w, float h);

		void moveAt(IUIBaseControl ui, int index);
	}
}
