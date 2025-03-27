/*{
    "CREDIT": "gllama / shadertoy 4XG3WV",
    "DESCRIPTION": "4d noise for LEDs",
    "TAGS": "noise",
    "VSN": "1.0",
    "INPUTS": [ 
		{"LABEL": "Speed", "NAME": "mat_speed", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Scale", "NAME": "mat_scale", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.0 }, 
		{"LABEL": "Smoothness", "NAME": "mat_smoothness", "TYPE": "float", "MIN": 0.0, "MAX": 10.0, "DEFAULT": 1.5 }, 
		{"LABEL": "Roundness", "NAME": "mat_simplify", "TYPE": "float", "MIN": 0.0, "MAX": 1.0, "DEFAULT": 0.5 }, 

		{"LABEL": "Move X", "NAME": "mat_mx", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Move Y", "NAME": "mat_my", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.0 }, 
		{"LABEL": "Move Z", "NAME": "mat_mz", "TYPE": "float", "MIN": 0.0, "MAX": 2.0, "DEFAULT": 0.1 }, 


	{ "LABEL": "Color/Invert", "NAME": "mat_invert", "TYPE": "bool", "DEFAULT": false,"FLAGS": "button" }, 
    { "LABEL": "Color/Back Color", "NAME": "mat_backgroundColor", "TYPE": "color", "DEFAULT": [ 0.0, 0.0, 0.0, 1.0 ], "FLAGS": "no_alpha" },
    { "LABEL": "Color/Front Color", "NAME": "mat_foregroundColor", "TYPE": "color", "DEFAULT": [ 1.0, 1.0, 1.0, 1.0 ], "FLAGS": "no_alpha" }, 
    { "LABEL": "Color/Brightness", "NAME": "mat_brightness", "TYPE": "float", "MIN": -1.0, "MAX": 1.0, "DEFAULT": 0.0 },
    { "LABEL": "Color/Contrast", "NAME": "mat_contrast", "TYPE": "float", "MIN": 0.0, "MAX": 4.0, "DEFAULT": 1. },
   
    ],
	"GENERATORS": [
        {"NAME": "mat_time", "TYPE": "time_base", "PARAMS": {"speed": "mat_speed"} },
    ],
}*/
// https://www.shadertoy.com/view/4XG3WV

const float PI = 3.1415926535897932384626433832795;

int Seed = 70; // < Changes your current Voronoi Cell // Some of my favorite seeds [-1000]

float  ofs = .0001; // Alters 4D displacement 

// Voronoi Stuff
#define hash44(p) fract(18.5453 * sin(p * mat4(127.1, 311.7, 269.5, 183.3, 421.1, 547.6, 231.3, 298.4, 87.6, 123.4, 356.1, 654.3, 765.1, 876.4, 132.5, 234.8)))
#define disp(p) ( -ofs + (1.0 + 2.0 * ofs) * hash44(p) )

// Hash function for 3D to 1D
#define hash31(p) fract(sin(dot(p, vec3(127.1, 311.7, 74.7))) * 43758.5453123)

// Noise function for 3D
float noise3d(vec3 p) {
    vec3 i = floor(p);
    vec3 f = fract(p); f = f * f * (3.0 - 2.0 * f);

    float v = mix(
                mix(
                    mix(hash31(i + vec3(0, 0, 0)), hash31(i + vec3(1, 0, 0)), f.x),
                    mix(hash31(i + vec3(0, 1, 0)), hash31(i + vec3(1, 1, 0)), f.x), f.y),
                mix(
                    mix(hash31(i + vec3(0, 0, 1)), hash31(i + vec3(1, 0, 1)), f.x),
                    mix(hash31(i + vec3(0, 1, 1)), hash31(i + vec3(1, 1, 1)), f.x), f.y), f.z);
    
    return 1.0 - abs(2.0 * v - 1.0);
}


// Rotation matrix for 3D (rotation around z-axis)
mat3 rot3(float angle) {
    float c = cos(angle), s = sin(angle);
    return mat3(
        c, -s, 0,
        s,  c, 0,
        0,  0, 1
    );
}


// We bastardize FBM a bit here -- performance vs speed trade-off
vec3 poor_mans_fbm33(vec3 p) {
    vec3 v = vec3(0.0);
    float a = 0.5;
    mat3 R = rot3(0.37);
    for (int i = 0; i < 6; i++, p *= 2.0, a /= 2.0) {
        p = R * p;
        // Dirty trick to reduce noise-call to once per octave
        float n = noise3d(p);
        float nn = n*n;
        v += a * vec3(n,nn,nn*n);
    }
    return v;
}


float voronoi4D_edge_distance(vec4 u) 
{
	
	vec3 Movement_3D = vec3(mat_mx,mat_my,mat_mz); // The direction & speed of movement through XYZ of 4D voronoi space (Higher values = faster speed)

    // We triangle-wave loop through u.w which "breathes" our current Voronoi-Cell
    u.w *= 0.1;
    u.w = 1.0 - 2.0 * abs(fract(u.w + 0.5) - 0.5);
    u.w *= 0.99;
    
    
    u.w += float(Seed); // Apply current Voronoi-Cell Seed
    u.xyz += Movement_3D*mat_time; // Apply XYZ Movement-Vector for 3D movement through the pattern
    
    // Get nearest 4D edge-distance 
    vec4 iu = floor(u);
    float m = 1e9, m2 = 1e9;
    vec4 P, P2;
    
    vec4 iu_offset = iu - 0.5;
    vec4 p, o, r;
    float d;
    
    // Loop over a 2x2x2x2 grid of cells around the current cell
    for (int k = 0; k < 6; k++) {
        p = iu_offset + vec4(k & 1, (k >> 1) & 1, (k >> 2) & 1, (k >> 3) & 1); // Using bitwise operations for faster modulus and division
        o = disp(p);  // Get the displacement for this cell
        r = p - u + o;
        d = dot(r, r);

        if (d < m) {
            m2 = m;
            P2 = P;
            m = d;
            P = r;
        } else if (d < m2 && dot(P - r, P - r) > 1e-5) {
            m2 = d;
            P2 = r;
        }
    }
    
    // Calculate the final value using the closest cells found
    if (m2 < 1e9) {
        m2 = 0.5 * dot((P + P2), normalize(P2 - P));
    }

    return m2;
}
// UV coords to 3D space
vec3 uvToCartesian3D(vec2 uv) {
    // Convert UV to spherical coords
    float theta = uv.x * 2.0 * 3.14159265359; // Longitude
    float phi = uv.y * 3.14159265359; // Latitude
    // Convert Spherical Coords to 
    float x = sin(phi) * cos(theta);
    float y = sin(phi) * sin(theta);
    float z = cos(phi);
    return vec3(x, y, z);
}

vec4 materialColorForPixel( vec2 texCoord )
{

	vec2 uv = texCoord.xy;
    vec3 pos = uvToCartesian3D(uv);
	pos *= mat_scale;
	vec3 f33=poor_mans_fbm33(pos/mat_smoothness);

	vec3 coo = mix(pos,f33,vec3(mat_simplify));
	float G = voronoi4D_edge_distance(vec4(coo, mat_time*0.45+0.334));

    // Apply brightness
    G += mat_brightness;
    // Apply contrast
    G = mix(0.5, G, mat_contrast);
	G = clamp(G,0.,1.);

	if (mat_invert) G = 1 - G;

	vec3 color = mix( mat_backgroundColor.rgb, mat_foregroundColor.rgb, G );


	vec4 col = vec4(color,1.);
	return col;
}