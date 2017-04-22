using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Graphics
{
	public class LitPrimitivesShaderProgram : BasePrimitivesShaderProgram
	{
		public int LightDirectionLocation { get; private set; }

		public LitPrimitivesShaderProgram(Shader vertexShader, Shader fragmentShader)
			: base(vertexShader, fragmentShader)
		{

		}

		protected override void InitializeLocations()
		{
			base.InitializeLocations();

			LightDirectionLocation = GetUniformLocation("light_direction");
		}

		public void Begin((Matrix4 projectionMatrix, Matrix4 viewMatrix, Matrix4 modelMatrix, Matrix4 normalMatrix) matrices, Vector3 lightDirection)
		{
			base.Begin(matrices, false);

			Uniform3(LightDirectionLocation, lightDirection);
		}
	}
}
