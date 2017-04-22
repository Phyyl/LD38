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
		public BasePrimitivesShaderProgram(Shader vertexShader, Shader fragmentShader)
			: base(vertexShader, fragmentShader)
		{
		}

		public void Begin((Matrix4 projectionMatrix, Matrix4 viewMatrix, Matrix4 modelMatrix, Matrix4 normalMatrix) matrices, bool textured)
		{
			UniformMatrix4(ProjectionMatrixLocation, matrices.projectionMatrix);
			UniformMatrix4(ViewMatrixLocation, matrices.viewMatrix);
			UniformMatrix4(ModelMatrixLocation, matrices.modelMatrix);
			UniformMatrix4(NormalMatrixLocation, matrices.normalMatrix);
			Uniform1(TexturedLocation, textured);
		}
	}
}
