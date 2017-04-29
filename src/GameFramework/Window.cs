using System;

namespace GameFramework
{
	public class Window : Baselayer, IBgLayerUI
	{
		public bool OpenAni = false;

		public override bool showBG
		{
			get
			{
				return true;
			}
		}

		public override bool openAni
		{
			get
			{
				return this.OpenAni;
			}
		}
	}
}
