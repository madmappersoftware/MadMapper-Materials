const float mat_PI = 3.14159265359;

flat out vec3 ro; // ray origin
out vec3 rd; // ray direction

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
  	ro = vec3(0.0, -mat_offset.y * mat_PI * 4., 4.0);  
  	ro.xz *= rotate(mat_offset.x * mat_PI);
 	vec3 ta = vec3(0.0);

	// calculate the ray direction (rd) for the current vertex
	// the vector will then be interpolated for each fragment by the GPU
  	vec3 forward = normalize(ta - ro); 
  	vec3 right = normalize(cross(forward, vec3(0.0, -1.0, 0.0))); 
  	vec3 up = normalize(cross(forward, right)); 

	vec2 uv = texCoord * 2.0 - 1.0; // center uvs

	rd = vec3(right * uv.x + 
				up * uv.y + 
				forward * mat_fov);
}