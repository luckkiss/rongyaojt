using System;
using System.Collections;
using UnityEngine;

namespace MuGame
{
	public class TransmitData
	{
		public Action after_clickBtnWalk;

		public Action after_clickBtnTransmit;

		public Action handle_customized_afterTransmit;

		public Action after_arrive;

		public bool check_beforeShow;

		public Vector3 targetPosition;

		public string[] closeWinName;

		private int _mapId;

		private int _mappointId;

		public int mappointId
		{
			get
			{
				return this._mappointId;
			}
		}

		public int mapId
		{
			get
			{
				return this._mapId;
			}
			set
			{
				int arg_10_0 = 1;
				int arg_0F_0 = 100;
				this._mapId = value;
				this._mappointId = arg_10_0 + arg_0F_0 * value;
			}
		}

		public static explicit operator ArrayList(TransmitData data)
		{
			return new ArrayList
			{
				data
			};
		}
	}
}
