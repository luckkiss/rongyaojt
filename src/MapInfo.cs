using System;
using UnityEngine;

[AddComponentMenu("服务器地图参数/地图信息（每张地图只能有一个）")]
public class MapInfo : MonoBehaviour
{
	public int 地图id;

	public string 地图名 = "--";

	public bool 是否是pk地图;

	private void Start()
	{
	}
}
