using System;

namespace Cross
{
	public interface IEffectParticles : IEffect, IGraphObject3D, IGraphObject
	{
		IAssetParticles asset
		{
			get;
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

		void play(float speed = 1f);

		void stop();

		void addEventListener(float time, Action finFun);

		void removeEventListener(float time);
	}
}
