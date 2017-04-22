using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Graphics
{
	public class Shader
	{
		public int ID { get; private set; }
		public ShaderType ShaderType { get; private set; }

		public bool Compiled
		{
			get
			{
				int status;
				GL.GetShader(ID, ShaderParameter.CompileStatus, out status);
				return status == 1;
			}
		}

		public Shader(ShaderType type, string source)
		{
			ShaderType = type;

			ID = GL.CreateShader(type);
			GL.ShaderSource(ID, source);
			GL.CompileShader(ID);
		}

		public string GetInfoLog()
		{
			return GL.GetShaderInfoLog(ID).Trim();
		}
	}
}
