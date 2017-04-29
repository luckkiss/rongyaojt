using System;

namespace Cross
{
	public interface IShader
	{
		IAssetShader asset
		{
			get;
			set;
		}

		void dispose();

		void setProperty(string propName, float value);

		void setProperty(string propName, float x, float y, float z, float w = 0f);

		void setTexture(string propName, string texPath);
	}
}
