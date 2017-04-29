using Cross;
using System;
using System.Collections.Generic;

namespace GameFramework
{
	public struct paramStruct
	{
		public Event _evt;

		public string _str;

		public IUIBaseControl _baseControl;

		public Variant _var;

		public Action _act;

		public Action<Variant> _act_Variant;

		public Action<Variant> fun;

		public List<UIbase> _tiles;

		public UIbase _tile;

		public List<IUIBaseControl> _baseControls;

		public int _value;

		public IUIBaseControl ui;

		public Variant misid;
	}
}
