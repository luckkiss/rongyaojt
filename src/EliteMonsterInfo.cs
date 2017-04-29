using MuGame;
using System;
using System.Collections.Generic;
using UnityEngine;

internal class EliteMonsterInfo
{
	public static Dictionary<uint, List<uint>> poolItemReward = new Dictionary<uint, List<uint>>();

	public int mapId;

	public uint lastKilledTime;

	public uint unRespawnTime;

	public string date;

	public string killerName;

	public Vector2 pos;

	public uint? lv;

	public uint? upLv;

	public List<uint> rewardItem;

	private uint monId;

	public uint MonId
	{
		get
		{
			return this.monId;
		}
		set
		{
			this.monId = value;
			bool flag = !this.lv.HasValue || !this.upLv.HasValue;
			if (flag)
			{
				this.lv = new uint?(XMLMgr.instance.GetSXML("monsters.monsters", "id==" + this.monId).getUint("lv"));
				this.upLv = new uint?(XMLMgr.instance.GetSXML("monsters.monsters", "id==" + this.monId).getUint("zhuan"));
			}
		}
	}

	public EliteMonsterInfo(uint lastKilledDate, uint respawnTime, string killerName, int mapId, Vector2 pos, uint monId)
	{
		this.lastKilledTime = lastKilledDate;
		this.date = this.GetDateBySec(lastKilledDate);
		this.unRespawnTime = respawnTime;
		this.killerName = killerName;
		this.mapId = mapId;
		this.pos = pos;
		this.MonId = monId;
	}

	private string GetDateBySec(uint sec)
	{
		bool flag = sec == 0u;
		string result;
		if (flag)
		{
			result = null;
		}
		else
		{
			result = new DateTime(1970, 1, 1, 8, 0, 0).AddSeconds(sec).ToString("yyyy-MM-dd HH:mm");
		}
		return result;
	}
}
