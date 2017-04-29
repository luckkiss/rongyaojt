using System;
using UnityEngine;

public static class SceneTFX
{
	public static GameObject[] m_TFX_Prefabs = new GameObject[10];

	public static GameObject[] m_Bullet_Prefabs = new GameObject[10];

	public static GameObject[] m_HFX_Prefabs = new GameObject[10];

	public static void InitScene()
	{
		for (int i = 0; i < 10; i++)
		{
			SceneTFX.m_TFX_Prefabs[i] = null;
			GameObject gameObject = Resources.Load<GameObject>("FX/TFX_" + i.ToString());
			bool flag = gameObject != null;
			if (flag)
			{
				SceneTFX.m_TFX_Prefabs[i] = gameObject;
			}
			else
			{
				SceneTFX.m_TFX_Prefabs[i] = U3DAPI.DEF_GAMEOBJ;
			}
			SceneTFX.m_Bullet_Prefabs[i] = null;
			GameObject gameObject2 = Resources.Load<GameObject>("bullet/b" + i.ToString());
			bool flag2 = gameObject2 != null;
			if (flag2)
			{
				SceneTFX.m_Bullet_Prefabs[i] = gameObject2;
			}
			else
			{
				SceneTFX.m_Bullet_Prefabs[i] = U3DAPI.DEF_GAMEOBJ;
			}
			SceneTFX.m_HFX_Prefabs[i] = null;
			GameObject gameObject3 = Resources.Load<GameObject>("FX/HFX/HFX_" + i.ToString());
			bool flag3 = gameObject3 != null;
			if (flag3)
			{
				SceneTFX.m_HFX_Prefabs[i] = gameObject3;
			}
			else
			{
				SceneTFX.m_HFX_Prefabs[i] = U3DAPI.DEF_GAMEOBJ;
			}
		}
	}
}
