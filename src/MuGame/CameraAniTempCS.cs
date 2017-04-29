using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	[AddComponentMenu("QSMY/CameraAni")]
	internal class CameraAniTempCS : MonoBehaviour
	{
		public bool lookatUser = false;

		public bool stop = false;

		private Dictionary<string, GameObject> dPreLab = new Dictionary<string, GameObject>();

		public void QSMY_LookAtUser(int b)
		{
			this.lookatUser = (b == 1);
		}

		public void QSMY_SetTimeScale(int f)
		{
			Globle.setTimeScale((float)f / 100f);
		}

		public void QSMY_STOP(int delta)
		{
			this.stop = true;
			GameCameraMgr.getInstance().stop();
		}

		public void QSMY_OPEN_WIN(string id)
		{
			InterfaceMgr.getInstance().closeAllWin("");
			InterfaceMgr.getInstance().open(id, null, false);
		}

		public void QSMY_OPEN_PRELAB_id_path(string parm)
		{
			string[] array = parm.Split(new char[]
			{
				','
			});
			bool flag = array.Length != 2;
			if (!flag)
			{
				GameObject gameObject = U3DAPI.U3DResLoad<GameObject>(array[1]);
				bool flag2 = gameObject == null;
				if (!flag2)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
					gameObject2.transform.SetParent(InterfaceMgr.getInstance().winLayer, false);
					InterfaceMgr.setUntouchable(gameObject2);
					this.dPreLab[array[0]] = gameObject2;
				}
			}
		}

		public void QSMY_CLOSE_PRELAB_id(string id)
		{
			bool flag = this.dPreLab.ContainsKey(id);
			if (flag)
			{
				UnityEngine.Object.Destroy(this.dPreLab[id]);
				this.dPreLab.Remove(id);
			}
		}

		public void clearAllPrelab()
		{
			foreach (GameObject current in this.dPreLab.Values)
			{
				UnityEngine.Object.Destroy(current);
			}
			this.dPreLab.Clear();
		}
	}
}
