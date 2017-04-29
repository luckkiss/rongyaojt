using System;

namespace Cross
{
	public class GRShader : IGRShader
	{
		protected string m_id;

		protected IShader m_material;

		public IShader graphMaterial
		{
			get
			{
				return this.m_material;
			}
		}

		public GRShader()
		{
			this.m_material = os.material.createShader();
		}

		public void load(Variant conf)
		{
			bool flag = conf == null;
			if (!flag)
			{
				this.m_id = conf["id"]._str;
				bool flag2 = conf.ContainsKey("method");
				if (flag2)
				{
					Variant variant = conf["method"][0];
					bool flag3 = variant.ContainsKey("file");
					if (flag3)
					{
						this.m_material.asset = os.asset.getAsset<IAssetShader>(variant["file"]._str);
					}
					foreach (string current in variant.Keys)
					{
						bool flag4 = current.IndexOf("_") == 0;
						if (flag4)
						{
							string str = variant[current]._str;
							bool flag5 = str.IndexOf('/') >= 0 || str.IndexOf('\\') >= 0;
							if (flag5)
							{
								this.m_material.setTexture(current, str);
							}
							else
							{
								bool flag6 = str.IndexOf("(") >= 0;
								if (flag6)
								{
									string text = str.Substring(str.IndexOf('(') + 1);
									text = text.Substring(0, text.IndexOf(')'));
									string[] array = text.Split(new char[]
									{
										','
									});
									bool flag7 = array.Length == 3;
									if (flag7)
									{
										this.m_material.setProperty(current, float.Parse(array[0]) / 255f, float.Parse(array[1]) / 255f, float.Parse(array[2]) / 255f, 0f);
									}
									else
									{
										DebugTrace.add(Define.DebugTrace.DTT_ERR, "Illegal color property in material config: " + current + " : " + text);
									}
								}
								else
								{
									bool flag8 = str.IndexOf(",") >= 0;
									if (flag8)
									{
										string[] array2 = str.Split(new char[]
										{
											','
										});
										bool flag9 = array2.Length == 3;
										if (flag9)
										{
											this.m_material.setProperty(current, float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2]), 0f);
										}
										else
										{
											bool flag10 = array2.Length == 4;
											if (flag10)
											{
												this.m_material.setProperty(current, float.Parse(array2[0]), float.Parse(array2[1]), float.Parse(array2[2]), float.Parse(array2[3]));
											}
										}
									}
									else
									{
										this.m_material.setProperty(current, float.Parse(str));
									}
								}
							}
						}
					}
				}
			}
		}

		public void dispose()
		{
			bool flag = this.m_material != null;
			if (flag)
			{
				this.m_material.dispose();
			}
		}

		public void setProperty(string propName, float value)
		{
			bool flag = this.m_material == null;
			if (!flag)
			{
				this.m_material.setProperty(propName, value);
			}
		}

		public void setProperty(string propName, float x, float y, float z, float w = 0f)
		{
			bool flag = this.m_material == null;
			if (!flag)
			{
				this.m_material.setProperty(propName, x, y, z, w);
			}
		}

		public void setTexture(string propName, string texPath)
		{
			bool flag = this.m_material == null;
			if (!flag)
			{
				this.m_material.setTexture(propName, texPath);
			}
		}
	}
}
