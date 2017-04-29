using Cross;
using GameFramework;
using System;

namespace MuGame
{
	public class ClientFWGameConf : configParser
	{
		public ClientFWGameConf(ClientConfig m) : base(m)
		{
		}

		public static ClientFWGameConf create(IClientBase m)
		{
			return new ClientFWGameConf(m as ClientConfig);
		}

		protected override Variant _formatConfig(Variant conf)
		{
			bool flag = conf.ContainsKey("chatitle");
			if (flag)
			{
				Variant variant = new Variant();
				for (int i = 0; i < conf["chatitle"].Length; i++)
				{
					Variant variant2 = conf["chatitle"][i];
					variant2["show"] = GameTools.array2Map(variant2["show"], "id", 1u);
					bool flag2 = variant2.ContainsKey("sort");
					if (flag2)
					{
						Variant variant3 = GameTools.split(variant2["sort"]._str, ",", 1u);
						for (int j = 0; j < variant3.Length; j++)
						{
							variant3[j] = variant3[j]._int;
						}
						variant2["sort"] = variant3;
					}
					bool flag3 = variant2.ContainsKey("tp") && variant2["tp"] != null;
					if (flag3)
					{
						variant[variant2["tp"]._str] = variant2;
					}
				}
				conf["chatitle"] = variant;
			}
			bool flag4 = conf.ContainsKey("chagrd");
			if (flag4)
			{
				conf["chagrd"] = GameTools.array2Map(conf["chagrd"], "tp", 1u);
			}
			bool flag5 = conf.ContainsKey("chadyn");
			if (flag5)
			{
				Variant variant4 = new Variant();
				foreach (Variant current in conf["chadyn"]._arr)
				{
					current["show"] = GameTools.array2Map(current["show"], "id", 1u);
					current["ani"] = GameTools.array2Map(current["ani"], "tp", 1u);
					variant4[current["tp"]._str] = current;
				}
				conf["chadyn"] = variant4;
			}
			bool flag6 = conf.ContainsKey("chabar");
			if (flag6)
			{
				Variant variant5 = new Variant();
				foreach (Variant current2 in conf["chabar"]._arr)
				{
					current2["show"] = GameTools.array2Map(current2["show"], "idx", 1u);
					current2["ani"] = current2["ani"][0];
					variant5[current2["tp"]._str] = current2;
				}
				conf["chabar"] = variant5;
			}
			bool flag7 = conf.ContainsKey("uiprop");
			if (flag7)
			{
				conf["uiprop"] = GameTools.array2Map(conf["uiprop"], "id", 1u);
			}
			bool flag8 = conf.ContainsKey("sound");
			if (flag8)
			{
				conf["sound"] = GameTools.array2Map(conf["sound"], "id", 1u);
			}
			bool flag9 = conf.ContainsKey("uieff");
			if (flag9)
			{
				conf["uieff"] = GameTools.array2Map(conf["uieff"], "id", 1u);
			}
			bool flag10 = conf.ContainsKey("carrAni");
			if (flag10)
			{
				conf["carrAni"] = GameTools.array2Map(conf["carrAni"], "id", 1u);
			}
			bool flag11 = conf.ContainsKey("carrLayer");
			if (flag11)
			{
				conf["carrLayer"] = GameTools.array2Map(conf["carrLayer"], "id", 1u);
			}
			return conf;
		}

		public Variant GetChaBarConf(string tp)
		{
			Variant variant = base.conf["chabar"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant[tp];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetChaTitleConf(string tp)
		{
			Variant variant = base.conf["chatitle"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant[tp];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetChaGroundConf(string tp)
		{
			Variant variant = base.conf["chagrd"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant[tp];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetChaDynamicConf(string tp)
		{
			Variant variant = base.conf["chadyn"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant[tp];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetUIProp(string id)
		{
			Variant variant = base.conf["uiprop"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant[id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public string GetSoundUrl(string id)
		{
			Variant variant = base.conf["sound"];
			bool flag = variant != null && variant[id] != null;
			string result;
			if (flag)
			{
				result = variant[id]["url"];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public Variant GetUIEffConf(string id)
		{
			Variant variant = base.conf["uieff"];
			bool flag = variant != null;
			Variant result;
			if (flag)
			{
				result = variant[id];
			}
			else
			{
				result = null;
			}
			return result;
		}

		public string GetAniByName(int carr, string aniName)
		{
			bool flag = base.conf["carrAni"][carr] != null;
			string result;
			if (flag)
			{
				Variant variant = base.conf["carrAni"][carr]["ani"];
				bool flag2 = variant;
				if (flag2)
				{
					foreach (Variant current in variant._arr)
					{
						bool flag3 = current["name"]._str == aniName;
						if (flag3)
						{
							result = current["ani"]._str;
							return result;
						}
					}
				}
			}
			result = "";
			return result;
		}

		public string GetLayerByAni(int carr, string aniName)
		{
			Variant variant = base.conf["carrLayer"][carr]["layer"];
			bool flag = variant;
			string result;
			if (flag)
			{
				foreach (Variant current in variant._arr)
				{
					bool flag2 = current["ani"]._str == aniName;
					if (flag2)
					{
						result = current["layer"]._str;
						return result;
					}
				}
			}
			result = "";
			return result;
		}
	}
}
