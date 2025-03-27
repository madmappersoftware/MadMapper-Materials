/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
  "CREDIT": "rbentos, adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from https://editor.isf.video/shaders/3976",
  "VSN": "1.0",
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
        "Label": "Rotate",
        "NAME": "mat_rotate",
        "TYPE": "float",
        "MIN": -1.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "Label": "Shift",
        "NAME": "mat_shift",
        "TYPE": "float",
        "MIN": -1.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },


    {
      "Label": "Scale1",
      "NAME" : "u_scale1",
      "TYPE" : "float",
      "MAX" : 20,
      "DEFAULT" : 17.484375,
      "MIN" : 1
    },
    {
      "Label": "Scale2",
      "NAME" : "u_scale2",
      "TYPE" : "float",
      "MAX" : 20,
      "DEFAULT" : 11.591263771057129,
      "MIN" : 1
    },
    {
      "Label": "Scale3",
      "NAME" : "u_scale3",
      "TYPE" : "float",
      "MAX" : 20,
      "DEFAULT" : 5.0493607521057129,
      "MIN" : 1
    },
    {
      "Label": "Apply Thresh",
      "NAME" : "u_apply_thresh",
      "TYPE" : "bool",
      "DEFAULT" : true
    },
    {
      "Label": "Thresh1",
      "NAME" : "u_thresh1",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.51025718450546265,
      "MIN" : 0
    },
    {
      "Label": "Thresh2",
      "NAME" : "u_thresh2",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.36108702421188354,
      "MIN" : 0
    },
    {
      "Label": "Thresh3",
      "NAME" : "u_thresh3",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 1,
      "MIN" : 0
    },
    {
      "Label": "Blob Offset",
      "NAME" : "u_offset",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.3082573413848877,
      "MIN" : 0
    },
    {
      "Label": "Color1",
      "NAME" : "u_color1",
      "TYPE" : "color",
      "DEFAULT" : [
        0.99705451726913452,
        0.92930144071578979,
        0,
        1
      ]
    },
    {
      "Label": "Color2",
      "NAME" : "u_color2",
      "TYPE" : "color",
      "DEFAULT" : [
        0.2166106253862381,
        0.94258779287338257,
        0.087304338812828064,
        1
      ]
    },
    {
      "Label": "Color3",
      "NAME" : "u_color3",
      "TYPE" : "color",
      "DEFAULT" : [
        0.91921168565750122,
        0.33995956182479858,
        0,
        1
      ]
    },
    {
        "LABEL": "Flow/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Flow/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Flow/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Flow/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Flow/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "LABEL": "Scroll/Animate",
        "NAME": "mat_scroll",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },

    {
        "LABEL": "Scroll/BPM Sync",
        "NAME": "mat_scroll_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scroll/Reverse",
        "NAME": "mat_scroll_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Scroll/Speed",
        "NAME": "mat_scroll_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 0.2
    },


    {
        "Label": "Scroll/Offset",
        "NAME": "mat_scroll_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "Label": "Scroll/Strob",
        "NAME": "mat_scroll_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

  ],
  "GENERATORS": [
    {
        "NAME": "mat_time",
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
        "NAME": "mat_scroll_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_scroll_speed",
            "speed_curve":2,
            "strob" : "mat_scroll_strob",
            "reverse": "mat_scroll_reverse",
            "bpm_sync": "mat_scroll_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 10;

vec2 random2(vec2 p) {
    return fract(sin(vec2(dot(p,vec2(127.1,311.7)),dot(p,vec2(269.5,183.3))))*43758.5453);
}

// cellular noise from Book Of Shaders
// https://thebookofshaders.com/edit.php?log=161127231150
float cellular(vec2 p,bool t,float thresh) {
    vec2 i_st = floor(p);
    vec2 f_st = fract(p);
    float m_dist = 10.0;
    for (int j=-1; j<=1; j++ ) {
        for (int i=-1; i<=1; i++ ) {
            vec2 neighbor = vec2(float(i),float(j));
            vec2 point = random2(i_st + neighbor);
            point = 0.5 + 0.5*sin(6.2831*point);
            vec2 diff = neighbor + point - f_st;
            float dist = length(diff);
            if( dist < m_dist ) {
                m_dist = dist;
            }
        }
    }
    //m_dist *= 2.0+sin((p.x+iTime*0.2)*3.0*3.1415)+cos((p.y+iTime*0.2)*3.0*3.1415);
    //m_dist *= 2.0+sin(p.x*3.1415)+cos(p.y*3.1415);
    m_dist *= 2.0+p.y+2.0+sin((p.y+iTime*0.5)*1.0*3.1415);
    if ( t ) {
        float p = 3.0/RENDERSIZE.y;
        m_dist = smoothstep(thresh-p,thresh+p,m_dist);
    }
    return 1.0-m_dist;
}

vec2 rotate2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

// --------------------------


vec4 materialColorForPixel(vec2 texCoord) {

    vec2 st = rotate2D(texCoord,PI*mat_rotate) - vec2(0.5);
    st *= mat_zoom;


    float scroll_time = mat_scroll_time - mat_scroll_offset * 10.;
    if (!mat_scroll) {
        scroll_time = 0.;
    }

    st.x -= scroll_time;

    st.y += mat_shift * 2.;

    vec3 source = vec3(st.x);

//  vec3 source = vec3(abs(st.x));
    float src = source.x;


    float c1 = cellular( st*u_scale1+u_offset*-1.0, u_apply_thresh, u_thresh1 );
    float c2 = cellular( st*u_scale2+u_offset*0.0, u_apply_thresh, u_thresh2 );
    float c3 = cellular( st*u_scale3+u_offset*1.0, u_apply_thresh, u_thresh3 );

    vec3 color = vec3(0);
    if ( c3 > 0.0)
        color = u_color3.xyz;
    else if ( c2 > 0.0)
        color = u_color2.xyz;
    else if ( c1 > 0.0)
        color = u_color1.xyz;

    //color = vec3(c1,c2,c3);

    return vec4(color, 1.0);




}