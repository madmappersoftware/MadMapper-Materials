# MadMapper Surface FX

MadMapper "Surface FX" are effects processed in real time when rendering a MadMapper surface. They are directly rendered on the screen by the GPU (graphics card). They are composed of a fragment shader file and optionally a vertex shader file.

Parameters declaration in MadMapper FX are based on ISF specification: http://interactiveshaderformat.com/

There are different kind of Surface FX for the different kind of MadMapper surfaces. They are separated in 3 categories:

- Surface 2D FX: for surface types Quad, Triangle & Circle
- Surface 3D FX: for 3D surfaces
- Surface Line FX: for Lines surfaces

Each of those have different uniforms and variables available. For instance for a 2D surface, you'll have access to alpha level resulting from feathering & masking. This will not be the case for 3D surfaces or Lines surfaces. For Lines surfaces, you'll get the pixel position along the path, path length etc. For 3D surfaces, you'll get lights position / settings etc. The available uniforms & inputs variables are documented in the "Empty Template" FX available for each surface type.  

The FX consist of a folder with a thumbnail (file name must be "thumbnail.jpg" or "thumbnail.png"), a fragment shader (.fs) and optionally a vertex shader (.vs).
The vertex and fragment shaders are the one mixed with a MadMapper internal shader also specific to the surface type to keep some complexity outside your FX code (ie specific blend modes, highlight selection, quad soft-edge etc.).

Shaders are compiled using GLSL version 150 core to be sure they work on a wide range of computers.

MadMapper provides extensions to the spec. And some restrictions.
We also provide in libraries that were written by Simon Geilfus for MadMapper, they are open source, check file headers for more info.
You will find here all details about writing Surface FX and using MadNoise.glsl and MadSDF.glsl libraries.

## EXAMPLE FX FILE

Let's write the simplest Surface 2D FX as an example: invert colors.
We'll first write an ISF header with FX information & an "invert" boolean parameter:

	/*{
		"CREDIT": "Your Name",
		"CATEGORIES": [ "Image Control" ],
		"VSN": 1,
		"INPUTS": [
			{
				"NAME": "fx_invert",
				"LABEL": "Invert",
				"TYPE": "bool",
				"DEFAULT": 0,
				"FLAGS": "button"
			}
		]
	}*/

We name the parameter "fx_invert" internally, so if this FX shader is merged with a material shader, we won't get naming conflict (convention is to name FX inputs with "fx_" prefix, and Material inputs with "mat_" prefix).
The FLAG "button" will make this boolean shown as a toggle button instead of a checkbox.

Now let's write the FX fragment shader code:

	vec4 fxColorForPixel(vec2 mm_FragNormCoord)
	{
		// Read the color of the input media at this position
		vec4 color = FX_NORM_PIXEL(mm_FragNormCoord);

	  // Invert RGB if needed (donnot invert alpha channel)
	  if (fx_invert) {.
			color.rgb = 1-color.rgb;
		}

		// Return the output color
		return color;
	}

FX_NORM_PIXEL is a macro taking the media color (vec4 for rgba) at a specific position (between (0,0) &(1,1)) whatever the media is (texture, rectangle texture (ie Syphon / AVFoundation movie), or a material shader)

The fxColorForPixel is the root function for a FX fragment shader file. Your FX file will be combined with MadMapper base surface shader.

The vertex shader code wouldn't be altered for this FX since we don't need any geometry changes. The vertex can either be a copy of the default one (ie the one from the "Empty Template" FX) or not be in the FX folder at all.

Let's make another example, this FX would alter the quad surface geometry:

The vertex shader "My2DFX.fs" file would look like the builtin "Vertex Noise" FX:

	/*{
	    "CREDIT": "No one",
	    "CATEGORIES": [
	        "Image Control"
	    ],
	    "VSN": 1,
	    "INPUTS": [
	        {
	            "NAME": "fx_power",
	            "LABEL": "Noise Power",
	            "TYPE": "float",
	            "DEFAULT": 0.5,
	            "MIN": 0.0,
	            "MAX": 1.0
	        },
	        {
	            "NAME": "fx_speed",
	            "LABEL": "Noise Speed",
	            "TYPE": "float",
	            "DEFAULT": 1.0,
	            "MIN": 0.0,
	            "MAX": 10.0
	        }     
	    ],
	    "GENERATORS": [
	        { "NAME": "fx_noise_time", "TYPE": "time_base", "PARAMS": {"speed": "fx_speed", "reverse": false, "strob": 0, "speed_curve":4, "link_speed_to_global_bpm":true} },
	    ],
	}*/

	void fxVsFunc()
	{
	    // Initialize Surface 2D fragment shader inputs
	    mm_FragNormCoord = (mm_TextureMatrix*vec4(mm_TexCoord0.xy,0,1)).xy;

	    // Tells OpenGL where this vertex should be on the output view
	    gl_Position = mm_ModelViewProjectionMatrix * vec4(mm_Vertex.x + sin(fx_noise_time)*fx_power, mm_Vertex.y, 0, 1);
	}

This vertex shader just offsets the X position of the quad with a sinusoidal signal which speed is defined by fx_speed and amplitude by fx_power.

The fragment shader "My2DFX.fs" file would just return the media color:

	vec4 fxColorForPixel(vec2 mm_FragNormCoord)
	{
	    return FX_NORM_PIXEL(mm_FragNormCoord);
	}


## Common Macros

ISF defines some macros to sample in the media (ie IMG_NORM_PIXEL(vec2 fragCoord)).
MadMapper adds some other ones:

- vec4 IMG_NORM_PIXEL_LOD(sampler2D sampler, vec2 uv, float lod): will only work if input image "GL_TEXTURE_MIN_FILTER" has been set to "LINEAR_MIPMAP_LINEAR" or other mipmapping mode (see MEDIA section below). This will return the media pixel position from the mipmapped texture. Cf https://www.khronos.org/registry/OpenGL-Refpages/gl4/html/textureLod.xhtml
- vec4 IMG_TEXEL_FETCH(sampler2D sampler, vec2 uv, float lod) : will only work if input image "GL_TEXTURE_MIN_FILTER" has been set to "LINEAR_MIPMAP_LINEAR" or other mipmapping mode (see MEDIA section below). This will return the media pixel position from the mipmapped texture. Cf https://www.khronos.org/registry/OpenGL-Refpages/gl4/html/texelFetch.xhtml

Surface FX have some more available. Some are common to all surface types, others not. The best when writing an FX is to start from existing ones code. The "Empty Template" FX and all builtin FX have the documentation as a comment in the code. But let's see what common macros are available to all surface types:

- vec4 FX_NORM_PIXEL(vec2 uv): returns the color of the pixel at normalized position uv (0,0)-(1,1), whether it's a texture or a material
- vec2 FX_IMG_SIZE(): returns the dimensions of the input media or 1024x1024 if a Material (not rendered to texture) is being used
- vec4 FX_NORM_PIXEL_LOD(vec2 uv, float lod): will only work if media "GL_TEXTURE_MIN_FILTER" has been set to "LINEAR_MIPMAP_LINEAR" or other mipmapping mode (see MEDIA section below). This will return the media pixel position from the mipmapped texture. Cf https://www.khronos.org/registry/OpenGL-Refpages/gl4/html/textureLod.xhtml
- vec4 FX_TEXEL_FETCH(vec2 uv, float lod) : will only work if media "GL_TEXTURE_MIN_FILTER" has been set to "LINEAR_MIPMAP_LINEAR" or other mipmapping mode (see MEDIA section below). This will return the media pixel position from the mipmapped texture. Cf https://www.khronos.org/registry/OpenGL-Refpages/gl4/html/texelFetch.xhtml
- #define IS_MATERIAL: defined only if a Material (not rendered to texture) is being used as input media (the FX & the Material shaders are combined into a single Shader Program)


## RESTRICTIONS AND CONVENTIONS:

- We advise using a convention for INPUT or IMPORTED names: start with "fx_". For instance "fx_speed", "fx_intensity"... Some names are reserved. If you use one, MadMapper will show an error message specifying which name you're using is reserved.
Also using "fx_" avoids conflicts if in MadMapper you use a "Surface FX" and a "Material" on the same surface (in which case both contains an ISF header and we should avoid the same name to be used in both cases)

- MadMapper Surface FX donnot support multi-pass ISF (because we might have dozens of video surface in a mapping and it would cause huge performance issues)

- MadMapper supports those uniform variables defined in ISF specification: FRAMEINDEX, TIME, TIMEDELTA, DATE, FRAMEINDEX

## Includes

MadMapper accepts including glsl files. Ie

	#include "MyCommonFunctions.glsl"

The path in the include statement must be valid relatively to the file containing the include statement, except for the few libraries we bundle with application (for Noises, Signed Distance Field & MadCommon):

	#include "MadCommon.glsl"
	#include "MadNoise.glsl"
	#include "MadSDF.glsl"

## MEDIA:

MadMapper Surface FX can declare a "MEDIA" dictionary to define some media rendering settings. Full declaration:

"MEDIA": {
		"REQUIRES_TEXTURE": true,
		"GL_TEXTURE_MIN_FILTER": "LINEAR",
		"GL_TEXTURE_MAG_FILTER": "LINEAR",
		"GL_TEXTURE_WRAP": "REPEAT"
},

Let explain those one by one.

- REQUIRES_TEXTURE: defines if this FX accepts working with a MadMapper Material media rendered directly on screen (not rasterized in a texture). MadMapper Materials consist of a function called materialColorForPixel(vec2 fragCoord) that return the color of the media at a specific position. This computation might be processing intensive. And some FX request for each pixel to render, a lot of samples of the input media (think of a blur effect, an edge detection etc). In those cases, the FX combined with an heavy material computating might result in very low framerate. To avoid this, Surface FX that require many samples of the input media should be marked with REQUIRES_TEXTURE and if user wants to use a material with it, a warning will be shown inviting him to activate the "Render To Texture" parameter of the material, so material is rendered once fully in a defined texture size. When the material is rasterized in a texture, sampling in the media will not cost more than with an image / movie / camera or other media.
- "GL_TEXTURE_MIN_FILTER": refer to https://www.khronos.org/opengles/sdk/docs/man/xhtml/glTexParameter.xml
	possible values: "LINEAR", "NEAREST", "LINEAR_MIPMAP_LINEAR", "NEAREST_MIPMAP_NEAREST", "LINEAR_MIPMAP_NEAREST", "NEAREST_MIPMAP_LINEAR"
- "GL_TEXTURE_MAG_FILTER": refer to https://www.khronos.org/opengles/sdk/docs/man/xhtml/glTexParameter.xml
	possible values: "LINEAR", "NEAREST"
- "GL_TEXTURE_WRAP": refer to https://www.khronos.org/opengles/sdk/docs/man/xhtml/glTexParameter.xml
	possible values: "REPEAT", "CLAMP_TO_EDGE", "MIRRORED_REPEAT"

## INPUTS:

MadMapper extends ISF with a few input types:

### Float Range

A "floatRange" INPUT is used to provide a value range, for instance 0.2 - 0.8. Such input will be transmitted to the shader as a vec2 uniform (ie myInput.x for minimum value & myInput.y) like a point2D. MadMapper will make a "Value Range Widget" in the user interface. The default value is provided as a array of two numeric values, but min & max (optional) are a single numeric value.

    {
      "NAME": "value_range",
      "LABEL": "Value Range",
      "TYPE": "floatRange",
      "DEFAULT": [0.2,0.8],
      "MIN": 0.0,
      "MAX": 1.0
    }


## IMPORTED TEXTURES:

The specification of ISF only allows declaring 2D texture. We added a TYPE field to allow loading cube map and 3D textures. Also we added texture filtering and texture wrapping settings.
Here is a complete example.

	/*{
	    "CREDIT": "mm team",
	    "CATEGORIES": [ "Image Control" ],
	    "INPUTS": [
	    	...
	    ],
	    "IMPORTED": [
	        {	  "NAME": "fx_myImage",
	            "TYPE": "2D",
	            "PATH": "image.png",
	            "GL_TEXTURE_MIN_FILTER": "LINEAR",
	            "GL_TEXTURE_MAG_FILTER": "LINEAR",
	            "GL_TEXTURE_WRAP": "REPEAT"
	        },
	        {	  "NAME": "fx_myCubeMapWithFiles",
	            "TYPE": "cube",
	            "PATH": ["posx.png","negx.png","posy.png","negy.png","posz.png","negz.png"]
	        },
	        {	  "NAME": "fx_myCubeMapWithFolder",
	            "TYPE": "cube",
	            "PATH": "cubeMadFolder"
	        },
	        { 	"NAME": "fx_myTexture3d",
	            "TYPE": "3D",
	            "PATH": "colorGrading.png",
	            "DEPTH": 32
	        }
	    ]
	}*/


"myImage" is the name of the uniform sample ("uniform sampler2D myImage;" is inserted in the final shader - or sampler3D / sampleCube)  
Parameters:

- "TYPE": "2D, "CUBE", "3D"
- "PATH": path of the file, with an exception for type CUBE, where PATH can be either an array of 6 files (ordered posx, negx, posy, negy, posz & negz) or the relative path to a folder that contains 6 images named osx, negx, posy, negy, posz & negz (with extension ".jpg" or ".png")
- "GL_TEXTURE_MIN_FILTER": refer to https://www.khronos.org/opengles/sdk/docs/man/xhtml/glTexParameter.xml
	possible values: "LINEAR", "NEAREST", "LINEAR_MIPMAP_LINEAR", "NEAREST_MIPMAP_NEAREST", "LINEAR_MIPMAP_NEAREST", "NEAREST_MIPMAP_LINEAR"
- "GL_TEXTURE_MAG_FILTER": refer to https://www.khronos.org/opengles/sdk/docs/man/xhtml/glTexParameter.xml
	possible values: "LINEAR", "NEAREST"
- "GL_TEXTURE_WRAP": refer to https://www.khronos.org/opengles/sdk/docs/man/xhtml/glTexParameter.xml
	possible values: "REPEAT", "CLAMP_TO_EDGE", "MIRRORED_REPEAT"
- "DEPTH": depth of the 3D texture. This parameter is optional.
	If not defined, we except a texture with Width = Height * Height, ie 256x32 pixels composed of 32 squares of 32x32 pixels next to each other.
	If specifying a depth, image width must be a multiple of Depth. If DEPTH is 64 and the full image is 256x32, it will make a 16x32x64 texture (width=16, height=32, depth=64)

## GENERATORS

With shaders, we can't store a value that we'll reuse when processing next frame (except using multiple render passes and storing a values in pixels of a persistent buffer, but it makes things a bit complicated to solve a simple problem and MadMapper doesn't support render passes for materials since if you have 400 surfaces using the same material, you wouldn't want to do 400 more render passes just to handle a proper animation...)

This problem is inherent to the fact that the shader code is executed in parallel for each vertex/pixel.

To animate something in a shader, you might write something like:

	float linePosition = sin(mat_speed * TIME);

Where mat_speed is an "INPUT" uniform controlled by user & TIME is the time in seconds (as defined in the ISF spec).
The problem with that is that when changing the speed, we got jumps in the animation, ie:

	frame N: TIME=120.0, speed=1 => linePosition=sin(120.0)=0.866
	frame N+1: TIME=120.16, speed=1 => linePosition=sin(120.166)=0.864

That's ok, now user sets speed to 0.2

	frame N+2: TIME=120.32, speed=0.2 => linePosition=sin(24.064)=0.407

The guy just wanted to reduce animation speed and the animation moved to a very different place, not good !

To solve that we added "GENERATORS". It doesn't only solve this case, it allows adding stuffs to filter an INPUT uniform (ie Damper).

GENERATORS are objects with parameters. Their output is a floating point value. For instance:

    "GENERATORS": [
        {
            "NAME": "fx_anim_time_base",
            "TYPE": "time_base",
            "PARAMS": {"speed": "fx_noise_speed", "speed_curve": 2}
        }
    ]

- "NAME": name of the generated uniform float available in the shader code
- "TYPE": type of generator: "time_base", "damper", "adr", "shape_generator", "animator", "pass_thru" (details below)
- "PARAMS": list parameters of the GENERATOR object. All parameters are optional. Parameter values can be either a value, an INPUT (defined above in the header), or another GENERATOR (so it would take its output and process it)

Let's see each GENERATOR type, why they are useful & their parameters:

### Time Base generator "time_base"

"time_base" is a generator that increments its output value (the uniform "move_position" in following example) depending on its "speed" parameter and elapsed time. Between each rendered frame it does something like: "output_value = output_value + speed * elapsedTime"
But there are more parameters, here is a complete example:

    {
      "NAME": "fx_move_position",
      "TYPE": "time_base",
      "FILTERS": {"speed": "fx_automovespeed",
      			  "reverse": false,
      			  "speed_curve":3,
      			  "strob": 0.0,
      			  "bpm_sync": false,
      			  "link_speed_to_global_bpm": false,
      			}
    }

Parameters:
- "speed": speed of the time base (floating point value)
- "reverse": invert speed value (boolean)
- "speed_curve": the input value will be interpreted as pow(INPUT_VALUE,speed_curve). Using a high speed curve allow to define precisely a low values and to be able to access high values at the same time. If you set a curve of 2 ot other even number, we'll take care to keep the sign (a negative number will stay negative)
- "strob": the output value will not be updated on each frame, but each "strob" seconds, ie if strob=1, each 1 second, if strob=0.1, 10 times per second.
- "bpm_sync": that's a handy one. The application has a global BPM, so if you set this parameter to "true", the speed and strob values will be interpreted as a divider or multiplier of the beat. For instance:
	* If your speed is 1, the output value should be increased by 1 each second
	* If BPM sync is on:
		* If speed is 1 and BPM is 60, the output value will also be increased by 1 each second
		* If speed is 0.5, speed will by quantised on a "power of 2" or "power of two divider" of one beat: 1/64, 1/32, 1/16, 1/8, 1/4, 1/2, 1, 2, 4, 8, 16, 32 ... and output value will be increased by "quantised_speed * elapsed_time * BPM/60"
- "link_speed_to_global_bpm": links the speed to MadMapper Global BPM (in Master settings tab). If this is set to "true", speed will be multiplied by global BPM / 120. It has no effect if "bpm_sync" is activated.

### Animator

	"animator" is a shape generator (Smooth, In, Out, Linear, Cut, Random) with many parameters. We made this case to avoid duplicating a lot of code in many materials, and so optimise shader code. It is based on the "time_base" generator above, so it has all the "time_base" parameters plus two others. It generates a value between 0 and 1.

Parameters:
- Parameters of time_base define how the animation goes
- "shape": possible values:
	* "Smooth": sinusoidal shape each cycle
	* "In": value goes from 0 to 1 in a cycle, then starts again
	* "Out": value goes from 1 to 0 in a cycle, then starts again
	* "Linear": value goes from 0 to 1 in half a cycle, then back to 0 in half a cycle
	* "Cut": value stays half a cycle at 0 then half a cycle at 1
	* "Noise": value will be animated by a Perlin noise

### Pass Thru

Useful to get a channel value (ie BPM value, BPM position, a MIDI/OSC/DMX channel ?) as a uniform, use pass_thru filter with the right channel URL as input.

	"GENERATORS": [
		{
			"NAME": "fx_bpm_position",
			"TYPE": "pass_thru",
			"PARAMS": {"input_value":"/custom/BPM/bpmPos"}
		}
		{
			"NAME": "fx_bpm_value",
			"TYPE": "pass_thru",
			"PARAMS": {"input_value": "/custom/BPM/bpm"}
		}
	]

### Damper filter

    "INPUTS": [
        {
        	"Label": "Width X",
        	"NAME": "fx_width_x",
        	"TYPE": "float",
        	"MIN": 0.0,
        	"MAX": 1.0,
        	"DEFAULT": 0.5
        }
    ],
	"GENERATORS": [
        {
            "NAME": "fx_width_x_damped",
            "TYPE": "damper",
            "PARAMS": {"input_value": "mat_width_x", "hardness": 0.1, "damping": 0.5}
        }
	]

Parameters:
- "hardness": floating point value, default: 0.1
- "damping": floating point value, default: 0.8

### Attack - Decay - Release filter

	"GENERATORS": [
	    {
	    	"NAME": "fx_adsr_switch",
			"TYPE": "adsr",
			"PARAMS": {"input_value": "fx_mask", "attack": 0.4, "release": 0.4, "decay": 0.0}
		}
	]

Useful one, like in audio application for keyboard->audio synth response, but without sustain cause that would have no sense in our case (no one is holding a key ;-).
Parameters:
- "attack": defines how fast output value can increase. If set to 0, when input value is higher than output value, output value will directly take the input value. If set to 1, it will need 10 seconds to go from 0 to 1. The maximum value increase at each computation is: maxIncrease = 10*pow(1-(0.01+attack*0.99),3) * elapsedSeconds
- "decay": when the input value in decreasing, time in seconds before we start decreasing output value
- "release": defines how fast output value can decrease. If set to 0, when input value is lower than output value (and decay time elapsed), output value will directly take the input value. If set to 1, it will need 10 seconds to go from 1 to 0. The maximum value increase at each computation is: maxDecrease = 10*pow(1-(0.01+release*0.99),3) * elapsedSeconds

### Linear Filter

	"GENERATORS": [
	    {
	    	"NAME": "fx_linear",
	    	"TYPE": "linear_filter",
	    	"PARAMS": {"input_value": "fx_value", "duration": 0.5}
	    }
	]

The "linear_filter" is used to make a linear interpolation from current value to new value in a specified duration. Each time the input value changes, the filter stored the current value and current time, output value will reach new value when the specified duration has elapsed.
Parameters:
- "duration": time in seconds to reach target position

### Ease Filter

	"GENERATORS": [
	    {
	    	"NAME": "fx_linear",
	    	"TYPE": "linear_filter",
	    	"PARAMS": {"input_value": "fx_value", "duration": 0.5}
	    }
	]

The "linear_filter" is used to make a linear interpolation from current value to new value in a specified duration. Each time the input value changes, the filter stored the current value and current time, output value will reach new value when the specified duration has elapsed.
Parameters:
- "type": "EaseInOut", "EaseIn" or "EaseOut" (default: "EaseInOut")
- "curve": defines the ease filter curve from 1 to 10 (default: 2)

### Multiplier

Multiplies up to 4 input values.

	"GENERATORS": [
	    {
	    	"NAME": "fx_multiplier",
			"TYPE": "multiplier",
			"PARAMS": {"value1": "fx_value", "value2": 2, "value3": 1, "value4": 1}
	    }
	]

Parameters:
- "value1", "value2", "value3", "value4", each input default is 1, the output is the multiplication of those 4 inputs.

### Incrementer

Increments or decrements a value.

 	"GENERATORS": [
		{
	    	"NAME": "fx_incrementer",
	      	"TYPE": "incrementer",
	      	"PARAMS": {"increment": "fx_plus_one", "decrement": "fx_less_one"}
	    }
	]

Parameters:
- "increment": should an "event" INPUT, each time it's triggered it will increase the output by one
- "decrement": should an "event" INPUT, each time it's triggered it will decrease the output by one


## Waveform and spectrum textures

We added some parameters compared to ISF spec. In the ISF spec, an "INPUT" of type "audioFFT" will be a texture with the audio spectrum. An "INPUT" with type "audio" will contain the waveform.

	{
	    "NAME": "fx_waveform",
	    "TYPE": "audio",
	    "SIZE": 512,
	},
	{
	    "NAME": "fx_spectrum_16_decay",
	    "TYPE": "audioFFT",
	    "SIZE": 16,
	    "ATTACK": 0.0,
	    "DECAY": 0.4,
	    "RELEASE": 0.0
	},
	{
	    "NAME": "fx_spectrum_16",
	    "TYPE": "audioFFT",
	    "SIZE": 16,
	    "ATTACK": 0.0,
	    "DECAY": 0.0,
	    "RELEASE": 0.2
	}

Additional settings compared to the spec:
- "SIZE": defines the size of the texture (number of values it will contain). 512 is the maximum. The texture resolution is SIZEx1 (height is always 1 pixel)
- "ATTACK" / "DECAY" / "RELEASE": on spectrum (audioFFT) you can set those parameters and we'll apply an ADR filter for each value of the spectrum texture

## Grouping input using LABEL with "/"

When you add an INPUT, it creates a widget with a label. You can organise those widgets into "Group Boxes" by setting your label to "GroupName/Parameter Label". The group order will be define by their appearance order in ISF header. Example:

  "LABEL": "Global/Line Width",
  "LABEL": "Global/Smooth",
  "LABEL": "Global/BPM Sync",
  "LABEL": "Noise/Noise",
  "LABEL": "Noise/Amount",
  "LABEL": "Noise/Speed",
  "LABEL": "Move/Auto Move",
  "LABEL": "Move/Size",
  "LABEL": "Move/Speed",
  "LABEL": "Move/Strob",
  "LABEL": "Scale/Auto Scale",
  "LABEL": "Scale/Size",
  "LABEL": "Scale/Speed",
  "LABEL": "Scale/Strob",
  "LABEL": "Scale/Shape",

Here we'll have 4 group boxes: Global, Noise, Move, Scale, in this order. And widgets inside each are also organised by the order of appearance in the ISF header. You can also add more depth, ie:

	"LABEL": "Scale/Animation/Active"

## Inputs flags

- Generate as define: for optimisation purpose, some "INPUT" uniforms can be handled differently by MadMapper. If you set the flag "generate_as_define" on an INPUT, we'll compile the shader with a #define added to specify the actual value of the INPUT. It's especially usefull with INPUTS of type "long" (enumeration). Then in the shader code, instead of writing "if (myValue == 0) {...} else ..." you write "#ifdef myValue_IS_FirstPossibleValue ... #endif". Check the LinePattern material:

	/*{
		"INPUTS": [
		    {
				"NAME": "fx_animation",
				"LABEL": "Animation",
				"TYPE": "long",
				"VALUES": ["Cross", "Centered Filled","Single","Filled"],
				"DEFAULT": "Cross",
				"FLAGS": "generate_as_define"
			}
		]
	}*/

	vec4 materialColorForPixel(vec2 texCoord)
	{
		#if defined(fx_animation_IS_Cross)
			...
		#elif defined(fx_animation_IS_Centered_Filled)
			...
		#elif defined(fx_animation_IS_Single)
			...
		#else // defined(fx_animation_IS_Filled)
			...
		#endif
		...
	}

You can also do it on a bool attribute:

        {
            "Label": "BassFX/Bass FX1",
            "NAME": "fx_bass_fx1",
            "TYPE": "bool",
            "DEFAULT": false,
            "FLAGS": "generate_as_define,button"
        }
        ...
	    #if defined(fx_bass_fx1_IS_true)
	    	...
	    #endif

- Button: if you declare a "bool" INPUT, it will make a check box widget. Setting the "button" flag will make a Push Button.

    {
      "NAME": "fx_autorotateactive",
      "LABEL": "Rotate/Auto Rotate",
      "TYPE": "bool",
      "DEFAULT": false,
      "FLAGS": "button"
    },

- SpinBox: if you declare an "int" or "float" INPUT, it will make a slider. Setting the "spinbox" flag, MadMapper will make a SpinBox.

    {
      "NAME": "fx_int_value",
      "LABEL": "Int Parameter",
      "TYPE": "int",
      "DEFAULT": 0,
      "MIN": 0,
      "MAX": 10,
      "FLAGS": "spinbox"
    },

- No Alpha: if you declare a "fx_color" INPUT, it will make Color Widget. Setting the "no_alpha" flag, the Colro widget will not handle alpha of the color, it will always be one, which is desired in some cases, ie in Chroma Key shader:

		{
			"LABEL": "Key Color",
			"NAME": "fx_color",
			"TYPE": "color",
			"DEFAULT": [ 0.0, 1.0, 0.0, 1.0 ],
			"FLAGS": "no_alpha"
		},


## Libraries

MadMapper is delivered with 2 major libraries developed by Simon Geilfus: MadNoise and MadSDF. Here is a quick explanation. MadNoise.glsl is a library to generate 2D and 3D noises in different ways. MadSDF.glsl is a library to make 2D shapes easily. SDF stands for Signed Distance Field.

### MadCommon

MadCommon.glsl provides a few common constants & functions: PI, rgb2hsv conversion etc. To use them, just add

	#include "MadCommon.glsl"

Full Header:

	//! Constants
	#define PI 3.1415926535897932384626433832795

	//! Returns the luminance of a RGB color
	float luma( vec3 color );

	//! Returns the luminance of a RGB color
	float luminance( vec3 color );

	//! Luminance based rgb8/linear conversion
	vec3 linearToRgb( vec3 color, vec3 range );

	//! Luminance based rgb8/linear conversion
	vec3 rgbToLinear( vec3 color, vec3 range )

	//! Contrast, saturation, brightness, For all settings: 1.0 = 100% 0.5=50% 1.5 = 150%
	vec3 applyContrastSaturationBrightness( vec3 color, float contrast, float saturation, float brightness )

	//! Returns HSV values for RGB input color
	vec3 rgb2hsv( vec3 color );

	//! Returns RGB values for HSV input color
	vec3 hsv2rgb( vec3 color );

	//! Returns the Hue value for RGB input color
	float rgb2hue( vec3 color );

	//! Usefull shortcuts
	#define saturate(x) clamp( x, 0.0, 1.0 )
	#define lerp(x,y,t) mix( x, y, t )

### MadNoise

MadNoise.glsl provides many noise functions. For instance, vnoise(xyPos) return a float depending on a vec2. To use them, just add

	#include "MadNoise.glsl"

Before your shader code, after ISF header. Most of them can be optimized using a Noise Lookup texture. To use this optimization, just write

	#define NOISE_TEXTURE_BASED
	#include "MadNoise.glsl"

And add the Noise lookup texture in the imported textures of your material:

	/*{
	    "CREDIT": "mm team",
	    "CATEGORIES": [ "Image Control" ],
	    "INPUTS": [
	    	...
	    ],
	    "IMPORTED": [
	        {
						"NAME": "noiseLUT",
					 	"PATH": "noiseLUT.png",
					 	"GL_TEXTURE_MIN_FILTER": "LINEAR",
					 	"GL_TEXTURE_MAG_FILTER": "LINEAR",
						"GL_TEXTURE_WRAP": "REPEAT"
					}
	    ]
	}/*

Full Header:

	//! Returns a 2D value noise in the range [0,1]
	float vnoise( vec2 p );
	//! Returns a 3D value noise in the range [0,1]
	float vnoise( vec3 p );

	//! Returns a 2D value noise with derivatives in the range [0,1]
	vec3 dvnoise( vec2 p );
	//! Returns a 3D value noise with derivatives in the range [0,1]
	vec4 dvnoise( vec3 p );

	//! Returns a 2D simplex noise in the range [-1,1]
	float noise( vec2 p );
	//! Returns a 3D simplex noise in the range [-1,1]
	float noise( vec3 p );

	//! Returns a 2D simplex noise with derivatives in the range [-1,1]
	vec3 dnoise( vec2 p );
	//! Returns a 3D simplex noise with derivatives in the range [-1,1]
	vec4 dnoise( vec3 p );

	#if ! defined( NOISE_WORLEY_ASHIMA_IMPL )
	//! Returns a 2D worley/cellular noise in the range [0,1]
	float worleyNoise( vec2 p );
	//! Returns a 3D worley/cellular noise in the range [0,1]
	float worleyNoise( vec3 p );
	#else
	//! Returns a 2D worley/cellular noise F1 and F2 in the range [0,1]
	vec2 worleyNoise( vec2 p );
	//! Returns a 3D worley/cellular noise F1 and F2 in the range [0,1]
	vec2 worleyNoise( vec3 p );
	#endif

	//! Returns a 2D worley/cellular noise with derivatives in the range [0,1]
	vec3 dWorleyNoise( vec2 p );
	//! Returns a 3D worley/cellular noise with derivatives in the range [0,1]
	vec4 dWorleyNoise( vec3 p );

	//! Returns a 2D simplex noise with rotating gradients in the range [-1,-1]
	float flowNoise( vec2 pos, float rot );
	//! Returns a 2D simplex noise with rotating gradients and derivatives in the range [-1,-1]
	vec3 dFlowNoise( vec2 pos, float rot );

	//! Returns a 2D Billowed Noise in the range [0,1]
	float billowedNoise( vec2 p );
	//! Returns a 3D Billowed Noise in the range [0,1]
	float billowedNoise( vec3 p );

	//! Returns a 2D Billowed Noise with Derivatives in the range [0,1]
	vec3 dBillowedNoise( vec2 p );
	//! Returns a 3D Billowed Noise with Derivatives in the range [0,1]
	vec4 dBillowedNoise( vec3 p );

	//! Returns a 2D Ridged Noise in the range [0,1]
	float ridgedNoise( vec2 p );
	//! Returns a 2D Ridged Noise in the range [0,1]
	float ridgedNoise( vec2 p, float ridge );
	//! Returns a 3D Ridged Noise in the range [0,1]
	float ridgedNoise( vec3 p );
	//! Returns a 3D Ridged Noise in the range [0,1]
	float ridgedNoise( vec3 p, float ridge );

	//! Returns a 2D Ridged Noise with Derivatives in the range [0,1]
	vec3 dRidgedNoise( vec2 p );
	//! Returns a 2D Ridged Noise with Derivatives in the range [0,1]
	vec3 dRidgedNoise( vec2 p, float ridge );
	//! Returns a 3D Ridged Noise with Derivatives in the range [0,1]
	vec4 dRidgedNoise( vec3 p );
	//! Returns a 3D Ridged Noise with Derivatives in the range [0,1]
	vec4 dRidgedNoise( vec3 p, float ridge );

	//! Returns a 2D Curl of a Simplex Noise in the range [-1,1]
	vec2 curlNoise( vec2 v );
	//! Returns a 2D Curl of a Simplex Flow Noise in the range [-1,1]
	vec2 curlNoise( vec2 v, float t );

	//! Returns a 2D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
	float fBm( vec2 p );
	//! Returns a 2D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
	float fBm( vec2 p, int octaves, float lacunarity, float gain );
	//! Returns a 3D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
	float fBm( vec3 p );
	//! Returns a 3D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
	float fBm( vec3 p, int octaves, float lacunarity, float gain );

	//! Returns a 2D Fractal Brownian Motion sum of Simplex Noise in the range [-1,1]
	vec3 dfBm( vec2 p );
	//! Returns a 2D Fractal Brownian Motion sum of Simplex Noise with Derivatives in the range [-1,1]
	vec3 dfBm( vec2 p, int octaves, float lacunarity, float gain );
	//! Returns a 3D Fractal Brownian Motion sum of Simplex Noise with Derivatives in the range [-1,1]
	vec4 dfBm( vec3 p );
	//! Returns a 3D Fractal Brownian Motion sum of Simplex Noise with Derivatives in the range [-1,1]
	vec4 dfBm( vec3 p, int octaves, float lacunarity, float gain );

	//! Returns a 2D Fractal Brownian Motion sum of Billowed Noise in the range [-1,1]
	float billowyTurbulence( vec2 p );
	//! Returns a 2D Fractal Brownian Motion sum of Billowed Noise in the range [-1,1]
	float billowyTurbulence( vec2 p, int octaves, float lacunarity, float gain );
	//! Returns a 3D Fractal Brownian Motion sum of Billowed Noise in the range [-1,1]
	float billowyTurbulence( vec3 p );
	//! Returns a 3D Fractal Brownian Motion sum of Billowed Noise in the range [-1,1]
	float billowyTurbulence( vec3 p, int octaves, float lacunarity, float gain );

	//! Returns a 2D Fractal Brownian Motion sum of Billowed Noise with Derivatives in the range [-1,1]
	vec3 dBillowyTurbulence( vec2 p );
	//! Returns a 2D Fractal Brownian Motion sum of Billowed Noise with Derivatives in the range [-1,1]
	vec3 dBillowyTurbulence( vec2 p, int octaves, float lacunarity, float gain );
	//! Returns a 3D Fractal Brownian Motion sum of Billowed Noise with Derivatives in the range [-1,1]
	vec4 dBillowyTurbulence( vec3 p );
	//! Returns a 3D Fractal Brownian Motion sum of Billowed Noise with Derivatives in the range [-1,1]
	vec4 dBillowyTurbulence( vec3 p, int octaves, float lacunarity, float gain );

	//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
	float ridgedMF( vec2 p );
	//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
	float ridgedMF( vec2 p, float ridgeOffset );
	//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
	float ridgedMF( vec2 p, float ridgeOffset, int octaves, float lacunarity, float gain );
	//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
	float ridgedMF( vec3 p );
	//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
	float ridgedMF( vec3 p, float ridgeOffset );
	//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
	float ridgedMF( vec3 p, float ridgeOffset, int octaves, float lacunarity, float gain );

	//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise in the range [-1,1]
	vec3 dRidgedMF( vec2 p );
	//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
	vec3 dRidgedMF( vec2 p, float ridgeOffset );
	//! Returns a 2D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
	vec3 dRidgedMF( vec2 p, float ridgeOffset, int octaves, float lacunarity, float gain );
	//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
	vec4 dRidgedMF( vec3 p );
	//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
	vec4 dRidgedMF( vec3 p, float ridgeOffset );
	//! Returns a 3D Ridged Multi-Fractal sum of Simplex Noise with Derivatives in the range [-1,1]
	vec4 dRidgedMF( vec3 p, float ridgeOffset, int octaves, float lacunarity, float gain );

### MadSDF

Check [Libraries/MadSDF.md](Libraries/MadSDF.md) for more info

## Unofficial features

Caution: you shouldn't use those features except if you have very specific needs and you're doing it only for you - NOT FOR SHARING with other people

When writing a Material in MadMapper, you can test what kind of surface is actually using it using those definitions:

- Quads: #ifdef SURFACE_IS_quad
- Circle: #ifdef SURFACE_IS_circle
- Lines: #ifdef SURFACE_IS_lines
- Triangle: #ifdef SURFACE_IS_triangle
- Surface3D: #ifdef SURFACE_IS_surface3D
- DMX Fixture: #ifdef SURFACE_IS_fixture
- DMX Fixture Line: #ifdef SURFACE_IS_fixture_line

You can specify GLSL version in ISF header. By default we use version "150 core" (june 2017, version 3.0.0).

	/*{
	    "CREDIT": "mm team",
	    "CATEGORIES": [ "Image Control" ],
	    "GLSL_VERSION": "150 core",
	    ...
	}/*
