# MadMapper Laser Materials

Laser Materials are a type of media used for laser projection in MadMapper. As a Material it consists of a GLSL shader based on ISF specification (Interactive Shader Format, see http://interactiveshaderformat.com/)
A Material actually writes color in pixels of your video output displays. A Laser Material is writing to a texture, but it's not actually writing colors, it's writing one or multiple 2D paths that include color.

Laser Materials are real-time animated laser visuals. Their output is a list of colored 2D paths. For instance it could output a moving circle, a grid, a bunch of lines with color gradients etc.

Laser Materials can be live coded in MadMapper or in any text/code editor. MadMapper will update the user interface and output in real-time when changing the code / ISF Header, for instance you can add parameters that will show as sliders, combo boxes etc. in the user interface.

A Laser Materials consists of a function "laserMaterialFunc" that will return a sample point X/Y coordinates, RGB color, shape number and eventually some user data to be reused in next iteration.
The "laserMaterialFunc" takes as input the point number for which it will write data, and the total point count it should output. By default MadMapper will request 8192 samples but this can be changed in "RENDER_SETTINGS" / "POINT_COUNT" (documented below). For each sample, it will set the 2D position, rgb color, shape number & eventually user data. The shape number allows MadMapper to know when you want to start a new path (for instance if you want to draw two circles, you don't want them connected, so when starting second circle you change the shape number).

Shaders are compiled using GLSL version 150 core to be sure they work on a wide range of computers.

Simple example of laser material shader:

	// This is a Json header following the ISF specifications for defining parameters
	// that will appear as a slider, a checkbox, a combox... in MadMapper Material settings
	/*{
	    "CREDIT": "MadTeam",
	    "DESCRIPTION": "Circle with settable center and size.",
	    "TAGS": "laser,tool,circle",
	    "VSN": "1.0",
	    "INPUTS": [
					{"LABEL": "Size", "NAME": "mat_size", "TYPE": "float", "DEFAULT": 0.2, "MIN": 0.0, "MAX": 0.5 },
					{"LABEL": "Center", "NAME": "mat_center", "TYPE": "point2D", "DEFAULT": [0,0], "MIN": [-1,-1], "MAX": [1,1] },
	    		{"LABEL": "Color", "NAME": "mat_color", "TYPE": "color", "DEFAULT": [1,1,1,1] },
	    ],
	    "RENDER_SETTINGS": {
	       "POINT_COUNT": 512
	    }
	}*/

	#define PI 3.1415926535897932384626433832795

	void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
	{
		float normalizedPointNumber = float(pointNumber)/(pointCount-1); // 0.0 for first point, 1.0 for last point so we completely close the circle
		pos = mat_center + mat_size * vec2(cos(normalizedPointNumber*2*PI),sin(normalizedPointNumber*2*PI));
		shapeNumber = 0;
	  color = mat_color;
	}

MadMapper also provides libraries that were written by Simon Geilfus for MadMapper, they are open source, check file headers for more info. The interesting one for laser materials is the MadNoise.glsl library.

Most of Laser Material documentation is similar to Materials documentation:
- Macros
- Includes
- Inputs
- Imported Textures
- Generators
- Waveform and spectrum textures
- Grouping input using LABEL with "/"
- Inputs flags
- Libraries

To avoid duplicating the documentation, please refer to Materials documentation. We'll only document things specific to Laser Materials here.


## Function laserMaterialFunc

The user implemented function laserMaterialFunc is called N times (8192 by default, but can be changed from Render Settings, see below). Each call will generate one sample point.

There are two input parameters:
- pointCount is constant, 8192 or the "POINT_COUNT" defined in render settings
- pointNumber will be 0 for first sample, then 1, 2, ... up to (pointCount-1)

The outputs are:
- pos: 2D position (xy) for this sample
- color: rgba value for this sample, note that alpha is ignore (makes no sense for laser since we cannot do compositing/blending on 2D pathes)
- shapeNumber: each time a sample has a value different to its predecessor, MadMapper will start a new path
- userData: you can put some data there to use it in next frame rendering (allows doing things depending on previous frame, like damping etc)

For instance to draw a white circle of radius 1, we would write:

	void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
	{
		float normalizedPointNumber = float(pointNumber)/(pointCount-1); // 0.0 for first point, 1.0 for last point so we completely close the circle
		pos = vec2(cos(normalizedPointNumber*2*PI),sin(normalizedPointNumber*2*PI));
		shapeNumber = 0;
		color = vec4(1);
	}

To draw two circles side by side of radius 0.5, we would write

	void laserMaterialFunc(int pointNumber, int pointCount, out vec2 pos, out vec4 color, out int shapeNumber, out vec4 userData)
	{
		int pointsPerShape = pointCount / 2;
		shapeNumber = pointNumber / pointsPerShape;

		// Be sure normalizedPosInShape starts at 0 and ends at 1 so we close the path
		float normalizedPosInShape = float(pointNumber-shapeNumber*pointsPerShape)/(pointsPerShape-1);

		pos = 0.5 * vec2(cos(normalizedPosInShape*2*PI),sin(normalizedPosInShape*2*PI));

		if (shapeNumber == 0) {
			// First circle left
			pos.x -= 0.5;
		} else {
			// Second circle right
			pos.x += 0.5;
		}

		color = vec4(1);
	}

## Render Settings

Laser Materials can specify how many sample points they wish to render. For instance if you want to render a single line, you just need two samples:

	"RENDER_SETTINGS": {
		 "POINT_COUNT": 2,
	}

To render a circle, you might want to have 1000 samples:

	"RENDER_SETTINGS": {
		 "POINT_COUNT": 1000,
	}

Laser Materials can specify some laser render settings values that can normally be set at surface level. It can be to adjust render settings to the specific effect it generates (ie: you want to draw multiple lines and force MadMapper scanning left to right instead of optimizing laser scan path) or for optimization (for instance if you're draw circles you can tell MadMapper to avoid searching for angles points in the generated shapes)
We expose a lot of parameters so people might solve problems specific to their project with it. But it's a good idea not to abuse of it since it prevents user from adjusting those settings at surface level.
Let's see parameters one by one, but first, see all parameters in the ISF header:

"RENDER_SETTINGS": {
	 "POINT_COUNT": 4000,
	 "MAX_SPEED": 2,
	 "SKIP_BLACK": false
	 "PRESERVE_ORDER": true,
	 "ANGLE_OPTIMIZATION": true,
	 "ANGLE_THRESHOLD": 0.5,
	 "ANGLE_MAX_DELAY": 0.05,
	 "FIRST_POINT_REPEAT": 0,
	 "LAST_POINT_REPEAT": 6,
	 "POLY_FADE_IN": 1,
	 "MIN_ILDA_POINTS_PER_POLYLINE": 0
}

### MAX_SPEED / Surface parameter Laser Render / Max Speed  

Max speed defines the maximum laser scan speed when rendering paths generated by this laser material. The faster we scan, the higher refresh rate (Ilda FPS) will be, but the lower fidelity you will get. If you just render a line, it's interesting, but MadMapper scanning might be too fast for the engine so it might not finish drawing the path at the end but before. If rendering a path with curved parts, the curve would not be respected with a too high scann speed.
Max Speed default value is 1 and corresponds to an arbitrary value in MadMapper. Maximum value is 4, which is quite high. It is limited because scanning too fast might make the galvo mirror engines too hot and they could melt.

### SKIP_BLACK / Surface parameter Laser Render / Skip Black

By default MadMapper optimises laser scanning by avoiding going through long black parts of paths. For instance if you have a circle and 3/4 of the circle is just black, we can concentrate all the light in the part where there is light to send by just scanning this part.
Why would you disable that ? Because it makes the laser scan very stable if the shape is not moving but you just change the light along the path. If MadMapper always scans the full path and just change the diode levels along the path, the path will be very stable (with a decent laser projector)

### PRESERVE_ORDER / Surface parameter Laser Render / Preserve Order

By default, MadMapper optimises laser scanning by searching for the best to render all paths for a frame.
In most case it's great, but in some cases it's problematic.

Example 1: I create a Laser Material generating 5 points that moves randomly, on frame N, best render order (to minimize blanking points) is:
 P0, P2, P1, P5, P4
On frame N+1, best render order, because P4 moved close to previous P0 position:
 P4, P2, P1, P5, P0
So at that moment, you'll notice a kind of flickering because P0 was not lightened for almost 2 frames time.

Example 2: My laser material generates 10 horizontal lines on top of each other, from top to bottom. The resulting draw order mifght be to start first line from left to right, the second from right to left (less blanking points that going back to the left), third line from left to right again etc.
This is not ideal in some cases because we might have a different effect on the start & end of a line, depending on scan speed (MAX_SPEED), "POLY_FADE_IN" (Laser Render / In Fade surface parameter) & "LAST_POINT_REPEAT" (Laser Render / End Repeat surface parameter)

PRESERVE_ORDER solves those situations by forcing MadMapper to render the generated pathes in the generation order and from start to end.

### Angle optimization
Concerns:
- ANGLE_OPTIMIZATION / Surface parameter Laser Render / Optimize Angles
- ANGLE_THRESHOLD / Surface parameter Laser Render / Angle MIN
- ANGLE_MAX_DELAY / Surface parameter Laser Render / Angle Delay

When scanning fast, angles would be rounded if not spending more scan time there. So we add points on path detected angles. However you can deactivate it if it's not necessary for the effects you're looking for. Also, not computing angles is a benefit for performances.
ANGLE_OPTIMIZATION is to enable / disable angle searching.
ANGLE_THRESHOLD is the minimum angle in radians for MadMapper to consider there's an angle there.
ANGLE_MAX_DELAY defines the maximum time we'll spend on the angles (for 180°, if angle is lower, time will be lower too).

### FIRST_POINT_REPEAT / Surface parameter Laser Render / Start Repeat

Used to add some ILDA points at the beginning of your path to make it brighter, value is a number in ILDA points.

### LAST_POINT_REPEAT / Surface parameter Laser Render / End Repeat

Used to add some ILDA points at the end of your path. It is generally used to ensure the ILDA rasterization will make the laser device end the shape at the right place when scan speed is too high.

### POLY_FADE_IN / Surface parameter Laser Render / In Fade

When starting drawing a shape, the laser projector scanner is stopped, as it has inertia, it will not go from speed 0 to speed X instantaneously, so you might have more light on path start. In Fade setting will delay turning the light on to avoid this too light point on path start.

### MIN_ILDA_POINTS_PER_POLYLINE

You can force MadMapper to generate a minimum number of ILDA sample points per polyline (this doesn't include points added for angles, but just the samples added at regular interval along the path). This is interesting some specific cases, it's a project oriented setting.


## Using Last Frame Data

The Laser Material can access data generated at previous frame (the output texture of previous engine frame), which allows creating more logic in the paths generation. This is passed as a sampler2D uniform named mm_LastFrameData.

The output of the laserMaterialFunc is written to a texture. This is what the texture will contain the output of the laserMaterialFunc in this way:

    Line 0:
      r = x position of the sample point (-1 to 1)
      g = y position of the sample point (-1 to 1)
      b = shape number of this sample point (0,1,2,3...): each time shape number is different from previous sample, MadMapper will start a new shape
      a = 0
    Line 1:
      rgba = color (clamp in 0-1)
    Line 2:
      rgba = user data

To access data from previous frame, you should write:

	vec2 lastFramePos = texelFetch(mm_LastFrameData,ivec2(pointNumber,0),0).rg;
	vec4 lastFrameColor = texelFetch(mm_LastFrameData,ivec2(pointNumber,1),0);
	vec4 lastFrameUserData = texelFetch(mm_LastFrameData,ivec2(pointNumber,2),0);
