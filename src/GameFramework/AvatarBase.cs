using Cross;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
	public class AvatarBase
	{
		public GameObject _avatar;

		public Animation m_animator;

		private string m_strCurPlayAnim;

		private GameObject con;

		public bool disposed = false;

		protected Dictionary<string, string> m_animPath = new Dictionary<string, string>();

		public AvatarBase(string id)
		{
			this.con = new GameObject();
			Variant conf = GraphManager.singleton.getCharacterConf(id);
			IAsset asset = os.asset.getAsset<IAssetMesh>(conf["file"]._str, delegate(IAsset ast)
			{
				bool flag = this.disposed;
				if (!flag)
				{
					GameObject assetObj = (ast as AssetMeshImpl).assetObj;
					this._avatar = UnityEngine.Object.Instantiate<GameObject>(assetObj);
					this.m_animator = this._avatar.GetComponent<Animation>();
					bool flag2 = this.m_animator == null;
					if (flag2)
					{
						this.m_animator = this._avatar.AddComponent<Animation>();
					}
					this._avatar.transform.SetParent(this.con.transform, false);
					bool flag3 = conf.ContainsKey("ani");
					if (flag3)
					{
						foreach (Variant current in conf["ani"].Values)
						{
							string text = null;
							bool flag4 = current.ContainsKey("name");
							if (flag4)
							{
								text = current["name"]._str;
							}
							bool flag5 = text == null || !current.ContainsKey("file");
							if (flag5)
							{
								Debug.LogError("表错误" + text);
							}
							bool flag6 = text != null;
							if (flag6)
							{
								this.m_animPath[text] = current["file"];
							}
						}
						this.playAni("idle");
					}
				}
			}, null, delegate(IAsset ast, string err)
			{
				Debug.LogError("加载失败::" + conf["file"]._str);
			});
			(asset as AssetImpl).loadImpl(false);
		}

		public void setParent(Transform trans)
		{
			this.con.transform.SetParent(trans, false);
		}

		public void setActive(bool b)
		{
			this.con.SetActive(b);
		}

		public void playAni(string anim)
		{
			bool flag = this.m_animator == null;
			if (!flag)
			{
				bool flag2 = this.m_animator.GetClip(anim) == null;
				if (flag2)
				{
					bool flag3 = !this.m_animPath.ContainsKey(anim);
					if (!flag3)
					{
						IAsset asset = os.asset.getAsset<IAssetSkAnimation>(this.m_animPath[anim], delegate(IAsset ast)
						{
							bool flag5 = this.disposed;
							if (!flag5)
							{
								AnimationClip anim2 = (ast as AssetSkAnimationImpl).anim;
								this.addAnim(anim, anim2);
								this.m_strCurPlayAnim = anim;
								this.m_animator.Play(anim);
							}
						}, null, delegate(IAsset ast, string err)
						{
							Debug.LogError("加载失败::" + this.m_animPath[anim]);
						});
						(asset as AssetImpl).loadImpl(false);
					}
				}
				else
				{
					bool flag4 = this.m_strCurPlayAnim == anim;
					if (flag4)
					{
						this.m_animator.Play(anim);
					}
					else
					{
						this.m_animator.CrossFade(anim, 0.3f);
						this.m_strCurPlayAnim = anim;
					}
				}
			}
		}

		public void addAnim(string id, AnimationClip clip)
		{
			bool flag = this.m_animator.GetClip(id) != null;
			if (flag)
			{
				this.m_animator.RemoveClip(id);
			}
			this.m_animator.AddClip(clip, id);
		}

		public void dispose()
		{
			this.disposed = true;
			UnityEngine.Object.Destroy(this.con);
		}
	}
}
