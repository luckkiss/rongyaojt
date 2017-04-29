using System;

namespace Cross
{
	public interface IContainer3D : IGraphObject3D, IGraphObject
	{
		int numChildren
		{
			get;
		}

		void addChild(IGraphObject3D child);

		void addChildAt(IGraphObject3D child, int idx);

		void removeChild(IGraphObject3D child);

		void removeChildAt(int idx);

		IGraphObject3D getChildAt(int idx);

		int indexOf(IGraphObject3D child);

		bool contains(IGraphObject3D obj);
	}
}
