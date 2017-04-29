using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFXMgr : MonoBehaviour
{
	private static Dictionary<string, AssetResInfo> m_mapAssetsRes = new Dictionary<string, AssetResInfo>();

	private static Dictionary<string, AssetResInfo> m_mapLoadingRes = new Dictionary<string, AssetResInfo>();

	private static string m_bAsync_Key = null;

	public static void Instantiate(string path, Vector3 position, Quaternion rotation, float time)
	{
		bool flag = !SceneFXMgr.m_mapAssetsRes.ContainsKey(path);
		if (flag)
		{
			AssetResInfo value = new AssetResInfo();
			SceneFXMgr.m_mapAssetsRes[path] = value;
			SceneFXMgr.m_mapLoadingRes[path] = value;
		}
		SceneFXMgr.m_mapAssetsRes[path].Instantiate(path, position, rotation, time);
	}

	private IEnumerator Async_Load()
	{
		SceneFXMgr.m_mapAssetsRes[SceneFXMgr.m_bAsync_Key].LoadAsync(SceneFXMgr.m_bAsync_Key);
		yield return SceneFXMgr.m_mapAssetsRes[SceneFXMgr.m_bAsync_Key].m_ResReq;
		SceneFXMgr.m_mapAssetsRes[SceneFXMgr.m_bAsync_Key].CheckFirstLoad();
		SceneFXMgr.m_mapLoadingRes.Remove(SceneFXMgr.m_bAsync_Key);
		SceneFXMgr.m_bAsync_Key = null;
		yield return null;
		yield break;
	}

	private void Update()
	{
		bool flag = SceneFXMgr.m_bAsync_Key == null;
		if (flag)
		{
			using (Dictionary<string, AssetResInfo>.KeyCollection.Enumerator enumerator = SceneFXMgr.m_mapLoadingRes.Keys.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					SceneFXMgr.m_bAsync_Key = current;
					base.StartCoroutine("Async_Load");
				}
			}
		}
	}
}
