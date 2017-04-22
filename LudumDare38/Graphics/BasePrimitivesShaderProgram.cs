using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Graphics
{
	public class BasePrimitivesShaderProgram : BaseShaderProgram
	{
		public BasePrimitivesShaderProgram(Shader vertexShader, Shader vragmentShader)
			: base(vertexShader, vragmentShader)
		{
		}

		public void Begin(Matrix4 mvpMatrix, bool textured)
		{
			UniformMatrix4(MvpMatrixLocation, mvpMatrix);
			Uniform1(TexturedLocation, textured);
		}
	}
}
