/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Mad Team modified by ProjectileObjects",
    "DESCRIPTION": "Dunes plus rotation, mirror effects, and Kaleidoscope options",
    "VSN": "1.7",
    "TAGS": "graphic",
    "INPUTS": [
        { "LABEL": "Scale", "NAME": "scale", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 4.0 },
        { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN" : 0.0, "MAX" : 4.0, "DEFAULT": 1.0 },
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": false, "FLAGS": "button" },
        { "LABEL": "Mirror Horizontal", "NAME": "mirrorH", "TYPE": "bool", "DEFAULT": false },
        { "LABEL": "Mirror Vertical", "NAME": "mirrorV", "TYPE": "bool", "DEFAULT": false },
        { "LABEL": "Kaleidoscope", "NAME": "kaleidoscope", "TYPE": "bool", "DEFAULT": false },
        { "LABEL": "Kaleidoscope Speed", "NAME": "kaleidoscopeSpeed", "TYPE": "float", "DEFAULT": 1.0, "MIN": -100, "MAX": 100.0 },
        { "LABEL": "Rotation", "NAME": "rotation", "TYPE": "float", "DEFAULT": 0.0, "MIN": 0.0, "MAX": 360.0 },
        { "LABEL": "Front Color", "NAME": "foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ] },  
        { "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN": 1.0, "MAX": 3.0, "DEFAULT": 1 },
    ],
    "GENERATORS": [
        { "NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "reverse": "reverse", "speed_curve": 3, "link_speed_to_global_bpm":true} }
    ]
}*/

vec2 hash( vec2 p )
{
  p = vec2(dot(p,vec2(127.1,311.7)),
           dot(p,vec2(269.5,183.3)));
  return -1. + 2.*fract(sin(p+20.)*53758.5453123);
}

float dunes_noise( in vec2 x )
{
  vec2 p = floor(x);
  vec2 f = fract(x);
  f = f*f*(3.0-2.0*f);
  vec2 uv = (p+vec2(37.0,17.0)) + f;
  vec2 rg = hash( uv/256.0 ).yx;
  return 0.5*mix( rg.x, rg.y, 0.5 );
}

#define GABOR_BLOBS_NB 10       
#define GABOR_BLOBS_SIZE 0.25   

float rnd(int i, int j)
{
  return dunes_noise(vec2(i, j));
}

float DuneStripes (vec2 uv, float d, float freq, float time)
{
  float hv = 0.;
  for (int i=0; i<GABOR_BLOBS_NB; i++) 
  {
    vec2 pos = vec2(rnd(i,0), rnd(i,1));
    vec2 dir = (.15+d)*vec2(rnd(i,2),rnd(i,3)) - d;
    hv += GABOR_BLOBS_SIZE * sin(dot(uv-pos, freq*dir) * 6. + time);
  }
  return hv;
}

vec4 materialColorForPixel(vec2 texCoord)
{
  vec2 uv = vec2(0.5,0.5) + (texCoord-vec2(0.5,0.5)) * scale;

  // Apply mirror effect if enabled
  if (mirrorH) {
    uv.y = abs(uv.y - 0.5) * 2.0;
  }

  if (mirrorV) {
    uv.x = abs(uv.x - 0.5) * 2.0;
  }

  // Apply kaleidoscope effect if enabled
  if (kaleidoscope) {
    float kaleidoAngle = mod(animation_time * kaleidoscopeSpeed, 360.0);
    float angle = radians(kaleidoAngle);
    
    vec2 centeredUV = uv - 0.5;
    float dist = length(centeredUV);
    float centerAngle = atan(centeredUV.y, centeredUV.x);
    centerAngle += angle;
    centerAngle = mod(centerAngle, radians(360.0));
    uv = vec2(cos(centerAngle), sin(centerAngle)) * dist + 0.5;
  }

  // Apply rotation
  float rotationAngle = radians(rotation);
  mat2 rotMat = mat2(cos(rotationAngle), sin(rotationAngle), -sin(rotationAngle), cos(rotationAngle));
  uv = rotMat * (uv - 0.5) + 0.5;

  float h = DuneStripes(uv, -.5, 10, - 3.5*animation_time);
  vec3 color = foregroundColor.rgb * clamp(h, 0.0, 1.0);

  // Apply contrast
  color = mix(vec3(0.5), color, contrast);

  // Apply brightness
  color += vec3(brightness);

  return vec4(color, 1);
}
