using System;
using UnityEngine;

public class GameAniLoginCamera : MonoBehaviour
{
	public void onLoginEnd()
	{
		SceneCamera.ResetAfterLoginCam();
	}
}
