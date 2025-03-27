/*{
        "RESOURCE_TYPE": "Material For MadMapper",
    "CREDIT": "1024 architecture\nadapted from Shane\nShaderToy XlBXWw",
    "TAGS": "texture",
    "INPUTS": [ 
		{ "Label": "Speed ", "NAME": "speed", "TYPE": "float", "MIN": -2.0, "MAX": 2.0, "DEFAULT": 0.8 },
		{ "Label": "Scale ", "NAME": "scale", "TYPE": "float", "MIN": 0.0, "MAX": 5.0, "DEFAULT": 1.0 },
		{ "Label": "Sharpness ", "NAME": "uSharp", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "Label": "Rotation ", "NAME": "uRotation", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.1 },
		{ "LABEL": "Pan", "NAME": "uPan", "TYPE": "point2D", "MAX": [ 4.0, 4.0 ], "MIN": [ -4.0, -4.0 ], "DEFAULT": [ 0.0, 0.0 ] },
        { "LABEL": "Color/R", "NAME": "uR", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.2 },
		{ "LABEL": "Color/G", "NAME": "uG", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/B", "NAME": "uB", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 1.0 },
		{ "LABEL": "Color/Brightness", "NAME": "brightness", "TYPE": "float", "MIN" : -1.0, "MAX" : 1.0, "DEFAULT": 0.0 },
        { "LABEL": "Color/Contrast", "NAME": "contrast", "TYPE": "float", "MIN" : 0.0, "MAX" : 5.0, "DEFAULT": 1.0 },
      ],
	"GENERATORS": [
        {"NAME": "animation_time", "TYPE": "time_base", "PARAMS": {"speed": "speed", "speed_curve":2, "link_speed_to_global_bpm":true}},
    ],
}*/

#include "MadCommon.glsl"
#include "MadNoise.glsl"
	
vec2 hash22(vec2 p) { 

    // Faster, but doesn't disperse things quite as nicely. However, when framerate
    // is an issue, and it often is, this is a good one to use. Basically, it's a tweaked 
    // amalgamation I put together, based on a couple of other random algorithms I've 
    // seen around... so use it with caution, because I make a tonne of mistakes. :)
    float n = sin(dot(p, vec2(41, 289)));
    return fract(vec2(262144, 32768)*n); 
    
    // Animated.
    //p = fract(vec2(262144, 32768)*n); 
    //return sin( p*6.2831853 + time )*0.5 + 0.5; 
    
}

// One of many 2D Voronoi algorithms getting around, but all are based on IQ's 
// original. I got bored and roughly explained it. It was a slow day. :) The
// explanations will be obvious to many, but not all.
float Voronoi(vec2 p)
{	
    // Partitioning the 2D space into repeat cells.
    vec2 ip = floor(p); // Analogous to the cell's unique ID.
    p = fract(p); // Fractional reference point within the cell.

    // Set the minimum distance (squared distance, in this case, because it's 
    // faster) to a maximum of 1. Outliers could reach as high as 2 (sqrt(2)^2)
    // but it's being capped to 1, because it covers a good portion of the range
    // (basically an inscribed unit circle) and dispenses with the need to 
    // normalize the final result.
    //
    // If you're finding that your Voronoi patterns are a little too contrasty,
    // you could raise "d" to something like "1.5." Just remember to divide
    // the final result by the same amount.
    float d = 1.;
    
    // Put a "unique" random point in the cell (using the cell ID above), and it's 8 
    // neighbors (using their cell IDs), then check for the minimum squared distance 
    // between the current fractional cell point and these random points.
    for (float i = -1.; i < 1.1; i++){
	    for (float j = -1.; j < 1.1; j++){
	    
     	    vec2 cellRef = vec2(i, j); // Base cell reference point.
            
            vec2 offset = hash22(ip + cellRef); // 2D offset.
            
            // Vector from the point in the cell to the offset point.
            vec2 r = cellRef + offset - p; 
            float d2 = dot(r, r); // Squared length of the vector above.
            
            d = min(d, d2); // If it's less than the previous minimum, store it.
        }
    }
    
    // In this case, the distance is being returned, but the squared distance
    // can be used too, if prefered.
    return sqrt(d); 
}


vec4 materialColorForPixel( vec2 texCoord )
{
	    // Screen coordinates.
	vec2 uv = texCoord*2.0 - vec2(1);
	uv *= scale;
	uv += uPan;

    // Variable setup, plus rotation.
	float t = animation_time, s, a, b, e;   
    
    // Rotation the canvas back and forth.
    float th = uRotation*sin(animation_time*0.1)*cos(animation_time*0.13)*40.;
    float cs = cos(th), si = sin(th);
    uv *= mat2(cs, -si, si, cs);
    
    // Setup: I find 2D bump mapping more intuitive to pretend I'm raytracing, then lighting a bump mapped plane 
    // situated at the origin. Others may disagree. :)  
    vec3 sp = vec3(uv, 0); // Surface posion. Hit point, if you prefer. Essentially, a screen at the origin.
    vec3 ro = vec3(0, 0, -1); // Camera position, ray origin, etc.
    vec3 rd = normalize(sp-ro); // Unit direction vector. From the origin to the screen plane.
    vec3 lp = vec3(cos(animation_time)*0.375, sin(animation_time)*0.1, -1.); // Light position - Back from the screen.
 
    
    // The number of layers. More gives you a more continous blend, but is obviously slower.
    // If you change the layer number, you'll proably have to tweak the "gFreq" value.
    const float L = 8.;
     // Global layer frequency, or global zoom, if you prefer.
    const float gFreq = 0.5;
    float sum = 0.; // Amplitude sum, of sorts.
    
    
    // Setting up the layer rotation matrix, used to rotate each layer.
    // Not completely necessary, but it helps mix things up. It's standard practice, but 
    // this one is based on Fabrice's example.
    th = 3.14159265*0.7071/L;
    cs = cos(th), si = sin(th);
    mat2 M = mat2(cs, -si, si, cs);
    
    
    // The overall scene color. Initiated to zero.
    vec3 col = vec3(0);
	
	
	   // Setting up the bump mapping variables and initiating them to zero.
    // f - Function value
    // fx - Change in "f" in in the X-direction.
    // fy - Change in "f" in in the Y-direction.
    float f=0., fx=0., fy=0.;
    vec2 eps = vec2(4./(10+1500.0*uSharp), 0.);
    
    // I've had to off-center this just a little to avoid an annoying white speck right
    // in the middle of the canvas. If anyone knows how to get rid of it, I'm all ears. :)
    vec2 offs = vec2(0.1);
    
    
    // Infinite Zoom.
    //
    // The first three lines are a little difficult to explain without describing what infinite 
    // zooming is in the first place. A lot of it is analogous to fBm. Sum a bunch of increasing
    // frequencies with decreasing amplitudes.
    //
    // Anyway, the effect is nothing more than a series of layers being expanded from an initial
    // size to a final size, then being snapped back to its original size to repeat the process 
    // again. However, each layer is doing it at diffent incremental stages in time, which tricks
    // the brain into believing the process is continuous. If you wish to spoil the illusion, 
    // simply reduce the layer count. If you wish to completely ruin the effect, set it to one.
	
    // Infinite zoom loop.
	for (float i = 0.; i<L; i++){
	
        // Fractional time component. Obviously, incremented by "1./L" and ranging from
        // zero to one, whist on a repeat cycle.
		s = fract((i - t*2.)/L);
        
        // Using the fractional time component to determine the layer frequency. It increases
        // with time, then snaps back to one in a cyclic fashion.
        // Note that exp2(t) is just another way to write pow(2., s). The latter is more
        // intuitive, but slower... Well, I assume it is.
        e = exp2(s*L)*gFreq; // Range (approx): [ 1, pow(2., L)*gFreq ]
        
        // Layer ampltude component. Inversely propotional to the frequency, which makes sense.
        // Because the layers are finite, you need to smoothly interpolate between them, 
        // and the "cos" setup below is just one of many ways to do it.
        a = (1.-cos(s*6.283))/e;  // Smooth transition.
        //a = (1. - abs(s-.5)*2.)/e; // Alternative linear fade. Not as smooth, or accurate.
        //a = (1. - abs(s-.5)*2.); a *= a*(3.-2.*a)/e; // Smooth linear fade, if so desired.
        
        
        // Accumulating each layer.
        
        // I had to have a bit of a think as to how to bump map this. Normally, you'd write a function
        // then call it three times, but that'd be too expensive, so it's all being done simultaneously.
        //
        // Either way, it's still pretty simple. In addition to accumulating the pixel value, accumulate 
        // sample values just to the left of it and above. The X-gradient and Y-gradient can then be 
        // determined outside the loop.
        //
        f += Voronoi(M*sp.xy*e + offs) * a; // Sample value multiplied by the amplitude.
        fx += Voronoi(M*(sp.xy-eps.xy)*e + offs) * a; // Same for the nearby sample in the X-direction.
        fy += Voronoi(M*(sp.xy-eps.yx)*e + offs) * a; // Same for the nearby sample in the Y-direction.
        
        // Sum each amplitude. Used to normalize the results once the loop is complete.
        sum += a;
        
        // Rotating each successive layer is pretty standard, but this is the way Fabrice does
        // it.
        M *= M;

	}	
	
	   // I doubt it'd happen, but just in case sum is zero.
    sum = max(sum, 0.001);
    
    // Normalizing the three Voronoi samples.
    f /= sum;
    fx /= sum;
    fy /= sum;
   
    // Common bump mapping stuff.
    float bumpFactor = 0.2;
    // Using the above to determine the dx and dy function gradients.
    fx = (fx-f)/eps.x; // Change in X
    fy = (fy-f)/eps.x; // Change in Y.
    // Using the gradient vector, "vec3(fx, fy, 0)," to perturb the XY plane normal ",vec3(0, 0, -1)."
    vec3 n = normalize( vec3(0, 0, -1) + vec3(fx, fy, 0)*bumpFactor );           
    
	// Determine the light direction vector, calculate its distance, then normalize it.
	vec3 ld = lp - sp;
	float lDist = max(length(ld), 0.001);
	ld /= lDist;  

    // Light attenuation.    
    float atten = 1.25/(1. + lDist*0.15 + lDist*lDist*0.15);
	//float atten = min(1./(dist*dist*2.), 1.);	

	// Diffuse value.
	float diff = max(dot(n, ld), 0.);  
    // Enhancing the diffuse value a bit. Made up.
    diff = pow(diff, 2.)*0.66 + pow(diff, 4.)*0.34; 
    // Specular highlighting.
    float spec = pow(max(dot( reflect(-ld, n), -rd), 0.), 16.); 
	
	vec3 objCol = vec3(pow(f,uR*10.0), pow(f, uG*10.0), pow(f, uB*10.0));   

    // Using the values above to produce the final color.   
    col = (objCol * (diff + .5) + vec3(.4, .6, 1.)*spec*1.5) * atten;

	vec4 finalColor = vec4(sqrt(min(col, 1.)), 1.);
	
		//////// brightness + contrast
	// Apply contrast
    finalColor.rgb = mix(vec3(0.5), finalColor.rgb, contrast);
    // Apply brightness
    finalColor.rgb += vec3(brightness);
	
	return finalColor;
}
