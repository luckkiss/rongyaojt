using System;

namespace Cross
{
	public interface IGREffectParticles : IGREntity
	{
		IAssetParticles asset
		{
			set;
		}

		float duration
		{
			get;
			set;
		}

		bool loop
		{
			get;
			set;
		}

		bool isPlaying
		{
			get;
		}

		void play();

		void stop();

		void addEventListener(float time, Action finFun);

		void removeEventListener(float time);
	}
}
