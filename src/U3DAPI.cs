using System;
using UnityEngine;

public static class U3DAPI
{
	public static GameObject DEF_GAMEOBJ = new GameObject("DEF_GAMEOBJ");

	public static Transform DEF_TRANSFORM = U3DAPI.DEF_GAMEOBJ.transform;

	public static Animator DEF_ANIMATOR = new Animator();

	public static SkinnedMeshRenderer DEF_SKINNEDMESHRENDERER;

	public static GameObject FX_POOL_OBJ = new GameObject("FX_POOL");

	public static Transform FX_POOL_TF = U3DAPI.FX_POOL_OBJ.transform;

	private static bool s_functionbar_mode = false;

	private static LightProbes s_curLightPro = null;

	private static Color s_curAmLight;

	public static void Init_DEFAULT()
	{
		U3DAPI.DEF_GAMEOBJ.name = "DEF_GAMEOBJ";
		GameObject gameObject = Resources.Load<GameObject>("default/skinned_mesh_renderer");
		U3DAPI.DEF_SKINNEDMESHRENDERER = gameObject.GetComponent<SkinnedMeshRenderer>();
	}

	public static void functionbar_ChangeTo()
	{
		bool flag = !U3DAPI.s_functionbar_mode;
		if (flag)
		{
			U3DAPI.s_functionbar_mode = true;
			U3DAPI.s_curAmLight = RenderSettings.ambientLight;
			RenderSettings.ambientLight = new Color(0f, 0f, 0f);
		}
	}

	public static void functionbar_BackFrom()
	{
		bool flag = U3DAPI.s_functionbar_mode;
		if (flag)
		{
			U3DAPI.s_functionbar_mode = false;
			RenderSettings.ambientLight = U3DAPI.s_curAmLight;
		}
	}

	public static T U3DResLoad<T>(string path) where T : UnityEngine.Object
	{
		return Resources.Load<T>(path);
	}
}
