using Cross;
using System;

namespace GameFramework
{
	public interface IMediaClientBase
	{
		bool isPlaySound
		{
			get;
		}

		void PlayMusicUrl(string url, Action finFun = null, bool force = false);

		void PlaySoundUrl(string url, bool loop, Action finFun = null);

		void StopSoundUrls(Variant urlArr);

		void setSoundVolume(float v = 1f);

		void setMusicVolume(float v = 1f);

		void StopMusic();
	}
}
