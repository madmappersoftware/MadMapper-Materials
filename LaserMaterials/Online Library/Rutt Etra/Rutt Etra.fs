/*{
    "RESOURCE_TYPE": "Laser Material for MadMapper",
    "CREDIT": "Mad Matt",
    "DESCRIPTION": "describe your material here",
    "TAGS": "template",
    "VSN": "1.0",
    "INPUTS": [ 
        {"LABEL": "Media", "NAME": "mat_media", "TYPE": "image" }, 

        {
            "LABEL": "Depth",
            "NAME": "fx_depth",
            "TYPE": "float",
            "MIN": 0,
            "MAX": 1,
            "DEFAULT": 0.2
        },
        {
            "LABEL": "Rotate",
            "NAME": "fx_rotate",
            "TYPE": "point2D",
            "MIN": [-90,-90],
            "MAX": [90,90],
            "DEFAULT": [0,0]
        },
        {
            "LABEL": "Zoom",
            "NAME": "fx_zoom",
            "TYPE": "float",
            "MIN": 0,
            "MAX": 4,
            "DEFAULT": 1
        },
        {
            "LABEL": "Origin",
            "NAME": "fx_origin",
            "TYPE": "point2D",
            "MIN": [-1,-1],
            "MAX": [1,1],
            "DEFAULT": [0,0]
        },

        {"LABEL": "Setting/Count", "NAME": "mat_shape_count", "TYPE": "int", "DEFAULT": 2, "MIN": 0, "MAX": 50 }, 
        {"LABEL": "Setting/V Spacing", "NAME": "mat_v_spacing", "TYPE": "float", "DEFAULT": 1.0, "MIN": 0.0, "MAX": 1.0 }, 
        {"LABEL": "Setting/Translation Y", "NAME": "mat_base_transitiony", "TYPE": "float", "DEFAULT": -1.0, "MIN": -1.0, "MAX": 1.0 },
    		{"LABEL": "Setting/Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1] }, 
    		{"LABEL": "Noise/Power", "NAME": "mat_noisepower", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.0 },
    		{"LABEL": "Noise/Scale", "NAME": "mat_noisescale", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 0.5 },
    		{"LABEL": "Noise/Speed", "NAME": "mat_noisespeed", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1.0 },
    ],
	"GENERATORS": [
		{"NAME": "mat_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_noisespeed", "speed_curve":2, "bpm_sync": false, "link_speed_to_global_bpm":true}},
	],
    "RENDER_SETTINGS": {
       "POINT_COUNT": 4000
    }
}*/

#include "auto_all.glsl"

mat4 CreatePerspectiveMatrix(in float fov, in float aspect, in float near, in float far)
{
    mat4 m = mat4(0.0);
    float angle = (fov / 180.0) * PI;
    float f = 1. / tan( angle * 0.5 );
    m[0][0] = f / aspect;
    m[1][1] = f;
    m[2][2] = (far + near) / (near - far);
    m[2][3] = -1.;
    m[3][2] = (2. * far*near) / (near - far);
    return m;
}

mat4 CamControl( vec3 eye, float pitch)
{
    float cosPitch = cos(pitch);
    float sinPitch = sin(pitch);
    vec3 xaxis = vec3( 1, 0, 0. );
    vec3 yaxis = vec3( 0., cosPitch, sinPitch );
    vec3 zaxis = vec3( 0., -sinPitch, cosPitch );

    // Create a 4x4 view matrix from the right, up, forward and eye position vectors
    mat4 viewMatrix = mat4(
        vec4(       xaxis.x,            yaxis.x,            zaxis.x,      0 ),
        vec4(       xaxis.y,            yaxis.y,            zaxis.y,      0 ),
        vec4(       xaxis.z,            yaxis.z,            zaxis.z,      0 ),
        vec4( -dot( xaxis, eye ), -dot( yaxis, eye ), -dot( zaxis, eye ), 1 )
    );
    return viewMatrix;
}

mat3 rotateAroundX( in float angle )
{
  float s = sin(angle);
  float c = cos(angle);
  return mat3(1.0,0.0,0.0,
              0.0,  c, -s,
              0.0,  s,  c);
}

mat3 rotateAroundY( in float angle )
{
  float s = sin(angle);
  float c = cos(angle);
  return mat3(  c,0.0,  s,
              0.0,1.0,0.0,
               -s,0.0,  c);
}


void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
  int pointsPerShape = pointCount / mat_shape_count;
  shapeNumber = pointNumber / pointsPerShape;
  if (shapeNumber >= mat_shape_count) {
    shapeNumber = -1; // point will be ignored if shape number is negative
    return;
  }
  float normalizedShapeId = float(shapeNumber)/mat_shape_count;

  // Be sure normalizedPosInShape starts at 0 and ends at 1 so we close the path
  float normalizedPosInShape = float(pointNumber-shapeNumber*pointsPerShape)/(pointsPerShape-1);
    
  // 2 points per line
  int lineNumber = shapeNumber;

  pos = vec2(-1 + 2*normalizedPosInShape,mat_v_spacing*(-1+2*normalizedShapeId+mat_base_transitiony));

  // Move
  float translateX = 0;
  if (mat_automoveactive) {
      if (mat_automoveactive) {
        // "Smooth"=0,"In"=1,"Linear"=2,"Cut"=3,"Noise"=4,"Smooth In"=5
        if (mat_automoveshape == 0) {
          translateX = mat_automovesize * sin((mat_move_position+normalizedShapeId*mat_automoveoffset)*2*PI) / 2;
        } else if (mat_automoveshape == 1) {
          translateX = mat_automovesize * fract(mat_move_position+normalizedShapeId*mat_automoveoffset);
        } else if (mat_automoveshape == 2) {
          translateX = mat_automovesize * (0.5-abs(mod((mat_move_position+normalizedShapeId*mat_automoveoffset)*2+1,2)-1));
        } else if (mat_automoveshape == 3) {
          translateX = mat_automovesize * (0.5-step(0.5,mod((mat_move_position+normalizedShapeId*mat_automoveoffset),1)));
        } else if (mat_automoveshape == 4) {
          translateX = mat_automovesize * (0.5*noise(vec2((mat_move_position+normalizedShapeId*mat_automoveoffset*99.5),0)));
        } else {
          translateX = mat_automovesize * (-0.5 * sin(-PI/2 + mod((mat_move_position+normalizedShapeId*mat_automoveoffset),1)*PI));
        }
      }
  }
  pos.y = -1 + 2*(fract((pos.y+1)/2+translateX));

  if (mat_noisepower > 0) {
    pos.y += mat_noisepower * noise(mat_noisescale * vec2(normalizedPosInShape,normalizedShapeId) + vec2(0,mat_noise_time));
  }

  mat3 transformMatrix = makeTransformMatrix(normalizedShapeId);
  pos = (vec3(pos,1) * transformMatrix).xy;

  vec4 mediaColor = IMG_NORM_PIXEL(mat_media,vec2((1+pos.x)/2,1-(1+pos.y)/2));
  float mediaLuma = dot(mediaColor.rgb, vec3(0.299, 0.587, 0.114)) * mediaColor.a;

  vec3 depthDir = vec3(-fx_origin,1);
  vec4 pos4D = vec4(pos,0,1);
  pos4D.xyz += depthDir*(mediaLuma-0.5)*fx_depth;

  vec3 eye = vec3(0,0,-1);
  mat4 projmat = CreatePerspectiveMatrix(90., 1, -0.1, 10.);
  mat4 viewmat = CamControl(eye, 0);
  mat4 vpmat = viewmat * projmat;

  mat3 rotMatrixX = rotateAroundX(-fx_rotate.y*PI/180);
  mat3 rotMatrixY = rotateAroundY(-fx_rotate.x*PI/180);
  pos4D.xyz *= rotMatrixX * rotMatrixY;

  pos4D.z += -1+eye.z; //mix(eye.z,-1+eye.z,max(abs(fx_rotate.x),abs(fx_rotate.y))/90);

  pos4D = vpmat * pos4D; 
  pos4D.xy /= pos4D.w;
  pos4D.z = 1;
  pos4D.w = 1;
  pos4D.xy *= 2*fx_zoom;

  pos = pos4D.xy;

  color = vec4(getLightValue(normalizedShapeId) * mat_color * getColorValue(normalizedShapeId)); 
}
