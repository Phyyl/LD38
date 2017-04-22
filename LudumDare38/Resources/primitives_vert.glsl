#version 330

uniform mat4 mvp_matrix;

in vec3 vert_position;
in vec4 vert_color;
in vec2 vert_tex_coord;
in vec3 vert_normal;

out vec2 vert_frag_tex_coord;
out vec4 vert_frag_color;

void main()
{
	gl_Position = mvp_matrix * vec4(vert_position, 1.0);
	
	vert_frag_tex_coord = vert_tex_coord;
	vert_frag_color = vert_color;
}
