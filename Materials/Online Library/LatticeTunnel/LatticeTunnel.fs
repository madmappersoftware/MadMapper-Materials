/*{
    "CREDIT": "Shadertoy wtlGRX\nby Cewein",
    "DESCRIPTION": "SDF tunnel",
    "TAGS": "tunnel",
    "VSN": "1.1",
    "INPUTS": [ 
		{ "LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.5 }, 

		{ "LABEL": "Offset", "NAME": "mat_offset", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },

		{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Tint", "NAME": "mat_tint", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },
		{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" },
      ],
	 "GENERATORS": [
        {"NAME": "mat_animation_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed", "speed_curve":2,"bpm_sync": "mat_bpm_sync", "link_speed_to_global_bpm":true}},
    ],

}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

const int MAX_MARCHING_STEPS = 48;
const float MIN_DIST = 0.0;
const float MAX_DIST = 200.0;
const float EPSILON = 0.001;
const float RADIANT = 0.0174533;


float intersectSDF(float distA, float distB) {
    return max(distA, distB);
}

float unionSDF(float distA, float distB) {
    float k = .5;
    return smin(distA, distB, k);
}

float differenceSDF(float distA, float distB) {
    return max(distA, -distB);
}

mat4 rotationMatrix(vec3 axis, float angle)
{
    axis = normalize(axis);
    float s = sin(angle);
    float c = cos(angle);
    float oc = 1.0 - c;
    
    return mat4(oc * axis.x * axis.x + c,           oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s,  0.0,
                oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           oc * axis.y * axis.z - axis.x * s,  0.0,
                oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c,           0.0,
                0.0,                                0.0,                                0.0,                                1.0);
}

float sphereSDF(vec3 samplePoint, float p, float s) {
    return length(samplePoint - p) - s;
}

float cylinderSDF( vec3 p, vec3 c )
{
  return length(p.xz-c.xy)-c.z;
}

float torusSDF( vec3 p, vec2 t )
{
  vec2 q = vec2(length(p.xz)-t.x,p.y);
  return length(q)-t.y;
}

float CappedCylinderSDF( vec3 p, vec2 h )
{
  vec2 d = abs(vec2(length(p.xz),p.y)) - h;
  return min(max(d.x,d.y),0.0) + length(max(d,0.0));
}

float sceneSDF(vec3 samplePoint) {
    
   	float modu = 4.7;
    
    samplePoint = mod(samplePoint, modu);
//	samplePoint = (rotationMatrix(vec3(0.,0.,1.), cos(samplePoint.z) * RADIANT) * vec4(samplePoint,1.)).xyz;
    
    float result = differenceSDF(sphereSDF(samplePoint, modu/2., .75),torusSDF(samplePoint - modu/2. + vec3(0.,sin(mat_animation_time),0.), vec2(0.,0.)));
    result = unionSDF(result, CappedCylinderSDF( samplePoint - modu/2., vec2(0.25,50.)));
    
    vec4 rot = rotationMatrix(vec3(1.,0.,0.), 90. * RADIANT) * vec4(samplePoint- modu/2.,1.);
   	result = unionSDF(result, CappedCylinderSDF( rot.xyz, vec2(0.25,50.)));
    
    rot = rotationMatrix(vec3(0.,0.,1.), 90. * RADIANT) * vec4(samplePoint- modu/2.,1.);
   	result = unionSDF(result, CappedCylinderSDF( rot.xyz, vec2(0.25,50.)));
    
    return result;
}


float shortestDistanceToSurface(vec3 eye, vec3 marchingDirection, float start, float end) {
    float depth = start;
    for (int i = 0; i < MAX_MARCHING_STEPS; i++) {
        float dist = sceneSDF(eye + depth * marchingDirection);
        if (dist < EPSILON) {
			return depth;
        }
        depth += dist;
        if (depth >= end) {
            return end;
        }
    }
    return end;
}
            
vec3 rayDirection(float fieldOfView, vec2 size, vec2 fragCoord) {
    vec2 xy = fragCoord - size / 2.0;
    float z = size.y / tan(radians(fieldOfView) / 2.0);
    return normalize(vec3(xy, -z));
}


vec3 estimateNormal(vec3 p) {
    return normalize(vec3(
        sceneSDF(vec3(p.x + EPSILON, p.y, p.z)) - sceneSDF(vec3(p.x - EPSILON, p.y, p.z)),
        sceneSDF(vec3(p.x, p.y + EPSILON, p.z)) - sceneSDF(vec3(p.x, p.y - EPSILON, p.z)),
        sceneSDF(vec3(p.x, p.y, p.z  + EPSILON)) - sceneSDF(vec3(p.x, p.y, p.z - EPSILON))
    ));
}


vec3 phongContribForLight(vec3 k_d, vec3 k_s, float alpha, vec3 p, vec3 eye,
                          vec3 lightPos, vec3 lightIntensity) {
    vec3 N = estimateNormal(p);
    vec3 L = normalize(lightPos - p);
    vec3 V = normalize(eye - p);
    vec3 R = normalize(reflect(-L, N));
    
    float dotLN = dot(L, N);
    float dotRV = dot(R, V);
    
    if (dotLN < 0.0) {
        // Light not visible from this point on the surface
        return vec3(0.0, 0.0, 0.0);
    } 
    
    if (dotRV < 0.0) {
        // Light reflection in opposite direction as viewer, apply only diffuse
        // component
        return lightIntensity * (k_d * dotLN);
    }
    return lightIntensity * (k_d * dotLN + k_s * pow(dotRV, alpha));
}

vec3 phongIllumination(vec3 k_a, vec3 k_d, vec3 k_s, float alpha, vec3 p, vec3 eye) {
    const vec3 ambientLight = 0.6 * vec3(1.0, 1.0, 1.0);
    vec3 color = ambientLight * k_a;
    
    vec3 light1Pos = vec3(0., 0.0, 10.0 - (mat_animation_time * 5.));
    
    vec3 light1Intensity = vec3(0.5, 0.5, 0.5);
    
    color += phongContribForLight(k_d, k_s, alpha, p, eye,
                                  light1Pos,
                                  light1Intensity);
  
    return color;
}
	

vec4 materialColorForPixel( vec2 texCoord )
{
	// get texture coordinates
	vec2 uv = texCoord;

	vec3 dir = rayDirection(90.0, vec2(1024.), uv*1024.);
    vec3 eye = vec3(mat_offset, 15.0 - (mat_animation_time * 5.));
    float dist = shortestDistanceToSurface(eye, dir, MIN_DIST, MAX_DIST);
    vec3 color = vec3(0);

    if (dist < MAX_DIST - EPSILON) {
    // The closest point on the surface to the eyepoint along the view ray
    vec3 p = eye + dist * dir;
    
	// light colors: ambiant, diffuse, specular
    vec3 K_a = vec3(0.5, 0.5, 0.5);
    vec3 K_d = vec3(1., 1., 1.);
    vec3 K_s = vec3(.5, .5, .5);
    float shininess = 10.0;

	// lambert
    color = phongIllumination(K_a, K_d, K_s, shininess, p, eye);

	// fog
    color *= 1.0 - dist*0.05;
	}
	// Apply contrast
    color = mix(vec3(0.5), color, mat_contrast);
    // Apply brightness
    color += vec3(mat_brightness);
	// Apply Tint
	color *= mat_tint.rgb;
	// Apply Invert
	color = mix(color, vec3(1.0)-color,float(mat_invert));
		
	vec4 final_color = vec4(color,1.0);	
	return final_color;
}
