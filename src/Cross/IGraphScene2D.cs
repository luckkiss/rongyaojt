using System;

namespace Cross
{
	public interface IGraphScene2D : IGraphScene
	{
		string tag
		{
			get;
			set;
		}

		int layer
		{
			get;
			set;
		}

		bool visible
		{
			get;
			set;
		}

		IContainer2D root
		{
			get;
		}

		IContainer2D createContainer2D();

		IBitmap createBitmap();

		IAniBitmap createAniBitmap();

		ITextInput createTextInput();

		IText createText();

		IRenderTexture createRenderTexture();

		void deleteObject2D(string id);

		void deleteObject2D(IGraphObject2D object2D);
	}
}
