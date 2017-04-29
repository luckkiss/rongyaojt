using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cross
{
	public interface ISkAniMesh : IAniMesh, IGraphObject3D, IGraphObject
	{
		IAssetSkAniMesh asset
		{
			get;
			set;
		}

		Dictionary<string, IAssetSkAnimation>.KeyCollection animKeys
		{
			get;
		}

		int numAnims
		{
			get;
		}

		IAssetSkAnimation curAnim
		{
			get;
		}

		string curAnimName
		{
			get;
		}

		bool enableLighting
		{
			get;
			set;
		}

		bool IsRunningAnim();

		void stateact_Change(QS_ACT_STATE sta);

		void action_Play(string aniName, int loop);

		void action_Speed(float s);

		void setDefaultFirstAnim(string aniName);

		void setActActionOverCB(Func<bool> callback);

		void setMtlColor(int nameid, Color color);

		void setMtlFloat(int nameid, float fdata);

		void setMtlInt(int nameid, int ndata);

		bool changeMtl(Material ml);

		void pushAnim(string animName, string animPath);

		void addAnim(string animName, IAssetSkAnimation anim);

		void removeAnim(IAssetSkAnimation anim);

		void removeAnim(string animName);

		void clearAnim();

		IAssetSkAnimation getAnim(string animName);

		void attachObject(string sBoneName, IGraphObject3D obj);

		void detachObject(string sBoneName, IGraphObject3D obj);

		void attachSkin(IAssetSkAniMesh asset, string obj, IShader mtrl = null);

		void detachSkin(IAssetSkAniMesh asset, string obj);

		void play(string animName, bool isLoop, float speed = 1f);

		void stop();

		void pause();

		void resume();

		void activateSubMesh(string sub, bool active);

		void setMaterial(string sub, IShader mtrl);

		void addEventListener(string name, float time, Action<string, float> finFun);

		void removeEventListener(string name, float time);
	}
}
