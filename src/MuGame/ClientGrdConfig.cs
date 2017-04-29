using Cross;
using GameFramework;
using System;
using System.Collections.Generic;

namespace MuGame
{
	public class ClientGrdConfig : configParser
	{
		protected Dictionary<string, List<string>> m_map = new Dictionary<string, List<string>>();

		protected List<string> m_list;

		public float[] m_hdtdata = null;

		protected short[] m_grd;

		protected float m_min;

		protected float m_max;

		public float m_hdt_z = 0.01f;

		public short[] grd
		{
			get
			{
				return this.m_grd;
			}
		}

		public float min
		{
			get
			{
				return this.m_min;
			}
		}

		public float max
		{
			get
			{
				return this.m_max;
			}
		}

		public ClientGrdConfig(ClientConfig m) : base(m)
		{
		}

		public static IObjectPlugin create(IClientBase m)
		{
			return new ClientGrdConfig(m as ClientConfig);
		}

		public void get_value(string id, Action onfin)
		{
			this.resolve(id);
			bool flag = !this.m_map.ContainsKey(id);
			if (flag)
			{
				GameTools.PrintCrash("map grd file load err!");
				onfin();
			}
			else
			{
				List<string> l = this.m_map[id];
				Action <>9__1;
				this.dataGrd(l[0], delegate
				{
					ClientGrdConfig arg_32_0 = this;
					string arg_32_1 = l[1];
					Action arg_32_2;
					if ((arg_32_2 = <>9__1) == null)
					{
						arg_32_2 = (<>9__1 = delegate
						{
							onfin();
						});
					}
					arg_32_0.dataHdt(arg_32_1, arg_32_2);
				});
			}
		}

		public void dataGrd(string path, Action onfin)
		{
			IURLReq urlImpl = this.m_mgr.g_netM.getUrlImpl();
			urlImpl.url = path;
			urlImpl.dataFormat = "binary";
			debug.Log("处理地图的阻挡数据，数据的文件位置" + path);
			urlImpl.load(delegate(IURLReq r, object ret)
			{
				byte[] d = ret as byte[];
				ByteArray byteArray = new ByteArray(d);
				byteArray.uncompress();
				this.m_grd = new short[byteArray.length / 2];
				int num = 0;
				while (byteArray.bytesAvailable > 0)
				{
					this.m_grd[num++] = byteArray.readShort();
				}
				byteArray.clear();
				onfin();
			}, delegate(IURLReq url, float prog)
			{
				Variant variant = new Variant();
				variant["tp"] = 10;
				variant["prog"] = prog;
				GameTools.PrintNotice("LOADING_MAP_GRD load prog:" + prog);
				this.m_mgr.g_gameM.dispatchEvent(GameEvent.Createimmedi(3050u, this, variant, false));
			}, delegate(IURLReq url, string err)
			{
				Variant variant = new Variant();
				variant["tp"] = 10;
				variant["err"] = err;
				this.m_mgr.g_gameM.dispatchEvent(GameEvent.Createimmedi(3050u, this, variant, false));
			});
		}

		public void dataHdt(string path, Action onfin)
		{
			bool flag = "null" == path;
			if (flag)
			{
				debug.Log("此地图不需要处理，地表的高度信息");
				this.m_hdtdata = null;
				onfin();
			}
			else
			{
				IURLReq urlImpl = this.m_mgr.g_netM.getUrlImpl();
				urlImpl.url = path;
				urlImpl.dataFormat = "binary";
				debug.Log("处理地图的高度数据，数据的文件位置" + path);
				urlImpl.load(delegate(IURLReq r, object ret)
				{
					byte[] d = ret as byte[];
					ByteArray byteArray = new ByteArray(d);
					byteArray.uncompress();
					this.m_hdtdata = new float[byteArray.length / 4];
					int num = 0;
					while (byteArray.bytesAvailable > 0)
					{
						this.m_hdtdata[num++] = byteArray.readFloat();
					}
					byteArray.clear();
					onfin();
				}, null, null);
			}
		}

		public void resolve(string id)
		{
			Variant getMapInfo = this.m_mgr.g_sceneM.getMapInfo;
			for (int i = 0; i < getMapInfo.Count; i++)
			{
				bool flag = getMapInfo[i]["id"]._str == id;
				if (flag)
				{
					this.m_min = getMapInfo[i]["min"];
					this.m_max = getMapInfo[i]["max"];
					this.m_hdt_z = getMapInfo[i]["hdt_z"];
					bool flag2 = !this.m_map.ContainsKey(id);
					if (flag2)
					{
						this.m_list = new List<string>();
						string item = getMapInfo[i]["mskfile"];
						this.m_list.Add(item);
						bool flag3 = getMapInfo[i].ContainsKey("hdtfile");
						if (flag3)
						{
							string item2 = getMapInfo[i]["hdtfile"];
							this.m_list.Add(item2);
						}
						else
						{
							this.m_list.Add("null");
						}
						this.m_map[id] = this.m_list;
					}
				}
			}
		}
	}
}
