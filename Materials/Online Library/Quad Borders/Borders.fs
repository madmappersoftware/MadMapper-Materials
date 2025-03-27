/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Matt Beghin",
    "DESCRIPTION": "Quad borders draft, to be improved. Anyone ?",
    "TAGS": "graphic,border,quad",
    "VSN": "1.0",
    "CATEGORIES": [
        "Image Control"
    ],
    "INPUTS": [
        {
            "LABEL": "Outline",
            "NAME": "mat_outline",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.1
        },
        {
            "LABEL": "Adjust Ratio",
            "NAME": "adjust",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 2.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "Speed",
            "NAME": "speed",
            "TYPE": "float",
            "MIN" : -4.0,
            "MAX" : 4.0,
            "DEFAULT": 1.0
        },
        {
          "LABEL": "BPM Sync",
          "NAME": "bpmsync",
          "TYPE": "bool",
          "DEFAULT": false
        }
    ],
    "GENERATORS": [
        {
            "NAME": "animation_time",
            "TYPE": "time_base",
            "PARAMS": {"speed": "speed", "bpm_sync": "bpmsync", "speed_curve": 3}
        }
    ]
}*/


#define M_PI 3.1415926535897932384626433832795
#define SMOOTH(r,R) (1.0-smoothstep(R-1.0,R+1.0, r))

vec4 materialColorForPixel(vec2 texCoord)
{
    vec2 uv = texCoord;
    uv = (uv * vec2(1,adjust)) + vec2(0,(1.0-adjust)/2.0);

    // bottom-left
    vec2 st = uv;    
    vec2 bl = smoothstep(vec2(mat_outline/2),vec2(mat_outline/2 + 0.005),st); 
    float pct = bl.x * bl.y;

    // top-right 
    vec2 tr = smoothstep(vec2(mat_outline/2),vec2(mat_outline/2 + 0.005),1.0-st);
    pct *= tr.x * tr.y;

	if (pct > 0) return vec4(0,0,0,1);
	
    // Animate
    vec2 vecFromCenter = texCoord-vec2(0.5,0.5);
    float angle = 2*M_PI+atan(vecFromCenter.y,vecFromCenter.x);
    float animAngle = 2*M_PI*fract(animation_time);
	float angleDist = mod(4*M_PI+(angle-animAngle),2*M_PI);
    float luma = 0.5 + 0.5 * sin(angleDist);

    return vec4(vec3(luma),1);
}
