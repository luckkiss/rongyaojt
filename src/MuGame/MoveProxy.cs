using Cross;
using System;
using UnityEngine;

namespace MuGame
{
	internal class MoveProxy : BaseProxy<MoveProxy>
	{
		private uint m_unLastReqX = 0u;

		private uint m_unLastReqY = 0u;

		private uint m_unLastReqToX = 0u;

		private uint m_unLastReqToY = 0u;

		private uint m_unLastReqTM = 0u;

		private uint m_unLastReqCount = 0u;

		private uint m_unReqMoreCount = 0u;

		private uint m_unSendMoveMsgCount = 6u;

		private float m_unLastReqRadian = 0f;

		private float m_fSyncPosTime = 0f;

		private bool m_bFirst = true;

		private float m_fLastSendTime;

		private uint m_unLastSendX = 0u;

		private uint m_unLastSendY = 0u;

		private uint m_unLastSendToX = 0u;

		private uint m_unLastSendToY = 0u;

		public MoveProxy()
		{
			this.addProxyListener(8u, new Action<Variant>(this.pos_correct));
			this.addProxyListener(9u, new Action<Variant>(this.move));
			this.addProxyListener(10u, new Action<Variant>(this.on_stop));
		}

		private void ReqChangeMoveMsg(uint x, uint y, uint tox, uint toy, uint tm, float radian = -1f, bool force = false)
		{
			this.m_unLastReqX = x;
			this.m_unLastReqY = y;
			this.m_unLastReqToX = tox;
			this.m_unLastReqToY = toy;
			this.m_unLastReqTM = tm;
			this.m_unLastReqCount = 0u;
			bool flag = radian != -1f;
			if (flag)
			{
				this.m_unLastReqRadian = radian;
			}
		}

		public void resetFirstMove()
		{
			this.m_bFirst = true;
		}

		public void TrySyncPos(float dt)
		{
			bool flag = this.m_fSyncPosTime < 0f;
			if (flag)
			{
				bool bFirst = this.m_bFirst;
				if (bFirst)
				{
					bool flag2 = this.m_unLastSendX != this.m_unLastReqX || this.m_unLastSendY != this.m_unLastReqY || this.m_unLastSendToX != this.m_unLastReqToX || this.m_unLastSendToY != this.m_unLastReqToY;
					if (flag2)
					{
						this.m_unLastSendX = this.m_unLastReqX;
						this.m_unLastSendY = this.m_unLastReqY;
						this.m_unLastSendToX = this.m_unLastReqToX;
						this.m_unLastSendToY = this.m_unLastReqToY;
						this.m_bFirst = false;
					}
				}
				bool flag3 = this.m_unLastSendX != this.m_unLastReqX || this.m_unLastSendY != this.m_unLastReqY || this.m_unLastSendToX != this.m_unLastReqToX || this.m_unLastSendToY != this.m_unLastReqToY;
				if (flag3)
				{
					this.m_unLastSendX = this.m_unLastReqX;
					this.m_unLastSendY = this.m_unLastReqY;
					this.m_unLastSendToX = this.m_unLastReqToX;
					this.m_unLastSendToY = this.m_unLastReqToY;
					Variant variant = Variant.alloc();
					variant["frm_x"] = this.m_unLastSendX;
					variant["frm_y"] = this.m_unLastSendY;
					variant["to_x"] = this.m_unLastSendToX;
					variant["to_y"] = this.m_unLastSendToY;
					variant["start_tm"] = this.m_unLastReqTM;
					variant["radian"] = this.m_unLastReqRadian;
					this.sendRPC(9u, variant);
					Variant.free(variant);
					this.m_fSyncPosTime = 0.5f;
				}
			}
			else
			{
				this.m_fSyncPosTime -= dt;
			}
		}

		public void sendVec()
		{
			bool flag = !SelfRole._inst.isDead;
			if (flag)
			{
				this.m_unLastSendX = 0u;
				this.m_unLastSendY = 0u;
				this.m_unLastSendToX = 1u;
				this.m_unLastSendToY = 1u;
				Variant variant = Variant.alloc();
				variant["frm_x"] = this.m_unLastSendX;
				variant["frm_y"] = this.m_unLastSendY;
				variant["to_x"] = this.m_unLastSendToX;
				variant["to_y"] = this.m_unLastSendToY;
				variant["start_tm"] = this.m_unLastReqTM;
				variant["radian"] = this.m_unLastReqRadian;
				this.sendRPC(9u, variant);
				Variant.free(variant);
			}
		}

		public bool SendMoveMsgToServer(Vector3 curPos, Vector3 tarPos)
		{
			return false;
		}

		private bool SendMoveMsgToServer(uint x, uint y, uint tox, uint toy, uint tm, float radian)
		{
			bool flag = (float)muNetCleint.instance.CurServerTimeStamp - this.m_fLastSendTime < 0.5f;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.m_unLastSendX != x || this.m_unLastSendY != y || this.m_unLastSendToX != tox || this.m_unLastSendToY != toy;
				if (flag2)
				{
					this.m_unLastSendX = x;
					this.m_unLastSendY = y;
					this.m_unLastSendToX = tox;
					this.m_unLastSendToY = toy;
					Variant variant = Variant.alloc();
					variant["frm_x"] = this.m_unLastSendX;
					variant["frm_y"] = this.m_unLastSendY;
					variant["to_x"] = this.m_unLastSendToX;
					variant["to_y"] = this.m_unLastSendToY;
					variant["start_tm"] = tm;
					variant["radian"] = radian;
					this.sendRPC(9u, variant);
					this.m_fLastSendTime = (float)muNetCleint.instance.CurServerTimeStamp;
					this.m_fSyncPosTime = 0.5f;
					Variant.free(variant);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		public void sendstop(uint x, uint y, uint face, float tm, bool force = false)
		{
			this.ReqChangeMoveMsg(x, y, x, y, (uint)tm, -1f, force);
		}

		public void pos_correct(Variant msgData)
		{
			debug.Log("KKKKUUUUU" + msgData.dump());
			uint @uint = msgData["iid"]._uint;
			ProfessionRole otherPlayer = OtherPlayerMgr._inst.GetOtherPlayer(@uint);
			bool flag = otherPlayer != null;
			if (!flag)
			{
				float x = msgData["x"]._float / 53.333f;
				float z = msgData["y"]._float / 53.333f;
				MonsterRole serverMonster = MonsterMgr._inst.getServerMonster(@uint);
				bool flag2 = serverMonster != null;
				if (flag2)
				{
					Vector3 vector = new Vector3(x, serverMonster.m_curModel.position.y, z);
					bool flag3 = GameRoomMgr.getInstance().curRoom == GameRoomMgr.getInstance().dRooms[3342u];
					if (flag3)
					{
						serverMonster.pos_correct(vector);
					}
					else
					{
						NavMeshHit navMeshHit;
						NavMesh.SamplePosition(vector, out navMeshHit, 100f, serverMonster.m_layer);
						serverMonster.pos_correct(navMeshHit.position);
					}
				}
			}
		}

		public void move(Variant msgData)
		{
			uint @uint = msgData["iid"]._uint;
			ProfessionRole otherPlayer = OtherPlayerMgr._inst.GetOtherPlayer(@uint);
			bool flag = otherPlayer != null;
			if (flag)
			{
				float x = msgData["to_x"]._float / 53.333f;
				float z = msgData["to_y"]._float / 53.333f;
				Vector3 vector = new Vector3(x, otherPlayer.m_curModel.position.y, z);
				bool flag2 = GameRoomMgr.getInstance().curRoom == GameRoomMgr.getInstance().dRooms[3342u];
				if (flag2)
				{
					otherPlayer.SetDestPos(vector);
				}
				else
				{
					NavMeshHit navMeshHit;
					NavMesh.SamplePosition(vector, out navMeshHit, 100f, otherPlayer.m_layer);
					otherPlayer.SetDestPos(navMeshHit.position);
				}
			}
			else
			{
				float x2 = msgData["to_x"]._float / 53.333f;
				float z2 = msgData["to_y"]._float / 53.333f;
				MonsterRole serverMonster = MonsterMgr._inst.getServerMonster(@uint);
				bool flag3 = serverMonster != null;
				if (flag3)
				{
					Vector3 vector2 = new Vector3(x2, serverMonster.m_curModel.position.y, z2);
					bool flag4 = GameRoomMgr.getInstance().curRoom == GameRoomMgr.getInstance().dRooms[3342u];
					if (flag4)
					{
						serverMonster.SetDestPos(vector2);
					}
					else
					{
						NavMeshHit navMeshHit2;
						NavMesh.SamplePosition(vector2, out navMeshHit2, 100f, serverMonster.m_layer);
						serverMonster.SetDestPos(navMeshHit2.position);
					}
				}
			}
		}

		public void on_stop(Variant msgData)
		{
		}

		public Vector3 GetLastSendXY()
		{
			return new Vector3(this.m_unLastSendToX, 0f, this.m_unLastSendToY);
		}
	}
}
