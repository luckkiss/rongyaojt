using System;

namespace Cross
{
	public class ShaderInterfaceImpl : IShaderInterface
	{
		public IShader createShader()
		{
			return new ShaderImpl();
		}
	}
}
