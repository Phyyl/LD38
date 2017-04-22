#version 330

#define AMBIENT 0.1
#define LIGHT_STRENGHT (1.0 - AMBIENT)

uniform sampler2D texture_0;
uniform bool textured;
uniform vec3 light_direction;

in vec2 vert_frag_tex_coord;
in vec4 vert_frag_color;
in vec3 vert_frag_normal;

out vec4 frag_color;

void main()
{
	vec4 texture_sample = textured ? texture(texture_0, vert_frag_tex_coord) : vec4(1.0, 1.0, 1.0, 1.0);

	float l = min(max(dot(normalize(vert_frag_normal), normalize(-light_direction)), 0.0), 1.0);
	l *= LIGHT_STRENGHT;
	l += AMBIENT;

	vec4 light = vec4(l, l, l, 1.0);
	frag_color = texture_sample * vert_frag_color * light;
}
