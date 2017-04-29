using System;
using UnityEngine;
using UnityEngine.UI;

namespace MuGame
{
	internal struct Panel4Player
	{
		public Transform transform;

		public Text txt_name;

		public BaseButton btn_see;

		public BaseButton btn_addFriend;

		public BaseButton btn_pinvite;

		public BaseButton btn_privateChat;

		public uint cid;

		public string name;
	}
}
