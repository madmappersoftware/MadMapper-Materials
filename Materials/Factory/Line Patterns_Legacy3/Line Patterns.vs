out mat3 linePatternsMatrix;
out float widthValue;

#ifndef M_PI
  #define M_PI 3.1415926535897932384626433832795
#endif

#include "MadNoise.glsl"

mat3 makeTransformMatrix()
{
    // Center
    mat3 linePatternsMatrix = mat3(1,0,-0.5,
                              0,1,-0.5,
                              0,0,1);

    // Rotate
    if (autorotateactive || (base_rotation!=0)) {
      float angle = base_rotation * 2*M_PI / 360;
      if (autorotateactive) {
        angle += fract(0.5 + rot_position) * 2*M_PI;
      }
      float sin_factor = sin(angle);
      float cos_factor = cos(angle);
      //uv *= mat2(cos_factor, sin_factor, -sin_factor, cos_factor);
      linePatternsMatrix *= mat3(cos_factor, sin_factor, 0,
                                 -sin_factor, cos_factor, 0,
                                 0, 0, 1);
    }

    // Scale
    if (autoscaleactive) {
      float scale;
      #if defined(autoscaleshape_IS_Smooth)
        scale = autoscalesize * (1+sin(scale_position*2*M_PI))/2;
      #elif defined(autoscaleshape_IS_In)
        scale = autoscalesize * (mod(scale_position,1));
      #elif defined(autoscaleshape_IS_Out)
        scale = autoscalesize * (1-mod(scale_position,1));
      #elif defined(autoscaleshape_IS_Linear)
        scale = autoscalesize * (-1+abs(mod(scale_position*2+1,2)-1));
      #elif defined(autoscaleshape_IS_Cut)
        scale = autoscalesize * step(0.5,mod(scale_position,1));
      #else // defined autoscaleshape_IS_Noise
        scale = autoscalesize * (0.5-vnoise(vec2(scale_position,0)));
      #endif
      scale = 0.5/(1-scale + 0.00000001);
      linePatternsMatrix *= mat3(scale,0,0,
                                 0,scale,0,
                                 0,0,1);
    }

    // Move
    float translateX = 0;
    if (automoveactive) {

      #if defined(automoveshape_IS_Smooth)
        translateX = automovesize * sin(move_position*2*M_PI) / 2;
      #elif defined automoveshape_IS_In
        translateX = automovesize * (0.5-mod(move_position,1));
      #elif defined automoveshape_IS_Out
        translateX = automovesize * (-0.5+mod(move_position,1));
      #elif defined automoveshape_IS_Linear
        translateX = automovesize * (0.5-abs(mod(move_position*2+1,2)-1));
      #elif defined automoveshape_IS_Cut
        translateX = automovesize * (0.5-step(0.5,mod(move_position,1)));
      #else // defined automoveshape_IS_Noise
        translateX = automovesize * (0.5-vnoise(vec2(move_position,1)));
      #endif

      translateX -= width / 2;
    }
    translateX += base_transition;

    linePatternsMatrix *= mat3(1,0,translateX,
                               0,1,0,
                               0,0,1);

    return linePatternsMatrix;
}

void materialVsFunc(vec2 uv) {
    if (autowidthactive) {
      // Width
      #if defined(autowidthshape_IS_Smooth)
        widthValue = autowidthsize * (1+sin(width_position*2*M_PI))/2;
      #elif defined(autowidthshape_IS_In)
        widthValue = autowidthsize * (mod(width_position,1));
      #elif defined(autowidthshape_IS_Out)
        widthValue = autowidthsize * (1-mod(width_position,1));
      #elif defined(autowidthshape_IS_Linear)
        widthValue = autowidthsize * (abs(mod(width_position*2+1,2)-1));
      #elif defined(autowidthshape_IS_Cut)
        widthValue = autowidthsize * step(0.5,mod(width_position,1));
      #else // defined autowidthshape_IS_Noise
        widthValue = autowidthsize * vnoise(vec2(width_position,2));
      #endif
    } else {
      widthValue = 1;
    }

    linePatternsMatrix=makeTransformMatrix();
}
