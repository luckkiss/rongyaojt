using System;

namespace Cross
{
	public interface IGraphObject2D : IGraphObject
	{
		string tag
		{
			get;
			set;
		}

		float x
		{
			get;
			set;
		}

		float y
		{
			get;
			set;
		}

		Vec2 pos
		{
			get;
			set;
		}

		float width
		{
			get;
			set;
		}

		float height
		{
			get;
			set;
		}

		float scaleX
		{
			get;
			set;
		}

		float scaleY
		{
			get;
			set;
		}

		float rotation
		{
			get;
			set;
		}

		IContainer2D parent
		{
			get;
		}

		bool visible
		{
			get;
			set;
		}

		float alpha
		{
			get;
			set;
		}

		bool cacheAsBitmap
		{
			get;
			set;
		}

		Style2D style
		{
			get;
			set;
		}

		bool flip
		{
			get;
			set;
		}

		IAssetBitmap mask
		{
			get;
			set;
		}

		Rect clipRect
		{
			get;
			set;
		}

		bool mouseEnable
		{
			get;
			set;
		}

		bool dragEnable
		{
			get;
			set;
		}

		bool spriteGray
		{
			get;
			set;
		}

		void dispose(bool remove);

		void addEventListener(Define.EventType eventType, Action<Event> cbFunc);

		void removeEventListener(Define.EventType eventType, Action<Event> cbFunc);

		void clearAllEventListeners();

		Vec2 globalToLocal(Vec2 pt);

		Vec2 localToGlobal(Vec2 pt);
	}
}
