/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "jeyko",
    "DESCRIPTION": "Liquid Like noise",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 }, 
{"LABEL": "Turbulence", "NAME": "mat_turb", "TYPE": "bool",  "DEFAULT": true}, 

{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button" }, 
{ "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ], "FLAGS": "no_alpha" },
{ "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ], "FLAGS": "no_alpha" }, 
{ "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
{ "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1. },
    ],
	"GENERATORS": [
{"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/

#define rot(a) mat2(cos(a),-sin(a),sin(a),cos(a))
#define pi acos(-1.)
#define pmod(p,a) mod(p - 0.5*(a),(a)) - 0.5*(a)

mat3 getOrthogonalBasis(vec3 direction){
    direction = normalize(direction);
    vec3 right = normalize(cross(vec3(0,1,0),direction));
    vec3 up = normalize(cross(direction, right));
    return mat3(right,up,direction);
}
float cyclicNoise(vec3 p, bool turbulent, float time){
    float noise = 0.;
    
    p.yz *= rot(0.5);
    float amp = 1.;
    float gain = 0.9 + sin(p.z*0.2)*0.2;
    const float lacunarity = 1.6;
    const int octaves = 5;
    
    const float warp = 2.2;    
    float warpTrk = 1.5 ;
    const float warpTrkGain = .2;
    
    vec3 seed = vec3(-4,-2.,0.5);
    mat3 rotMatrix = getOrthogonalBasis(seed);
    
    for(int i = 0; i < octaves; i++){
        
        p += sin(p.zxy*warpTrk + vec3(0,-time*2.,0) - 2.*warpTrk)*warp; 
        noise += sin(dot(cos(p), sin(p.zxy + vec3(0,time*0.3,0))))*amp;
    
        p *= rotMatrix;
        p *= lacunarity;
        
        warpTrk *= warpTrkGain;
        amp *= gain;
    }
    
    if(turbulent){
        return 1. - abs(noise)*0.5;
    
    }{
        return (noise*0.25 + 0.5);

    }
}

vec4 materialColorForPixel( vec2 texCoord )
{
	vec2 uv = texCoord *2. -1.;
	float n = cyclicNoise(vec3(uv*mat_scale,mat_time),mat_turb, 0.);

   // Apply brightness
    n += mat_brightness;
    // Apply contrast
    n = mix(0.5, n, mat_contrast);
	n = clamp(n,0.,1.);
	if (mat_invert) n = 1 - n;

	vec3 color = mix( mat_backgroundColor.rgb, mat_foregroundColor.rgb, n );
	return vec4(color,1.);
}