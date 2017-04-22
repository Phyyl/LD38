#version 330

uniform mat4 model_matrix;
uniform mat4 view_matrix;
uniform mat4 projection_matrix;
uniform mat4 normal_matrix;

in vec3 vert_position;
in vec4 vert_color;
in vec2 vert_tex_coord;
in vec3 vert_normal;

out vec2 vert_frag_tex_coord;
out vec4 vert_frag_color;
out vec3 vert_frag_normal;

void main()
{
	gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vert_position, 1.0);
	
	vert_frag_tex_coord = vert_tex_coord;
	vert_frag_color = vert_color;
	vert_frag_normal = (normal_matrix * vec4(vert_normal, 1.0)).xyz;
}
