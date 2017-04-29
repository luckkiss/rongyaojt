using System;

namespace Cross
{
	public interface IAudio : IMediaObject
	{
		IAssetAudio asset
		{
			get;
			set;
		}

		float volume
		{
			get;
			set;
		}

		bool loop
		{
			get;
			set;
		}

		void play();

		void stop();

		void pause();
	}
}
