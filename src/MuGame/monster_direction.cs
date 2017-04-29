using Cross;
using GameFramework;
using System;
using UnityEngine;

namespace MuGame
{
	internal class monster_direction : FloatUi
	{
		private RectTransform rect;

		private processStruct process;

		private bool hided = false;

		private LGAvatarGameInst role;

		public override void init()
		{
			this.process = new processStruct(new Action<float>(this.onUpdate), "fb_main", false, false);
			this.rect = base.getComponentByPath<RectTransform>("arrow");
		}

		public override void onShowed()
		{
			InterfaceMgr.setUntouchable(this.rect.gameObject);
			(CrossApp.singleton.getPlugin("processManager") as processManager).addProcess(this.process, false);
			base.onShowed();
		}

		public override void onClosed()
		{
			this.role = null;
			(CrossApp.singleton.getPlugin("processManager") as processManager).removeProcess(this.process, false);
			base.onClosed();
		}

		private void showIt()
		{
			bool flag = !this.hided;
			if (!flag)
			{
				this.hided = false;
				this.rect.anchoredPosition = Vector2.zero;
			}
		}

		private void hideIt()
		{
			bool flag = this.hided;
			if (!flag)
			{
				this.hided = true;
				this.rect.anchoredPosition = new Vector2(65535f, 65535f);
			}
		}

		private void onUpdate(float s)
		{
			bool flag = InterfaceMgr.ui_Camera_cam == null || GRMap.GAME_CAM_CAMERA == null;
			if (!flag)
			{
				bool flag2 = this.role == null || this.role.IsDie();
				if (flag2)
				{
					this.role = LGMonsters.instacne.getNearMon(1000);
				}
				bool flag3 = this.role == null;
				if (flag3)
				{
					this.hideIt();
				}
				else
				{
					try
					{
						bool flag4 = this.role.pGameobject == null;
						if (flag4)
						{
							this.role = null;
							this.hideIt();
							return;
						}
					}
					catch (Exception var_6_9D)
					{
						this.role = null;
						this.hideIt();
						return;
					}
					LGAvatarGameInst lGAvatarGameInst = this.role;
					float distance = this.getDistance(lGAvatarGameInst);
					bool flag5 = lGAvatarGameInst != null && !lGAvatarGameInst.IsDie() && distance > 300f;
					if (flag5)
					{
						this.showIt();
						this.rotationToPt(lGAvatarGameInst);
					}
					else
					{
						this.hideIt();
					}
				}
			}
		}

		public float getDistance(LGAvatarGameInst avatar)
		{
			return Math.Abs(lgSelfPlayer.instance.x - avatar.x) + Math.Abs(lgSelfPlayer.instance.y - avatar.y);
		}

		public void rotationToPt(LGAvatarGameInst av)
		{
			bool flag = av == null;
			if (!flag)
			{
				Vector3 vector = GRMap.GAME_CAM_CAMERA.WorldToScreenPoint(av.gameObj.transform.position);
				Vector3 vector2 = GRMap.GAME_CAM_CAMERA.WorldToScreenPoint(lgSelfPlayer.instance.pGameobject.transform.position);
				float z = (float)(Math.Atan2((double)(vector.y - vector2.y), (double)(vector.x - vector2.x)) * 57.295779513082323) - 90f;
				this.rect.eulerAngles = new Vector3(0f, 0f, z);
			}
		}
	}
}
