/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "CREDIT": "Original by balkhan, adapted by Jason Beyers",
  "VSN": "1.0",
    "INPUTS": [
        {
            "Label": "Zoom",
            "NAME": "mat_zoom",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 5.0,
            "DEFAULT": 1.0
        },
        {
            "LABEL": "BPM Sync",
            "NAME": "mat_bpm_sync",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "button"
        },
        {
            "LABEL": "Reverse",
            "NAME": "mat_reverse",
            "TYPE": "bool",
            "DEFAULT": 0,
            "FLAGS": "button"
        },
        {
            "LABEL": "Speed",
            "NAME": "mat_speed",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 4.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Offset",
            "NAME": "mat_offset",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },
        {
            "Label": "Strob",
            "NAME": "mat_strob",
            "TYPE": "float",
            "MIN": 0.0,
            "MAX": 1.0,
            "DEFAULT": 0.0
        },

        { "LABEL": "I Max", "NAME": "I_MAX", "TYPE": "float", "MIN": 20.0, "MAX": 400.0, "DEFAULT": 400.0 },

    { "LABEL": "Background On", "NAME": "background_on", "TYPE": "bool", "DEFAULT": 1, "FLAGS": "button" },


    { "LABEL": "Far", "NAME": "FAR", "TYPE": "float", "MIN": 10.0, "MAX": 500.0, "DEFAULT": 50.0 },


     { "LABEL": "Modifier A", "NAME": "mod_a", "TYPE": "float", "MIN": 0.0, "MAX": 3.0, "DEFAULT": 1.0 },

    { "LABEL": "Modifier B", "NAME": "mod_b", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },

    { "LABEL": "Modifier C", "NAME": "mod_c", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },

    { "LABEL": "Modifier D", "NAME": "mod_d", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },

    { "LABEL": "Modifier E", "NAME": "mod_e", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },

    { "LABEL": "Modifier F", "NAME": "mod_f", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 },
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
        }
    ],
  "DESCRIPTION" : "Automatically converted from https:\/\/www.shadertoy.com\/view\/lt2fDz by balkhan.  Another one",
}
*/


/*
* License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
* Created by bal-khan
*/

vec2    march(vec3 pos, vec3 dir);
vec3    camera(vec2 uv);
void    rotate(inout vec2 v, float angle);

float   t;          // time
vec3    ret_col;    // torus color
vec3    h;          // light amount

float iTime = mat_time - mat_offset * 10;

// #define I_MAX        400.
#define E           0.00001
// #define FAR          50.
// #define PI          3.14

// blackbody by aiekick : https://www.shadertoy.com/view/lttXDn

// -------------blackbody----------------- //

// return color from temperature
//http://www.physics.sfasu.edu/astro/color/blackbody.html
//http://www.vendian.org/mncharity/dir3/blackbody/
//http://www.vendian.org/mncharity/dir3/blackbody/UnstableURLs/bbr_color.html

vec3 blackbody(float Temp)
{
    vec3 col = vec3(255.);
    col.x = 56100000. * pow(Temp,(-3. / 2.)) + 148.;
    col.y = 100.04 * log(Temp) - 623.6;
    if (Temp > 6500.) col.y = 35200000. * pow(Temp,(-3. / 2.)) + 184.;
    col.z = 194.18 * log(Temp) - 1448.6;
    col = clamp(col, 0., 255.)/255.;
    if (Temp < 1000.) col *= Temp/1000.;
    return col;
}

// -------------blackbody----------------- //



float   scene(vec3 p)
{
    float   var;
    float   mind = 1e5;
    p.z += 10.;

    rotate(p.xz, 1.57-.5*iTime );
    rotate(p.yz, 1.57-.5*iTime );
    var = atan(p.x,p.y);
    vec2 q = vec2( ( length(p.xy) )-6.,p.z);
    rotate(q, var*.25+iTime*2.*0.);
    vec2 oq = q ;
    q = abs(q)-2.5;
    if (oq.x < q.x && oq.y > q.y)
        rotate(q, ( (var*1.)+iTime*0.)*3.14+iTime*0.);
    else
        rotate(q, ( .28-(var*1.)+iTime*0.)*3.14+iTime*0.);
    float   oldvar = var;
    ret_col = 1.-vec3(.350, .2, .3);
    mind = length(q)+.5+mod_a*1.05*(length(fract(mod_d*q*.5*(3.+3.*sin(oldvar*1. - iTime*2.)) )-.5)-1.215);
    h -= vec3(-3.20,.20,1.0)*vec3(1.)*mod_b*.0025/(.051+(mind-sin(oldvar*1. - iTime*2. + 3.14)*.125 )*(mind-sin(oldvar*1. - iTime*2. + 3.14)*.125 ) );
    h -= vec3(1.20,-.50,-.50)*mod_c*vec3(1.)*.025/(.501+(mind-sin(oldvar*1. - iTime*2.)*.5 )*(mind-sin(oldvar*1. - iTime*2.)*.5 ) );
    h += vec3(.25, .4, .5)*.0025/(.021+mind*mind);

    return (mind);
}

vec2    march(vec3 pos, vec3 dir)
{
    vec2    dist = vec2(0.0, 0.0);
    vec3    p = vec3(0.0, 0.0, 0.0);
    vec2    s = vec2(0.0, 0.0);

    float far = FAR;

    // if (!background_on) {
    //   far = 100000000000000.;
    // }

        for (float i = -1.; i < I_MAX; ++i)
        {
            p = pos + dir * dist.y;
            dist.x = scene(p);
            dist.y += dist.x*.2; // makes artefacts disappear
            // log trick by aiekick


            if (log(dist.y*dist.y/dist.x/1e5) > .0 || dist.x < E || (background_on && dist.y > FAR))
            {
                break;
            }
            s.x++;
    }
    s.y = dist.y;
    return (s);
}

// Utilities

void rotate(inout vec2 v, float angle)
{
    v = vec2(cos(angle)*v.x+sin(angle)*v.y,-sin(angle)*v.x+cos(angle)*v.y);
}

vec3    camera(vec2 uv)
{
    float       fov = 1.;
    vec3        forw  = vec3(0.0, 0.0, -1.0);
    vec3        right = vec3(1.0, 0.0, 0.0);
    vec3        up    = vec3(0.0, 1.0, 0.0);

    return (normalize((uv.x) * right + (uv.y) * up + fov * forw));
}

vec4 materialColorForPixel( vec2 texCoord ) {

    t  = iTime*.125;
    vec3    col = vec3(0., 0., 0.);

    vec2 uv  = texCoord - vec2(0.5);

    uv *= mat_zoom;

    vec3    dir = camera(uv);
    vec3    pos = vec3(.0, .0, 0.0);
    pos.z = mod_e*4.5+1.5*sin(t*10.);
    h*=0.;
    vec2    inter = (march(pos, dir));
    col.xyz = ret_col*(1.-inter.x*.0125);
    col += h * .4 * mod_f;
    return vec4(col,1.0);
}