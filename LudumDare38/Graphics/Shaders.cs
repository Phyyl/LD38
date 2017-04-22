using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Graphics
{
	public static class Shaders
	{
		public static BasePrimitivesShaderProgram BasePrimitives;

		public static void InitializeShaders()
		{
			Shader baseVertexShader = new Shader(ShaderType.VertexShader, GetShaderSource("Resources/primitives_vert.glsl"));
			Shader baseFragmentShader = new Shader(ShaderType.FragmentShader, GetShaderSource("Resources/primitives_frag.glsl"));
			BasePrimitives = new BasePrimitivesShaderProgram(baseVertexShader, baseFragmentShader);
		}

		private static string GetShaderSource(string path)
		{
			return ResourceManager.LoadEmbedded<string>(path);
		}
	}
}
