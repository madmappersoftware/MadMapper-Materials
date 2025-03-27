/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#57588.0",
  "VSN": "1.1",
  "INPUTS" : [

    {
        "Label": "Zoom",
        "NAME": "mat_zoom",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 5.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Width",
        "NAME": "mat_width",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.2
    },
    {
        "Label": "Layers",
        "NAME": "layers",
        "TYPE": "int",
        "MIN": 8,
        "MAX": 12,
        "DEFAULT": 10
    },
    {
        "Label": "Path",
        "NAME": "mat_path",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.5
    },
    {
        "LABEL": "Mask1",
        "NAME": "mat_mask1",
        "TYPE": "bool",
        "DEFAULT": true,
        "FLAGS": "button"
    },
    {
        "LABEL": "Mask2",
        "NAME": "mat_mask2",
        "TYPE": "bool",
        "DEFAULT": true,
        "FLAGS": "button"
    },
    {
        "LABEL": "Move/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Move/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Move/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Move/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Move/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "LABEL": "Color/Scale",
        "NAME": "color_scale",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "LABEL": "Color/BPM Sync",
        "NAME": "mat_color_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Color/Reverse",
        "NAME": "mat_color_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Color/Speed",
        "NAME": "mat_color_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Color/Offset",
        "NAME": "mat_color_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Color/Strob",
        "NAME": "mat_color_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
  ],
  "GENERATORS": [
    {
        "NAME": "mat_move_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_speed",
            "speed_curve":2,
            "strob" : "mat_strob",
            "reverse": "mat_reverse",
            "bpm_sync": "mat_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },
    {
        "NAME": "mat_color_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_color_speed",
            "speed_curve":2,
            "strob" : "mat_color_strob",
            "reverse": "mat_color_reverse",
            "bpm_sync": "mat_color_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    }
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float move_time = mat_move_time - mat_offset * 10;

float color_time =  mat_color_time - mat_color_offset * 10;
/*
 * Original shader from: https://www.shadertoy.com/view/3scGWM
 */
// --------[ Original ShaderToy begins here ]---------- //
// FYI: LEFT:37  UP:38  RIGHT:39  DOWN:40   PAGEUP:33  PAGEDOWN:34  END : 35  HOME: 3

float rand(vec2 p)
{
    p = fract(p*vec2(234.51,124.89));
    p += dot(p,p+54.23);
    p = fract(p*vec2(121.80,456.12));
    p += dot(p,p+25.12);
    return fract(p.x);
}

float width = mat_width;


vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv_orig = texCoord - vec2(0.5);
    vec2 uv_scaled = uv_orig * mat_zoom;

    vec2 pos = 5.*vec2(sin(move_time*0.2)+0.1*move_time,cos(move_time*0.2)+0.1*move_time);
    vec3 col = vec3(0.);//0.5 + 0.5*cos(color_time+(uv_orig).xyx+vec3(0,2,4));

    for(float i=5.;i<layers;i+=1.)
    {
        vec2 uv = pos+((20.-1.8*i)*uv_scaled);

        vec2 gv = (fract(uv)-0.5);
        vec2 id = floor(uv);
        // float color_scale = 0.15;
        // vec3 col2 = (0.5 + 0.2*sin(color_time+(i/2.)+color_scale*0.3*uv.xyx+vec3(0,2,4))*sin(color_time+(i/2.)+color_scale*0.3*uv.xyx+vec3(0,2,4)) + 0.5*cos(color_time+(i/2.)+color_scale*0.3*uv.xyx+vec3(0,2,4)))*(i+1.)/11.;

        vec3 col2 = (0.5 + 0.2*sin(color_time+(i/2.)+color_scale*0.3*uv.xyx+vec3(0,2,4))*sin(color_time+(i/2.)+color_scale*0.3*uv.xyx+vec3(0,2,4)) + 0.5*cos(color_time+(i/2.)+color_scale*0.3*uv.xyx+vec3(0,2,4)))*(i+1.)/11.;
        // col2 = vec3(0.7);

        gv.x *= (float(rand(id*i)>mat_path)-0.5)*2.;

        float mask1 = 0.;
        float mask2 = 1.;

        float mask1_tmp = smoothstep(-0.01,0.01,width-abs(gv.x+gv.y-0.5*sign(gv.x+gv.y+0.01)));

        if (mat_mask1) {
            mask1 = mask1_tmp;
        }
        if (mat_mask2) {
            mask2 = smoothstep(-0.2,0.2,width-abs(gv.x+gv.y-0.5*sign(gv.x+gv.y+0.01)));
        }

        // Output to screen
        // col = - 0.3*mask2 + 0.5*(col2.r*col2.r+col2.g*col2.g+col2.b*col2.b + col2*col2)*col2*mask1_tmp + col*(1.-mask1);
        col = - 0.3*mask2 + 0.5*(col2.r*col2.r+col2.g*col2.g+col2.b*col2.b + col2*col2)*col2*mask1_tmp + col*(1.-mask1);
    }
    return vec4(col,1.0);




}