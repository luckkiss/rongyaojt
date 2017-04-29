using System;

namespace Cross
{
	public interface IGraphInterface
	{
		IGraphScene3D defaultScene3D
		{
			get;
			set;
		}

		IGraphScene2D createScene2D();

		void deleteScene2D(IGraphScene2D scene);

		IGraphScene3D createScene3D();

		void deleteScene3D(IGraphScene3D scene);

		IGraphScene3D getScene3D(string name);

		IShader createShader();

		void setCursorStyle(string styleName, bool keepStyle = false);

		void onResize(int width, int height);
	}
}
