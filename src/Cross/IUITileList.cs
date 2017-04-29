using System;

namespace Cross
{
	public interface IUITileList : IUIContainer, IUIBaseControl
	{
		Define.UIDirection direction
		{
			get;
			set;
		}

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

		void ParentMove(float x, float y, float speed);

		void AddItem(Variant prop);

		void AddItem(IUIBaseControl uibase);

		void ReMoveItem(params int[] indexs);

		void ReMoveItem(string id);

		void ReMoveItem(IUIBaseControl uibase);
	}
}
