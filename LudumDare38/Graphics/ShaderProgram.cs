using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudumDare38.Graphics
{
	public class ShaderProgram
	{
		public int ID { get; private set; }

		public bool Linked
		{
			get
			{
				int status;
				GL.GetProgram(ID, GetProgramParameterName.LinkStatus, out status);
				return status == 1;
			}
		}

		public ShaderProgram()
		{
			ID = GL.CreateProgram();
		}

		public virtual void AttachShader(int id)
		{
			GL.AttachShader(ID, id);
		}

		public virtual void AttachShader(Shader shader)
		{
			AttachShader(shader.ID);
		}

		public virtual bool Link()
		{
			GL.LinkProgram(ID);
			GL.ValidateProgram(ID);

			return Linked;
		}

		public void Use()
		{
			GL.UseProgram(ID);
		}

		public void Unuse()
		{
			GL.UseProgram(0);
		}

		public string GetInfoLog()
		{
			return GL.GetProgramInfoLog(ID).Trim();
		}

		public int GetUniformLocation(string name)
		{
			return GL.GetUniformLocation(ID, name);
		}

		public int GetAttribLocation(string name)
		{
			return GL.GetAttribLocation(ID, name);
		}

		protected bool CompileAndLink(params Shader[] shaders)
		{
			foreach (var shader in shaders)
			{
				if (shader.Compiled)
				{
					AttachShader(shader);
				}
				else
				{
					Console.WriteLine($"{shader.ShaderType} compilation failed: {shader.GetInfoLog()}");
					return false;
				}
			}

			if (!Link())
			{
				Console.WriteLine($"Shader linking failed: {GetInfoLog()}");
				return false;
			}

			return true;
		}

		#region Uniforms

		public void UniformMatrix2(int location, Matrix2 value, bool transpose = false)
		{
			if (location < 0)
			{
				return;
			}

			GL.UniformMatrix2(location, transpose, ref value);
		}
		public void UniformMatrix3(int location, Matrix3 value, bool transpose = false)
		{
			if (location < 0)
			{
				return;
			}

			GL.UniformMatrix3(location, transpose, ref value);
		}
		public void UniformMatrix4(int location, Matrix4 value, bool transpose = false)
		{
			if (location < 0)
			{
				return;
			}

			GL.UniformMatrix4(location, transpose, ref value);
		}

		public void Uniform1(int location, float value)
		{
			if (location < 0)
			{
				return;
			}

			GL.Uniform1(location, value);
		}

		public void Uniform1(int location, bool value)
		{
			if (location < 0)
			{
				return;
			}

			GL.Uniform1(location, value ? 1 : 0);
		}

		public void Uniform1(int location, int value)
		{
			if (location < 0)
			{
				return;
			}

			GL.Uniform1(location, value);
		}

		public void Uniform2(int location, Vector2 value)
		{
			if (location < 0)
			{
				return;
			}

			GL.Uniform2(location, value);
		}

		public void Uniform3(int location, Vector3 value)
		{
			if (location < 0)
			{
				return;
			}

			GL.Uniform3(location, value);
		}

		public void Uniform4(int location, Vector4 value)
		{
			if (location < 0)
			{
				return;
			}

			GL.Uniform4(location, value);
		}

		#endregion
	}
}
