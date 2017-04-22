using LudumDare38.Resources;
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
		public static LitPrimitivesShaderProgram LitPrimitives;

		public static void InitializeShaders()
		{
			Shader vertexShader = new Shader(ShaderType.VertexShader, GetShaderSource("Resources/primitives_vert.glsl"));
			Shader fragmentShader = new Shader(ShaderType.FragmentShader, GetShaderSource("Resources/primitives_frag.glsl"));
			BasePrimitives = new BasePrimitivesShaderProgram(vertexShader, fragmentShader);

			fragmentShader = new Shader(ShaderType.FragmentShader, GetShaderSource("Resources/lit_primitives_frag.glsl"));
			LitPrimitives = new LitPrimitivesShaderProgram(vertexShader, fragmentShader);
		}

		private static string GetShaderSource(string path)
		{
			return ResourceManager.LoadEmbedded<string>(path);
		}
	}
}
