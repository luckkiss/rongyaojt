using Cross;
using System;
using UnityEngine;

namespace MuGame
{
	internal class MJJDProxy : BaseProxy<MJJDProxy>
	{
		public void sendMsgtoServer(int id, int param = 0)
		{
			Debug.Log("Send Message_244 , type == " + id);
			Variant variant = new Variant();
			variant["operation"] = id;
			this.sendRPC(244u, variant);
		}
	}
}
