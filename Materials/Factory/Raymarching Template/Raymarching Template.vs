const float PI = 3.14159265359;

flat out vec3 vro; // ray origin
out vec3 vrd; // ray direction

///// Utility

mat2 rotate (float a) 
{
	float ca = cos(a);
	float sa = sin(a);
	return mat2(ca, sa, -sa, ca);
}

///// Vertex function

void materialVsFunc (vec2 texCoord) 
{
	// configure the camera 
  	vro = vec3(0.0, -mat_offset.y * 2.0, 4.0);  
  	vro.xz *= rotate(mat_offset.x * PI);
 	vec3 ta = vec3(0.0);

	// calculate the ray direction (rd) for the current vertex
	// the vector will then be interpolated for each fragment by the GPU
  	vec3 forward = normalize(ta - vro); 
  	vec3 right = normalize(cross(forward, vec3(0.0, -1.0, 0.0))); 
  	vec3 up = normalize(cross(forward, right)); 

	vec2 uv = texCoord * 2.0 - 1.0; // center uvs

	vrd = vec3(right * uv.x + 
				up * uv.y + 
				forward * mat_fov);
}