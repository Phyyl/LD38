using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Graphics
{
	public class BaseShaderProgram : ShaderProgram
	{
		public int MvpMatrixLocation { get; private set; }
		public int TexturedLocation { get; private set; }
		public int PositionLocation { get; private set; }
		public int ColorLocation { get; private set; }
		public int TexCoordLocation { get; private set; }
		public int NormalLocation { get; private set; }

		public BaseShaderProgram(Shader vertexShader, Shader vragmentShader)
		{
			CompileAndLink(vertexShader, vragmentShader);
		}

		public override bool Link()
		{
			if (base.Link())
			{
				MvpMatrixLocation = GetUniformLocation("mvp_matrix");
				TexturedLocation = GetUniformLocation("textured");
				PositionLocation = GetAttribLocation("vert_position");
				ColorLocation = GetAttribLocation("vert_color");
				TexCoordLocation = GetAttribLocation("vert_tex_coord");
				NormalLocation = GetAttribLocation("vert_normal");

				return true;
			}

			return false;
		}

		public virtual void Setup(Vao vao, Vbo vbo)
		{
			vao.VertexAttribPointer(vbo, PositionLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, Vertex.PositionOffset);
			vao.VertexAttribPointer(vbo, ColorLocation, 4, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, Vertex.ColorOffset);
			vao.VertexAttribPointer(vbo, TexCoordLocation, 2, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, Vertex.TexCoordOffset);
			vao.VertexAttribPointer(vbo, NormalLocation, 3, VertexAttribPointerType.Float, false, Vertex.SizeInBytes, Vertex.NormalOffset);
		}
	}
}
