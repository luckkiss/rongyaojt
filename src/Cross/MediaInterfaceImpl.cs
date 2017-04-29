using System;
using UnityEngine;

namespace Cross
{
	public class MediaInterfaceImpl : IMediaInterface
	{
		public MediaInterfaceImpl()
		{
			osImpl.singleton.mainU3DObj.AddComponent<AudioListener>();
		}

		public IAudio createAudio()
		{
			return new AudioImpl();
		}
	}
}
