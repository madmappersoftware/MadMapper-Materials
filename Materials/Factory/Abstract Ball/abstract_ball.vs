out mat3 mat_linePatternsMatrix;
out float mat_currentMoveSpeed;
out float mat_noiseAmountNow;

#ifndef M_PI
  #define M_PI 3.1415926535897932384626433832795
#endif

mat3 makeTransformMatrix()
{
    // Center
    //uv -= vec2(0.5,0.5);
    mat3 linePatternsMatrix = mat3(1,0,-0.5,
                              0,1,-0.5,
                              0,0,1);

    // Move
    //uv.x += mat_automovesize * sin(mat_move_position*2*M_PI) / 2;
    if (mat_automoveactive) {
      float translateX = sin(mat_move_position*2*M_PI-mat_currentMoveSpeed) * mat_automovesize / 2;

      linePatternsMatrix *= mat3(1,0,translateX,
                                 0,1,0,
                                 0,0,1);
    }

    // Scale
    //uv /= mix(1,1+sin(scale_position*4),mat_autoscalesize);
    if (mat_autoscaleactive) {
      float scale;
      #if defined(mat_autoscaleshape_IS_Smooth)
        scale = mat_autoscalesize * (1+sin(mat_scale_position*2*M_PI))/2;
      #elif defined(mat_autoscaleshape_IS_In)
        scale = mat_autoscalesize * (mod(mat_scale_position,1));
      #elif defined(mat_autoscaleshape_IS_Out)
        scale = mat_autoscalesize * (1-mod(mat_scale_position,1));
      #elif defined(mat_autoscaleshape_IS_Linear)
        scale = mat_autoscalesize * (-1+abs(mod(mat_scale_position*2+1,2)-1));
      #else // defined(mat_autoscaleshape_IS_Cut)
        scale = mat_autoscalesize * step(0.5,mod(mat_scale_position,1));
      #endif
      scale = 1/(scale + 0.00000001);
      linePatternsMatrix *= mat3(scale,0,0,
                                 0,scale,0,
                                 0,0,1);
    }

    return linePatternsMatrix;
}

void materialVsFunc(vec2 uv) {
    if (mat_automoveactive) {
      mat_currentMoveSpeed = abs(cos(mat_move_position*2*M_PI)) * mat_automovesize;
    } else {
      mat_currentMoveSpeed = 0;
    }
    if (mat_autonoiseactive) {
      mat_noiseAmountNow = 2 * mat_autonoisesize * abs(1-mat_currentMoveSpeed);
    } else {
      mat_noiseAmountNow=0;
    }

    mat_linePatternsMatrix=makeTransformMatrix();
}
