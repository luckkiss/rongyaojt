using System;

namespace Cross
{
	public interface IUIImageBox : IUIBaseControl
	{
		bool flip
		{
			get;
			set;
		}

		bool dragEnable
		{
			get;
			set;
		}

		bool imgAutoSize
		{
			get;
			set;
		}

		IAniBitmap imageBmp
		{
			get;
		}

		float bmpWidth
		{
			get;
			set;
		}

		float bmpHeight
		{
			get;
			set;
		}

		bool spriteGray
		{
			get;
			set;
		}

		bool anibitmap
		{
			get;
			set;
		}

		void setImgStyle(string v);

		void loadImgFun(Action<IUIImageBox> onfun);
	}
}
