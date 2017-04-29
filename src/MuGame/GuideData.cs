using Cross;
using System;

namespace MuGame
{
	internal class GuideData
	{
		public Variant conf;

		public Variant currStep;

		public int nextStep = 0;

		public IGuideUI ui;

		public string desc;

		public Rect target;

		public Variant userdata;

		public Action stopFun;
	}
}
