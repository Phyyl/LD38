#version 330

uniform sampler2D texture_0;
uniform bool textured;

in vec2 vert_frag_tex_coord;
in vec4 vert_frag_color;
in vec3 vert_frag_normal;

out vec4 frag_color;

void main()
{
	vec4 texture_sample = textured ? texture(texture_0, vert_frag_tex_coord) : vec4(1.0, 1.0, 1.0, 1.0);
	frag_color = texture_sample * vert_frag_color;
}
