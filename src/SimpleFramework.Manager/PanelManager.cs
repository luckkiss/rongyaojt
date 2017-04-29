using DG.Tweening;
using GameFramework;
using LuaInterface;
using MuGame;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleFramework.Manager
{
	public class PanelManager : View
	{
		private Transform m_winlayer_parent;

		private Transform m_floatui_parent;

		private Transform m_loadingui_parent;

		private Transform WinLayerParent
		{
			get
			{
				if (this.m_winlayer_parent == null)
				{
					GameObject gameObject = GameObject.Find("canvas_main/winLayer");
					if (gameObject != null)
					{
						this.m_winlayer_parent = gameObject.transform;
					}
				}
				return this.m_winlayer_parent;
			}
		}

		private Transform FloatUIParent
		{
			get
			{
				if (this.m_floatui_parent == null)
				{
					GameObject gameObject = GameObject.Find("canvas_main/floatUI");
					if (gameObject != null)
					{
						this.m_floatui_parent = gameObject.transform;
					}
				}
				return this.m_floatui_parent;
			}
		}

		private Transform LoadingUIParent
		{
			get
			{
				if (this.m_loadingui_parent == null)
				{
					GameObject gameObject = GameObject.Find("Canvas_overlay/loadingLayer");
					if (gameObject != null)
					{
						this.m_loadingui_parent = gameObject.transform;
					}
				}
				return this.m_loadingui_parent;
			}
		}

		public void ui_unshow()
		{
			if (GameObject.Find("Canvas_overlay/loadingLayer/fightingup") && GameObject.Find("Canvas_overlay/loadingLayer/fightingup").gameObject.activeSelf)
			{
				GameObject.Find("Canvas_overlay/loadingLayer/fightingup").gameObject.SetActive(false);
			}
			if (GameObject.Find("Canvas_overlay/loadingLayer/a3_attChange(Clone)") && GameObject.Find("Canvas_overlay/loadingLayer/a3_attChange(Clone)").gameObject.activeSelf)
			{
				GameObject.Find("Canvas_overlay/loadingLayer/a3_attChange(Clone)").gameObject.SetActive(false);
			}
		}

		public void open(string name)
		{
		}

		public void CreateUI_Layer(string name, int type, LuaFunction func = null)
		{
			GameObject gameObject = base.ResManager.LoadAsset("ui_win", name);
			Transform transform = this.WinLayerParent;
			if (type == 2)
			{
				transform = this.FloatUIParent;
			}
			if (type == 3)
			{
				transform = this.LoadingUIParent;
			}
			if (transform.FindChild(name) != null || gameObject == null)
			{
				return;
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			gameObject2.name = name;
			gameObject2.layer = LayerMask.NameToLayer("Default");
			gameObject2.transform.SetParent(transform, false);
			gameObject2.transform.localScale = Vector3.one;
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.AddComponent<LuaUI>();
			if (func != null)
			{
				func.Call(new object[]
				{
					gameObject2
				});
			}
			Debug.Log(string.Concat(new object[]
			{
				"CreateUI_Layer::>> ",
				name,
				" ",
				gameObject
			}));
		}

		public GameObject newGameobject(string name)
		{
			GameObject gameObject = new GameObject();
			gameObject.AddComponent<RectTransform>();
			gameObject.name = name;
			return gameObject;
		}

		public void onSound(string path)
		{
			MediaClient.instance.PlaySoundUrl("audio/common/" + path, false, null);
		}

		public Image newImage(GameObject go)
		{
			return go.AddComponent<Image>();
		}

		public void GAME_CAMERA(bool tf)
		{
			if (GRMap.GAME_CAMERA != null)
			{
				GRMap.GAME_CAMERA.SetActive(tf);
			}
		}

		public ScrollControler newScrollControler(Transform trans)
		{
			ScrollControler scrollControler = new ScrollControler();
			ScrollRect component = trans.GetComponent<ScrollRect>();
			scrollControler.create(component, 4);
			return scrollControler;
		}

		public void newcellSize(Transform trans, float x, float y)
		{
			if (trans == null)
			{
				return;
			}
			trans.GetComponent<GridLayoutGroup>().cellSize = new Vector2(x, y);
		}

		public string[] new_Split(string str)
		{
			return str.Split(new char[]
			{
				','
			});
		}

		public TabControl newTabControler(Transform trans, Transform main, LuaFunction onswitch)
		{
			TabControl tabControl = new TabControl();
			if (onswitch != null)
			{
				tabControl.onClickHanle = delegate(TabControl tc)
				{
					for (int i = 0; i < main.childCount; i++)
					{
						main.GetChild(i).gameObject.SetActive(false);
					}
					Transform transform = main.FindChild(trans.GetChild(tc.getSeletedIndex()).name);
					if (transform != null)
					{
						transform.gameObject.SetActive(true);
					}
					onswitch.Call(new object[]
					{
						tc
					});
				};
			}
			tabControl.create(trans.gameObject, main.gameObject, 0, 0, false);
			return tabControl;
		}

		public GameObject resLoad(string pkgname, string goname)
		{
			GameObject original = base.ResManager.LoadAsset(pkgname, goname);
			return UnityEngine.Object.Instantiate<GameObject>(original);
		}

		public Sprite resPicLoad(string pkgname, string goname)
		{
			return base.ResManager.LoadSpriteAsset(pkgname, goname);
		}

		public XMLMgr xmlMgr()
		{
			return XMLMgr.instance;
		}

		public KeyWord getKeyWord()
		{
			return KeyWord.instance;
		}

		public Tween domoveX(Transform trans, float value, float duration, object handle = null)
		{
			if (handle != null && handle is LuaFunction)
			{
				return trans.DOLocalMoveX(value, duration, false).OnComplete(delegate
				{
					(handle as LuaFunction).Call();
				});
			}
			return trans.DOLocalMoveX(value, duration, false);
		}

		public Tween domoveY(Transform trans, float value, float duration, object handle = null)
		{
			if (handle != null && handle is LuaFunction)
			{
				return trans.DOLocalMoveY(value, duration, false).OnComplete(delegate
				{
					(handle as LuaFunction).Call();
				});
			}
			return trans.DOLocalMoveY(value, duration, false);
		}

		public Tween doScaleX(Transform trans, float value, float duration, object handle = null)
		{
			if (handle != null && handle is LuaFunction)
			{
				return trans.DOScaleX(value, duration).OnComplete(delegate
				{
					(handle as LuaFunction).Call();
				});
			}
			return trans.DOScaleX(value, duration);
		}

		public Tween doScaleY(Transform trans, float value, float duration, object handle = null)
		{
			if (handle != null && handle is LuaFunction)
			{
				return trans.DOScaleY(value, duration).OnComplete(delegate
				{
					(handle as LuaFunction).Call();
				});
			}
			return trans.DOScaleY(value, duration);
		}

		public Tween doScale(Transform trans, Vector3 vec, float duration, object handle = null)
		{
			if (handle != null && handle is LuaFunction)
			{
				return trans.DOScale(vec, duration).OnComplete(delegate
				{
					(handle as LuaFunction).Call();
				});
			}
			return trans.DOScale(vec, duration);
		}

		public Tween doRotate(Transform trans, Vector3 vec, float duration, object handle = null)
		{
			if (handle != null && handle is LuaFunction)
			{
				return trans.DOLocalRotate(vec, duration, RotateMode.Fast).OnComplete(delegate
				{
					(handle as LuaFunction).Call();
				});
			}
			return trans.DOLocalRotate(vec, duration, RotateMode.Fast);
		}

		public void killTween(Transform trans)
		{
			trans.DOKill(false);
		}

		public string getCont(string id, LuaTable prams = null)
		{
			if (ContMgr.dText == null)
			{
				ContMgr.init();
			}
			if (!ContMgr.dText.ContainsKey(id))
			{
				return id;
			}
			string text = ContMgr.dText[id];
			if (prams != null)
			{
				int num = 0;
				foreach (string separator in prams.Values)
				{
					string[] separator2 = new string[]
					{
						"<" + num + ">"
					};
					string[] value = text.Split(separator2, StringSplitOptions.None);
					text = string.Join(separator, value);
					num++;
				}
			}
			return text;
		}

		public string getError(string id)
		{
			return ContMgr.getError(id);
		}

		public void openByC(string name)
		{
			MuGame.InterfaceMgr.getInstance().open(name, null, false);
		}

		public void closeByC(string name)
		{
			MuGame.InterfaceMgr.getInstance().close(name);
		}

		public void changeStateByC(int state)
		{
			MuGame.InterfaceMgr.getInstance().changeState(state);
		}

		public GameObject addBehaviour(GameObject go, string behaviour)
		{
			Type type = ConfigUtil.getType("CollectRole");
			Type type2 = ConfigUtil.getType(behaviour);
			go.AddComponent(type2);
			return go;
		}

		public EventTriggerListenerLua getEventTrigger(GameObject go)
		{
			return EventTriggerListenerLua.Get(go);
		}

		public void sliderOnValueChanged(Slider go, LuaFunction func)
		{
			if (go == null || func == null)
			{
				return;
			}
			go.onValueChanged.AddListener(delegate(float value)
			{
				if (func != null)
				{
					func.Call((double)value);
				}
			});
		}

		public void doByC(string name, params object[] args)
		{
			MuGame.InterfaceMgr.getInstance().doAction(name, args);
		}

		public void tween(Transform trans, string fun, LuaTable table)
		{
		}

		public FunctionOpenMgr functionOpenMgr()
		{
			return FunctionOpenMgr.instance;
		}
	}
}
