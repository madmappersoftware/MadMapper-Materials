/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nadapted from Shadertoy",
    "TAGS": "texture",
    "INPUTS": [
		{ "Label": "Cutoff", "NAME": "cutoff", "TYPE": "float", "MIN": 0.05, "MAX": 1.0, "DEFAULT": 0.2 },
		{ "Label": "Noise Speed", "NAME": "speed", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.7 },
		{ "Label": "Noise Power", "NAME": "noisePower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "Label": "Noise Scale", "NAME": "noiseScale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 2.3 },
		{ "Label": "R.Speed.x", "NAME": "speed_x", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.0 },
		{ "Label": "R.Speed.y", "NAME": "speed_y", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.0 },
		{ "LABEL": "Cam/Zoom", "NAME": "zoom", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
		{ "LABEL": "Cam/Orbit", "NAME": "uCam", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ],
		"DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Cam/Light XY", "NAME": "uLight", "TYPE": "point2D", "MAX": [ 1.0, 1.0 ], "MIN": [ -1.0, -1.0 ],
		"DEFAULT": [ 0.0, 0.0 ] },
		{ "LABEL": "Color/Color A", "NAME": "color_a", "TYPE": "color", "DEFAULT": [ 1.0, 0.1, 0.0, 1.0 ] },
		{ "LABEL": "Color/Color B", "NAME": "color_b", "TYPE": "color", "DEFAULT": [ 1.0, 0.7, 0.0, 1.0 ] },
		{ "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
      ],
	  
	"GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", 
		"PARAMS": {"speed": "speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
		{"NAME": "animation_x", "TYPE": "time_base", 
		"PARAMS": {"speed": "speed_x", "speed_curve":2, "link_speed_to_global_bpm":true}},
		{"NAME": "animation_y", "TYPE": "time_base", 
		"PARAMS": {"speed": "speed_y", "speed_curve":2, "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
const float delta  = 0.02;

float sphere(vec3 position, float r)
{
    return length(position) - r ;  
//	return length(position) - r - 0.53*sin(position.y*1.1 );
	
	return length(position) - r + 0.53*sin(position.y*1.1 + mod(animation_time*0.2, 2.0*PI)-PI)
        + 0.17*sin(position.z*2.2 + mod(animation_time*0.02, 2.0*PI)-PI);
}


vec3 rotateX(vec3 pos, float alpha) {
	mat4 trans= mat4(1.0, 0.0, 0.0, 0.0,
				0.0, cos(alpha), -sin(alpha), 0.0,
				0.0, sin(alpha), cos(alpha), 0.0,
				0.0, 0.0, 0.0, 1.0);
				
				
	return vec3(trans * vec4(pos, 1.0));
}

vec3 rotateY(vec3 pos, float alpha) {

				
	mat4 trans2= mat4(cos(alpha), 0.0, sin(alpha), 0.0,
				0.0, 1.0, 0.0, 0.0,
				-sin(alpha), 0.0, cos(alpha), 0.0,
				0.0, 0.0, 0.0, 1.0);
				
	return vec3(trans2 * vec4(pos, 1.0));
}

vec3 translate(vec3 position, vec3 translation) {
	return position - translation;
}


float sdTorus( vec3 p, vec2 t )
{
  vec2 q = vec2(length(p.xz)-t.x,p.y);
  return length(q)-t.y;
}

float udRoundBox( vec3 p, vec3 b, float r )
{
  return length(max(abs(p)-b,0.0))-r;
}

float sdSphere( vec3 p, float s )
{
  return length(p)-s;
}

float opS( float d1, float d2 )
{
    return max(-d1,d2);
}

float opRep( vec3 p, vec3 c )
{
    vec3 q = mod(p,c)-0.5*c;
    return sdSphere( q ,0.5);
}

	vec3 opRep2( vec3 p, vec3 c )
{
   return mod(p,c)-0.5*c;

}


float opU( float d1, float d2 )
{
    return min(d1,d2);
}

vec3 opTwist( vec3 p )
{
    float c = cos(sin(animation_time)*2.0*p.y);
    float s = sin(sin(animation_time)*2.0*p.y);
    mat2  m = mat2(c,-s,s,c);
    vec3  q = vec3(m*p.xz,p.y);
    return q;
}


vec4 permute(vec4 x) {
     return mod289(((x*34.0)+1.0)*x);
}

vec4 taylorInvSqrt(vec4 r)
{
  return 1.79284291400159 - 0.85373472095314 * r;
}

float taylorInvSqrt(float r)
{
  return 1.79284291400159 - 0.85373472095314 * r;
}

vec4 grad4(float j, vec4 ip)
  {
  const vec4 ones = vec4(1.0, 1.0, 1.0, -1.0);
  vec4 p,s;

  p.xyz = floor( fract (vec3(j) * ip.xyz) * 7.0) * ip.z - 1.0;
  p.w = 1.5 - dot(abs(p.xyz), ones.xyz);
  s = vec4(lessThan(p, vec4(0.0)));
  p.xyz = p.xyz + (s.xyz*2.0 - 1.0) * s.www;

  return p;
  }

// (sqrt(5) - 1)/4 = F4, used once below
#define F4 0.309016994374947451

float snoise(vec4 v)
  {
  const vec4 C = vec4( 0.138196601125011, // (5 - sqrt(5))/20 G4
                        0.276393202250021, // 2 * G4
                        0.414589803375032, // 3 * G4
                       -0.447213595499958); // -1 + 4 * G4

// First corner
  vec4 i = floor(v + dot(v, vec4(F4)) );
  vec4 x0 = v - i + dot(i, C.xxxx);

// Other corners

// Rank sorting originally contributed by Bill Licea-Kane, AMD (formerly ATI)
  vec4 i0;
  vec3 isX = step( x0.yzw, x0.xxx );
  vec3 isYZ = step( x0.zww, x0.yyz );
// i0.x = dot( isX, vec3( 1.0 ) );
  i0.x = isX.x + isX.y + isX.z;
  i0.yzw = 1.0 - isX;
// i0.y += dot( isYZ.xy, vec2( 1.0 ) );
  i0.y += isYZ.x + isYZ.y;
  i0.zw += 1.0 - isYZ.xy;
  i0.z += isYZ.z;
  i0.w += 1.0 - isYZ.z;

  // i0 now contains the unique values 0,1,2,3 in each channel
  vec4 i3 = clamp( i0, 0.0, 1.0 );
  vec4 i2 = clamp( i0-1.0, 0.0, 1.0 );
  vec4 i1 = clamp( i0-2.0, 0.0, 1.0 );

  vec4 x1 = x0 - i1 + C.xxxx;
  vec4 x2 = x0 - i2 + C.yyyy;
  vec4 x3 = x0 - i3 + C.zzzz;
  vec4 x4 = x0 + C.wwww;

// Permutations
  i = mod289(i);
  float j0 = permute( permute( permute( permute(i.w) + i.z) + i.y) + i.x);
  vec4 j1 = permute( permute( permute( permute (
             i.w + vec4(i1.w, i2.w, i3.w, 1.0 ))
           + i.z + vec4(i1.z, i2.z, i3.z, 1.0 ))
           + i.y + vec4(i1.y, i2.y, i3.y, 1.0 ))
           + i.x + vec4(i1.x, i2.x, i3.x, 1.0 ));

// Gradients: 7x7x6 points over a cube, mapped onto a 4-cross polytope
// 7*7*6 = 294, which is close to the ring size 17*17 = 289.
  vec4 ip = vec4(1.0/294.0, 1.0/49.0, 1.0/7.0, 0.0) ;

  vec4 p0 = grad4(j0, ip);
  vec4 p1 = grad4(j1.x, ip);
  vec4 p2 = grad4(j1.y, ip);
  vec4 p3 = grad4(j1.z, ip);
  vec4 p4 = grad4(j1.w, ip);

// Normalise gradients
  vec4 norm = taylorInvSqrt(vec4(dot(p0,p0), dot(p1,p1), dot(p2, p2), dot(p3,p3)));
  p0 *= norm.x;
  p1 *= norm.y;
  p2 *= norm.z;
  p3 *= norm.w;
  p4 *= taylorInvSqrt(dot(p4,p4));

	  
// Mix contributions from the five corners
  vec3 m0 = max(0.6 - vec3(dot(x0,x0), dot(x1,x1), dot(x2,x2)), 0.0);
  vec2 m1 = max(0.6 - vec2(dot(x3,x3), dot(x4,x4) ), 0.0);
  m0 = m0 * m0;
  m1 = m1 * m1;
  return 49.0 * ( dot(m0*m0, vec3( dot( p0, x0 ), dot( p1, x1 ), dot( p2, x2 )))
               + dot(m1*m1, vec2( dot( p3, x3 ), dot( p4, x4 ) ) ) ) ;

  }
/////////////////////////

float function(vec3 position) {

//	vec3 pos = rotateY(position,-animation_y);
	vec3 pos = rotateY(rotateX(position, animation_x),animation_y);
	float N = 	snoise(vec4(pos*noiseScale, animation_time*1.9)) * noisePower;
	return sphere(pos, 0.8)-0.21*N;	
}

vec3 ray(vec3 start, vec3 direction, float t) {
	return start + t * direction;
}

vec3 gradient(vec3 position) {

	return vec3(function(position + vec3(delta, 0.0, 0.0)) - function(position - vec3(delta, 0.0, 0.0)),
	function(position + vec3(0.0,delta, 0.0)) - function(position - vec3(0.0, delta, 0.0)),
	function(position + vec3(0.0, 0.0, delta)) - function(position - vec3(0.0, 0.0, delta)));	
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec3 lightPosition = vec3(uLight,-1.0);
	vec3 cameraPosition = vec3(uCam.x, uCam.y, -(2 - zoom));
	float width = 1024.0;
	float height  =1024.0;
	float aspect = 1.0;
	vec3 nearPlanePosition = vec3(texCoord*2.0 - vec2(1.0),0.0);							  
	vec3 viewDirection = normalize(nearPlanePosition - cameraPosition);
	
	float t = 0.0;
	float dist;
	vec3 position;
	vec4 color = vec4(vec3(0.,0.,0.0),1.0) ;
	vec3 normal;
	vec3 up = normalize(vec3(-0.0, 1.0,0.0));
	
	// fixed ray depth to 16
	float thresh = pow(cutoff,3.3) * 0.05;
	for(int i=0; i < 16; i++) {
		position = ray(cameraPosition,	viewDirection, t);
		dist = function(position);
		
		if(dist < thresh || i == 39) {
						
			normal = normalize(gradient(position));
			
			vec4 color1 = color_a;
			vec4 color2 = color_b;		
			vec4 color3 = mix(color2, color1, (1.0+dot(up, normal))/2.0);		
			color = color3*0.7 * max(dot(normal, normalize(lightPosition-position)),0.0) ;//+vec4(0.1,0.1,0.1,1.0);

			//specular
			vec3 E = normalize(cameraPosition - position);
			vec3 R = reflect(-normalize(lightPosition-position), normal);		
			float specular = pow( max(dot(R, E), 0.0),  39.0);	
			float alpha = 1.0-clamp( pow(length(position-vec3(0.0,0.0,1.0)),3.0)*0.0018,0.0, 1.0);

			color = vec4(color.xyz + color3.xyz*0.4, 1.0);
			color +=vec4(vec3(specular),0) ;
			// interation glow
			float col = 0.5;
			float alph = col/0.5;
			color += vec4((vec3(0.4, 0.1,0.8)*(1.0-alph)+
						  	vec3(0.2, 0.1,0.1)*alph)*0.8*pow(float(i)/32.0*1.0, 2.0) *1.0,1.0);
			
			break;
		}
		
			t = t + dist * 0.70;
	}
	
	//////// brightness + contrast
	// Apply contrast
    color.rgb = mix(vec3(0.5), color.rgb, contrast);
    // Apply brightness
    color.rgb += vec3(brightness);
	
	return color;

}
