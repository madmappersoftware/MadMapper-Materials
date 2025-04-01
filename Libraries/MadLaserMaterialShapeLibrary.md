####*```MadLaserMaterialShapeLibrary.glsl```*
#### LaserMaterial Shape Library

MadLaserMaterialShapeLibrary is a small library that implements a set of function for 2D drawing to simplify coding.
This is a first draft of the library
While not really documented for now, here are the different functions availble in any shader that includes "MadNoise.glsl"
Note that in MadMapper, any shader can include the file by adding
```c++
  #include "MadLaserMaterialShapeLibrary.glsl"
```

Library declarations:

```c++
//! Initializes the library with the requested pointNumber to render
void sl_init(int pointNumber);

//! Start a new path
void sl_beginPath()

//! Start new shape at targetPos (increases shape number but still in same path)
//! Must be called after a sl_beginPath before any drawing command, & to start a new shape in the path
void sl_moveTo(vec2 targetPos)

//! Create a line to from current pos (defined by last moveTo / cubicTo or quadTo command) to targetPos
void sl_lineTo(vec2 targetPos)

//! Cubic bezier from current pos to targetPos with specified bezier handles
void sl_cubicTo(vec2 bezierHandle1, vec2 bezierHandle2, vec2 targetPos)

//! Quadratic bezier from current pos to targetPos with specified bezier handle 
void sl_quadTo(vec2 bezierHandle, vec2 targetPos)

//! Start a new shape and draw a laser beam / a single point shape
void sl_drawBeam(vec2 start)

//! Start a new shape and draw a line (equalivalent of a sl_moveTo and a sl_lineTo)
void sl_drawLine(vec2 start, vec2 end, inout vec2 pos)

//! Start a new shape and draw an ellipse (equalivalent of a sl_moveTo and 4x sl_cubicTo calls)
void sl_drawEllipse(vec2 ellipseCenter, vec2 ellipseSize)

//! Start a new shape and draw a rectangle (equalivalent of a sl_moveTo and 4x sl_lineTo calls)
void sl_drawRect(vec2 center, vec2 size)

//! Start a new shape and draw an arc
void drawArc(vec2 center, float radius, float startAngle, float endAngle)

//! End path, returns true if point being process (specified by pointNumber) is inside the current path, in which case
//! you can use normalizedPosInShape to colorize the path depending on the position of the point inside the path
//! fill pos, color & userData and return
bool endPath(out float normalizedPosInShape)
```

Usage example: 

```c++
#include "MadLaserMaterialShapeLibrary.glsl"

void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
{
  sl_init(pointNumber);

  // Draw a quad (not rectangular)
  sl_beginPath();
  sl_moveTo(vec2(-0.75,-0.75));
  sl_lineTo(vec2(-0.25,-0.85));
  sl_lineTo(vec2(-0.25,-0.35));
  sl_lineTo(vec2(-0.75,-0.25));
  sl_lineTo(vec2(-0.75,-0.75));
  if (sl_endPath(normalizedPosInShape)) {
    shapeNumber = sl_getThisPointShapeNumber();
    pos = sl_getThisPointPosition();
    // Cycling mix of mat_color1 & mat_color2
    color = mix(mat_color1,mat_color2,fract(normalizedPosInShape+mat_animation_time));
    return;
  }

  // Draw rectangle
  sl_beginPath();
  sl_moveTo(vec2(0.75,0.75));
  sl_lineTo(vec2(0.25,0.75));
  sl_lineTo(vec2(0.25,0.25));
  sl_lineTo(vec2(0.75,0.25));
  sl_lineTo(vec2(0.75,0.75)); // Close path
  if (sl_endPath(normalizedPosInShape)) {
    shapeNumber = sl_getThisPointShapeNumber();
    pos = sl_getThisPointPosition();
    // Animated grayscale pattern over the path
    float luminosity = 0.5+0.5*sin((mat_animation_time + normalizedPosInShape) * 2 * PI);
    color = vec4(luminosity,luminosity,luminosity,1);
    return;
  }

  // Draw circle
  vec2 circleCenter = vec2(-0.5,0.5);
  float circleRadius = 0.25;
  float handleOffset = 0.5522848; // Magic number for circle approximation
  sl_beginPath();
  sl_moveTo(circleCenter + circleRadius * vec2(-1, 0));
  sl_cubicTo(
       circleCenter + circleRadius * vec2(-1, handleOffset),  // Handle 1
       circleCenter + circleRadius * vec2(-handleOffset, 1),  // Handle 2
       circleCenter + circleRadius * vec2(0, 1));           // Target point
  sl_cubicTo(
       circleCenter + circleRadius * vec2(handleOffset, 1),     // Handle 1
       circleCenter + circleRadius * vec2(1, handleOffset),     // Handle 2
       circleCenter + circleRadius * vec2(1, 0));           // Target point
  sl_cubicTo(
       circleCenter + circleRadius * vec2(1, -handleOffset),    // Handle 1
       circleCenter + circleRadius * vec2(handleOffset, -1),    // Handle 2
       circleCenter + circleRadius * vec2(0, -1));          // Target point
  sl_cubicTo(
       circleCenter + circleRadius * vec2(-handleOffset, -1),   // Handle 1
       circleCenter + circleRadius * vec2(-1, -handleOffset),   // Handle 2
       circleCenter + circleRadius * vec2(-1, 0));          // Target point
  if (sl_endPath(normalizedPosInShape)) {
    shapeNumber = sl_getThisPointShapeNumber();
    pos = sl_getThisPointPosition();
    color = vec4(1); // Make circle white
    return;
  }

  // Draw green circle
  sl_beginPath();
  sl_drawEllipse(vec2(0,0), vec2(circleRadius));
  if (sl_endPath(normalizedPosInShape)) {
    shapeNumber = sl_getThisPointShapeNumber();
    pos = sl_getThisPointPosition();
    color = vec4(0,1,0,1); // Green
    return;
  }

  // Draw heart
  vec2 heartCenter = vec2(0.5,-0.5);
  float heartScale = 0.3;
  sl_beginPath();
  sl_moveTo(heartCenter + heartScale * vec2(-0.3465, 0.02));
  
  // Heart left side
  sl_cubicTo(
       heartCenter + heartScale * vec2(-0.33, 0.15),   // Handle 1
       heartCenter + heartScale * vec2(-0.25, 0.38),   // Handle 2
       heartCenter + heartScale * vec2(0.0, 0.22));    // Target point
  
  // Heart right side
  sl_cubicTo(
       heartCenter + heartScale * vec2(0.25, 0.38),    // Handle 1
       heartCenter + heartScale * vec2(0.33, 0.15),    // Handle 2
       heartCenter + heartScale * vec2(0.3465, 0.02)); // Target point
  
  // Heart bottom
  sl_cubicTo(
       heartCenter + heartScale * vec2(0.33, -0.15),   // Handle 1
       heartCenter + heartScale * vec2(0.25, -0.38),   // Handle 2
       heartCenter + heartScale * vec2(0.0, -0.65));   // Target point
  
  sl_cubicTo(
       heartCenter + heartScale * vec2(-0.25, -0.38),  // Handle 1
       heartCenter + heartScale * vec2(-0.33, -0.15),  // Handle 2
       heartCenter + heartScale * vec2(-0.3465, 0.02)  // Target point
       );
       
  if (sl_endPath(normalizedPosInShape)) {
    shapeNumber = sl_getThisPointShapeNumber();
    pos = sl_getThisPointPosition();
    color = vec4(1,0,0,1); // red
    return;
  }

  shapeNumber = -1;
}
```
