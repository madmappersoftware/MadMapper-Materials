/* MadLaserMaterialShapeLibrary is a library for path drawing. It supports drawing lines, cubic bezier, quadratic bezier...
To use this library, just include it using #include "MadLaserMaterialShapeLibrary.glsl"

Available functions are

// Initialize library
void sl_init(int pointNumber);

// Start a new path
void sl_beginPath()

// Start new shape at targetPos (increases shape number but still in same path)
// Must be called after a sl_beginPath before any drawing command, & to start a new shape in the path
void sl_moveTo(vec2 targetPos)

// Create a line to from current pos (defined by last moveTo / cubicTo or quadTo command) to targetPos
void sl_lineTo(vec2 targetPos)

// Cubic bezier from current pos to targetPos with specified bezier handles
void sl_cubicTo(vec2 bezierHandle1, vec2 bezierHandle2, vec2 targetPos)

// Quadratic bezier from current pos to targetPos with specified bezier handle 
void sl_quadTo(vec2 bezierHandle, vec2 targetPos)

// Start a new shape and draw a laser beam / a single point shape
void sl_drawBeam(vec2 start)

// Start a new shape and draw a line (equalivalent of a sl_moveTo and a sl_lineTo)
void sl_drawLine(vec2 start, vec2 end)

// Start a new shape and draw an ellipse (equalivalent of a sl_moveTo and 4x sl_cubicTo calls)
void sl_drawEllipse(vec2 ellipseCenter, vec2 ellipseSize)

// Start a new shape and draw a rectangle (equalivalent of a sl_moveTo and 4x sl_lineTo calls)
void sl_drawRect(vec2 center, vec2 size)

// Start a new shape and draw an arc
void drawArc(vec2 center, float radius, float startAngle, float endAngle)

// End path, returns true if point being process (specified by pointNumber) is inside the current path, in which case
// you can use normalizedPosInShape to colorize the path depending on the position of the point inside the path
bool endPath(out float normalizedPosInShape)

Usage example: 

	sl_init(pointNumber);

	// Draw a red line
	sl_beginPath();
	drawLine(vec2(-1,0),vec2(1,0));
	if (sl_endPath(normalizedPosInShape)) {
		shapeNumber = sl_getThisPointShapeNumber();
		pos = sl_getThisPointPosition();
		color = vec4(1,0,0,1); // Red
		return;
	}

	// Draw a free path with sl_moveTo / sl_lineTo
	sl_beginPath();
	sl_moveTo(vec2(-0.75,-0.75));
	sl_lineTo(vec2(-0.25,-0.85));
	sl_lineTo(vec2(-0.25,-0.35));
	sl_lineTo(vec2(-0.75,-0.25));
	sl_cubicTo(vec2(-0.75,-0.5),vec2(-0.75,-0.5),vec2(-0.75,-0.75));
	if (sl_endPath(normalizedPosInShape)) {
		shapeNumber = sl_getThisPointShapeNumber();
		pos = sl_getThisPointPosition();
		// Cycling mix of mat_color1 & mat_color2
		color = mix(mat_color1,mat_color2,fract(normalizedPosInShape+mat_animation_time));
		return;
	}

*/


#ifndef PI
	#define PI 3.14159265359
#endif

int currentShapeNumber = 0;
int currentPointNumber;
vec2 currentPos;
vec2 executedPointPosition;
int shapeStartPointNumber;
float normalizedPosInShape;

void sl_init(int pointNumber)
{
	currentPointNumber = pointNumber;
}

// Render a path: sl_beginPath, moveTo, lineTo/arcTo/cubicTo/quadTo... endPath
void sl_beginPath()
{
	shapeStartPointNumber = currentPointNumber;
}

void sl_moveTo(vec2 targetPos)
{
	currentPos = targetPos;
	currentShapeNumber++;
}

void sl_lineTo(vec2 targetPos)
{
	int pointsForShape = int(1 + length(targetPos-currentPos) * 1024);
	if (currentPointNumber >= 0 && currentPointNumber < pointsForShape) {
		float normalizedPosInShape = currentPointNumber / float(pointsForShape-1);
		executedPointPosition = mix(currentPos,targetPos,normalizedPosInShape);
	}
	currentPointNumber -= pointsForShape;
	currentPos = targetPos;
}

// Cubic Bezier curve implementation
void sl_cubicTo(vec2 bezierHandle1, vec2 bezierHandle2, vec2 targetPos)
{
	int pointsForShape = int(1 + (length(bezierHandle1-currentPos) + length(bezierHandle2-bezierHandle1) + length(targetPos-bezierHandle2)) * 512);
	if (currentPointNumber >= 0 && currentPointNumber < pointsForShape) {
		float normalizedPosInShape = currentPointNumber / float(pointsForShape-1);
		executedPointPosition = mix(mix(mix(currentPos, bezierHandle1, normalizedPosInShape), 
					   mix(bezierHandle1, bezierHandle2, normalizedPosInShape), normalizedPosInShape),
				 mix(mix(bezierHandle1, bezierHandle2, normalizedPosInShape),
					 mix(bezierHandle2, targetPos, normalizedPosInShape), normalizedPosInShape),
				 normalizedPosInShape);
		// float t = normalizedPosInShape;
		// float omt = 1-t;
		// executedPointPosition =    currentPos * omt * omt * omt
        //     + 3 * bezierHandle1 * t   * omt * omt
        //     + 3 * bezierHandle2 * t   * t   * omt
        //     +     targetPos * t   * t   * t;
	}
	currentPointNumber -= pointsForShape;
	currentPos = targetPos;
}

// Quadratic Bezier curve implementation
void sl_quadTo(vec2 bezierHandle, vec2 targetPos)
{
	int pointsForShape = int(1 + (length(bezierHandle-currentPos) + length(targetPos-bezierHandle)) * 512);
	if (currentPointNumber >= 0 && currentPointNumber < pointsForShape) {
		float normalizedPosInShape = currentPointNumber / float(pointsForShape-1);
		vec2 p1 = mix(currentPos, bezierHandle, normalizedPosInShape);
		vec2 p2 = mix(bezierHandle, targetPos, normalizedPosInShape);
		executedPointPosition = mix(p1, p2, normalizedPosInShape);
	}
	currentPointNumber -= pointsForShape;
	currentPos = targetPos;
}

// Arc implementation using the current position as starting point
void sl_arcTo(vec2 center, float radius, float startAngle, float endAngle)
{
	float arcLength = abs(endAngle - startAngle);
	int pointsForShape = int(1 + radius * arcLength * 1024);
	if (currentPointNumber >= 0 && currentPointNumber < pointsForShape) {
		float normalizedPosInShape = currentPointNumber / float(pointsForShape-1);
		float angle = mix(startAngle, endAngle, normalizedPosInShape);
		executedPointPosition = center + radius * vec2(cos(angle), sin(angle));
	}
	currentPointNumber -= pointsForShape;
	currentPos = center + radius * vec2(cos(endAngle), sin(endAngle));
}

bool sl_endPath(out float normalizedPosInShape)
{
	if (currentPointNumber < 0) {
		int shapePoint = -currentPointNumber;
		int shapePointCount = shapeStartPointNumber-currentPointNumber;
		if (shapePointCount > 1) {
			normalizedPosInShape = float(shapePoint) / (shapePointCount-1);
		} else {
			normalizedPosInShape = 0; // Single point path is valid
		}
	}
	return currentPointNumber < 0;
}

int sl_getThisPointShapeNumber()
{
	return currentShapeNumber;
}

vec2 sl_getThisPointPosition()
{
	return executedPointPosition;
}

void sl_drawBeam(vec2 beamPos)
{
	// begin new shape
	currentShapeNumber++;

	int pointsForShape = 1;
	if (currentPointNumber < pointsForShape) {
		executedPointPosition = beamPos;
	}

	currentPointNumber -= pointsForShape;
}

void sl_drawLine(vec2 start, vec2 end)
{
	// begin new shape
	currentShapeNumber++;

	int pointsForShape = int(1 + length(end-start) * 1024);
	if (currentPointNumber < pointsForShape) {
		float normalizedPosInShape = currentPointNumber / float(pointsForShape-1);
		executedPointPosition = mix(start,end,normalizedPosInShape);
	}

	currentPointNumber -= pointsForShape;
}

void sl_drawRect(vec2 center, vec2 size)
{
	// begin new shape
	currentShapeNumber++;

	int pointsForShape = int(1 + size.x * 1024 + size.y * 1024);
	if (currentPointNumber < pointsForShape) {
		float normalizedPosInShape = currentPointNumber / float(pointsForShape-1);
		float normalizedPosInShapePart = fract(normalizedPosInShape*4);
		if (normalizedPosInShape < 1./4) {
			executedPointPosition = vec2(center.x - size.x/2 + size.x * normalizedPosInShapePart, center.y - size.y/2);
		} else if (normalizedPosInShape < 2./4) {
			executedPointPosition = vec2(center.x + size.x/2, center.y - size.y/2 + size.y * normalizedPosInShapePart);
		} else if (normalizedPosInShape < 3./4) {
			executedPointPosition = vec2(center.x + size.x/2 - size.x * normalizedPosInShapePart, center.y + size.y/2);
		} else {
			executedPointPosition = vec2(center.x - size.x/2, center.y + size.y/2 - size.y * normalizedPosInShapePart);
		}
	}

	currentPointNumber -= pointsForShape;
}

void sl_drawEllipse(vec2 ellipseCenter, vec2 ellipseSize)
{
	// begin new shape
	currentShapeNumber++;

	int pointsForShape = int(1+(ellipseSize.x+ellipseSize.y) * (PI*1024));
	if (currentPointNumber < pointsForShape) {
		float normalizedPosInShape = currentPointNumber / float(pointsForShape-1);
		float angle = normalizedPosInShape * 2 * PI;
		executedPointPosition = ellipseCenter + ellipseSize * vec2(cos(angle),sin(angle));
	}

	currentPointNumber -= pointsForShape;
}

void sl_drawArc(vec2 center, float radius, float startAngle, float endAngle)
{
	// begin new shape
	currentShapeNumber++;

	float arcLength = abs(endAngle - startAngle);
	int pointsForShape = int(1 + radius * arcLength * 1024);
	if (currentPointNumber < pointsForShape) {
		float normalizedPosInShape = currentPointNumber / float(pointsForShape-1);
		float angle = mix(startAngle, endAngle, normalizedPosInShape);
		executedPointPosition = center + radius * vec2(cos(angle), sin(angle));
	}

	currentPointNumber -= pointsForShape;
}

