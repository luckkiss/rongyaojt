using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("COMM_COMPONENT/NPC")]
public class NpcSC : MonoBehaviour
{
	public string rolename = "路人";

	public string openID = string.Empty;

	public List<string> desc = new List<string>
	{
		"0:你好！",
		"1:你好！"
	};

	public Vector3 offset = new Vector3(-131.4f, -4.25f, -128f);

	public Vector3 scale = new Vector3(2.5f, 2.5f, 2.5f);

	public bool navmesh = true;

	private void Start()
	{
		NpcRole npcRole = base.gameObject.AddComponent<NpcRole>();
		npcRole.openid = this.openID;
		npcRole.lDesc = this.desc;
		npcRole.id = int.Parse(base.gameObject.name);
		npcRole.name = this.rolename;
		npcRole.talkOffset = this.offset;
		npcRole.talkScale = this.scale;
		npcRole.nav = this.navmesh;
		UnityEngine.Object.Destroy(this);
	}
}
