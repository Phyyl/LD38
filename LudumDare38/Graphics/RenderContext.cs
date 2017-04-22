using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace LudumDare38.Graphics
{
	public class RenderContext
	{
		private Vao vao;
		private Vbo vbo;
		private Stack<RenderContextState> states = new Stack<RenderContextState>();
		private RenderContextState CurrentState => states.Peek();

		public Matrix4 ProjectionMatrix
		{
			get { return CurrentState.ProjectionMatrix; }
			set { SetProjectionMatrix(value); }
		}

		public Matrix4 ViewMatrix
		{
			get { return CurrentState.ViewMatrix; }
			set { SetViewMatrix(value); }
		}

		public Matrix4 ModelMatrix
		{
			get { return CurrentState.ModelMatrix; }
			set { SetModelMatrix(value); }
		}

		public Matrix4 MvpMatrix => CurrentState.ModelMatrix * CurrentState.ViewMatrix * CurrentState.ProjectionMatrix;

		public MatrixMode MatrixMode { get; set; } = MatrixMode.Model;

		public RenderContext()
		{
			vao = new Vao();
			vbo = new Vbo();

			Shaders.BasePrimitives.Setup(vao, vbo);

			PushIdentity();
		}

		public void Pop()
		{
			states.Pop();

			if (states.Count == 0)
			{
				PushIdentity();
			}
		}

		public void ClearMatrices()
		{
			states.Clear();
			PushIdentity();
		}

		public void Push()
		{
			states.Push(CurrentState);
		}

		public void PushIdentity()
		{
			states.Push(RenderContextState.CreateDefault());
		}

		public void LoadIdentity()
		{
			SetMatrix(Matrix4.Identity);
		}

		private void SetProjectionMatrix(Matrix4 matrix)
		{
			RenderContextState current = states.Pop();
			current.ProjectionMatrix = matrix;
			states.Push(current);
		}

		private void SetViewMatrix(Matrix4 matrix)
		{
			RenderContextState current = states.Pop();
			current.ViewMatrix = matrix;
			states.Push(current);
		}

		private void SetModelMatrix(Matrix4 matrix)
		{
			RenderContextState current = states.Pop();
			current.ModelMatrix = matrix;
			states.Push(current);
		}

		public void SetMatrix(Matrix4 matrix)
		{
			RenderContextState current = states.Pop();

			switch (MatrixMode)
			{
				case MatrixMode.Model:
					current.ModelMatrix = matrix;
					break;
				case MatrixMode.View:
					current.ViewMatrix = matrix;
					break;
				case MatrixMode.Projection:
					current.ProjectionMatrix = matrix;
					break;
				default:
					break;
			}

			states.Push(current);
		}

		public void Clear(Color4? color = null)
		{
			GL.ClearColor(color ?? Color4.Black);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		}

		#region Enable/Disable

		public void EnableTransparency()
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		}

		public void DisableTransparency()
		{
			GL.Disable(EnableCap.Blend);
		}

		public void EnableCullFace(CullFaceMode mode = CullFaceMode.Back)
		{
			GL.Enable(EnableCap.CullFace);
			GL.CullFace(mode);
		}

		public void DisableCullFace()
		{
			GL.Disable(EnableCap.CullFace);
		}

		public void EnableDepth()
		{
			GL.Enable(EnableCap.DepthTest);
		}

		public void DisableDepth()
		{
			GL.Disable(EnableCap.DepthTest);
		}

		#endregion

		#region Render

		public void DrawLine(Vector3 start, Vector3 end, Color4 color)
		{
			Vector4 colorVector = color.ToVector();

			DrawVertices(PrimitiveType.Lines, new Vertex[]
			{
				new Vertex(start, colorVector),
				new Vertex(end, colorVector)
			});
		}

		public void DrawLine(Vector2 start, Vector2 end, Color4 color)
		{
			DrawLine(new Vector3(start), new Vector3(end), color);
		}

		public void DrawCircle(Vector2 center, float radius, Color4 color, int sides = 30)
		{
			Vector4 colorVector = color.ToVector();
			List<Vertex> vertices = new List<Vertex>();

			sides = Math.Max(sides, 3);
			vertices.Add(new Vertex(center));

			for (int i = 0; i <= sides; i++)
			{
				float radian = (i / (float)sides) * MathHelper.TwoPi;

				vertices.Add(new Vertex(center + new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian)) * radius, colorVector));
			}

			DrawVertices(PrimitiveType.TriangleFan, vertices.ToArray());
		}

		public void DrawTriangle(Vector3 a, Vector3 b, Vector3 c, Color4 color)
		{
			Vector4 colorVector = color.ToVector();

			DrawVertices(PrimitiveType.TriangleFan, new Vertex[]
			{
				new Vertex(a, colorVector),
				new Vertex(b, colorVector),
				new Vertex(c, colorVector)
			});
		}

		public void DrawTriangle(Vector2 a, Vector2 b, Vector2 c, Color4 color)
		{
			DrawTriangle(new Vector3(a), new Vector3(b), new Vector3(c), color);
		}

		public void DrawRect(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Color4 color)
		{
			Vector4 colorVector = color.ToVector();

			DrawVertices(PrimitiveType.TriangleFan, new Vertex[]
			{
				new Vertex(a, colorVector),
				new Vertex(b, colorVector),
				new Vertex(c, colorVector),
				new Vertex(d, colorVector)
			});
		}

		public void DrawRect(Vector2 a, Vector2 b, Vector2 c, Vector2 d, Color4 color)
		{
			DrawRect(new Vector3(a), new Vector3(b), new Vector3(c), new Vector3(d), color);
		}

		public void DrawRect(RectangleF rectangle, Color4 color)
		{
			DrawRect(new Vector3(rectangle.Left, rectangle.Top, 0), new Vector3(rectangle.Left, rectangle.Bottom, 0), new Vector3(rectangle.Right, rectangle.Bottom, 0), new Vector3(rectangle.Right, rectangle.Top, 0), color);
		}

		public void DrawImage(RectangleF rectangle, Texture texture, Color4? tint = null, RectangleF? region = null)
		{
			RectangleF finalRegion = region ?? texture.Bounds;

			finalRegion.X /= texture.Width;
			finalRegion.Width /= texture.Width;
			finalRegion.Y /= texture.Height;
			finalRegion.Height /= texture.Height;

			Vector4 colorVector = (tint ?? Color4.White).ToVector();

			EnableTransparency();
			DrawVertices(PrimitiveType.TriangleFan, new Vertex[]
			{
				new Vertex(rectangle.GetTopLeft(), colorVector, finalRegion.GetTopLeft()),
				new Vertex(rectangle.GetBottomLeft(), colorVector, finalRegion.GetBottomLeft()),
				new Vertex(rectangle.GetBottomRight(), colorVector, finalRegion.GetBottomRight()),
				new Vertex(rectangle.GetTopRight(), colorVector, finalRegion.GetTopRight())
			}, texture);
		}

		public void DrawVertices(PrimitiveType primitiveType, Vertex[] vertices, Texture texture = null)
		{
			BeginDrawArrays(vertices);
			Shaders.BasePrimitives.Use();
			Shaders.BasePrimitives.Begin(MvpMatrix, texture != null);
			DrawArrays(primitiveType, texture);
			Shaders.BasePrimitives.Unuse();
		}

		public void BeginDrawArrays(Vertex[] vertices)
		{
			vbo.BufferData(vertices, Vertex.SizeInBytes);
		}

		public void DrawArrays(PrimitiveType primitiveType, Texture texture = null)
		{
			vao.Bind();
			texture?.Bind();
			GL.DrawArrays(primitiveType, 0, vbo.VertexCount);
			Texture.Unbind();
		}

		#endregion

		#region Transforms

		public void RotateX(float angle)
		{
			ApplyMatrix(Matrix4.CreateRotationX(angle));
		}

		public void RotateY(float angle)
		{
			ApplyMatrix(Matrix4.CreateRotationY(angle));
		}

		public void RotateZ(float angle)
		{
			ApplyMatrix(Matrix4.CreateRotationZ(angle));
		}

		public void Translate(Vector2 translation)
		{
			Translate(translation.X, translation.Y);
		}

		public void Translate(float x, float y)
		{
			Translate(x, y, 0);
		}

		public void Translate(float x, float y, float z)
		{
			ApplyMatrix(Matrix4.CreateTranslation(x, y, z));
		}

		public void Scale(float x, float y)
		{
			ApplyMatrix(Matrix4.CreateScale(x, y, 1));
		}

		private void ApplyMatrix(Matrix4 matrix)
		{
			RenderContextState current = states.Pop();
			switch (MatrixMode)
			{
				case MatrixMode.Model:
					current.ModelMatrix *= matrix;
					break;
				case MatrixMode.View:
					current.ViewMatrix *= matrix;
					break;
				case MatrixMode.Projection:
					current.ProjectionMatrix *= matrix;
					break;
				default:
					break;
			}
			states.Push(current);
		}

		#endregion

		private struct RenderContextState
		{
			public Matrix4 ProjectionMatrix;
			public Matrix4 ViewMatrix;
			public Matrix4 ModelMatrix;

			public static RenderContextState CreateDefault()
			{
				return new RenderContextState
				{
					ProjectionMatrix = Matrix4.Identity,
					ViewMatrix = Matrix4.Identity,
					ModelMatrix = Matrix4.Identity
				};
			}
		}

		private static string GetShaderSource(string path)
		{
			return ResourceManager.LoadEmbedded<string>(path, typeof(RenderContext).Assembly);
		}
	}

	public enum MatrixMode
	{
		Model,
		View,
		Projection
	}
}
