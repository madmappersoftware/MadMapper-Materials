/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "Collectif Scale | Furtive Vision",
    "TAGS": "Eiffel",
    "DESCRIPTION": "Eiffel",
    "VSN": "1.6",
    "INPUTS": [  
        { "LABEL": "Reverse", "NAME": "reverse", "TYPE": "bool", "DEFAULT": 0, "FLAGS": "button" }, 
        // { "LABEL": "Speed", "NAME": "speed", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 0.5 },
        { "LABEL": "Density", "NAME": "density", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 },
        { "LABEL": "Decay", "NAME": "speed_decay", "TYPE": "float", "MIN": -5.0, "MAX": -1.0, "DEFAULT": -2 },
        { "LABEL": "Release", "NAME": "release", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 },
        { "LABEL": "Cell X", "NAME": "cell_x", "TYPE": "float", "MIN": 1.0, "MAX": 100.0, "DEFAULT": 48.0 },
        { "LABEL": "Cell Y", "NAME": "cell_y", "TYPE": "float", "MIN": 1.0, "MAX": 500.0, "DEFAULT": 81.0 },
        // { "LABEL": "Rotation", "NAME": "rotation", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0},
        { "LABEL": "Seed", "NAME": "mat_seed", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0},
    ],
    "GENERATORS": [
        {"NAME": "anim_time", "TYPE": "time_base", "PARAMS": {"speed": 1, "reverse": "reverse", "speed_curve": 2, "link_speed_to_global_bpm":true}},
        {"NAME": "decay_time", "TYPE": "time_base", "PARAMS": {"speed": "speed_decay", "speed_curve": 2, "link_speed_to_global_bpm":true}}
    ],
    "RASTERISATION_SETTINGS": {
        "DEFAULT_RENDER_TO_TEXTURE": true,
        "DEFAULT_WIDTH": 512,
        "DEFAULT_HEIGHT": 512,
        "DEFAULT_PIXEL_FORMAT": "PF_U8_BGRA",
        "REQUIRES_LAST_FRAME": true
    },
}*/

// #define NOISE_TEXTURE_BASED
#define SDF_ANTIALIASING_MEDIUM

#include "MadCommon.glsl"
#include "MadNoise.glsl"
#include "MadSDF.glsl"

float map(float value, float min1, float max1, float min2, float max2) {
  return min2 + (value - min1) * (max2 - min2) / (max1 - min1);
  // return clamp(min2 + (value - min1) * (max2 - min2) / (max1 - min1), min2, max2);
}



vec4 materialColorForPixel(vec2 texCoord) {

    // first grid
    vec2 posId;
    vec2 pos = texCoord; // 0. -> 1.
    vec3 seed;
    float release = pow(release, .1);
    vec2 cells = vec2(cell_x, cell_y);


    // speed = 
    float anim_time = anim_time;
    float density = density;
    anim_time = mix(anim_time, decay_time, 0.5);
    density = mix(density, (1. - (-speed_decay) / 5.), 0.2);

    

    //if with better performance
    // pos = rotate(pos,rotation * PI);
    vec2 is_more_than_2 = step(vec2(2.), cells);
    cells.x = is_more_than_2.x * cells.x + (1. - is_more_than_2.x) * map(cells.x, 1., 2., 0., 2.);
    cells.y = is_more_than_2.y * cells.y + (1. - is_more_than_2.y) * map(cells.y, 1., 2., 0., 2.);

    // vec4 lastFrame = IMG_NORMTHIS_PIXEL(mm_LastFrame);
    vec4 lastFrame = texture(mm_LastFrame,vec2(texCoord.xy));
    pos = repeat(pos, vec2( 1. / (cells.x), 1. / (cells.y)), posId);
    seed = vec3(posId.xy * 80., anim_time + (mat_seed * 1000.));
    float noise_1 = ridgedNoise(seed);


    // Apply contrast
    float n = noise_1;
    vec3 color = vec3(0.0);
    color += lastFrame.rgb * release;
    color += vec3(step(n, density));

    // color.rgb = vec3(posId.x * 0.1);

    if(color.r < 0.05) {
      color = vec3(0.);
    }



    // Apply brightness
    // color += vec3(brightness);

    return vec4(color, 1.0);
}
