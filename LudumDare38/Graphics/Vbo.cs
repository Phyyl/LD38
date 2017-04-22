using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Graphics
{
	public class Vbo
	{
		public int ID { get; private set; }

		public int VertexCount { get; private set; }

		public Vbo()
		{
			ID = GL.GenBuffer();
		}

		public void BufferData<T>(T[] data, int sizePerValue) where T : struct
		{
			Bind();

			GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizePerValue * data.Length), data, BufferUsageHint.StaticDraw);
			VertexCount = data.Length;

			Unbind();
		}

		public void Bind()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
		}

		public void Unbind()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}
	}
}
