using Cross;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuGame
{
	internal class MapEffMgr
	{
		public static readonly int TYPE_AUTO = 0;

		public static readonly int TYPE_LOOP = 1;

		public float interval = 0.2f;

		public float lifeCycle = 1f;

		private float lastconbinedTime = 0f;

		private Dictionary<string, EffectItem> dEffectItem;

		private Dictionary<string, effData> dLifeTime;

		public static Dictionary<string, GameObject> dGo;

		private Transform con;

		private TickItem process;

		private static MapEffMgr _instacne;

		public MapEffMgr()
		{
			this.dLifeTime = new Dictionary<string, effData>();
			MapEffMgr.dGo = new Dictionary<string, GameObject>();
			this.dEffectItem = new Dictionary<string, EffectItem>();
			this.con = new GameObject
			{
				name = "effects"
			}.transform;
			this.process = new TickItem(new Action<float>(this.onUpdate));
			TickMgr.instance.addTick(this.process);
		}

		private void onUpdate(float s)
		{
			List<EffectItem> list = new List<EffectItem>();
			foreach (EffectItem current in this.dEffectItem.Values)
			{
				current.update(s);
				bool disposed = current._disposed;
				if (disposed)
				{
					list.Add(current);
				}
			}
			foreach (EffectItem current2 in list)
			{
				this.dEffectItem.Remove(current2._id);
			}
		}

		public EffectItem addEffItem(string id, string path, int effType = 0, Transform effcon = null)
		{
			this.removeEffItem(id);
			bool flag = !this.dLifeTime.ContainsKey(path);
			EffectItem result;
			effData effData;
			if (flag)
			{
				SXML sXML = XMLMgr.instance.GetSXML("effect.eff", "id==" + id);
				bool flag2 = sXML == null;
				if (flag2)
				{
					result = null;
					return result;
				}
				effData = default(effData);
				effData.path = sXML.getString("file");
				effData.lifetime = (float)(sXML.getInt("lenth") / 10);
				this.dLifeTime[path] = effData;
			}
			else
			{
				effData = this.dLifeTime[path];
			}
			float len = effData.lifetime;
			bool flag3 = effType == MapEffMgr.TYPE_LOOP;
			if (flag3)
			{
				len = -1f;
			}
			bool flag4 = this.dEffectItem.ContainsKey(id);
			if (flag4)
			{
				this.dEffectItem[id].dispose();
			}
			EffectItem effectItem = new EffectItem(id, effData.path, len);
			this.dEffectItem[id] = effectItem;
			bool flag5 = effcon == null;
			if (flag5)
			{
				effectItem.setParent(this.con);
			}
			else
			{
				effectItem.setParent(effcon);
			}
			result = effectItem;
			return result;
		}

		public void removeEffItem(string id)
		{
			bool flag = this.dEffectItem.ContainsKey(id);
			if (flag)
			{
				EffectItem effectItem = this.dEffectItem[id];
				effectItem.dispose();
				this.dEffectItem.Remove(id);
			}
		}

		public void clearAll()
		{
			foreach (EffectItem current in this.dEffectItem.Values)
			{
				current.dispose();
			}
			this.dEffectItem.Clear();
			for (int i = 0; i < this.con.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.con.GetChild(i).gameObject);
			}
		}

		public void playLineTo(string path, Vector3 begin, Vector3 end, int pointNum, float life = 0f)
		{
			Quaternion qua = Quaternion.LookRotation(end - begin);
			for (int i = 1; i < pointNum; i++)
			{
				Vector3 pos = Vector3.Lerp(begin, end, (float)i / (float)pointNum);
				this.play(path, pos, qua, life, null);
			}
		}

		public void playMoveto(string path, Vector3 begin, Vector3 end, float life = 0f)
		{
			Vector3 lhs = end - begin;
			bool flag = lhs == Vector3.zero;
			if (!flag)
			{
				Quaternion qua = Quaternion.LookRotation(end - begin);
				this.play(path, begin, qua, life, delegate(GameObject go)
				{
					go.transform.DOMove(end, life, false);
				});
			}
		}

		public void play(string path, Transform tran, float life)
		{
			this.play(path, tran.position, tran.localEulerAngles, life);
		}

		public void play(string path, Transform tran, float rotY, float life)
		{
			Vector3 eulerAngles = tran.eulerAngles;
			eulerAngles.y = rotY;
			this.play(path, tran.position, eulerAngles, life);
		}

		public void play(string path, Vector3 pos, Quaternion qua, float life, Action<GameObject> handle = null)
		{
			this.getGameObject(path, delegate(GameObject goa)
			{
				float num = life;
				bool flag = num == 0f;
				if (flag)
				{
					num = this.dLifeTime[path].lifetime;
				}
				this.initFadeInObj(goa, pos, qua, num);
				bool flag2 = handle != null;
				if (flag2)
				{
					handle(goa);
				}
			});
		}

		public void play(string path, Vector3 pos, Vector3 eulerAngles, float life)
		{
			this.getGameObject(path, delegate(GameObject goa)
			{
				float num = life;
				bool flag = num == 0f;
				if (flag)
				{
					num = this.dLifeTime[path].lifetime;
				}
				this.initFadeInObj(goa, pos, eulerAngles, num);
			});
		}

		private void getGameObject(string id, Action<GameObject> handle)
		{
			bool flag = id == "" || id == null;
			if (!flag)
			{
				bool flag2 = !this.dLifeTime.ContainsKey(id);
				if (flag2)
				{
					Variant effectConf = GraphManager.singleton.getEffectConf(id);
					bool flag3 = effectConf == null;
					if (flag3)
					{
						return;
					}
					effData value = default(effData);
					value.path = effectConf["file"];
					value.lifetime = effectConf["lenth"]._float / 10f;
					this.dLifeTime[id] = value;
				}
				string path = this.dLifeTime[id].path;
				bool flag4 = !MapEffMgr.dGo.ContainsKey(id);
				if (flag4)
				{
					IAsset asset = os.asset.getAsset<IAssetParticles>(path, delegate(IAsset ast)
					{
						GameObject assetObj = (ast as AssetParticlesImpl).assetObj;
						MapEffMgr.dGo[id] = assetObj;
						GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(assetObj);
						gameObject2.transform.SetParent(this.con, false);
						handle(gameObject2);
					}, null, delegate(IAsset ast, string err)
					{
						Debug.LogError("加载特效失败" + id);
					});
					(asset as AssetImpl).loadImpl(false);
				}
				else
				{
					bool flag5 = MapEffMgr.dGo[id] != null;
					if (flag5)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(MapEffMgr.dGo[id]);
						gameObject.transform.SetParent(this.con, false);
						handle(gameObject);
					}
				}
			}
		}

		private void initFadeInObj(GameObject go, Vector3 position, Vector3 eulerAngles, float life)
		{
			go.transform.position = position;
			go.transform.localEulerAngles = eulerAngles;
			fadeout fadeout = go.AddComponent<fadeout>();
			fadeout.lifeTime = life;
		}

		private void initFadeInObj(GameObject go, Vector3 position, Quaternion qua, float life)
		{
			go.transform.position = position;
			go.transform.rotation = qua;
			fadeout fadeout = go.AddComponent<fadeout>();
			fadeout.lifeTime = life;
		}

		public static MapEffMgr getInstance()
		{
			bool flag = MapEffMgr._instacne == null;
			if (flag)
			{
				MapEffMgr._instacne = new MapEffMgr();
			}
			return MapEffMgr._instacne;
		}
	}
}
