/*
{
  "CATEGORIES" : [
    "GLSLSandbox"
  ],
      "RESOURCE_TYPE": "Material For MadMapper",
  "CREDIT": "adapted by Jason Beyers",
  "DESCRIPTION" : "Converted from http:\/\/glslsandbox.com\/e#57634.0",
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
        "Label": "Cluster Size",
        "NAME": "cluster_size",
        "TYPE": "int",
        "MIN": 1,
        "MAX": 10,
        "DEFAULT": 4
    },
    {
        "Label": "Camera/Roll X",
        "NAME": "camera_roll_x",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Camera/Roll Y",
        "NAME": "camera_roll_y",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Camera/Roll Z",
        "NAME": "camera_roll_z",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },


    {
        "LABEL": "Color/Invert Cube",
        "NAME": "invert_cube",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Color/Edge Color",
        "NAME": "cube_edge_color",
        "TYPE": "color",
        "DEFAULT": [ 0.0, 1.0, 1.0, 1.0 ]
    },

    {
        "LABEL": "Color/Cube Color",
        "NAME": "cube_color",
        "TYPE": "color",
        "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ]
    },

    {
        "LABEL": "Color/Back Color1",
        "NAME": "background_color1",
        "TYPE": "color",
        "DEFAULT": [ 0.1, 0.1, 0.1, 1.0 ]
    },
    {
        "LABEL": "Color/Back Color2",
        "NAME": "background_color2",
        "TYPE": "color",
        "DEFAULT": [ 0.0, 0.0, 0.0, 0.0 ]
    },
    {
        "LABEL": "Color/Background Alpha",
        "NAME": "background_alpha",
        "TYPE": "bool",
        "DEFAULT": true,
        "FLAGS": "button"
    },

    {
        "LABEL": "Size/BPM Sync",
        "NAME": "mat_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Size/Reverse",
        "NAME": "mat_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Size/Speed",
        "NAME": "mat_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Size/Offset",
        "NAME": "mat_offset",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Size/Strob",
        "NAME": "mat_strob",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },

    {
        "LABEL": "Rotate/BPM Sync",
        "NAME": "mat_rot_bpm_sync",
        "TYPE": "bool",
        "DEFAULT": false,
        "FLAGS": "button"
    },
    {
        "LABEL": "Rotate/Reverse",
        "NAME": "mat_rot_reverse",
        "TYPE": "bool",
        "DEFAULT": 0,
        "FLAGS": "button"
    },
    {
        "LABEL": "Rotate/Speed",
        "NAME": "mat_rot_speed",
        "TYPE": "float",
        "MIN": 0.0,
        "MAX": 4.0,
        "DEFAULT": 1.0
    },
    {
        "Label": "Rotate/Offset",
        "NAME": "mat_rot_offset",
        "TYPE": "float",
        "MIN": -1.0,
        "MAX": 1.0,
        "DEFAULT": 0.0
    },
    {
        "Label": "Rotate/Strob",
        "NAME": "mat_rot_strob",
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
        "NAME": "mat_rot_time",
        "TYPE": "time_base",
        "PARAMS": {
            "speed": "mat_rot_speed",
            "speed_curve":2,
            "strob" : "mat_rot_strob",
            "reverse": "mat_rot_reverse",
            "bpm_sync": "mat_rot_bpm_sync",
            "link_speed_to_global_bpm":true
        }
    },
]

}
*/

#include "MadCommon.glsl"
// #include "MadNoise.glsl"
// #include "MadSDF.glsl"

float iTime = mat_time - mat_offset * 4.0;

float rot_time = mat_rot_time - mat_rot_offset * 10.;

bool intersects(vec3 ro, vec3 rd, vec3 box_min, vec3 box_max, out float t_intersection)
{
    float t_near = -1e6;
    float t_far = 1e6;

    vec3 normal = vec3(0.);

    for (int i = 0; i < 3; i++) {
        if (rd[i] == 0.) {
            // ray is parallel to plane
            if (ro[i] < box_min[i] || ro[i] > box_max[i])
                return false;
        } else {
            vec2 t = vec2(box_min[i] - ro[i], box_max[i] - ro[i])/rd[i];

            if (t[0] > t[1])
                t = t.yx;

            t_near = max(t_near, t[0]);
            t_far = min(t_far, t[1]);

            if (t_near > t_far || t_far < 0.)
                return false;
        }
    }

    t_intersection = t_near;

    return true;
}

mat3 camera(vec3 e, vec3 la) {
    vec3 roll = vec3(camera_roll_x, camera_roll_y, camera_roll_z);
    vec3 f = normalize(la - e);
    vec3 r = normalize(cross(roll, f));
    vec3 u = normalize(cross(f, r));

    return mat3(r, u, f);
}

vec4 materialColorForPixel(vec2 texCoord) {

    vec2 uv = texCoord - vec2(0.5);
    uv *= mat_zoom;

    float a = .75*rot_time;

    vec3 ro = 8.0*vec3(cos(a), 1.0, -sin(a));
    vec3 rd = camera(ro, vec3(0))*normalize(vec3(uv, 2.));

    const float INFINITY = 1e6;

    float t_intersection = INFINITY;

    // const float cluster_size = 4.;
    float inside = 0.;

    for (float i = 0.; i < cluster_size; i++) {
        for (float j = 0.; j < cluster_size; j++) {
            for (float k = 0.; k < cluster_size; k++) {
                vec3 p = 1.75*(vec3(i, j, k) - .5*vec3(cluster_size - 1.));
        float l = length(p);

                float s = .6*(.5 + .5*sin(.25*iTime*4.*PI - 3.5*l));

                float t = 0.;

                if (intersects(ro, rd, p - vec3(s), p + vec3(s), t) && t < t_intersection) {
                    t_intersection = t;

                    vec3 n = ro + rd*t_intersection - p;

                    const float EPSILON = .05;
                    vec3 normal = step(vec3(s - EPSILON), n) + step(vec3(s - EPSILON), -n);

                    inside = step(2., normal.x + normal.y + normal.z);
                }
            }
        }
    }

    vec4 c;


    if (t_intersection == INFINITY) {
        c = mix(background_color1, background_color2, .5*length(uv));

        if (background_alpha) {
            c.a = 1.0;
        } else {
            c.a = 0.;
        }
    } else {
        // c = inside*vec4(0., 2., 3., 1.);
        // c = inside*cube_color;

        if (invert_cube) {
            c = mix(cube_edge_color, cube_color, inside);
        } else {
            c = mix(cube_color, cube_edge_color, inside);
        }



    }

    return c;




}