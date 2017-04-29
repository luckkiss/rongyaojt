using System;

namespace Cross
{
	public interface IUIJoystick : IUIBaseControl
	{
		float range
		{
			get;
			set;
		}
	}
}
