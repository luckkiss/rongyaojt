using System;

namespace Cross
{
	public interface IAniBitmap : IGraphObject2D, IGraphObject
	{
		IAssetAniBitmap asset
		{
			get;
			set;
		}

		float rate
		{
			set;
		}

		bool loop
		{
			set;
		}

		int frame
		{
			get;
			set;
		}

		int numFrames
		{
			get;
		}

		void play();

		void stop();

		void playAt(int idx);

		void addFrameEvent(uint frame, Action<int> Function);

		void clearFrameEvent();
	}
}
