using System;
using UnityEngine;

public class GameAniTrrigerCamera : MonoBehaviour
{
	public void onEnd()
	{
		SceneCamera.ResetAfterTrrigerCam();
	}
}
