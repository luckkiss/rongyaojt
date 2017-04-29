using System;
using UnityEngine;

public class AssetResInfo
{
	public ResourceRequest m_ResReq;

	public Vector3 m_position;

	public Quaternion m_rotation;

	public float m_time;

	public void Instantiate(string path, Vector3 position, Quaternion rotation, float time)
	{
		bool flag = this.m_ResReq != null && this.m_ResReq.isDone;
		if (flag)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.m_ResReq.asset, position, rotation) as GameObject;
			bool flag2 = SceneCamera.m_nSkillEff_Level > 1;
			if (flag2)
			{
				Transform transform = gameObject.transform.FindChild("hide");
				bool flag3 = transform != null;
				if (flag3)
				{
					transform.gameObject.SetActive(false);
				}
			}
			gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
			UnityEngine.Object.Destroy(gameObject, time);
		}
		else
		{
			this.m_position = position;
			this.m_rotation = rotation;
			this.m_time = time;
		}
	}

	public void LoadAsync(string path)
	{
		bool flag = this.m_ResReq == null;
		if (flag)
		{
			this.m_ResReq = Resources.LoadAsync(path);
		}
	}

	public bool CheckFirstLoad()
	{
		bool isDone = this.m_ResReq.isDone;
		bool result;
		if (isDone)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(this.m_ResReq.asset, this.m_position, this.m_rotation) as GameObject;
			gameObject.transform.SetParent(U3DAPI.FX_POOL_TF, false);
			UnityEngine.Object.Destroy(gameObject, this.m_time);
			result = true;
		}
		else
		{
			result = false;
		}
		return result;
	}
}
