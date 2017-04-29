using System;

namespace Cross
{
	public interface IContainer2D : IGraphObject2D, IGraphObject
	{
		int numChildren
		{
			get;
		}

		bool mouseChildren
		{
			get;
			set;
		}

		void addChild(IGraphObject2D child);

		void addChildAt(IGraphObject2D child, int idx);

		void removeChild(IGraphObject2D child);

		void removeChildAt(int idx);

		IGraphObject2D getChildAt(int idx);

		int indexOf(IGraphObject2D child);

		bool contains(IGraphObject2D obj);
	}
}
