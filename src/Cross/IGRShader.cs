using System;

namespace Cross
{
	public interface IGRShader
	{
		void load(Variant conf);

		void dispose();

		void setProperty(string propName, float value);

		void setProperty(string propName, float x, float y, float z, float w = 0f);

		void setTexture(string propName, string texPath);
	}
}
