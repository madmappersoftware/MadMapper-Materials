/*{
    "CREDIT": "Mad Team modified by ProjectileObjects",
    "DESCRIPTION": "Clouds with Smooth Rotation, Reverse Speed, Kaleidoscope and Mirror.",
    "VSN": "2.0",
    "TAGS": "noise,clouds,smooth_rotation,mirror_function,kaleidoscope_effect",
    "INPUTS": [
        {
            "Label": "Speed",
            "NAME": "speed",
            "TYPE": "float",
            "MIN" : -5.0,
            "MAX" : 5.0,
            "DEFAULT": 1.0
        },
        {
            "Label": "Scale",
            "NAME": "scale",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 4.0
        },
        {
            "Label": "Contrast",
            "NAME": "kontrast",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.0,
            "MAX": 4.0
        },
        {
            "Label": "Parallax X",
            "NAME": "parallaxX",
            "TYPE": "float",
            "DEFAULT": 0.5,
            "MIN": -1,
            "MAX": 1
        },
        {
            "Label": "Parallax Y",
            "NAME": "parallaxY",
            "TYPE": "float",
            "DEFAULT": 0.5,
            "MIN": -1,
            "MAX": 1
        },
        {
            "Label": "Rotate",
            "NAME": "rotate",
            "TYPE": "float",
            "DEFAULT": 0.0,
            "MIN": 0,
            "MAX": 360
        },
        {
            "Label": "Mirror",
            "NAME": "mirror",
            "TYPE": "bool",
            "DEFAULT": true
        },
        {
            "Label": "Mirror H",
            "NAME": "mirrorH",
            "TYPE": "bool",
            "DEFAULT": true
        },
        {
            "Label": "Kaleidoscope",
            "NAME": "kaleidoscope",
            "TYPE": "bool",
            "DEFAULT": true
        },
        {
            "Label": "K Speed",
            "NAME": "kaleidoscopeSpeed",
            "TYPE": "float",
            "DEFAULT": 1.0,
            "MIN": 0.1,
            "MAX": 10.0
        }
    ],
    "GENERATORS": [
        {
            "NAME": "animation_time",
            "TYPE": "time_base",
            "PARAMS": {"speed": "speed", "link_speed_to_global_bpm":true}
        }
    ]
}*/

#define clouds_volsteps 8
#define clouds_stepsize 0.190
 
#define clouds_zoom 3.900
#define clouds_tile   0.450
 
#define clouds_distfading 0.560
#define clouds_saturation 0.400

#define cloud 0.2

float triangle(float x, float a)
{
    float output2 = 2.0 * abs(2.0 * ((x / a) - floor((x / a) + 0.5))) - 1.0;
    return output2;
}

float field(in vec3 p) {
    float strength = 7. + .03 * log(1.e-6 + fract(sin(animation_time) * 4373.11));
    float accum = 0.;
    float prev = 0.;
    float tw = 0.;
    

    for (int i = 0; i < 6; ++i) {
        float mag = dot(p, p);
        p = abs(p) / mag + vec3(-.5, -.8 + 0.1 * sin(animation_time * 0.2 + 2.0), -1.1 + 0.3 * cos(animation_time * 0.15));
        float w = exp(-float(i) / 7.);
        accum += w * exp(-strength * pow(abs(mag - prev), 2.3));
        tw += w;
        prev = mag;
    }
    return max(0., 5. * accum / tw - .7);
}

vec4 materialColorForPixel(vec2 texCoord) {
    //get coords and direction
    vec2 uv = vec2(0.5, 0.5) + (texCoord - vec2(0.5, 0.5)) * scale;
               
    float a_xz = 0.9;
    float a_yz = -0.6;
    float a_xy = 0.9 + animation_time * 0.04;
    float a_rot = radians(rotate);
        
    mat2 rot_xz = mat2(cos(a_xz), sin(a_xz), -sin(a_xz), cos(a_xz));   
    mat2 rot_yz = mat2(cos(a_yz), sin(a_yz), -sin(a_yz), cos(a_yz));       
    mat2 rot_xy = mat2(cos(a_xy), sin(a_xy), -sin(a_xy), cos(a_xy));
    
    // Smooth rotation interpolation
    float prev_rotate = 0.0; // Initialize previous rotation angle
    float a_rot_smooth = mix(prev_rotate, a_rot, 0.2); // You can adjust the interpolation factor (0.2) as needed
    mat2 rot = mat2(cos(a_rot_smooth), sin(a_rot_smooth), -sin(a_rot_smooth), cos(a_rot_smooth));
    
    // Update previous rotation angle
    prev_rotate = a_rot;
    
    // Apply horizontal mirror if enabled
    if (mirrorH) {
        // Mirror the texture about the center horizontally
        if (uv.y > 0.5) {
            uv.y = 1.0 - uv.y;
        }
    }

    // Apply vertical mirror if enabled
    if (mirror) {
        // Mirror the texture about the center vertically
        if (uv.x > 0.5) {
            uv.x = 1.0 - uv.x;
        }
    }
    
    // Apply kaleidoscope effect if enabled
    if (kaleidoscope) {
        // Calculate kaleidoscope angle
        float kaleidoAngle = mod(animation_time * kaleidoscopeSpeed, 360.0);
        float angle = radians(kaleidoAngle);
        
        // Adjust UV coordinates to center
        vec2 centeredUV = uv - 0.5;
        
        // Calculate distance from center
        float dist = length(centeredUV);
        
        // Calculate angle from center
        float centerAngle = atan(centeredUV.y, centeredUV.x);
        
        // Adjust angle for kaleidoscope effect
        centerAngle += angle;
        
        // Wrap angle within 360 degrees
        centerAngle = mod(centerAngle, radians(360.0));
        
        // Adjust UV coordinates for kaleidoscope effect around the center
        uv = vec2(cos(centerAngle), sin(centerAngle)) * dist + 0.5;
    }
    
    vec3 dir = vec3(uv * clouds_zoom, 1.);
 
    vec3 from = vec3(0.0, 0.0, 0.0);
    
    from.x -= parallaxX * scale;
    from.y -= parallaxY * scale;
    
    vec3 forward = vec3(0., 0., 1.);
                
    from.z += 0.003 * animation_time;
    
    dir.xy *= rot_xy * rot;
    forward.xy *= rot_xy * rot;

    dir.xz *= rot_xz * rot;
    forward.xz *= rot_xz * rot;

    dir.yz *= rot_yz * rot;
    forward.yz *= rot_yz * rot;
    
    from.xy *= -(rot_xy * rot);
    from.xz *= (rot_xz * rot);
    from.yz *= (rot_yz * rot);
     
    //zoom
    float zooom = (animation_time - 3311.) * 0.01;
    from += forward * zooom;
    float sampleShift = (0.5 + 0.5 * sin(zooom)) * clouds_stepsize;
     
    float zoffset = -sampleShift;
    sampleShift /= clouds_stepsize; // make from 0 to 1

    //volumetric rendering
    float s = 0.24;
    float s3 = s + clouds_stepsize / 2.0;
    vec3 v = vec3(0.);
    float t3 = 0.0;

    vec3 backCol2 = vec3(0.);
    for (int r = 0; r < clouds_volsteps; r++) {
        vec3 p3 = (from + (s3 + zoffset) * dir) * (1.9 / clouds_zoom);
        
        p3 = abs(vec3(clouds_tile) - mod(p3, vec3(clouds_tile * 2.))); // tiling fold
        
        #ifdef cloud
        t3 = field(p3);
        #endif
    
        float fade = pow(clouds_distfading, max(0., float(r) - sampleShift));
        
        backCol2 += 0.75 * vec3(t3) * fade;
        
        s += clouds_stepsize;
        s3 += clouds_stepsize;        
    }
               
    v = mix(vec3(length(v)), v, clouds_saturation); //color adjust
    
    #ifdef cloud
    backCol2 *= cloud;
    #endif
    
    vec4 color = vec4(backCol2, 1.0);
    color.rgb = pow(color.rgb + vec3(kontrast * 0.05), vec3(kontrast));
    
    return color;
}
