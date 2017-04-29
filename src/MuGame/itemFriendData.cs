using System;
using UnityEngine;

namespace MuGame
{
	internal struct itemFriendData
	{
		public uint cid;

		public string name;

		public uint carr;

		public int lvl;

		public uint zhuan;

		public string clan_name;

		public bool online;

		public int map_id;

		public uint llid;

		public string team;

		public uint combpt;

		public uint hatred;

		public uint kill_tm;

		public string pos;

		public bool isNew;

		public float timer;

		public int mlzd_lv;

		public Transform root;

		public itemFriendPrefab itemFPrefab;

		public itemBlackListPrefab itemBListPrefab;

		public itemNearbyListPrefab itemNListPrefab;

		public itemEnemyListPrefab itemEListPrefab;

		public itemRecommendListPrefab itemECListPrefab;
	}
}
