using System;
using System.Collections.Generic;

namespace Cross
{
	public interface IUIBaseControl
	{
		string id
		{
			get;
			set;
		}

		int layer
		{
			get;
			set;
		}

		Style2D align
		{
			get;
			set;
		}

		IUIContainer parent
		{
			get;
		}

		bool isDisposed
		{
			get;
		}

		string file
		{
			get;
			set;
		}

		float width
		{
			get;
			set;
		}

		float height
		{
			get;
			set;
		}

		float x
		{
			get;
			set;
		}

		float y
		{
			get;
			set;
		}

		float alpha
		{
			get;
			set;
		}

		bool visible
		{
			get;
			set;
		}

		float scale
		{
			get;
			set;
		}

		Dictionary<string, Action<IUIBaseControl, Event>> eventReceiver
		{
			get;
			set;
		}

		bool enable
		{
			get;
			set;
		}

		IUIBase bindUI
		{
			get;
			set;
		}

		float rotation
		{
			get;
			set;
		}

		bool mouseChildren
		{
			get;
			set;
		}

		bool dragDropDropEnable
		{
			get;
			set;
		}

		bool mouseEnabled
		{
			get;
			set;
		}

		IAssetBitmap mask
		{
			set;
		}

		float depthStart
		{
			get;
			set;
		}

		IUIBaseControl creator(string id);

		void dispose();

		void loadConfig(Variant prop = null, HashSet<IAsset> refAsts = null);

		void addEventListener(Define.EventType eventType, Action<IUIBaseControl, Event> cbFunc);

		void removeEventListener(Define.EventType eventType, Action<IUIBaseControl, Event> cbFunc);

		void applyNewSkin(string newSkinName);

		Vec2 localToGlobal(Vec2 pt);

		Vec2 globalToLocal(Vec2 pt);

		void startDrag(bool lockCenter = false, Rect bounds = null);

		void stopDrag();

		void createUIObject(HashSet<IAsset> refAsts = null);

		void attachToSceneRoot();

		void updateUI();

		bool isgetDisp();

		Vec3 changePot(Vec3 pos);
	}
}
