using Cross;
using DG.Tweening;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	public class LGCamera : lgGDBase, IObjectPlugin
	{
		public static LGCamera instance;

		public LGCamera(gameManager m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new LGCamera(m as gameManager);
		}

		public override void init()
		{
			LGCamera.instance = this;
			this.g_mgr.g_gameM.addEventListenerCL("LG_JOIN_WORLD", 3034u, new Action<GameEvent>(this.onJoinWorld));
		}

		private void onJoinWorld(GameEvent e)
		{
			this.g_mgr.g_sceneM.dispatchEvent(GameEvent.Createimmedi(2180u, this, null, false));
			base.dispatchEvent(GameEvent.Createimmedi(2170u, this, null, false));
		}

		public void updateMainPlayerPos(bool force = false)
		{
		}

		public void updateMainPlayerPos(float x, float y, float z)
		{
			bool flag = GRMap.GAME_CAMERA == null;
			if (!flag)
			{
				Vector3 vector = new Vector3(x, y, z);
				Transform transform = GRMap.GAME_CAMERA.transform;
				bool flag2 = Math.Abs(x - transform.position.x) > 15f || Math.Abs(z - transform.position.z) > 15f;
				if (flag2)
				{
					transform.position = vector;
				}
				else
				{
					transform.DOMove(vector, 0.3f, false);
				}
			}
		}

		public void obj_mask(Vec3 chapos)
		{
			base.dispatchEvent(GameEvent.Createimmedi(2226u, this, chapos, true));
		}
	}
}
