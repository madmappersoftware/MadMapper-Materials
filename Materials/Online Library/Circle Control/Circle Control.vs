out mat3 linePatternsMatrix;
out float lumaValue;

#define M_PI 3.1415926535897932384626433832795

float fast_noise( float v ) {
    //return fract(sin(v * 127.1)*43758.5453);
    return IMG_NORM_PIXEL(noiseLUT,vec2(fract(v/20),1)).r * 2 - 1;
}

mat3 makeTransformMatrix()
{
    // Center
    mat3 linePatternsMatrix = mat3(1,0,-0.5-circle_x,
                              0,1,-0.5-circle_y,
                              0,0,1);

	// Move
    if (automovexactive) {
      float translateX=circle_x;
      translateX += automovexnoise * fast_noise(movex_position);

      #if defined(automovexshape_IS_Smooth)
        translateX += automovexsize * sin(movex_position*2*M_PI) / 2;
      #elif defined automovexshape_IS_In
        translateX += automovexsize * (0.5-mod(movex_position,1));
      #elif defined automovexshape_IS_Out
        translateX += automovexsize * (-0.5+mod(movex_position,1));
      #elif defined automovexshape_IS_Linear
        translateX += automovexsize * (0.5-abs(mod(movex_position*2+1,2)-1));
      #else // defined automovexshape_IS_Cut
        translateX += automovexsize * (0.5-step(0.5,mod(movex_position,1)));
      #endif

      linePatternsMatrix *= mat3(1,0,fract(translateX),
                                 0,1,0,
                                 0,0,1);
    }

    if (automoveyactive) {
      float translateY=circle_y;
      translateY += automoveynoise * fast_noise(movey_position+0.5);

      #if defined(automoveyshape_IS_Smooth)
        translateY += automoveysize * cos(movey_position*2*M_PI) / 2;
      #elif defined automoveyshape_IS_In
        translateY += automoveysize * (0.5-mod(movey_position,1));
      #elif defined automoveyshape_IS_Out
        translateY += automoveysize * (-0.5+mod(movey_position,1));
      #elif defined automoveyshape_IS_Linear
        translateY += automoveysize * (0.5-abs(mod(movey_position*2+1,2)-1));
      #else // defined automoveyshape_IS_Cut
        translateY += automoveysize * (0.5-step(0.5,mod(movey_position,1)));
      #endif

      linePatternsMatrix *= mat3(1,0,0,
                                 0,1,translateY,
                                 0,0,1);
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
      #else // defined(autoscaleshape_IS_Cut)
        scale = autoscalesize * step(0.5,mod(scale_position,1));
      #endif
      scale = 0.5/(1-scale + 0.00000001);
      linePatternsMatrix *= mat3(scale,0,0,
                                 0,scale,0,
                                 0,0,1);
    }

    return linePatternsMatrix;
}

void materialVsFunc(vec2 uv) {
    if (autolumaactive) {
      // luma
      #if defined(autolumashape_IS_Smooth)
        lumaValue = autolumasize * (1+sin(luma_position*2*M_PI))/2;
      #elif defined(autolumashape_IS_In)
        lumaValue = autolumasize * (mod(luma_position,1));
      #elif defined(autolumashape_IS_Out)
        lumaValue = autolumasize * (1-mod(luma_position,1));
      #elif defined(autolumashape_IS_Linear)
        lumaValue = autolumasize * (-1+abs(mod(luma_position*2+1,2)-1));
      #else // defined(autolumashape_IS_Cut)
        lumaValue = autolumasize * step(0.5,mod(luma_position,1));
      #endif
    } else {
      lumaValue = 1;
    }

    linePatternsMatrix=makeTransformMatrix();
}
