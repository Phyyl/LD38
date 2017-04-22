using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Graphics
{
	public class Vao
	{
		public int ID { get; set; }

		public Vao()
		{
			ID = GL.GenVertexArray();
		}

		public void VertexAttribPointer(Vbo vbo, int index, int size, VertexAttribPointerType type, bool normalized, int stride, int offset)
		{
			if (index < 0)
			{
				return;
			}

			Bind();

			vbo.Bind();
			GL.VertexAttribPointer(index, size, type, normalized, stride, offset);
			GL.EnableVertexAttribArray(index);

			Unbind();
		}

		public void Bind()
		{
			GL.BindVertexArray(ID);
		}

		public void Unbind()
		{
			GL.BindVertexArray(0);
		}
	}
}
